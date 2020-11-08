Public Class SourceMdlBodyPart10

	'FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
	'// body part index
	'typedef struct
	'{
	'	char				name[64];
	'	int					nummodels;
	'	int					base;
	'	int					modelindex; // index into models array
	'} mstudiobodyparts_t;

	Public name(63) As Char
	Public modelCount As Integer
	Public base As Integer
	Public modelOffset As Integer

	Public theName As String
	Public theModels As List(Of SourceMdlModel10)

End Class
