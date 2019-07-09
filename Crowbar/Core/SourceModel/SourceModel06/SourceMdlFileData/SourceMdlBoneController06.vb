Public Class SourceMdlBoneController06

	'FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
	'typedef struct
	'{
	'	int					bone;			// -1 == 0 (TOMAS??)
	'	int					type;			// X, Y, Z, XR, YR, ZR, M
	'	float				start;
	'	float				end;
	'} mstudiobonecontroller_t;

	Public boneIndex As Integer
	Public type As Integer
	Public startAngleDegrees As Double
	Public endAngleDegrees As Double

End Class
