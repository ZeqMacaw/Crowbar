Imports System.ComponentModel
Imports System.IO

Public Class SourcePackageVpk
	Inherits SourcePackage

#Region "Creation and Destruction"

	Public Sub New(ByVal packagePathFileName As String)
		MyBase.New(packagePathFileName)

		Me.theVpkFile = Nothing
		Me.SetDirectoryPathFileName()
	End Sub

#End Region

#Region "Methods"

	'Public Overrides Sub UnpackEntryDatasToFiles(ByVal entries As List(Of BasePackageDirectoryEntry), ByVal outputPath As String, ByVal selectedRelativeOutputPath As String)
	'	Try
	'		Me.theEntries = entries
	'		Me.theOutputPath = outputPath
	'		Me.theSelectedRelativeOutputPath = selectedRelativeOutputPath
	'		Me.ReadFile(Me.thePackagePathFileName, AddressOf Me.UnpackEntryDatasToFiles_Internal)
	'	Catch ex As Exception
	'		Dim debug As Integer = 4242
	'	End Try
	'End Sub

#End Region

#Region "Private Methods"

	Private Sub SetDirectoryPathFileName()
		Try
			Me.ReadFile(Me.thePackagePathFileName, AddressOf Me.SetDirectoryPathFileName_Internal)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub SetDirectoryPathFileName_Internal()
		If Me.theVpkFileData Is Nothing Then
			Me.theVpkFileData = New VpkFileData()
		End If

		Dim vpkFile As New VpkFile(Me.thePackageFileReader, Nothing, Me.theVpkFileData)

		vpkFile.ReadHeader()
		If Me.theVpkFileData IsNot Nothing AndAlso Not Me.theVpkFileData.IsSourcePackage Then
			Dim tempPath As String = FileManager.GetPath(Me.thePackagePathFileName)
			Dim tempFileName As String = Path.GetFileNameWithoutExtension(Me.thePackagePathFileName)
			Dim pos As Integer = tempFileName.LastIndexOf("_")
			If pos >= 0 Then
				Dim dirFileName As String = tempFileName.Remove(pos + 1) + "dir.vpk"
				'TODO: Get all VPK file names that contain package path name that end with "_dir.vpk".
				'      Currently just gets the first one. Not sure how to get the other ones read.
				If dirFileName.StartsWith("client") Then
					For Each aPackagePathFileName As String In Directory.GetFiles(tempPath, "*" + dirFileName)
						dirFileName = aPackagePathFileName
						Exit For
					Next
				End If
				Me.thePackagePathFileName = Path.Combine(tempPath, dirFileName)
			End If
		End If
	End Sub

	Protected Overrides Sub GetEntries_Internal()
		If Me.theVpkFileData Is Nothing Then
			Me.theVpkFileData = New VpkFileData()
		End If

		Dim vpkFile As New VpkFile(Me.thePackageFileReader, Nothing, Me.theVpkFileData)

		vpkFile.ReadHeader()
		If Me.theVpkFileData IsNot Nothing AndAlso Me.theVpkFileData.IsSourcePackage Then
			vpkFile.ReadEntries()
			Me.ProcessEntries()
		End If

		Me.theEntries = Me.theVpkFileData.theEntries
	End Sub

	Protected Sub GetEntriesFromMultiFileVpk_Internal()
		If Me.theVpkFileData Is Nothing Then
			Me.theVpkFileData = New VpkFileData()
		End If

		Dim vpkFile As New VpkFile(Me.thePackageFileReader, Nothing, Me.theVpkFileData)

		vpkFile.ReadHeader()
		If Me.theVpkFileData IsNot Nothing AndAlso Me.theVpkFileData.IsSourcePackage Then
			vpkFile.ReadEntries()
			Me.ProcessEntries()
		End If

		Me.theEntries = Me.theVpkFileData.theEntries
	End Sub

	Private Sub ProcessEntries()
		For Each entry As VpkDirectoryEntry In Me.theVpkFileData.theEntries
			Dim packagePath As String
			Dim packageFileNameWithoutExtension As String
			Dim packageFileNamePrefix As String
			Dim packageDataPathFileName As String

			If entry.multiFilePackageFileIndex <> &H7FFF Then
				packagePath = FileManager.GetPath(Me.thePackagePathFileName)
				packageFileNameWithoutExtension = Path.GetFileNameWithoutExtension(Me.thePackagePathFileName)
				packageFileNamePrefix = packageFileNameWithoutExtension.Substring(0, packageFileNameWithoutExtension.LastIndexOf(Me.theVpkFileData.DirectoryFileNameSuffix))
				' The version = 196610 is used by Titanfall and Titanfall 2.
				If Me.theVpkFileData.version = 196610 Then
					' Remove up to the word "client".
					Dim lowercaseEntryPath As String = packageFileNamePrefix.ToLower()
					Dim positionOfClientText As Integer = lowercaseEntryPath.IndexOf("client")
					If positionOfClientText > -1 Then
						packageFileNamePrefix = packageFileNamePrefix.Remove(0, positionOfClientText)
					End If
				End If
				packageDataPathFileName = Path.Combine(packagePath, packageFileNamePrefix + "_" + entry.multiFilePackageFileIndex.ToString("000") + Me.theVpkFileData.FileExtension)
			Else
				packageDataPathFileName = Me.thePackagePathFileName
			End If
			entry.PackageDirPathFileName = Me.thePackagePathFileName
			entry.PackageDataPathFileName = packageDataPathFileName
			If entry.PackageDataPathFileName = "E:\Games\Titanfall\vpk\client_mp_airbase.bsp.pak000_004.vpk" Then
				Dim debug As Integer = 4242
			End If
			entry.PackageDataPathFileNameExists = File.Exists(packageDataPathFileName)
		Next
	End Sub

	Protected Overrides Sub UnpackEntryDatasToFiles_Internal()
		Try
			Me.ReadDirFile(CType(Me.theEntries(0), VpkDirectoryEntry).PackageDirPathFileName, AddressOf Me.UnpackEntryDatasToFiles_Internal2)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Protected Sub UnpackEntryDatasToFiles_Internal2()
		If Me.theEntries.Count > 0 Then
			If Me.theVpkFileData Is Nothing Then
				Me.theVpkFileData = New VpkFileData()
			End If
			Me.theVpkFile = New VpkFile(Me.thePackageDirFileReader, Me.thePackageFileReader, Me.theVpkFileData)

			Dim outputPathStart As String
			If TheApp.Settings.UnpackFolderForEachPackageIsChecked Then
				Dim targetFolder As String = Path.GetFileNameWithoutExtension(Me.thePackagePathFileName)
				outputPathStart = Path.Combine(Me.theOutputPath, targetFolder)
			Else
				outputPathStart = Me.theOutputPath
			End If

			For Each entry As SourcePackageDirectoryEntry In Me.theEntries
				Dim entryPathFileName As String = entry.DisplayPathFileName

				Dim outputPathFileName As String
				If TheApp.Settings.UnpackKeepFullPathIsChecked Then
					outputPathFileName = Path.Combine(outputPathStart, entryPathFileName)
				Else
					Dim entryRelativePathFileName As String = FileManager.GetRelativePathFileName(Me.theSelectedRelativeOutputPath, entryPathFileName)
					outputPathFileName = Path.Combine(outputPathStart, entryRelativePathFileName)
				End If

				Dim outputPath As String = FileManager.GetPath(outputPathFileName)

				If FileManager.PathExistsAfterTryToCreate(outputPath) Then
					Me.theVpkFile.UnpackEntryDataToFile(entry, outputPathFileName)
					Me.NotifyProgress(ProgressOptions.WritingFileFinished, outputPathFileName, entry)
				End If
			Next
		End If
	End Sub

	Protected Sub ReadDirFile(ByVal pathFileName As String, ByVal readFileAction As ReadFileDelegate)
		Dim inputFileStream As FileStream = Nothing
		Me.thePackageDirFileReader = Nothing
		Try
			inputFileStream = New FileStream(pathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
			If inputFileStream IsNot Nothing Then
				Try
					' Always set the encoding, to make explicit and not rely on default that could change.
					' Never use Text.Encoding.Default because it depends on context.
					' Text.Encoding.ASCII does not correctly read in non-English letters.
					' Works for Windows system locale set to English or Japanese.
					Me.thePackageDirFileReader = New BufferedBinaryReader(inputFileStream, System.Text.Encoding.GetEncoding(1252))
					' Does not work.
					'Me.thePackageDirFileReader = New BufferedBinaryReader(inputFileStream, System.Text.Encoding.UTF8)
					' Other possibilities if GetEncoding(1252) does not work for something.
					'Me.thePackageDirFileReader = New BufferedBinaryReader(inputFileStream, System.Text.Encoding.GetEncoding(437))
					'Me.thePackageDirFileReader = New BufferedBinaryReader(inputFileStream, System.Text.Encoding.GetEncoding(28591))

					readFileAction.Invoke()
				Catch ex As Exception
					Throw
				Finally
					If Me.thePackageDirFileReader IsNot Nothing Then
						Me.thePackageDirFileReader.Close()
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

#End Region

#Region "Data"

	Private theVpkFileData As VpkFileData
	Private theVpkFile As VpkFile
	'Private thePackageDirectoryPathFileName As String
	Protected thePackageDirFileReader As BufferedBinaryReader

#End Region

End Class
