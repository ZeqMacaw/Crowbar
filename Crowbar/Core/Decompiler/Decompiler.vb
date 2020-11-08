Imports System.ComponentModel
Imports System.IO

Public Class Decompiler
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.theDecompiledQcFiles = New BindingListEx(Of String)()
		Me.theDecompiledFirstRefSmdFiles = New BindingListEx(Of String)()
		Me.theDecompiledFirstLodSmdFiles = New BindingListEx(Of String)()
		Me.theDecompiledPhysicsFiles = New BindingListEx(Of String)()
		Me.theDecompiledVtaFiles = New BindingListEx(Of String)()
		Me.theDecompiledFirstBoneAnimSmdFiles = New BindingListEx(Of String)()
		Me.theDecompiledVrdFiles = New BindingListEx(Of String)()
		Me.theDecompiledDeclareSequenceQciFiles = New BindingListEx(Of String)()
		Me.theDecompiledFirstTextureBmpFiles = New BindingListEx(Of String)()
		Me.theDecompiledLogFiles = New BindingListEx(Of String)()
		Me.theDecompiledFirstDebugFiles = New BindingListEx(Of String)()

		Me.WorkerReportsProgress = True
		Me.WorkerSupportsCancellation = True
		AddHandler Me.DoWork, AddressOf Me.Decompiler_DoWork
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

	Public Function GetOutputPathFolderOrFileName() As String
		Return Me.theOutputPathOrModelOutputFileName
	End Function

#End Region

#Region "Private Methods"

#End Region

