Imports System.ComponentModel
Imports System.IO

Public Class Unpacker
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.theUnpackedMdlFiles = New BindingListEx(Of String)()
		Me.theLogFiles = New BindingListEx(Of String)()
		Me.theUnpackedPaths = New List(Of String)()
		Me.theUnpackedRelativePathsAndFileNames = New BindingListEx(Of String)()
		Me.theUnpackedTempPathsAndPathFileNames = New List(Of String)()

		Me.WorkerReportsProgress = True
		Me.WorkerSupportsCancellation = True
		AddHandler Me.DoWork, AddressOf Me.Unpacker_DoWork
	End Sub

#End Region

#Region "Init and Free"

	'Private Sub Init()
	'End Sub

	'Private Sub Free()
	'End Sub

#End Region

#Region "Properties"

#End Region

#Region "Methods"

	Public Sub Run(ByVal unpackerAction As ArchiveAction, ByVal archivePathFileNameToEntryIndexesMap As SortedList(Of String, List(Of Integer)), ByVal outputPathIsExtendedWithPackageName As Boolean, ByVal selectedRelativeOutputPath As String)
		Me.theSynchronousWorkerIsActive = False
		Dim info As New UnpackerInputInfo()
		info.theArchiveAction = unpackerAction
		info.theArchivePathFileNameToEntryIndexesMap = archivePathFileNameToEntryIndexesMap
		info.theOutputPathIsExtendedWithPackageName = outputPathIsExtendedWithPackageName
		info.theSelectedRelativeOutputPath = selectedRelativeOutputPath
		Me.RunWorkerAsync(info)
	End Sub

	Public Function RunSynchronous(ByVal unpackerAction As ArchiveAction, ByVal archivePathFileNameToEntryIndexesMap As SortedList(Of String, List(Of Integer)), ByVal outputPathIsExtendedWithPackageName As Boolean, ByVal selectedRelativeOutputPath As String) As String
		Me.theSynchronousWorkerIsActive = True
		Dim info As New UnpackerInputInfo()
		info.theArchiveAction = unpackerAction
		info.theArchivePathFileNameToEntryIndexesMap = archivePathFileNameToEntryIndexesMap
		info.theOutputPathIsExtendedWithPackageName = outputPathIsExtendedWithPackageName
		info.theSelectedRelativeOutputPath = selectedRelativeOutputPath

		Me.theRunSynchronousResultMessage = ""
		Dim e As New System.ComponentModel.DoWorkEventArgs(info)
		Me.OnDoWork(e)
		Return Me.theRunSynchronousResultMessage
	End Function

	Public Sub UnpackFolderTreeFromVPK(ByVal folderTreeToExtract As String)
		Me.theSynchronousWorkerIsActive = True
		Dim info As New UnpackerInputInfo()
		info.theArchiveAction = ArchiveAction.ExtractFolderTree
		info.theGamePath = folderTreeToExtract
		Dim e As New System.ComponentModel.DoWorkEventArgs(info)
		Me.OnDoWork(e)
	End Sub

	'Public Sub GetTempPathFileNames(ByVal packInternalPathFileNames As List(Of String), ByRef tempPathFileNames As List(Of String))
	'	tempPathFileNames = New List(Of String)()
	'	For Each packInternalPathFileName As String In packInternalPathFileNames
	'		tempPathFileNames.Add(Path.Combine(Me.theTempUnpackPaths(0), packInternalPathFileName))
	'	Next
	'End Sub
	'Public Function GetTempPathsAndPathFileNames(ByVal packInternalPathFileNames As List(Of String)) As List(Of String)
	'	Dim tempPathFileNames As List(Of String)

	'	tempPathFileNames = New List(Of String)()
	'	For Each packInternalPathFileName As String In packInternalPathFileNames
	'		tempPathFileNames.Add(Path.Combine(Me.theOutputPath, packInternalPathFileName))
	'	Next

	'	Return tempPathFileNames
	'End Function
	Public Function GetTempRelativePathsAndFileNames() As List(Of String)
		Dim tempRelativePathsAndFileNames As New List(Of String)()

		Dim topRelativePath As String
		For Each relativePathOrFileName As String In Me.theUnpackedRelativePathsAndFileNames
			topRelativePath = FileManager.GetTopFolderPath(relativePathOrFileName)
			If topRelativePath = "" Then
				tempRelativePathsAndFileNames.Add(Path.Combine(Me.theOutputPath, relativePathOrFileName))
			Else
				tempRelativePathsAndFileNames.Add(Path.Combine(Me.theOutputPath, topRelativePath))
			End If
		Next

		Return tempRelativePathsAndFileNames
	End Function

	Public Sub SkipCurrentPackage()
		'NOTE: This might have thread race condition, but it probably doesn't matter.
		Me.theSkipCurrentPackIsActive = True
	End Sub

	Public Function GetOutputPathFileName(ByVal relativePathFileName As String) As String
		Dim pathFileName As String

		pathFileName = Path.Combine(Me.theOutputPath, relativePathFileName)
		pathFileName = Path.GetFullPath(pathFileName)

		Return pathFileName
	End Function

	Public Function GetOutputPathOrOutputFileName() As String
		Return Me.theOutputPathOrModelOutputFileName
	End Function

	Public Sub DeleteTempUnpackFolder()
		If Me.theUnpackedPathsAreInTempPath Then
			Me.theUnpackedPathsAreInTempPath = False
			Try
				For Each unpackedPath As String In Me.theUnpackedPaths
					If unpackedPath IsNot Nothing AndAlso Directory.Exists(unpackedPath) Then
						Directory.Delete(unpackedPath, True)
					End If
				Next
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

#End Region

#Region "Private Methods"

#End Region

