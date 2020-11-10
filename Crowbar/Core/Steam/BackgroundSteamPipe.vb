Imports System.ComponentModel
Imports System.IO

Public Class BackgroundSteamPipe

	Public Sub New()
		MyBase.New()

		Me.theActiveSteamPipes = New List(Of SteamPipe)()
		Me.theActiveBackgroundWorkers = New List(Of BackgroundWorkerEx)()
	End Sub

	Public Sub Kill()
		'NOTE: Prevent handlers from doing stuff because some of them might access widgets that are disposed.
		For i As Integer = Me.theActiveBackgroundWorkers.Count - 1 To 0 Step -1
			Dim aBackgroundWorker As BackgroundWorkerEx = Me.theActiveBackgroundWorkers(i)
			aBackgroundWorker.Kill()
			Me.theActiveBackgroundWorkers.Remove(aBackgroundWorker)
		Next
		For i As Integer = Me.theActiveSteamPipes.Count - 1 To 0 Step -1
			Dim aSteamPipe As SteamPipe = Me.theActiveSteamPipes(i)
			aSteamPipe.Kill()
			Me.theActiveSteamPipes.Remove(aSteamPipe)
		Next
	End Sub

#Region "Download Item"

	Public Class DownloadItemInputInfo
		Public AppID As UInteger
		Public PublishedItemID As String
		Public TargetPath As String
	End Class

	Public Class DownloadItemOutputInfo
		Public Result As String
		Public SteamAgreementStatus As String
		Public AppID As UInteger
		Public PublishedItemID As String
		Public ItemUpdated_Text As String
		Public ItemTitle As String
		Public ContentFile As Byte()
		Public ContentFolderOrFileName As String
		Public BytesReceived As Long
		Public TotalBytesToReceive As Long
	End Class

	Public Sub DownloadItem(ByVal given_ProgressChanged As ProgressChangedEventHandler, ByVal given_RunWorkerCompleted As RunWorkerCompletedEventHandler, ByVal inputInfo As DownloadItemInputInfo)
		Me.theDownloadItemBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(Me.theDownloadItemBackgroundWorker, AddressOf Me.DownloadItem_DoWork, given_ProgressChanged, given_RunWorkerCompleted, inputInfo)
	End Sub

	'NOTE: This is run in a background thread.
	Private Sub DownloadItem_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Dim bw As BackgroundWorkerEx = CType(sender, BackgroundWorkerEx)
		Me.theActiveBackgroundWorkers.Add(bw)
		Dim inputInfo As DownloadItemInputInfo = CType(e.Argument, DownloadItemInputInfo)
		Dim outputInfo As New BackgroundSteamPipe.DownloadItemOutputInfo()

		If inputInfo.AppID = 0 Then
			'NOTE: Use appID for "Source SDK" when item's appID is unknown.
			TheApp.WriteSteamAppIdFile(211)

			Dim steamPipeToGetAppID As New SteamPipe()
			Me.theActiveSteamPipes.Add(steamPipeToGetAppID)
			Dim resultOfGetAppID As String = steamPipeToGetAppID.Open("GetItemAppID", bw, "Getting item app ID")
			If resultOfGetAppID <> "success" Then
				outputInfo.Result = "error"
				e.Result = outputInfo
				Me.theActiveSteamPipes.Remove(steamPipeToGetAppID)
				Me.theActiveBackgroundWorkers.Remove(bw)
				Exit Sub
			End If

			If bw.CancellationPending Then
				steamPipeToGetAppID.Kill()
				e.Cancel = True
				Me.theActiveSteamPipes.Remove(steamPipeToGetAppID)
				Me.theActiveBackgroundWorkers.Remove(bw)
				Exit Sub
			End If

			Dim appID_text As String = ""
			Dim publishedItem As WorkshopItem = Nothing
			publishedItem = steamPipeToGetAppID.SteamRemoteStorage_GetPublishedFileDetails(inputInfo.PublishedItemID, inputInfo.AppID.ToString(), appID_text)

			If bw.CancellationPending Then
				steamPipeToGetAppID.Kill()
				e.Cancel = True
				Me.theActiveSteamPipes.Remove(steamPipeToGetAppID)
				Me.theActiveBackgroundWorkers.Remove(bw)
				Exit Sub
			End If

			steamPipeToGetAppID.Shut()
			Me.theActiveSteamPipes.Remove(steamPipeToGetAppID)

			If appID_text <> "" Then
				TheApp.WriteSteamAppIdFile(CUInt(appID_text))
			Else
				'NOTE: Error message is stored in publishedItem.Title.
				bw.ReportProgress(0, "ERROR: " + publishedItem.Title + vbCrLf)
				outputInfo.Result = "error"
				e.Result = outputInfo
				Me.theActiveBackgroundWorkers.Remove(bw)
				Exit Sub
			End If
		Else
			TheApp.WriteSteamAppIdFile(inputInfo.AppID)
		End If
		outputInfo.AppID = inputInfo.AppID

		Dim steamPipe As New SteamPipe()
		Me.theActiveSteamPipes.Add(steamPipe)
		Dim result As String = steamPipe.Open("DownloadItem", bw, "Downloading item")
		If result <> "success" Then
			outputInfo.Result = "error"
			e.Result = outputInfo
			Me.theActiveSteamPipes.Remove(steamPipe)
			Me.theActiveBackgroundWorkers.Remove(bw)
			Exit Sub
		End If

		outputInfo.PublishedItemID = inputInfo.PublishedItemID
		outputInfo.ContentFolderOrFileName = ""
		Dim contentFile As Byte() = {0}
		Dim returned_AppID_Text As String = "0"
		result = steamPipe.Crowbar_DownloadContentFolderOrFile(inputInfo.PublishedItemID, inputInfo.TargetPath, contentFile, outputInfo.ItemUpdated_Text, outputInfo.ItemTitle, outputInfo.ContentFolderOrFileName, returned_AppID_Text)
		If result = "success" Then
			outputInfo.ContentFile = contentFile
		ElseIf result = "success_SteamUGC" Then
			outputInfo.AppID = UInteger.Parse(returned_AppID_Text)
		Else
			bw.ReportProgress(0, "ERROR: Unable to download the content file name from Steam." + vbCrLf)
		End If
		outputInfo.Result = result

		steamPipe.Shut()
		Me.theActiveSteamPipes.Remove(steamPipe)
		Me.theActiveBackgroundWorkers.Remove(bw)

		e.Result = outputInfo
	End Sub

	Public Sub UnsubscribeItem(ByVal given_ProgressChanged As ProgressChangedEventHandler, ByVal given_RunWorkerCompleted As RunWorkerCompletedEventHandler, ByVal inputInfo As DownloadItemInputInfo)
		Me.theUnsubscribeItemBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(Me.theUnsubscribeItemBackgroundWorker, AddressOf Me.UnsubscribeItem_DoWork, given_ProgressChanged, given_RunWorkerCompleted, inputInfo)
	End Sub

	'NOTE: This is run in a background thread.
	Private Sub UnsubscribeItem_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Dim bw As BackgroundWorkerEx = CType(sender, BackgroundWorkerEx)
		Me.theActiveBackgroundWorkers.Add(bw)
		Dim inputInfo As DownloadItemInputInfo = CType(e.Argument, DownloadItemInputInfo)
		Dim outputInfo As New BackgroundSteamPipe.DownloadItemOutputInfo()

		TheApp.WriteSteamAppIdFile(inputInfo.AppID)

		Dim steamPipe As New SteamPipe()
		Me.theActiveSteamPipes.Add(steamPipe)
		Dim result As String = steamPipe.Open("UnsubscribeItem", bw, "Unsubscribing from item")
		If result <> "success" Then
			outputInfo.Result = "error"
			e.Result = outputInfo
			Me.theActiveSteamPipes.Remove(steamPipe)
			Me.theActiveBackgroundWorkers.Remove(bw)
			Exit Sub
		End If

		result = steamPipe.SteamUGC_UnsubscribeItem(inputInfo.PublishedItemID)
		If result <> "success" Then
			bw.ReportProgress(0, "ERROR: Unable to download the content file name from Steam." + vbCrLf)
		End If
		outputInfo.Result = result

		steamPipe.Shut()
		Me.theActiveSteamPipes.Remove(steamPipe)
		Me.theActiveBackgroundWorkers.Remove(bw)

		e.Result = outputInfo
	End Sub

	Private theDownloadItemBackgroundWorker As BackgroundWorkerEx
	Private theUnsubscribeItemBackgroundWorker As BackgroundWorkerEx

