Public Class SourceMdlMesh04

	Public name(31) As Char
	Public faceCount As Integer
	Public unknownCount As Integer
	Public textureWidth As UInteger
	Public textureHeight As UInteger

	Public theName As String
	Public theFaces As List(Of SourceMdlFace04)
	Public theTextureBmpData As List(Of Byte)
	Public theTextureFileName As String

End Class
