Imports System.IO
Imports System.IO.Pipes
Imports System.Text
Imports System.Threading
Imports Steamworks

Public Module CrowbarSteamPipe

	Public Sub Main()
		Dim pipeNameSuffix As String = ""
		If My.Application.CommandLineArgs.Count > 0 Then
			pipeNameSuffix = My.Application.CommandLineArgs(0)
		End If
		Dim pipeClient As New NamedPipeClientStream(".", "CrowbarSteamPipe" + pipeNameSuffix, PipeDirection.InOut, PipeOptions.WriteThrough)

		' Connect to the pipe or wait until the pipe is available.
		Console.WriteLine("Attempting to connect to the pipe ...")
		pipeClient.Connect()
		Console.WriteLine("... Connected to pipe.")

		sw = New StreamWriter(pipeClient)
		sw.AutoFlush = True
		sr = New StreamReader(pipeClient)
		Dim command As String
		Try
			While pipeClient.IsConnected
				theItemIsUploading = False

				command = sr.ReadLine()
				Console.WriteLine("Command from server: " + command)

				If command = "Init" Then
					InitSteam()
				ElseIf command = "Free" Then
					FreeSteam()
					Console.WriteLine("Client is closing pipe due to command Free.")
					pipeClient.Close()

				ElseIf command = "Crowbar_DeleteContentFile" Then
					Crowbar_DeleteContentFile()
				ElseIf command = "Crowbar_DownloadContentFolderOrFile" Then
					Crowbar_DownloadContentFolderOrFile()
				ElseIf command = "Crowbar_DownloadPreviewFile" Then
					Crowbar_DownloadPreviewFile()

				ElseIf command = "SteamApps_BIsSubscribedApp" Then
					SteamApps_BIsSubscribedApp()
				ElseIf command = "SteamApps_GetAppInstallDir" Then
					SteamApps_GetAppInstallDir()

				ElseIf command = "SteamRemoteStorage_DeletePublishedFile" Then
					SteamRemoteStorage_DeletePublishedFile()
				ElseIf command = "SteamRemoteStorage_FileDelete" Then
					SteamRemoteStorage_FileDelete()
					'ElseIf command = "SteamRemoteStorage_FileRead" Then
					'	SteamRemoteStorage_FileRead()
				ElseIf command = "SteamRemoteStorage_FileWrite" Then
					SteamRemoteStorage_FileWrite()
				ElseIf command = "SteamRemoteStorage_CommitPublishedFileUpdate" Then
					SteamRemoteStorage_CommitPublishedFileUpdate()
				ElseIf command = "SteamRemoteStorage_CreatePublishedFileUpdateRequest" Then
					SteamRemoteStorage_CreatePublishedFileUpdateRequest()
				ElseIf command = "SteamRemoteStorage_GetPublishedFileDetails" Then
					SteamRemoteStorage_GetPublishedFileDetails(New PublishedFileId_t(0))
				ElseIf command = "SteamRemoteStorage_GetQuota" Then
					SteamRemoteStorage_GetQuota()
				ElseIf command = "SteamRemoteStorage_PublishWorkshopFile" Then
					SteamRemoteStorage_PublishWorkshopFile()
					'ElseIf command = "SteamRemoteStorage_UGCDownload" Then
					'	SteamRemoteStorage_UGCDownload()
					'ElseIf command = "SteamRemoteStorage_UGCDownloadToLocation" Then
					'	SteamRemoteStorage_UGCDownloadToLocation()
					'ElseIf command = "SteamRemoteStorage_UGCRead" Then
					'	SteamRemoteStorage_UGCRead()
				ElseIf command = "SteamRemoteStorage_UpdatePublishedFileFile" Then
					SteamRemoteStorage_UpdatePublishedFileFile()
				ElseIf command = "SteamRemoteStorage_UpdatePublishedFileSetChangeDescription" Then
					SteamRemoteStorage_UpdatePublishedFileSetChangeDescription()

				ElseIf command = "SteamUGC_CreateItem" Then
					SteamUGC_CreateItem()
				ElseIf command = "SteamUGC_CreateQueryUGCDetailsRequest" Then
					SteamUGC_CreateQueryUGCDetailsRequest()
				ElseIf command = "SteamUGC_CreateQueryUserUGCRequest" Then
					SteamUGC_CreateQueryUserUGCRequest()
				ElseIf command = "SteamUGC_DeleteItem" Then
					SteamUGC_DeleteItem()
					'ElseIf command = "SteamUGC_DownloadItem" Then
					'	SteamUGC_DownloadItem()
					'ElseIf command = "SteamUGC_GetItemUpdateProgress" Then
					'	SteamUGC_GetItemUpdateProgress()
				ElseIf command = "SteamUGC_SendQueryUGCRequest" Then
					SteamUGC_SendQueryUGCRequest()
				ElseIf command = "SteamUGC_SetItemContent" Then
					SteamUGC_SetItemContent()
				ElseIf command = "SteamUGC_SetItemDescription" Then
					SteamUGC_SetItemDescription()
				ElseIf command = "SteamUGC_SetItemPreview" Then
					SteamUGC_SetItemPreview()
				ElseIf command = "SteamUGC_SetItemTags" Then
					SteamUGC_SetItemTags()
				ElseIf command = "SteamUGC_SetItemTitle" Then
					SteamUGC_SetItemTitle()
				ElseIf command = "SteamUGC_SetItemUpdateLanguage" Then
					SteamUGC_SetItemUpdateLanguage()
				ElseIf command = "SteamUGC_SetItemVisibility" Then
					SteamUGC_SetItemVisibility()
				ElseIf command = "SteamUGC_StartItemUpdate" Then
					SteamUGC_StartItemUpdate()
				ElseIf command = "SteamUGC_SubmitItemUpdate" Then
					SteamUGC_SubmitItemUpdate()
				ElseIf command = "SteamUGC_UnsubscribeItem" Then
					SteamUGC_UnsubscribeItem()

				ElseIf command = "SteamUser_GetSteamID" Then
					SteamUser_GetSteamID()
				End If
			End While
		Catch ex As Exception
			Console.WriteLine("EXCEPTION: " + ex.Message)
		Finally
			If pipeClient IsNot Nothing Then
				Console.WriteLine("Client is closing pipe.")
				pipeClient.Close()
				pipeClient = Nothing
			End If

#If DEBUG Then
			'For debugging, keep console open until Enter Is pressed.
			Console.ReadLine()
#End If
        End Try
	End Sub

#Region "Init and Free"

	Private Sub InitSteam()
		Dim result As String

		If SteamAPI.Init() Then
			Console.WriteLine("SteamAPI.Init()")
			result = "success"
		Else
			Console.WriteLine("SteamAPI.Init() failed.")
			result = "error"
		End If

		sw.WriteLine(result)
	End Sub

	Private Sub FreeSteam()
		SteamAPI.Shutdown()
		Console.WriteLine("SteamAPI.Shutdown()")
	End Sub

#End Region

#Region "Crowbar"

	'NOTE: These are convenience functions that combine calls to Steam functions to save time 
	'      by avoiding the passing of several values over the named pipes.

#Region "Crowbar_DeleteContentFile"

	Private Sub Crowbar_DeleteContentFile()
		Dim itemID As PublishedFileId_t
		Dim itemID_text As String
		itemID_text = sr.ReadLine()
		itemID.m_PublishedFileId = ULong.Parse(itemID_text)

		Dim result As SteamAPICall_t = SteamRemoteStorage.GetPublishedFileDetails(itemID, 0)
		CrowbarSteamPipe.SetResultAndRunCallbacks(Of RemoteStorageGetPublishedFileDetailsResult_t)(AddressOf OnGetPublishedFileDetailsForInternalResults, result)

		If theResultMessage = "success" Then
			Dim publishedFileExists As Boolean = SteamRemoteStorage_FileExists(theItemContentFileName)
			SteamRemoteStorage_FileDelete(theItemContentFileName)
		Else
			sw.WriteLine(theResultMessage)
		End If
	End Sub

	Private Sub OnGetPublishedFileDetailsForInternalResults(ByVal pCallResult As RemoteStorageGetPublishedFileDetailsResult_t, ByVal bIOFailure As Boolean)
		Try
			If pCallResult.m_eResult = EResult.k_EResultOK Then
				Console.WriteLine("OnGetPublishedFileDetailsForInternalResults - success")

				theUGCHandleForContentFile = pCallResult.m_hFile
				theUGCHandleForPreviewImageFile = pCallResult.m_hPreviewFile
				'theUGCPreviewImageFileSize = pCallResult.m_nPreviewFileSize

				'theAppID = pCallResult.m_nConsumerAppID
				theItemTitle = pCallResult.m_rgchTitle
				theItemUpdated = pCallResult.m_rtimeUpdated
				theItemContentFileName = pCallResult.m_pchFileName
				theAppID = pCallResult.m_nConsumerAppID

				theResultMessage = "success"
			Else
				Console.WriteLine("OnGetPublishedFileDetailsForInternalResults ERROR: " + pCallResult.m_eResult.ToString())
				theResultMessage = GetErrorMessage(pCallResult.m_eResult)
			End If
		Catch ex As Exception
			Console.WriteLine("EXCEPTION: " + ex.Message)
			theResultMessage = "EXCEPTION: " + ex.Message
		End Try

		theCallResultIsFinished = True
	End Sub

#End Region

