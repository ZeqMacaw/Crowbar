Public Class SourceMdlSequenceDesc10

	'FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
	'// sequence descriptions
	'typedef struct
	'{
	'	char				label[32];	// sequence label
	'
	'	float				fps;		// frames per second	
	'	int					flags;		// looping/non-looping flags
	'
	'	int					activity;
	'	int					actweight;
	'
	'	int					numevents;
	'	int					eventindex;
	'
	'	int					numframes;	// number of frames per sequence
	'
	'	int					numpivots;	// number of foot pivots
	'	int					pivotindex;
	'
	'	int					motiontype;	
	'	int					motionbone;
	'	vec3_t				linearmovement;
	'	int					automoveposindex;
	'	int					automoveangleindex;
	'
	'	vec3_t				bbmin;		// per sequence bounding box
	'	vec3_t				bbmax;		
	'
	'	int					numblends;
	'	int					animindex;		// mstudioanim_t pointer relative to start of sequence group data
	'										// [blend][bone][X, Y, Z, XR, YR, ZR]
	'
	'	int					blendtype[2];	// X, Y, Z, XR, YR, ZR
	'	float				blendstart[2];	// starting value
	'	float				blendend[2];	// ending value
	'	int					blendparent;
	'
	'	int					seqgroup;		// sequence group for demand loading
	'
	'	int					entrynode;		// transition node at entry
	'	int					exitnode;		// transition node at exit
	'	int					nodeflags;		// transition rules
	'	
	'	int					nextseq;		// auto advancing sequences
	'} mstudioseqdesc_t;



	'	char				label[32];	// sequence label
	Public name(31) As Char

	'	float				fps;		// frames per second	
	Public fps As Double

	Public flags As Integer
	Public activityId As Integer
	Public activityWeight As Integer
	Public eventCount As Integer
	Public eventOffset As Integer
	Public frameCount As Integer
	Public pivotCount As Integer
	Public pivotOffset As Integer

	Public motiontype As Integer
	Public motionbone As Integer
	Public linearmovement As New SourceVector()
	Public automoveposindex As Integer
	Public automoveangleindex As Integer

	Public bbMin As New SourceVector()
	Public bbMax As New SourceVector()

	Public blendCount As Integer

	'	int					animindex;		// mstudioanim_t pointer relative to start of sequence group data
	'										// [blend][bone][X, Y, Z, XR, YR, ZR]
	Public animOffset As Integer

	'	int					blendtype[2];	// X, Y, Z, XR, YR, ZR
	'	float				blendstart[2];	// starting value
	'	float				blendend[2];	// ending value
	'	int					blendparent;
	Public blendType(1) As Integer
	Public blendStart(1) As Double
	Public blendEnd(1) As Double
	Public blendParent As Integer

	'	int					seqgroup;		// sequence group for demand loading
	Public groupIndex As Integer

	'	int					entrynode;		// transition node at entry
	'	int					exitnode;		// transition node at exit
	'	int					nodeflags;		// transition rules
	Public entryNodeIndex As Integer
	Public exitNodeIndex As Integer
	Public nodeFlags As Integer

	'	int					nextseq;		// auto advancing sequences
	Public nextSeq As Integer



	Public theName As String
	' There are blendCount file names.
	Public theSmdRelativePathFileNames As List(Of String)
	Public theAnimations As List(Of SourceMdlAnimation10)
	Public theEvents As List(Of SourceMdlEvent10)
	Public thePivots As List(Of SourceMdlPivot10)


	' For the flags field
	'FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
	'// sequence flags
	'#define STUDIO_LOOPING	0x0001

	Public Const STUDIO_LOOPING As Integer = &H1



End Class