#Region "Private Methods in Background Thread"

	Private Sub Unpacker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		If Not Me.theSynchronousWorkerIsActive Then
			'TODO: This indication that work has started in backgroundworker seems unimportant and should probably be removed.
			Me.ReportProgress(0, "")
		End If

		Dim info As UnpackerInputInfo
		info = CType(e.Argument, UnpackerInputInfo)
		Me.theOutputPathIsExtendedWithPackageName = info.theOutputPathIsExtendedWithPackageName
		Me.theSelectedRelativeOutputPath = info.theSelectedRelativeOutputPath

		Me.theUnpackedPathsAreInTempPath = False

		Dim status As AppEnums.StatusMessage
		If info.theArchiveAction = ArchiveAction.ExtractFolderTree Then
			status = Me.ExtractFolderTree(info.theGamePath)
		Else
			If Me.UnpackerInputsAreValid() Then
				If info.theArchiveAction = ArchiveAction.List Then
					Me.List()
				ElseIf info.theArchiveAction = ArchiveAction.Unpack Then
					status = Me.Unpack(info.theArchivePathFileNameToEntryIndexesMap)
					'ElseIf info.theArchiveAction = ArchiveAction.Extract Then
					'	status = Me.Extract(info.theArchivePathFileNameToEntryIndexesMap)
				ElseIf info.theArchiveAction = ArchiveAction.ExtractAndOpen Then
					status = Me.ExtractWithoutLogging(info.theArchivePathFileNameToEntryIndexesMap)
					If status = StatusMessage.Success Then
						Me.StartFile(Path.Combine(Me.theOutputPath, Me.theUnpackedRelativePathsAndFileNames(0)))
					End If
				ElseIf info.theArchiveAction = ArchiveAction.ExtractToTemp Then
					status = Me.ExtractWithoutLogging(info.theArchivePathFileNameToEntryIndexesMap)
				End If
			Else
				status = StatusMessage.Error
			End If

			e.Result = Me.GetUnpackerOutputInfo(status)

			If Me.CancellationPending Then
				e.Cancel = True
			End If
		End If
	End Sub

	Private Function GetOutputPath() As String
		Dim outputPath As String

		If TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.SameFolder Then
			outputPath = TheApp.Settings.UnpackOutputSamePath
		ElseIf TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.Subfolder Then
			If File.Exists(TheApp.Settings.UnpackPackagePathFolderOrFileName) Then
				outputPath = Path.Combine(FileManager.GetPath(TheApp.Settings.UnpackPackagePathFolderOrFileName), TheApp.Settings.UnpackOutputSubfolderName)
			ElseIf Directory.Exists(TheApp.Settings.UnpackPackagePathFolderOrFileName) Then
				outputPath = Path.Combine(TheApp.Settings.UnpackPackagePathFolderOrFileName, TheApp.Settings.UnpackOutputSubfolderName)
			Else
				outputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			End If
		Else
			outputPath = TheApp.Settings.UnpackOutputFullPath
		End If

		'This will change a relative path to an absolute path.
		outputPath = Path.GetFullPath(outputPath)
		Return outputPath
	End Function

	Private Function UnpackerInputsAreValid() As Boolean
		Dim inputsAreValid As Boolean = True

		If String.IsNullOrEmpty(TheApp.Settings.UnpackPackagePathFolderOrFileName) Then
			inputsAreValid = False
			Me.WriteErrorMessage(1, "Package file or folder has not been selected.")
		ElseIf TheApp.Settings.UnpackMode = InputOptions.File AndAlso Not File.Exists(TheApp.Settings.UnpackPackagePathFolderOrFileName) Then
			inputsAreValid = False
			Me.WriteErrorMessage(1, "The package file, """ + TheApp.Settings.UnpackPackagePathFolderOrFileName + """, does not exist.")
		End If

		Return inputsAreValid
	End Function

	Private Function GetUnpackerOutputInfo(ByVal status As AppEnums.StatusMessage) As UnpackerOutputInfo
		Dim unpackResultInfo As New UnpackerOutputInfo()

		unpackResultInfo.theStatus = status

		If Me.theUnpackedMdlFiles.Count > 0 Then
			unpackResultInfo.theUnpackedRelativePathFileNames = Me.theUnpackedMdlFiles
		ElseIf Me.theUnpackedRelativePathsAndFileNames.Count > 0 Then
			unpackResultInfo.theUnpackedRelativePathFileNames = Me.theUnpackedRelativePathsAndFileNames
		ElseIf TheApp.Settings.UnpackLogFileIsChecked Then
			unpackResultInfo.theUnpackedRelativePathFileNames = Me.theLogFiles
		Else
			unpackResultInfo.theUnpackedRelativePathFileNames = Nothing
		End If

		If unpackResultInfo.theUnpackedRelativePathFileNames Is Nothing OrElse unpackResultInfo.theUnpackedRelativePathFileNames.Count <= 0 OrElse Me.theUnpackedMdlFiles.Count <= 0 Then
			Me.theOutputPathOrModelOutputFileName = ""
		ElseIf unpackResultInfo.theUnpackedRelativePathFileNames.Count = 1 Then
			Me.theOutputPathOrModelOutputFileName = Path.Combine(Me.theOutputPath, unpackResultInfo.theUnpackedRelativePathFileNames(0))
		Else
			Me.theOutputPathOrModelOutputFileName = Me.theOutputPath
		End If

		Return unpackResultInfo
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

	Private Function CreateLogTextFile() As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.UnpackLogFileIsChecked Then
			Dim logPath As String
			Dim logFileName As String
			Dim logPathFileName As String

			Try
				logPath = Me.theOutputPath
				'logFileName = vpkPathFileName + " " + My.Resources.Unpack_LogFileNameSuffix
				logFileName = My.Resources.Unpack_LogFileNameSuffix
				FileManager.CreatePath(logPath)
				logPathFileName = Path.Combine(logPath, logFileName)

				Me.theLogFileStream = File.CreateText(logPathFileName)
				Me.theLogFileStream.AutoFlush = True

				If File.Exists(logPathFileName) Then
					Me.theLogFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, logPathFileName))
				End If

				Me.theLogFileStream.WriteLine("// " + TheApp.GetHeaderComment())
				Me.theLogFileStream.Flush()
			Catch ex As Exception
				Me.UpdateProgress()
				Me.UpdateProgress(2, "ERROR: Crowbar tried to write the unpack log file but the system gave this message: " + ex.Message)
				status = StatusMessage.Error
			End Try
		Else
			Me.theLogFileStream = Nothing
		End If

		Return status
	End Function

	Private Sub UpdateProgressInternal(ByVal progressValue As Integer, ByVal line As String)
		If progressValue = 1 AndAlso Me.theLogFileStream IsNot Nothing Then
			Me.theLogFileStream.WriteLine(line)
			Me.theLogFileStream.Flush()
		End If

		If Not Me.theSynchronousWorkerIsActive Then
			Me.ReportProgress(progressValue, line)
		End If
	End Sub

	Private Function ExtractFolderTree(ByVal gamePath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		' Example: 
		'      Me.theGamePath = gamePath = "E:\Users\ZeqMacaw\Steam\steamapps\common\Half-Life 2\hl2"
		'      gameRootPath              = "E:\Users\ZeqMacaw\Steam\steamapps\common\Half-Life 2"
		Me.theGamePath = gamePath
		Dim gameRootPath As String = FileManager.GetPath(gamePath)

		Try
			Me.ExtractFolderTreeFromArchivesInFolderRecursively(gameRootPath)
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Return status
	End Function

	Private Function ExtractFolderTreeFromArchivesInFolderRecursively(ByVal packagePath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.ExtractFolderTreeFromArchivesInFolder(packagePath)

		Try
			For Each aPathSubFolder As String In Directory.GetDirectories(packagePath)
				Me.ExtractFolderTreeFromArchivesInFolderRecursively(aPathSubFolder)

				If Me.CancellationPending Then
					Return StatusMessage.Canceled
				End If
			Next
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Return status
	End Function

	Private Function ExtractFolderTreeFromArchivesInFolder(ByVal packagePath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			'NOTE: Feature only valid for VPK files.
			Dim packageFileNameFilter As String = "*.vpk"
			For Each aPackagePathFileName As String In Directory.GetFiles(packagePath, packageFileNameFilter)
				Me.ExtractFolderTreeFromArchive(aPackagePathFileName)

				If Me.CancellationPending Then
					Return StatusMessage.Canceled
				End If
			Next
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Return status
	End Function

	Private Function ExtractFolderTreeFromArchive(ByVal packagePathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim aPackageFileData As BasePackageFileData = Nothing
		'aVpkFileData = New BasePackageFileData()

		Dim inputFileStream As FileStream = Nothing
		Me.theInputFileReader = Nothing
		Try
			inputFileStream = New FileStream(packagePathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
			If inputFileStream IsNot Nothing Then
				Try
					Me.theInputFileReader = New BinaryReader(inputFileStream, System.Text.Encoding.ASCII)

					'Dim vpkFile As New VpkFile(Me.theInputFileReader, aVpkFileData)
					Dim vpkFile As BasePackageFile
					vpkFile = BasePackageFile.Create(packagePathFileName, Me.theArchiveDirectoryInputFileReader, Me.theInputFileReader, aPackageFileData)

					vpkFile.ReadHeader()
					vpkFile.ReadEntries(Me)
				Catch ex As Exception
					Throw
				Finally
					If Me.theInputFileReader IsNot Nothing Then
						Me.theInputFileReader.Close()
					End If
				End Try
			End If
		Catch ex As Exception
			status = StatusMessage.Error
			Throw
		Finally
			If inputFileStream IsNot Nothing Then
				inputFileStream.Close()
			End If
		End Try

		If Me.CancellationPending Then
			Return StatusMessage.Canceled
		End If

		If aPackageFileData IsNot Nothing AndAlso aPackageFileData.IsSourcePackage Then
			Dim entry As BasePackageDirectoryEntry
			Dim line As String
			Dim archivePathFileName As String
			Dim vpkPath As String
			Dim vpkFileNameWithoutExtension As String
			Dim vpkFileNamePrefix As String
			Dim paths As New List(Of String)()

			'Me.UpdateProgressInternal(1, "")
			For i As Integer = 0 To aPackageFileData.theEntries.Count - 1
				entry = aPackageFileData.theEntries(i)
				If entry.archiveIndex <> &H7FFF Then
					vpkPath = FileManager.GetPath(packagePathFileName)
					vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(packagePathFileName)
					vpkFileNamePrefix = vpkFileNameWithoutExtension.Substring(0, vpkFileNameWithoutExtension.LastIndexOf(aPackageFileData.DirectoryFileNameSuffix))
					archivePathFileName = Path.Combine(vpkPath, vpkFileNamePrefix + "_" + entry.archiveIndex.ToString("000") + aPackageFileData.FileExtension)
				Else
					archivePathFileName = packagePathFileName
				End If

				line = aPackageFileData.theEntryDataOutputTexts(i)

				'Example output:
				'addonimage.jpg crc=0x50ea4a15 metadatasz=0 fnumber=32767 ofs=0x0 sz=10749
				'materials/models/weapons/melee/crowbar_normal.vtf crc=0x7ac0e054 metadatasz=0 fnumber=32767 ofs=0x2fed8 sz=1398196

				Dim fields() As String
				fields = line.Split(" "c)

				Dim pathFileName As String = fields(0)
				'NOTE: The last 5 fields should not have any spaces, but the path+filename field might.
				For fieldIndex As Integer = 1 To fields.Length - 6
					pathFileName = pathFileName + " " + fields(fieldIndex)
				Next

				'NOTE: Only need to create "models" folder-tree to have models accessible in HLMV.
				If pathFileName.StartsWith("models/") Then
					Dim aRelativePath As String = FileManager.GetPath(pathFileName)
					Dim aPath As String = Path.Combine(Me.theGamePath, aRelativePath)
					If Not FileManager.PathExistsAfterTryToCreate(aPath) Then
						'TODO: [ExtractFolderTreeFromArchive] Path was not created, so warn user.
						'Me.UpdateProgressInternal(1, "")
						Dim debug As Integer = 4242
					End If
				End If
			Next
		End If

		Return status
	End Function

	Private Sub List()
		Dim archivePathFileName As String
		Dim archivePath As String = ""
		archivePathFileName = TheApp.Settings.UnpackPackagePathFolderOrFileName
		If File.Exists(archivePathFileName) Then
			archivePath = FileManager.GetPath(archivePathFileName)
		ElseIf Directory.Exists(archivePathFileName) Then
			archivePath = archivePathFileName
		End If

		If archivePath = "" Then
			Exit Sub
		End If

		Me.theArchivePathFileNameToFileDataMap = New SortedList(Of String, BasePackageFileData)()

		Try
			If TheApp.Settings.UnpackMode = InputOptions.FolderRecursion Then
				Me.ListArchivesInFolderRecursively(archivePath)
			ElseIf TheApp.Settings.UnpackMode = InputOptions.Folder Then
				Me.ListArchivesInFolder(archivePath)
			Else
				Me.ListArchive(archivePathFileName, True)
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ListArchivesInFolderRecursively(ByVal archivePath As String)
		Me.ListArchivesInFolder(archivePath)

		Try
			For Each aPathSubFolder As String In Directory.GetDirectories(archivePath)
				Me.ListArchivesInFolderRecursively(aPathSubFolder)

				If Me.CancellationPending Then
					Return
				End If
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ListArchivesInFolder(ByVal archivePath As String)
		Try
			Dim packageExtensions As List(Of String) = BasePackageFile.GetListOfPackageExtensions()
			For Each packageExtension As String In packageExtensions
				For Each anArchivePathFileName As String In Directory.GetFiles(archivePath, packageExtension)
					Me.ListArchive(anArchivePathFileName, False)

					If Me.CancellationPending Then
						Return
					End If
				Next
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	'checkForDirFile = true: Check if package file is valid. If not, check for a directory package file in same folder and open that instead.
	Private Sub ListArchive(ByVal packageDirectoryPathFileName As String, ByVal checkForDirFile As Boolean)
		Dim aPackageFileData As BasePackageFileData = Nothing

		Dim inputFileStream As FileStream = Nothing
		Me.theInputFileReader = Nothing
		Dim loopingIsNeeded As Boolean = True
		While loopingIsNeeded
			Try
				inputFileStream = New FileStream(packageDirectoryPathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
				If inputFileStream IsNot Nothing Then
					Try
						Me.theInputFileReader = New BinaryReader(inputFileStream, System.Text.Encoding.ASCII)

						Dim packageFile As BasePackageFile
						packageFile = BasePackageFile.Create(packageDirectoryPathFileName, Me.theArchiveDirectoryInputFileReader, Me.theInputFileReader, aPackageFileData)

						packageFile.ReadHeader()
						If aPackageFileData IsNot Nothing AndAlso aPackageFileData.IsSourcePackage Then
							Me.thePackageDirectoryPathFileName = packageDirectoryPathFileName
							Me.thePackageFileData = aPackageFileData
							Me.UpdateProgressInternal(1, "")
							AddHandler packageFile.PackEntryRead, AddressOf Me.Package_PackEntryRead
							packageFile.ReadEntries(Me)
							RemoveHandler packageFile.PackEntryRead, AddressOf Me.Package_PackEntryRead
							loopingIsNeeded = False
						ElseIf checkForDirFile AndAlso Path.GetExtension(packageDirectoryPathFileName) = ".vpk" Then
							'NOTE: Reaches this when user tries to list from a VPK file that is part of a multi-file package, but it is not the "dir" file.
							'NOTE: Set this to false to only check once for a package directory file.
							checkForDirFile = False

							Dim tempPath As String = FileManager.GetPath(packageDirectoryPathFileName)
							Dim tempFileName As String = Path.GetFileNameWithoutExtension(packageDirectoryPathFileName)
							Dim pos As Integer = tempFileName.LastIndexOf("_")
							If pos >= 0 Then
								Dim dirFileName As String = tempFileName.Remove(pos + 1) + "dir.vpk"
								packageDirectoryPathFileName = Path.Combine(tempPath, dirFileName)
							Else
								loopingIsNeeded = False
							End If
						Else
							loopingIsNeeded = False
						End If
					Catch ex As Exception
						Throw
					Finally
						If Me.theInputFileReader IsNot Nothing Then
							Me.theInputFileReader.Close()
						End If
					End Try
				End If
			Catch ex As Exception
				Throw
			Finally
				If inputFileStream IsNot Nothing Then
					inputFileStream.Close()
				End If
			End Try

			If Me.CancellationPending Then
				Return
			End If
		End While

		'If aPackageFileData IsNot Nothing AndAlso aPackageFileData.IsSourcePackage Then
		'	Dim entry As BasePackageDirectoryEntry
		'	Dim line As String
		'	Dim archivePathFileName As String
		'	Dim vpkPath As String
		'	Dim vpkFileNameWithoutExtension As String
		'	Dim vpkFileNamePrefix As String

		'	Me.UpdateProgressInternal(1, "")
		'	For i As Integer = 0 To aPackageFileData.theEntries.Count - 1
		'		entry = aPackageFileData.theEntries(i)
		'		If entry.archiveIndex <> &H7FFF Then
		'			vpkPath = FileManager.GetPath(packageDirectoryPathFileName)
		'			vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(packageDirectoryPathFileName)
		'			vpkFileNamePrefix = vpkFileNameWithoutExtension.Substring(0, vpkFileNameWithoutExtension.LastIndexOf(aPackageFileData.DirectoryFileNameSuffix))
		'			archivePathFileName = Path.Combine(vpkPath, vpkFileNamePrefix + "_" + entry.archiveIndex.ToString("000") + aPackageFileData.FileExtension)
		'		Else
		'			archivePathFileName = packageDirectoryPathFileName
		'		End If
		'		If Not Me.theArchivePathFileNameToFileDataMap.Keys.Contains(archivePathFileName) Then
		'			Me.theArchivePathFileNameToFileDataMap.Add(archivePathFileName, aPackageFileData)
		'		End If
		'		Me.UpdateProgressInternal(2, archivePathFileName)

		'		line = aPackageFileData.theEntryDataOutputTexts(i)
		'		Me.UpdateProgressInternal(3, line)

		'		If Me.CancellationPending Then
		'			Return
		'		End If
		'	Next
		'End If
	End Sub

	Private Sub Package_PackEntryRead(ByVal sender As Object, ByVal e As SourcePackageEventArgs)
		Dim entry As BasePackageDirectoryEntry
		Dim line As String
		Dim archivePathFileName As String
		Dim vpkPath As String
		Dim vpkFileNameWithoutExtension As String
		Dim vpkFileNamePrefix As String

		entry = e.Entry
		If entry.archiveIndex <> &H7FFF Then
			vpkPath = FileManager.GetPath(Me.thePackageDirectoryPathFileName)
			vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(Me.thePackageDirectoryPathFileName)
			vpkFileNamePrefix = vpkFileNameWithoutExtension.Substring(0, vpkFileNameWithoutExtension.LastIndexOf(Me.thePackageFileData.DirectoryFileNameSuffix))
			archivePathFileName = Path.Combine(vpkPath, vpkFileNamePrefix + "_" + entry.archiveIndex.ToString("000") + Me.thePackageFileData.FileExtension)
		Else
			archivePathFileName = Me.thePackageDirectoryPathFileName
		End If
		If Not Me.theArchivePathFileNameToFileDataMap.Keys.Contains(archivePathFileName) Then
			Me.theArchivePathFileNameToFileDataMap.Add(archivePathFileName, Me.thePackageFileData)
		End If
		Me.UpdateProgressInternal(2, archivePathFileName)
		If File.Exists(archivePathFileName) Then
			Me.UpdateProgressInternal(3, "True")
		Else
			Me.UpdateProgressInternal(3, "False")
		End If

		line = e.EntryDataOutputText
		Me.UpdateProgressInternal(4, line)

		If Me.CancellationPending Then
			Return
		End If
	End Sub

	Private Function Unpack(ByVal archivePathFileNameToEntryIndexMap As SortedList(Of String, List(Of Integer))) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theSkipCurrentPackIsActive = False

		Me.theUnpackedPaths.Clear()
		Me.theUnpackedRelativePathsAndFileNames.Clear()
		Me.theUnpackedMdlFiles.Clear()
		Me.theLogFiles.Clear()

		Me.theOutputPath = Me.GetOutputPath()
		Dim vpkPathFileName As String
		vpkPathFileName = TheApp.Settings.UnpackPackagePathFolderOrFileName
		If File.Exists(vpkPathFileName) Then
			Me.theInputVpkPath = FileManager.GetPath(vpkPathFileName)
		ElseIf Directory.Exists(vpkPathFileName) Then
			Me.theInputVpkPath = vpkPathFileName
		End If

		Dim progressDescriptionText As String
		progressDescriptionText = "Unpacking with " + TheApp.GetProductNameAndVersion() + ": "

		Try
			'If TheApp.Settings.UnpackMode = InputOptions.FolderRecursion Then
			'	progressDescriptionText += """" + Me.theInputVpkPath + """ (folder + subfolders)"
			'	Me.UpdateProgressStart(progressDescriptionText + " ...")

			'	status = Me.CreateLogTextFile("")

			'	Me.UnpackArchivesInFolderRecursively(Me.theInputVpkPath)
			'ElseIf TheApp.Settings.UnpackMode = InputOptions.Folder Then
			'	progressDescriptionText += """" + Me.theInputVpkPath + """ (folder)"
			'	Me.UpdateProgressStart(progressDescriptionText + " ...")

			'	status = Me.CreateLogTextFile("")

			'	Me.UnpackArchivesInFolder(Me.theInputVpkPath)
			'Else
			'	'vpkPathFileName = TheApp.Settings.UnpackVpkPathFileName
			'	progressDescriptionText += """" + vpkPathFileName + """"
			'	Me.UpdateProgressStart(progressDescriptionText + " ...")
			'	'Me.UnpackArchive(vpkPathFileName)
			'	Me.ExtractFromArchive(vpkPathFileName, Nothing)
			'End If
			'------
			progressDescriptionText += """" + vpkPathFileName + """"
			Me.UpdateProgressStart(progressDescriptionText + " ...")
			Me.ExtractFromArchive(vpkPathFileName, archivePathFileNameToEntryIndexMap)
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		If Me.CancellationPending Then
			Me.UpdateProgressStop("... " + progressDescriptionText + " canceled.")
		Else
			Me.UpdateProgressStop("... " + progressDescriptionText + " finished.")
		End If

		Return status
	End Function

	'Private Function Extract(ByVal archivePathFileNameToEntryIndexMap As SortedList(Of String, List(Of Integer))) As AppEnums.StatusMessage
	'	Dim status As AppEnums.StatusMessage = StatusMessage.Success

	'	Me.theSkipCurrentPackIsActive = False

	'	Me.theUnpackedPaths.Clear()
	'	Me.theUnpackedRelativePathFileNames.Clear()
	'	Me.theUnpackedMdlFiles.Clear()
	'	Me.theLogFiles.Clear()

	'	Me.theOutputPath = Me.GetAdjustedOutputPath()
	'	Dim vpkPathFileName As String
	'	vpkPathFileName = TheApp.Settings.UnpackPackagePathFolderOrFileName
	'	If File.Exists(vpkPathFileName) Then
	'		Me.theInputVpkPath = FileManager.GetPath(vpkPathFileName)
	'	ElseIf Directory.Exists(vpkPathFileName) Then
	'		Me.theInputVpkPath = vpkPathFileName
	'	End If

	'	Dim progressDescriptionText As String
	'	progressDescriptionText = "Unpacking with " + TheApp.GetProductNameAndVersion() + ": "

	'	Try
	'		'vpkPathFileName = TheApp.Settings.UnpackVpkPathFileName
	'		progressDescriptionText += """" + vpkPathFileName + """"
	'		Me.UpdateProgressStart(progressDescriptionText + " ...")
	'		'Me.ExtractFromArchive(vpkPathFileName, entries)
	'		Me.ExtractFromArchive(vpkPathFileName, archivePathFileNameToEntryIndexMap)
	'	Catch ex As Exception
	'		status = StatusMessage.Error
	'	End Try

	'	If Me.CancellationPending Then
	'		Me.UpdateProgressStop("... " + progressDescriptionText + " canceled.")
	'	Else
	'		Me.UpdateProgressStop("... " + progressDescriptionText + " finished.")
	'	End If

	'	Return status
	'End Function

	Private Function ExtractWithoutLogging(ByVal archivePathFileNameToEntryIndexMap As SortedList(Of String, List(Of Integer))) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theUnpackedPaths.Clear()
		Me.theUnpackedRelativePathsAndFileNames.Clear()
		Me.theUnpackedTempPathsAndPathFileNames.Clear()

		' Create and add a folder to the Temp path, to prevent potential file collisions and to provide user more obvious folder name.
		Dim guid As Guid
		guid = Guid.NewGuid()
		Dim folder As String
		folder = "Crowbar_" + guid.ToString()
		Me.theOutputPath = Path.Combine(Path.GetTempPath(), folder)
		Me.theUnpackedPathsAreInTempPath = True
		If Not FileManager.PathExistsAfterTryToCreate(Me.theOutputPath) Then
			Me.theRunSynchronousResultMessage = "WARNING: Tried to create """ + Me.theOutputPath + """ needed for extracting, but Windows did not allow it."
			status = StatusMessage.ErrorUnableToCreateTempFolder
			Return status
		End If

		'Dim vpkPathFileName As String
		'vpkPathFileName = TheApp.Settings.UnpackPackagePathFolderOrFileName

		Try
			Dim archivePathFileName As String
			Dim archiveEntryIndexes As List(Of Integer)

			Me.theArchiveDirectoryFileNamePrefix = ""
			For i As Integer = 0 To archivePathFileNameToEntryIndexMap.Count - 1
				archivePathFileName = archivePathFileNameToEntryIndexMap.Keys(i)
				archiveEntryIndexes = archivePathFileNameToEntryIndexMap.Values(i)

				Dim vpkPath As String
				Dim vpkFileName As String
				vpkPath = FileManager.GetPath(archivePathFileName)
				vpkFileName = Path.GetFileName(archivePathFileName)

				Me.OpenArchiveDirectoryFile(Me.theArchivePathFileNameToFileDataMap(archivePathFileName), archivePathFileName)
				Me.DoUnpackAction(Me.theArchivePathFileNameToFileDataMap(archivePathFileName), vpkPath, vpkFileName, archiveEntryIndexes)
			Next
			If Me.theArchiveDirectoryFileNamePrefix <> "" Then
				Me.CloseArchiveDirectoryFile()
			End If
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Return status
	End Function

	'Private Sub UnpackArchivesInFolderRecursively(ByVal archivePath As String)
	'	Me.UnpackArchivesInFolder(archivePath)

	'	For Each aPathSubFolder As String In Directory.GetDirectories(archivePath)
	'		Me.UnpackArchivesInFolderRecursively(aPathSubFolder)
	'		If Me.CancellationPending Then
	'			Return
	'		End If
	'	Next
	'End Sub

	'Private Sub UnpackArchivesInFolder(ByVal archivePath As String)
	'	For Each anArchivePathFileName As String In Directory.GetFiles(archivePath, "*.vpk")
	'		'Me.UnpackArchive(anArchivePathFileName)
	'		Me.ExtractFromArchive(anArchivePathFileName, Nothing)

	'		If Not Me.theSynchronousWorkerIsActive Then
	'			'TODO: Double-check if this is wanted. If so, then add equivalent to Decompiler.DecompileModelsInFolder().
	'			Me.ReportProgress(5, "")
	'		End If

	'		If Me.CancellationPending Then
	'			Return
	'		ElseIf Me.theSkipCurrentPackIsActive Then
	'			Me.theSkipCurrentPackIsActive = False
	'			Continue For
	'		End If
	'	Next
	'End Sub

	'Private Sub UnpackArchive(ByVal archivePathFileName As String)
	'	Try
	'		Dim vpkPath As String
	'		Dim vpkFileName As String
	'		Dim vpkRelativePath As String
	'		Dim vpkRelativePathFileName As String
	'		vpkPath = FileManager.GetPath(archivePathFileName)
	'		vpkFileName = Path.GetFileName(archivePathFileName)
	'		vpkRelativePath = FileManager.GetRelativePathFileName(Me.theInputVpkPath, FileManager.GetPath(archivePathFileName))
	'		vpkRelativePathFileName = Path.Combine(vpkRelativePath, vpkFileName)

	'		Dim vpkName As String
	'		vpkName = Path.GetFileNameWithoutExtension(archivePathFileName)

	'		Me.CreateLogTextFile(vpkName)

	'		Me.UpdateProgress()
	'		Me.UpdateProgress(1, "Unpacking """ + vpkRelativePathFileName + """ ...")

	'		Me.DoUnpackAction(vpkPath, vpkFileName, Nothing)

	'		If Me.CancellationPending Then
	'			Me.UpdateProgress(1, "... Unpacking """ + vpkRelativePathFileName + """ canceled. Check above for any errors.")
	'		Else
	'			Me.UpdateProgress(1, "... Unpacking """ + vpkRelativePathFileName + """ finished. Check above for any errors.")
	'		End If
	'	Catch ex As Exception
	'		Dim debug As Integer = 4242
	'	Finally
	'		If Me.theLogFileStream IsNot Nothing Then
	'			Me.theLogFileStream.Flush()
	'			Me.theLogFileStream.Close()
	'			Me.theLogFileStream = Nothing
	'		End If
	'	End Try
	'End Sub

	Private Function ExtractFromArchive(ByVal archiveDirectoryPathFileName As String, ByVal archivePathFileNameToEntryIndexMap As SortedList(Of String, List(Of Integer))) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			Dim vpkPath As String
			Dim vpkFileName As String
			Dim vpkRelativePath As String
			Dim vpkRelativePathFileName As String
			'vpkPath = FileManager.GetPath(archiveDirectoryPathFileName)
			vpkFileName = Path.GetFileName(archiveDirectoryPathFileName)
			vpkRelativePath = FileManager.GetRelativePathFileName(Me.theInputVpkPath, FileManager.GetPath(archiveDirectoryPathFileName))
			vpkRelativePathFileName = Path.Combine(vpkRelativePath, vpkFileName)

			Dim vpkName As String
			vpkName = Path.GetFileNameWithoutExtension(archiveDirectoryPathFileName)

			Dim vpkFileNameWithoutExtension As String
			vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(vpkFileName)
			'Dim extractPath As String
			'extractPath = Path.Combine(Me.theOutputPath, vpkFileNameWithoutExtension)

			'Me.CreateLogTextFile(vpkName)
			status = Me.CreateLogTextFile()

			Me.UpdateProgress()
			'Me.UpdateProgress(1, "Unpacking from """ + vpkRelativePathFileName + """ to """ + extractPath + """ ...")
			Me.UpdateProgress(1, "Unpacking from """ + vpkRelativePathFileName + """ to """ + Me.theOutputPath + """ ...")

			'Me.DoUnpackAction(Me.theVpkFileDatas.Values(0), vpkPath, vpkFileName, entryIndexes)
			'======
			'If archivePathFileNameToEntryIndexMap Is Nothing Then
			'	vpkPath = FileManager.GetPath(archiveDirectoryPathFileName)
			'	Me.DoUnpackAction(Me.theArchivePathFileNameToFileDataMap(archiveDirectoryPathFileName), vpkPath, vpkFileName, Nothing)
			'Else
			Dim archivePathFileName As String
			Dim archiveEntryIndexes As List(Of Integer)

			Me.theArchiveDirectoryFileNamePrefix = ""
			For i As Integer = 0 To archivePathFileNameToEntryIndexMap.Count - 1
				archivePathFileName = archivePathFileNameToEntryIndexMap.Keys(i)
				archiveEntryIndexes = archivePathFileNameToEntryIndexMap.Values(i)

				vpkPath = FileManager.GetPath(archivePathFileName)
				vpkFileName = Path.GetFileName(archivePathFileName)

				Me.OpenArchiveDirectoryFile(Me.theArchivePathFileNameToFileDataMap(archivePathFileName), archivePathFileName)
				Me.DoUnpackAction(Me.theArchivePathFileNameToFileDataMap(archivePathFileName), vpkPath, vpkFileName, archiveEntryIndexes)
			Next
			If Me.theArchiveDirectoryFileNamePrefix <> "" Then
				Me.CloseArchiveDirectoryFile()
			End If
			'End If

			If Me.CancellationPending Then
				Me.UpdateProgress(1, "... Unpacking from """ + vpkRelativePathFileName + """ canceled. Check above for any errors.")
			Else
				Me.UpdateProgress(1, "... Unpacking from """ + vpkRelativePathFileName + """ finished. Check above for any errors.")
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

		Return status
	End Function

	Private Sub OpenArchiveDirectoryFile(ByVal vpkFileData As BasePackageFileData, ByVal archivePathFileName As String)
		If vpkFileData Is Nothing Then
			Exit Sub
		End If
		If vpkFileData.DirectoryFileNameSuffix = "" Then
			Exit Sub
		End If

		Dim archiveDirectoryPathFileName As String
		Dim vpkFileNameWithoutExtension As String
		Dim vpkFileNamePrefix As String
		Dim underscoreIndex As Integer

		vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(archivePathFileName)
		underscoreIndex = vpkFileNameWithoutExtension.LastIndexOf("_")
		If underscoreIndex >= 0 Then
			vpkFileNamePrefix = vpkFileNameWithoutExtension.Substring(0, underscoreIndex)
		Else
			vpkFileNamePrefix = ""
		End If

		If vpkFileNamePrefix <> Me.theArchiveDirectoryFileNamePrefix Then
			Me.CloseArchiveDirectoryFile()

			Try
				Me.theArchiveDirectoryFileNamePrefix = vpkFileNamePrefix

				Dim vpkPath As String
				vpkPath = FileManager.GetPath(archivePathFileName)
				archiveDirectoryPathFileName = Path.Combine(vpkPath, vpkFileNamePrefix + vpkFileData.DirectoryFileNameSuffixWithExtension)

				If File.Exists(archiveDirectoryPathFileName) Then
					Me.theArchiveDirectoryInputFileStream = New FileStream(archiveDirectoryPathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
					If Me.theArchiveDirectoryInputFileStream IsNot Nothing Then
						Try
							Me.theArchiveDirectoryInputFileReader = New BinaryReader(Me.theArchiveDirectoryInputFileStream, System.Text.Encoding.ASCII)
						Catch ex As Exception
							Throw
						End Try
					End If
				End If
			Catch ex As Exception
				Me.CloseArchiveDirectoryFile()
				Throw
			End Try
		End If
	End Sub

	Private Sub CloseArchiveDirectoryFile()
		If Me.theArchiveDirectoryInputFileReader IsNot Nothing Then
			Me.theArchiveDirectoryInputFileReader.Close()
			Me.theArchiveDirectoryInputFileReader = Nothing
		End If
		If Me.theArchiveDirectoryInputFileStream IsNot Nothing Then
			Me.theArchiveDirectoryInputFileStream.Close()
			Me.theArchiveDirectoryInputFileStream = Nothing
		End If
	End Sub

	Private Sub DoUnpackAction(ByVal vpkFileData As BasePackageFileData, ByVal vpkPath As String, ByVal vpkFileName As String, ByVal entryIndexes As List(Of Integer))
		If vpkFileData Is Nothing Then
			Exit Sub
		End If

		'Dim currentPath As String
		'currentPath = Directory.GetCurrentDirectory()
		'Directory.SetCurrentDirectory(vpkPath)

		Dim entries As List(Of BasePackageDirectoryEntry)
		If entryIndexes Is Nothing Then
			entries = New List(Of BasePackageDirectoryEntry)(vpkFileData.theEntries.Count)
			For Each entry As BasePackageDirectoryEntry In vpkFileData.theEntries
				entries.Add(entry)
			Next
		Else
			entries = New List(Of BasePackageDirectoryEntry)(entryIndexes.Count)
			For Each entryIndex As Integer In entryIndexes
				entries.Add(vpkFileData.theEntries(entryIndex))
			Next
		End If

		Dim vpkPathFileName As String
		vpkPathFileName = Path.Combine(vpkPath, vpkFileName)
		Dim vpkFileNameWithoutExtension As String
		vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(vpkFileName)
		'Dim extractPath As String
		'extractPath = Path.Combine(Me.theOutputPath, vpkFileNameWithoutExtension)
		''TODO: Make this a unique folder so that its name is extremely unlikely to interfere with existing temp folders; maybe use a GUID at end. 
		'If Not Me.theTempUnpackPaths.Contains(extractPath) Then
		'	Me.theTempUnpackPaths.Add(extractPath)
		'End If

		'If vpkFileNameWithoutExtension.EndsWith("_dir") Then
		'	Dim vpkFileNameWithoutIndex As String
		'	vpkFileNameWithoutIndex = vpkFileNameWithoutExtension.Substring(0, vpkFileNameWithoutExtension.LastIndexOf("_dir"))

		'	For Each entry As VpkDirectoryEntry In entries
		'		If entry.archiveIndex <> &H7FFF Then
		'			vpkPathFileName = Path.Combine(vpkPath, vpkFileNameWithoutIndex + "_" + entry.archiveIndex.ToString("000") + ".vpk")
		'		End If
		'		Me.UnpackEntryDatasToFiles(vpkFileData, vpkPathFileName, entries, extractPath)
		'	Next
		'Else
		'Me.UnpackEntryDatasToFiles(vpkFileData, vpkPathFileName, entries, extractPath)
		Me.UnpackEntryDatasToFiles(vpkFileData, vpkPathFileName, entries)
		'End If

		'Directory.SetCurrentDirectory(currentPath)
	End Sub

	'Private Sub UnpackEntryDatasToFiles(ByVal vpkFileData As BasePackageFileData, ByVal vpkPathFileName As String, ByVal entries As List(Of VpkDirectoryEntry), ByVal extractPath As String)
	Private Sub UnpackEntryDatasToFiles(ByVal vpkFileData As BasePackageFileData, ByVal vpkPathFileName As String, ByVal entries As List(Of BasePackageDirectoryEntry))
		' Example: [03-Nov-2019] Left 4 Dead main multi-file VPK does not have a "pak01_048.vpk" file.
		If Not File.Exists(vpkPathFileName) Then
			Me.UpdateProgress(2, "WARNING: Package file not found - """ + vpkPathFileName + """. The following files are indicated as being in the missing package file: ")
			For Each entry As BasePackageDirectoryEntry In entries
				Me.UpdateProgress(3, """" + entry.thePathFileName + """")
			Next
			Exit Sub
		End If

		Dim inputFileStream As FileStream = Nothing
		Me.theInputFileReader = Nothing

		Try
			inputFileStream = New FileStream(vpkPathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
			If inputFileStream IsNot Nothing Then
				Try
					Me.theInputFileReader = New BinaryReader(inputFileStream, System.Text.Encoding.ASCII)

					'Dim packageDirectoryFileNameWithoutExtension As String = Path.GetFileNameWithoutExtension(vpkPathFileName)
					Dim packageDirectoryFileNameWithoutExtension As String = Me.GetPackageDirectoryFileNameWithoutExtension(vpkPathFileName, vpkFileData)

					'Dim vpkFile As New VpkFile(Me.theArchiveDirectoryInputFileReader, Me.theInputFileReader, vpkFileData)
					Dim vpkFile As BasePackageFile
					vpkFile = BasePackageFile.Create(vpkPathFileName, Me.theArchiveDirectoryInputFileReader, Me.theInputFileReader, vpkFileData)

					For Each entry As BasePackageDirectoryEntry In entries
						'Me.UnpackEntryDataToFile(vpkFile, entry, extractPath)
						Me.UnpackEntryDataToFile(packageDirectoryFileNameWithoutExtension, vpkFile, entry)

						If Me.CancellationPending Then
							Exit For
						End If
					Next
				Catch ex As Exception
					Throw
				Finally
					If Me.theInputFileReader IsNot Nothing Then
						Me.theInputFileReader.Close()
					End If
				End Try
			End If
		Catch ex As Exception
			Throw
		Finally
			If inputFileStream IsNot Nothing Then
				inputFileStream.Close()
			End If
		End Try
	End Sub

	'Private Sub UnpackEntryDataToFile(ByVal vpkFile As VpkFile, ByVal entry As VpkDirectoryEntry, ByVal extractPath As String)
	Private Sub UnpackEntryDataToFile(ByVal packageFileNameWithoutExtension As String, ByVal vpkFile As BasePackageFile, ByVal entry As BasePackageDirectoryEntry)
		Dim outputPathStart As String
		If TheApp.Settings.UnpackFolderForEachPackageIsChecked OrElse Me.theOutputPathIsExtendedWithPackageName Then
			outputPathStart = Path.Combine(Me.theOutputPath, packageFileNameWithoutExtension)
		Else
			outputPathStart = Me.theOutputPath
		End If

		Dim entryPathFileName As String
		If entry.thePathFileName.StartsWith("<") Then
			entryPathFileName = entry.theRealPathFileName
		Else
			entryPathFileName = entry.thePathFileName
		End If

		Dim outputPathFileName As String
		If TheApp.Settings.UnpackKeepFullPathIsChecked Then
			outputPathFileName = Path.Combine(outputPathStart, entryPathFileName)
		Else
			Dim entryRelativePathFileName As String = FileManager.GetRelativePathFileName(Me.theSelectedRelativeOutputPath, entryPathFileName)
			outputPathFileName = Path.Combine(outputPathStart, entryRelativePathFileName)
		End If

		Dim outputPath As String
		outputPath = FileManager.GetPath(outputPathFileName)

		If FileManager.PathExistsAfterTryToCreate(outputPath) Then
			vpkFile.UnpackEntryDataToFile(entry, outputPathFileName)
		End If

		If File.Exists(outputPathFileName) Then
			If Not Me.theUnpackedPaths.Contains(Me.theOutputPath) Then
				Me.theUnpackedPaths.Add(Me.theOutputPath)
			End If
			Me.theUnpackedRelativePathsAndFileNames.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, outputPathFileName))
			'If Not Me.theUnpackedTempPathsAndPathFileNames.Contains(entry.thePathFileName) Then
			'	Me.theUnpackedTempPathsAndPathFileNames.Add(entry.thePathFileName)
			'End If
			If Path.GetExtension(outputPathFileName) = ".mdl" Then
				Me.theUnpackedMdlFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, outputPathFileName))
			End If

			If entry.thePathFileName.StartsWith("<") Then
				Me.UpdateProgress(2, "Unpacked: """ + entry.thePathFileName + """ as """ + entry.theRealPathFileName + """")
			Else
				Me.UpdateProgress(2, "Unpacked: " + entry.thePathFileName)
			End If
		Else
			Me.UpdateProgress(2, "WARNING: Not unpacked: " + entry.thePathFileName)
		End If
	End Sub

	'Private Sub StartFile(ByVal packInternalPathFileName As String)
	'	Dim tempUnpackRelativePathFileName As String
	'	Dim pathFileName As String
	'	tempUnpackRelativePathFileName = Path.Combine(Me.theTempUnpackPaths(0), packInternalPathFileName)
	'	pathFileName = Me.GetOutputPathFileName(tempUnpackRelativePathFileName)

	'	System.Diagnostics.Process.Start(pathFileName)
	'End Sub
	Private Sub StartFile(ByVal pathFileName As String)
		System.Diagnostics.Process.Start(pathFileName)
	End Sub

	Private Function GetPackageDirectoryFileNameWithoutExtension(ByVal archivePathFileName As String, ByVal vpkFileData As BasePackageFileData) As String
		Dim packageDirectoryFileNameWithoutExtension As String = ""

		Dim vpkFileNameWithoutExtension As String
		Dim vpkFileNamePrefix As String
		Dim underscoreIndex As Integer
		vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(archivePathFileName)

		packageDirectoryFileNameWithoutExtension = vpkFileNameWithoutExtension
		If vpkFileData.DirectoryFileNameSuffix <> "" Then
			underscoreIndex = vpkFileNameWithoutExtension.LastIndexOf("_")
			If underscoreIndex >= 0 Then
				vpkFileNamePrefix = vpkFileNameWithoutExtension.Substring(0, underscoreIndex)
				Dim packageDirectoryPathFileName As String = Path.Combine(FileManager.GetPath(archivePathFileName), vpkFileNamePrefix + vpkFileData.DirectoryFileNameSuffix + vpkFileData.FileExtension)
				If File.Exists(packageDirectoryPathFileName) Then
					packageDirectoryFileNameWithoutExtension = vpkFileNamePrefix + vpkFileData.DirectoryFileNameSuffix
				End If
			End If
		End If

		Return packageDirectoryFileNameWithoutExtension
	End Function

#End Region

#Region "Data"

	Private theSkipCurrentPackIsActive As Boolean
	Private theSynchronousWorkerIsActive As Boolean
	Private theRunSynchronousResultMessage As String
	Private theInputVpkPath As String
	Private theOutputPath As String
	Private theOutputPathOrModelOutputFileName As String
	Private theOutputPathIsExtendedWithPackageName As Boolean
	Private theSelectedRelativeOutputPath As String

	Private theLogFileStream As StreamWriter
	Private theLastLine As String

	'TODO: Not currently used for anything but for drag-drop temp path deleteion.
	Private theUnpackedPaths As List(Of String)
	'NOTE: Extra guard against deleting non-temp paths from accidental bad coding.
	Private theUnpackedPathsAreInTempPath As Boolean
	' Used for listing unpacked files in combobox.
	Private theUnpackedRelativePathsAndFileNames As BindingListEx(Of String)
	'TODO: Not currently used for anything.
	Private theUnpackedTempPathsAndPathFileNames As List(Of String)
	Private theUnpackedMdlFiles As BindingListEx(Of String)
	Private theLogFiles As BindingListEx(Of String)

	'Private theTempUnpackPaths As List(Of String)

	'Private theVpkFileData As BasePackageFileData
	Private theArchivePathFileNameToFileDataMap As SortedList(Of String, BasePackageFileData)
	Private theArchiveDirectoryFileNamePrefix As String
	Private theArchiveDirectoryInputFileStream As FileStream
	Private theArchiveDirectoryInputFileReader As BinaryReader
	Private theInputFileReader As BinaryReader

	Private thePackageDirectoryPathFileName As String
	Private thePackageFileData As BasePackageFileData

	Private theGamePath As String

#End Region

End Class
