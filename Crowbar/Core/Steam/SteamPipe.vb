Imports System.ComponentModel
Imports System.IO
Imports System.IO.Pipes

Public Class SteamPipe

#Region "Create and Destroy"

#End Region

#Region "Init and Free"

#End Region

#Region "Properties"

#End Region

#Region "Methods"

	'NOTE: Start a process that does the steamworks stuff, so that when that process ends, 
	'      Steam will stop showing the game as status and the AppID can be changed.
	'NOTE: Use Named Pipes to send info between the processes.
	' Parameters:
	'      reasontext - A short phrase such as "Publishing item" or "Getting item details" to be used in opening and closing connection messages.
	Public Function Open(ByVal pipeNameSuffix As String, ByVal bw As BackgroundWorker, ByVal reasonText As String) As String
		Me.theBackgroundWorker = bw
		Me.theReasonText = reasonText

		If Me.theBackgroundWorker IsNot Nothing Then
			Dim logMessage As String = Me.theReasonText + " - opening connection to Steam Workshop." + vbCrLf
			Me.theBackgroundWorker.ReportProgress(0, logMessage)
		End If

		Me.theCrowbarSteamPipeProcess = New Process()
		Try
			Me.theCrowbarSteamPipeProcess.StartInfo.UseShellExecute = False
			Me.theCrowbarSteamPipeProcess.StartInfo.FileName = App.CrowbarSteamPipeFileName
			Me.theCrowbarSteamPipeProcess.StartInfo.Arguments = pipeNameSuffix
#If DEBUG Then
			Me.theCrowbarSteamPipeProcess.StartInfo.CreateNoWindow = False
#Else
			Me.theCrowbarSteamPipeProcess.StartInfo.CreateNoWindow = True
#End If
			Me.theCrowbarSteamPipeProcess.Start()
		Catch ex As Exception
			Console.WriteLine("EXCEPTION: " + ex.Message)
		Finally
		End Try

		Me.theCrowbarSteamPipeServer = New NamedPipeServerStream("CrowbarSteamPipe" + pipeNameSuffix, PipeDirection.InOut, 1)
		Console.WriteLine("Waiting for client to connect to pipe ...")
		Me.theCrowbarSteamPipeServer.WaitForConnection()
		Console.WriteLine("... Client connected to pipe.")

		Me.theStreamWriter = New StreamWriter(Me.theCrowbarSteamPipeServer)
		Me.theStreamWriter.AutoFlush = True
		Me.theStreamReader = New StreamReader(Me.theCrowbarSteamPipeServer)
		Try
			'If Me.theBackgroundWorker IsNot Nothing Then
			'	Dim logMessage As String = "Connecting to Steam Workshop." + vbCrLf
			'	Me.theBackgroundWorker.ReportProgress(0, logMessage)
			'End If
			Me.theStreamWriter.WriteLine("Init")
			Console.WriteLine("Command: Init")

			Dim result As String = Me.theStreamReader.ReadLine()
			Console.WriteLine("Result: " + result)
			If Me.theBackgroundWorker IsNot Nothing Then
				If result <> "success" Then
					Dim logMessage As String
					'NOTE: Reaches this code when user tries to download a workshop item of a game user does not own.
					'      Can not determine if user owns game here because can not Init Steam.
					'Me.theStreamWriter.WriteLine("SteamApps_BIsSubscribedApp")
					'Dim resultOfBIsSubscribedApp As String = Me.theStreamReader.ReadLine()
					'If resultOfBIsSubscribedApp = "success" Then
					'	logMessage = "Connection to Steam Workshop failed. This most likely means you need to login to Steam." + vbCrLf
					'Else
					'	logMessage = "Connection to Steam Workshop failed because you do not own the app or game to which this item belongs." + vbCrLf
					'End If
					logMessage = "Connection to Steam Workshop failed. This most likely means you need to login to Steam or you do not own the app or game to which this item belongs." + vbCrLf
					Me.theBackgroundWorker.ReportProgress(0, logMessage)
					If Me.theCrowbarSteamPipeServer IsNot Nothing Then
						Console.WriteLine("Closing pipe due to error.")
						Me.theCrowbarSteamPipeServer.Close()
						Me.theCrowbarSteamPipeServer = Nothing
					End If
					Return "error"
					'Else
					'	Dim logMessage As String = "Connection to Steam Workshop initialized." + vbCrLf
					'	Me.theBackgroundWorker.ReportProgress(0, logMessage)
				End If
			End If
		Catch ex As IOException
			Console.WriteLine("EXCEPTION: " + ex.Message)
			If Me.theCrowbarSteamPipeServer IsNot Nothing Then
				Console.WriteLine("Closing pipe due to error.")
				Me.theCrowbarSteamPipeServer.Close()
				Me.theCrowbarSteamPipeServer = Nothing
			End If
		Finally
		End Try

		Return "success"
	End Function

	Public Sub Shut()
		Try
			Me.theStreamWriter.WriteLine("Free")
			Console.WriteLine("Command: Free")
			If Me.theBackgroundWorker IsNot Nothing Then
				Dim logMessage As String = Me.theReasonText + " finished - closing connection to Steam Workshop." + vbCrLf
				Me.theBackgroundWorker.ReportProgress(0, logMessage)
			End If
		Catch ex As IOException
			Console.WriteLine("EXCEPTION: " + ex.Message)
			If Me.theCrowbarSteamPipeServer IsNot Nothing Then
				Console.WriteLine("Closing pipe due to error.")
				Me.theCrowbarSteamPipeServer.Close()
				Me.theCrowbarSteamPipeServer = Nothing
			End If
		Finally
