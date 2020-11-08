Imports System.ComponentModel
Imports System.IO

Public Class Viewer
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.isDisposed = False

		Me.WorkerReportsProgress = True
		Me.WorkerSupportsCancellation = True
		AddHandler Me.DoWork, AddressOf Me.ModelViewer_DoWork
	End Sub

#Region "IDisposable Support"

	Public Overloads Sub Dispose()
		' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) below.
		Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub

	Protected Overloads Sub Dispose(ByVal disposing As Boolean)
		If Not Me.IsDisposed Then
			'Me.Halt(False)
			If disposing Then
				Me.Free()
			End If
			'NOTE: free shared unmanaged resources
		End If
		Me.IsDisposed = True
		MyBase.Dispose(disposing)
	End Sub

	'Protected Overrides Sub Finalize()
	'	' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
	'	Dispose(False)
	'	MyBase.Finalize()
	'End Sub

#End Region

#End Region

#Region "Init and Free"

	'Private Sub Init()
	'End Sub

	Private Sub Free()
		Me.Halt(False)
		Me.FreeViewModel()
	End Sub

#End Region

#Region "Methods"

	Public Sub Run(ByVal gameSetupSelectedIndex As Integer, ByVal inputMdlPathFileName As String, ByVal viewAsReplacement As Boolean, ByVal viewAsReplacementExtraSubfolder As String)
		Dim info As New ViewerInfo()
		info.viewerAction = ViewerInfo.ViewerActionType.ViewModel
		info.gameSetupSelectedIndex = gameSetupSelectedIndex
		info.mdlPathFileName = inputMdlPathFileName
		info.viewAsReplacement = viewAsReplacement
		info.viewAsReplacementExtraSubfolder = viewAsReplacementExtraSubfolder
		Me.RunWorkerAsync(info)
	End Sub

	Public Sub Run(ByVal inputMdlPathFileName As String, ByVal mdlVersionOverride As SupportedMdlVersion)
		Dim info As New ViewerInfo()
		info.viewerAction = ViewerInfo.ViewerActionType.GetData
		info.mdlPathFileName = inputMdlPathFileName
		info.mdlVersionOverride = mdlVersionOverride
		Me.RunWorkerAsync(info)
	End Sub

	Public Sub Run(ByVal gameSetupSelectedIndex As Integer)
		Dim info As New ViewerInfo()
		info.viewerAction = ViewerInfo.ViewerActionType.OpenViewer
		info.gameSetupSelectedIndex = gameSetupSelectedIndex
		Me.RunWorkerAsync(info)
	End Sub

	'Public Sub Halt()
	'	Me.Halt(False)
	'End Sub

#End Region

#Region "Event Handlers"

	'Private Sub HlmvApp_Exited(ByVal sender As Object, ByVal e As System.EventArgs)
	'	Me.Halt(True)
	'End Sub

#End Region

#Region "Private Methods that can be called in either the main thread or the background thread"

	Private Sub Halt(ByVal calledFromBackgroundThread As Boolean)
		If Me.theHlmvAppProcess IsNot Nothing Then
			'RemoveHandler Me.theHlmvAppProcess.Exited, AddressOf HlmvApp_Exited

			Try
				If Not Me.theHlmvAppProcess.HasExited AndAlso Not Me.theHlmvAppProcess.CloseMainWindow() Then
					Me.theHlmvAppProcess.Kill()
				End If
			Catch ex As Exception
				Dim debug As Integer = 4242
			Finally
				'NOTE: Due to threading, Me.theHlmvAppProcess might be Nothing at this point.
				If Me.theHlmvAppProcess IsNot Nothing Then
					Me.theHlmvAppProcess.Close()
					'NOTE: This raises an exception when the background thread has already completed its work.
					'If calledFromBackgroundThread Then
					'	Me.UpdateProgressStop("Model viewer closed.")
					'End If
					Me.theHlmvAppProcess = Nothing
				End If
			End Try
		End If
	End Sub

#End Region

