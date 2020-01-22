Public Class SourceMdlBoneController

	'FROM: VERSION 10, inputfield is called index. Also, unused[8] is not in the struct.
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

	'FROM: SourceEngineXXXX_source\public\studio.h
	'// bone controllers
	'struct mstudiobonecontroller_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int					bone;	// -1 == 0
	'	int					type;	// X, Y, Z, XR, YR, ZR, M
	'	float				start;
	'	float				end;
	'	int					rest;	// byte index value at rest
	'	int					inputfield;	// 0-3 user set controller, 4 mouth
	'	int					unused[8];
	'};


	Public boneIndex As Integer
	Public type As Integer
	Public startBlah As Double
	Public endBlah As Double
	Public restIndex As Integer
	Public inputField As Integer
	Public unused(7) As Integer


	'// motion flags
	'#define STUDIO_X		0x00000001
	'#define STUDIO_Y		0x00000002	
	'#define STUDIO_Z		0x00000004
	'#define STUDIO_XR		0x00000008
	'#define STUDIO_YR		0x00000010
	'#define STUDIO_ZR		0x00000020
	'
	'#define STUDIO_LX		0x00000040
	'#define STUDIO_LY		0x00000080
	'#define STUDIO_LZ		0x00000100
	'#define STUDIO_LXR		0x00000200
	'#define STUDIO_LYR		0x00000400
	'#define STUDIO_LZR		0x00000800
	'
	'#define STUDIO_LINEAR	0x00001000
	'
	'#define STUDIO_TYPES	0x0003FFFF
	'#define STUDIO_RLOOP	0x00040000	// controller that wraps shortest distance
	'
	'FROM: SourceEngine2006_source\public\studiomdl.h
	'#define STUDIO_QUADRATIC_MOTION 0x00002000

	Public STUDIO_X As Integer = &H1
	Public STUDIO_Y As Integer = &H2
	Public STUDIO_Z As Integer = &H4
	Public STUDIO_XR As Integer = &H8
	Public STUDIO_YR As Integer = &H10
	Public STUDIO_ZR As Integer = &H20

	Public STUDIO_LX As Integer = &H40
	Public STUDIO_LY As Integer = &H80
	Public STUDIO_LZ As Integer = &H100
	Public STUDIO_LXR As Integer = &H200
	Public STUDIO_LYR As Integer = &H400
	Public STUDIO_LZR As Integer = &H800

	Public STUDIO_LINEAR As Integer = &H1000
	Public STUDIO_QUADRATIC_MOTION As Integer = &H2000

	Public STUDIO_TYPES As Integer = &H3FFFF
	Public STUDIO_RLOOP As Integer = &H40000

	' For the type field.
	'FROM: SourceEngine2006_source\public\studio.h
	'int lookupControl( char *string )
	'{
	'	if (stricmp(string,"X")==0) return STUDIO_X;
	'	if (stricmp(string,"Y")==0) return STUDIO_Y;
	'	if (stricmp(string,"Z")==0) return STUDIO_Z;
	'	if (stricmp(string,"XR")==0) return STUDIO_XR;
	'	if (stricmp(string,"YR")==0) return STUDIO_YR;
	'	if (stricmp(string,"ZR")==0) return STUDIO_ZR;

	'	if (stricmp(string,"LX")==0) return STUDIO_LX;
	'	if (stricmp(string,"LY")==0) return STUDIO_LY;
	'	if (stricmp(string,"LZ")==0) return STUDIO_LZ;
	'	if (stricmp(string,"LXR")==0) return STUDIO_LXR;
	'	if (stricmp(string,"LYR")==0) return STUDIO_LYR;
	'	if (stricmp(string,"LZR")==0) return STUDIO_LZR;

	'	if (stricmp(string,"LM")==0) return STUDIO_LINEAR;
	'	if (stricmp(string,"LQ")==0) return STUDIO_QUADRATIC_MOTION;

	'	return -1;
	'}
	Public ReadOnly Property TypeName() As String
		Get
			If (Me.type And STUDIO_X) > 0 Then
				Return "X"
			ElseIf (Me.type And STUDIO_Y) > 0 Then
				Return "Y"
			ElseIf (Me.type And STUDIO_Z) > 0 Then
				Return "Z"
			ElseIf (Me.type And STUDIO_XR) > 0 Then
				Return "XR"
			ElseIf (Me.type And STUDIO_YR) > 0 Then
				Return "YR"
			ElseIf (Me.type And STUDIO_ZR) > 0 Then
				Return "ZR"
			ElseIf (Me.type And STUDIO_LX) > 0 Then
				Return "LX"
			ElseIf (Me.type And STUDIO_LY) > 0 Then
				Return "LY"
			ElseIf (Me.type And STUDIO_LZ) > 0 Then
				Return "LZ"
			ElseIf (Me.type And STUDIO_LXR) > 0 Then
				Return "LXR"
			ElseIf (Me.type And STUDIO_LYR) > 0 Then
				Return "LYR"
			ElseIf (Me.type And STUDIO_LZR) > 0 Then
				Return "LZR"
			ElseIf (Me.type And STUDIO_LINEAR) > 0 Then
				Return "LM"
			ElseIf (Me.type And STUDIO_QUADRATIC_MOTION) > 0 Then
				Return "LQ"
			End If

			Return ""
		End Get
		'Set(ByVal value As String)

		'End Set
	End Property

End Class