#If DEBUG Then
			'NOTE: This 'If DEBUG Then' block allows the CrowbarSteamPipe console window to stay open, if it is set to do that. 
			If Me.theCrowbarSteamPipeServer IsNot Nothing Then
				Console.WriteLine("Closing pipe.")
				Me.theCrowbarSteamPipeServer.Close()
				Me.theCrowbarSteamPipeServer = Nothing
			End If
			If Me.theCrowbarSteamPipeProcess IsNot Nothing Then
				Me.theCrowbarSteamPipeProcess.Close()
				Me.theCrowbarSteamPipeProcess = Nothing
			End If
#Else
			Me.Kill()
#End If
		End Try
	End Sub

	Public Sub Kill()
		If Me.theCrowbarSteamPipeServer IsNot Nothing Then
			Console.WriteLine("Closing pipe.")
			Me.theCrowbarSteamPipeServer.Close()
			Me.theCrowbarSteamPipeServer = Nothing
		End If
		If Me.theCrowbarSteamPipeProcess IsNot Nothing Then
			Try
				If Not Me.theCrowbarSteamPipeProcess.HasExited AndAlso Not Me.theCrowbarSteamPipeProcess.CloseMainWindow() Then
					Console.WriteLine("Killing pipe process.")
					Me.theCrowbarSteamPipeProcess.Kill()
				End If
			Catch ex As Exception
				Dim debug As Integer = 4242
			Finally
				If Me.theCrowbarSteamPipeProcess IsNot Nothing Then
					Me.theCrowbarSteamPipeProcess.Close()
					Me.theCrowbarSteamPipeProcess = Nothing
				End If
			End Try
		End If
	End Sub

