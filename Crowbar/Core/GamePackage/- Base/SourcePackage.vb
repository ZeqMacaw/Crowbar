Imports System.ComponentModel
Imports System.IO

Public Class SourcePackage

#Region "Shared"

	Public Shared Function Create(ByVal packagePathFileName As String) As SourcePackage
		Dim package As SourcePackage = Nothing

		Try
			Dim extension As String
			extension = Path.GetExtension(packagePathFileName)

			If extension = ".apk" Then
				package = New SourcePackageApk(packagePathFileName)
			ElseIf extension = ".fpx" Or extension = ".vpk" Then
				package = New SourcePackageVpk(packagePathFileName)
			ElseIf extension = ".gma" Then
				package = New SourcePackageGma(packagePathFileName)
			ElseIf extension = ".hfs" Then
				'package = New SourcePackageHfs(packagePathFileName)
			Else
				' Package not implemented.
				package = Nothing
			End If
		Catch ex As Exception
			Throw
		End Try

		Return package
	End Function

	Public Shared Function GetListOfPackageExtensions() As List(Of String)
		Dim packageExtensions As New List(Of String)
		packageExtensions.Add("*.apk")
		packageExtensions.Add("*.fpx")
		packageExtensions.Add("*.gma")
		'packageExtensions.Add("*.hfs")
		packageExtensions.Add("*.vpk")
		Return packageExtensions
	End Function

#End Region

#Region "Creation and Destruction"

	Protected Sub New(ByVal packagePathFileName As String)
		Me.thePackagePathFileName = packagePathFileName
	End Sub

#End Region

#Region "Properties"

	Public Overridable ReadOnly Property DirectoryPathFileName() As String
		Get
			Return Me.thePackagePathFileName
		End Get
	End Property

#End Region

#Region "Methods"

	Public Overridable Function GetEntries() As List(Of SourcePackageDirectoryEntry)
		Try
			Me.ReadFile(Me.thePackagePathFileName, AddressOf Me.GetEntries_Internal)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		Return Me.theEntries
	End Function

	Public Overridable Sub UnpackEntryDatasToFiles(ByVal entries As List(Of SourcePackageDirectoryEntry), ByVal outputPath As String, ByVal selectedRelativeOutputPath As String)
		Try
			Me.theEntries = entries
			Me.theOutputPath = outputPath
			Me.theSelectedRelativeOutputPath = selectedRelativeOutputPath
			Me.ReadFile(Me.theEntries(0).PackageDataPathFileName, AddressOf Me.UnpackEntryDatasToFiles_Internal)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

#End Region

#Region "Protected Methods"

	Protected Overridable Sub GetEntries_Internal()
	End Sub

	Protected Overridable Sub UnpackEntryDatasToFiles_Internal()
	End Sub

	Protected Sub ReadFile(ByVal pathFileName As String, ByVal readFileAction As ReadFileDelegate)
		Dim inputFileStream As FileStream = Nothing
		Me.thePackageFileReader = Nothing
		Try
			inputFileStream = New FileStream(pathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
			If inputFileStream IsNot Nothing Then
				Try
					' Always set the encoding, to make explicit and not rely on default that could change.
					' Never use Text.Encoding.Default because it depends on context.
					' Text.Encoding.ASCII does not correctly read in non-English letters.
					' Works for Windows system locale set to English or Japanese.
					' Example VPK where this matters: Left 4 Dead 2 "left4dead2_dlc3\pak01_dir.vpk"
					'    File name in VPK that raises exception on Japanese but not English when opening the VPK for listing (because of accented e): "materials/dons_decals/serviet bugé.vmt"
					Me.thePackageFileReader = New BufferedBinaryReader(inputFileStream, System.Text.Encoding.GetEncoding(1252))
					' Does not work.
					'Me.thePackageFileReader = New BufferedBinaryReader(inputFileStream, System.Text.Encoding.UTF8)
					' Other possibilities if GetEncoding(1252) does not work for something.
					'Me.thePackageFileReader = New BufferedBinaryReader(inputFileStream, System.Text.Encoding.GetEncoding(437))
					'Me.thePackageFileReader = New BufferedBinaryReader(inputFileStream, System.Text.Encoding.GetEncoding(28591))

					readFileAction.Invoke()
				Catch ex As Exception
					Throw
				Finally
					If Me.thePackageFileReader IsNot Nothing Then
						Me.thePackageFileReader.Close()
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

#Region "Private Delegates"

	Protected Delegate Sub ReadFileDelegate()

#End Region

#Region "Data"

	Protected thePackagePathFileName As String
	Protected thePackageFileReader As BufferedBinaryReader
	'Protected thePackageFileData As BasePackageFileData
	Protected theEntries As List(Of SourcePackageDirectoryEntry)
	Protected theOutputPath As String
	Protected theSelectedRelativeOutputPath As String

#End Region

#Region "Progress Event"

	Public Event Progress As ProgressEventHandler
	Public Delegate Sub ProgressEventHandler(ByVal sender As Object, ByVal e As ProgressEventArgs)

	Public Class ProgressEventArgs
		Inherits System.EventArgs

		Public Sub New(ByVal progress As ProgressOptions, ByVal message As String, ByVal entry As SourcePackageDirectoryEntry)
			MyBase.New()

			Me.theProgress = progress
			Me.theMessage = message
			Me.theEntry = entry
		End Sub

		Public ReadOnly Property Progress As ProgressOptions
			Get
				Return Me.theProgress
			End Get
		End Property

		Public ReadOnly Property Message As String
			Get
				Return Me.theMessage
			End Get
		End Property

		Public ReadOnly Property Entry As SourcePackageDirectoryEntry
			Get
				Return Me.theEntry
			End Get
		End Property

		Private theProgress As ProgressOptions
		Private theMessage As String
		Private theEntry As SourcePackageDirectoryEntry

	End Class

	Protected Sub NotifyProgress(ByVal progressOptions As ProgressOptions, ByVal message As String, ByVal entry As SourcePackageDirectoryEntry)
		RaiseEvent Progress(Me, New ProgressEventArgs(progressOptions, message, entry))
	End Sub

#End Region

End Class
