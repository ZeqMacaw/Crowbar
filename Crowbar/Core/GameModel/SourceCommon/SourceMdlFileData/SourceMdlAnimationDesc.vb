Public Class SourceMdlAnimationDesc
	Inherits SourceMdlAnimationDescBase

	Public Sub New()
		Me.theLinkedSequences = New List(Of SourceMdlSequenceDesc)()
	End Sub

	'FROM: SourceEngine2006_source\public\studio.h
	'struct mstudioanimdesc_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int					baseptr;
	'	inline studiohdr_t	*pStudiohdr( void ) const { return (studiohdr_t *)(((byte *)this) + baseptr); }

	'	int					sznameindex;
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }

	'	float				fps;		// frames per second	
	'	int					flags;		// looping/non-looping flags

	'	int					numframes;

	'	// piecewise movement
	'	int					nummovements;
	'	int					movementindex;
	'	inline mstudiomovement_t * const pMovement( int i ) const { return (mstudiomovement_t *)(((byte *)this) + movementindex) + i; };

	'	int					unused1[6];			// remove as appropriate (and zero if loading older versions)	

	'	int					animblock;
	'	int					animindex;	 // non-zero when anim data isn't in sections
	'	mstudioanim_t *pAnimBlock( int block, int index ) const; // returns pointer to a specific anim block (local or external)
	'	mstudioanim_t *pAnim( int *piFrame, float &flStall ) const; // returns pointer to data and new frame index
	'	mstudioanim_t *pAnim( int *piFrame ) const; // returns pointer to data and new frame index

	'	int					numikrules;
	'	int					ikruleindex;	// non-zero when IK data is stored in the mdl
	'	int					animblockikruleindex; // non-zero when IK data is stored in animblock file
	'	mstudioikrule_t *pIKRule( int i ) const;

	'	int					numlocalhierarchy;
	'	int					localhierarchyindex;
	'	mstudiolocalhierarchy_t *pHierarchy( int i ) const;

	'	int					sectionindex;
	'	int					sectionframes; // number of frames used in each fast lookup section, zero if not used
	'	inline mstudioanimsections_t * const pSection( int i ) const { return (mstudioanimsections_t *)(((byte *)this) + sectionindex) + i; }

	'	short				zeroframespan;	// frames per span
	'	short				zeroframecount; // number of spans
	'	int					zeroframeindex;
	'	byte				*pZeroFrameData( ) const { if (zeroframeindex) return (((byte *)this) + zeroframeindex); else return NULL; };
	'	mutable float		zeroframestalltime;		// saved during read stalls

	'	mstudioanimdesc_t(){}
	'private:
	'	// No copy constructors allowed
	'	mstudioanimdesc_t(const mstudioanimdesc_t& vOther);
	'};


	'NOTE: Size of this struct: 100 bytes.

	'	int					baseptr;
	'	inline studiohdr_t	*pStudiohdr( void ) const { return (studiohdr_t *)(((byte *)this) + baseptr); }
	Public baseHeaderOffset As Integer

	'	int					sznameindex;
	Public nameOffset As Integer

	'	float				fps;		// frames per second	
	Public fps As Single
	'	int					flags;		// looping/non-looping flags
	Public flags As Integer

	'	int					numframes;
	Public frameCount As Integer

	'	// piecewise movement
	'	int					nummovements;
	Public movementCount As Integer
	'	int					movementindex;
	Public movementOffset As Integer
	'	inline mstudiomovement_t * const pMovement( int i ) const { return (mstudiomovement_t *)(((byte *)this) + movementindex) + i; };

	'	int					unused1[6];			// remove as appropriate (and zero if loading older versions)	
	Public unused1(5) As Integer

	'	int					animblock;
	Public animBlock As Integer
	'	int					animindex;	 // non-zero when anim data isn't in sections
	Public animOffset As Integer
	'	mstudioanim_t *pAnimBlock( int block, int index ) const; // returns pointer to a specific anim block (local or external)
	'	mstudioanim_t *pAnim( int *piFrame, float &flStall ) const; // returns pointer to data and new frame index
	'	mstudioanim_t *pAnim( int *piFrame ) const; // returns pointer to data and new frame index

	'	int					numikrules;
	Public ikRuleCount As Integer
	'	int					ikruleindex;	// non-zero when IK data is stored in the mdl
	Public ikRuleOffset As Integer
	'	int					animblockikruleindex; // non-zero when IK data is stored in animblock file
	Public animblockIkRuleOffset As Integer
	'	mstudioikrule_t *pIKRule( int i ) const;

	'	int					numlocalhierarchy;
	Public localHierarchyCount As Integer
	'	int					localhierarchyindex;
	Public localHierarchyOffset As Integer
	'	mstudiolocalhierarchy_t *pHierarchy( int i ) const;

	'	int					sectionindex;
	Public sectionOffset As Integer
	'	int					sectionframes; // number of frames used in each fast lookup section, zero if not used
	Public sectionFrameCount As Integer
	'	inline mstudioanimsections_t * const pSection( int i ) const { return (mstudioanimsections_t *)(((byte *)this) + sectionindex) + i; }

	'	short				zeroframespan;	// frames per span
	Public spanFrameCount As Short
	'	short				zeroframecount; // number of spans
	Public spanCount As Short
	'	int					zeroframeindex;
	Public spanOffset As Integer
	'	byte				*pZeroFrameData( ) const { if (zeroframeindex) return (((byte *)this) + zeroframeindex); else return NULL; };
	'	mutable float		zeroframestalltime;		// saved during read stalls
	Public spanStallTime As Single


	''TODO: Why is this field here?
	'Public unknown(5) As Integer


	' Moved to SourceMdlAnimationDescBase
	''	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	'Public theName As String

	'Public theAnimations As List(Of SourceMdlAnimation)
	Public theSectionsOfAnimations As List(Of List(Of SourceMdlAnimation))
	'Public theAniFrameAnims As List(Of SourceAniFrameAnim)
	Public theAniFrameAnim As SourceAniFrameAnim49
	Public theIkRules As List(Of SourceMdlIkRule)
	Public theSections As List(Of SourceMdlAnimationSection)
    Public theMovements As List(Of SourceMdlMovement)
    Public theLocalHierarchies As List(Of SourceMdlLocalHierarchy)

	Public theAnimIsLinkedToSequence As Boolean
	Public theLinkedSequences As List(Of SourceMdlSequenceDesc)



	' Values for the field, flags:
	'FROM: SourceEngineXXXX_source\public\studio.h
	'// sequence and autolayer flags
	'#define STUDIO_LOOPING	0x0001		// ending frame should be the same as the starting frame
	'#define STUDIO_SNAP		0x0002		// do not interpolate between previous animation and this one
	'#define STUDIO_DELTA	0x0004		// this sequence "adds" to the base sequences, not slerp blends
	'#define STUDIO_AUTOPLAY	0x0008		// temporary flag that forces the sequence to always play
	'#define STUDIO_POST		0x0010		// 
	'#define STUDIO_ALLZEROS	0x0020		// this animation/sequence has no real animation data
	'//						0x0040
	'#define STUDIO_CYCLEPOSE 0x0080		// cycle index is taken from a pose parameter index
	'#define STUDIO_REALTIME	0x0100		// cycle index is taken from a real-time clock, not the animations cycle index
	'#define STUDIO_LOCAL	0x0200		// sequence has a local context sequence
	'#define STUDIO_HIDDEN	0x0400		// don't show in default selection views
	'#define STUDIO_OVERRIDE	0x0800		// a forward declared sequence (empty)
	'#define STUDIO_ACTIVITY	0x1000		// Has been updated at runtime to activity index
	'#define STUDIO_EVENT	0x2000		// Has been updated at runtime to event index
	'#define STUDIO_WORLD	0x4000		// sequence blends in worldspace
	'------
	'VERSION 49
	'FROM: AlienSwarm_source\src\public\studio.h
	'      Adds these to the above.
	'#define STUDIO_FRAMEANIM 0x0040		// animation is encoded as by frame x bone instead of RLE bone x frame
	'#define STUDIO_NOFORCELOOP 0x8000	// do not force the animation loop
	'#define STUDIO_EVENT_CLIENT 0x10000	// Has been updated at runtime to event index on client


	Public Const STUDIO_LOOPING As Integer = &H1
	Public Const STUDIO_SNAP As Integer = &H2
	Public Const STUDIO_DELTA As Integer = &H4
	Public Const STUDIO_AUTOPLAY As Integer = &H8
	Public Const STUDIO_POST As Integer = &H10
	Public Const STUDIO_ALLZEROS As Integer = &H20
	' &H40
	Public Const STUDIO_CYCLEPOSE As Integer = &H80
	Public Const STUDIO_REALTIME As Integer = &H100
    'NOTE: STUDIO_LOCAL used internally by studiomdl and not needed by Crowbar.
    Public Const STUDIO_LOCAL As Integer = &H200
    Public Const STUDIO_HIDDEN As Integer = &H400
	Public Const STUDIO_OVERRIDE As Integer = &H800
    'NOTE: STUDIO_ACTIVITY used internally by game engine and not needed by Crowbar.
    Public Const STUDIO_ACTIVITY As Integer = &H1000
    'NOTE: STUDIO_EVENT used internally by game engine and not needed by Crowbar.
    Public Const STUDIO_EVENT As Integer = &H2000
	Public Const STUDIO_WORLD As Integer = &H4000
	'------
	'VERSION 49
	Public Const STUDIO_FRAMEANIM As Integer = &H40
	Public Const STUDIO_NOFORCELOOP As Integer = &H8000
	Public Const STUDIO_EVENT_CLIENT As Integer = &H10000

End Class