#Region "Crowbar"

	Public Function Crowbar_DeleteContentFile(ByVal itemID_text As String) As String
		Me.theStreamWriter.WriteLine("Crowbar_DeleteContentFile")
		Me.theStreamWriter.WriteLine(itemID_text)
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

	Public Function Crowbar_DownloadContentFolderOrFile(ByVal itemID_text As String, ByVal targetPath As String, ByRef contentFileBytes As Byte(), ByRef itemUpdated_Text As String, ByRef itemTitle As String, ByRef contentFolderOrFileName As String, ByRef appID_Text As String) As String
		Me.theStreamWriter.WriteLine("Crowbar_DownloadContentFolderOrFile")
		Me.theStreamWriter.WriteLine(itemID_text)
		Me.theStreamWriter.WriteLine(targetPath)

		Dim result As String = Me.theStreamReader.ReadLine()
		If result = "success" Then
			itemUpdated_Text = Me.theStreamReader.ReadLine()
			itemTitle = Me.ReadMultipleLinesOfText(Me.theStreamReader)
			contentFolderOrFileName = Me.theStreamReader.ReadLine()
			Dim byteCount As Integer
			byteCount = CInt(Me.theStreamReader.ReadLine())
			If byteCount > 0 Then
				Dim batchByteCount As Integer = 1024
				Dim batchData As Byte() = New Byte(batchByteCount - 1) {}
				contentFileBytes = New Byte(byteCount - 1) {}
				Dim byteOffset As Integer = 0
				Dim bytesRemaining As Integer = byteCount
				Dim length As Integer
				Try
					Dim outputInfo As New BackgroundSteamPipe.DownloadItemOutputInfo()
					'Me.theStreamReader.BaseStream.Read(data, 0, data.Length)
					While bytesRemaining > 0
						'bw.ReportProgress(0, "Read" + vbCrLf)
						'length = Me.theStreamReader.BaseStream.Read(batchData, byteOffset, batchByteCount)
						length = Me.theStreamReader.BaseStream.Read(batchData, 0, batchByteCount)
						'bw.ReportProgress(0, "CopyTo: " + contentFileBytes.Length.ToString() + " offset = " + byteOffset.ToString() + vbCrLf)
						Array.Copy(batchData, 0, contentFileBytes, byteOffset, length)

						If bytesRemaining < batchByteCount Then
							outputInfo.BytesReceived = bytesRemaining
						Else
							outputInfo.BytesReceived = batchByteCount
						End If
						outputInfo.TotalBytesToReceive = byteCount
						Me.theBackgroundWorker.ReportProgress(1, outputInfo)

						byteOffset += length
						bytesRemaining -= batchByteCount
					End While
				Catch ex As Exception
					Me.theBackgroundWorker.ReportProgress(0, "WARNING: Unable to get content folder or file. Exception raised: " + ex.Message + vbCrLf)
				End Try
			End If
		ElseIf result = "success_SteamUGC" Then
			itemUpdated_Text = Me.theStreamReader.ReadLine()
			itemTitle = Me.ReadMultipleLinesOfText(Me.theStreamReader)
			contentFolderOrFileName = Me.theStreamReader.ReadLine()
			appID_Text = Me.theStreamReader.ReadLine()

			Dim debug As Integer = 4242
		Else
			Me.theBackgroundWorker.ReportProgress(0, "WARNING: Unable to get content folder or file. Steam message: " + result + vbCrLf)
		End If

		Return result
	End Function

	Public Function Crowbar_DownloadPreviewFile(ByRef previewImagePathFileName As String) As String
		Me.theStreamWriter.WriteLine("Crowbar_DownloadPreviewFile")

		Dim result As String = Me.theStreamReader.ReadLine()
		If result = "success" Then
			previewImagePathFileName = Me.theStreamReader.ReadLine()
			Dim byteCount As Integer
			byteCount = CInt(Me.theStreamReader.ReadLine())
			If byteCount > 0 Then
				Dim data As Byte() = New Byte(byteCount) {}
				Try
					Me.theStreamReader.BaseStream.Read(data, 0, data.Length)

					If Me.theBackgroundWorker.CancellationPending Then
						Return "cancelled"
					End If

					Dim pictureBytes As New MemoryStream(data)
					Me.theBackgroundWorker.ReportProgress(1, Image.FromStream(pictureBytes))
				Catch ex As Exception
					Me.theBackgroundWorker.ReportProgress(0, "WARNING: Unable to get preview image." + vbCrLf)
				End Try
			End If
		Else
			Me.theBackgroundWorker.ReportProgress(0, "WARNING: Unable to get preview image. Steam message: " + result + vbCrLf)
		End If

		Return result
	End Function

