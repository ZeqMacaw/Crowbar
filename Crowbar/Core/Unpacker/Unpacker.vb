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

	Public Sub Run(ByVal unpackerAction As PackageAction, ByVal packagePathFileNameToEntriesMap As SortedList(Of String, List(Of SourcePackageDirectoryEntry)), ByVal outputPathIsExtendedWithPackageName As Boolean, ByVal selectedRelativeOutputPath As String)
		Me.theSynchronousWorkerIsActive = False
		Dim info As New UnpackerInputInfo()
		info.thePackageAction = unpackerAction
		info.thePackagePathFileNameToEntriesMap = packagePathFileNameToEntriesMap
		info.theOutputPathIsExtendedWithPackageName = outputPathIsExtendedWithPackageName
		info.theSelectedRelativeOutputPath = selectedRelativeOutputPath
		Me.RunWorkerAsync(info)
	End Sub

	Public Function RunSynchronous(ByVal unpackerAction As PackageAction, ByVal packagePathFileNameToEntriesMap As SortedList(Of String, List(Of SourcePackageDirectoryEntry)), ByVal outputPathIsExtendedWithPackageName As Boolean, ByVal selectedRelativeOutputPath As String) As String
		Me.theSynchronousWorkerIsActive = True
		Dim info As New UnpackerInputInfo()
		info.thePackageAction = unpackerAction
		info.thePackagePathFileNameToEntriesMap = packagePathFileNameToEntriesMap
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
		Me.theSkipCurrentPackageIsActive = True
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
					status = StatusMessage.Success
				ElseIf info.thePackageAction = PackageAction.Unpack Then
					status = Me.UnpackWithLogging(info.thePackagePathFileNameToEntriesMap)
				ElseIf info.thePackageAction = PackageAction.UnpackToTemp Then
					status = Me.UnpackToTempWithoutLogging(info.thePackagePathFileNameToEntriesMap)
				ElseIf info.thePackageAction = PackageAction.UnpackToTempAndOpen Then
					status = Me.UnpackToTempWithoutLogging(info.thePackagePathFileNameToEntriesMap)
					If status = StatusMessage.Success Then
						Me.StartFile(Path.Combine(Me.theOutputPath, Me.theUnpackedRelativePathsAndFileNames(0)))
					End If
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

		unpackResultInfo.entries = Me.thePackageEntries

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

	Private Sub StartLog(ByVal packagePathFileName As String, ByRef packageRelativePathFileName As String)
		Dim logFileStatus As AppEnums.StatusMessage = Me.CreateLogTextFile()

		Dim inputPackagePath As String = ""
		If File.Exists(packagePathFileName) Then
			inputPackagePath = FileManager.GetPath(packagePathFileName)
		ElseIf Directory.Exists(packagePathFileName) Then
			inputPackagePath = packagePathFileName
		End If

		Dim packageFileName As String
		Dim packageRelativePath As String
		packageFileName = Path.GetFileName(packagePathFileName)
		packageRelativePath = FileManager.GetRelativePathFileName(inputPackagePath, FileManager.GetPath(packagePathFileName))
		packageRelativePathFileName = Path.Combine(packageRelativePath, packageFileName)

		Me.UpdateProgress()
		Me.UpdateProgress(1, "Unpacking from """ + packageRelativePathFileName + """ to """ + Me.theOutputPath + """ ...")
	End Sub

	Private Sub StopLog(ByVal status As AppEnums.StatusMessage, ByVal packageRelativePathFileName As String)
		If status <> StatusMessage.Error Then
			If Me.CancellationPending Then
				Me.UpdateProgress(1, "... Unpacking from """ + packageRelativePathFileName + """ canceled. Check above for any errors.")
			Else
				Me.UpdateProgress(1, "... Unpacking from """ + packageRelativePathFileName + """ finished. Check above for any errors.")
			End If
		Else
			Me.UpdateProgress(1, "... Unpacking from """ + packageRelativePathFileName + """ failed. Check above for any errors.")
		End If

		Me.CloseLogTextFile()
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

		Dim package As SourcePackage = Nothing
		Try
			package = SourcePackage.Create(packagePathFileName)
			Me.thePackageEntries = package.GetEntries()
			For Each entry As SourcePackageDirectoryEntry In Me.thePackageEntries
				'NOTE: Only need to create "models" folder-tree to have models accessible in HLMV.
				If entry.DisplayPathFileName.StartsWith("models/") Then
					Dim aRelativePath As String = FileManager.GetPath(entry.DisplayPathFileName)
					Dim aPath As String = Path.Combine(Me.theGamePath, aRelativePath)
					If Not FileManager.PathExistsAfterTryToCreate(aPath) Then
						'TODO: [UnpackFolderTreeFromPackage] Path was not created, so warn user.
						'Me.UpdateProgressInternal(1, "")
						Dim debug As Integer = 4242
					End If
				End If
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

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

		Try
			Me.thePackageDirectoryPathFileNamesAlreadyProcessed = New SortedSet(Of String)()

			Me.thePackageEntries = New List(Of SourcePackageDirectoryEntry)()
			If TheApp.Settings.UnpackMode = InputOptions.FolderRecursion Then
				Me.ListPackagesInFolderRecursively(packagePath)
			ElseIf TheApp.Settings.UnpackMode = InputOptions.Folder Then
				Me.ListPackagesInFolder(packagePath)
			Else
				Me.ListPackage(packagePathFileName)
			End If

			Me.thePackageDirectoryPathFileNamesAlreadyProcessed = Nothing
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
			Dim packageExtensions As List(Of String) = SourcePackage.GetListOfPackageExtensions()
			For Each packageExtension As String In packageExtensions
				For Each aPackagePathFileName As String In Directory.GetFiles(packagePath, packageExtension)
					Me.ListPackage(aPackagePathFileName)

					If Me.CancellationPending Then
						Return
					End If
				Next
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ListPackage(ByVal packagePathFileName As String)
		Dim package As SourcePackage = Nothing
		Try
			package = SourcePackage.Create(packagePathFileName)
			' Avoid processing a multi-file package more than once.
			If Not Me.thePackageDirectoryPathFileNamesAlreadyProcessed.Contains(package.DirectoryPathFileName) Then
				Me.thePackageDirectoryPathFileNamesAlreadyProcessed.Add(package.DirectoryPathFileName)
				Me.thePackageEntries.AddRange(package.GetEntries())
				Me.UpdateProgressInternal(1, "")
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Function UnpackWithLogging(ByVal packagePathFileNameToEntriesMap As SortedList(Of String, List(Of SourcePackageDirectoryEntry))) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theSkipCurrentPackageIsActive = False

		Me.theUnpackedPaths.Clear()
		Me.theUnpackedRelativePathsAndFileNames.Clear()
		Me.theUnpackedMdlFiles.Clear()
		Me.theLogFiles.Clear()

		Me.theOutputPath = Me.GetOutputPath()

		Dim progressDescriptionText As String
		progressDescriptionText = "Unpacking with " + TheApp.GetProductNameAndVersion() + ": "

		Dim packageRelativePathFileName As String = Nothing
		Try
			Dim packagePathFileName As String
			packagePathFileName = TheApp.Settings.UnpackPackagePathFolderOrFileName

			progressDescriptionText += """" + packagePathFileName + """"
			Me.UpdateProgressStart(progressDescriptionText + " ...")

			Me.StartLog(packagePathFileName, packageRelativePathFileName)

			status = Me.UnpackFromPackages(packagePathFileNameToEntriesMap)
		Catch ex As Exception
			status = StatusMessage.Error
		Finally
			Me.StopLog(status, packageRelativePathFileName)
		End Try

		If Me.CancellationPending Then
			Me.UpdateProgressStop("... " + progressDescriptionText + " canceled.")
		Else
			Me.UpdateProgressStop("... " + progressDescriptionText + " finished.")
		End If

		Return status
	End Function

	Private Function UnpackToTempWithoutLogging(ByVal packagePathFileNameToEntriesMap As SortedList(Of String, List(Of SourcePackageDirectoryEntry))) As AppEnums.StatusMessage
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
			status = Me.UnpackFromPackages(packagePathFileNameToEntriesMap)
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Return status
	End Function

	Private Function UnpackFromPackages(ByVal packagePathFileNameToEntriesMap As SortedList(Of String, List(Of SourcePackageDirectoryEntry))) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			For i As Integer = 0 To packagePathFileNameToEntriesMap.Count - 1
				Dim package As SourcePackage = SourcePackage.Create(packagePathFileNameToEntriesMap.Keys(i))
				AddHandler package.Progress, AddressOf Me.Package_Progress
				package.UnpackEntryDatasToFiles(packagePathFileNameToEntriesMap.Values(i), Me.theOutputPath, Me.theSelectedRelativeOutputPath)
				RemoveHandler package.Progress, AddressOf Me.Package_Progress
			Next
		Catch ex As Exception
			status = StatusMessage.Error
			'Finally
		End Try

		Return status
	End Function

	Private Sub StartFile(ByVal pathFileName As String)
		System.Diagnostics.Process.Start(pathFileName)
	End Sub

#Region "Event Handlers"

	Private Sub Package_Progress(ByVal sender As Object, ByVal e As SourcePackage.ProgressEventArgs)
		If e.Progress = ProgressOptions.WritingFileFailed Then
			Me.UpdateProgress(4, e.Message)
		ElseIf e.Progress = ProgressOptions.WritingFileFinished Then
			Dim entry As SourcePackageDirectoryEntry = e.Entry
			Dim outputPathFileName As String = e.Message
			Dim fileName As String = Path.GetFileName(outputPathFileName)
			Me.UpdateProgress(4, fileName)

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

				If entry.DisplayPathFileName.StartsWith("<") Then
					Me.UpdateProgress(2, "Unpacked: """ + entry.DisplayPathFileName + """ as """ + entry.PathFileName + """")
				Else
					Me.UpdateProgress(2, "Unpacked: " + entry.DisplayPathFileName)
				End If
			Else
				Me.UpdateProgress(2, "WARNING: Not unpacked: " + entry.DisplayPathFileName)
			End If
		Else
			Dim progressUnhandled As Integer = 4242
		End If
	End Sub

#End Region

#End Region

#Region "Data"

	Private theSkipCurrentPackageIsActive As Boolean
	Private theSynchronousWorkerIsActive As Boolean
	Private theRunSynchronousResultMessage As String
	Private theOutputPath As String
	Private theOutputPathOrModelOutputFileName As String
	Private theOutputPathIsExtendedWithPackageName As Boolean
	Private theSelectedRelativeOutputPath As String

	Private theLogFileStream As StreamWriter

	' Used for drag-drop temp path deleteion.
	Private theUnpackedPaths As List(Of String)
	'NOTE: Extra guard against deleting non-temp paths from accidental bad coding.
	Private theUnpackedPathsAreInTempPath As Boolean
	' Used for listing unpacked files in combobox.
	Private theUnpackedRelativePathsAndFileNames As BindingListEx(Of String)
	Private theUnpackedMdlFiles As BindingListEx(Of String)
	Private theLogFiles As BindingListEx(Of String)

	Private thePackageDirectoryPathFileNamesAlreadyProcessed As SortedSet(Of String)
	Private thePackageEntries As List(Of SourcePackageDirectoryEntry)

	Private theGamePath As String

#End Region

End Class