#End Region

#Region "GetPublishedItems"

	Public Sub GetPublishedItems(ByVal given_ProgressChanged As ProgressChangedEventHandler, ByVal given_RunWorkerCompleted As RunWorkerCompletedEventHandler, ByVal appID_text As String)
		Me.theGetPublishedItemsBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(Me.theGetPublishedItemsBackgroundWorker, AddressOf Me.GetPublishedItems_DoWork, given_ProgressChanged, given_RunWorkerCompleted, appID_text)
	End Sub

	'NOTE: This is run in a background thread.
	Private Sub GetPublishedItems_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Dim bw As BackgroundWorkerEx = CType(sender, BackgroundWorkerEx)
		Me.theActiveBackgroundWorkers.Add(bw)
		Dim appID_text As String = CType(e.Argument, String)

		Dim steamPipe As New SteamPipe()
		Me.theActiveSteamPipes.Add(steamPipe)
		Dim result As String = steamPipe.Open("GetPublishedItems", bw, "Getting list of published items")
		If result <> "success" Then
			e.Cancel = True
			Me.theActiveSteamPipes.Remove(steamPipe)
			Me.theActiveBackgroundWorkers.Remove(bw)
			Exit Sub
		End If

		Dim resultOfSendRequest As String
		Dim pageNumber As UInteger = 1UI
		While pageNumber > 0
			Dim result_SteamUGC_CreateQueryUserUGCRequest As String
			result_SteamUGC_CreateQueryUserUGCRequest = steamPipe.SteamUGC_CreateQueryUserUGCRequest(appID_text, pageNumber)
			If result_SteamUGC_CreateQueryUserUGCRequest <> "success" Then
				bw.ReportProgress(0, "ERROR: " + result_SteamUGC_CreateQueryUserUGCRequest + vbCrLf)
				Exit While
			End If

			If bw.CancellationPending Then
				steamPipe.Kill()
				e.Cancel = True
				Me.theActiveSteamPipes.Remove(steamPipe)
				Me.theActiveBackgroundWorkers.Remove(bw)
				Exit Sub
			End If

			resultOfSendRequest = steamPipe.SteamUGC_SendQueryUGCRequest()
			If resultOfSendRequest = "success" Then
				pageNumber += 1UI
			Else
				pageNumber = 0
			End If

			If bw.CancellationPending Then
				steamPipe.Kill()
				e.Cancel = True
				Me.theActiveSteamPipes.Remove(steamPipe)
				Me.theActiveBackgroundWorkers.Remove(bw)
				Exit Sub
			End If
		End While

		steamPipe.Shut()
		Me.theActiveSteamPipes.Remove(steamPipe)
		Me.theActiveBackgroundWorkers.Remove(bw)
	End Sub

	Private theGetPublishedItemsBackgroundWorker As BackgroundWorkerEx