#End Region

#Region "SteamApps"

	Public Function GetAppInstallPath(ByVal appID_text As String) As String
		Me.theStreamWriter.WriteLine("SteamApps_GetAppInstallDir")
		Me.theStreamWriter.WriteLine(appID_text)
		Dim result As String = Me.theStreamReader.ReadLine()
		If result = "success" Then
			Dim appInstallPath As String = Me.theStreamReader.ReadLine()
			Return appInstallPath
		Else
			Return ""
		End If
	End Function

#End Region

#Region "SteamRemoteStorage"

	Public Function SteamRemoteStorage_CommitPublishedFileUpdate() As String
		Me.theStreamWriter.WriteLine("SteamRemoteStorage_CommitPublishedFileUpdate")
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

	Public Function SteamRemoteStorage_CreatePublishedFileUpdateRequest(ByVal itemID_text As String) As String
		Me.theStreamWriter.WriteLine("SteamRemoteStorage_CreatePublishedFileUpdateRequest")
		Me.theStreamWriter.WriteLine(itemID_text)
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

	Public Function SteamRemoteStorage_DeletePublishedFile(ByVal itemID_text As String) As String
		Me.theStreamWriter.WriteLine("SteamRemoteStorage_DeletePublishedFile")
		Me.theStreamWriter.WriteLine(itemID_text)
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

	Public Function SteamRemoteStorage_FileDelete(ByVal targetFileName As String) As String
		Me.theStreamWriter.WriteLine("SteamRemoteStorage_FileDelete")
		Me.theStreamWriter.WriteLine(targetFileName)
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

	Public Function SteamRemoteStorage_FileWrite(ByVal localPathFileName As String, ByVal remotePathFileName As String) As String
		Me.theStreamWriter.WriteLine("SteamRemoteStorage_FileWrite")
		Me.theStreamWriter.WriteLine(localPathFileName)
		Me.theStreamWriter.WriteLine(remotePathFileName)
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

	Public Function SteamRemoteStorage_GetPublishedFileDetails(ByVal itemID_text As String, ByVal appID_text As String, ByRef oAppID_text As String) As WorkshopItem
		Dim publishedItem As New WorkshopItem()

		Me.theStreamWriter.WriteLine("SteamRemoteStorage_GetPublishedFileDetails")
		Me.theStreamWriter.WriteLine(itemID_text)

		Dim result As String = Me.theStreamReader.ReadLine()
		If result = "success" Then
			Dim itemAppID As String = ""
			Dim unixTimeStampText As String
			Dim ownerSteamIDText As String

			publishedItem.ID = Me.theStreamReader.ReadLine()
			Console.WriteLine("Item ID: " + publishedItem.ID)

			publishedItem.CreatorAppID = Me.theStreamReader.ReadLine()

			itemAppID = Me.theStreamReader.ReadLine()
			oAppID_text = itemAppID

			ownerSteamIDText = Me.theStreamReader.ReadLine()
			publishedItem.OwnerID = ULong.Parse(ownerSteamIDText)
			publishedItem.OwnerName = Me.theStreamReader.ReadLine()

			unixTimeStampText = Me.theStreamReader.ReadLine()
			publishedItem.Posted = Long.Parse(unixTimeStampText)
			unixTimeStampText = Me.theStreamReader.ReadLine()
			publishedItem.Updated = Long.Parse(unixTimeStampText)

			publishedItem.Title = Me.ReadMultipleLinesOfText(Me.theStreamReader)
			publishedItem.Description = Me.ReadMultipleLinesOfText(Me.theStreamReader)

			publishedItem.ContentSize = CInt(Me.theStreamReader.ReadLine())
			publishedItem.ContentPathFolderOrFileName = Me.theStreamReader.ReadLine()
			publishedItem.PreviewImageSize = CInt(Me.theStreamReader.ReadLine())
			'NOTE: This is URL and is probably not preview file name. There does not seem to be a way to get preview file name.
			publishedItem.PreviewImagePathFileName = Me.theStreamReader.ReadLine()
			publishedItem.VisibilityText = Me.theStreamReader.ReadLine()

			publishedItem.TagsAsTextLine = Me.theStreamReader.ReadLine()

			If itemAppID <> appID_text Then
				publishedItem.ID = "0"
				publishedItem.Title = "Item is not published under selected game."
			End If
		Else
			publishedItem.ID = "0"
			publishedItem.Title = result
		End If

		Return publishedItem
	End Function

	'NOTE: [10-Mar-2019] Not using GetQuota because it is unclear if it shows quota for Workshop items.
	'      [10-Mar-2019] There are indications on forums that Workshop items no longer affect user's quota.
	Public Sub GetQuota(ByRef availableBytes As ULong, ByRef totalBytes As ULong)
		Dim availableBytesText As String
		Dim totalBytesText As String

		Me.theStreamWriter.WriteLine("SteamRemoteStorage_GetQuota")

		Dim resultIsSuccess As String = Me.theStreamReader.ReadLine()
		If resultIsSuccess = "success" Then
			availableBytesText = Me.theStreamReader.ReadLine()
			availableBytes = ULong.Parse(availableBytesText)
			totalBytesText = Me.theStreamReader.ReadLine()
			totalBytes = ULong.Parse(totalBytesText)
		End If
	End Sub

	Public Function SteamRemoteStorage_PublishWorkshopFile(ByVal contentFileName As String, ByVal previewFileName As String, ByVal appID_text As String, ByVal title As String, ByVal description As String, ByVal visibility_text As String, ByVal tags As BindingListEx(Of String), ByRef returnedPublishedItemID As String) As String
		Me.theStreamWriter.WriteLine("SteamRemoteStorage_PublishWorkshopFile")
		Me.theStreamWriter.WriteLine(contentFileName)
		Me.theStreamWriter.WriteLine(previewFileName)
		Me.theStreamWriter.WriteLine(appID_text)
		Me.WriteTextThatMightHaveMultipleLines(Me.theStreamWriter, title)
		Me.WriteTextThatMightHaveMultipleLines(Me.theStreamWriter, description)
		Me.theStreamWriter.WriteLine(visibility_text)

		Me.theStreamWriter.WriteLine(tags.Count.ToString())
		For Each tag As String In tags
			Me.theStreamWriter.WriteLine(tag)
		Next

		Dim result As String = Me.theStreamReader.ReadLine()
		If result.StartsWith("success") Then
			returnedPublishedItemID = Me.theStreamReader.ReadLine()
		End If
		Return result
	End Function

	Public Function SteamRemoteStorage_UpdatePublishedFileFile(ByVal contentFileName As String) As String
		Me.theStreamWriter.WriteLine("SteamRemoteStorage_UpdatePublishedFileFile")
		Me.theStreamWriter.WriteLine(contentFileName)
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

	Public Function SteamRemoteStorage_UpdatePublishedFileSetChangeDescription(ByVal changeNote As String) As String
		Me.theStreamWriter.WriteLine("SteamRemoteStorage_UpdatePublishedFileSetChangeDescription")
		Me.WriteTextThatMightHaveMultipleLines(Me.theStreamWriter, changeNote)
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

