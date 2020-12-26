Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Web.Script.Serialization

Public Class Packer
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.thePackedLogFiles = New BindingListEx(Of String)()
		Me.thePackedFiles = New BindingListEx(Of String)()

		Me.WorkerReportsProgress = True
		Me.WorkerSupportsCancellation = True
		AddHandler Me.DoWork, AddressOf Me.Packer_DoWork
	End Sub

#End Region

#Region "Properties"

#End Region

#Region "Methods"

	Public Sub Run()
		Me.RunWorkerAsync()
	End Sub

	Public Sub SkipCurrentFolder()
		'NOTE: This might have thread race condition, but it probably doesn't matter.
		Me.theSkipCurrentModelIsActive = True
	End Sub

	Public Function GetOutputPathFileName(ByVal relativePathFileName As String) As String
		Dim pathFileName As String

		pathFileName = Path.Combine(Me.theOutputPath, relativePathFileName)
		pathFileName = Path.GetFullPath(pathFileName)

		Return pathFileName
	End Function

#End Region

#Region "Private Methods"

#End Region

#Region "Private Methods in Background Thread"

	Private Sub Packer_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Me.ReportProgress(0, "")

		Me.theOutputPath = Me.GetOutputPath()

		Dim status As AppEnums.StatusMessage
		If Me.PackerInputsAreValid() Then
			status = Me.Pack()
		Else
			status = StatusMessage.Error
		End If
		e.Result = Me.GetPackerOutputs(status)

		If Me.CancellationPending Then
			e.Cancel = True
		End If
	End Sub

	Private Function GetGamePackerPathFileName() As String
		Dim gamePackerPathFileName As String

		Dim gameSetup As GameSetup
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.PackGameSetupSelectedIndex)
		gamePackerPathFileName = gameSetup.PackerPathFileName

		Return gamePackerPathFileName
	End Function

	Private Function GetGamePath() As String
		Dim gamePath As String

		Dim gameSetup As GameSetup
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.PackGameSetupSelectedIndex)
		gamePath = FileManager.GetPath(gameSetup.GamePathFileName)

		Return gamePath
	End Function

	Private Function GetOutputPath() As String
		Dim outputPath As String

		If TheApp.Settings.PackOutputFolderOption = PackOutputPathOptions.ParentFolder Then
			outputPath = TheApp.Settings.PackOutputParentPath
		Else
			outputPath = TheApp.Settings.PackOutputPath
		End If

		'This will change a relative path to an absolute path.
		outputPath = Path.GetFullPath(outputPath)
		Return outputPath
	End Function

	Private Function PackerInputsAreValid() As Boolean
		Dim inputsAreValid As Boolean = True

		Dim gamePackerPathFileName As String
		gamePackerPathFileName = Me.GetGamePackerPathFileName()
		Dim gameSetup As GameSetup
		Dim gamePathFileName As String
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.PackGameSetupSelectedIndex)
		gamePathFileName = gameSetup.GamePathFileName

		If Not File.Exists(gamePackerPathFileName) Then
			inputsAreValid = False
			Me.WriteErrorMessage(1, "The model packer, """ + gamePackerPathFileName + """, does not exist.")
			Me.UpdateProgress(1, My.Resources.ErrorMessageSDKMissingCause)
		End If
		If Not File.Exists(gamePathFileName) Then
			inputsAreValid = False
			Me.WriteErrorMessage(1, "The game's """ + gamePathFileName + """ file does not exist.")
			Me.UpdateProgress(1, My.Resources.ErrorMessageSDKMissingCause)
		End If
		If String.IsNullOrEmpty(TheApp.Settings.PackInputPath) Then
			inputsAreValid = False
			Me.WriteErrorMessage(1, "Input Folder has not been selected.")
		ElseIf Not Directory.Exists(TheApp.Settings.PackInputPath) Then
			inputsAreValid = False
			Me.WriteErrorMessage(1, "The Input Folder, """ + TheApp.Settings.PackInputPath + """, does not exist.")
		End If
		If TheApp.Settings.PackOutputFolderOption = PackOutputPathOptions.WorkFolder Then
			If Not FileManager.PathExistsAfterTryToCreate(Me.theOutputPath) Then
				inputsAreValid = False
				Me.WriteErrorMessage(1, "The Output Folder, """ + Me.theOutputPath + """ could not be created.")
			End If
		End If

		Return inputsAreValid
	End Function

	Private Function GetPackerOutputs(ByVal status As AppEnums.StatusMessage) As PackerOutputInfo
		Dim packResultInfo As New PackerOutputInfo()

		packResultInfo.theStatus = status

		Dim gameSetup As GameSetup
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.PackGameSetupSelectedIndex)

		If Me.thePackedFiles.Count > 0 Then
			packResultInfo.thePackedRelativePathFileNames = Me.thePackedFiles
		ElseIf TheApp.Settings.PackLogFileIsChecked Then
			packResultInfo.thePackedRelativePathFileNames = Me.thePackedLogFiles
		End If

		Return packResultInfo
	End Function

	Private Function Pack() As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theSkipCurrentModelIsActive = False

		Me.thePackedLogFiles.Clear()
		Me.thePackedFiles.Clear()

		Dim inputPath As String
		inputPath = TheApp.Settings.PackInputPath

		Dim progressDescriptionText As String
		progressDescriptionText = "Packing with " + TheApp.GetProductNameAndVersion() + ": "

		Try
			If TheApp.Settings.PackMode = PackInputOptions.ParentFolder Then
				progressDescriptionText += """" + inputPath + """ (parent folder)"
				Me.UpdateProgressStart(progressDescriptionText + " ...")

				status = Me.CreateLogTextFile(inputPath, Nothing)

				Me.PackFoldersInParentFolder(inputPath)
			Else
				progressDescriptionText += """" + inputPath + """"
				Me.UpdateProgressStart(progressDescriptionText + " ...")
				status = Me.PackOneFolder(inputPath)
			End If
		Catch ex As Exception
			status = StatusMessage.Error
		Finally
			If Me.theLogFileStream IsNot Nothing Then
				Me.theLogFileStream.Flush()
				Me.theLogFileStream.Close()
				Me.theLogFileStream = Nothing
			End If
		End Try

		Me.UpdateProgressStop("... " + progressDescriptionText + " finished.")

		Return status
	End Function

	Private Sub PackFoldersInParentFolder(ByVal parentPath As String)
		For Each aChildPath As String In Directory.GetDirectories(parentPath)
			Me.PackOneFolder(aChildPath)

			'TODO: Double-check if this is wanted. If so, then add equivalent to Unpacker.UnpackModelsInFolder().
			Me.ReportProgress(5, "")

			If Me.CancellationPending Then
				Return
			ElseIf Me.theSkipCurrentModelIsActive Then
				Me.theSkipCurrentModelIsActive = False
				Continue For
			End If
		Next
	End Sub

	Private Function PackOneFolder(ByVal inputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			If TheApp.Settings.PackMode = PackInputOptions.Folder Then
				status = Me.CreateLogTextFile(Nothing, inputPath)
			End If

			Me.UpdateProgress()
			Me.UpdateProgress(1, "Packing """ + inputPath + """ ...")

			Dim result As String
			result = Me.CheckFiles(inputPath)
			If result = "success" Then
				Me.UpdateProgress(2, "Output from packer """ + Me.GetGamePackerPathFileName() + """: ")
				Me.RunPackerApp(inputPath)

				If Not Me.theProcessHasOutputData Then
					Me.UpdateProgress(2, "ERROR: The packer did not return any status messages.")
					Me.UpdateProgress(2, "CAUSE: The packer is not the correct one for the selected game.")
					Me.UpdateProgress(2, "SOLUTION: Verify integrity of game files via Steam so that the correct packer is installed.")
				Else
					Me.ProcessPackage(inputPath)
				End If

				'' Clean up any created folders.
				'If qcModelTopNonextantPath <> "" Then
				'	Dim fullPathDeleted As String
				'	fullPathDeleted = FileManager.DeleteEmptySubpath(qcModelTopNonextantPath)
				'	If fullPathDeleted <> "" Then
				'		Me.UpdateProgress(2, "CROWBAR: Deleted empty temporary pack folder """ + fullPathDeleted + """.")
				'	End If
				'End If
			End If

			Me.UpdateProgress(1, "... Packing """ + inputPath + """ finished. Check above for any errors.")
		Catch ex As Exception
			'TODO: [PackOneFolder] Should at least give an error message to let user know something prevented the pack.
			Dim debug As Integer = 4242
		End Try

		Return status
	End Function

	Private Function CheckFiles(ByVal inputPath As String) As String
		Dim result As String = "success"

		'TODO: Determine what to check before Packer runs for a folder.
		Dim gamePackerPathFileName As String = Me.GetGamePackerPathFileName()
		Dim gamePackerFileName As String = Path.GetFileName(gamePackerPathFileName)
		If gamePackerFileName = "gmad.exe" Then
			If Directory.Exists(inputPath) Then
				Dim garrysModAppInfo As GarrysModSteamAppInfo = New GarrysModSteamAppInfo()
				Dim addonJsonPathFileName As String = garrysModAppInfo.CreateAddonJsonFile(inputPath, TheApp.Settings.PackGmaTitle, TheApp.Settings.PackGmaItemTags)
				If Not File.Exists(addonJsonPathFileName) Then
					result = "error"
				End If
			End If
		End If

		Return result
	End Function

	Private Sub RunPackerApp(ByVal inputPath As String)
		Dim currentFolder As String = Directory.GetCurrentDirectory()
		Dim parentPath As String = FileManager.GetPath(inputPath)
		Dim inputFolder As String = Path.GetFileName(inputPath)
		Directory.SetCurrentDirectory(parentPath)

		Dim gamePackerPathFileName As String = Me.GetGamePackerPathFileName()
		Dim gamePackerFileName As String = Path.GetFileName(gamePackerPathFileName)

		Dim arguments As String = ""
		If gamePackerFileName = "gmad.exe" Then
			arguments += "create -folder "
		End If
		arguments += """"
		arguments += inputFolder
		arguments += """"
		arguments += " "
		'NOTE: Gmad.exe and vpk.exe expect extra options after the input folder option.
		arguments += TheApp.Settings.PackOptionsText

		Dim myProcess As New Process()
		Dim myProcessStartInfo As New ProcessStartInfo(gamePackerPathFileName, arguments)
		myProcessStartInfo.UseShellExecute = False
		myProcessStartInfo.RedirectStandardOutput = True
		myProcessStartInfo.RedirectStandardError = True
		myProcessStartInfo.RedirectStandardInput = True
		myProcessStartInfo.CreateNoWindow = True
		myProcess.StartInfo = myProcessStartInfo
		''NOTE: Need this line to make Me.myProcess_Exited be called.
		'myProcess.EnableRaisingEvents = True
		AddHandler myProcess.OutputDataReceived, AddressOf Me.myProcess_OutputDataReceived
		AddHandler myProcess.ErrorDataReceived, AddressOf Me.myProcess_ErrorDataReceived

		myProcess.Start()
		myProcess.StandardInput.AutoFlush = True
		myProcess.BeginOutputReadLine()
		myProcess.BeginErrorReadLine()
		Me.theProcessHasOutputData = False
		Me.theGmadResultFileName = ""
		myProcess.WaitForExit()

		myProcess.Close()
		RemoveHandler myProcess.OutputDataReceived, AddressOf Me.myProcess_OutputDataReceived
		RemoveHandler myProcess.ErrorDataReceived, AddressOf Me.myProcess_ErrorDataReceived

		Directory.SetCurrentDirectory(currentFolder)
	End Sub

	Private Sub ProcessPackage(ByVal inputPath As String)
		Dim gameSetup As GameSetup = TheApp.Settings.GameSetups(TheApp.Settings.PackGameSetupSelectedIndex)
		Dim gamePackerFileName As String = Path.GetFileName(gameSetup.PackerPathFileName)
		Dim sourcePathFileName As String = inputPath
		If gamePackerFileName = "gmad.exe" Then
			'sourcePathFileName += ".gma"
			' Gmad removes the first dot and text past that from the created file name, 
			'    so use the file name shown in the log from Gmad.
			sourcePathFileName = Path.GetDirectoryName(sourcePathFileName)
			sourcePathFileName = Path.Combine(sourcePathFileName, Me.theGmadResultFileName)
		Else
			sourcePathFileName += ".vpk"
		End If
		If File.Exists(sourcePathFileName) Then
			Dim targetPath As String = Me.theOutputPath
			Dim targetFileName As String = Path.GetFileName(sourcePathFileName)
			Dim targetPathFileName As String = Path.Combine(targetPath, targetFileName)

			If String.Compare(sourcePathFileName, targetPathFileName, True) <> 0 Then
				Try
					If File.Exists(targetPathFileName) Then
						File.Delete(targetPathFileName)
					End If
				Catch ex As Exception
					Dim debug As Integer = 4242
				End Try
				Try
					File.Move(sourcePathFileName, targetPathFileName)
					Me.UpdateProgress(2, "CROWBAR: Moved package file """ + sourcePathFileName + """ to """ + targetPath + """")
				Catch ex As Exception
					Me.UpdateProgress()
					Me.UpdateProgress(2, "WARNING: Crowbar tried to move the file, """ + sourcePathFileName + """, to the output folder, but Windows complained with this message: " + ex.Message.Trim())
					Me.UpdateProgress(2, "SOLUTION: Pack again (and hope Windows does not complain again) or move the file yourself.")
					Me.UpdateProgress()
				End Try
			End If

			Me.thePackedFiles.Add(targetFileName)
		End If
	End Sub

	'' GoldSource:
	''     "C:\"                   => ""            [absolute path is same as if the path were relative]
	''     ""                      => ""            [no "models" so assume relative to it, like with Source]
	''     "test"                  => "test"        [no "models" so assume relative to it, like with Source]
	''     "test\models"           => ""            [has "models" so ignore path before it]
	''     "test\models\subfolder" => "subfolder"   [has "models" so ignore path before it]
	'' Source:
	''     "C:\"                   => ""            [absolute path is same as GoldSource method]
	''     "test"                  => "test"        [relative path is always "models" subfolder]
	'Private Function GetModelsSubpath(ByVal iPath As String, ByVal iGameEngine As GameEngine) As String
	'	Dim modelsSubpath As String = ""
	'	Dim tempSubpath As String
	'	Dim lastFolderInPath As String

	'	If iPath = "" Then
	'		Return ""
	'	End If

	'	Dim fullPath As String
	'	fullPath = Path.GetFullPath(iPath)

	'	If iGameEngine = GameEngine.GoldSource OrElse iPath = fullPath Then
	'		tempSubpath = iPath
	'		While tempSubpath <> ""
	'			lastFolderInPath = Path.GetFileName(tempSubpath)
	'			If lastFolderInPath = "models" Then
	'				Exit While
	'			ElseIf lastFolderInPath = "" Then
	'				modelsSubpath = ""
	'				Exit While
	'			Else
	'				modelsSubpath = Path.Combine(lastFolderInPath, modelsSubpath)
	'			End If
	'			tempSubpath = FileManager.GetPath(tempSubpath)
	'		End While
	'	Else
	'		modelsSubpath = iPath
	'	End If

	'	Return modelsSubpath
	'End Function

	Private Sub myProcess_OutputDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
		Dim myProcess As Process = CType(sender, Process)
		Dim line As String

		Try
			line = e.Data
			If line IsNot Nothing Then
				' Gmad removes the first dot and text past that from the created file name, 
				'    so get the file name shown in the log from Gmad.
				If line.StartsWith("Successfully saved to ") Then
					Dim delimiters As Char() = {""""c}
					Dim tokens As String() = {""}
					tokens = line.Split(delimiters)
					Me.theGmadResultFileName = tokens(1)
				End If

				Me.theProcessHasOutputData = True
				Me.UpdateProgress(3, line)
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If Me.CancellationPending Then
				Me.StopPack(True, myProcess)
			ElseIf Me.theSkipCurrentModelIsActive Then
				Me.StopPack(True, myProcess)
			End If
		End Try
	End Sub

	Private Sub myProcess_ErrorDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
		Dim myProcess As Process = CType(sender, Process)
		Dim line As String

		Try
			line = e.Data
			If line IsNot Nothing Then
				Me.UpdateProgress(3, line)
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If Me.CancellationPending Then
				Me.StopPack(True, myProcess)
			ElseIf Me.theSkipCurrentModelIsActive Then
				Me.StopPack(True, myProcess)
			End If
		End Try
	End Sub

	Private Sub StopPack(ByVal processIsCanceled As Boolean, ByVal myProcess As Process)
		If myProcess IsNot Nothing AndAlso Not myProcess.HasExited Then
			Try
				myProcess.CancelOutputRead()
				myProcess.CancelErrorRead()
				myProcess.Kill()
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If

		If processIsCanceled Then
			Me.theLastLine = "...Packing canceled."
		End If
	End Sub

	Private Function CreateLogTextFile(ByVal inputParentPath As String, ByVal inputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success
		Dim gameSetup As GameSetup
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.PackGameSetupSelectedIndex)

		If TheApp.Settings.PackLogFileIsChecked Then
			Dim inputFolderName As String
			Dim logPath As String
			Dim logFileName As String
			Dim logPathFileName As String

			Try
				If inputParentPath IsNot Nothing Then
					logPath = inputParentPath
					logFileName = "pack-log.txt"
				Else
					logPath = FileManager.GetPath(inputPath)
					inputFolderName = Path.GetFileNameWithoutExtension(inputPath)
					logFileName = inputFolderName + " pack-log.txt"
				End If
				FileManager.CreatePath(logPath)
				logPathFileName = Path.Combine(logPath, logFileName)

				Me.theLogFileStream = File.CreateText(logPathFileName)
				Me.theLogFileStream.AutoFlush = True

				If File.Exists(logPathFileName) Then
					Me.thePackedLogFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, logPathFileName))
				End If

				Me.theLogFileStream.WriteLine("// " + TheApp.GetHeaderComment())
				Me.theLogFileStream.Flush()
			Catch ex As Exception
				Me.UpdateProgress()
				Me.UpdateProgress(2, "ERROR: Crowbar tried to write the pack log file but the system gave this message: " + ex.Message)
				status = StatusMessage.Error
			End Try
		Else
			Me.theLogFileStream = Nothing
		End If

		Return status
	End Function

	Private Sub UpdateProgressStart(ByVal line As String)
		Me.UpdateProgressInternal(0, line)
	End Sub

	Private Sub UpdateProgressStop(ByVal line As String)
		Me.UpdateProgressInternal(100, vbCr + line)
	End Sub

	Private Sub UpdateProgress()
		Me.UpdateProgressInternal(1, "")
	End Sub

	Private Sub WriteErrorMessage(ByVal indentLevel As Integer, ByVal line As String)
		Me.UpdateProgress(indentLevel, "Crowbar ERROR: " + line)
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
		If progressValue = 1 AndAlso Me.theLogFileStream IsNot Nothing Then
			Me.theLogFileStream.WriteLine(line)
			Me.theLogFileStream.Flush()
		End If

		Me.ReportProgress(progressValue, line)
	End Sub

#End Region

#Region "Data"

	Private theSkipCurrentModelIsActive As Boolean
	Private theOutputPath As String

	Private theLogFileStream As StreamWriter
	Private theLastLine As String

	Private thePackedLogFiles As BindingListEx(Of String)
	Private thePackedFiles As BindingListEx(Of String)

	Private theDefineBonesFileStream As StreamWriter

	Private theProcessHasOutputData As Boolean
	Private theGmadResultFileName As String

#End Region

End Class
