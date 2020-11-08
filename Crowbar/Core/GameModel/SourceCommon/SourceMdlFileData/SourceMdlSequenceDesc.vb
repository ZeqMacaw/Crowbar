Public Class SourceMdlSequenceDesc
	Inherits SourceMdlSequenceDescBase

	Public Sub New()
		Me.theWeightListIndex = -1
		'Me.theCorrectiveSubtractAnimationOptionIsUsed = False
		Me.theCorrectiveAnimationName = Nothing
	End Sub

	'// sequence descriptions
	'struct mstudioseqdesc_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int					baseptr;
	'	inline studiohdr_t	*pStudiohdr( void ) const { return (studiohdr_t *)(((byte *)this) + baseptr); }

	'	int					szlabelindex;
	'	inline char * const pszLabel( void ) const { return ((char *)this) + szlabelindex; }

	'	int					szactivitynameindex;
	'	inline char * const pszActivityName( void ) const { return ((char *)this) + szactivitynameindex; }

	'	int					flags;		// looping/non-looping flags

	'	int					activity;	// initialized at loadtime to game DLL values
	'	int					actweight;

	'	int					numevents;
	'	int					eventindex;
	'	inline mstudioevent_t *pEvent( int i ) const { Assert( i >= 0 && i < numevents); return (mstudioevent_t *)(((byte *)this) + eventindex) + i; };

	'	Vector				bbmin;		// per sequence bounding box
	'	Vector				bbmax;		

	'	int					numblends;

	'	// Index into array of shorts which is groupsize[0] x groupsize[1] in length
	'	int					animindexindex;

	'	inline int			anim( int x, int y ) const
	'	{
	'		if ( x >= groupsize[0] )
	'		{
	'			x = groupsize[0] - 1;
	'		}

	'		if ( y >= groupsize[1] )
	'		{
	'			y = groupsize[ 1 ] - 1;
	'		}

	'		int offset = y * groupsize[0] + x;
	'		short *blends = (short *)(((byte *)this) + animindexindex);
	'		int value = (int)blends[ offset ];
	'		return value;
	'	}

	'	int					movementindex;	// [blend] float array for blended movement
	'	int					groupsize[2];
	'	int					paramindex[2];	// X, Y, Z, XR, YR, ZR
	'	float				paramstart[2];	// local (0..1) starting value
	'	float				paramend[2];	// local (0..1) ending value
	'	int					paramparent;

	'	float				fadeintime;		// ideal cross fate in time (0.2 default)
	'	float				fadeouttime;	// ideal cross fade out time (0.2 default)

	'	int					localentrynode;		// transition node at entry
	'	int					localexitnode;		// transition node at exit
	'	int					nodeflags;		// transition rules

	'	float				entryphase;		// used to match entry gait
	'	float				exitphase;		// used to match exit gait

	'	float				lastframe;		// frame that should generation EndOfSequence

	'	int					nextseq;		// auto advancing sequences
	'	int					pose;			// index of delta animation between end and nextseq

	'	int					numikrules;

	'	int					numautolayers;	//
	'	int					autolayerindex;
	'	inline mstudioautolayer_t *pAutolayer( int i ) const { Assert( i >= 0 && i < numautolayers); return (mstudioautolayer_t *)(((byte *)this) + autolayerindex) + i; };

	'	int					weightlistindex;
	'	inline float		*pBoneweight( int i ) const { return ((float *)(((byte *)this) + weightlistindex) + i); };
	'	inline float		weight( int i ) const { return *(pBoneweight( i)); };

	'	// FIXME: make this 2D instead of 2x1D arrays
	'	int					posekeyindex;
	'	float				*pPoseKey( int iParam, int iAnim ) const { return (float *)(((byte *)this) + posekeyindex) + iParam * groupsize[0] + iAnim; }
	'	float				poseKey( int iParam, int iAnim ) const { return *(pPoseKey( iParam, iAnim )); }

	'	int					numiklocks;
	'	int					iklockindex;
	'	inline mstudioiklock_t *pIKLock( int i ) const { Assert( i >= 0 && i < numiklocks); return (mstudioiklock_t *)(((byte *)this) + iklockindex) + i; };

	'	// Key values
	'	int					keyvalueindex;
	'	int					keyvaluesize;
	'	inline const char * KeyValueText( void ) const { return keyvaluesize != 0 ? ((char *)this) + keyvalueindex : NULL; }

	'	int					cycleposeindex;		// index of pose parameter to use as cycle index

	'	int					unused[7];		// remove/add as appropriate (grow back to 8 ints on version change!)
	'======
	'FROM: VERSION 49
	'	int					activitymodifierindex;
	'	int					numactivitymodifiers;
	'	inline mstudioactivitymodifier_t *pActivityModifier( int i ) const { Assert( i >= 0 && i < numactivitymodifiers); return activitymodifierindex != 0 ? (mstudioactivitymodifier_t *)(((byte *)this) + activitymodifierindex) + i : NULL; };
	'	int					unused[5];		// remove/add as appropriate (grow back to 8 ints on version change!)

	'	mstudioseqdesc_t(){}
	'private:
	'	// No copy constructors allowed
	'	mstudioseqdesc_t(const mstudioseqdesc_t& vOther);
	'};

	'NOTE: Size of this class is 212 bytes.

	'	int					baseptr;
	'	inline studiohdr_t	*pStudiohdr( void ) const { return (studiohdr_t *)(((byte *)this) + baseptr); }
	Public baseHeaderOffset As Integer

	'	int					szlabelindex;
	'	inline char * const pszLabel( void ) const { return ((char *)this) + szlabelindex; }
	Public nameOffset As Integer

	'	int					szactivitynameindex;
	'	inline char * const pszActivityName( void ) const { return ((char *)this) + szactivitynameindex; }
	Public activityNameOffset As Integer

	'	int					flags;		// looping/non-looping flags
	Public flags As Integer

	'	int					activity;	// initialized at loadtime to game DLL values
	Public activity As Integer
	'	int					actweight;
	Public activityWeight As Integer

	'	int					numevents;
	Public eventCount As Integer
	'	int					eventindex;
	'	inline mstudioevent_t *pEvent( int i ) const { Assert( i >= 0 && i < numevents); return (mstudioevent_t *)(((byte *)this) + eventindex) + i; };
	Public eventOffset As Integer

	'	Vector				bbmin;		// per sequence bounding box
	Public bbMin As New SourceVector()
	'	Vector				bbmax;		
	Public bbMax As New SourceVector()

	'	int					numblends;
	Public blendCount As Integer

	'	// Index into array of shorts which is groupsize[0] x groupsize[1] in length
	'	int					animindexindex;
	Public animIndexOffset As Integer

	'	int					movementindex;	// [blend] float array for blended movement
	Public movementIndex As Integer
	'	int					groupsize[2];
	Public groupSize(1) As Integer
	'	int					paramindex[2];	// X, Y, Z, XR, YR, ZR
	Public paramIndex(1) As Integer
	'	float				paramstart[2];	// local (0..1) starting value
	Public paramStart(1) As Single
	'	float				paramend[2];	// local (0..1) ending value
	Public paramEnd(1) As Single
	'	int					paramparent;
	Public paramParent As Integer

	'	float				fadeintime;		// ideal cross fate in time (0.2 default)
	Public fadeInTime As Single
	'	float				fadeouttime;	// ideal cross fade out time (0.2 default)
	Public fadeOutTime As Single

	'	int					localentrynode;		// transition node at entry
	Public localEntryNodeIndex As Integer
	'	int					localexitnode;		// transition node at exit
	Public localExitNodeIndex As Integer
	'	int					nodeflags;		// transition rules
	Public nodeFlags As Integer

	'	float				entryphase;		// used to match entry gait
	Public entryPhase As Single
	'	float				exitphase;		// used to match exit gait
	Public exitPhase As Single

	'	float				lastframe;		// frame that should generation EndOfSequence
	Public lastFrame As Single

	'	int					nextseq;		// auto advancing sequences
	Public nextSeq As Integer
	'	int					pose;			// index of delta animation between end and nextseq
	Public pose As Integer

	'	int					numikrules;
	Public ikRuleCount As Integer

	'	int					numautolayers;	//
	Public autoLayerCount As Integer
	'	int					autolayerindex;
	'	inline mstudioautolayer_t *pAutolayer( int i ) const { Assert( i >= 0 && i < numautolayers); return (mstudioautolayer_t *)(((byte *)this) + autolayerindex) + i; };
	Public autoLayerOffset As Integer

	'	int					weightlistindex;
	Public weightOffset As Integer
	'	inline float		*pBoneweight( int i ) const { return ((float *)(((byte *)this) + weightlistindex) + i); };

	'	// FIXME: make this 2D instead of 2x1D arrays
	'	int					posekeyindex;
	Public poseKeyOffset As Integer
	'	float				*pPoseKey( int iParam, int iAnim ) const { return (float *)(((byte *)this) + posekeyindex) + iParam * groupsize[0] + iAnim; }

	'	int					numiklocks;
	Public ikLockCount As Integer
	'	int					iklockindex;
	'	inline mstudioiklock_t *pIKLock( int i ) const { Assert( i >= 0 && i < numiklocks); return (mstudioiklock_t *)(((byte *)this) + iklockindex) + i; };
	Public ikLockOffset As Integer

	'	// Key values
	'	int					keyvalueindex;
	Public keyValueOffset As Integer
	'	int					keyvaluesize;
	'	inline const char * KeyValueText( void ) const { return keyvaluesize != 0 ? ((char *)this) + keyvalueindex : NULL; }
	Public keyValueSize As Integer

	'	int					cycleposeindex;		// index of pose parameter to use as cycle index
	Public cyclePoseIndex As Integer

	'	int					unused[7];		// remove/add as appropriate (grow back to 8 ints on version change!)
	'======
	' Some Version 48 (such as Team Fortress 2 and Source SDK Base Multiplayer 2013, but not Garry's Mod)
	'FROM: VERSION 49
	'	int					activitymodifierindex;
	'	int					numactivitymodifiers;
	'	inline mstudioactivitymodifier_t *pActivityModifier( int i ) const { Assert( i >= 0 && i < numactivitymodifiers); return activitymodifierindex != 0 ? (mstudioactivitymodifier_t *)(((byte *)this) + activitymodifierindex) + i : NULL; };
	'	int					unused[5];		// remove/add as appropriate (grow back to 8 ints on version change!)
	Public activityModifierOffset As Integer
	Public activityModifierCount As Integer
	Public unused(6) As Integer


	Public theName As String
	Public theActivityName As String
	Public thePoseKeys As List(Of Double)
	Public theEvents As List(Of SourceMdlEvent)
	Public theAutoLayers As List(Of SourceMdlAutoLayer)
	Public theIkLocks As List(Of SourceMdlIkLock)
	'NOTE: In the file, a bone weight is a 32-bit float, i.e. a Single, but is stored as Double for better writing to file.
	Public theBoneWeights As List(Of Double)
	Public theWeightListIndex As Integer
	Public theAnimDescIndexes As List(Of Short)
	Public theKeyValues As String
	Public theActivityModifiers As List(Of SourceMdlActivityModifier)


	Public theBoneWeightsAreDefault As Boolean
	'Public theCorrectiveSubtractAnimationOptionIsUsed As Boolean
	Public theCorrectiveAnimationName As String

End Class