#End Region

#Region "SteamUGC"

#Region "QueryViaSteamUGC"

	Public Function SteamUGC_CreateQueryUGCDetailsRequest(ByVal itemID_Text As String) As String
		Me.theStreamWriter.WriteLine("SteamUGC_CreateQueryUGCDetailsRequest")
		Me.theStreamWriter.WriteLine(itemID_Text)
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

	Public Function SteamUGC_CreateQueryUserUGCRequest(ByVal appID_text As String, ByVal pageNumber As UInteger) As String
		Me.theStreamWriter.WriteLine("SteamUGC_CreateQueryUserUGCRequest")
		Me.theStreamWriter.WriteLine(appID_text)
		Me.theStreamWriter.WriteLine(pageNumber)
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

	Public Function SteamUGC_SendQueryUGCRequest() As String
		Me.theStreamWriter.WriteLine("SteamUGC_SendQueryUGCRequest")

		Dim result As String = Me.theStreamReader.ReadLine()
		If result = "success" Then
			Dim resultsCountText As String
			Dim resultsCount As UInteger
			resultsCountText = Me.theStreamReader.ReadLine()
			resultsCount = UInteger.Parse(resultsCountText)
			If resultsCount = 0 Then
				result = "done"
			Else
				Dim totalCountText As String
				Dim totalCount As UInteger
				totalCountText = Me.theStreamReader.ReadLine()
				totalCount = UInteger.Parse(totalCountText)

				Dim item As WorkshopItem
				Dim unixTimeStampText As String
				Dim ownerSteamIDText As String
				'Dim steamID As Steamworks.CSteamID = Steamworks.SteamUser.GetSteamID()

				Me.theBackgroundWorker.ReportProgress(1, totalCount)
				For i As UInteger = 0 To CUInt(resultsCount - 1)
					item = New WorkshopItem()

					item.ID = Me.theStreamReader.ReadLine()

					'If item.ID <> "0" Then
					ownerSteamIDText = Me.theStreamReader.ReadLine()
					item.OwnerID = ULong.Parse(ownerSteamIDText)
					item.OwnerName = Me.theStreamReader.ReadLine()
					unixTimeStampText = Me.theStreamReader.ReadLine()
					item.Posted = Long.Parse(unixTimeStampText)
					unixTimeStampText = Me.theStreamReader.ReadLine()
					item.Updated = Long.Parse(unixTimeStampText)

					'item.Title = Me.streamReaderForQuerying.ReadLine()
					'======
					item.Title = Me.ReadMultipleLinesOfText(Me.theStreamReader)

					'item.Description = Me.streamReaderForQuerying.ReadLine()

					item.ContentPathFolderOrFileName = Me.theStreamReader.ReadLine()
					item.PreviewImagePathFileName = Me.theStreamReader.ReadLine()
					item.VisibilityText = Me.theStreamReader.ReadLine()
					item.TagsAsTextLine = Me.theStreamReader.ReadLine()

					'publishedItems.Add(item)
					Me.theBackgroundWorker.ReportProgress(2, item)
					'End If

					If Me.theBackgroundWorker.CancellationPending Then
						Exit For
					End If
				Next
			End If
		End If

		Return result
	End Function

	'Public Function SteamUGC_SendQueryUGCRequest_ContentPathOrFileName() As String
	'	Me.theStreamWriter.WriteLine("SteamUGC_SendQueryUGCRequest_ContentPathOrFileName")
	'	Dim contentPathOrFileName As String = ""
	'	Dim result As String = Me.theStreamReader.ReadLine()
	'	If result = "success" Then
	'		contentPathOrFileName = Me.theStreamReader.ReadLine()
	'	End If
	'	Return contentPathOrFileName
	'End Function