#Region "Private Methods that are called in the background thread"

	Private Sub ModelViewer_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Me.ReportProgress(0, "")

		Dim info As ViewerInfo

		info = CType(e.Argument, ViewerInfo)
		Me.theInputMdlPathFileName = info.mdlPathFileName
		Me.theViewAsReplacementExtraSubfolder = info.viewAsReplacementExtraSubfolder
		Me.theInputMdlIsViewedAsReplacement = info.viewAsReplacement
		Me.theGameSetup = TheApp.Settings.GameSetups(info.gameSetupSelectedIndex)

		If ViewerInputsAreOkay(info.viewerAction) Then
			If info.viewerAction = ViewerInfo.ViewerActionType.GetData Then
				Me.ViewData(info.mdlVersionOverride)
			ElseIf info.viewerAction = ViewerInfo.ViewerActionType.ViewModel Then
				'Me.UpdateProgress(1, "Model viewer opening ...")
				Me.UpdateProgress(1, "Model viewer opened.")
				Me.ViewModel()
				'Me.UpdateProgress(1, "Model viewer opened.")
			ElseIf info.viewerAction = ViewerInfo.ViewerActionType.OpenViewer Then
				'Me.UpdateProgress(1, "Model viewer opening ...")
				Me.UpdateProgress(1, "Model viewer opened.")
				Me.OpenViewer()
				'Me.UpdateProgress(1, "Model viewer opened.")
			End If
		End If
	End Sub

	'TODO: Check inputs as done in Compiler.CompilerInputsAreValid().
	Private Function ViewerInputsAreOkay(ByVal viewerAction As ViewerInfo.ViewerActionType) As Boolean
		Dim inputsAreValid As Boolean

		inputsAreValid = True

		If viewerAction = ViewerInfo.ViewerActionType.GetData OrElse viewerAction = ViewerInfo.ViewerActionType.ViewModel Then
			If String.IsNullOrEmpty(Me.theInputMdlPathFileName) Then
				Me.UpdateProgressStart("")
				Me.WriteErrorMessage("MDL file is blank.")
				inputsAreValid = False
			ElseIf Not File.Exists(Me.theInputMdlPathFileName) Then
				Me.UpdateProgressStart("")
				Me.WriteErrorMessage("MDL file does not exist.")
				inputsAreValid = False
			End If
		End If

		If viewerAction = ViewerInfo.ViewerActionType.ViewModel OrElse viewerAction = ViewerInfo.ViewerActionType.OpenViewer Then
			Dim gamePath As String
			Dim modelViewerPathFileName As String
			gamePath = FileManager.GetPath(Me.theGameSetup.GamePathFileName)
			'modelViewerPathFileName = Path.Combine(FileManager.GetPath(Me.theGameSetup.CompilerPathFileName), "hlmv.exe")
			modelViewerPathFileName = Me.theGameSetup.ViewerPathFileName

			If Not File.Exists(modelViewerPathFileName) Then
				inputsAreValid = False
				Me.WriteErrorMessage("The model viewer, """ + modelViewerPathFileName + """, does not exist.")
				Me.UpdateProgress(1, My.Resources.ErrorMessageSDKMissingCause)
			End If
		End If

		If viewerAction = ViewerInfo.ViewerActionType.OpenViewer Then
		End If

		Return inputsAreValid
	End Function

	Private Sub ViewData(ByVal mdlVersionOverride As SupportedMdlVersion)
		Dim progressDescriptionText As String
		progressDescriptionText = "Getting model data for "
		progressDescriptionText += """" + Path.GetFileName(Me.theInputMdlPathFileName) + """"

		Me.UpdateProgressStart(progressDescriptionText + " ...")

		Me.ShowDataFromMdlFile(mdlVersionOverride)

		Me.UpdateProgressStop("... " + progressDescriptionText + " finished.")
	End Sub

	Private Sub ShowDataFromMdlFile(ByVal mdlVersionOverride As SupportedMdlVersion)
		Dim model As SourceModel = Nothing
		Dim version As Integer
		Try
			If File.Exists(Me.theInputMdlPathFileName) Then
				model = SourceModel.Create(Me.theInputMdlPathFileName, mdlVersionOverride, version)
				If model IsNot Nothing Then
					Dim textLines As List(Of String)
					textLines = model.GetOverviewTextLines(Me.theInputMdlPathFileName, mdlVersionOverride)
					Me.UpdateProgress()
					For Each aTextLine As String In textLines
						Me.UpdateProgress(1, aTextLine)
					Next
				Else
					Me.UpdateProgress(1, "ERROR: Model version not currently supported: " + CStr(version))
					Me.UpdateProgress(1, "       Try changing 'Override MDL version' option.")
				End If
			Else
				Me.UpdateProgress(1, "ERROR: Model file not found: " + """" + Me.theInputMdlPathFileName + """")
			End If
		Catch ex As Exception
			Me.WriteErrorMessage(ex.Message)
		End Try
	End Sub

	Private Sub ViewModel()
		Me.InitViewModel()
		Me.RunHlmvApp(True)
		Me.FreeViewModel()
	End Sub

	Private Sub OpenViewer()
		Me.RunHlmvApp(False)
	End Sub

	Private Sub RunHlmvApp(ByVal viewerIsOpeningModel As Boolean)
		Dim modelViewerPathFileName As String
		Dim gamePath As String
		Dim tempGamePath As String
		Dim gameFileName As String
		Dim gameModelsPath As String
		Dim currentFolder As String = ""

		If Me.theInputMdlIsViewedAsReplacement Then
			tempGamePath = Me.GetTempGamePath()
		Else
			tempGamePath = FileManager.GetPath(Me.theGameSetup.GamePathFileName)
		End If
		gameFileName = Path.GetFileName(Me.theGameSetup.GamePathFileName)
		'modelViewerPathFileName = Path.Combine(FileManager.GetPath(Me.theGameSetup.CompilerPathFileName), "hlmv.exe")
		modelViewerPathFileName = Me.theGameSetup.ViewerPathFileName
		gameModelsPath = Path.Combine(tempGamePath, "models")

		'TODO: Counter-Strike: Source and Portal (and maybe others) do not have a "models" folder.
		'      Can Crowbar avoid SetCurrentDirectory() in these cases?

		If Directory.Exists(gameModelsPath) Then
			currentFolder = Directory.GetCurrentDirectory()
			Directory.SetCurrentDirectory(gameModelsPath)
		End If

		Dim arguments As String = ""
		If gameFileName.ToLower() = "gameinfo.txt" Then
			gamePath = FileManager.GetPath(Me.theGameSetup.GamePathFileName)
			'NOTE: The -olddialogs param adds "(Steam) Load Model" menu item, which usually means HLMV can then open a model from anywhere in file system via the "Load Model" menu item.
			'      This also allows some HLMVs to open MDL v49 via the View button.
			arguments += " -olddialogs -game """
			arguments += gamePath
			arguments += """"
		End If
		If viewerIsOpeningModel Then
			arguments += " """
			If Me.theInputMdlIsViewedAsReplacement Then
				arguments += Me.theInputMdlRelativePathName
			Else
				arguments += Me.theInputMdlPathFileName
			End If
			arguments += """"
		End If

		Me.theHlmvAppProcess = New Process()
		Dim hlmvAppProcessStartInfo As New ProcessStartInfo(modelViewerPathFileName, arguments)
		hlmvAppProcessStartInfo.CreateNoWindow = True
		hlmvAppProcessStartInfo.RedirectStandardError = True
		hlmvAppProcessStartInfo.RedirectStandardOutput = True
		hlmvAppProcessStartInfo.UseShellExecute = False
		'NOTE: Instead of using asynchronous running, use synchronous and wait for process to exit, 
		'      so this background thread won't complete until model viewer is closed.
		'      This allows background thread to announce to main thread when model viewer process exits.
		Me.theHlmvAppProcess.EnableRaisingEvents = False
		Me.theHlmvAppProcess.StartInfo = hlmvAppProcessStartInfo

		Me.theHlmvAppProcess.Start()
		Me.theHlmvAppProcess.WaitForExit()
		Me.Halt(True)

		'TODO: Test if this code works if placed immediately after starting process, to prevent a second view from setting current folder to what the first view was using as a temp current folder.
		If currentFolder <> "" Then
			Directory.SetCurrentDirectory(currentFolder)
		End If
	End Sub

	Private Sub InitViewModel()
		If Me.theGameSetup.GameEngine = GameEngine.Source Then
			Dim gamePath As String
			Dim gameModelsPath As String

			gamePath = FileManager.GetPath(Me.theGameSetup.GamePathFileName)
			gameModelsPath = Path.Combine(gamePath, "models")

			If Not Me.theInputMdlPathFileName.StartsWith(gameModelsPath) Then
				'NOTE: Avoid any changes and copying if user used the "View" button.
				If Me.theInputMdlIsViewedAsReplacement Then
					Me.ModifyGameInfoFile()

					Me.theInputMdlRelativePathName = Me.CreateReplacementModelFiles()
					If String.IsNullOrEmpty(Me.theInputMdlRelativePathName) Then
						Exit Sub
					End If

					'TODO: Uncomment this after it only copies the files used by the model.
					'Me.CopyMaterialAndTextureFiles()
				End If
			End If
		End If
	End Sub

	Private Sub FreeViewModel()
		If Me.theGameSetup.GameEngine = GameEngine.Source Then
			Me.RevertGameInfoFile()

			If Me.theInputMdlIsViewedAsReplacement Then
				Me.DeleteReplacementModelFiles()
			End If

			'TODO: Uncomment this after CopyMaterialAndTextureFiles() has been redone.
			'Me.DeleteMaterialAndTextureFiles()
		End If
	End Sub

	Private Function GetTempGamePath() As String
		Dim gamePath As String

		gamePath = FileManager.GetPath(Me.theGameSetup.GamePathFileName)
		'NOTE: These two lines change gamePath from actual gamePath to the new "crowbar" gamePath for temp use.
		gamePath = FileManager.GetPath(gamePath)
		gamePath = Path.Combine(gamePath, "crowbar")

		Return gamePath
	End Function

	Private Sub ModifyGameInfoFile()
		Me.theGameInfoFile = GameInfoTxtFile.Create()
		Me.theGameInfoFile.WriteNewGamePath(Me.theGameSetup.GamePathFileName, "crowbar")
	End Sub

	Private Sub RevertGameInfoFile()
		If Me.theGameInfoFile IsNot Nothing Then
			Me.theGameInfoFile.RestoreGameInfoFile(Me.theGameSetup.GamePathFileName)
			Me.theGameInfoFile = Nothing
		End If
	End Sub

	Private Function CreateReplacementModelFiles() As String
		Dim replacementMdlRelativePathFileName As String
		Dim replacementMdlPathFileName As String

		Dim replacementMdlRelativePath As String
		Dim gamePath As String
		Dim gameModelsPath As String
		Dim gameModelsTempPath As String
		replacementMdlRelativePath = Me.theViewAsReplacementExtraSubfolder
		gamePath = Me.GetTempGamePath()
		gameModelsPath = Path.Combine(gamePath, "models")
		gameModelsTempPath = Path.Combine(gameModelsPath, replacementMdlRelativePath)

		If FileManager.PathExistsAfterTryToCreate(gameModelsTempPath) Then
			Dim replacementMdlFileName As String
			replacementMdlFileName = Path.GetFileName(Me.theInputMdlPathFileName)
			replacementMdlRelativePathFileName = Path.Combine(replacementMdlRelativePath, replacementMdlFileName)
			Me.thePathForModelFiles = gameModelsPath
			Me.thePathForModelFilesForViewAsReplacement = gameModelsTempPath
			replacementMdlPathFileName = Path.Combine(gameModelsTempPath, replacementMdlFileName)

			Try
				If File.Exists(replacementMdlPathFileName) Then
					File.Delete(replacementMdlPathFileName)
				End If
				File.Copy(Me.theInputMdlPathFileName, replacementMdlPathFileName)
			Catch ex As Exception
				Me.WriteErrorMessage("Crowbar tried to copy the file """ + Me.theInputMdlPathFileName + """ to """ + replacementMdlPathFileName + """ but Windows gave this message: " + ex.Message)
			End Try

			If File.Exists(replacementMdlPathFileName) Then
				Dim model As SourceModel = Nothing
				Dim version As Integer
				Try
					model = SourceModel.Create(Me.theInputMdlPathFileName, SupportedMdlVersion.DoNotOverride, version)
					If model IsNot Nothing Then
						model.WriteMdlFileNameToMdlFile(replacementMdlPathFileName, replacementMdlRelativePathFileName)
						model.WriteAniFileNameToMdlFile(replacementMdlPathFileName, replacementMdlRelativePathFileName)
					Else
						Me.WriteErrorMessage("Model version not currently supported: " + CStr(version))
						Return ""
					End If
				Catch ex As FormatException
					Me.WriteErrorMessage(ex.Message)
				Catch ex As Exception
					Me.WriteErrorMessage("Crowbar tried to write to the temporary replacement MDL file but the system gave this message: " + ex.Message)
					Return ""
				End Try

				Dim inputMdlPath As String
				Dim inputMdlFileNameWithoutExtension As String
				Dim replacementMdlPath As String
				Dim targetFileName As String
				Dim targetPathFileName As String = ""
				inputMdlPath = FileManager.GetPath(Me.theInputMdlPathFileName)
				inputMdlFileNameWithoutExtension = Path.GetFileNameWithoutExtension(Me.theInputMdlPathFileName)
				replacementMdlPath = FileManager.GetPath(replacementMdlPathFileName)
				Me.theModelFilesForViewAsReplacement = New List(Of String)()
				For Each inputPathFileName As String In Directory.GetFiles(inputMdlPath, inputMdlFileNameWithoutExtension + ".*")
					Try
						targetFileName = Path.GetFileName(inputPathFileName)
						targetPathFileName = Path.Combine(replacementMdlPath, targetFileName)
						If Not File.Exists(targetPathFileName) Then
							File.Copy(inputPathFileName, targetPathFileName)
						End If
						Me.theModelFilesForViewAsReplacement.Add(targetPathFileName)
					Catch ex As Exception
						Me.WriteErrorMessage("Crowbar tried to copy the file """ + inputPathFileName + """ to """ + targetPathFileName + """ but Windows gave this message: " + ex.Message)
					End Try
				Next
			End If
		Else
			Me.WriteErrorMessage("Crowbar tried to create """ + gameModelsTempPath + """, but it failed.")
			replacementMdlRelativePathFileName = ""
		End If

		Return replacementMdlRelativePathFileName
	End Function

	Private Sub DeleteReplacementModelFiles()
		If Me.theModelFilesForViewAsReplacement Is Nothing Then
			Exit Sub
		End If

		Dim pathFileName As String
		For modelFileIndex As Integer = Me.theModelFilesForViewAsReplacement.Count - 1 To 0 Step -1
			Try
				pathFileName = Me.theModelFilesForViewAsReplacement(modelFileIndex)
				If File.Exists(pathFileName) Then
					File.Delete(pathFileName)
					Me.theModelFilesForViewAsReplacement.RemoveAt(modelFileIndex)
				End If
			Catch ex As Exception
				'TODO: Write a warning message.
				Dim debug As Integer = 4242
			End Try
		Next

		Try
			'NOTE: Give a little time for other Viewer threads to complete; otherwise the Delete will not happen.
			System.Threading.Thread.Sleep(500)
			If Directory.Exists(Me.thePathForModelFilesForViewAsReplacement) Then
				Directory.Delete(Me.thePathForModelFilesForViewAsReplacement)
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		Try
			'NOTE: Give a little time for other Viewer threads to complete; otherwise the Delete will not happen.
			System.Threading.Thread.Sleep(500)
			If Directory.Exists(Me.thePathForModelFiles) Then
				Directory.Delete(Me.thePathForModelFiles)
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub CopyMaterialAndTextureFiles()
		Dim tempPath As String
		Dim tempFolder As String
		Dim inputMaterialsPath As String
		tempPath = FileManager.GetPath(Me.theInputMdlPathFileName)
		Do
			If tempPath.Length <= 3 Then
				'TODO: Tell user that model is not in a models folder.
				Exit Sub
			End If
			tempFolder = Path.GetFileName(tempPath)
			tempPath = FileManager.GetPath(tempPath)
		Loop Until tempFolder = "models"
		inputMaterialsPath = Path.Combine(tempPath, "materials")

		Dim gamePath As String
		Dim gameMaterialsPath As String
		gamePath = Me.GetTempGamePath()
		gameMaterialsPath = Path.Combine(gamePath, "materials")

		If inputMaterialsPath <> gameMaterialsPath AndAlso Directory.Exists(inputMaterialsPath) Then
			'Me.theGameMaterialsFolder = GameMaterialsFolder.Create()
			'Me.theGameMaterialsFolder.CopyFolder(inputMaterialsPath, gameMaterialsPath)
			Try
				If FileManager.PathExistsAfterTryToCreate(gameMaterialsPath) Then
					My.Computer.FileSystem.CopyDirectory(inputMaterialsPath, gameMaterialsPath)
					Me.theGameMaterialsFolder = gameMaterialsPath
				Else
					'errorMessage = "Crowbar tried to create """ + gameMaterialsPath + """, but it failed."
				End If
			Catch ex As Exception
				Dim debug As Integer = 4242
				'Throw
			End Try
		End If
	End Sub

	Private Sub DeleteMaterialAndTextureFiles()
		If Me.theGameMaterialsFolder IsNot Nothing Then
			Try
				Dim gamePath As String
				Dim gameMaterialsPath As String
				gamePath = Me.GetTempGamePath()
				gameMaterialsPath = Path.Combine(gamePath, "materials")

				If Me.theGameMaterialsFolder = gameMaterialsPath Then
					Me.theGameMaterialsFolder = ""

					If Directory.Exists(gameMaterialsPath) Then
						Directory.Delete(gameMaterialsPath, True)
					End If
				End If
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub UpdateProgressStart(ByVal line As String)
		Me.UpdateProgressInternal(0, line)
	End Sub

	Private Sub UpdateProgressStop(ByVal line As String)
		Me.UpdateProgressInternal(100, vbCr + line)
	End Sub

	Private Sub UpdateProgress()
		Me.UpdateProgressInternal(1, "")
	End Sub

	Private Sub WriteErrorMessage(ByVal line As String)
		Me.UpdateProgressInternal(1, "ERROR: " + line)
	End Sub

	Private Sub UpdateProgress(ByVal indentLevel As Integer, ByVal line As String)
		Dim indentedLine As String

		indentedLine = ""
		For i As Integer = 1 To indentLevel
			indentedLine += "  "
		Next
		indentedLine += line
		Me.UpdateProgressInternal(1, indentedLine)
	End Sub

	Private Sub UpdateProgressInternal(ByVal progressValue As Integer, ByVal line As String)
		''If progressValue = 0 Then
		''	Do not write to file stream.
		'If progressValue = 1 AndAlso Me.theLogFileStream IsNot Nothing Then
		'    Me.theLogFileStream.WriteLine(line)
		'    Me.theLogFileStream.Flush()
		'End If

		Me.ReportProgress(progressValue, line)
	End Sub

#End Region

#Region "Data"

	Private isDisposed As Boolean

	Private theGameSetup As GameSetup
	Private theInputMdlPathFileName As String
	Private theInputMdlRelativePathName As String
	Private theInputMdlIsViewedAsReplacement As Boolean
	Private theViewAsReplacementExtraSubfolder As String

	Private theHlmvAppProcess As Process
	Private theGameInfoFile As GameInfoTxtFile
	'Private theGameMaterialsFolder As GameMaterialsFolder
	Private theGameMaterialsFolder As String
	Private theModelFilesForViewAsReplacement As List(Of String)
	Private thePathForModelFiles As String
	Private thePathForModelFilesForViewAsReplacement As String
	'Private theMaterialPathsThatWereCreated As List(Of String)
	'Private theMaterialFilesThatWereCopied As List(Of String)
	'Private theMaterialsFolderThatWasRenamed As String

#End Region

End Class
