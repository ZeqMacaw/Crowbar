Public Class SourceMdlFileDataBase
	Inherits SourceFileData

	Public Sub New()
		MyBase.New()

		' Set to -1 so versions without fileSize field will have this value.
		Me.fileSize = -1

		Me.theChecksumIsValid = True
		Me.theMdlFileOnlyHasAnimations = False
		Me.theUpAxisYCommandWasUsed = False
	End Sub

	Public id(3) As Char
	Public version As Integer
	Public fileSize As Integer
	Public checksum As Integer

	Public theActualFileSize As Long
	Public theID As String
	Public theModelName As String
	Public theFileName As String
	Public theModifiedTexturePaths As New List(Of String)()
	Public theModifiedTextureFileNames As New List(Of String)()

	Public theChecksumIsValid As Boolean
	Public theMdlFileOnlyHasAnimations As Boolean
	Public theUpAxisYCommandWasUsed As Boolean

End Class