#End Region

#Region "GetPublishedItemDetails"

	Public Class GetPublishedFileDetailsInputInfo
		Public ItemID_text As String
		Public AppID_text As String
		Public Action As String

		Public Sub New(ByVal iItemID_text As String, ByVal iAppID_text As String, ByVal action As String)
			Me.ItemID_text = iItemID_text
			Me.AppID_text = iAppID_text
			Me.Action = action
		End Sub
	End Class

	Public Class GetPublishedFileDetailsOutputInfo
		Public PublishedItem As WorkshopItem
		Public Action As String

		Public Sub New(ByVal publishedItem As WorkshopItem, ByVal action As String)
			Me.PublishedItem = publishedItem
			Me.Action = action
		End Sub
	End Class

	Public Sub GetPublishedItemDetails(ByVal given_ProgressChanged As ProgressChangedEventHandler, ByVal given_RunWorkerCompleted As RunWorkerCompletedEventHandler, ByVal input As GetPublishedFileDetailsInputInfo)
		Me.theGetPublishedItemDetailsBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(Me.theGetPublishedItemDetailsBackgroundWorker, AddressOf Me.GetPublishedItemDetails_DoWork, given_ProgressChanged, given_RunWorkerCompleted, input)
	End Sub

	'NOTE: This is run in a background thread.
	Private Sub GetPublishedItemDetails_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Dim bw As BackgroundWorkerEx = CType(sender, BackgroundWorkerEx)
		Me.theActiveBackgroundWorkers.Add(bw)
		Dim input As GetPublishedFileDetailsInputInfo = CType(e.Argument, GetPublishedFileDetailsInputInfo)

		If Me.theGetItemDetailsSteamPipe IsNot Nothing Then
			Me.theGetItemDetailsSteamPipe.Kill()
		End If
		Me.theGetItemDetailsSteamPipe = New SteamPipe()
		Me.theActiveSteamPipes.Add(Me.theGetItemDetailsSteamPipe)

		Dim result As String = Me.theGetItemDetailsSteamPipe.Open("GetItemDetails", bw, "Getting item details")
		If result <> "success" Then
			e.Cancel = True
			Me.theActiveSteamPipes.Remove(Me.theGetItemDetailsSteamPipe)
			Me.theActiveBackgroundWorkers.Remove(bw)
			Exit Sub
		End If

		If bw.CancellationPending Then
			Me.theGetItemDetailsSteamPipe.Kill()
			e.Cancel = True
			Me.theActiveSteamPipes.Remove(Me.theGetItemDetailsSteamPipe)
			Me.theActiveBackgroundWorkers.Remove(bw)
			Exit Sub
		End If

		Dim appID_text As String = ""
		Dim publishedItem As WorkshopItem = Nothing
		publishedItem = Me.theGetItemDetailsSteamPipe.SteamRemoteStorage_GetPublishedFileDetails(input.ItemID_text, input.AppID_text, appID_text)

		If bw.CancellationPending Then
			Me.theGetItemDetailsSteamPipe.Kill()
			e.Cancel = True
			Me.theActiveSteamPipes.Remove(Me.theGetItemDetailsSteamPipe)
			Me.theActiveBackgroundWorkers.Remove(bw)
			Exit Sub
		End If

		If publishedItem.ID <> "0" Then
			result = Me.theGetItemDetailsSteamPipe.Crowbar_DownloadPreviewFile(publishedItem.PreviewImagePathFileName)
		Else
			'NOTE: Error message is stored in publishedItem.Title.
			bw.ReportProgress(0, "ERROR: " + publishedItem.Title + vbCrLf)
			bw.ReportProgress(1, Nothing)
		End If

		Me.theGetItemDetailsSteamPipe.Shut()
		Me.theActiveSteamPipes.Remove(Me.theGetItemDetailsSteamPipe)
		Me.theGetItemDetailsSteamPipe = Nothing

		If bw.CancellationPending Then
			e.Cancel = True
			Me.theActiveBackgroundWorkers.Remove(bw)
			Exit Sub
		End If

		Me.theActiveBackgroundWorkers.Remove(bw)
		Dim output As New GetPublishedFileDetailsOutputInfo(publishedItem, input.Action)
		e.Result = output
	End Sub

	Private theGetPublishedItemDetailsBackgroundWorker As BackgroundWorkerEx
	Private theGetItemDetailsSteamPipe As SteamPipe

#End Region

