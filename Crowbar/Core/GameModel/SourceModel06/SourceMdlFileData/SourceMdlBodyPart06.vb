Public Class SourceMdlBodyPart06

	'FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
	'typedef struct
	'{
	'	char				name[64];
	'	int					nummodels;
	'	int					base;
	'	int					modelindex; // index into models array (->mstudiomodel_t)
	'} mstudiobodyparts_t;

	Public name(63) As Char
	Public modelCount As Integer
	Public base As Integer
	Public modelOffset As Integer

	Public theName As String
	Public theModels As List(Of SourceMdlModel06)

End Class