#End Region

#Region "PublishViaSteamUGC"

	Public Function SteamUGC_CreateItem(ByVal appID_text As String, ByRef returnedPublishedItemID As String) As String
		Me.theStreamWriter.WriteLine("SteamUGC_CreateItem")
		Me.theStreamWriter.WriteLine(appID_text)

		Dim result As String = Me.theStreamReader.ReadLine()
		If result.StartsWith("success") Then
			returnedPublishedItemID = Me.theStreamReader.ReadLine()
		End If
		Return result
	End Function

	Public Function SteamUGC_StartItemUpdate(ByVal appID_text As String, ByVal itemID_text As String) As String
		Me.theStreamWriter.WriteLine("SteamUGC_StartItemUpdate")
		Me.theStreamWriter.WriteLine(appID_text)
		Me.theStreamWriter.WriteLine(itemID_text)
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

#Region "SteamUGC_SetItem Details"

	Public Function SteamUGC_SetItemTitle(ByVal title As String) As String
		Me.theStreamWriter.WriteLine("SteamUGC_SetItemTitle")
		Me.WriteTextThatMightHaveMultipleLines(Me.theStreamWriter, title)
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

	Public Function SteamUGC_SetItemDescription(ByVal description As String) As String
		Me.theStreamWriter.WriteLine("SteamUGC_SetItemDescription")
		Me.WriteTextThatMightHaveMultipleLines(Me.theStreamWriter, description)
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

	Public Function SteamUGC_SetItemContent(ByVal contentPathFolderOrFileName As String) As String
		Me.theStreamWriter.WriteLine("SteamUGC_SetItemContent")
		Me.theStreamWriter.WriteLine(contentPathFolderOrFileName)
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

	Public Function SteamUGC_SetItemPreview(ByVal previewPathFileName As String) As String
		Me.theStreamWriter.WriteLine("SteamUGC_SetItemPreview")
		Me.theStreamWriter.WriteLine(previewPathFileName)
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

	Public Function SteamUGC_SetItemVisibility(ByVal visibility_text As String) As String
		Me.theStreamWriter.WriteLine("SteamUGC_SetItemVisibility")
		Me.theStreamWriter.WriteLine(visibility_text)
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

	Public Function SteamUGC_SetItemTags(ByVal tags As BindingListEx(Of String)) As String
		Me.theStreamWriter.WriteLine("SteamUGC_SetItemTags")
		Me.theStreamWriter.WriteLine(tags.Count.ToString())
		For Each tag As String In tags
			Me.theStreamWriter.WriteLine(tag)
		Next
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

