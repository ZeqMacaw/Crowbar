Public Class SourceMdlTexture06

	'FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
	'typedef struct
	'{
	'	char				name[64];
	'	int					flags;
	'	int					width;
	'	int					height;
	'	int					index;
	'} mstudiotexture_t;

	Public fileName(63) As Char
	Public flags As Integer
	Public width As UInt32
	Public height As UInt32
	Public dataOffset As UInt32


	Public theFileName As String
	Public theData As List(Of Byte)

End Class
