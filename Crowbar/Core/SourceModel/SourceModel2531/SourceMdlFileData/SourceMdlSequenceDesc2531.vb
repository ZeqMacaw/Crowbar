Public Class SourceMdlSequenceDesc2531
	Inherits SourceMdlSequenceDescBase

	Public Sub New()
		'	short				anim[MAXSTUDIOBLENDS][MAXSTUDIOBLENDS];	// f64: 16x16x2 = 512 bytes each anim a short
		Me.anim = New List(Of List(Of Short))(MAXSTUDIOBLENDS)
		For rowIndex As Integer = 0 To MAXSTUDIOBLENDS - 1
			Dim animRow As New List(Of Short)(MAXSTUDIOBLENDS)
			For columnIndex As Integer = 0 To MAXSTUDIOBLENDS - 1
				animRow.Add(0)
			Next
			Me.anim.Add(animRow)
		Next
	End Sub

	'FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
	'struct mstudioseqdesc_t
	'{
	'	int					szlabelindex;
	'	inline char * const pszLabel( void ) const { return ((char *)this) + szlabelindex; }
	'
	'	int					szactivitynameindex;
	'	inline char * const pszActivityName( void ) const { return ((char *)this) + szactivitynameindex; }
	'
	'	int					flags;		// looping/non-looping flags
	'
	'	int					activity;	// initialized at loadtime to game DLL values
	'	int					actweight;
	'
	'	int					numevents;
	'	int					eventindex;
	'	inline mstudioevent_t *pEvent( int i ) const { return (mstudioevent_t *)(((byte *)this) + eventindex) + i; };
	'
	'	Vector				bbmin;		// per sequence bounding box
	'	Vector				bbmax;		
	'
	'	int					numblends;
	'
	'	short				anim[MAXSTUDIOBLENDS][MAXSTUDIOBLENDS];	// f64: 16x16x2 = 512 bytes each anim a short
	'
	'	int					movementindex;	// [blend] float array for blended movement
	'	int					groupsize[2];
	'	int					paramindex[2];	// X, Y, Z, XR, YR, ZR
	'	float				paramstart[2];	// local (0..1) starting value
	'	float				paramend[2];	// local (0..1) ending value
	'	int					paramparent;
	'
	'	int					seqgroup;		// sequence group for demand loading
	'
	'	float				fadeintime;		// ideal cross fate in time (0.2 default)
	'	float				fadeouttime;	// ideal cross fade out time (0.2 default)
	'
	'	float				entrynode;	// f64: ~int, FIXME: this is a placeholder not transition node at entry
	'	int					exitnode;		// transition node at exit
	'	int					nodeflags;		// transition rules
	'
	'	float				entryphase;		// used to match entry gait
	'	float				exitphase;		// used to match exit gait
	'	
	'	float				lastframe;		// frame that should generation EndOfSequence
	'
	'	int					nextseq;		// auto advancing sequences
	'	int					pose;			// index of delta animation between end and nextseq
	'
	'	int					numikrules;
	'
	'	int					numautolayers;	//
	'	int					autolayerindex;
	'	inline mstudioautolayer_t *pAutolayer( int i ) const { return (mstudioautolayer_t *)(((byte *)this) + autolayerindex) + i; };
	'
	'	int					weightlistindex;
	'	float				*pBoneweight( int i ) const { return ((float *)(((byte *)this) + weightlistindex) + i); };
	'	float				weight( int i ) const { return *(pBoneweight( i)); };
	'
	'	int					posekeyindex;
	'	float				*pPoseKey( int iParam, int iAnim ) const { return (float *)(((byte *)this) + posekeyindex) + iParam * groupsize[0] + iAnim; }
	'	float				poseKey( int iParam, int iAnim ) const { return *(pPoseKey( iParam, iAnim )); }
	'
	'	Vector				bbmin2;		// f64: +
	'	Vector				bbmax2;		// f64: +
	'
	'	int					numiklocks;
	'	int					iklockindex;
	'	inline mstudioiklock_t *pIKLock( int i ) const { return (mstudioiklock_t *)(((byte *)this) + iklockindex) + i; };
	'
	'	// Key values
	'	int					keyvalueindex;
	'	int					keyvaluesize;
	'	inline const char * KeyValueText( void ) const { return keyvaluesize != 0 ? ((char *)this) + keyvalueindex : NULL; }
	'
	'	int unkindex1;	// f64: +
	'
	'	unsigned int unkunsigned1;	// f64: +
	'	unsigned int unkunsigned2;	// f64: +
	'
	'	int unkint[7];		// f64: +
	'	float unkfloat[3];	// f64: +
	'
	'//	int					unused[3];	// f64: -	// remove/add as appropriate (grow back to 8 ints on version change!)
	'};

	Public nameOffset As Integer
	Public activityNameOffset As Integer
	Public flags As Integer
	Public activityId As Integer
	Public activityWeight As Integer
	Public eventCount As Integer
	Public eventOffset As Integer

	Public bbMin As New SourceVector()
	Public bbMax As New SourceVector()

	Public blendCount As Integer

	Public anim As List(Of List(Of Short))

	Public movementIndex As Integer
	Public groupSize(1) As Integer
	Public paramIndex(1) As Integer
	Public paramStart(1) As Double
	Public paramEnd(1) As Double
	Public paramParent As Integer

	'	int					seqgroup;		// sequence group for demand loading
	Public sequenceGroup As Integer

	Public fadeInTime As Double
	Public fadeOutTime As Double

	Public test As Double	' same value as fadeInTime and fadeOutTime: 0.2xxxxx

	Public localEntryNodeIndex As Integer
	Public localExitNodeIndex As Integer
	Public nodeFlags As Integer

	Public entryPhase As Double
	Public exitPhase As Double
	Public lastFrame As Double

	Public nextSeq As Integer
	Public pose As Integer

	Public ikRuleCount As Integer
	Public autoLayerCount As Integer
	Public autoLayerOffset As Integer
	'Public weightOffset As Integer
	'Public poseKeyOffset As Integer
	Public unknown01 As Integer

	''Vector				bbmin2;		// f64: +
	''Vector				bbmax2;		// f64: +
	'Public bbMin2 As New SourceVector()
	'Public bbMax2 As New SourceVector()
	Public test02(5) As Double
	Public test03 As Integer

	Public ikLockCount As Integer
	Public ikLockOffset As Integer
	'Public keyValueOffset As Integer
	'Public keyValueSize As Integer
	Public keyValueSize As Integer
	Public keyValueOffset As Integer

	''int unkindex1;	// f64: +
	'Public unknown01 As Integer

	'unsigned int unkunsigned1;	// f64: +
	'unsigned int unkunsigned2;	// f64: +
	'Public unknown02 As UInt32
	'Public unknown03 As UInt32
	Public unknown02 As Double
	Public unknown03 As Double

	'int unkint[7];		// f64: +
	'float unkfloat[3];	// f64: +
	Public unknown04(6) As Integer
	Public unknown05(2) As Double


	Public theName As String
	Public theActivityName As String
	Public thePoseKeys As List(Of Double)
	Public theEvents As List(Of SourceMdlEvent)
	Public theAutoLayers As List(Of SourceMdlAutoLayer)
	Public theIkLocks As List(Of SourceMdlIkLock)
	'NOTE: In the file, a bone weight is a 32-bit float, i.e. a Single, but is stored as Double for better writing to file.
	Public theBoneWeights As List(Of Double)
	Public theWeightListIndex As Integer
	'Public theAnimDescIndexes As List(Of Short)
	Public theKeyValues As String


	Public theBoneWeightsAreDefault As Boolean

End Class
