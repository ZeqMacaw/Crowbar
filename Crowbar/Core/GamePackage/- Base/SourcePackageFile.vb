Imports System.ComponentModel
Imports System.IO

Public MustInherit Class SourcePackageFile

#Region "Shared"

	'Public Shared Function Create(ByVal packagePathFileName As String, ByVal packageDirectoryFileReader As BinaryReader, ByVal packageFileReader As BinaryReader, ByRef packageFileData As BasePackageFileData) As BasePackageFile
	'	Dim packageFile As BasePackageFile = Nothing

	'	Try
	'		Dim extension As String
	'		extension = Path.GetExtension(packagePathFileName)

	'		If extension = ".apk" Then
	'			If packageFileData Is Nothing Then
	'				packageFileData = New ApkFileData()
	'			End If
	'			packageFile = New ApkFile(packageFileReader, CType(packageFileData, ApkFileData))
	'		ElseIf extension = ".fpx" Or extension = ".vpk" Then
	'			If packageFileData Is Nothing Then
	'				packageFileData = New VpkFileData()
	'			End If
	'			'packageFile = New VpkFile(packageDirectoryFileReader, packageFileReader, CType(packageFileData, VpkFileData))
	'		ElseIf extension = ".gma" Then
	'			If packageFileData Is Nothing Then
	'				packageFileData = New GmaFileData()
	'			End If
	'			packageFile = New GmaFile(packageFileReader, CType(packageFileData, GmaFileData))
	'		ElseIf extension = ".hfs" Then
	'			If packageFileData Is Nothing Then
	'				packageFileData = New HfsFileData()
	'			End If
	'			packageFile = New HfsFile(packageFileReader, CType(packageFileData, HfsFileData))
	'		Else
	'			' Package not implemented.
	'			packageFileData = Nothing
	'			packageFile = Nothing
	'		End If
	'	Catch ex As Exception
	'		Throw
	'	End Try

	'	Return packageFile
	'End Function

	'Public Shared Function Create(ByVal packagePathFileName As String, ByVal packageFileReader As BufferedBinaryReader, ByRef packageFileData As BasePackageFileData) As BasePackageFile
	'	Dim packageFile As BasePackageFile = Nothing

	'	Try
	'		Dim extension As String
	'		extension = Path.GetExtension(packagePathFileName)

	'		If extension = ".apk" Then
	'			If packageFileData Is Nothing Then
	'				packageFileData = New ApkFileData()
	'			End If
	'			'packageFile = New ApkFile(packageDirectoryFileReader, packageFileReader, CType(packageFileData, ApkFileData))
	'		ElseIf extension = ".fpx" Or extension = ".vpk" Then
	'			If packageFileData Is Nothing Then
	'				packageFileData = New VpkFileData()
	'			End If
	'			packageFile = New VpkFile(packageDirectoryFileReader, packageFileReader, CType(packageFileData, VpkFileData))
	'		ElseIf extension = ".gma" Then
	'			If packageFileData Is Nothing Then
	'				packageFileData = New GmaFileData()
	'			End If
	'			'packageFile = New GmaFile(packageDirectoryFileReader, packageFileReader, CType(packageFileData, GmaFileData))
	'		ElseIf extension = ".hfs" Then
	'			If packageFileData Is Nothing Then
	'				packageFileData = New HfsFileData()
	'			End If
	'			'packageFile = New HfsFile(packageDirectoryFileReader, packageFileReader, CType(packageFileData, HfsFileData))
	'		Else
	'			' Package not implemented.
	'			packageFileData = Nothing
	'			packageFile = Nothing
	'		End If
	'	Catch ex As Exception
	'		Throw
	'	End Try

	'	Return packageFile
	'End Function

	'Public Shared Function GetListOfPackageExtensions() As List(Of String)
	'	Dim packageExtensions As New List(Of String)
	'	packageExtensions.Add("*.apk")
	'	packageExtensions.Add("*.fpx")
	'	packageExtensions.Add("*.gma")
	'	'packageExtensions.Add("*.hfs")
	'	packageExtensions.Add("*.vpk")
	'	Return packageExtensions
	'End Function

#End Region

#Region "Methods"

	Public MustOverride Sub ReadHeader()
	Public MustOverride Sub ReadEntries()
	Public MustOverride Sub UnpackEntryDataToFile(ByVal iEntry As SourcePackageDirectoryEntry, ByVal outputPathFileName As String)

#End Region

#Region "Events"

	'Public Delegate Sub PackEntryReadEventHandler(ByVal sender As Object, ByVal e As SourcePackageEventArgs)
	'Public Event PackEntryRead As PackEntryReadEventHandler
	''Protected Sub NotifyPackEntryRead(ByVal entry As BasePackageDirectoryEntry, ByVal entryDataOutputText As String)
	''	RaiseEvent PackEntryRead(Me, New SourcePackageEventArgs(entry, entryDataOutputText.ToString()))
	''End Sub
	'Protected Sub NotifyPackEntryRead(ByVal entry As BasePackageDirectoryEntry)
	'	RaiseEvent PackEntryRead(Me, New SourcePackageEventArgs(entry))
	'End Sub

#End Region

End Class