#End Region

	Public Function SteamUGC_SubmitItemUpdate(ByVal changeNote As String) As String
		Me.theStreamWriter.WriteLine("SteamUGC_SubmitItemUpdate")
		Me.WriteTextThatMightHaveMultipleLines(Me.theStreamWriter, changeNote)

		Dim result As String = ""
		Dim outputInfo As New BackgroundSteamPipe.PublishItemProgressInfo()
		Dim previousOutputInfo As New BackgroundSteamPipe.PublishItemProgressInfo()

		While True
			result = Me.theStreamReader.ReadLine()
			'NOTE: The Sleep() is needed to prevent Crowbar Publish locking up when publishing to a workshop via SteamUGC.
			'      Unfortunately, I do not understand how this prevents lock up considering that each ReadLine() waits for available input.
			Threading.Thread.Sleep(1)
			If result = "OnSubmitItemUpdate" Then
				result = Me.theStreamReader.ReadLine()
				Exit While
			Else
				'Threading.Thread.Sleep(1)
				outputInfo.Status = result
				outputInfo.UploadedByteCount = CULng(Me.theStreamReader.ReadLine())
				outputInfo.TotalUploadedByteCount = CULng(Me.theStreamReader.ReadLine())
				'Threading.Thread.Sleep(1)
				If outputInfo.Status = "invalid" Then
					Dim debug As Integer = 4242
				Else
					'Threading.Thread.Sleep(1)
					If previousOutputInfo.Status <> outputInfo.Status OrElse previousOutputInfo.UploadedByteCount <> outputInfo.UploadedByteCount OrElse previousOutputInfo.TotalUploadedByteCount <> outputInfo.TotalUploadedByteCount Then
						'Threading.Thread.Sleep(1)
						'If outputInfo.TotalUploadedByteCount > 0 Then
						Me.theBackgroundWorker.ReportProgress(2, outputInfo)

						'Threading.Thread.Sleep(1)
						previousOutputInfo.Status = outputInfo.Status
						previousOutputInfo.UploadedByteCount = outputInfo.UploadedByteCount
						previousOutputInfo.TotalUploadedByteCount = outputInfo.TotalUploadedByteCount
						'End If
					End If
				End If
			End If
		End While

		Return result
	End Function

#End Region

