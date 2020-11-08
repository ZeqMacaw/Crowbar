Public Class SourceMdlBoneController10

	'FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
	'// bone controllers
	'typedef struct 
	'{
	'	int					bone;	// -1 == 0
	'	int					type;	// X, Y, Z, XR, YR, ZR, M
	'	float				start;
	'	float				end;
	'	int					rest;	// byte index value at rest
	'	int					index;	// 0-3 user set controller, 4 mouth
	'} mstudiobonecontroller_t;


	'	int					bone;	// -1 == 0
	Public boneIndex As Integer

	'	int					type;	// X, Y, Z, XR, YR, ZR, M
	Public type As Integer

	'	float				start;
	'	float				end;
	Public startAngleDegrees As Double
	Public endAngleDegrees As Double

	'	int					rest;	// byte index value at rest
	Public restIndex As Integer

	'	int					index;	// 0-3 user set controller, 4 mouth
	Public index As Integer



End Class
