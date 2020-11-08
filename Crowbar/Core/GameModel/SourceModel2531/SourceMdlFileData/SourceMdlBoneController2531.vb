Public Class SourceMdlBoneController2531

	'FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
	'struct mstudiobonecontroller_t
	'{
	'	int					bone;	// -1 == 0
	'	int					type;	// X, Y, Z, XR, YR, ZR, M
	'	float				start;
	'	float				end;
	'	int					rest;	// byte index value at rest
	'	int					inputfield;	// 0-3 user set controller, 4 mouth
	'	char				padding[32];	// future expansion.
	'};

	Public boneIndex As Integer
	Public type As Integer
	Public startAngleDegrees As Double
	Public endAngleDegrees As Double
	Public restIndex As Integer
	Public inputField As Integer
	Public unused(31) As Integer


End Class
