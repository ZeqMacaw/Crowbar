Public Class SourceMdlBone10

	'FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
	'// bones
	'typedef struct 
	'{
	'	char				name[32];	// bone name for symbolic links
	'	int		 			parent;		// parent bone
	'	int					flags;		// ??
	'	int					bonecontroller[6];	// bone controller index, -1 == none
	'	float				value[6];	// default DoF values
	'	float				scale[6];   // scale for delta DoF values
	'} mstudiobone_t;



	'	char				name[32];	// bone name for symbolic links
	Public name(31) As Char

	'	int		 			parent;		// parent bone
	Public parentBoneIndex As Integer

	'	int					flags;		// ??
	Public flags As Integer

	'	int					bonecontroller[6];	// bone controller index, -1 == none
	Public boneControllerIndex(5) As Integer

	'FROM: [1999] HLStandardSDK\SourceCode\utils\studiomdl\write.c
	'		pbone[i].value[0]		= bonetable[i].pos[0];
	'		pbone[i].value[1]		= bonetable[i].pos[1];
	'		pbone[i].value[2]		= bonetable[i].pos[2];
	'		pbone[i].value[3]		= bonetable[i].rot[0];
	'		pbone[i].value[4]		= bonetable[i].rot[1];
	'		pbone[i].value[5]		= bonetable[i].rot[2];
	'		pbone[i].scale[0]		= bonetable[i].posscale[0];
	'		pbone[i].scale[1]		= bonetable[i].posscale[1];
	'		pbone[i].scale[2]		= bonetable[i].posscale[2];
	'		pbone[i].scale[3]		= bonetable[i].rotscale[0];
	'		pbone[i].scale[4]		= bonetable[i].rotscale[1];
	'		pbone[i].scale[5]		= bonetable[i].rotscale[2];
	'------
	'	float				value[6];	// default DoF values
	'	float				scale[6];   // scale for delta DoF values
	Public position As SourceVector
	Public rotation As SourceVector
	Public positionScale As SourceVector
	Public rotationScale As SourceVector



	Public theName As String

End Class
