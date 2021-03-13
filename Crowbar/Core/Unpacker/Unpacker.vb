Imports System.ComponentModel
Imports System.IO

Public Class Unpacker
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.theUnpackedPaths = New List(Of String)()
		Me.theUnpackedRelativePathsAndFileNames = New BindingListEx(Of String)()
		Me.theUnpackedMdlFiles = New BindingListEx(Of String)()
		Me.theLogFiles = New BindingListEx(Of String)()

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

	Public Sub Run(ByVal unpackerAction As PackageAction, ByVal packagePathFileNameToEntryIndexesMap As SortedList(Of String, List(Of Integer)), ByVal outputPathIsExtendedWithPackageName As Boolean, ByVal selectedRelativeOutputPath As String)
		Me.theSynchronousWorkerIsActive = False
		Dim info As New UnpackerInputInfo()
		info.thePackageAction = unpackerAction
		info.thePackagePathFileNameToEntryIndexesMap = packagePathFileNameToEntryIndexesMap
		info.theOutputPathIsExtendedWithPackageName = outputPathIsExtendedWithPackageName
		info.theSelectedRelativeOutputPath = selectedRelativeOutputPath
		Me.RunWorkerAsync(info)
	End Sub

	Public Function RunSynchronous(ByVal unpackerAction As PackageAction, ByVal packagePathFileNameToEntryIndexesMap As SortedList(Of String, List(Of Integer)), ByVal outputPathIsExtendedWithPackageName As Boolean, ByVal selectedRelativeOutputPath As String) As String
		Me.theSynchronousWorkerIsActive = True
		Dim info As New UnpackerInputInfo()
		info.thePackageAction = unpackerAction
		info.thePackagePathFileNameToEntryIndexesMap = packagePathFileNameToEntryIndexesMap
		info.theOutputPathIsExtendedWithPackageName = outputPathIsExtendedWithPackageName
		info.theSelectedRelativeOutputPath = selectedRelativeOutputPath

		Me.theRunSynchronousResultMessage = ""
		Dim e As New System.ComponentModel.DoWorkEventArgs(info)
		Me.OnDoWork(e)
		Return Me.theRunSynchronousResultMessage
	End Function

	Public Sub UnpackFolderTreeFromGame(ByVal gamePath As String)
		Me.theSynchronousWorkerIsActive = True
		Dim info As New UnpackerInputInfo()
		info.thePackageAction = PackageAction.UnpackFolderTree
		info.theGamePath = gamePath
		Dim e As New System.ComponentModel.DoWorkEventArgs(info)
		Me.OnDoWork(e)
	End Sub

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
		If info.thePackageAction = PackageAction.UnpackFolderTree Then
			status = Me.UnpackFolderTree(info.theGamePath)
		Else
			If Me.UnpackerInputsAreValid() Then
				If info.thePackageAction = PackageAction.List Then
					Me.List()
				ElseIf info.thePackageAction = PackageAction.Unpack Then
					status = Me.Unpack(info.thePackagePathFileNameToEntryIndexesMap)
				ElseIf info.thePackageAction = PackageAction.UnpackAndOpen Then
					status = Me.UnpackWithoutLogging(info.thePackagePathFileNameToEntryIndexesMap)
					If status = StatusMessage.Success Then
						Me.StartFile(Path.Combine(Me.theOutputPath, Me.theUnpackedRelativePathsAndFileNames(0)))
					End If
				ElseIf info.thePackageAction = PackageAction.UnpackToTemp Then
					status = Me.UnpackWithoutLogging(info.thePackagePathFileNameToEntryIndexesMap)
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

	Private Sub CloseLogTextFile()
		If Me.theLogFileStream IsNot Nothing Then
			Me.theLogFileStream.Flush()
			Me.theLogFileStream.Close()
			Me.theLogFileStream = Nothing
		End If
	End Sub

	Private Sub UpdateProgressInternal(ByVal progressValue As Integer, ByVal line As String)
		If progressValue = 1 AndAlso Me.theLogFileStream IsNot Nothing Then
			Me.theLogFileStream.WriteLine(line)
			Me.theLogFileStream.Flush()
		End If

		If Not Me.theSynchronousWorkerIsActive Then
			Me.ReportProgress(progressValue, line)
		End If
	End Sub

	Private Function UnpackFolderTree(ByVal gamePath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		' Example: 
		'      Me.theGamePath = gamePath = "E:\Users\ZeqMacaw\Steam\steamapps\common\Half-Life 2\hl2"
		'      gameRootPath              = "E:\Users\ZeqMacaw\Steam\steamapps\common\Half-Life 2"
		Me.theGamePath = gamePath
		Dim gameRootPath As String = FileManager.GetPath(gamePath)

		Try
			Me.UnpackFolderTreeFromPackagesInFolderRecursively(gameRootPath)
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Return status
	End Function

	Private Function UnpackFolderTreeFromPackagesInFolderRecursively(ByVal packagePath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.UnpackFolderTreeFromPackagesInFolder(packagePath)

		Try
			For Each aPathSubFolder As String In Directory.GetDirectories(packagePath)
				Me.UnpackFolderTreeFromPackagesInFolderRecursively(aPathSubFolder)

				If Me.CancellationPending Then
					Return StatusMessage.Canceled
				End If
			Next
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Return status
	End Function

	Private Function UnpackFolderTreeFromPackagesInFolder(ByVal packagePath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			'NOTE: Feature only valid for VPK files.
			Dim packageFileNameFilter As String = "*.vpk"
			For Each aPackagePathFileName As String In Directory.GetFiles(packagePath, packageFileNameFilter)
				Me.UnpackFolderTreeFromPackage(aPackagePathFileName)

				If Me.CancellationPending Then
					Return StatusMessage.Canceled
				End If
			Next
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Return status
	End Function

	Private Function UnpackFolderTreeFromPackage(ByVal packagePathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim aPackageFileData As BasePackageFileData = Nothing

		Dim inputFileStream As FileStream = Nothing
		Me.theInputFileReader = Nothing
		Try
			inputFileStream = New FileStream(packagePathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
			If inputFileStream IsNot Nothing Then
				Try
					Me.theInputFileReader = New BinaryReader(inputFileStream, System.Text.Encoding.ASCII)

					Dim packageFile As BasePackageFile
					packageFile = BasePackageFile.Create(packagePathFileName, Me.thePackageDirectoryInputFileReader, Me.theInputFileReader, aPackageFileData)

					packageFile.ReadHeader()
					packageFile.ReadEntries(Me)
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
			'Dim entry As BasePackageDirectoryEntry
			Dim line As String
			'Dim archivePathFileName As String
			'Dim packagePath As String
			'Dim packageFileNameWithoutExtension As String
			'Dim packageFileNamePrefix As String
			Dim paths As New List(Of String)()

			For i As Integer = 0 To aPackageFileData.theEntries.Count - 1
				'entry = aPackageFileData.theEntries(i)
				'If entry.archiveIndex <> &H7FFF Then
				'	packagePath = FileManager.GetPath(packagePathFileName)
				'	packageFileNameWithoutExtension = Path.GetFileNameWithoutExtension(packagePathFileName)
				'	packageFileNamePrefix = packageFileNameWithoutExtension.Substring(0, packageFileNameWithoutExtension.LastIndexOf(aPackageFileData.DirectoryFileNameSuffix))
				'	archivePathFileName = Path.Combine(packagePath, packageFileNamePrefix + "_" + entry.archiveIndex.ToString("000") + aPackageFileData.FileExtension)
				'Else
				'	archivePathFileName = packagePathFileName
				'End If

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
						'TODO: [UnpackFolderTreeFromPackage] Path was not created, so warn user.
						'Me.UpdateProgressInternal(1, "")
						Dim debug As Integer = 4242
					End If
				End If
			Next
		End If

		Return status
	End Function

	Private Sub List()
		Dim packagePathFileName As String
		Dim packagePath As String = ""
		packagePathFileName = TheApp.Settings.UnpackPackagePathFolderOrFileName
		If File.Exists(packagePathFileName) Then
			packagePath = FileManager.GetPath(packagePathFileName)
		ElseIf Directory.Exists(packagePathFileName) Then
			packagePath = packagePathFileName
		End If

		If packagePath = "" Then
			Exit Sub
		End If

		Me.thePackagePathFileNameToFileDataMap = New SortedList(Of String, BasePackageFileData)()

		Try
			If TheApp.Settings.UnpackMode = InputOptions.FolderRecursion Then
				Me.ListPackagesInFolderRecursively(packagePath)
			ElseIf TheApp.Settings.UnpackMode = InputOptions.Folder Then
				Me.ListPackagesInFolder(packagePath)
			Else
				Me.ListPackage(packagePathFileName, True)
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ListPackagesInFolderRecursively(ByVal packagePath As String)
		Me.ListPackagesInFolder(packagePath)

		Try
			For Each aPathSubFolder As String In Directory.GetDirectories(packagePath)
				Me.ListPackagesInFolderRecursively(aPathSubFolder)

				If Me.CancellationPending Then
					Return
				End If
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ListPackagesInFolder(ByVal packagePath As String)
		Try
			Dim packageExtensions As List(Of String) = BasePackageFile.GetListOfPackageExtensions()
			For Each packageExtension As String In packageExtensions
				For Each anPackagePathFileName As String In Directory.GetFiles(packagePath, packageExtension)
					Me.ListPackage(anPackagePathFileName, False)

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
	Private Sub ListPackage(ByVal packageDirectoryPathFileName As String, ByVal checkForDirFile As Boolean)
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
						packageFile = BasePackageFile.Create(packageDirectoryPathFileName, Me.thePackageDirectoryInputFileReader, Me.theInputFileReader, aPackageFileData)

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
	End Sub

	Private Sub Package_PackEntryRead(ByVal sender As Object, ByVal e As SourcePackageEventArgs)
		Dim entry As BasePackageDirectoryEntry
		Dim line As String
		Dim packagePathFileName As String
		Dim packagePath As String
		Dim packageFileNameWithoutExtension As String
		Dim packageFileNamePrefix As String

		entry = e.Entry
		If entry.archiveIndex <> &H7FFF Then
			packagePath = FileManager.GetPath(Me.thePackageDirectoryPathFileName)
			packageFileNameWithoutExtension = Path.GetFileNameWithoutExtension(Me.thePackageDirectoryPathFileName)
			packageFileNamePrefix = packageFileNameWithoutExtension.Substring(0, packageFileNameWithoutExtension.LastIndexOf(Me.thePackageFileData.DirectoryFileNameSuffix))
			packagePathFileName = Path.Combine(packagePath, packageFileNamePrefix + "_" + entry.archiveIndex.ToString("000") + Me.thePackageFileData.FileExtension)
		Else
			packagePathFileName = Me.thePackageDirectoryPathFileName
		End If
		If Not Me.thePackagePathFileNameToFileDataMap.Keys.Contains(packagePathFileName) Then
			Me.thePackagePathFileNameToFileDataMap.Add(packagePathFileName, Me.thePackageFileData)
		End If
		Me.UpdateProgressInternal(2, packagePathFileName)
		If File.Exists(packagePathFileName) Then
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

	Private Function Unpack(ByVal packagePathFileNameToEntryIndexMap As SortedList(Of String, List(Of Integer))) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theSkipCurrentPackIsActive = False

		Me.theUnpackedPaths.Clear()
		Me.theUnpackedRelativePathsAndFileNames.Clear()
		Me.theUnpackedMdlFiles.Clear()
		Me.theLogFiles.Clear()

		Me.theOutputPath = Me.GetOutputPath()

		Dim packagePathFileName As String
		packagePathFileName = TheApp.Settings.UnpackPackagePathFolderOrFileName
		If File.Exists(packagePathFileName) Then
			Me.theInputPackagePath = FileManager.GetPath(packagePathFileName)
		ElseIf Directory.Exists(packagePathFileName) Then
			Me.theInputPackagePath = packagePathFileName
		End If

		Dim progressDescriptionText As String
		progressDescriptionText = "Unpacking with " + TheApp.GetProductNameAndVersion() + ": "

		Try
			progressDescriptionText += """" + packagePathFileName + """"
			Me.UpdateProgressStart(progressDescriptionText + " ...")
			Me.UnpackFromPackage(packagePathFileName, packagePathFileNameToEntryIndexMap)
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

	Private Function UnpackFromPackage(ByVal packageDirectoryPathFileName As String, ByVal packagePathFileNameToEntryIndexMap As SortedList(Of String, List(Of Integer))) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			Dim packagePath As String
			Dim packageFileName As String
			Dim packageRelativePath As String
			Dim packageRelativePathFileName As String
			packageFileName = Path.GetFileName(packageDirectoryPathFileName)
			packageRelativePath = FileManager.GetRelativePathFileName(Me.theInputPackagePath, FileManager.GetPath(packageDirectoryPathFileName))
			packageRelativePathFileName = Path.Combine(packageRelativePath, packageFileName)

			status = Me.CreateLogTextFile()

			Me.UpdateProgress()
			Me.UpdateProgress(1, "Unpacking from """ + packageRelativePathFileName + """ to """ + Me.theOutputPath + """ ...")

			Dim packagePathFileName As String
			Dim packageEntryIndexes As List(Of Integer)

			Me.thePackageDirectoryFileNamePrefix = ""
			For i As Integer = 0 To packagePathFileNameToEntryIndexMap.Count - 1
				packagePathFileName = packagePathFileNameToEntryIndexMap.Keys(i)
				packageEntryIndexes = packagePathFileNameToEntryIndexMap.Values(i)

				packagePath = FileManager.GetPath(packagePathFileName)
				packageFileName = Path.GetFileName(packagePathFileName)

				Me.OpenPackageDirectoryFile(Me.thePackagePathFileNameToFileDataMap(packagePathFileName), packagePathFileName)
				Me.DoUnpackAction(Me.thePackagePathFileNameToFileDataMap(packagePathFileName), packagePath, packageFileName, packageEntryIndexes)
			Next
			If Me.thePackageDirectoryFileNamePrefix <> "" Then
				Me.ClosePackageDirectoryFile()
			End If

			If Me.CancellationPending Then
				Me.UpdateProgress(1, "... Unpacking from """ + packageRelativePathFileName + """ canceled. Check above for any errors.")
			Else
				Me.UpdateProgress(1, "... Unpacking from """ + packageRelativePathFileName + """ finished. Check above for any errors.")
			End If
		Catch ex As Exception
			status = StatusMessage.Error
		Finally
			Me.CloseLogTextFile()
		End Try

		Return status
	End Function

	Private Function UnpackWithoutLogging(ByVal packagePathFileNameToEntryIndexMap As SortedList(Of String, List(Of Integer))) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theUnpackedPaths.Clear()
		Me.theUnpackedRelativePathsAndFileNames.Clear()
		Me.theUnpackedMdlFiles.Clear()

		' Create and add a folder to the Temp path, to prevent potential file collisions and to provide user more obvious folder name.
		Dim guid As Guid
		guid = Guid.NewGuid()
		Dim folder As String
		folder = "Crowbar_" + guid.ToString()
		Me.theOutputPath = Path.Combine(Path.GetTempPath(), folder)

		Me.theUnpackedPathsAreInTempPath = True
		If Not FileManager.PathExistsAfterTryToCreate(Me.theOutputPath) Then
			Me.theRunSynchronousResultMessage = "ERROR: Tried to create """ + Me.theOutputPath + """ needed for unpacking, but Windows did not allow it."
			status = StatusMessage.ErrorUnableToCreateTempFolder
			Return status
		End If

		Try
			Dim packagePathFileName As String
			Dim packageEntryIndexes As List(Of Integer)
			Dim packagePath As String
			Dim packageFileName As String

			Me.thePackageDirectoryFileNamePrefix = ""
			For i As Integer = 0 To packagePathFileNameToEntryIndexMap.Count - 1
				packagePathFileName = packagePathFileNameToEntryIndexMap.Keys(i)
				packageEntryIndexes = packagePathFileNameToEntryIndexMap.Values(i)

				packagePath = FileManager.GetPath(packagePathFileName)
				packageFileName = Path.GetFileName(packagePathFileName)

				Me.OpenPackageDirectoryFile(Me.thePackagePathFileNameToFileDataMap(packagePathFileName), packagePathFileName)
				Me.DoUnpackAction(Me.thePackagePathFileNameToFileDataMap(packagePathFileName), packagePath, packageFileName, packageEntryIndexes)
			Next
			If Me.thePackageDirectoryFileNamePrefix <> "" Then
				Me.ClosePackageDirectoryFile()
			End If
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Return status
	End Function

	Private Sub OpenPackageDirectoryFile(ByVal packageFileData As BasePackageFileData, ByVal packagePathFileName As String)
		If packageFileData Is Nothing Then
			Exit Sub
		End If
		If packageFileData.DirectoryFileNameSuffix = "" Then
			Exit Sub
		End If

		Dim packageDirectoryPathFileName As String
		Dim packageFileNameWithoutExtension As String
		Dim packageFileNamePrefix As String
		Dim underscoreIndex As Integer

		packageFileNameWithoutExtension = Path.GetFileNameWithoutExtension(packagePathFileName)
		underscoreIndex = packageFileNameWithoutExtension.LastIndexOf("_")
		If underscoreIndex >= 0 Then
			packageFileNamePrefix = packageFileNameWithoutExtension.Substring(0, underscoreIndex)
		Else
			packageFileNamePrefix = ""
		End If

		If packageFileNamePrefix <> Me.thePackageDirectoryFileNamePrefix Then
			Me.ClosePackageDirectoryFile()

			Try
				Me.thePackageDirectoryFileNamePrefix = packageFileNamePrefix

				Dim packagePath As String
				packagePath = FileManager.GetPath(packagePathFileName)
				packageDirectoryPathFileName = Path.Combine(packagePath, packageFileNamePrefix + packageFileData.DirectoryFileNameSuffixWithExtension)

				If File.Exists(packageDirectoryPathFileName) Then
					Me.thePackageDirectoryInputFileStream = New FileStream(packageDirectoryPathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
					If Me.thePackageDirectoryInputFileStream IsNot Nothing Then
						Try
							Me.thePackageDirectoryInputFileReader = New BinaryReader(Me.thePackageDirectoryInputFileStream, System.Text.Encoding.ASCII)
						Catch ex As Exception
							Throw
						End Try
					End If
				End If
			Catch ex As Exception
				Me.ClosePackageDirectoryFile()
				Throw
			End Try
		End If
	End Sub

	Private Sub ClosePackageDirectoryFile()
		If Me.thePackageDirectoryInputFileReader IsNot Nothing Then
			Me.thePackageDirectoryInputFileReader.Close()
			Me.thePackageDirectoryInputFileReader = Nothing
		End If
		If Me.thePackageDirectoryInputFileStream IsNot Nothing Then
			Me.thePackageDirectoryInputFileStream.Close()
			Me.thePackageDirectoryInputFileStream = Nothing
		End If
	End Sub

	Private Sub DoUnpackAction(ByVal packageFileData As BasePackageFileData, ByVal packagePath As String, ByVal packageFileName As String, ByVal entryIndexes As List(Of Integer))
		If packageFileData Is Nothing Then
			Exit Sub
		End If

		Dim entries As List(Of BasePackageDirectoryEntry)
		If entryIndexes Is Nothing Then
			entries = New List(Of BasePackageDirectoryEntry)(packageFileData.theEntries.Count)
			For Each entry As BasePackageDirectoryEntry In packageFileData.theEntries
				entries.Add(entry)
			Next
		Else
			entries = New List(Of BasePackageDirectoryEntry)(entryIndexes.Count)
			For Each entryIndex As Integer In entryIndexes
				entries.Add(packageFileData.theEntries(entryIndex))
			Next
		End If

		Dim packagePathFileName As String
		packagePathFileName = Path.Combine(packagePath, packageFileName)
		Dim packageFileNameWithoutExtension As String
		packageFileNameWithoutExtension = Path.GetFileNameWithoutExtension(packageFileName)

		Me.UnpackEntryDatasToFiles(packageFileData, packagePathFileName, entries)
	End Sub

	Private Sub UnpackEntryDatasToFiles(ByVal packageFileData As BasePackageFileData, ByVal packagePathFileName As String, ByVal entries As List(Of BasePackageDirectoryEntry))
		' Example: [03-Nov-2019] Left 4 Dead main multi-file VPK does not have a "pak01_048.vpk" file.
		If Not File.Exists(packagePathFileName) Then
			Me.UpdateProgress(2, "WARNING: Package file not found - """ + packagePathFileName + """. The following files are indicated as being in the missing package file: ")
			For Each entry As BasePackageDirectoryEntry In entries
				Me.UpdateProgress(3, """" + entry.thePathFileName + """")
			Next
			Exit Sub
		End If

		Dim inputFileStream As FileStream = Nothing
		Me.theInputFileReader = Nothing

		Try
			inputFileStream = New FileStream(packagePathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
			If inputFileStream IsNot Nothing Then
				Try
					Me.theInputFileReader = New BinaryReader(inputFileStream, System.Text.Encoding.ASCII)

					Dim targetFolder As String = Me.GetPackageDirectoryFileNameWithoutExtension(packagePathFileName, packageFileData)

					Dim packageFile As BasePackageFile
					packageFile = BasePackageFile.Create(packagePathFileName, Me.thePackageDirectoryInputFileReader, Me.theInputFileReader, packageFileData)

					For Each entry As BasePackageDirectoryEntry In entries
						Me.UnpackEntryDataToFile(targetFolder, packageFile, entry)

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

	Private Sub UnpackEntryDataToFile(ByVal targetFolder As String, ByVal packageFile As BasePackageFile, ByVal entry As BasePackageDirectoryEntry)
		Dim outputPathStart As String
		If Me.theOutputPathIsExtendedWithPackageName Then
			outputPathStart = Path.Combine(Me.theOutputPath, targetFolder)
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
			packageFile.UnpackEntryDataToFile(entry, outputPathFileName)
		End If

		If File.Exists(outputPathFileName) Then
			If Not Me.theUnpackedPaths.Contains(Me.theOutputPath) Then
				Me.theUnpackedPaths.Add(Me.theOutputPath)
			End If

			Dim relativePathFileName As String = ""
			relativePathFileName = FileManager.GetRelativePathFileName(Me.theOutputPath, outputPathFileName)
			Me.theUnpackedRelativePathsAndFileNames.Add(relativePathFileName)

			If Path.GetExtension(outputPathFileName) = ".mdl" Then
				Me.theUnpackedMdlFiles.Add(relativePathFileName)
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

	Private Function GetPackageDirectoryFileNameWithoutExtension(ByVal PackagePathFileName As String, ByVal packageFileData As BasePackageFileData) As String
		Dim packageDirectoryFileNameWithoutExtension As String = ""

		Dim packageFileNameWithoutExtension As String
		Dim packageFileNamePrefix As String
		Dim underscoreIndex As Integer
		packageFileNameWithoutExtension = Path.GetFileNameWithoutExtension(PackagePathFileName)

		packageDirectoryFileNameWithoutExtension = packageFileNameWithoutExtension
		If packageFileData.DirectoryFileNameSuffix <> "" Then
			underscoreIndex = packageFileNameWithoutExtension.LastIndexOf("_")
			If underscoreIndex >= 0 Then
				packageFileNamePrefix = packageFileNameWithoutExtension.Substring(0, underscoreIndex)
				Dim packageDirectoryPathFileName As String = Path.Combine(FileManager.GetPath(PackagePathFileName), packageFileNamePrefix + packageFileData.DirectoryFileNameSuffix + packageFileData.FileExtension)
				If File.Exists(packageDirectoryPathFileName) Then
					packageDirectoryFileNameWithoutExtension = packageFileNamePrefix + packageFileData.DirectoryFileNameSuffix
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
	Private theInputPackagePath As String
	Private theOutputPath As String
	Private theOutputPathOrModelOutputFileName As String
	Private theOutputPathIsExtendedWithPackageName As Boolean
	Private theSelectedRelativeOutputPath As String

	Private theLogFileStream As StreamWriter
	Private theLastLine As String

	' Used for drag-drop temp path deleteion.
	Private theUnpackedPaths As List(Of String)
	'NOTE: Extra guard against deleting non-temp paths from accidental bad coding.
	Private theUnpackedPathsAreInTempPath As Boolean
	' Used for listing unpacked files in combobox.
	Private theUnpackedRelativePathsAndFileNames As BindingListEx(Of String)
	Private theUnpackedMdlFiles As BindingListEx(Of String)
	Private theLogFiles As BindingListEx(Of String)

	Private thePackagePathFileNameToFileDataMap As SortedList(Of String, BasePackageFileData)
	Private thePackageDirectoryFileNamePrefix As String
	Private thePackageDirectoryInputFileStream As FileStream
	Private thePackageDirectoryInputFileReader As BinaryReader
	Private theInputFileReader As BinaryReader

	Private thePackageDirectoryPathFileName As String
	Private thePackageFileData As BasePackageFileData

	Private theGamePath As String

#End Region

End Class
