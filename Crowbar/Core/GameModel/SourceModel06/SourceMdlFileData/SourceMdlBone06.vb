Public Class SourceMdlBone06

	'FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
	'typedef struct
	'{
	'	char				name[32];		// bone name for symbolic links
	'	int		 			parent;			// parent bone
	'	int		 			unused[6];
	'} mstudiobone_t;

	Public name(31) As Char
	Public parentBoneIndex As Integer
	'Public unused(5) As Integer
	Public position As New SourceVector()
	Public rotation As New SourceVector()


	Public theName As String

End Class