#Region "Private Methods in Background Thread"

	Private Sub Decompiler_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Me.ReportProgress(0, "")

		Me.theOutputPath = Me.GetOutputPath()

		Dim status As AppEnums.StatusMessage
		If Me.DecompilerInputsAreValid() Then
			status = Me.Decompile()
		Else
			status = StatusMessage.Error
		End If
		e.Result = Me.GetDecompilerOutputs(status)

		If Me.CancellationPending Then
			e.Cancel = True
		End If
	End Sub

	Private Function GetOutputPath() As String
		Dim outputPath As String

		If TheApp.Settings.DecompileOutputFolderOption = DecompileOutputPathOptions.Subfolder Then
			If File.Exists(TheApp.Settings.DecompileMdlPathFileName) Then
				outputPath = Path.Combine(FileManager.GetPath(TheApp.Settings.DecompileMdlPathFileName), TheApp.Settings.DecompileOutputSubfolderName)
			ElseIf Directory.Exists(TheApp.Settings.DecompileMdlPathFileName) Then
				outputPath = Path.GetFullPath(Path.Combine(TheApp.Settings.DecompileMdlPathFileName, TheApp.Settings.DecompileOutputSubfolderName))
			Else
				outputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			End If
		Else
			outputPath = TheApp.Settings.DecompileOutputFullPath
		End If

		Return outputPath
	End Function

	Private Function DecompilerInputsAreValid() As Boolean
		Dim inputsAreValid As Boolean

		If String.IsNullOrEmpty(TheApp.Settings.DecompileMdlPathFileName) Then
			inputsAreValid = False
		Else
			inputsAreValid = FileManager.PathExistsAfterTryToCreate(Me.theOutputPath)
		End If

		Return inputsAreValid
	End Function

	Private Function GetDecompilerOutputs(ByVal status As AppEnums.StatusMessage) As DecompilerOutputInfo
		Dim decompileResultInfo As New DecompilerOutputInfo()

		decompileResultInfo.theStatus = status

		If TheApp.Settings.DecompileQcFileIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledQcFiles
		ElseIf TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledFirstRefSmdFiles
		ElseIf TheApp.Settings.DecompileLodMeshSmdFilesIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledFirstLodSmdFiles
		ElseIf TheApp.Settings.DecompilePhysicsMeshSmdFileIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledPhysicsFiles
		ElseIf TheApp.Settings.DecompileVertexAnimationVtaFileIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledVtaFiles
		ElseIf TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledFirstBoneAnimSmdFiles
		ElseIf TheApp.Settings.DecompileProceduralBonesVrdFileIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledVrdFiles
		ElseIf TheApp.Settings.DecompileDeclareSequenceQciFileIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledDeclareSequenceQciFiles
		ElseIf TheApp.Settings.DecompileTextureBmpFilesIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledFirstTextureBmpFiles
		ElseIf TheApp.Settings.DecompileLogFileIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledLogFiles
		Else
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledFirstDebugFiles
		End If

		If decompileResultInfo.theDecompiledRelativePathFileNames.Count <= 0 OrElse Me.theDecompiledQcFiles.Count <= 0 Then
			Me.theOutputPathOrModelOutputFileName = ""
			'ElseIf decompileResultInfo.theDecompiledRelativePathFileNames.Count = 1 Then
			'	Me.theOutputPathOrModelOutputFileName = decompileResultInfo.theDecompiledRelativePathFileNames(0)
		Else
			Me.theOutputPathOrModelOutputFileName = Me.theOutputPath
		End If

		Return decompileResultInfo
	End Function

	Private Function Decompile() As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theSkipCurrentModelIsActive = False

		Me.theDecompiledQcFiles.Clear()
		Me.theDecompiledFirstRefSmdFiles.Clear()
		Me.theDecompiledFirstLodSmdFiles.Clear()
		Me.theDecompiledPhysicsFiles.Clear()
		Me.theDecompiledVtaFiles.Clear()
		Me.theDecompiledFirstBoneAnimSmdFiles.Clear()
		Me.theDecompiledVrdFiles.Clear()
		Me.theDecompiledDeclareSequenceQciFiles.Clear()
		Me.theDecompiledFirstTextureBmpFiles.Clear()
		Me.theDecompiledLogFiles.Clear()
		Me.theDecompiledFirstDebugFiles.Clear()

		Dim mdlPathFileName As String
		mdlPathFileName = TheApp.Settings.DecompileMdlPathFileName
		If File.Exists(mdlPathFileName) Then
			Me.theInputMdlPathName = FileManager.GetPath(mdlPathFileName)
		ElseIf Directory.Exists(mdlPathFileName) Then
			Me.theInputMdlPathName = mdlPathFileName
		End If

		Dim progressDescriptionText As String
		progressDescriptionText = "Decompiling with " + TheApp.GetProductNameAndVersion() + ": "

		Try
			If Me.theInputMdlPathName = "" Then
				'Can get here if mdlPathFileName exists, but only with parts of the path using "Length8.3" names.
				'Somehow when drag-dropping such a pathFileName, even though Windows shows full names in the path, Crowbar shows it with "Length8.3" names.
				progressDescriptionText += """" + mdlPathFileName + """"
				Me.UpdateProgressStart(progressDescriptionText + " ...")
				Me.UpdateProgress()
				Me.UpdateProgress(1, "ERROR: Failed because actual path is too long.")
				status = StatusMessage.Error
			ElseIf TheApp.Settings.DecompileMode = InputOptions.FolderRecursion Then
				progressDescriptionText += """" + Me.theInputMdlPathName + """ (folder + subfolders)"
				Me.UpdateProgressStart(progressDescriptionText + " ...")

				status = Me.CreateLogTextFile("")
				'If status = StatusMessage.Error Then
				'	Return status
				'End If

				Me.DecompileModelsInFolderRecursively(Me.theInputMdlPathName)
			ElseIf TheApp.Settings.DecompileMode = InputOptions.Folder Then
				progressDescriptionText += """" + Me.theInputMdlPathName + """ (folder)"
				Me.UpdateProgressStart(progressDescriptionText + " ...")

				status = Me.CreateLogTextFile("")
				'If status = StatusMessage.Error Then
				'	Return status
				'End If

				Me.DecompileModelsInFolder(Me.theInputMdlPathName)
			Else
				progressDescriptionText += """" + mdlPathFileName + """"
				Me.UpdateProgressStart(progressDescriptionText + " ...")
				status = Me.DecompileOneModel(mdlPathFileName)
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

	Private Function DecompileModelsInFolderRecursively(ByVal modelsPathName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		status = Me.DecompileModelsInFolder(modelsPathName)
		If Me.CancellationPending Then
			status = StatusMessage.Canceled
			Return status
		End If

		For Each aPathName As String In Directory.GetDirectories(modelsPathName)
			status = Me.DecompileModelsInFolderRecursively(aPathName)
			If Me.CancellationPending Then
				status = StatusMessage.Canceled
				Return status
			End If
		Next
	End Function

	Private Function DecompileModelsInFolder(ByVal modelsPathName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		For Each aPathFileName As String In Directory.GetFiles(modelsPathName, "*.mdl")
			status = Me.DecompileOneModel(aPathFileName)

			If Me.CancellationPending Then
				status = StatusMessage.Canceled
				Return status
			ElseIf Me.theSkipCurrentModelIsActive Then
				Me.theSkipCurrentModelIsActive = False
				Continue For
			End If
		Next
	End Function

	Private Function DecompileOneModel(ByVal mdlPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			Dim mdlFileName As String
			Dim mdlRelativePathName As String
			Dim mdlRelativePathFileName As String
			mdlFileName = Path.GetFileName(mdlPathFileName)
			mdlRelativePathName = FileManager.GetRelativePathFileName(Me.theInputMdlPathName, FileManager.GetPath(mdlPathFileName))
			mdlRelativePathFileName = Path.Combine(mdlRelativePathName, mdlFileName)

			Dim modelName As String
			modelName = Path.GetFileNameWithoutExtension(mdlPathFileName)

			Me.theModelOutputPath = Path.Combine(Me.theOutputPath, mdlRelativePathName)
			Me.theModelOutputPath = Path.GetFullPath(Me.theModelOutputPath)
			If TheApp.Settings.DecompileFolderForEachModelIsChecked Then
				Me.theModelOutputPath = Path.Combine(Me.theModelOutputPath, modelName)
			End If

			FileManager.CreatePath(Me.theModelOutputPath)

			'Try
			'	Me.CreateLogTextFile(mdlPathFileName)
			'Catch ex As Exception
			'	Me.UpdateProgress()
			'	Me.UpdateProgress(2, "ERROR: Crowbar tried to write the decompile log file but the system gave this message: " + ex.Message)
			'	status = StatusMessage.Error
			'	Return status
			'End Try
			If TheApp.Settings.DecompileMode = InputOptions.File Then
				status = Me.CreateLogTextFile(mdlPathFileName)
				'If status = StatusMessage.Error Then
				'	Return status
				'End If
			End If

			Me.UpdateProgress()
			Me.UpdateProgress(1, "Decompiling """ + mdlRelativePathFileName + """ ...")

			Dim model As SourceModel = Nothing
			Dim version As Integer
			Try
				model = SourceModel.Create(mdlPathFileName, TheApp.Settings.DecompileOverrideMdlVersion, version)
				If model IsNot Nothing Then
					If TheApp.Settings.DecompileOverrideMdlVersion = SupportedMdlVersion.DoNotOverride Then
						Me.UpdateProgress(2, "Model version " + CStr(model.Version) + " detected.")
					Else
						Me.UpdateProgress(2, "Model version overridden to be " + CStr(model.Version) + ".")
					End If
				Else
					Me.UpdateProgress(2, "ERROR: Model version " + CStr(version) + " not currently supported.")
					Me.UpdateProgress(2, "       If the model works in-game or HLMV, try changing 'Override MDL version' option.")
					Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ FAILED.")
					status = StatusMessage.Error
					Return status
				End If
			Catch ex As FormatException
				Me.UpdateProgress(2, ex.Message)
				Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ FAILED.")
				status = StatusMessage.Error
				Return status
			Catch ex As Exception
				'Me.UpdateProgress(2, "ERROR: " + ex.Message)
				Me.UpdateProgress(2, "Crowbar tried to read the MDL file but the system gave this message: " + ex.Message)
				Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ FAILED.")
				status = StatusMessage.Error
				Return status
			End Try

			Me.UpdateProgress(2, "Reading MDL file header ...")
			status = model.ReadMdlFileHeader()
			'If status = StatusMessage.ErrorInvalidMdlFileId Then
			'	Me.UpdateProgress(2, "ERROR: File does not have expected MDL header ID (first 4 bytes of file) of 'IDST' (without quotes). MDL file is not a GoldSource- or Source-engine MDL file.")
			'	Return status
			'ElseIf status = StatusMessage.ErrorInvalidInternalMdlFileSize Then
			'	Me.UpdateProgress(3, "WARNING: The internally recorded file size is different than the actual file size. Some data might not decompile correctly.")
			'ElseIf status = StatusMessage.ErrorRequiredMdlFileNotFound Then
			'	Me.UpdateProgress(2, "ERROR: MDL file not found.")
			'	Return status
			'End If
			If status = StatusMessage.ErrorInvalidInternalMdlFileSize Then
				Me.UpdateProgress(3, "WARNING: The internally recorded file size is different than the actual file size. Some data might not decompile correctly.")
			End If
			Me.UpdateProgress(2, "... Reading MDL file header finished.")

			Me.UpdateProgress(2, "Checking for required files ...")
			Dim filesFoundFlags As AppEnums.FilesFoundFlags
			filesFoundFlags = model.CheckForRequiredFiles()
			'If status = StatusMessage.ErrorRequiredSequenceGroupMdlFileNotFound Then
			'	Me.UpdateProgress(2, "ERROR: Sequence Group MDL file not found.")
			'	Return status
			'ElseIf status = StatusMessage.ErrorRequiredTextureMdlFileNotFound Then
			'	Me.UpdateProgress(2, "ERROR: Texture MDL file not found.")
			'	Return status
			'ElseIf status = StatusMessage.ErrorRequiredAniFileNotFound Then
			'	Me.UpdateProgress(2, "ERROR: ANI file not found.")
			'	Return status
			'ElseIf status = StatusMessage.ErrorRequiredVtxFileNotFound Then
			'	Me.UpdateProgress(2, "ERROR: VTX file not found.")
			'	Return status
			'ElseIf status = StatusMessage.ErrorRequiredVvdFileNotFound Then
			'	Me.UpdateProgress(2, "ERROR: VVD file not found.")
			'	Return status
			'End If
			'Me.UpdateProgress(2, "... All required files found.")
			If filesFoundFlags = AppEnums.FilesFoundFlags.ErrorRequiredSequenceGroupMdlFileNotFound Then
				Me.UpdateProgress(2, "ERROR: Sequence Group MDL file not found.")
				Return StatusMessage.ErrorRequiredSequenceGroupMdlFileNotFound
			ElseIf filesFoundFlags = AppEnums.FilesFoundFlags.ErrorRequiredTextureMdlFileNotFound Then
				Me.UpdateProgress(2, "ERROR: Texture MDL file not found.")
				Return StatusMessage.ErrorRequiredTextureMdlFileNotFound
			End If
			If (filesFoundFlags And AppEnums.FilesFoundFlags.ErrorRequiredAniFileNotFound) > 0 Then
				Me.UpdateProgress(3, "WARNING: ANI file not found.")
			End If
			If (filesFoundFlags And AppEnums.FilesFoundFlags.ErrorRequiredVtxFileNotFound) > 0 Then
				Me.UpdateProgress(3, "WARNING: VTX file not found.")
			End If
			If (filesFoundFlags And AppEnums.FilesFoundFlags.ErrorRequiredVvdFileNotFound) > 0 Then
				Me.UpdateProgress(3, "WARNING: VVD file not found.")
			End If
			If filesFoundFlags = AppEnums.FilesFoundFlags.AllFilesFound Then
				Me.UpdateProgress(2, "... All required files found.")
			Else
				Me.UpdateProgress(2, "... Not all required files found, but decompiling available files.")
			End If

			If Me.CancellationPending Then
				Return status
			ElseIf Me.theSkipCurrentModelIsActive Then
				Return status
			End If

			Me.UpdateProgress(2, "Reading data ...")
			status = Me.ReadCompiledFiles(mdlPathFileName, model)
			If status = StatusMessage.ErrorRequiredMdlFileNotFound _
				OrElse status = StatusMessage.ErrorRequiredAniFileNotFound _
				OrElse status = StatusMessage.ErrorRequiredVtxFileNotFound _
				OrElse status = StatusMessage.ErrorRequiredVvdFileNotFound Then
				Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ stopped due to missing file.")
				Return status
			ElseIf status = StatusMessage.ErrorInvalidMdlFileId Then
				Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ stopped due to invalid file.")
				Return status
			ElseIf status = StatusMessage.Error Then
				Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ stopped due to error.")
				Return status
			ElseIf Me.CancellationPending Then
				Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ canceled.")
				status = StatusMessage.Canceled
				Return status
			ElseIf Me.theSkipCurrentModelIsActive Then
				Me.UpdateProgress(1, "... Skipping """ + mdlRelativePathFileName + """.")
				Return status
			Else
				Me.UpdateProgress(2, "... Reading data finished.")
			End If

			'Me.UpdateProgress(2, "Processinging data ...")
			'status = Me.ProcessData(model)
			'Me.UpdateProgress(2, "... Processinging data finished.")

			'NOTE: Write log files before data files, in case something goes wrong with writing data files.
			If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
				Me.UpdateProgress(2, "Writing decompile-info files ...")
				Me.WriteDebugFiles(model)
				If Me.CancellationPending Then
					Me.UpdateProgress(1, "... Decompile of """ + mdlRelativePathFileName + """ canceled.")
					status = StatusMessage.Canceled
					Return status
				ElseIf Me.theSkipCurrentModelIsActive Then
					Me.UpdateProgress(1, "... Skipping """ + mdlRelativePathFileName + """.")
					status = StatusMessage.Skipped
					Return status
				Else
					Me.UpdateProgress(2, "... Writing decompile-info files finished.")
				End If
			End If

			Me.UpdateProgress(2, "Writing data ...")
			Me.WriteDecompiledFiles(model)
			If Me.CancellationPending Then
				Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ canceled.")
				status = StatusMessage.Canceled
				Return status
			ElseIf Me.theSkipCurrentModelIsActive Then
				Me.UpdateProgress(1, "... Skipping """ + mdlRelativePathFileName + """.")
				status = StatusMessage.Skipped
				Return status
			Else
				Me.UpdateProgress(2, "... Writing data finished.")
			End If

			Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ finished.")
		Catch ex As Exception
			Dim debug As Integer = 4242
			'Finally
			'	If Me.theLogFileStream IsNot Nothing Then
			'		Me.theLogFileStream.Flush()
			'		Me.theLogFileStream.Close()
			'	End If
		End Try

		Return status
	End Function

	Private Function CreateLogTextFile(ByVal mdlPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileLogFileIsChecked Then
			Dim mdlFileName As String
			Dim logPath As String
			Dim logFileName As String
			Dim logPathFileName As String

			Try
				If mdlPathFileName <> "" Then
					logPath = Me.theModelOutputPath
					mdlFileName = Path.GetFileNameWithoutExtension(mdlPathFileName)
					logFileName = mdlFileName + " " + My.Resources.Decompile_LogFileNameSuffix
				Else
					logPath = Me.theOutputPath
					logFileName = My.Resources.Decompile_LogFileNameSuffix
				End If
				FileManager.CreatePath(logPath)
				logPathFileName = Path.Combine(logPath, logFileName)

				Me.theLogFileStream = File.CreateText(logPathFileName)
				Me.theLogFileStream.AutoFlush = True

				If File.Exists(logPathFileName) Then
					Me.theDecompiledLogFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, logPathFileName))
				End If

				Me.theLogFileStream.WriteLine("// " + TheApp.GetHeaderComment())
				Me.theLogFileStream.Flush()
			Catch ex As Exception
				Me.UpdateProgress()
				Me.UpdateProgress(2, "ERROR: Crowbar tried to write the decompile log file but the system gave this message: " + ex.Message)
				status = StatusMessage.Error
			End Try
		Else
			Me.theLogFileStream = Nothing
		End If

		Return status
	End Function

	Private Function ReadCompiledFiles(ByVal mdlPathFileName As String, ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.UpdateProgress(3, "Reading MDL file ...")
		status = model.ReadMdlFile()
		If status = StatusMessage.Success Then
			Me.UpdateProgress(3, "... Reading MDL file finished.")
		ElseIf status = StatusMessage.Error Then
			Me.UpdateProgress(3, "... Reading MDL file FAILED. (Probably unexpected format.)")
			Return status
		End If

		If model.SequenceGroupMdlFilesAreUsed Then
			Me.UpdateProgress(3, "Reading sequence group MDL files ...")
			status = model.ReadSequenceGroupMdlFiles()
			If status = StatusMessage.Success Then
				Me.UpdateProgress(3, "... Reading sequence group MDL files finished.")
			ElseIf status = StatusMessage.Error Then
				Me.UpdateProgress(3, "... Reading sequence group MDL files FAILED. (Probably unexpected format.)")
			End If
		End If

		If model.TextureMdlFileIsUsed Then
			Me.UpdateProgress(3, "Reading texture MDL file ...")
			status = model.ReadTextureMdlFile()
			If status = StatusMessage.Success Then
				Me.UpdateProgress(3, "... Reading texture MDL file finished.")
			ElseIf status = StatusMessage.Error Then
				Me.UpdateProgress(3, "... Reading texture MDL file FAILED. (Probably unexpected format.)")
			End If
		End If

		If model.PhyFileIsUsed Then
			Me.UpdateProgress(3, "Reading PHY file ...")
			AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
			status = model.ReadPhyFile()
			RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
			If status = StatusMessage.Success Then
				Me.UpdateProgress(3, "... Reading PHY file finished.")
			ElseIf status = StatusMessage.Error Then
				Me.UpdateProgress(3, "... Reading PHY file FAILED. (Probably unexpected format.)")
			End If
		End If

		If model.VtxFileIsUsed Then
			Me.UpdateProgress(3, "Reading VTX file ...")
			status = model.ReadVtxFile()
			If status = StatusMessage.Success Then
				Me.UpdateProgress(3, "... Reading VTX file finished.")
			ElseIf status = StatusMessage.Error Then
				Me.UpdateProgress(3, "... Reading VTX file FAILED. (Probably unexpected format.)")
			End If
		End If

		If model.AniFileIsUsed AndAlso TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked Then
			Me.UpdateProgress(3, "Reading ANI file ...")
			status = model.ReadAniFile()
			If status = StatusMessage.Success Then
				Me.UpdateProgress(3, "... Reading ANI file finished.")
			ElseIf status = StatusMessage.Error Then
				Me.UpdateProgress(3, "... Reading ANI file FAILED. (Probably unexpected format.)")
			End If
		End If

		If model.VvdFileIsUsed Then
			Me.UpdateProgress(3, "Reading VVD file ...")
			status = model.ReadVvdFile()
			If status = StatusMessage.Success Then
				Me.UpdateProgress(3, "... Reading VVD file finished.")
			ElseIf status = StatusMessage.Error Then
				Me.UpdateProgress(3, "... Reading VVD file FAILED. (Probably unexpected format.)")
			End If
		End If

		Return status
	End Function

	'Private Function ProcessData(ByVal model As SourceModel) As AppEnums.StatusMessage
	'	Dim status As AppEnums.StatusMessage = StatusMessage.Success

	'	'TODO: Create all possible SMD file names before using them, so can handle any name collisions.
	'	'      Store mesh SMD file names in list in SourceMdlModel where the index is lodIndex.
	'	'      Store anim SMD file name in SourceMdlAnimationDesc48.

	'	Return status
	'End Function

	Private Function WriteDecompiledFiles(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		TheApp.SmdFileNames.Clear()

		'TEST:
		'Me.TestWriteDmx()

		status = Me.WriteQcFile(model)
		If status = StatusMessage.Canceled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		status = Me.WriteReferenceMeshFiles(model)
		If status = StatusMessage.Canceled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		status = Me.WriteLodMeshFiles(model)
		If status = StatusMessage.Canceled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		status = Me.WritePhysicsMeshFile(model)
		If status = StatusMessage.Canceled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		status = Me.WriteProceduralBonesFile(model)
		If status = StatusMessage.Canceled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		status = Me.WriteVertexAnimationFiles(model)
		If status = StatusMessage.Canceled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		status = Me.WriteBoneAnimationFiles(model)
		If status = StatusMessage.Canceled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		status = Me.WriteTextureFiles(model)
		If status = StatusMessage.Canceled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		status = Me.WriteDeclareSequenceQciFile(model)

		Return status
	End Function

	'Private Sub TestWriteDmx()
	'	Dim currentFolder As String
	'	currentFolder = Directory.GetCurrentDirectory()
	'	Directory.SetCurrentDirectory(Me.theModelOutputPath)

	'	Dim HelloWorld As New Datamodel.Datamodel("model", 1)		' must provide a format name (can be anything) and version
	'	HelloWorld.Root = HelloWorld.CreateElement("my_root")
	'	HelloWorld.Root("Hello") = "World"		' any supported attribute type can be assigned
	'	Dim MyString As String = HelloWorld.Root.[Get](Of String)("Hello")

	'	HelloWorld.Save("hello world.dmx", "binary", 2)		' must provide an encoding name and version	

	'	Directory.SetCurrentDirectory(currentFolder)
	'End Sub

	Private Function WriteQcFile(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileQcFileIsChecked Then
			If TheApp.Settings.DecompileGroupIntoQciFilesIsChecked Then
				'Me.UpdateProgress(3, "Writing QC and QCI files ...")
				Me.UpdateProgress(3, "QC and QCI files: ")
			Else
				'Me.UpdateProgress(3, "Writing QC file ...")
				Me.UpdateProgress(3, "QC file: ")
			End If
			Me.theDecompiledFileType = DecompiledFileType.QC
			Me.theFirstDecompiledFileHasBeenAdded = False
			AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress

			Dim qcPathFileName As String
			qcPathFileName = Path.Combine(Me.theModelOutputPath, model.Name + ".qc")

			status = model.WriteQcFile(qcPathFileName)

			If File.Exists(qcPathFileName) Then
				Me.theDecompiledQcFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, qcPathFileName))
			End If

			RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
			'If TheApp.Settings.DecompileGroupIntoQciFilesIsChecked Then
			'	Me.UpdateProgress(3, "... Writing QC and QCI files finished.")
			'Else
			'	Me.UpdateProgress(3, "... Writing QC file finished.")
			'End If
		End If

		If Me.CancellationPending Then
			status = StatusMessage.Canceled
		ElseIf Me.theSkipCurrentModelIsActive Then
			status = StatusMessage.Skipped
		End If

		Return status
	End Function

	Private Function WriteReferenceMeshFiles(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked Then
			If model.HasMeshData Then
				'Me.UpdateProgress(3, "Writing reference mesh files ...")
				Me.UpdateProgress(3, "Reference mesh files: ")
				Me.theDecompiledFileType = DecompiledFileType.ReferenceMesh
				Me.theFirstDecompiledFileHasBeenAdded = False
				AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress

				status = model.WriteReferenceMeshFiles(Me.theModelOutputPath)

				RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
				'Me.UpdateProgress(3, "... Writing reference mesh files finished.")
			End If
		End If

		Return status
	End Function

	Private Function WriteLodMeshFiles(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileLodMeshSmdFilesIsChecked Then
			If model.HasLodMeshData Then
				'Me.UpdateProgress(3, "Writing LOD mesh files ...")
				Me.UpdateProgress(3, "LOD mesh files: ")
				Me.theDecompiledFileType = DecompiledFileType.LodMesh
				Me.theFirstDecompiledFileHasBeenAdded = False
				AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress

				status = model.WriteLodMeshFiles(Me.theModelOutputPath)

				RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
				'Me.UpdateProgress(3, "... Writing LOD mesh files finished.")
			End If
		End If

		Return status
	End Function

	Private Function WritePhysicsMeshFile(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompilePhysicsMeshSmdFileIsChecked Then
			If model.HasPhysicsMeshData Then
				'Me.UpdateProgress(3, "Writing physics mesh file ...")
				Me.UpdateProgress(3, "Physics mesh file: ")
				Me.theDecompiledFileType = DecompiledFileType.PhysicsMesh
				Me.theFirstDecompiledFileHasBeenAdded = False
				AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress

				status = model.WritePhysicsMeshSmdFile(Me.theModelOutputPath)

				RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
				'Me.UpdateProgress(3, "... Writing physics mesh file finished.")
			End If
		End If

		If Me.CancellationPending Then
			status = StatusMessage.Canceled
		ElseIf Me.theSkipCurrentModelIsActive Then
			status = StatusMessage.Skipped
		End If

		Return status
	End Function

	Private Function WriteVertexAnimationFiles(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileVertexAnimationVtaFileIsChecked Then
			If model.HasVertexAnimationData Then
				'Me.UpdateProgress(3, "Writing VTA file ...")
				Me.UpdateProgress(3, "Vertex animation files: ")
				Me.theDecompiledFileType = DecompiledFileType.VertexAnimation
				Me.theFirstDecompiledFileHasBeenAdded = False
				AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress

				'Dim vtaPathFileName As String
				'vtaPathFileName = Path.Combine(Me.theModelOutputPath, SourceFileNamesModule.GetVtaFileName(model.Name))

				'status = model.WriteVertexAnimationVtaFile(vtaPathFileName)
				status = model.WriteVertexAnimationVtaFiles(Me.theModelOutputPath)

				'If File.Exists(vtaPathFileName) Then
				'	Me.theDecompiledVtaFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, vtaPathFileName))
				'End If

				RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
				'Me.UpdateProgress(3, "... Writing VTA file finished.")
			End If
		End If

		If Me.CancellationPending Then
			status = StatusMessage.Canceled
		ElseIf Me.theSkipCurrentModelIsActive Then
			status = StatusMessage.Skipped
		End If

		Return status
	End Function

	Private Function WriteBoneAnimationFiles(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked Then
			If model.HasBoneAnimationData Then
				Dim outputPath As String
				outputPath = Path.Combine(Me.theModelOutputPath, SourceFileNamesModule.GetAnimationSmdRelativePath(model.Name))
				If FileManager.PathExistsAfterTryToCreate(outputPath) Then
					'Me.UpdateProgress(3, "Writing bone animation SMD files ...")
					Me.UpdateProgress(3, "Bone animation files: ")
					Me.theDecompiledFileType = DecompiledFileType.BoneAnimation
					Me.theFirstDecompiledFileHasBeenAdded = False
					AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress

					status = model.WriteBoneAnimationSmdFiles(Me.theModelOutputPath)

					RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
					'Me.UpdateProgress(3, "... Writing bone animation SMD files finished.")
				Else
					Me.UpdateProgress(3, "WARNING: Unable to create """ + outputPath + """ where bone animation SMD files would be written.")
				End If
			End If
		End If

		Return status
	End Function

	Private Function WriteProceduralBonesFile(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileProceduralBonesVrdFileIsChecked Then
			If model.HasProceduralBonesData Then
				'Me.UpdateProgress(3, "Writing VRD file ...")
				Me.UpdateProgress(3, "Procedural bones file: ")
				Me.theDecompiledFileType = DecompiledFileType.ProceduralBones
				Me.theFirstDecompiledFileHasBeenAdded = False
				AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress

				Dim vrdPathFileName As String
				vrdPathFileName = Path.Combine(Me.theModelOutputPath, SourceFileNamesModule.GetVrdFileName(model.Name))

				status = model.WriteVrdFile(vrdPathFileName)

				If File.Exists(vrdPathFileName) Then
					Me.theDecompiledVrdFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, vrdPathFileName))
				End If

				RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
				'Me.UpdateProgress(3, "... Writing VRD file finished.")
			End If
		End If

		If Me.CancellationPending Then
			status = StatusMessage.Canceled
		ElseIf Me.theSkipCurrentModelIsActive Then
			status = StatusMessage.Skipped
		End If

		Return status
	End Function

	Private Function WriteDeclareSequenceQciFile(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileDeclareSequenceQciFileIsChecked Then
			If model.HasBoneAnimationData Then
				Me.UpdateProgress(3, "DeclareSequence QCI file: ")
				Me.theDecompiledFileType = DecompiledFileType.DeclareSequenceQci
				Me.theFirstDecompiledFileHasBeenAdded = False
				AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress

				Dim declareSequenceQciPathFileName As String
				declareSequenceQciPathFileName = Path.Combine(Me.theModelOutputPath, SourceFileNamesModule.GetDeclareSequenceQciFileName(model.Name))

				status = model.WriteDeclareSequenceQciFile(declareSequenceQciPathFileName)

				If File.Exists(declareSequenceQciPathFileName) Then
					Me.theDecompiledDeclareSequenceQciFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, declareSequenceQciPathFileName))
				End If

				RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
			End If
		End If

		If Me.CancellationPending Then
			status = StatusMessage.Canceled
		ElseIf Me.theSkipCurrentModelIsActive Then
			status = StatusMessage.Skipped
		End If

		Return status
	End Function

	Private Function WriteTextureFiles(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileTextureBmpFilesIsChecked Then
			If model.HasTextureFileData Then
				'Me.UpdateProgress(3, "Writing texture files ...")
				Me.UpdateProgress(3, "Texture files: ")
				Me.theDecompiledFileType = DecompiledFileType.TextureBmp
				Me.theFirstDecompiledFileHasBeenAdded = False
				AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress

				status = model.WriteTextureFiles(Me.theModelOutputPath)

				RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
				'Me.UpdateProgress(3, "... Writing texture files finished.")
			End If
		End If

		Return status
	End Function

	Private Sub WriteDebugFiles(ByVal model As SourceModel)
		Dim debugPath As String

		debugPath = TheApp.GetDebugPath(Me.theModelOutputPath, model.Name)
		FileManager.CreatePath(debugPath)

		Me.theDecompiledFileType = DecompiledFileType.Debug
		Me.theFirstDecompiledFileHasBeenAdded = False
		AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress

		model.WriteAccessedBytesDebugFiles(debugPath)
		If Me.CancellationPending Then
			Return
		ElseIf Me.theSkipCurrentModelIsActive Then
			Return
		End If

		RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress

		'Dim debug3File As AppDebug3File
		'debug3File = New AppDebug3File()
		'debugPathFileName = Path.Combine(debugPathName, model.Name + " debug - unknown bytes.txt")
		'debug3File.WriteFile(debugPathFileName, model.MdlFileData.theUnknownValues)

		'	Dim debugFile As AppDebug1File
		'	debugFile = New AppDebug1File()
		'	debugPathFileName = Path.Combine(debugPathName, TheSourceEngineModel.ModelName + " debug - Structure info.txt")
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

#Region "Event Handlers"

	Private Sub Model_SourceModelProgress(ByVal sender As Object, ByVal e As SourceModelProgressEventArgs)
		If e.Progress = ProgressOptions.WarningPhyFileChecksumDoesNotMatchMdlFileChecksum Then
			'TODO: Test that this shows when needed.
			Me.UpdateProgress(4, "WARNING: The PHY file's checksum value does not match the MDL file's checksum value.")
		'ElseIf e.Progress = ProgressOptions.WritingFileStarted Then
		'	Dim pathFileName As String
		'	Dim fileName As String
		'	pathFileName = e.Message
		'	fileName = Path.GetFileName(pathFileName)
		'	'TODO: Figure out how to rename SMD file if already written in a previous step, which might happen if, for example, an anim is named "<modelname>_reference" or "<modelname>_physics".
		'	'      Could also happen if the loop through SequenceDescs has already created the SMD file before the loop through AnimationDescs.
		'	'      The same name can be used by multiple sequences, as is the case for 3 "frontkick" sequences in Half-Life Opposing Force "massn.mdl".
		'	If TheApp.SmdFileNames.Contains(pathFileName) Then
		'		Dim model As SourceModel
		'		model = CType(sender, SourceModel)
		'		model.WritingSingleFileIsCanceled = True
		'		'Me.UpdateProgress(4, "WARNING: The file, """ + smdFileName + """, was written already in a previous step.")
		'		'Else
		'		'	Me.UpdateProgress(4, "Writing """ + fileName + """ file ...")
		'	End If
		ElseIf e.Progress = ProgressOptions.WritingFileFailed Then
			Me.UpdateProgress(4, e.Message)
		ElseIf e.Progress = ProgressOptions.WritingFileFinished Then
			Dim pathFileName As String
			Dim fileName As String
			pathFileName = e.Message
			fileName = Path.GetFileName(pathFileName)
			'Me.UpdateProgress(4, "... Writing """ + fileName + """ file finished.")
			Me.UpdateProgress(4, fileName)

			If Not Me.theFirstDecompiledFileHasBeenAdded AndAlso File.Exists(pathFileName) Then
				Dim relativePathFileName As String
				relativePathFileName = FileManager.GetRelativePathFileName(Me.theOutputPath, pathFileName)

				If Me.theDecompiledFileType = DecompiledFileType.ReferenceMesh Then
					Me.theDecompiledFirstRefSmdFiles.Add(relativePathFileName)
				ElseIf Me.theDecompiledFileType = DecompiledFileType.LodMesh Then
					Me.theDecompiledFirstLodSmdFiles.Add(relativePathFileName)
				ElseIf Me.theDecompiledFileType = DecompiledFileType.BoneAnimation Then
					Me.theDecompiledFirstBoneAnimSmdFiles.Add(relativePathFileName)
				ElseIf Me.theDecompiledFileType = DecompiledFileType.PhysicsMesh Then
					Me.theDecompiledPhysicsFiles.Add(relativePathFileName)
				ElseIf Me.theDecompiledFileType = DecompiledFileType.TextureBmp Then
					Me.theDecompiledFirstTextureBmpFiles.Add(relativePathFileName)
				ElseIf Me.theDecompiledFileType = DecompiledFileType.Debug Then
					Me.theDecompiledFirstDebugFiles.Add(relativePathFileName)
				End If

				Me.theFirstDecompiledFileHasBeenAdded = True
			End If
			'TheApp.SmdFileNames.Add(pathFileName)

			Dim model As SourceModel
			model = CType(sender, SourceModel)
			If Me.CancellationPending Then
				'status = StatusMessage.Canceled
				model.WritingIsCanceled = True
				'ElseIf Me.theSkipCurrentModelIsActive Then
				'	'status = StatusMessage.Skipped
				'	model.WritingSingleFileIsCanceled = True
			End If
		Else
			Dim progressUnhandled As Integer = 4242
		End If
	End Sub

#End Region

#Region "Data"

	Private theSkipCurrentModelIsActive As Boolean
	Private theInputMdlPathName As String
	Private theOutputPath As String
	Private theModelOutputPath As String
	Private theOutputPathOrModelOutputFileName As String

	Private theLogFileStream As StreamWriter

	Private theDecompiledQcFiles As BindingListEx(Of String)
	Private theDecompiledFirstRefSmdFiles As BindingListEx(Of String)
	Private theDecompiledFirstLodSmdFiles As BindingListEx(Of String)
	Private theDecompiledPhysicsFiles As BindingListEx(Of String)
	Private theDecompiledVtaFiles As BindingListEx(Of String)
	Private theDecompiledFirstBoneAnimSmdFiles As BindingListEx(Of String)
	Private theDecompiledVrdFiles As BindingListEx(Of String)
	Private theDecompiledDeclareSequenceQciFiles As BindingListEx(Of String)
	Private theDecompiledFirstTextureBmpFiles As BindingListEx(Of String)
	Private theDecompiledLogFiles As BindingListEx(Of String)
	Private theDecompiledFirstDebugFiles As BindingListEx(Of String)

	Private theDecompiledFileType As AppEnums.DecompiledFileType
	Private theFirstDecompiledFileHasBeenAdded As Boolean

#End Region

End Class