#Region "DeleteItem"

	Public Function SteamUGC_DeleteItem(ByVal itemID_text As String) As String
		Me.theStreamWriter.WriteLine("SteamUGC_DeleteItem")
		Me.theStreamWriter.WriteLine(itemID_text)
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

#End Region

#Region "UnsubscribeItem"

	Public Function SteamUGC_UnsubscribeItem(ByVal itemID_text As String) As String
		Me.theStreamWriter.WriteLine("SteamUGC_UnsubscribeItem")
		Me.theStreamWriter.WriteLine(itemID_text)
		Dim result As String = Me.theStreamReader.ReadLine()
		Return result
	End Function

#End Region

#End Region

#Region "SteamUser"

	Public Function GetUserSteamID() As ULong
		Me.theStreamWriter.WriteLine("SteamUser_GetSteamID")
		Dim idText As String = Me.theStreamReader.ReadLine()
		Return CULng(idText)
	End Function

#End Region

#Region "Private Functions"

	'NOTE: WriteLine only writes string until first LF or CR, so need to adjust how to send this.
	'NOTE: From TextReader.ReadLine: A line is defined as a sequence of characters followed by 
	'      a carriage return (0x000d), a line feed (0x000a), a carriage return followed by a line feed, Environment.NewLine, or the end-of-stream marker.
	'      https://docs.microsoft.com/en-us/dotnet/api/system.io.textreader.readline?view=netframework-4.0
	Public Sub WriteTextThatMightHaveMultipleLines(ByVal sw As StreamWriter, ByVal text As String)
		'NOTE: Replace all CR in text because they are not needed and will show as blank characters in Windows TextBox.
		text = text.Replace(vbCr, "")
		Dim stringSeparators() As String = {vbLf}
		Dim textList As String() = text.Split(stringSeparators, StringSplitOptions.None)
		sw.WriteLine(textList.Length)
		Console.WriteLine("    Line count: " + textList.Length.ToString())
		Dim i As Integer = 1
		For Each line As String In textList
			sw.WriteLine(line)
			Console.WriteLine("    Line " + i.ToString() + ": " + line)
			i += 1
		Next
	End Sub

	Public Function ReadMultipleLinesOfText(ByVal sr As StreamReader) As String
		Dim text As String = ""

		Dim textLineCount As Integer
		textLineCount = CInt(sr.ReadLine())
		If textLineCount > 0 Then
			'NOTE: Do not add CRLF to last line.
			For i As Integer = 0 To textLineCount - 2
				'NOTE: Add CRLF because TextBoxes use it for newlines.
				text += sr.ReadLine() + vbCrLf
			Next
			text += sr.ReadLine()
		End If

		Return text
	End Function

#End Region

#End Region

#Region "Core Event Handlers"

#End Region

#Region "Private Methods"

#End Region

#Region "Events"

#End Region

#Region "Constants"

	Public Const AgreementMessageForCreate As String = "WARNING: Created workshop item, but is unavailable to anyone else until you accept" + vbCrLf + "    the Steam Subscriber Agreement. (NOTE: This can occur when the agreement" + vbCrLf + "    has been updated.) Click ""Steam Subscriber Agreement"" button above to open the web page" + vbCrLf + "    where you can accept the agreement."
	Public Const AgreementMessageForUpdate As String = "WARNING: Updated workshop item, but is unavailable to anyone else until you accept" + vbCrLf + "    the Steam Subscriber Agreement. (NOTE: This can occur when the agreement" + vbCrLf + "    has been updated.) Click ""Steam Subscriber Agreement"" button above to open the web page" + vbCrLf + "    where you can accept the agreement."

#End Region

#Region "Data"

	Dim theBackgroundWorker As BackgroundWorker
	Dim theCrowbarSteamPipeProcess As Process
	Dim theCrowbarSteamPipeServer As NamedPipeServerStream = Nothing
	Dim theStreamWriter As StreamWriter = Nothing
	Dim theStreamReader As StreamReader = Nothing
	Dim theReasonText As String

#End Region

End Class