#Region "Crowbar_DownloadContentFolderOrFile"

	Private Sub Crowbar_DownloadContentFolderOrFile()
		Dim itemID_text As String
		itemID_text = sr.ReadLine()
		theItemID.m_PublishedFileId = ULong.Parse(itemID_text)
		Dim targetPath As String
		targetPath = sr.ReadLine()

		Dim itemState As Steamworks.EItemState = CType(SteamUGC.GetItemState(theItemID), EItemState)
		Console.WriteLine("Crowbar_DownloadContentFolderOrFile - GetItemState " + GetItemStateText(itemState))
		If (itemState And Steamworks.EItemState.k_EItemStateLegacyItem) <> 0 Then
			Console.WriteLine("Crowbar_DownloadContentFolderOrFile - Download via RemoteStorage")

			Dim result As SteamAPICall_t = SteamRemoteStorage.GetPublishedFileDetails(theItemID, 0)
			CrowbarSteamPipe.SetResultAndRunCallbacks(Of RemoteStorageGetPublishedFileDetailsResult_t)(AddressOf OnGetPublishedFileDetailsForInternalResults, result)

			'Console.WriteLine("Crowbar_DownloadContentFolderOrFile - theUGCHandleForContentFile: " + theUGCHandleForContentFile.ToString())

			result = SteamRemoteStorage.UGCDownload(theUGCHandleForContentFile, 0)
			CrowbarSteamPipe.SetResultAndRunCallbacks(Of RemoteStorageDownloadUGCResult_t)(AddressOf OnDownloadUGC_ContentFile, result)
			'ElseIf (itemState And Steamworks.EItemState.k_EItemStateInstalled) <> 0 Then
			'	'NOTE: Must init these 4 vars with values; otherwise only zeroes and an empty string are returned in them from GetItemInstallInfo().
			'	'NOTE: The value for itemFolderNameLength must be greater than 0, presumably to tell Steam how many characters itemFolderName can hold.
			'	Dim itemSize As ULong = 0
			'	Dim itemFolderName As String = ""
			'	Dim itemFolderNameLength As UInteger = 1024
			'	Dim itemUpdated As UInteger = 0
			'	Dim resultOfGetItemInstallInfoIsSuccess As Boolean = SteamUGC.GetItemInstallInfo(theItemID, itemSize, itemFolderName, itemFolderNameLength, itemUpdated)
			'	Console.WriteLine("Crowbar_DownloadContentFolderOrFile - GetItemInstallInfo: " + theItemID.ToString() + " " + itemSize.ToString() + " """ + itemFolderName + """ " + itemUpdated.ToString())
			'	If resultOfGetItemInstallInfoIsSuccess Then
			'		sw.WriteLine("success_SteamUGC")
			'		sw.WriteLine(itemUpdated)
			'		Dim result As SteamAPICall_t = SteamRemoteStorage.GetPublishedFileDetails(theItemID, 0)
			'		CrowbarSteamPipe.SetResultAndRunCallbacks(Of RemoteStorageGetPublishedFileDetailsResult_t)(AddressOf OnGetPublishedFileDetailsForInternalResults, result)
			'		WriteTextThatMightHaveMultipleLines(theItemTitle)
			'		sw.WriteLine(itemFolderName)
			'		sw.WriteLine(theAppID.ToString())
			'	Else
			'		sw.WriteLine("error")
			'	End If

		ElseIf (itemState And Steamworks.EItemState.k_EItemStateInstalled) <> 0 OrElse (itemState And Steamworks.EItemState.k_EItemStateNeedsUpdate) <> 0 OrElse itemState = EItemState.k_EItemStateNone Then
			'NOTE: Even if Steam thinks the item is installed, download it anyway.
			Console.WriteLine("Crowbar_DownloadContentFolderOrFile - Download via SteamUGC")

			Dim result As SteamAPICall_t = SteamRemoteStorage.GetPublishedFileDetails(theItemID, 0)
			CrowbarSteamPipe.SetResultAndRunCallbacks(Of RemoteStorageGetPublishedFileDetailsResult_t)(AddressOf OnGetPublishedFileDetailsForInternalResults, result)

			'NOTE: Using SteamUGC.BInitWorkshopForGameServer made extra folders in the target path 
			'      and also copied entire content folder that includes subscribed and other installed workshop items.
			'Console.WriteLine("Crowbar_DownloadContentFolderOrFile - BInitWorkshopForGameServer: """ + targetPath + """")
			'Dim depotID As DepotId_t
			'depotID.m_DepotId = theAppID.m_AppId
			'Dim resultOfInitIsSuccess As Boolean = SteamUGC.BInitWorkshopForGameServer(depotID, targetPath)

			'If resultOfInitIsSuccess Then
			'	Console.WriteLine("Crowbar_DownloadContentFolderOrFile - BInitWorkshopForGameServer success")

			Console.WriteLine("Crowbar_DownloadContentFolderOrFile - itemID: " + theItemID.ToString())
			Dim resultIsSuccess As Boolean = SteamUGC.DownloadItem(theItemID, True)

			If resultIsSuccess Then
				Console.WriteLine("Crowbar_DownloadContentFolderOrFile - DownloadItem success")
				Dim DownloadItemCallback As Callback(Of DownloadItemResult_t)
				Dim InstalledItemCallback As Callback(Of ItemInstalled_t)
				DownloadItemCallback = Callback(Of DownloadItemResult_t).Create(AddressOf OnDownloadItem)
				InstalledItemCallback = Callback(Of ItemInstalled_t).Create(AddressOf OnInstalledItem)

				CrowbarSteamPipe.RunCallbacks()

				If theResultMessage = "success" Then
					Console.WriteLine("Crowbar_DownloadContentFolderOrFile - OnDownloadItem success")

					itemState = CType(SteamUGC.GetItemState(theItemID), EItemState)
					Console.WriteLine("Crowbar_DownloadContentFolderOrFile - GetItemState " + GetItemStateText(itemState))

					'NOTE: Must init these 4 vars with values; otherwise only zeroes and an empty string are returned in them from GetItemInstallInfo().
					'NOTE: The value for itemFolderNameLength must be greater than 0, presumably to tell Steam how many characters folderName can hold.
					Dim itemSize As ULong = 0
					Dim folderName As String = ""
					Dim folderNameLength As UInteger = 1024
					Dim updated As UInteger = 0
					Dim resultOfGetItemInstallInfoIsSuccess As Boolean = SteamUGC.GetItemInstallInfo(theItemID, itemSize, folderName, folderNameLength, updated)
					Console.WriteLine("Crowbar_DownloadContentFolderOrFile - GetItemInstallInfo: " + theItemID.ToString() + " " + itemSize.ToString() + " """ + folderName + """ " + updated.ToString())
					Dim fileExists As Boolean = SteamRemoteStorage_FileExists(theItemContentFileName)
					Console.WriteLine("Crowbar_DownloadContentFolderOrFile - publishedFileExists = " + fileExists.ToString())
					If resultOfGetItemInstallInfoIsSuccess Then
						Console.WriteLine("Crowbar_DownloadContentFolderOrFile - GetItemInstallInfo success")
						sw.WriteLine("success_SteamUGC")
						sw.WriteLine(updated)
						'Dim result As SteamAPICall_t = SteamRemoteStorage.GetPublishedFileDetails(theItemID, 0)
						'CrowbarSteamPipe.SetResultAndRunCallbacks(Of RemoteStorageGetPublishedFileDetailsResult_t)(AddressOf OnGetPublishedFileDetailsForInternalResults, result)
						WriteTextThatMightHaveMultipleLines(theItemTitle)
						sw.WriteLine(folderName)
						sw.WriteLine(theAppID.ToString())
					Else
						Console.WriteLine("Crowbar_DownloadContentFolderOrFile - GetItemInstallInfo error")
						sw.WriteLine("error")
					End If
				Else
					Console.WriteLine("Crowbar_DownloadContentFolderOrFile - OnDownloadItem error")
					sw.WriteLine("error")
				End If
			Else
				Console.WriteLine("Crowbar_DownloadContentFolderOrFile - DownloadItem error")
				sw.WriteLine("error")
			End If
			'Else
			'	Console.WriteLine("Crowbar_DownloadContentFolderOrFile - BInitWorkshopForGameServer error")
			'	sw.WriteLine("error")
			'End If
		End If
		Console.WriteLine("Crowbar_DownloadContentFolderOrFile - End Sub")
	End Sub

	Private Sub OnDownloadUGC_ContentFile(ByVal pCallResult As RemoteStorageDownloadUGCResult_t, ByVal bIOFailure As Boolean)
		Try
			If pCallResult.m_eResult = EResult.k_EResultOK Then
				sw.WriteLine("success")
				Console.WriteLine("OnDownloadUGC_ContentFile - file name: " + pCallResult.m_pchFileName)
				Console.WriteLine("OnDownloadUGC_ContentFile - byte size: " + pCallResult.m_nSizeInBytes.ToString())

				sw.WriteLine(theItemUpdated)
				WriteTextThatMightHaveMultipleLines(theItemTitle)
				sw.WriteLine(pCallResult.m_pchFileName)

				Dim data As Byte() = New Byte(pCallResult.m_nSizeInBytes - 1) {}
				Dim byteCountRead As Integer = SteamRemoteStorage.UGCRead(theUGCHandleForContentFile, data, pCallResult.m_nSizeInBytes, 0, EUGCReadAction.k_EUGCRead_ContinueReadingUntilFinished)
				sw.WriteLine(data.Length.ToString())
				sw.BaseStream.Write(data, 0, data.Length)
			Else
				Console.WriteLine("OnDownloadUGC_ContentFile ERROR: " + pCallResult.m_eResult.ToString())
				sw.WriteLine(GetErrorMessage(pCallResult.m_eResult))
			End If
		Catch ex As Exception
			Console.WriteLine("OnDownloadUGC_ContentFile EXCEPTION: " + ex.Message)
		End Try

		theCallResultIsFinished = True
	End Sub

	Private Sub OnDownloadItem(ByVal pCallback As DownloadItemResult_t)
		Console.WriteLine("OnDownloadItem")
		Try
			theResultMessage = "error"
			If pCallback.m_nPublishedFileId = theItemID Then
				If pCallback.m_eResult = EResult.k_EResultOK Then
					Console.WriteLine("OnDownloadItem - success")

					theResultMessage = "success"
				Else
					Console.WriteLine("OnDownloadItem ERROR: " + pCallback.m_eResult.ToString())
					theResultMessage = pCallback.m_eResult.ToString()
				End If

				theCallResultIsFinished = True
			End If
		Catch ex As Exception
			Console.WriteLine("OnDownloadItem EXCEPTION: " + ex.Message)
			theResultMessage = "OnDownloadItem EXCEPTION: " + ex.Message
		End Try
	End Sub

	Private Sub OnInstalledItem(ByVal pCallback As ItemInstalled_t)
		Console.WriteLine("OnInstalledItem")
		Try
			'theResultMessage = "error"
			If pCallback.m_nPublishedFileId = theItemID Then
				Console.WriteLine("OnInstalledItem")
				'theResultMessage = "success - success"

				'theCallResultIsFinished = True
			End If
		Catch ex As Exception
			Console.WriteLine("OnInstalledItem EXCEPTION: " + ex.Message)
			'theResultMessage = "OnInstalledItem EXCEPTION: " + ex.Message
		End Try
	End Sub

#End Region

#Region "Crowbar_DownloadPreviewFile"

	Private Sub Crowbar_DownloadPreviewFile()
		'Dim fileID As PublishedFileId_t
		'Dim fileID_Text As String
		'fileID_Text = sr.ReadLine()
		'fileID.m_PublishedFileId = ULong.Parse(fileID_Text)

		'theCallResultIsFinished = False
		'GetPublishedFileDetailsCallResult = CallResult(Of RemoteStorageGetPublishedFileDetailsResult_t).Create(AddressOf OnGetPublishedFileDetailsForInternalResults)
		'Dim result As SteamAPICall_t
		'result = SteamRemoteStorage.GetPublishedFileDetails(fileID, 0)
		'GetPublishedFileDetailsCallResult.Set(result)

		'While Not theCallResultIsFinished
		'	SteamAPI.RunCallbacks()
		'End While

		Dim result As SteamAPICall_t = SteamRemoteStorage.UGCDownload(theUGCHandleForPreviewImageFile, 0)
		CrowbarSteamPipe.SetResultAndRunCallbacks(Of RemoteStorageDownloadUGCResult_t)(AddressOf OnUGCDownload_PreviewFile, result)
	End Sub

	Private Sub OnUGCDownload_PreviewFile(ByVal pCallResult As RemoteStorageDownloadUGCResult_t, ByVal bIOFailure As Boolean)
		Try
			If pCallResult.m_eResult = EResult.k_EResultOK Then
				Console.WriteLine("OnDownloadUGC_PreviewFile - file name: " + pCallResult.m_pchFileName)
				Console.WriteLine("OnDownloadUGC_PreviewFile - byte size: " + pCallResult.m_nSizeInBytes.ToString())

				Dim data As Byte() = New Byte(pCallResult.m_nSizeInBytes - 1) {}
				'data(0) = 1
				'data(1) = 2
				'data(2) = 3
				Dim byteCountRead As Integer = SteamRemoteStorage.UGCRead(theUGCHandleForPreviewImageFile, data, pCallResult.m_nSizeInBytes, 0, EUGCReadAction.k_EUGCRead_ContinueReadingUntilFinished)

				'Console.WriteLine("OnDownloadUGC_PreviewFile UGCRead - size: " + pCallResult.m_nSizeInBytes.ToString())
				'Console.WriteLine("OnDownloadUGC_PreviewFile UGCRead - byteCountRead: " + byteCountRead.ToString())
				'Console.WriteLine("OnDownloadUGC_PreviewFile UGCRead - data size: " + data.Length.ToString())
				'Console.WriteLine("OnDownloadUGC_PreviewFile UGCRead - data: " + data.ToString())
				'Console.WriteLine(data(0))
				'Console.WriteLine(data(1))
				'Console.WriteLine(data(2))

				sw.WriteLine("success")
				sw.WriteLine(pCallResult.m_pchFileName)
				sw.WriteLine(data.Length.ToString())
				sw.BaseStream.Write(data, 0, data.Length)
			Else
				Console.WriteLine("OnDownloadUGC_PreviewFile ERROR: " + pCallResult.m_eResult.ToString())
				sw.WriteLine(GetErrorMessage(pCallResult.m_eResult))
			End If
		Catch ex As Exception
			Console.WriteLine("OnDownloadUGC_PreviewFile EXCEPTION: " + ex.Message)
		End Try

		theCallResultIsFinished = True
	End Sub

#End Region

#End Region

#Region "SteamApps"

#Region "SteamApps_BIsSubscribedApp"

	Private Sub SteamApps_BIsSubscribedApp()
		Dim appID_Text As String
		appID_Text = sr.ReadLine()
		Dim appID As AppId_t
		appID.m_AppId = UInteger.Parse(appID_Text)

		Console.WriteLine("SteamApps_BIsSubscribedApp appID_Text: " + appID_Text)
		Dim resultIsSuccess As Boolean = SteamApps.BIsSubscribedApp(appID)
		Console.WriteLine("SteamApps_BIsSubscribedApp = " + resultIsSuccess.ToString())
		If resultIsSuccess Then
			sw.WriteLine("success")
		Else
			sw.WriteLine("error")
		End If
	End Sub

#End Region

#Region "SteamApps_GetAppInstallDir"

	' SteamApps.GetAppInstallDir(AppId_t appID, char *pchFolder, uint32 cchFolderBufferSize)
	'    Gets the install folder for a specific AppID.
	'    This works even if the application is not installed, based on where the game would be installed with the default Steam library location.
	'    Returns: uint32
	'    Returns the install directory path as a string into the buffer provided in pchFolder and returns the number of bytes that were copied into that buffer.
	Private Sub SteamApps_GetAppInstallDir()
		Dim appID_Text As String
		appID_Text = sr.ReadLine()
		Dim appID As AppId_t
		appID.m_AppId = UInteger.Parse(appID_Text)

		Dim appInstallPath As String = ""
		Dim appInstallPathLength As UInteger = 1024
		Console.WriteLine("SteamApps_GetAppInstallDir appID_Text: " + appID_Text)
		Dim appInstallPathActualLength As UInteger = SteamApps.GetAppInstallDir(appID, appInstallPath, appInstallPathLength)
		Console.WriteLine("SteamApps_GetAppInstallDir appInstallPath(" + appInstallPathActualLength.ToString() + "): " + appInstallPath)
		If appInstallPathActualLength > 0 Then
			sw.WriteLine("success")
			sw.WriteLine(appInstallPath)
		Else
			sw.WriteLine("error")
		End If
	End Sub

#End Region

#End Region

#Region "SteamRemoteStorage"

#Region "SteamRemoteStorage_DeletePublishedFile"

	Private Sub SteamRemoteStorage_DeletePublishedFile()
		Dim itemID_text As String
		itemID_text = sr.ReadLine()
		Dim itemID As PublishedFileId_t
		itemID.m_PublishedFileId = ULong.Parse(itemID_text)

		Dim result As SteamAPICall_t = SteamRemoteStorage.DeletePublishedFile(itemID)
		CrowbarSteamPipe.SetResultAndRunCallbacks(Of RemoteStorageDeletePublishedFileResult_t)(AddressOf OnDeletePublishedFile, result)
	End Sub

	Private Sub OnDeletePublishedFile(ByVal pCallResult As RemoteStorageDeletePublishedFileResult_t, ByVal bIOFailure As Boolean)
		Try
			If pCallResult.m_eResult = EResult.k_EResultOK Then
				Console.WriteLine("OnDeletePublishedFile - success")
				sw.WriteLine("success")
			Else
				Console.WriteLine("OnDeletePublishedFile ERROR: " + pCallResult.m_eResult.ToString())
				sw.WriteLine(GetErrorMessage(pCallResult.m_eResult))
			End If
		Catch ex As Exception
			Console.WriteLine("EXCEPTION: " + ex.Message)
			sw.WriteLine("EXCEPTION: " + ex.Message)
		End Try

		theCallResultIsFinished = True
	End Sub

#End Region

#Region "SteamRemoteStorage_FileDelete"

	Private Sub SteamRemoteStorage_FileDelete()
		Dim targetFileName As String
		targetFileName = sr.ReadLine()

		SteamRemoteStorage_FileDelete(targetFileName)
	End Sub

	Private Sub SteamRemoteStorage_FileDelete(ByVal targetFileName As String)
		Console.WriteLine("SteamRemoteStorage_FileDelete targetFileName: " + targetFileName)
		Dim resultIsSuccess As Boolean = SteamRemoteStorage.FileDelete(targetFileName)

		If resultIsSuccess Then
			sw.WriteLine("success")
		Else
			sw.WriteLine("error")
		End If
	End Sub

#End Region

#Region "SteamRemoteStorage_FileExists"

	Private Function SteamRemoteStorage_FileExists(ByVal targetFileName As String) As Boolean
		Console.WriteLine("SteamRemoteStorage_FileExists targetFileName: " + targetFileName)
		Dim resultIsSuccess As Boolean = SteamRemoteStorage.FileExists(targetFileName)
		Console.WriteLine("SteamRemoteStorage_FileExists = " + resultIsSuccess.ToString())
		Return resultIsSuccess
	End Function

#End Region

#Region "SteamRemoteStorage_FileRead"

	'Private Sub SteamRemoteStorage_FileRead()
	'	Dim fileName As String
	'	fileName = sr.ReadLine()

	'	Dim size As Int32 = SteamRemoteStorage.GetFileSize(fileName)

	'	Dim data As Byte() = New Byte(size) {}
	'	Dim byteCountRead As Integer = SteamRemoteStorage.FileRead(fileName, data, size)

	'	Console.WriteLine("fileName: " + fileName)
	'	Console.WriteLine("size: " + size.ToString())
	'	Console.WriteLine("byteCountRead: " + byteCountRead.ToString())
	'	Console.WriteLine("data: " + data.ToString())

	'	sw.WriteLine(byteCountRead.ToString())
	'	sw.Write(data)
	'	'For Each aByte In data
	'	'	sw.Write(aByte)
	'	'Next
	'End Sub

#End Region

#Region "SteamRemoteStorage_FileWrite"

	Private Sub SteamRemoteStorage_FileWrite()
		Dim sourcePathFileName As String
		sourcePathFileName = sr.ReadLine()
		Dim targetFileName As String
		targetFileName = sr.ReadLine()

		Dim resultIsSuccess As Boolean = False
		Dim sourceFileInfo As New FileInfo(sourcePathFileName)
		If sourceFileInfo.Length < Steamworks.Constants.k_unMaxCloudFileChunkSize Then
			Dim data As Byte() = File.ReadAllBytes(sourcePathFileName)
			resultIsSuccess = SteamRemoteStorage.FileWrite(targetFileName, data, data.Length)
			If resultIsSuccess Then
				sw.WriteLine("success")
			Else
				sw.WriteLine("error")
			End If
		Else
			Using sourceStream As New FileStream(sourcePathFileName, FileMode.Open, FileAccess.Read, FileShare.Read)
				'Returns k_UGCFileStreamHandleInvalid under the following conditions:
				'    You tried to write to an invalid path or filename. Because Steam Cloud is cross platform the files need to have valid names on all supported OSes and file systems. See Microsoft's documentation on Naming Files, Paths, and Namespaces.
				'    The current user's Steam Cloud storage quota has been exceeded. They may have run out of space, or have too many files.
				Console.WriteLine("targetFileName: " + targetFileName.ToString())
				Dim writeHandle As UGCFileWriteStreamHandle_t
				writeHandle = SteamRemoteStorage.FileWriteStreamOpen(targetFileName)
				If writeHandle <> UGCFileWriteStreamHandle_t.Invalid Then
					Dim byteCountRead As Integer = 1
					Dim data() As Byte = New Byte(Steamworks.Constants.k_unMaxCloudFileChunkSize - 1) {}
					While byteCountRead > 0
						'Console.WriteLine("byteCountRead: " + byteCountRead.ToString())
						Console.WriteLine("data.Length: " + data.Length.ToString())
						byteCountRead = sourceStream.Read(data, 0, data.Length)
						If byteCountRead > 0 Then
							'true if the data was successfully written to the file write stream.
							'false if writeHandle is not a valid file write stream, cubData is negative or larger than k_unMaxCloudFileChunkSize, or the current user's Steam Cloud storage quota has been exceeded. They may have run out of space, or have too many files.
							resultIsSuccess = SteamRemoteStorage.FileWriteStreamWriteChunk(writeHandle, data, byteCountRead)
							Console.WriteLine("FileWriteStreamWriteChunk: " + resultIsSuccess.ToString())
							If Not resultIsSuccess Then
								Exit While
							End If
						End If
					End While

					'resultIsSuccess = SteamRemoteStorage.FileWriteStreamClose(writeHandle)
					'If resultIsSuccess Then
					'	sw.WriteLine("success")
					'Else
					'	sw.WriteLine("error")
					'End If
					Console.WriteLine("FileWriteStreamClose")
					SteamRemoteStorage.FileWriteStreamClose(writeHandle)
					If resultIsSuccess Then
						sw.WriteLine("success")
					Else
						sw.WriteLine("error")
					End If
				Else
					'TODO: Check if file already exists. If so, tell user about it. Maybe it is locked by another app.
					sw.WriteLine("error")
				End If
			End Using
		End If
	End Sub

#End Region

#Region "SteamRemoteStorage_CommitPublishedFileUpdate"

	Private Sub SteamRemoteStorage_CommitPublishedFileUpdate()
		Dim result As SteamAPICall_t = SteamRemoteStorage.CommitPublishedFileUpdate(thePublishedFileUpdateHandle)
		CrowbarSteamPipe.SetResultAndRunCallbacks(Of RemoteStorageUpdatePublishedFileResult_t)(AddressOf OnCommitPublishedFileUpdate, result)
	End Sub

	Private Sub OnCommitPublishedFileUpdate(ByVal pCallResult As RemoteStorageUpdatePublishedFileResult_t, ByVal bIOFailure As Boolean)
		Try
			If pCallResult.m_eResult = EResult.k_EResultOK Then
				If pCallResult.m_bUserNeedsToAcceptWorkshopLegalAgreement Then
					sw.WriteLine("success_agreement")
				Else
					sw.WriteLine("success")
				End If
			Else
				sw.WriteLine(GetErrorMessage(pCallResult.m_eResult))
			End If
		Catch ex As Exception
			Console.WriteLine("EXCEPTION: " + ex.Message)
			sw.WriteLine("EXCEPTION: " + ex.Message)
		End Try

		theCallResultIsFinished = True
	End Sub

#End Region

#Region "SteamRemoteStorage_CreatePublishedFileUpdateRequest"

	Private Sub SteamRemoteStorage_CreatePublishedFileUpdateRequest()
		Dim fileID_Text As String
		fileID_Text = sr.ReadLine()
		Dim fileID As PublishedFileId_t
		fileID.m_PublishedFileId = ULong.Parse(fileID_Text)

		thePublishedFileUpdateHandle = SteamRemoteStorage.CreatePublishedFileUpdateRequest(fileID)
		If thePublishedFileUpdateHandle <> PublishedFileUpdateHandle_t.Invalid Then
			sw.WriteLine("success")
		Else
			sw.WriteLine("error")
		End If
	End Sub

#End Region

#Region "SteamRemoteStorage_GetPublishedFileDetails"

	Private Sub SteamRemoteStorage_GetPublishedFileDetails(ByVal itemID As PublishedFileId_t)
		If itemID.m_PublishedFileId = 0 Then
			Dim itemID_text As String
			itemID_text = sr.ReadLine()
			itemID.m_PublishedFileId = ULong.Parse(itemID_text)
		End If

		Console.WriteLine("SteamRemoteStorage_GetPublishedFileDetails - fileID = " + itemID.ToString())

		Dim result As SteamAPICall_t = SteamRemoteStorage.GetPublishedFileDetails(itemID, 0)
		CrowbarSteamPipe.SetResultAndRunCallbacks(Of RemoteStorageGetPublishedFileDetailsResult_t)(AddressOf OnGetPublishedFileDetails, result)
	End Sub

	Private Sub OnGetPublishedFileDetails(ByVal pCallResult As RemoteStorageGetPublishedFileDetailsResult_t, ByVal bIOFailure As Boolean)
		Try
			Console.WriteLine("OnGetPublishedFileDetails")
			If pCallResult.m_eResult = EResult.k_EResultOK Then
				sw.WriteLine("success")
				sw.WriteLine(pCallResult.m_nPublishedFileId)
				sw.WriteLine(pCallResult.m_nCreatorAppID)
				sw.WriteLine(pCallResult.m_nConsumerAppID)
				sw.WriteLine(pCallResult.m_ulSteamIDOwner)
				sw.WriteLine(SteamFriends.GetFriendPersonaName(New CSteamID(pCallResult.m_ulSteamIDOwner)))
				sw.WriteLine(pCallResult.m_rtimeCreated)
				sw.WriteLine(pCallResult.m_rtimeUpdated)
				WriteTextThatMightHaveMultipleLines(pCallResult.m_rgchTitle)
				WriteTextThatMightHaveMultipleLines(pCallResult.m_rgchDescription)
				Console.WriteLine("OnGetPublishedFileDetails - pCallResult.m_rgchDescription: " + pCallResult.m_rgchDescription)
				sw.WriteLine(pCallResult.m_nFileSize)
				sw.WriteLine(pCallResult.m_pchFileName)
				sw.WriteLine(pCallResult.m_nPreviewFileSize)
				'NOTE: URL is probably not preview file name. There does not seem to be a way to get preview file name.
				sw.WriteLine(pCallResult.m_rgchURL)
				sw.WriteLine(CType(pCallResult.m_eVisibility, Steamworks.ERemoteStoragePublishedFileVisibility).ToString("d"))
				sw.WriteLine(pCallResult.m_rgchTags)

				theUGCHandleForContentFile = pCallResult.m_hFile
				Console.WriteLine("OnGetPublishedFileDetails - pCallResult.m_hFile: " + pCallResult.m_hFile.ToString())
				theUGCHandleForPreviewImageFile = pCallResult.m_hPreviewFile
			Else
				sw.WriteLine(GetErrorMessage(pCallResult.m_eResult))
			End If
		Catch ex As Exception
			Console.WriteLine("EXCEPTION: " + ex.Message)
			sw.WriteLine("EXCEPTION: " + ex.Message)
		End Try

		theCallResultIsFinished = True
	End Sub

#End Region

#Region "SteamRemoteStorage_GetQuota"

	Private Sub SteamRemoteStorage_GetQuota()
		Dim availableBytes As ULong
		Dim totalBytes As ULong

		Dim resultIsSuccess As Boolean = SteamRemoteStorage.GetQuota(totalBytes, availableBytes)
		If resultIsSuccess Then
			sw.WriteLine("success")
			sw.WriteLine(availableBytes)
			sw.WriteLine(totalBytes)
		Else
			sw.WriteLine("error")
		End If
	End Sub

#End Region

#Region "SteamRemoteStorage_PublishWorkshopFile"

	Private Sub SteamRemoteStorage_PublishWorkshopFile()
		Console.WriteLine("SteamRemoteStorage_PublishWorkshopFile")
		Dim contentFileName As String
		contentFileName = sr.ReadLine()
		Console.WriteLine("SteamRemoteStorage_PublishWorkshopFile - contentFileName: " + contentFileName)
		Dim previewFileName As String
		previewFileName = sr.ReadLine()
		Dim appID_Text As String
		appID_Text = sr.ReadLine()
		Dim appID As AppId_t
		appID.m_AppId = UInteger.Parse(appID_Text)
		Dim title As String
		title = ReadMultipleLinesOfText(sr)
		Dim description As String
		description = ReadMultipleLinesOfText(sr)
		Dim visibility_text As String
		visibility_text = sr.ReadLine()
		Dim visibility As ERemoteStoragePublishedFileVisibility
		'visibility = CType([Enum].Parse(GetType(ERemoteStoragePublishedFileVisibility), visibility_text), ERemoteStoragePublishedFileVisibility)
		visibility = ConvertVisibilityFromCrowbarToSteamworks(visibility_text)

		Dim tagCountText As String
		tagCountText = sr.ReadLine()
		Dim tagCount As Integer
		tagCount = Integer.Parse(tagCountText)
		Dim tags As New List(Of String)(tagCount)
		Dim tag As String
		For i As Integer = 0 To tagCount - 1
			tag = sr.ReadLine()
			tags.Add(tag)
		Next

		' Sending Nothing for the Tags param causes an exception.
		Dim result As SteamAPICall_t = SteamRemoteStorage.PublishWorkshopFile(contentFileName, previewFileName, appID, title, description, visibility, tags, EWorkshopFileType.k_EWorkshopFileTypeCommunity)
		'NOTE: Using RemoteStoragePublishFileResult_t even though docs indicate using RemoteStoragePublishFileProgress_t, 
		'      but RemoteStoragePublishFileProgress_t has no return value for the published file ID.
		'      In testing, RemoteStoragePublishFileProgress_t handler is only called once with progress of 0 
		'      and seems to be called right before RemoteStoragePublishFileResult_t handler;
		'      also, RemoteStoragePublishFileResult_t handler gets an error even though item uploaded.
		'      Using RemoteStoragePublishFileResult_t has been tested and it does indeed return the published file ID.
		'CrowbarSteamPipe.SetResultAndRunCallbacks(Of RemoteStoragePublishFileProgress_t)(AddressOf OnPublishFileProgress, result)
		CrowbarSteamPipe.SetResultAndRunCallbacks(Of RemoteStoragePublishFileResult_t)(AddressOf OnPublishFileResult, result)
	End Sub

	Private Sub OnPublishFileProgress(ByVal pCallResult As RemoteStoragePublishFileProgress_t, ByVal bIOFailure As Boolean)
		'If pCallResult.m_bPreview Then
		'    Me.UploadResultsTextBox.Text += "PublishFileProgress: Preview is True" + vbCrLf
		'End If
		'sw.WriteLine("Done")

		'theCallResultIsFinished = True

		'======

		Console.WriteLine("OnPublishFileProgress: " + pCallResult.m_dPercentFile.ToString())
		'If pCallResult.m_dPercentFile >= 100 Then
		'	theCallResultIsFinished = True
		'End If
	End Sub

	Private Sub OnPublishFileResult(ByVal pCallResult As RemoteStoragePublishFileResult_t, ByVal bIOFailure As Boolean)
		Try
			Console.WriteLine("OnPublishFileResult")
			If pCallResult.m_eResult = EResult.k_EResultOK Then
				If pCallResult.m_bUserNeedsToAcceptWorkshopLegalAgreement Then
					sw.WriteLine("success_agreement")
				Else
					sw.WriteLine("success")
				End If
				Console.WriteLine("OnPublishFileResult ItemID: " + pCallResult.m_nPublishedFileId.ToString())
				sw.WriteLine(pCallResult.m_nPublishedFileId.ToString())
			Else
				sw.WriteLine(GetErrorMessage(pCallResult.m_eResult))
			End If
		Catch ex As Exception
			Console.WriteLine("EXCEPTION: " + ex.Message)
			sw.WriteLine("EXCEPTION: " + ex.Message)
		End Try

		theCallResultIsFinished = True
	End Sub

#End Region

#Region "SteamRemoteStorage_UGCDownload"

	'Private Sub SteamRemoteStorage_UGCDownload()
	'	'Dim fileID As PublishedFileId_t
	'	'Dim fileID_Text As String
	'	'fileID_Text = sr.ReadLine()
	'	'fileID.m_PublishedFileId = ULong.Parse(fileID_Text)

	'	'theCallResultIsFinished = False
	'	'GetPublishedFileDetailsCallResult = CallResult(Of RemoteStorageGetPublishedFileDetailsResult_t).Create(AddressOf OnGetPublishedFileDetailsForInternalResults)
	'	'Dim result As SteamAPICall_t
	'	'result = SteamRemoteStorage.GetPublishedFileDetails(fileID, 0)
	'	'GetPublishedFileDetailsCallResult.Set(result)

	'	'While Not theCallResultIsFinished
	'	'	SteamAPI.RunCallbacks()
	'	'End While

	'	theCallResultIsFinished = False
	'	DownloadUGCCallResult = CallResult(Of RemoteStorageDownloadUGCResult_t).Create(AddressOf OnDownloadUGC)
	'	Dim result As SteamAPICall_t
	'	result = SteamRemoteStorage.UGCDownload(theUGCHandleForPreviewImageFile, 0)
	'	DownloadUGCCallResult.Set(result)

	'	While Not theCallResultIsFinished
	'		SteamAPI.RunCallbacks()
	'	End While
	'End Sub

	'Private Sub OnDownloadUGC(ByVal pCallResult As RemoteStorageDownloadUGCResult_t, ByVal bIOFailure As Boolean)
	'	If pCallResult.m_eResult = EResult.k_EResultOK Then
	'		sw.WriteLine("success")
	'		Console.WriteLine("OnDownloadUGC - file name: " + pCallResult.m_pchFileName)
	'		Console.WriteLine("OnDownloadUGC - byte size: " + pCallResult.m_nSizeInBytes.ToString())


	'		Dim data As Byte() = New Byte(pCallResult.m_nSizeInBytes - 1) {}
	'		'data(0) = 1
	'		'data(1) = 2
	'		'data(2) = 3
	'		Dim byteCountRead As Integer = SteamRemoteStorage.UGCRead(theUGCHandleForPreviewImageFile, data, pCallResult.m_nSizeInBytes, 0, EUGCReadAction.k_EUGCRead_ContinueReadingUntilFinished)

	'		'Console.WriteLine("OnDownloadUGC UGCRead - size: " + pCallResult.m_nSizeInBytes.ToString())
	'		'Console.WriteLine("OnDownloadUGC UGCRead - byteCountRead: " + byteCountRead.ToString())
	'		'Console.WriteLine("OnDownloadUGC UGCRead - data size: " + data.Length.ToString())
	'		'Console.WriteLine("OnDownloadUGC UGCRead - data: " + data.ToString())
	'		'Console.WriteLine(data(0))
	'		'Console.WriteLine(data(1))
	'		'Console.WriteLine(data(2))

	'		sw.WriteLine(pCallResult.m_pchFileName)
	'		sw.WriteLine(data.Length.ToString())
	'		sw.BaseStream.Write(data, 0, data.Length)
	'	Else
	'		Console.WriteLine("OnDownloadUGC ERROR: " + pCallResult.m_eResult.ToString())
	'		sw.WriteLine("error")
	'	End If

	'	theCallResultIsFinished = True
	'End Sub

#End Region

#Region "SteamRemoteStorage_UGCDownloadToLocation"

	'Private Sub SteamRemoteStorage_UGCDownloadToLocation()
	'	Dim fileID As PublishedFileId_t
	'	Dim fileID_Text As String
	'	fileID_Text = sr.ReadLine()
	'	fileID.m_PublishedFileId = ULong.Parse(fileID_Text)
	'	Dim fileName As String
	'	fileName = sr.ReadLine()

	'	theCallResultIsFinished = False
	'	GetPublishedFileDetailsCallResult = CallResult(Of RemoteStorageGetPublishedFileDetailsResult_t).Create(AddressOf OnGetPublishedFileDetailsForInternalResults)
	'	Dim result As SteamAPICall_t
	'	result = SteamRemoteStorage.GetPublishedFileDetails(fileID, 0)
	'	GetPublishedFileDetailsCallResult.Set(result)

	'	While Not theCallResultIsFinished
	'		SteamAPI.RunCallbacks()
	'	End While

	'	theCallResultIsFinished = False
	'	DownloadUGCCallResult = CallResult(Of RemoteStorageDownloadUGCResult_t).Create(AddressOf OnDownloadUGC)
	'	result = SteamRemoteStorage.UGCDownloadToLocation(theUGCHandleForPreviewImageFile, fileName, 0)
	'	DownloadUGCCallResult.Set(result)

	'	While Not theCallResultIsFinished
	'		SteamAPI.RunCallbacks()
	'	End While
	'End Sub

#End Region

#Region "SteamRemoteStorage_UGCRead"

	'Private Sub SteamRemoteStorage_UGCRead()
	'	Dim fileID As PublishedFileId_t
	'	Dim fileID_Text As String
	'	fileID_Text = sr.ReadLine()
	'	fileID.m_PublishedFileId = ULong.Parse(fileID_Text)

	'	theCallResultIsFinished = False
	'	GetPublishedFileDetailsCallResult = CallResult(Of RemoteStorageGetPublishedFileDetailsResult_t).Create(AddressOf OnGetPublishedFileDetailsForInternalResults)
	'	Dim result As SteamAPICall_t
	'	result = SteamRemoteStorage.GetPublishedFileDetails(fileID, 0)
	'	GetPublishedFileDetailsCallResult.Set(result)

	'	While Not theCallResultIsFinished
	'		SteamAPI.RunCallbacks()
	'	End While

	'	Dim data As Byte() = New Byte(theUGCPreviewImageFileSize - 1) {}
	'	Dim byteCountRead As Integer = SteamRemoteStorage.UGCRead(theUGCHandleForPreviewImageFile, data, theUGCPreviewImageFileSize, 0, EUGCReadAction.k_EUGCRead_ContinueReadingUntilFinished)
	'	sw.WriteLine(data.Length.ToString())
	'	sw.BaseStream.Write(data, 0, data.Length)
	'End Sub

#End Region

#Region "SteamRemoteStorage_UpdatePublishedFileFile"

	Private Sub SteamRemoteStorage_UpdatePublishedFileFile()
		Dim targetFileName As String
		targetFileName = sr.ReadLine()

		Dim setItemContentWasSuccessful As Boolean = SteamRemoteStorage.UpdatePublishedFileFile(thePublishedFileUpdateHandle, targetFileName)
		If setItemContentWasSuccessful Then
			sw.WriteLine("success")
		Else
			sw.WriteLine("error")
		End If
	End Sub

#End Region

#Region "SteamRemoteStorage_UpdatePublishedFileSetChangeDescription"

	Private Sub SteamRemoteStorage_UpdatePublishedFileSetChangeDescription()
		Dim changeNote As String
		changeNote = ReadMultipleLinesOfText(sr)

		Dim setItemContentWasSuccessful As Boolean = SteamRemoteStorage.UpdatePublishedFileSetChangeDescription(thePublishedFileUpdateHandle, changeNote)
		If setItemContentWasSuccessful Then
			sw.WriteLine("success")
		Else
			sw.WriteLine("error")
		End If
	End Sub

#End Region

#End Region

#Region "SteamUGC"

#Region "SteamUGC_DeleteItem"

	Private Sub SteamUGC_DeleteItem()
		Dim itemID_text As String
		itemID_text = sr.ReadLine()
		Dim itemID As PublishedFileId_t
		itemID.m_PublishedFileId = ULong.Parse(itemID_text)

		Dim result As SteamAPICall_t = SteamUGC.DeleteItem(itemID)
		CrowbarSteamPipe.SetResultAndRunCallbacks(Of DeleteItemResult_t)(AddressOf OnDeleteItem, result)
	End Sub

	Private Sub OnDeleteItem(ByVal pCallResult As DeleteItemResult_t, ByVal bIOFailure As Boolean)
		Try
			If pCallResult.m_eResult = EResult.k_EResultOK Then
				Console.WriteLine("OnDeleteItem - success")
				sw.WriteLine("success")
			Else
				Console.WriteLine("OnDeleteItem ERROR: " + pCallResult.m_eResult.ToString())
				sw.WriteLine(GetErrorMessage(pCallResult.m_eResult))
			End If
		Catch ex As Exception
			Console.WriteLine("EXCEPTION: " + ex.Message)
			sw.WriteLine("EXCEPTION: " + ex.Message)
		End Try

		theCallResultIsFinished = True
	End Sub

#End Region

#Region "SteamUGC_DownloadItem"

	''DownloadItem
	''bool DownloadItem( PublishedFileId_t nPublishedFileID, bool bHighPriority );
	''Name	Type	Description
	''nPublishedFileID	PublishedFileId_t	The workshop item to download.
	''bHighPriority	bool	Start the download in high priority mode, pausing any existing in-progress Steam downloads and immediately begin downloading this workshop item.
	''
	''Download or update a workshop item.
	''
	''If the return value is true then register and wait for the Callback DownloadItemResult_t before calling GetItemInstallInfo or accessing the workshop item on disk.
	''
	''If the user is not subscribed to the item (e.g. a Game Server using anonymous login), the workshop item will be downloaded and cached temporarily.
	''
	''If the workshop item has an item state of k_EItemStateNeedsUpdate, then this function can be called to initiate the update. Do not access the workshop item on disk until the Callback DownloadItemResult_t is called.
	''
	''NOTE: This method only works with workshop items created with ISteamUGC. It will not work with the legacy ISteamRemoteStorage workshop items.
	''
	''The DownloadItemResult_t callback contains the app ID associated with the workshop item. It should be compared against the running app ID as the handler will be called for all item downloads regardless of the running application.
	''
	''Returns: bool
	''Triggers a DownloadItemResult_t callback.
	''true if the download was successfully started; otherwise, false if nPublishedFileID is invalid or the user is not logged on.
	'Private Sub SteamUGC_DownloadItem()
	'	Dim appID_Text As String
	'	appID_Text = sr.ReadLine()
	'	theAppID.m_AppId = UInteger.Parse(appID_Text)
	'	Dim fileID_Text As String
	'	fileID_Text = sr.ReadLine()
	'	Dim fileID As PublishedFileId_t
	'	fileID.m_PublishedFileId = ULong.Parse(fileID_Text)

	'	Dim resultIsSuccess As Boolean = SteamUGC.DownloadItem(fileID, True)

	'	If resultIsSuccess Then
	'		theCallResultIsFinished = False
	'		DownloadItemCallback = Callback(Of DownloadItemResult_t).Create(AddressOf OnDownloadItem)

	'		While Not theCallResultIsFinished
	'			SteamAPI.RunCallbacks()
	'		End While
	'	End If
	'End Sub

	'Private Sub OnDownloadItem(ByVal pCallback As DownloadItemResult_t)
	'	Try
	'		If pCallback.m_unAppID = theAppID Then
	'			If pCallback.m_eResult = EResult.k_EResultOK Then
	'				Console.WriteLine("OnDownloadItem - success")

	'				theResultMessage = "success"
	'			Else
	'				Console.WriteLine("OnDownloadItem ERROR: " + pCallback.m_eResult.ToString())
	'				theResultMessage = pCallback.m_eResult.ToString()
	'			End If
	'		End If
	'	Catch ex As Exception
	'		Console.WriteLine("OnDownloadItem EXCEPTION: " + ex.Message)
	'		theResultMessage = "OnDownloadItem EXCEPTION: " + ex.Message
	'	End Try

	'	sw.WriteLine(theResultMessage)

	'	theCallResultIsFinished = True
	'End Sub

	'Private DownloadItemCallback As Callback(Of DownloadItemResult_t)

#End Region

#Region "QueryViaSteamUGC"

#Region "SteamUGC_CreateQueryUGCDetailsRequest"

	Private Sub SteamUGC_CreateQueryUGCDetailsRequest()
		Dim itemID_Text As String
		itemID_Text = sr.ReadLine()
		Dim itemID As PublishedFileId_t
		itemID.m_PublishedFileId = ULong.Parse(itemID_Text)
		Dim publishedFileIDList() As PublishedFileId_t = {itemID}

		theUGCQueryHandle = SteamUGC.CreateQueryUGCDetailsRequest(publishedFileIDList, 1)
		If theUGCQueryHandle <> UGCQueryHandle_t.Invalid Then
			sw.WriteLine("success")
		Else
			sw.WriteLine("error")
		End If
	End Sub

#End Region

#Region "SteamUGC_CreateQueryUserUGCRequest"

	Private Sub SteamUGC_CreateQueryUserUGCRequest()
		Dim appID_Text As String
		appID_Text = sr.ReadLine()
		Dim appID As AppId_t
		appID.m_AppId = UInteger.Parse(appID_Text)
		Dim pageNumberText As String
		pageNumberText = sr.ReadLine()
		Dim pageNumber As UInteger
		pageNumber = UInteger.Parse(pageNumberText)

		'#If DEBUG Then
		'		'NOTE: Use account that has over 750 items in L4D2 workshop.
		'		Dim steamID As CSteamID = New CSteamID(76561198006306928)
		'#Else
		Dim steamID As CSteamID = SteamUser.GetSteamID()
		'#End If

		Dim accountID As AccountID_t = steamID.GetAccountID()
		Console.WriteLine("steamID: " + steamID.ToString())
		Console.WriteLine("accountID: " + accountID.ToString())
		Console.WriteLine("appID: " + appID.m_AppId.ToString())

		'NOTE: Use this invalid AppID to get all addons for the game instead of just ones created by a separate tool or just ones by the game.
		Dim nullCreatorAppID As New AppId_t(0)

		Console.WriteLine("SteamUGC_CreateQueryUserUGCRequest - pageNumber: " + pageNumber.ToString())
		theUGCQueryHandle = SteamUGC.CreateQueryUserUGCRequest(accountID, EUserUGCList.k_EUserUGCList_Published, EUGCMatchingUGCType.k_EUGCMatchingUGCType_Items_ReadyToUse, EUserUGCListSortOrder.k_EUserUGCListSortOrder_LastUpdatedDesc, nullCreatorAppID, appID, pageNumber)

		If theUGCQueryHandle <> UGCQueryHandle_t.Invalid Then
			'Console.WriteLine("SteamUGC_CreateQueryUserUGCRequest - good UGCQueryHandle_t")
			sw.WriteLine("success")

			' Disable getting unwanted fields.
			'ISteamUGC: SetReturnKeyValueTags -Sets whether to return any key-value tags for the items on a pending UGC Query.
			'ISteamUGC: SetReturnLongDescription -Sets whether to return the full description for the items on a pending UGC Query.
			'ISteamUGC: SetReturnMetadata -Sets whether to return the developer specified metadata for the items on a pending UGC Query.
			'ISteamUGC: SetReturnChildren -Sets whether to return the IDs of the child items of the items on a pending UGC Query.
			'ISteamUGC: SetReturnAdditionalPreviews -Sets whether to return any additional images/videos attached to the items on a pending UGC Query.
			'Console.WriteLine("SteamUGC_CreateQueryUserUGCRequest - Disable some fields.")
			SteamUGC.SetReturnKeyValueTags(theUGCQueryHandle, False)
			SteamUGC.SetReturnLongDescription(theUGCQueryHandle, False)
			'SteamUGC.SetReturnLongDescription(theUGCQueryHandle, True)
			SteamUGC.SetReturnMetadata(theUGCQueryHandle, False)
			SteamUGC.SetReturnChildren(theUGCQueryHandle, False)
			SteamUGC.SetReturnAdditionalPreviews(theUGCQueryHandle, False)
		Else
			'Console.WriteLine("SteamUGC_CreateQueryUserUGCRequest - invalid UGCQueryHandle_t")
			sw.WriteLine("error")
		End If
	End Sub

#End Region

#Region "SteamUGC_SendQueryUGCRequest"

	Private Sub SteamUGC_SendQueryUGCRequest()
		Dim result As SteamAPICall_t = SteamUGC.SendQueryUGCRequest(theUGCQueryHandle)
		CrowbarSteamPipe.SetResultAndRunCallbacks(Of SteamUGCQueryCompleted_t)(AddressOf OnSteamUGCQueryCompleted, result)

		Console.WriteLine("SteamUGC_ReleaseQueryUGCRequest")
		SteamUGC.ReleaseQueryUGCRequest(theUGCQueryHandle)
	End Sub

	'NOTE: Will return max of kNumUGCResultsPerPage pages. Steamworks.Constants.kNumUGCResultsPerPage
	'm_eResult   EResult	The result of the operation.
	'm_unNumResultsReturned  UInt32	The number of items returned.
	'm_unTotalMatchingResults    UInt32	The total number of items that matched the query.
	Private Sub OnSteamUGCQueryCompleted(ByVal pCallResult As SteamUGCQueryCompleted_t, ByVal bIOFailure As Boolean)
		Try
			If pCallResult.m_eResult = EResult.k_EResultOK Then
				sw.WriteLine("success")
				sw.WriteLine(pCallResult.m_unNumResultsReturned.ToString())
				If pCallResult.m_unNumResultsReturned > 0 Then
					sw.WriteLine(pCallResult.m_unTotalMatchingResults.ToString())
					Console.WriteLine("OnSteamUGCQueryCompleted (" + pCallResult.m_unNumResultsReturned.ToString() + "):")

					Dim queryResult As Boolean
					Dim itemDetails As New SteamUGCDetails_t()
					For resultItemIndex As UInteger = 0 To CUInt(pCallResult.m_unNumResultsReturned - 1)
						queryResult = SteamUGC.GetQueryUGCResult(theUGCQueryHandle, resultItemIndex, itemDetails)

						''NOTE: Only send items that the user created.
						'Dim steamID As CSteamID = SteamUser.GetSteamID()
						'If steamID.m_SteamID = itemDetails.m_ulSteamIDOwner Then
						Console.WriteLine("OnSteamUGCQueryCompleted ItemID: " + itemDetails.m_nPublishedFileId.ToString())
						sw.WriteLine(itemDetails.m_nPublishedFileId)
						sw.WriteLine(itemDetails.m_ulSteamIDOwner)
						sw.WriteLine(SteamFriends.GetFriendPersonaName(New CSteamID(itemDetails.m_ulSteamIDOwner)))
						sw.WriteLine(itemDetails.m_rtimeCreated)
						sw.WriteLine(itemDetails.m_rtimeUpdated)

						WriteTextThatMightHaveMultipleLines(itemDetails.m_rgchTitle)
						'WriteTextThatMightHaveMultipleLines(itemDetails.m_rgchDescription)

						sw.WriteLine(itemDetails.m_pchFileName)
						sw.WriteLine(itemDetails.m_hPreviewFile)
						sw.WriteLine(CType(itemDetails.m_eVisibility, Steamworks.ERemoteStoragePublishedFileVisibility).ToString("d"))
						sw.WriteLine(itemDetails.m_rgchTags)

						'Console.WriteLine("  " + resultItemIndex.ToString())
						'Console.WriteLine("    OwnerName: " + SteamFriends.GetFriendPersonaName(New CSteamID(itemDetails.m_ulSteamIDOwner)))
						'Console.WriteLine("    Created: " + itemDetails.m_rtimeCreated.ToString())
						'Console.WriteLine("    Updated: " + itemDetails.m_rtimeUpdated.ToString())
						'Console.WriteLine("    Title: " + itemDetails.m_rgchTitle)
						'Console.WriteLine("    Description: " + itemDetails.m_rgchDescription)
						'Console.WriteLine("    " + itemDetails.m_rgchTags)
						'Console.WriteLine("    " + itemDetails.m_rgchURL)
						'Console.WriteLine("    m_hFile: " + itemDetails.m_hFile.ToString())

						'queryResult = SteamUGC.GetQueryUGCPreviewURL(theUGCQueryHandle, resultItemIndex, itemDetails)

						' Test use of meta data. Need to change SteamUGC.SetReturnMetadata() second param from False to True.
						'Dim metaData As String = ""
						'Dim metaDataSize As UInteger
						'queryResult = SteamUGC.GetQueryUGCMetadata(theUGCQueryHandle, resultItemIndex, metaData, metaDataSize)
						'Console.WriteLine("    metaDataSize: " + metaDataSize.ToString() + " END")
						'Console.WriteLine("    metaData(" + metaData.Length().ToString() + "): " + metaData + " END")
					Next
				End If
			Else
				sw.WriteLine(GetErrorMessage(pCallResult.m_eResult))
			End If
		Catch ex As Exception
			Console.WriteLine("EXCEPTION: " + ex.Message)
			sw.WriteLine("EXCEPTION: " + ex.Message)
		End Try

		theCallResultIsFinished = True
	End Sub

#End Region

#End Region

#Region "PublishViaSteamUGC"

#Region "SteamUGC_CreateItem"

	Private Sub SteamUGC_CreateItem()
		Dim appID_Text As String
		appID_Text = sr.ReadLine()
		Dim appID As AppId_t
		appID.m_AppId = UInteger.Parse(appID_Text)

		Dim result As SteamAPICall_t = SteamUGC.CreateItem(appID, EWorkshopFileType.k_EWorkshopFileTypeCommunity)
		CrowbarSteamPipe.SetResultAndRunCallbacks(Of CreateItemResult_t)(AddressOf OnCreateItem, result)
	End Sub

	'm_eResult	EResult	The result of the operation. Some of the possible return values include:
	'    k_EResultOK - The operation completed successfully.
	'    k_EResultInsufficientPrivilege - The user creating the item is currently banned in the community.
	'    k_EResultTimeout - The operation took longer than expected, have the user retry the create process.
	'    k_EResultNotLoggedOn - The user is not currently logged into Steam.
	'm_nPublishedFileId	PublishedFileId_t	The new items unique ID.
	'm_bUserNeedsToAcceptWorkshopLegalAgreement	bool	Does the user need to accept the Steam Workshop legal agreement (true) or not (false)? See the Workshop Legal Agreement for more information.
	Private Sub OnCreateItem(ByVal pCallResult As CreateItemResult_t, ByVal bIOFailure As Boolean)
		Try
			If pCallResult.m_eResult = EResult.k_EResultOK Then
				If pCallResult.m_bUserNeedsToAcceptWorkshopLegalAgreement Then
					sw.WriteLine("success_agreement")
				Else
					sw.WriteLine("success")
				End If
				sw.WriteLine(pCallResult.m_nPublishedFileId.ToString())
			Else
				sw.WriteLine(GetErrorMessage(pCallResult.m_eResult))
			End If
		Catch ex As Exception
			Console.WriteLine("EXCEPTION: " + ex.Message)
			sw.WriteLine("EXCEPTION: " + ex.Message)
		End Try

		theCallResultIsFinished = True
	End Sub

#End Region

	Private Sub SteamUGC_GetItemUpdateProgress()
		Dim uploadedByteCount As ULong = 0
		Dim totalUploadedByteCount As ULong = 0

		Dim status As EItemUpdateStatus = SteamUGC.GetItemUpdateProgress(theUGCUpdateHandle, uploadedByteCount, totalUploadedByteCount)

		'k_EItemUpdateStatusInvalid	0	The item update handle was invalid, the job might be finished, a SubmitItemUpdateResult_t call result should have been returned for it.
		'k_EItemUpdateStatusPreparingConfig	1	The item update is processing configuration data.
		'k_EItemUpdateStatusPreparingContent	2	The item update is reading and processing content files.
		'k_EItemUpdateStatusUploadingContent	3	The item update is uploading content changes to Steam.
		'k_EItemUpdateStatusUploadingPreviewFile	4	The item update is uploading new preview file image.
		'k_EItemUpdateStatusCommittingChanges	5	The item update is committing all changes.
		If status = EItemUpdateStatus.k_EItemUpdateStatusPreparingConfig Then
			'           Console.WriteLine("Preparing config")
			sw.WriteLine("Preparing config")
        ElseIf status = EItemUpdateStatus.k_EItemUpdateStatusPreparingContent Then
			'           Console.WriteLine("Preparing content")
			sw.WriteLine("Preparing content")
        ElseIf status = EItemUpdateStatus.k_EItemUpdateStatusUploadingContent Then
			'           Console.WriteLine("Uploading content")
			sw.WriteLine("Uploading content")
            If totalUploadedByteCount > 0 AndAlso uploadedByteCount = totalUploadedByteCount Then
                theItemIsUploading = False
            End If
        ElseIf status = EItemUpdateStatus.k_EItemUpdateStatusUploadingPreviewFile Then
			'           Console.WriteLine("Uploading preview")
			sw.WriteLine("Uploading preview")
        ElseIf status = EItemUpdateStatus.k_EItemUpdateStatusCommittingChanges Then
			'           Console.WriteLine("Committing changes")
			sw.WriteLine("Committing changes")
        Else
			'           Console.WriteLine("invalid")
			sw.WriteLine("invalid")
            theItemIsUploading = False
        End If
		sw.WriteLine(uploadedByteCount)
		sw.WriteLine(totalUploadedByteCount)
	End Sub

#Region "SteamUGC_StartItemUpdate"

	Private Sub SteamUGC_StartItemUpdate()
		Dim appID_Text As String
		appID_Text = sr.ReadLine()
		Dim appID As AppId_t
		appID.m_AppId = UInteger.Parse(appID_Text)
		Dim itemID_Text As String
		itemID_Text = sr.ReadLine()
		Dim itemID As PublishedFileId_t
		itemID.m_PublishedFileId = ULong.Parse(itemID_Text)

		theUGCUpdateHandle = SteamUGC.StartItemUpdate(appID, itemID)

		If theUGCUpdateHandle <> UGCUpdateHandle_t.Invalid Then
			sw.WriteLine("success")
		Else
			sw.WriteLine("error")
		End If
	End Sub

#End Region

#Region "SteamUGC_SetItem Details"

	Private Sub SteamUGC_SetItemUpdateLanguage()
		Dim language As String
		language = sr.ReadLine()

		Dim resultIsSuccess As Boolean = SteamUGC.SetItemUpdateLanguage(theUGCUpdateHandle, language)

		If resultIsSuccess Then
			sw.WriteLine("success")
		Else
			sw.WriteLine("error")
		End If
	End Sub

	Private Sub SteamUGC_SetItemTitle()
		Dim titleText As String
		titleText = ReadMultipleLinesOfText(sr)

		Dim resultIsSuccess As Boolean = SteamUGC.SetItemTitle(theUGCUpdateHandle, titleText)

		If resultIsSuccess Then
			sw.WriteLine("success")
		Else
			sw.WriteLine("error")
		End If
	End Sub

	Private Sub SteamUGC_SetItemDescription()
		Dim descriptionText As String
		descriptionText = ReadMultipleLinesOfText(sr)

		Dim resultIsSuccess As Boolean = SteamUGC.SetItemDescription(theUGCUpdateHandle, descriptionText)

		If resultIsSuccess Then
			sw.WriteLine("success")
		Else
			sw.WriteLine("error")
		End If
	End Sub

	Private Sub SteamUGC_SetItemContent()
		Dim contentPathFileName As String
		contentPathFileName = sr.ReadLine()

		Dim resultIsSuccess As Boolean = SteamUGC.SetItemContent(theUGCUpdateHandle, contentPathFileName)

		If resultIsSuccess Then
			sw.WriteLine("success")
		Else
			sw.WriteLine("error")
		End If
	End Sub

	Private Sub SteamUGC_SetItemPreview()
		Dim previewPathFileName As String
		previewPathFileName = sr.ReadLine()

		Dim resultIsSuccess As Boolean = SteamUGC.SetItemPreview(theUGCUpdateHandle, previewPathFileName)

		If resultIsSuccess Then
			sw.WriteLine("success")
		Else
			sw.WriteLine("error")
		End If
	End Sub

	Private Sub SteamUGC_SetItemVisibility()
		Dim visibility_text As String
		visibility_text = sr.ReadLine()
		Dim visibility As ERemoteStoragePublishedFileVisibility
		'visibility = CType([Enum].Parse(GetType(ERemoteStoragePublishedFileVisibility), visibility_text), ERemoteStoragePublishedFileVisibility)
		visibility = ConvertVisibilityFromCrowbarToSteamworks(visibility_text)

		Dim resultIsSuccess As Boolean = SteamUGC.SetItemVisibility(theUGCUpdateHandle, visibility)

		If resultIsSuccess Then
			sw.WriteLine("success")
		Else
			sw.WriteLine("error")
		End If
	End Sub

	Private Sub SteamUGC_SetItemTags()
		Dim tagCountText As String
		tagCountText = sr.ReadLine()
		Dim tagCount As Integer
		tagCount = Integer.Parse(tagCountText)
		Dim tags As New List(Of String)(tagCount)
		Dim tag As String
		For i As Integer = 0 To tagCount - 1
			tag = sr.ReadLine()
			tags.Add(tag)
		Next

		Dim resultIsSuccess As Boolean = SteamUGC.SetItemTags(theUGCUpdateHandle, tags)

		If resultIsSuccess Then
			sw.WriteLine("success")
		Else
			sw.WriteLine("error")
		End If
	End Sub

#End Region

#Region "SteamUGC_SubmitItemUpdate"

	Private Sub SteamUGC_SubmitItemUpdate()
		Dim changeNote As String
		changeNote = ReadMultipleLinesOfText(sr)

		theItemIsUploading = True

		Dim result As SteamAPICall_t = SteamUGC.SubmitItemUpdate(theUGCUpdateHandle, changeNote)
		CrowbarSteamPipe.SetResultAndRunCallbacks(Of SubmitItemUpdateResult_t)(AddressOf OnSubmitItemUpdate, result)
	End Sub

	Private Sub OnSubmitItemUpdate(ByVal pCallResult As SubmitItemUpdateResult_t, ByVal bIOFailure As Boolean)
		theItemIsUploading = False
		'Console.WriteLine("OnSubmitItemUpdate")
		sw.WriteLine("OnSubmitItemUpdate")
		Try
			If pCallResult.m_eResult = EResult.k_EResultOK Then
				If pCallResult.m_bUserNeedsToAcceptWorkshopLegalAgreement Then
					sw.WriteLine("success_agreement")
				Else
					'Console.WriteLine("OnSubmitItemUpdate success")
					sw.WriteLine("success")
				End If
			Else
				'Console.WriteLine("OnSubmitItemUpdate " + GetErrorMessage(pCallResult.m_eResult))
				sw.WriteLine(GetErrorMessage(pCallResult.m_eResult))
			End If
			'Console.WriteLine("OnSubmitItemUpdate result: " + pCallResult.m_eResult.ToString())
		Catch ex As Exception
			'Console.WriteLine("OnSubmitItemUpdate EXCEPTION: " + ex.Message)
			sw.WriteLine("EXCEPTION: " + ex.Message)
		End Try

		theCallResultIsFinished = True
	End Sub

#End Region

#Region "SteamUGC_UnsubscribeItem"

	Private Sub SteamUGC_UnsubscribeItem()
		Dim itemID_Text As String
		itemID_Text = sr.ReadLine()
		Dim itemID As PublishedFileId_t
		itemID.m_PublishedFileId = ULong.Parse(itemID_Text)

		Dim result As SteamAPICall_t = SteamUGC.UnsubscribeItem(itemID)
		CrowbarSteamPipe.SetResultAndRunCallbacks(Of RemoteStorageUnsubscribePublishedFileResult_t)(AddressOf OnUnsubscribeItem, result)
	End Sub

	Private Sub OnUnsubscribeItem(ByVal pCallResult As RemoteStorageUnsubscribePublishedFileResult_t, ByVal bIOFailure As Boolean)
		Try
			If pCallResult.m_eResult = EResult.k_EResultOK Then
				sw.WriteLine("success")
			Else
				sw.WriteLine(GetErrorMessage(pCallResult.m_eResult))
			End If
			Console.WriteLine("OnUnsubscribeItem result: " + pCallResult.m_eResult.ToString())
		Catch ex As Exception
			Console.WriteLine("OnUnsubscribeItem EXCEPTION: " + ex.Message)
			sw.WriteLine("EXCEPTION: " + ex.Message)
		End Try

		theCallResultIsFinished = True
	End Sub

#End Region

#End Region

#End Region

#Region "SteamUser"

	Private Sub SteamUser_GetSteamID()
		Dim steamID As CSteamID = SteamUser.GetSteamID()
		sw.WriteLine(steamID.ToString())
	End Sub

#End Region

#Region "Private Functions"

	Private Function ConvertVisibilityFromCrowbarToSteamworks(ByVal input As String) As Steamworks.ERemoteStoragePublishedFileVisibility
		Dim output As Steamworks.ERemoteStoragePublishedFileVisibility
		If input = "Public" Then
			output = Steamworks.ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic
		ElseIf input = "FriendsOnly" Then
			output = Steamworks.ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityFriendsOnly
		ElseIf input = "Unlisted" Then
			output = Steamworks.ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityUnlisted
		Else
			output = Steamworks.ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPrivate
		End If
		Return output
	End Function

	Private Sub SetResultAndRunCallbacks(Of T)(ByVal func As CallResult(Of T).APIDispatchDelegate, ByVal result As SteamAPICall_t)
		Dim aCallResult As CallResult(Of T) = CallResult(Of T).Create(func)
		aCallResult.Set(result)
		CrowbarSteamPipe.RunCallbacks()
	End Sub

	Private Sub RunCallbacks()
		theCallResultIsFinished = False
		While Not theCallResultIsFinished
			If theItemIsUploading Then
				SteamUGC_GetItemUpdateProgress()
			End If
			SteamAPI.RunCallbacks()
		End While
	End Sub

	'NOTE: WriteLine only writes string until first LF or CR, so need to adjust how to send this.
	'NOTE: From TextReader.ReadLine: A line is defined as a sequence of characters followed by 
	'      a carriage return (0x000d), a line feed (0x000a), a carriage return followed by a line feed, Environment.NewLine, or the end-of-stream marker.
	'      https://docs.microsoft.com/en-us/dotnet/api/system.io.textreader.readline?view=netframework-4.0
	Private Sub WriteTextThatMightHaveMultipleLines(ByVal text As String)
		text = ConvertText(text)

		'NOTE: Delete all CR in text because they are not needed and will show as blank characters in Windows TextBox.
		text = text.Replace(vbCr, "")
		Dim stringSeparators() As String = {vbLf}
		Dim textLines_array As String() = text.Split(stringSeparators, StringSplitOptions.None)
		Dim textLines As New List(Of String)(textLines_array)
		'NOTE: Delete last line if empty because it is always added to what user actually published.
		If textLines(textLines.Count - 1) = "" Then
			textLines.RemoveAt(textLines.Count - 1)
		End If
		sw.WriteLine(textLines.Count)
		'Console.WriteLine("    Line count: " + textLines.Count.ToString())
		Dim i As Integer = 1
		For Each line As String In textLines
			sw.WriteLine(line)
			'Console.WriteLine("    Line " + i.ToString() + ": " + line)
			i += 1
		Next
	End Sub

	Public Function ConvertText(ByVal strData As String) As String
		Dim bytes() As Byte
		bytes = Text.Encoding.Default.GetBytes(strData)
		Return Text.Encoding.UTF8.GetString(bytes)
	End Function

	Private Function ReadMultipleLinesOfText(ByVal sr As StreamReader) As String
		Dim text As String = ""

		Dim textLineCount As Integer
		textLineCount = CInt(sr.ReadLine())
		For i As Integer = 0 To textLineCount - 1
			'NOTE: Add LF because web page needs them for newlines.
			text += sr.ReadLine() + vbLf
		Next
		'Console.WriteLine("ReadMultipleLinesOfText: " + text)

		Return text
	End Function

	'k_EResultOK -The operation completed successfully.
	'k_EResultFail -Generic failure.
	'k_EResultInvalidParam -Either the provided app ID Is invalid Or doesn't match the consumer app ID of the item or, you have not enabled ISteamUGC for the provided app ID on the Steam Workshop Configuration App Admin page.
	'    The preview file Is smaller than 16 bytes.
	'k_EResultAccessDenied -The user doesn't own a license for the provided app ID.
	'k_EResultFileNotFound -Failed to get the workshop info for the item Or failed to read the preview file.
	'k_EResultLockingFailed -Failed to aquire UGC Lock.
	'k_EResultFileNotFound -The provided content folder Is Not valid.
	'k_EResultLimitExceeded -The preview image Is too large, it must be less than 1 Megabyte; Or there Is Not enough space available on the users Steam Cloud.
	Private Function GetErrorMessage(ByVal steamErrorResult As EResult) As String
		Dim errorMessage As String = "ERROR: "

		If steamErrorResult = EResult.k_EResultAccessDenied Then
			'sw.WriteLine("Access denied. The AppID in the steam_api.txt file is incorrect or the steam_api.txt file is not beside the CrowbarSteamPipe.exe file.")
			errorMessage += "Access denied. The user's Steam account does not own a license for the provided App ID."
		ElseIf steamErrorResult = EResult.k_EResultFileNotFound Then
			'errorMessage += "File not found. The provided content folder, content file, or preview image file is invalid."
			errorMessage += "File not found. The provided content folder, content file, or preview image file is invalid."
		ElseIf steamErrorResult = EResult.k_EResultInsufficientPrivilege Then
			errorMessage += "Insufficient privilege. The user's Steam account is currently restricted from uploading content due to a hub ban, account lock, or community ban."
		ElseIf steamErrorResult = EResult.k_EResultInvalidParam Then
			errorMessage += "Invalid paramater. Content file too big, invalid App ID, or the preview file is smaller than 16 bytes."
		ElseIf steamErrorResult = EResult.k_EResultLimitExceeded Then
			errorMessage += "Limit exceeded. The preview image is too large, it must be less than 1 megabyte; or there is not enough space available on the user's Steam Cloud."
		ElseIf steamErrorResult = EResult.k_EResultLockingFailed Then
			errorMessage += "Locking failed. Failed to aquire UGC Lock."
		ElseIf steamErrorResult = EResult.k_EResultNotLoggedOn Then
			errorMessage += "Not logged on. The user's Steam account is not currently logged in."
		ElseIf steamErrorResult = EResult.k_EResultTimeout Then
			errorMessage += "Timeout. Action timed-out and did not complete."
		Else
			errorMessage += steamErrorResult.ToString()
		End If

		Return errorMessage
	End Function

	Private Function GetItemStateText(ByVal itemState As Steamworks.EItemState) As String
		Dim itemStateText As String = ""

		If itemState = Steamworks.EItemState.k_EItemStateNone Then
			itemStateText += " k_EItemStateNone"
		Else
			If (itemState And Steamworks.EItemState.k_EItemStateDownloading) <> 0 Then
				itemStateText += " k_EItemStateDownloading"
			End If
			If (itemState And Steamworks.EItemState.k_EItemStateDownloadPending) <> 0 Then
				itemStateText += " k_EItemStateDownloadPending"
			End If
			If (itemState And Steamworks.EItemState.k_EItemStateInstalled) <> 0 Then
				itemStateText += " k_EItemStateInstalled"
			End If
			If (itemState And Steamworks.EItemState.k_EItemStateLegacyItem) <> 0 Then
				itemStateText += " k_EItemStateLegacyItem"
			End If
			If (itemState And Steamworks.EItemState.k_EItemStateNeedsUpdate) <> 0 Then
				itemStateText += " k_EItemStateNeedsUpdate"
			End If
			If (itemState And Steamworks.EItemState.k_EItemStateSubscribed) <> 0 Then
				itemStateText += " k_EItemStateSubscribed"
			End If
		End If
		itemStateText = itemStateText.TrimStart()

		Return itemStateText
	End Function

#End Region

#Region "Data"

	Private sw As StreamWriter
	Private sr As StreamReader

	Private theItemIsUploading As Boolean
	Private theCallResultIsFinished As Boolean
	Private theUGCQueryHandle As UGCQueryHandle_t
	Private theUGCUpdateHandle As UGCUpdateHandle_t

	Private thePublishedFileUpdateHandle As PublishedFileUpdateHandle_t
	Private theUGCHandleForContentFile As UGCHandle_t
	Private theUGCHandleForPreviewImageFile As UGCHandle_t
	'Private theUGCPreviewImageFileSize As Integer

	Private theAppID As AppId_t
	Private theItemID As PublishedFileId_t
	Private theItemUpdated As UInteger
	Private theItemTitle As String
	Private theItemContentFileName As String

	Private theResultMessage As String

#End Region

End Module