#Region "DeletePublishedItemFromWorkshop"

	Public Sub DeletePublishedItemFromWorkshop(ByVal given_ProgressChanged As ProgressChangedEventHandler, ByVal given_RunWorkerCompleted As RunWorkerCompletedEventHandler, ByVal itemID_text As String)
		Me.theDeletePublishedItemFromWorkshopBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(Me.theDeletePublishedItemFromWorkshopBackgroundWorker, AddressOf Me.DeletePublishedItemFromWorkshop_DoWork, given_ProgressChanged, given_RunWorkerCompleted, itemID_text)
	End Sub

	'NOTE: This is run in a background thread.
	Private Sub DeletePublishedItemFromWorkshop_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Dim bw As BackgroundWorkerEx = CType(sender, BackgroundWorkerEx)
		Me.theActiveBackgroundWorkers.Add(bw)
		Dim itemID_text As String = CType(e.Argument, String)

		Dim steamPipe As New SteamPipe()
		Me.theActiveSteamPipes.Add(steamPipe)
		Dim result As String = steamPipe.Open("DeleteItem", bw, "Deleting item")
		If result <> "success" Then
			e.Cancel = True
			Me.theActiveSteamPipes.Remove(steamPipe)
			Me.theActiveBackgroundWorkers.Remove(bw)
			Exit Sub
		End If

		result = "failed"
		If TheApp.SteamAppInfos(TheApp.Settings.PublishGameSelectedIndex).UsesSteamUGC Then
			result = steamPipe.SteamUGC_DeleteItem(itemID_text)
		Else
			result = steamPipe.SteamRemoteStorage_DeletePublishedFile(itemID_text)
		End If

		steamPipe.Shut()
		Me.theActiveSteamPipes.Remove(steamPipe)
		Me.theActiveBackgroundWorkers.Remove(bw)

		e.Result = result
	End Sub

	Private theDeletePublishedItemFromWorkshopBackgroundWorker As BackgroundWorkerEx

#End Region

