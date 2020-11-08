Imports System.ComponentModel
Imports System.IO

Public Class Compiler
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.theCompiledLogFiles = New BindingListEx(Of String)()
		Me.theCompiledMdlFiles = New BindingListEx(Of String)()

		Me.WorkerReportsProgress = True
		Me.WorkerSupportsCancellation = True
		AddHandler Me.DoWork, AddressOf Me.Compiler_DoWork
	End Sub

#End Region

#Region "Properties"

#End Region

#Region "Methods"

	Public Sub Run()
		Me.RunWorkerAsync()
	End Sub

	Public Sub SkipCurrentModel()
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

	Private Sub Compiler_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Me.ReportProgress(0, "")

		Me.theOutputPath = Me.GetOutputPath()

		Dim status As AppEnums.StatusMessage
		If Me.CompilerInputsAreValid() Then
			status = Me.Compile()
		Else
			status = StatusMessage.Error
		End If
		e.Result = Me.GetCompilerOutputs(status)

		If Me.CancellationPending Then
			e.Cancel = True
		End If
	End Sub

	Private Function GetGameCompilerPathFileName() As String
		Dim gameCompilerPathFileName As String

		Dim gameSetup As GameSetup
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex)
		gameCompilerPathFileName = gameSetup.CompilerPathFileName

		Return gameCompilerPathFileName
	End Function

	Private Function GetGamePath() As String
		Dim gamePath As String

		Dim gameSetup As GameSetup
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex)
		gamePath = FileManager.GetPath(gameSetup.GamePathFileName)

		Return gamePath
	End Function

	Private Function GetGameModelsPath() As String
		Dim gameModelsPath As String

		gameModelsPath = Path.Combine(Me.GetGamePath(), "models")

		Return gameModelsPath
	End Function

	'Private Function GetOutputPath() As String
	'	Dim outputPath As String

	'	If TheApp.Settings.CompileOutputFolderIsChecked Then
	'		If TheApp.Settings.CompileOutputFolderOption = OutputFolderOptions.SubfolderName Then
	'			If File.Exists(TheApp.Settings.CompileQcPathFileName) Then
	'				outputPath = Path.Combine(FileManager.GetPath(TheApp.Settings.CompileQcPathFileName), TheApp.Settings.CompileOutputSubfolderName)
	'			ElseIf Directory.Exists(TheApp.Settings.CompileQcPathFileName) Then
	'				outputPath = Path.Combine(TheApp.Settings.CompileQcPathFileName, TheApp.Settings.CompileOutputSubfolderName)
	'			Else
	'				outputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
	'			End If
	'		Else
	'			outputPath = TheApp.Settings.CompileOutputFullPath
	'		End If
	'	Else
	'		outputPath = Me.GetGameModelsPath()
	'	End If

	'	'This will change a relative path to an absolute path.
	'	outputPath = Path.GetFullPath(outputPath)
	'	Return outputPath
	'End Function

	Private Function GetOutputPath() As String
		Dim outputPath As String

		If TheApp.Settings.CompileOutputFolderOption <> CompileOutputPathOptions.GameModelsFolder Then
			If TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.Subfolder Then
				If File.Exists(TheApp.Settings.CompileQcPathFileName) Then
					outputPath = Path.Combine(FileManager.GetPath(TheApp.Settings.CompileQcPathFileName), TheApp.Settings.CompileOutputSubfolderName)
				ElseIf Directory.Exists(TheApp.Settings.CompileQcPathFileName) Then
					outputPath = Path.Combine(TheApp.Settings.CompileQcPathFileName, TheApp.Settings.CompileOutputSubfolderName)
				Else
					outputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
				End If
			Else
				outputPath = TheApp.Settings.CompileOutputFullPath
			End If
		Else
			outputPath = Me.GetGameModelsPath()
		End If

		'This will change a relative path to an absolute path.
		outputPath = Path.GetFullPath(outputPath)
		Return outputPath
	End Function

	Private Function CompilerInputsAreValid() As Boolean
		Dim inputsAreValid As Boolean = True

		''NOTE: Check for qc path file name first, because status file is written relative to it.
		'If Not File.Exists(info.qcPathFileName) Then
		'	'WriteCriticalErrorMesssage("", Nothing, "ERROR: Missing file.", e, info)
		'	WriteCriticalErrorMesssage("", "Missing file: " + info.qcPathFileName, info)
		'	Return
		'End If
		'If Not File.Exists(info.compilerPathFileName) Then
		'	'WriteCriticalErrorMesssage(info.qcPathFileName, Nothing, "ERROR: Missing file.", e, info)
		'	WriteCriticalErrorMesssage(info.qcPathFileName, "Missing file: " + info.compilerPathFileName, info)
		'	Return
		'End If
		'If Not File.Exists(info.gamePathFileName) Then
		'	'WriteCriticalErrorMesssage(info.qcPathFileName, Nothing, "ERROR: Missing file.", e, info)
		'	WriteCriticalErrorMesssage(info.qcPathFileName, "Missing file: " + info.gamePathFileName, info)
		'	Return
		'End If

		Dim gameCompilerPathFileName As String
		gameCompilerPathFileName = Me.GetGameCompilerPathFileName()
		Dim gameSetup As GameSetup
		Dim gamePathFileName As String
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex)
		gamePathFileName = gameSetup.GamePathFileName

		If Not File.Exists(gameCompilerPathFileName) Then
			inputsAreValid = False
			Me.WriteErrorMessage(1, "The model compiler, """ + gameCompilerPathFileName + """, does not exist.")
			Me.UpdateProgress(1, My.Resources.ErrorMessageSDKMissingCause)
		End If
		'TODO: [CompilerInputsAreValid] If GoldSource, then only check for liblist.gam if output is for game's "models" folder.
		'TODO: [CompilerInputsAreValid] Change error message to include "liblist.gam" or "gameinfo.txt" as appropriate.
		If Not File.Exists(gamePathFileName) Then
			inputsAreValid = False
			Me.WriteErrorMessage(1, "The game's """ + gamePathFileName + """ file does not exist.")
			Me.UpdateProgress(1, My.Resources.ErrorMessageSDKMissingCause)
		End If
		If String.IsNullOrEmpty(TheApp.Settings.CompileQcPathFileName) Then
			inputsAreValid = False
			Me.WriteErrorMessage(1, "QC file or folder has not been selected.")
		ElseIf TheApp.Settings.CompileMode = InputOptions.File AndAlso Not File.Exists(TheApp.Settings.CompileQcPathFileName) Then
			inputsAreValid = False
			Me.WriteErrorMessage(1, "The QC file, """ + TheApp.Settings.CompileQcPathFileName + """, does not exist.")
		End If
		If TheApp.Settings.CompileOptionDefineBonesIsChecked Then
			If TheApp.Settings.CompileOptionDefineBonesCreateFileIsChecked Then
				Dim defineBonesPathFileName As String
				defineBonesPathFileName = Me.GetDefineBonesPathFileName()
				If File.Exists(defineBonesPathFileName) Then
					inputsAreValid = False
					Me.WriteErrorMessage(1, "The DefineBones file, """ + defineBonesPathFileName + """, already exists.")
				End If
			End If
		End If
		'If TheApp.Settings.CompileOutputFolderIsChecked Then
		If TheApp.Settings.CompileOutputFolderOption <> CompileOutputPathOptions.GameModelsFolder Then
			If Not FileManager.PathExistsAfterTryToCreate(Me.theOutputPath) Then
				inputsAreValid = False
				Me.WriteErrorMessage(1, "The Output Folder, """ + Me.theOutputPath + """ could not be created.")
			End If
		End If

		Return inputsAreValid
	End Function

	Private Function GetCompilerOutputs(ByVal status As AppEnums.StatusMessage) As CompilerOutputInfo
		Dim compileResultInfo As New CompilerOutputInfo()

		compileResultInfo.theStatus = status

		Dim gameSetup As GameSetup
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex)

		If Me.theCompiledMdlFiles.Count > 0 Then
			compileResultInfo.theCompiledRelativePathFileNames = Me.theCompiledMdlFiles
		ElseIf gameSetup.GameEngine = GameEngine.GoldSource AndAlso TheApp.Settings.CompileGoldSourceLogFileIsChecked Then
			compileResultInfo.theCompiledRelativePathFileNames = Me.theCompiledLogFiles
		ElseIf gameSetup.GameEngine = GameEngine.Source AndAlso TheApp.Settings.CompileSourceLogFileIsChecked Then
			compileResultInfo.theCompiledRelativePathFileNames = Me.theCompiledLogFiles
		End If

		Return compileResultInfo
	End Function

	Private Function Compile() As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theSkipCurrentModelIsActive = False

		Me.theCompiledLogFiles.Clear()
		Me.theCompiledMdlFiles.Clear()

		Dim qcPathFileName As String
		qcPathFileName = TheApp.Settings.CompileQcPathFileName
		If File.Exists(qcPathFileName) Then
			Me.theInputQcPath = FileManager.GetPath(qcPathFileName)
		ElseIf Directory.Exists(qcPathFileName) Then
			Me.theInputQcPath = qcPathFileName
		End If

		'Dim gameSetup As GameSetup
		'gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex)

		'Dim info As New CompilerInputInfo()
		'info.compilerPathFileName = gameSetup.CompilerPathFileName
		'info.compilerOptions = TheApp.Settings.CompileOptionsText
		'info.gamePathFileName = gameSetup.GamePathFileName
		'info.qcPathFileName = TheApp.Settings.CompileQcPathFileName
		'info.customModelFolder = TheApp.Settings.CompileOutputSubfolderName
		'info.theCompileMode = TheApp.Settings.CompileMode

		Dim progressDescriptionText As String
		progressDescriptionText = "Compiling with " + TheApp.GetProductNameAndVersion() + ": "

		Try
			If TheApp.Settings.CompileMode = InputOptions.FolderRecursion Then
				progressDescriptionText += """" + Me.theInputQcPath + """ (folder + subfolders)"
				Me.UpdateProgressStart(progressDescriptionText + " ...")

				status = Me.CreateLogTextFile("")
				'If status = StatusMessage.Error Then
				'	Return status
				'End If

				Me.CompileModelsInFolderRecursively(Me.theInputQcPath)
			ElseIf TheApp.Settings.CompileMode = InputOptions.Folder Then
				progressDescriptionText += """" + Me.theInputQcPath + """ (folder)"
				Me.UpdateProgressStart(progressDescriptionText + " ...")

				status = Me.CreateLogTextFile("")
				'If status = StatusMessage.Error Then
				'	Return status
				'End If

				Me.CompileModelsInFolder(Me.theInputQcPath)
			Else
				progressDescriptionText += """" + qcPathFileName + """"
				Me.UpdateProgressStart(progressDescriptionText + " ...")
				status = Me.CompileOneModel(qcPathFileName)
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

	Private Sub CompileModelsInFolderRecursively(ByVal modelsPathName As String)
		Me.CompileModelsInFolder(modelsPathName)

		For Each aPathName As String In Directory.GetDirectories(modelsPathName)
			Me.CompileModelsInFolderRecursively(aPathName)
			If Me.CancellationPending Then
				Return
			End If
		Next
	End Sub

	Private Sub CompileModelsInFolder(ByVal modelsPathName As String)
		For Each aPathFileName As String In Directory.GetFiles(modelsPathName, "*.qc")
			Me.CompileOneModel(aPathFileName)

			'TODO: Double-check if this is wanted. If so, then add equivalent to Decompiler.DecompileModelsInFolder().
			Me.ReportProgress(5, "")

			If Me.CancellationPending Then
				Return
			ElseIf Me.theSkipCurrentModelIsActive Then
				Me.theSkipCurrentModelIsActive = False
				Continue For
			End If
		Next
	End Sub

	'SET Left4Dead2PathRootFolder=C:\Program Files (x86)\Steam\SteamApps\common\left 4 dead 2\
	'SET StudiomdlPathName=%Left4Dead2PathRootFolder%bin\studiomdl.exe
	'SET Left4Dead2PathSubFolder=%Left4Dead2PathRootFolder%left4dead2
	'SET StudiomdlParams=-game "%Left4Dead2PathSubFolder%" -nop4 -verbose -nox360
	'SET FileName=%ModelName%_%TargetApp%
	'"%StudiomdlPathName%" %StudiomdlParams% .\%FileName%.qc > .\%FileName%.log
	Private Function CompileOneModel(ByVal qcPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			Dim qcPath As String
			Dim qcFileName As String
			Dim qcRelativePath As String
			Dim qcRelativePathFileName As String
			qcPath = FileManager.GetPath(qcPathFileName)
			qcFileName = Path.GetFileName(qcPathFileName)
			qcRelativePath = FileManager.GetRelativePathFileName(Me.theInputQcPath, FileManager.GetPath(qcPathFileName))
			qcRelativePathFileName = Path.Combine(qcRelativePath, qcFileName)

			'Dim gameSetup As GameSetup
			'Dim gamePath As String
			'Dim gameModelsPath As String
			'gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex)
			'gamePath = FileManager.GetPath(gameSetup.GamePathFileName)
			'gameModelsPath = Path.Combine(gamePath, "models")
			Dim gameModelsPath As String
			gameModelsPath = Me.GetGameModelsPath()

			Dim gameSetup As GameSetup
			gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex)

			Dim qcFile As SourceQcFile
			Dim qcModelName As String
			'Dim qcModelTopFolderPath As String
			Dim qcModelLongestExtantPath As String
			Dim qcModelTopNonextantPath As String = ""
			Dim compiledMdlPathFileName As String
			Dim compiledMdlPath As String
			qcFile = New SourceQcFile()
			qcModelName = qcFile.GetQcModelName(qcPathFileName)
			Try
				compiledMdlPathFileName = Path.GetFullPath(qcModelName)
				'qcModelTopFolderPath = FileManager.GetTopFolderPath(qcModelName)
				If compiledMdlPathFileName <> qcModelName Then
					If gameSetup.GameEngine = GameEngine.GoldSource Then
						'	- The compiler does not create folders, so Crowbar needs to create the relative or absolute path found in $modelname, 
						'		starting in the "current folder" [SetCurrentDirectory()].
						'		* For example, with $modelname "C:\valve/models/barney.mdl", need to create "C:\valve\models" path.
						'		* For example, with $modelname "valve/models/barney.mdl", need to create "[current folder]\valve\models" path.
						compiledMdlPathFileName = Path.Combine(qcPath, qcModelName)
						'If qcModelTopFolderPath <> "" Then
						'	qcModelTopFolderPath = Path.Combine(qcPath, qcModelTopFolderPath)
						'End If
					Else
						compiledMdlPathFileName = Path.Combine(gameModelsPath, qcModelName)
						'If qcModelTopFolderPath <> "" Then
						'	qcModelTopFolderPath = Path.Combine(gameModelsPath, qcModelTopFolderPath)
						'End If
					End If
					compiledMdlPathFileName = Path.GetFullPath(compiledMdlPathFileName)
					'If qcModelTopFolderPath <> "" Then
					'	qcModelTopFolderPath = Path.GetFullPath(qcModelTopFolderPath)
					'End If
				End If

				If Path.GetExtension(compiledMdlPathFileName) <> ".mdl" Then
					compiledMdlPathFileName = Path.ChangeExtension(compiledMdlPathFileName, ".mdl")
				End If
			Catch ex As Exception
				compiledMdlPathFileName = ""
			End Try
			compiledMdlPath = FileManager.GetPath(compiledMdlPathFileName)
			qcModelLongestExtantPath = FileManager.GetLongestExtantPath(compiledMdlPath, qcModelTopNonextantPath)
			If qcModelTopNonextantPath <> "" Then
				FileManager.CreatePath(compiledMdlPath)
			End If

			'Me.theModelOutputPath = Path.Combine(Me.theOutputPath, qcRelativePathName)
			'Me.theModelOutputPath = Path.GetFullPath(Me.theModelOutputPath)
			'If TheApp.Settings.CompileFolderForEachModelIsChecked Then
			'    Dim modelName As String
			'    modelName = Path.GetFileNameWithoutExtension(modelRelativePathFileName)
			'    Me.theModelOutputPath = Path.Combine(Me.theModelOutputPath, modelName)
			'End If
			'Me.UpdateProgressWithModelOutputPath(Me.theModelOutputPath)

			'FileManager.CreatePath(Me.theModelOutputPath)

			'Me.CreateLogTextFile(qcPathFileName)
			If TheApp.Settings.CompileMode = InputOptions.File Then
				status = Me.CreateLogTextFile(qcPathFileName)
				'If status = StatusMessage.Error Then
				'	Return status
				'End If
			End If

			Me.UpdateProgress()
			Me.UpdateProgress(1, "Compiling """ + qcRelativePathFileName + """ ...")

			Dim result As String
			result = Me.CheckFiles()
			If result = "success" Then
				If TheApp.Settings.CompileOptionDefineBonesIsChecked AndAlso TheApp.Settings.CompileOptionDefineBonesCreateFileIsChecked Then
					Me.OpenDefineBonesFile()
				End If

				Me.UpdateProgress(2, "Output from compiler """ + Me.GetGameCompilerPathFileName() + """: ")
				Me.RunStudioMdlApp(qcPath, qcFileName)

				If Not Me.theProcessHasOutputData Then
					Me.UpdateProgress(2, "ERROR: The compiler did not return any status messages.")
					Me.UpdateProgress(2, "CAUSE: The compiler is not the correct one for the selected game.")
					Me.UpdateProgress(2, "SOLUTION: Verify integrity of game files via Steam so that the correct compiler is installed.")
				ElseIf gameSetup.GameEngine = GameEngine.Source AndAlso TheApp.Settings.CompileOptionDefineBonesIsChecked Then
					If Me.theDefineBonesFileStream IsNot Nothing Then
						If TheApp.Settings.CompileOptionDefineBonesModifyQcFileIsChecked Then
							Me.InsertAnIncludeDefineBonesFileCommandIntoQcFile()
						End If

						Me.CloseDefineBonesFile()
					End If
				Else
					If File.Exists(compiledMdlPathFileName) Then
						Me.ProcessCompiledModel(compiledMdlPathFileName, qcModelName)
					End If
				End If

				' Clean up any created folders.
				'If qcModelTopFolderPath <> "" Then
				'	Dim fullPathDeleted As String
				'	fullPathDeleted = FileManager.DeleteEmptySubpath(qcModelTopFolderPath)
				'	If fullPathDeleted <> "" Then
				'		Me.UpdateProgress(2, "Crowbar: Deleted empty temporary compile folder """ + fullPathDeleted + """")
				'	End If
				'End If
				'------
				If qcModelTopNonextantPath <> "" Then
					Dim fullPathDeleted As String
					fullPathDeleted = FileManager.DeleteEmptySubpath(qcModelTopNonextantPath)
					If fullPathDeleted <> "" Then
						Me.UpdateProgress(2, "CROWBAR: Deleted empty temporary compile folder """ + fullPathDeleted + """.")
					End If
				End If
			End If

			Me.UpdateProgress(1, "... Compiling """ + qcRelativePathFileName + """ finished. Check above for any errors.")
		Catch ex As Exception
			'TODO: [CompileOneModel] Should at least give an error message to let user know something prevented the compile.
			Dim debug As Integer = 4242
			'Finally
			'	If Me.theLogFileStream IsNot Nothing Then
			'		Me.theLogFileStream.Flush()
			'		Me.theLogFileStream.Close()
			'	End If
		End Try

		Return status
	End Function

	Private Function CheckFiles() As String
		Dim result As String = "success"

		'TODO: Implement counting of all materials used in all mesh SMD files, excluding the phy mesh.

		Return result
	End Function

	Private Sub RunStudioMdlApp(ByVal qcPath As String, ByVal qcFileName As String)
		Dim currentFolder As String
		currentFolder = Directory.GetCurrentDirectory()
		Directory.SetCurrentDirectory(qcPath)

		Dim gameCompilerPathFileName As String
		gameCompilerPathFileName = Me.GetGameCompilerPathFileName()

		Dim arguments As String = ""
		Dim gameSetup As GameSetup
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex)
		If gameSetup.GameEngine = GameEngine.Source Then
			arguments += "-game"
			arguments += " "
			arguments += """"
			arguments += Me.GetGamePath()
			arguments += """"
			arguments += " "
		End If
		arguments += TheApp.Settings.CompileOptionsText
		arguments += " "
		arguments += """"
		arguments += qcFileName
		arguments += """"

		Dim myProcess As New Process()
		Dim myProcessStartInfo As New ProcessStartInfo(gameCompilerPathFileName, arguments)
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
		'Directory.SetCurrentDirectory(currentFolder)
		myProcess.StandardInput.AutoFlush = True
		myProcess.BeginOutputReadLine()
		myProcess.BeginErrorReadLine()
		Me.theProcessHasOutputData = False

		'myProcess.StandardOutput.ReadToEnd()
		''NOTE: Do this to handle "hit a key to continue" at the end of Dota 2's compiler.
		'myProcess.StandardInput.Write(" ")
		'myProcess.StandardInput.Close()

		myProcess.WaitForExit()

		myProcess.Close()
		RemoveHandler myProcess.OutputDataReceived, AddressOf Me.myProcess_OutputDataReceived
		RemoveHandler myProcess.ErrorDataReceived, AddressOf Me.myProcess_ErrorDataReceived

		Directory.SetCurrentDirectory(currentFolder)
	End Sub

	' Possible source and target paths:
	' mdlRelativePathFileName = qcFile.GetMdlRelativePathFileName(qcPathFileName)
	' GoldSource:
	'     source (compile) path  : FileManager.GetPath(compiledMdlPathFileName)
	'     Game's "models" folder : Me.theOutputPath + modelsSubpath
	'     Work folder            : Me.theOutputPath + mdlRelativePathStartingAtModels
	'     Subfolder (of QC input): Me.theOutputPath + mdlRelativePathStartingAtModels
	' Source:
	'     source (compile) path  : FileManager.GetPath(compiledMdlPathFileName)
	'     Game's "models" folder : Me.theOutputPath + modelsSubpath OR source (compile) path
	'     Work folder            : Me.theOutputPath + mdlRelativePathStartingAtModels
	'     Subfolder (of QC input): Me.theOutputPath + mdlRelativePathStartingAtModels
	' Examples of $modelname and output target:
	'     C:\model.mdl                         [Every Source model compiler I have tested stops compile with error for absolute path as $modelname.]
	'     C:\test\model.mdl                    [Every Source model compiler I have tested stops compile with error for absolute path as $modelname.]
	'     C:\test\models\model.mdl             [Every Source model compiler I have tested stops compile with error for absolute path as $modelname.]
	'     C:\test\models\subfolder\model.mdl   [Every Source model compiler I have tested stops compile with error for absolute path as $modelname.]
	'     model.mdl                            => <output folder>\models\model.mdl             [no "models" so assume relative to it, like with Source]
	'     test\model.mdl                       => <output folder>\models\test\model.mdl        [no "models" so assume relative to it, like with Source]
	'     test\models\model.mdl                => <output folder>\models\model.mdl             [has "models" so ignore path before it]
	'     test\models\subfolder\model.mdl      => <output folder>\models\subfolder\model.mdl   [has "models" so ignore path before it]
	Private Sub ProcessCompiledModel(ByVal compiledMdlPathFileName As String, ByVal qcModelName As String)
		Dim sourcePath As String
		Dim sourceFileNameWithoutExtension As String
		Dim targetPathFileName As String
		Dim createdFolders As New List(Of String)
		Dim outputPathModelsFolder As String
		Dim modelsSubpath As String
		Dim targetPath As String

		Dim gameSetup As GameSetup = TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex)

		sourcePath = FileManager.GetPath(compiledMdlPathFileName)
		sourceFileNameWithoutExtension = Path.GetFileNameWithoutExtension(compiledMdlPathFileName)

		If TheApp.Settings.CompileOutputFolderOption = CompileOutputPathOptions.GameModelsFolder Then
			outputPathModelsFolder = Me.theOutputPath
		Else
			outputPathModelsFolder = Path.Combine(Me.theOutputPath, "models")
		End If
		modelsSubpath = Me.GetModelsSubpath(FileManager.GetPath(qcModelName), gameSetup.GameEngine)
		targetPath = Path.Combine(outputPathModelsFolder, modelsSubpath)
		FileManager.CreatePath(targetPath)

		Dim searchPattern As String
		Dim listOfCompiledExtensions As List(Of String)
		If gameSetup.GameEngine = GameEngine.GoldSource Then
			searchPattern = sourceFileNameWithoutExtension + "*.mdl"
			listOfCompiledExtensions = New List(Of String)(New String() {".mdl"})
		Else
			searchPattern = sourceFileNameWithoutExtension + ".*"
			listOfCompiledExtensions = New List(Of String)(New String() {".ani", ".mdl", ".phy", ".vtx", ".vvd"})
		End If
		For Each sourcePathFileName As String In Directory.EnumerateFiles(sourcePath, searchPattern)
			If Not listOfCompiledExtensions.Contains(Path.GetExtension(sourcePathFileName).ToLower()) Then
				Continue For
			End If

			targetPathFileName = Path.Combine(targetPath, Path.GetFileName(sourcePathFileName))

			If String.Compare(sourcePathFileName, targetPathFileName, True) <> 0 Then
				'If TheApp.Settings.CompileOutputFolderOption <> CompileOutputPathOptions.GameModelsFolder OrElse gameSetup.GameEngine = GameEngine.GoldSource Then
				Try
					If File.Exists(targetPathFileName) Then
						File.Delete(targetPathFileName)
					End If
				Catch ex As Exception
					Dim debug As Integer = 4242
				End Try
				Try
					File.Move(sourcePathFileName, targetPathFileName)
					Me.UpdateProgress(2, "CROWBAR: Moved compiled model file """ + sourcePathFileName + """ to """ + targetPath + """")
				Catch ex As Exception
					Me.UpdateProgress()
					Me.UpdateProgress(2, "WARNING: Crowbar tried to move the file, """ + sourcePathFileName + """, to the output folder, but Windows complained with this message: " + ex.Message.Trim())
					Me.UpdateProgress(2, "SOLUTION: Compile the model again (and hope Windows does not complain again) or move the file yourself.")
					Me.UpdateProgress()
				End Try
				'End If
			End If

			'NOTE: Make list of main MDL files compiled.
			If String.Compare(Path.GetFileName(targetPathFileName), Path.GetFileName(compiledMdlPathFileName), True) = 0 Then
				Me.theCompiledMdlFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, targetPathFileName))
			End If
		Next
	End Sub

	' GoldSource:
	'     "C:\"                   => ""            [absolute path is same as if the path were relative]
	'     ""                      => ""            [no "models" so assume relative to it, like with Source]
	'     "test"                  => "test"        [no "models" so assume relative to it, like with Source]
	'     "test\models"           => ""            [has "models" so ignore path before it]
	'     "test\models\subfolder" => "subfolder"   [has "models" so ignore path before it]
	' Source:
	'     "C:\"                   => ""            [absolute path is same as GoldSource method]
	'     "test"                  => "test"        [relative path is always "models" subfolder]
	Private Function GetModelsSubpath(ByVal iPath As String, ByVal iGameEngine As GameEngine) As String
		Dim modelsSubpath As String = ""
		Dim tempSubpath As String
		Dim lastFolderInPath As String

		If iPath = "" Then
			Return ""
		End If

		Dim fullPath As String
		fullPath = Path.GetFullPath(iPath)

		If iGameEngine = GameEngine.GoldSource OrElse iPath = fullPath Then
			tempSubpath = iPath
			While tempSubpath <> ""
				lastFolderInPath = Path.GetFileName(tempSubpath)
				If lastFolderInPath = "models" Then
					Exit While
				ElseIf lastFolderInPath = "" Then
					modelsSubpath = ""
					Exit While
				Else
					modelsSubpath = Path.Combine(lastFolderInPath, modelsSubpath)
				End If
				tempSubpath = FileManager.GetPath(tempSubpath)
			End While
		Else
			modelsSubpath = iPath
		End If

		Return modelsSubpath
	End Function

	Private Sub myProcess_OutputDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
		Dim myProcess As Process = CType(sender, Process)
		Dim line As String

		Try
			line = e.Data
			If line IsNot Nothing Then
				Me.theProcessHasOutputData = True
				Me.UpdateProgress(3, line)

				If Me.theDefineBonesFileStream IsNot Nothing Then
					line = line.Trim()
					If line.StartsWith("$definebone") Then
						Me.theDefineBonesFileStream.WriteLine(line)
					End If
				End If

				If line.StartsWith("Hit a key") Then
					Me.StopCompile(False, myProcess)
				End If
				'TEST: 
				'Else
				'	Dim i As Integer = 42

				'NOTE: This works for handling CSGO's studiomdl when an MDL file exists where the new one is being compiled, but the new one has fewer sequences.
				'      Not sure why the line "Please confirm sequence deletion: [y/n]" does not show until after Crowbar writes the "y".
				If line.StartsWith("WARNING: This model has fewer sequences than its predecessor.") Then
					myProcess.StandardInput.Write("y")
				End If
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If Me.CancellationPending Then
				Me.StopCompile(True, myProcess)
			ElseIf Me.theSkipCurrentModelIsActive Then
				Me.StopCompile(True, myProcess)
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
				Me.StopCompile(True, myProcess)
			ElseIf Me.theSkipCurrentModelIsActive Then
				Me.StopCompile(True, myProcess)
			End If
		End Try
	End Sub

	Private Sub StopCompile(ByVal processIsCanceled As Boolean, ByVal myProcess As Process)
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
			Me.theLastLine = "...Compiling canceled."
		End If
	End Sub

	Private Function CreateLogTextFile(ByVal qcPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success
		Dim gameSetup As GameSetup
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex)

		If (gameSetup.GameEngine = GameEngine.GoldSource AndAlso TheApp.Settings.CompileGoldSourceLogFileIsChecked) OrElse (gameSetup.GameEngine = GameEngine.Source AndAlso TheApp.Settings.CompileSourceLogFileIsChecked) Then
			Dim qcFileName As String
			Dim logPath As String
			Dim logFileName As String
			Dim logPathFileName As String

			Try
				If qcPathFileName <> "" Then
					logPath = FileManager.GetPath(qcPathFileName)
					qcFileName = Path.GetFileNameWithoutExtension(qcPathFileName)
					logFileName = qcFileName + " compile-log.txt"
				Else
					logPath = Me.theInputQcPath
					logFileName = "compile-log.txt"
				End If
				FileManager.CreatePath(logPath)
				logPathFileName = Path.Combine(logPath, logFileName)

				Me.theLogFileStream = File.CreateText(logPathFileName)
				Me.theLogFileStream.AutoFlush = True

				If File.Exists(logPathFileName) Then
					Me.theCompiledLogFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, logPathFileName))
				End If

				Me.theLogFileStream.WriteLine("// " + TheApp.GetHeaderComment())
				Me.theLogFileStream.Flush()
			Catch ex As Exception
				Me.UpdateProgress()
				Me.UpdateProgress(2, "ERROR: Crowbar tried to write the compile log file but the system gave this message: " + ex.Message)
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

	Private Function GetDefineBonesPathFileName() As String
		Dim fileName As String
		If String.IsNullOrEmpty(Path.GetExtension(TheApp.Settings.CompileOptionDefineBonesQciFileName)) Then
			fileName = TheApp.Settings.CompileOptionDefineBonesQciFileName + ".qci"
		Else
			fileName = TheApp.Settings.CompileOptionDefineBonesQciFileName
		End If
		Dim qcPath As String
		qcPath = FileManager.GetPath(TheApp.Settings.CompileQcPathFileName)
		Dim pathFileName As String
		pathFileName = Path.Combine(qcPath, fileName)

		Return pathFileName
	End Function

	Private Sub OpenDefineBonesFile()
		Try
			Me.theDefineBonesFileStream = File.CreateText(Me.GetDefineBonesPathFileName())
		Catch ex As Exception
			Me.theDefineBonesFileStream = Nothing
		End Try
	End Sub

	Private Sub CloseDefineBonesFile()
		Me.theDefineBonesFileStream.Flush()
		Me.theDefineBonesFileStream.Close()
		Me.theDefineBonesFileStream = Nothing
	End Sub

	Private Sub InsertAnIncludeDefineBonesFileCommandIntoQcFile()
		Dim qciPathFileName As String
		Dim qcFile As SourceQcFile
		qcFile = New SourceQcFile()
		qciPathFileName = CType(Me.theDefineBonesFileStream.BaseStream, FileStream).Name
		qcFile.InsertAnIncludeFileCommand(TheApp.Settings.CompileQcPathFileName, qciPathFileName)
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
	Private theInputQcPath As String
	Private theOutputPath As String
	'Private theModelOutputPath As String

	Private theLogFileStream As StreamWriter
	Private theLastLine As String

	Private theCompiledLogFiles As BindingListEx(Of String)
	Private theCompiledMdlFiles As BindingListEx(Of String)

	Private theDefineBonesFileStream As StreamWriter

	Private theProcessHasOutputData As Boolean

#End Region

End Class
