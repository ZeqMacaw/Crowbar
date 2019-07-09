Imports System.IO

Public Class SourceModel38
	Inherits SourceModel37

#Region "Creation and Destruction"

	Public Sub New(ByVal mdlPathFileName As String, ByVal mdlVersion As Integer)
		MyBase.New(mdlPathFileName, mdlVersion)
	End Sub

#End Region

#Region "Properties"

	Public Overrides ReadOnly Property VvdFileIsUsed As Boolean
		Get
			Return Not Me.theMdlFileData.theMdlFileOnlyHasAnimations
		End Get
	End Property

#End Region

#Region "Methods"

	'Public Overrides Function ReadAniFile(ByVal mdlPathFileName As String) As AppEnums.StatusMessage
	'	Dim status As AppEnums.StatusMessage = StatusMessage.Success
	'	Dim aniPathFileName As String

	'	aniPathFileName = Path.ChangeExtension(mdlPathFileName, ".ani")
	'	If File.Exists(aniPathFileName) Then
	'		Dim aniFile As SourceAniFile
	'		aniFile = New SourceAniFile()
	'		Try
	'			'TODO: Instead of assuming ANI file is beside the MDL file, use the actual path for ANI file found in the MDL file.
	'			'      These lines should be moved to the caller of this function.
	'			''NOTE: The ani file name is stored in the mdl file header.
	'			''Example from L4D2 v_models\v_molotov.mdl:
	'			''models/v_models/anim_v_molotov.ani
	'			'inputPathFileName = FileManager.GetPath(mdlPathFileName)
	'			'inputPathFileName = Path.Combine(inputPathFileName, Path.GetFileName(mdlFileData.theAnimBlockRelativePathFileName))
	'			'If Not File.Exists(inputPathFileName) Then
	'			'	Return
	'			'End If

	'			Me.ReadFile(aniPathFileName, AddressOf Me.ReadAniFile)
	'		Catch ex As Exception
	'			status = StatusMessage.Error
	'		End Try
	'	End If

	'	Return status
	'End Function

	'Public Overrides Function ReadVvdFile(ByVal mdlPathFileName As String) As AppEnums.StatusMessage
	'	Dim status As AppEnums.StatusMessage = StatusMessage.Success
	'	Dim vvdPathFileName As String

	'	vvdPathFileName = Path.ChangeExtension(mdlPathFileName, ".vvd")
	'	If File.Exists(vvdPathFileName) Then
	'		Dim vvdFile As SourceVvdFile
	'		vvdFile = New SourceVvdFile()
	'		Try
	'			Me.ReadFile(vvdPathFileName, AddressOf Me.ReadVvdFile)
	'		Catch ex As Exception
	'			status = StatusMessage.Error
	'		End Try
	'	End If

	'	Return status
	'End Function

#End Region

#Region "Data"

	Private theAniFileData As SourceAniFileData38
	Private theMdlFileData As SourceMdlFileData38
	Private theVvdFileData As SourceVvdFileData38

#End Region

End Class
