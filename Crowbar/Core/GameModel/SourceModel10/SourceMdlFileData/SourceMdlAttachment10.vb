Public Class SourceMdlAttachment10

	'FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
	'// attachment
	'typedef struct 
	'{
	'	char				name[32];
	'	int					type;
	'	int					bone;
	'	vec3_t				org;	// attachment point
	'	vec3_t				vectors[3];
	'} mstudioattachment_t;



	Public name(31) As Char
	Public type As Integer
	Public boneIndex As Integer
	Public attachmentPoint As SourceVector
	Public vectors(2) As SourceVector



	Public theName As String

End Class
