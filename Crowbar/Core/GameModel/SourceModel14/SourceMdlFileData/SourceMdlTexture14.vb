Public Class SourceMdlTexture14


	Public fileName(63) As Char
	Public textureName(63) As Char
	Public flags As Integer
	Public width As UInt32
	Public height As UInt32
	Public dataOffset As UInt32


	Public theFileName As String
	Public theTextureName As String
	Public theData As List(Of Byte)


	'// lighting options
	'#define STUDIO_NF_FLATSHADE		0x0001
	'#define STUDIO_NF_CHROME		0x0002
	'#define STUDIO_NF_FULLBRIGHT	0x0004
	'#define STUDIO_NF_NOMIPS        0x0008
	'#define STUDIO_NF_ALPHA         0x0010
	'#define STUDIO_NF_ADDITIVE      0x0020
	'#define STUDIO_NF_MASKED        0x0040

	Public Const STUDIO_NF_FLATSHADE As Integer = &H1
	Public Const STUDIO_NF_CHROME As Integer = &H2
	Public Const STUDIO_NF_FULLBRIGHT As Integer = &H4
	Public Const STUDIO_NF_NOMIPS As Integer = &H8
	Public Const STUDIO_NF_ALPHA As Integer = &H10
	Public Const STUDIO_NF_ADDITIVE As Integer = &H20
	Public Const STUDIO_NF_MASKED As Integer = &H40

End Class
