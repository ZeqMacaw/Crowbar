Imports System.IO

Public Class SourcePackageApk
	Inherits SourcePackage

#Region "Creation and Destruction"

	Public Sub New(ByVal packagePathFileName As String)
		MyBase.New(packagePathFileName)
	End Sub

#End Region

#Region "Methods"

#End Region

#Region "Private Methods"

	Protected Overrides Sub GetEntries_Internal()
		If Me.theApkFileData Is Nothing Then
			Me.theApkFileData = New ApkFileData()
		End If

		Dim packageFile As New ApkFile(Me.thePackageFileReader, Me.theApkFileData)

		packageFile.ReadHeader()
		If Me.theApkFileData IsNot Nothing AndAlso Me.theApkFileData.IsSourcePackage Then
			packageFile.ReadEntries()
			Me.ProcessEntries()
		End If

		Me.theEntries = Me.theApkFileData.theEntries
	End Sub

	Private Sub ProcessEntries()
		For Each entry As SourcePackageDirectoryEntry In Me.theApkFileData.theEntries
			entry.PackageDataPathFileName = Me.thePackagePathFileName
			entry.PackageDataPathFileNameExists = File.Exists(entry.PackageDataPathFileName)
		Next
	End Sub

	Protected Overrides Sub UnpackEntryDatasToFiles_Internal()
		If Me.theApkFileData Is Nothing Then
			Me.theApkFileData = New ApkFileData()
		End If

		Dim packageFile As New ApkFile(Me.thePackageFileReader, Me.theApkFileData)

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
				packageFile.UnpackEntryDataToFile(entry, outputPathFileName)
				Me.NotifyProgress(ProgressOptions.WritingFileFinished, outputPathFileName, entry)
			End If
		Next
	End Sub

#End Region

#Region "Data"

	Private theApkFileData As ApkFileData

#End Region

End Class