#Region "PublishItem"

	Public Class PublishItemInputInfo
		Public AppInfo As SteamAppInfoBase
		Public Item As WorkshopItem
	End Class

	Public Class PublishItemProgressInfo
		Public Status As String
		Public UploadedByteCount As ULong
		Public TotalUploadedByteCount As ULong
	End Class

	Public Class PublishItemOutputInfo
		Public Result As String
		Public SteamAgreementStatus As String
		Public PublishedItemID As String
		Public PublishedItemOwnerID As ULong
		Public PublishedItemOwnerName As String
		Public PublishedItemPosted As Long
		Public PublishedItemUpdated As Long
	End Class

	Public Sub PublishItem(ByVal given_ProgressChanged As ProgressChangedEventHandler, ByVal given_RunWorkerCompleted As RunWorkerCompletedEventHandler, ByVal inputInfo As PublishItemInputInfo)
		Me.thePublishItemBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(Me.thePublishItemBackgroundWorker, AddressOf Me.PublishItem_DoWork, given_ProgressChanged, given_RunWorkerCompleted, inputInfo)
	End Sub

	'NOTE: This is run in a background thread.
	Private Sub PublishItem_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Dim bw As BackgroundWorkerEx = CType(sender, BackgroundWorkerEx)
		Me.theActiveBackgroundWorkers.Add(bw)
		Dim inputInfo As PublishItemInputInfo = CType(e.Argument, PublishItemInputInfo)

		'TODO: Process content folder or file before accessing Steam.
		Dim processedContentPathFolderOrFileName As String = ""
		If inputInfo.Item.ContentPathFolderOrFileNameIsChanged AndAlso inputInfo.Item.ContentPathFolderOrFileName <> "" Then
			Dim processedFileCheckIsSuccessful As Boolean = True
			Try
				Me.thePublishItemBackgroundWorker.ReportProgress(0, "Processing content for upload." + vbCrLf)
				processedContentPathFolderOrFileName = inputInfo.AppInfo.ProcessFileBeforeUpload(inputInfo.Item, bw)

				If inputInfo.AppInfo.CanUseContentFolderOrFile Then
					If Not Directory.Exists(processedContentPathFolderOrFileName) AndAlso Not File.Exists(processedContentPathFolderOrFileName) Then
						processedFileCheckIsSuccessful = False
					End If
				ElseIf inputInfo.AppInfo.UsesSteamUGC Then
					If Not Directory.Exists(processedContentPathFolderOrFileName) Then
						processedFileCheckIsSuccessful = False
					End If
				Else
					If Not File.Exists(processedContentPathFolderOrFileName) Then
						processedFileCheckIsSuccessful = False
					End If
				End If
			Catch ex As Exception
				Me.thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: " + ex.Message + vbCrLf)
				processedFileCheckIsSuccessful = False
			End Try

			If Not processedFileCheckIsSuccessful Then
				Me.thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: Processing content failed. Review log messages above for reason." + vbCrLf)
				e.Cancel = True
				Me.theActiveBackgroundWorkers.Remove(bw)
				Exit Sub
			End If
		End If

		If inputInfo.Item.CreatorAppID = "0" Then
			TheApp.WriteSteamAppIdFile(inputInfo.AppInfo.ID.m_AppId)
		Else
			TheApp.WriteSteamAppIdFile(inputInfo.Item.CreatorAppID)
		End If

		Dim steamPipe As New SteamPipe()
		Me.theActiveSteamPipes.Add(steamPipe)
		Dim result As String = steamPipe.Open("PublishItem", bw, "Publishing item")
		If result <> "success" Then
			e.Cancel = True
			Me.theActiveSteamPipes.Remove(steamPipe)
			Me.theActiveBackgroundWorkers.Remove(bw)
			Exit Sub
		End If

		Dim outputInfo As BackgroundSteamPipe.PublishItemOutputInfo
		If inputInfo.AppInfo.UsesSteamUGC Then
			outputInfo = Me.PublishViaSteamUGC(steamPipe, inputInfo, processedContentPathFolderOrFileName)
		Else
			outputInfo = Me.PublishViaRemoteStorage(steamPipe, inputInfo, processedContentPathFolderOrFileName)
		End If

		If outputInfo.PublishedItemID <> "0" Then
			Dim appID_text As String = ""
			Dim publishedItem As WorkshopItem = Nothing
			publishedItem = steamPipe.SteamRemoteStorage_GetPublishedFileDetails(outputInfo.PublishedItemID, inputInfo.AppInfo.ID.ToString(), appID_text)
			outputInfo.PublishedItemOwnerID = publishedItem.OwnerID
			outputInfo.PublishedItemOwnerName = publishedItem.OwnerName
			outputInfo.PublishedItemPosted = publishedItem.Posted
			outputInfo.PublishedItemUpdated = publishedItem.Updated
		End If

		steamPipe.Shut()
		Me.theActiveSteamPipes.Remove(steamPipe)
		Me.theActiveBackgroundWorkers.Remove(bw)

		e.Result = outputInfo
	End Sub

	'NOTE: This is run in a background thread.
	Private Function PublishViaSteamUGC(ByVal steamPipe As SteamPipe, ByVal inputInfo As PublishItemInputInfo, ByVal processedContentPathFolderOrFileName As String) As BackgroundSteamPipe.PublishItemOutputInfo
		Dim changeNote As String
		Dim appID_text As String = inputInfo.AppInfo.ID.ToString()
		Dim itemID_text As String = "0"

		Dim outputInfo As New BackgroundSteamPipe.PublishItemOutputInfo()
		outputInfo.PublishedItemID = "0"
		outputInfo.SteamAgreementStatus = "Accepted"

		If inputInfo.Item.IsDraft Then
			Dim returnedPublishedItemID As String = ""
			Dim resultOfCreateItem As String = steamPipe.SteamUGC_CreateItem(appID_text, returnedPublishedItemID)

			If resultOfCreateItem = "success" Then
				itemID_text = returnedPublishedItemID
				outputInfo.PublishedItemID = returnedPublishedItemID
			ElseIf resultOfCreateItem = "success_agreement" Then
				Me.thePublishItemBackgroundWorker.ReportProgress(0, SteamPipe.AgreementMessageForCreate + vbCrLf)
				itemID_text = returnedPublishedItemID
				outputInfo.PublishedItemID = returnedPublishedItemID
				outputInfo.SteamAgreementStatus = "NotAccepted"
			Else
				Me.thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: Unable to create workshop item. Steam error message: " + resultOfCreateItem + vbCrLf)
				outputInfo.Result = "Failed"
				'Return outputInfo
			End If

			changeNote = ""
		Else
			itemID_text = inputInfo.Item.ID
			changeNote = inputInfo.Item.ChangeNote
		End If

		If outputInfo.Result <> "Failed" Then
			Dim result As String = ""

			result = Me.StartItemUpdate(steamPipe, appID_text, itemID_text)
			If result <> "success" Then
				outputInfo.Result = "Failed"
				'Return outputInfo
			Else
				result = Me.UpdateNonContentFileOptions(steamPipe, inputInfo)
				If result <> "success" Then
					outputInfo.Result = "Failed"
					'Return outputInfo
				Else
					If inputInfo.Item.ContentPathFolderOrFileNameIsChanged AndAlso inputInfo.Item.ContentPathFolderOrFileName <> "" Then
						If outputInfo.Result <> "Failed" Then
							If Directory.Exists(processedContentPathFolderOrFileName) OrElse (inputInfo.AppInfo.CanUseContentFolderOrFile AndAlso File.Exists(processedContentPathFolderOrFileName)) Then
								Dim setItemContentWasSuccessful As String = steamPipe.SteamUGC_SetItemContent(processedContentPathFolderOrFileName)
								If setItemContentWasSuccessful = "success" Then
									Me.thePublishItemBackgroundWorker.ReportProgress(0, "Set item content completed." + vbCrLf)
								Else
									Me.thePublishItemBackgroundWorker.ReportProgress(0, "Set item content failed." + vbCrLf)
									outputInfo.Result = "Failed"
									'Return outputInfo
								End If
							End If
						End If
					End If

					If outputInfo.Result <> "Failed" Then
						If inputInfo.Item.IsDraft Then
							Me.thePublishItemBackgroundWorker.ReportProgress(0, "Publishing new item." + vbCrLf)
						Else
							Me.thePublishItemBackgroundWorker.ReportProgress(0, "Publishing the update." + vbCrLf)
						End If

						result = Me.SubmitItemUpdate(steamPipe, changeNote)
						If result = "success" Then
							Me.thePublishItemBackgroundWorker.ReportProgress(0, "Publishing succeeded." + vbCrLf)
							outputInfo.Result = "Succeeded"
							If outputInfo.PublishedItemID = "0" Then
								outputInfo.PublishedItemID = inputInfo.Item.ID
							End If
						ElseIf result = "success_agreement" Then
							If outputInfo.PublishedItemID = "0" Then
								Me.thePublishItemBackgroundWorker.ReportProgress(0, SteamPipe.AgreementMessageForUpdate + vbCrLf)
								outputInfo.PublishedItemID = inputInfo.Item.ID
							ElseIf outputInfo.SteamAgreementStatus = "Accepted" Then
								Me.thePublishItemBackgroundWorker.ReportProgress(0, SteamPipe.AgreementMessageForCreate + vbCrLf)
							End If
							outputInfo.SteamAgreementStatus = "NotAccepted"
							outputInfo.Result = "Succeeded"
							'Return outputInfo
						Else
							outputInfo.Result = "Failed"
						End If
					End If

					If inputInfo.Item.ContentPathFolderOrFileNameIsChanged AndAlso inputInfo.Item.ContentPathFolderOrFileName <> "" Then
						inputInfo.AppInfo.CleanUpAfterUpload(Me.thePublishItemBackgroundWorker)
					End If
				End If
			End If
		End If

		Return outputInfo
	End Function

	'NOTE: This is run in a background thread.
	Private Function PublishViaRemoteStorage(ByVal steamPipe As SteamPipe, ByVal inputInfo As PublishItemInputInfo, ByVal processedContentPathFolderOrFileName As String) As BackgroundSteamPipe.PublishItemOutputInfo
		Dim outputInfo As BackgroundSteamPipe.PublishItemOutputInfo

		If inputInfo.Item.IsDraft Then
			outputInfo = Me.CreateViaRemoteStorage(steamPipe, inputInfo, processedContentPathFolderOrFileName)
		Else
			outputInfo = Me.UpdateViaRemoteStorage(steamPipe, inputInfo, processedContentPathFolderOrFileName)
		End If

		Return outputInfo
	End Function

	'NOTE: SteamRemoteStorage_PublishWorkshopFile requires Item to have a Title, a Description, a Content File, and a Preview Image.
	'NOTE: This is run in a background thread.
	Private Function CreateViaRemoteStorage(ByVal steamPipe As SteamPipe, ByVal inputInfo As PublishItemInputInfo, ByVal processedContentPathFolderOrFileName As String) As BackgroundSteamPipe.PublishItemOutputInfo
		Dim outputInfo As New BackgroundSteamPipe.PublishItemOutputInfo()
		outputInfo.PublishedItemID = "0"
		outputInfo.SteamAgreementStatus = "Accepted"

		Dim previewFileName As String = Path.GetFileName(inputInfo.Item.PreviewImagePathFileName)
		Dim resultForPreview_SteamRemoteStorage_FileWrite As String = steamPipe.SteamRemoteStorage_FileWrite(inputInfo.Item.PreviewImagePathFileName, previewFileName)
		If resultForPreview_SteamRemoteStorage_FileWrite <> "success" Then
			Me.thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: " + resultForPreview_SteamRemoteStorage_FileWrite + vbCrLf)
			outputInfo.Result = "Failed"
			'Return outputInfo
		End If

		If outputInfo.Result <> "Failed" Then
			If outputInfo.Result <> "Failed" AndAlso File.Exists(processedContentPathFolderOrFileName) Then
				Dim fileName As String = Path.GetFileName(processedContentPathFolderOrFileName)
				Dim resultForContent_SteamRemoteStorage_FileWrite As String = steamPipe.SteamRemoteStorage_FileWrite(processedContentPathFolderOrFileName, fileName)
				If resultForContent_SteamRemoteStorage_FileWrite <> "success" Then
					Me.thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: " + resultForContent_SteamRemoteStorage_FileWrite + vbCrLf)
					outputInfo.Result = "Failed"
					'Return outputInfo
				Else
					Dim appID_text As String = inputInfo.AppInfo.ID.ToString()

					Me.thePublishItemBackgroundWorker.ReportProgress(0, "Publishing new item." + vbCrLf)
					Dim returnedPublishedItemID As String = ""
					Dim resultOfCreateItem As String = steamPipe.SteamRemoteStorage_PublishWorkshopFile(fileName, previewFileName, appID_text, inputInfo.Item.Title, inputInfo.Item.Description, inputInfo.Item.VisibilityText, inputInfo.Item.Tags, returnedPublishedItemID)

					If resultOfCreateItem = "success" Then
						Me.thePublishItemBackgroundWorker.ReportProgress(0, "Publishing succeeded." + vbCrLf)
						outputInfo.PublishedItemID = returnedPublishedItemID
						outputInfo.Result = "Succeeded"
					ElseIf resultOfCreateItem = "success_agreement" Then
						Me.thePublishItemBackgroundWorker.ReportProgress(0, SteamPipe.AgreementMessageForCreate + vbCrLf)
						outputInfo.Result = "Succeeded"
						outputInfo.PublishedItemID = returnedPublishedItemID
						outputInfo.SteamAgreementStatus = "NotAccepted"
					Else
						Me.thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: Unable to publish workshop item. Steam error message: " + resultOfCreateItem + vbCrLf)
						outputInfo.Result = "Failed"
					End If
				End If
			End If

			inputInfo.AppInfo.CleanUpAfterUpload(Me.thePublishItemBackgroundWorker)
		End If

		Return outputInfo
	End Function

	'NOTE: This is run in a background thread.
	Private Function UpdateViaRemoteStorage(ByVal steamPipe As SteamPipe, ByVal inputInfo As PublishItemInputInfo, ByVal processedContentPathFolderOrFileName As String) As BackgroundSteamPipe.PublishItemOutputInfo
		Dim outputInfo As New BackgroundSteamPipe.PublishItemOutputInfo()
		outputInfo.PublishedItemID = "0"
		outputInfo.SteamAgreementStatus = "Accepted"

		Dim changeNote As String = inputInfo.Item.ChangeNote

		Dim result As String = ""

		Me.thePublishItemBackgroundWorker.ReportProgress(0, "Publishing non-content parts of update." + vbCrLf)
		result = Me.StartItemUpdate(steamPipe, inputInfo.AppInfo.ID.ToString(), inputInfo.Item.ID)
		If result <> "success" Then
			outputInfo.Result = "Failed"
			'Return outputInfo
		Else
			result = Me.UpdateNonContentFileOptions(steamPipe, inputInfo)
			If result <> "success" Then
				outputInfo.Result = "Failed"
				'Return outputInfo
			Else
				'NOTE: The changeNote will not be changed via this SteamUGC function call because updated item is in SteamRemoteStorage.
				result = Me.SubmitItemUpdate(steamPipe, changeNote)
				If result = "success" Then
					Me.thePublishItemBackgroundWorker.ReportProgress(0, "Publishing non-content parts of update succeeded." + vbCrLf)
					outputInfo.PublishedItemID = inputInfo.Item.ID
					outputInfo.Result = "Succeeded"
				ElseIf result = "success_agreement" Then
					Me.thePublishItemBackgroundWorker.ReportProgress(0, SteamPipe.AgreementMessageForUpdate + vbCrLf)
					outputInfo.PublishedItemID = inputInfo.Item.ID
					outputInfo.SteamAgreementStatus = "NotAccepted"
					outputInfo.Result = "Succeeded"
					'Return outputInfo
				Else
					outputInfo.Result = "Failed"
				End If

				If outputInfo.Result <> "Failed" AndAlso inputInfo.Item.ContentPathFolderOrFileNameIsChanged AndAlso inputInfo.Item.ContentPathFolderOrFileName <> "" Then
					'Delete old content file.
					'NOTE: This deletion does not seem to be needed, so do not bother user with any messages related to this.
					'If result_Crowbar_DeleteContentFile <> "success" Then
					'	Me.LogTextBox.AppendText("WARNING: " + result_Crowbar_DeleteContentFile + vbCrLf)
					'End If
					Dim result_Crowbar_DeleteContentFile_BeforeWrite As String = steamPipe.Crowbar_DeleteContentFile(inputInfo.Item.ID)

					If outputInfo.Result <> "Failed" AndAlso File.Exists(processedContentPathFolderOrFileName) Then
						' Write/upload content file to RemoteStorage (Steam Cloud).
						Dim fileName As String = Path.GetFileName(processedContentPathFolderOrFileName)
						Dim result_SteamRemoteStorage_FileWrite As String = steamPipe.SteamRemoteStorage_FileWrite(processedContentPathFolderOrFileName, fileName)
						If result_SteamRemoteStorage_FileWrite <> "success" Then
							'TODO: This error seems to occur when content file is bigger than available space for app's Steam Cloud.
							Me.thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: Not enough space on this game's Steam Cloud for content file." + result_SteamRemoteStorage_FileWrite + vbCrLf)
							outputInfo.Result = "Failed"
							'Return outputInfo
						Else
							Dim result_SteamRemoteStorage_CreatePublishedFileUpdateRequest As String = steamPipe.SteamRemoteStorage_CreatePublishedFileUpdateRequest(inputInfo.Item.ID)
							If result_SteamRemoteStorage_CreatePublishedFileUpdateRequest <> "success" Then
								Me.thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: " + result_SteamRemoteStorage_CreatePublishedFileUpdateRequest + vbCrLf)
								outputInfo.Result = "Failed"
								'Return outputInfo
							Else
								Dim result_SteamRemoteStorage_UpdatePublishedFileFile As String = steamPipe.SteamRemoteStorage_UpdatePublishedFileFile(fileName)
								If result_SteamRemoteStorage_UpdatePublishedFileFile <> "success" Then
									Me.thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: Update of content file failed." + vbCrLf)
									outputInfo.Result = "Failed"
									'Return outputInfo
								Else
									Dim updateCompletedLogText As String = "Update of content file"

									Dim result_SteamRemoteStorage_UpdatePublishedFileSetChangeDescription As String = steamPipe.SteamRemoteStorage_UpdatePublishedFileSetChangeDescription(changeNote)
									If result_SteamRemoteStorage_UpdatePublishedFileSetChangeDescription <> "success" Then
										Me.thePublishItemBackgroundWorker.ReportProgress(0, "WARNING: Update of change note failed." + vbCrLf)
									Else
										updateCompletedLogText += " and change note"
									End If

									' Copy content file from RemoteStorage (Steam Cloud) to SteamUGC storage. The copy might actually occur in an earlier function call.
									Me.thePublishItemBackgroundWorker.ReportProgress(0, "Publishing the content part of update." + vbCrLf)
									Dim result_SteamRemoteStorage_CommitPublishedFileUpdate As String = steamPipe.SteamRemoteStorage_CommitPublishedFileUpdate()
									If result_SteamRemoteStorage_CommitPublishedFileUpdate = "success" Then
										'Me.thePublishItemBackgroundWorker.ReportProgress(0, updateCompletedLogText + " completed." + vbCrLf)
										Me.thePublishItemBackgroundWorker.ReportProgress(0, "Publishing the content part of update succeeded." + vbCrLf)
										outputInfo.PublishedItemID = inputInfo.Item.ID
										outputInfo.Result = "Succeeded"
									ElseIf result_SteamRemoteStorage_CommitPublishedFileUpdate = "success_agreement" Then
										Me.thePublishItemBackgroundWorker.ReportProgress(0, SteamPipe.AgreementMessageForUpdate + vbCrLf)
										outputInfo.PublishedItemID = inputInfo.Item.ID
										outputInfo.Result = "FailedContentAndChangeNote"
										outputInfo.SteamAgreementStatus = "NotAccepted"
										outputInfo.Result = "Succeeded"
										'Return outputInfo
									Else
										Dim result_SteamRemoteStorage_FileDelete As String = steamPipe.SteamRemoteStorage_FileDelete(fileName)
										Me.thePublishItemBackgroundWorker.ReportProgress(0, result_SteamRemoteStorage_CommitPublishedFileUpdate + vbCrLf)
										outputInfo.Result = "FailedContentAndChangeNote"
										'Return outputInfo
									End If
								End If
							End If

							' Delete content file from RemoteStorage (Steam Cloud). No need to bother user with any messages related to this.
							Dim result_Crowbar_DeleteContentFile As String = steamPipe.Crowbar_DeleteContentFile(inputInfo.Item.ID)
						End If
					End If
				End If
			End If
		End If

		inputInfo.AppInfo.CleanUpAfterUpload(Me.thePublishItemBackgroundWorker)

		Return outputInfo
	End Function

	'NOTE: The SteamUGC API can be used to update all but the content-file and change note options for both RemoteStorage API and SteamUGC API.
	'NOTE: This is run in a background thread.
	Private Function UpdateNonContentFileOptions(ByVal steamPipe As SteamPipe, ByVal inputInfo As PublishItemInputInfo) As String
		Dim result As String = "success"

		If inputInfo.Item.TitleIsChanged AndAlso inputInfo.Item.Title <> "" Then
			Dim setItemTitleWasSuccessful As String = steamPipe.SteamUGC_SetItemTitle(inputInfo.Item.Title)
			If setItemTitleWasSuccessful = "success" Then
				Me.thePublishItemBackgroundWorker.ReportProgress(0, "Set item title completed." + vbCrLf)
			Else
				Me.thePublishItemBackgroundWorker.ReportProgress(0, "Set item title failed." + vbCrLf)
				Return "error"
			End If
		End If

		If inputInfo.Item.DescriptionIsChanged AndAlso inputInfo.Item.Description <> "" Then
			Dim setItemDescriptionWasSuccessful As String = steamPipe.SteamUGC_SetItemDescription(inputInfo.Item.Description)
			If setItemDescriptionWasSuccessful = "success" Then
				Me.thePublishItemBackgroundWorker.ReportProgress(0, "Set item description completed." + vbCrLf)
			Else
				Me.thePublishItemBackgroundWorker.ReportProgress(0, "Set item description failed." + vbCrLf)
				Return "error"
			End If
		End If

		If inputInfo.Item.PreviewImagePathFileNameIsChanged AndAlso inputInfo.Item.PreviewImagePathFileName <> "" Then
			Dim setItemPreviewWasSuccessful As String = steamPipe.SteamUGC_SetItemPreview(inputInfo.Item.PreviewImagePathFileName)
			If setItemPreviewWasSuccessful = "success" Then
				Me.thePublishItemBackgroundWorker.ReportProgress(0, "Set item preview completed." + vbCrLf)
			Else
				Me.thePublishItemBackgroundWorker.ReportProgress(0, "Set item preview failed." + vbCrLf)
				Return "error"
			End If
		End If

		If inputInfo.Item.VisibilityIsChanged Then
			Dim visibility_text As String = inputInfo.Item.VisibilityText
			Dim setItemVisibilityWasSuccessful As String = steamPipe.SteamUGC_SetItemVisibility(visibility_text)
			If setItemVisibilityWasSuccessful = "success" Then
				Me.thePublishItemBackgroundWorker.ReportProgress(0, "Set item visibility completed." + vbCrLf)
			Else
				Me.thePublishItemBackgroundWorker.ReportProgress(0, "Set item visibility failed." + vbCrLf)
				Return "error"
			End If
		End If

		If inputInfo.Item.TagsIsChanged Then
			Dim setItemTagsWasSuccessful As String = steamPipe.SteamUGC_SetItemTags(inputInfo.Item.Tags)
			If setItemTagsWasSuccessful = "success" Then
				Me.thePublishItemBackgroundWorker.ReportProgress(0, "Set item tags completed." + vbCrLf)
			Else
				Me.thePublishItemBackgroundWorker.ReportProgress(0, "Set item tags failed." + vbCrLf)
				Return "error"
			End If
		End If

		Return result
	End Function

	'NOTE: This is run in a background thread.
	Private Function StartItemUpdate(ByVal steamPipe As SteamPipe, ByVal appID_Text As String, ByVal itemID_text As String) As String
		Dim result As String = steamPipe.SteamUGC_StartItemUpdate(appID_Text, itemID_text)
		If result <> "success" Then
			Me.thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: Unable to start the update of item." + vbCrLf)
		End If
		Return result
	End Function

	'NOTE: This is run in a background thread.
	Private Function SubmitItemUpdate(ByVal steamPipe As SteamPipe, ByVal changeNote As String) As String
		Dim result As String = steamPipe.SteamUGC_SubmitItemUpdate(changeNote)
		If Not result.StartsWith("success") Then
			Me.thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: Unable to submit the update of item. Steam error: " + result + vbCrLf)
		End If
		Return result
	End Function

	Private thePublishItemBackgroundWorker As BackgroundWorkerEx

#End Region

#Region "Private Methods"

#End Region

#Region "Data"

	Dim theActiveSteamPipes As List(Of SteamPipe)
	Dim theActiveBackgroundWorkers As List(Of BackgroundWorkerEx)

#End Region

End Class
