Public Class SourceMdlSequenceDesc06

	'FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
	'typedef struct
	'{
	'	char				label[32];		// sequence label
	'
	'	float				fps;			// frames per second
	'	int					flags;			// looping/non-looping flags
	'
	'	int					numevents;		// TOMAS: USED (not always 0)
	'	int					eventindex;
	'
	'	int					numframes;		// number of frames per sequence
	'
	'	int					unused01;		// TOMAS: UNUSED (checked)
	'
	'	int					numpivots;		// number of foot pivots
	'	// TOMAS: polyrobo.mdl use this (4)
	'	int					pivotindex;
	'
	'	int					motiontype;		// TOMAS: USED (not always 0)
	'	int					motionbone;		// motion bone id (0)
	'
	'	int					unused02;		// TOMAS: UNUSED (checked)
	'	vec3_t				linearmovement;	// TOMAS: USED (not always 0)
	'
	'	int					numblends;		// TOMAS: UNUSED (checked)
	'	int					animindex;		// (->mstudioanim_t)
	'
	'	int					unused03[ 2 ];	// TOMAS: UNUSED (checked)
	'
	'} mstudioseqdesc_t;

	Public name(31) As Char
	Public fps As Double

	Public flags As Integer
	Public eventCount As Integer
	Public eventOffset As Integer
	Public frameCount As Integer
	Public unused01 As Integer
	Public pivotCount As Integer
	Public pivotOffset As Integer

	Public motiontype As Integer
	Public motionbone As Integer
	Public unused02 As Integer
	Public linearmovement As New SourceVector()

	Public blendCount As Integer
	Public animOffset As Integer

	Public unused03(1) As Integer


	Public theName As String
	Public theSmdRelativePathFileName As String
	Public theAnimations As List(Of SourceMdlAnimation06)
	Public theEvents As List(Of SourceMdlEvent06)
	Public thePivots As List(Of SourceMdlPivot06)


	' For the flags field
	'FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
	'// sequence flags
	'#define STUDIO_LOOPING	0x0001

	Public Const STUDIO_LOOPING As Integer = &H1

End Class
