Public Class SourceMdlFileData44
	Inherits SourceMdlFileDataBase

	Public Sub New()
		MyBase.New()

		'NOTE: Set an extremely large number so that the calculations later will work well.
		Me.theSectionFrameMinFrameCount = 2000000

		'Me.theMdlFileOnlyHasAnimations = False
		Me.theFirstAnimationDesc = Nothing
		Me.theFirstAnimationDescFrameLines = New SortedList(Of Integer, AnimationFrameLine)()
		Me.theWeightLists = New List(Of SourceMdlWeightList)()
	End Sub

	'FROM: SourceEngine2006_source\public\studio.h
	'#define STUDIO_VERSION		44
	'struct studiohdr_t
	'{
	'	int					id;
	'	int					version;

	'	long				checksum;		// this has to be the same in the phy and vtx files to load!

	'	inline const char *	pszName( void ) const { return name; }
	'	char				name[64];
	'	int					length;
	'
	'
	'	Vector				eyeposition;	// ideal eye position
	'
	'	Vector				illumposition;	// illumination center
	'	
	'	Vector				hull_min;		// ideal movement hull size
	'	Vector				hull_max;			
	'
	'	Vector				view_bbmin;		// clipping bounding box
	'	Vector				view_bbmax;		
	'
	'	int					flags;
	'
	'	int					numbones;			// bones
	'	int					boneindex;
	'	inline mstudiobone_t *pBone( int i ) const { Assert( i >= 0 && i < numbones); return (mstudiobone_t *)(((byte *)this) + boneindex) + i; };
	'	int					RemapSeqBone( int iSequence, int iLocalBone ) const;	// maps local sequence bone to global bone
	'	int					RemapAnimBone( int iAnim, int iLocalBone ) const;		// maps local animations bone to global bone
	'
	'	int					numbonecontrollers;		// bone controllers
	'	int					bonecontrollerindex;
	'	inline mstudiobonecontroller_t *pBonecontroller( int i ) const { Assert( i >= 0 && i < numbonecontrollers); return (mstudiobonecontroller_t *)(((byte *)this) + bonecontrollerindex) + i; };
	'
	'	int					numhitboxsets;
	'	int					hitboxsetindex;
	'
	'	// Look up hitbox set by index
	'	mstudiohitboxset_t	*pHitboxSet( int i ) const 
	'	{ 
	'		Assert( i >= 0 && i < numhitboxsets); 
	'		return (mstudiohitboxset_t *)(((byte *)this) + hitboxsetindex ) + i; 
	'	};
	'
	'	// Calls through to hitbox to determine size of specified set
	'	inline mstudiobbox_t *pHitbox( int i, int set ) const 
	'	{ 
	'		mstudiohitboxset_t const *s = pHitboxSet( set );
	'		if ( !s )
	'			return NULL;
	'
	'		return s->pHitbox( i );
	'	};
	'
	'	// Calls through to set to get hitbox count for set
	'	inline int			iHitboxCount( int set ) const
	'	{
	'		mstudiohitboxset_t const *s = pHitboxSet( set );
	'		if ( !s )
	'			return 0;
	'
	'		return s->numhitboxes;
	'	};
	'
	'	// file local animations? and sequences
	'//private:
	'	int					numlocalanim;			// animations/poses
	'	int					localanimindex;		// animation descriptions
	'  	inline mstudioanimdesc_t *pLocalAnimdesc( int i ) const { if (i < 0 || i >= numlocalanim) i = 0; return (mstudioanimdesc_t *)(((byte *)this) + localanimindex) + i; };
	'
	'	int					numlocalseq;				// sequences
	'	int					localseqindex;
	'  	inline mstudioseqdesc_t *pLocalSeqdesc( int i ) const { if (i < 0 || i >= numlocalseq) i = 0; return (mstudioseqdesc_t *)(((byte *)this) + localseqindex) + i; };
	'
	'//public:
	'	bool				SequencesAvailable() const;
	'	int					GetNumSeq() const;
	'	mstudioanimdesc_t	&pAnimdesc( int i ) const;
	'	mstudioseqdesc_t	&pSeqdesc( int i ) const;
	'	int					iRelativeAnim( int baseseq, int relanim ) const;	// maps seq local anim reference to global anim index
	'	int					iRelativeSeq( int baseseq, int relseq ) const;		// maps seq local seq reference to global seq index
	'
	'//private:
	'	mutable int			activitylistversion;	// initialization flag - have the sequences been indexed?
	'	mutable int			eventsindexed;
	'//public:
	'	int					GetSequenceActivity( int iSequence );
	'	void				SetSequenceActivity( int iSequence, int iActivity );
	'	int					GetActivityListVersion( void ) const;
	'	void				SetActivityListVersion( int version ) const;
	'	int					GetEventListVersion( void ) const;
	'	void				SetEventListVersion( int version ) const;

	'	// raw textures
	'	int					numtextures;
	'	int					textureindex;
	'	inline mstudiotexture_t *pTexture( int i ) const { return (mstudiotexture_t *)(((byte *)this) + textureindex) + i; }; 


	'	// raw textures search paths
	'	int					numcdtextures;
	'	int					cdtextureindex;
	'	inline char			*pCdtexture( int i ) const { return (((char *)this) + *((int *)(((byte *)this) + cdtextureindex) + i)); };
	'
	'	// replaceable textures tables
	'	int					numskinref;
	'	int					numskinfamilies;
	'	int					skinindex;
	'	inline short		*pSkinref( int i ) const { return (short *)(((byte *)this) + skinindex) + i; };
	'
	'	int					numbodyparts;		
	'	int					bodypartindex;
	'	inline mstudiobodyparts_t	*pBodypart( int i ) const { return (mstudiobodyparts_t *)(((byte *)this) + bodypartindex) + i; };
	'
	'	// queryable attachable points
	'//private:
	'	int					numlocalattachments;
	'	int					localattachmentindex;
	'	inline mstudioattachment_t	*pLocalAttachment( int i ) const { Assert( i >= 0 && i < numlocalattachments); return (mstudioattachment_t *)(((byte *)this) + localattachmentindex) + i; };
	'//public:
	'	int					GetNumAttachments( void ) const;
	'	const mstudioattachment_t &pAttachment( int i ) const;
	'	int					GetAttachmentBone( int i ) const;
	'	// used on my tools in hlmv, not persistant
	'	void				SetAttachmentBone( int iAttachment, int iBone );
	'
	'	// animation node to animation node transition graph
	'//private:
	'	int					numlocalnodes;
	'	int					localnodeindex;
	'	int					localnodenameindex;
	'	inline char			*pszLocalNodeName( int iNode ) const { Assert( iNode >= 0 && iNode < numlocalnodes); return (((char *)this) + *((int *)(((byte *)this) + localnodenameindex) + iNode)); }
	'	inline byte			*pLocalTransition( int i ) const { Assert( i >= 0 && i < (numlocalnodes * numlocalnodes)); return (byte *)(((byte *)this) + localnodeindex) + i; };
	'
	'//public:
	'	int					EntryNode( int iSequence ) const;
	'	int					ExitNode( int iSequence ) const;
	'	char				*pszNodeName( int iNode ) const;
	'	int					GetTransition( int iFrom, int iTo ) const;
	'
	'	int					numflexdesc;
	'	int					flexdescindex;
	'	inline mstudioflexdesc_t *pFlexdesc( int i ) const { Assert( i >= 0 && i < numflexdesc); return (mstudioflexdesc_t *)(((byte *)this) + flexdescindex) + i; };
	'
	'	int					numflexcontrollers;
	'	int					flexcontrollerindex;
	'	inline mstudioflexcontroller_t *pFlexcontroller( int i ) const { Assert( i >= 0 && i < numflexcontrollers); return (mstudioflexcontroller_t *)(((byte *)this) + flexcontrollerindex) + i; };

	'	int					numflexrules;
	'	int					flexruleindex;
	'	inline mstudioflexrule_t *pFlexRule( int i ) const { Assert( i >= 0 && i < numflexrules); return (mstudioflexrule_t *)(((byte *)this) + flexruleindex) + i; };
	'
	'	int					numikchains;
	'	int					ikchainindex;
	'	inline mstudioikchain_t *pIKChain( int i ) const { Assert( i >= 0 && i < numikchains); return (mstudioikchain_t *)(((byte *)this) + ikchainindex) + i; };
	'
	'	int					nummouths;
	'	int					mouthindex;
	'	inline mstudiomouth_t *pMouth( int i ) const { Assert( i >= 0 && i < nummouths); return (mstudiomouth_t *)(((byte *)this) + mouthindex) + i; };
	'
	'//private:
	'	int					numlocalposeparameters;
	'	int					localposeparamindex;
	'	inline mstudioposeparamdesc_t *pLocalPoseParameter( int i ) const { Assert( i >= 0 && i < numlocalposeparameters); return (mstudioposeparamdesc_t *)(((byte *)this) + localposeparamindex) + i; };
	'//public:
	'	int					GetNumPoseParameters( void ) const;
	'	const mstudioposeparamdesc_t &pPoseParameter( int i ) const;
	'	int					GetSharedPoseParameter( int iSequence, int iLocalPose ) const;
	'
	'	int					surfacepropindex;
	'	inline char * const pszSurfaceProp( void ) const { return ((char *)this) + surfacepropindex; }
	'
	'	// Key values
	'	int					keyvalueindex;
	'	int					keyvaluesize;
	'	inline const char * KeyValueText( void ) const { return keyvaluesize != 0 ? ((char *)this) + keyvalueindex : NULL; }
	'
	'	int					numlocalikautoplaylocks;
	'	int					localikautoplaylockindex;
	'	inline mstudioiklock_t *pLocalIKAutoplayLock( int i ) const { Assert( i >= 0 && i < numlocalikautoplaylocks); return (mstudioiklock_t *)(((byte *)this) + localikautoplaylockindex) + i; };
	'
	'	int					GetNumIKAutoplayLocks( void ) const;
	'	const mstudioiklock_t &pIKAutoplayLock( int i ) const;
	'	int					CountAutoplaySequences() const;
	'	int					CopyAutoplaySequences( unsigned short *pOut, int outCount ) const;
	'	int					GetAutoplayList( unsigned short **pOut ) const;
	'
	'	// The collision model mass that jay wanted
	'	float				mass;
	'	int					contents;
	'
	'	// external animations, models, etc.
	'	int					numincludemodels;
	'	int					includemodelindex;
	'	inline mstudiomodelgroup_t *pModelGroup( int i ) const { Assert( i >= 0 && i < numincludemodels); return (mstudiomodelgroup_t *)(((byte *)this) + includemodelindex) + i; };
	'	// implementation specific call to get a named model
	'	const studiohdr_t	*FindModel( void **cache, char const *modelname ) const;
	'
	'	// implementation specific back pointer to virtual data
	'	mutable void		*virtualModel;
	'	virtualmodel_t		*GetVirtualModel( void ) const;
	'
	'	// for demand loaded animation blocks
	'	int					szanimblocknameindex;	
	'	inline char * const pszAnimBlockName( void ) const { return ((char *)this) + szanimblocknameindex; }
	'	int					numanimblocks;
	'	int					animblockindex;
	'	inline mstudioanimblock_t *pAnimBlock( int i ) const { Assert( i > 0 && i < numanimblocks); return (mstudioanimblock_t *)(((byte *)this) + animblockindex) + i; };
	'	mutable void		*animblockModel;
	'	byte *				GetAnimBlock( int i ) const;
	'
	'	int					bonetablebynameindex;
	'	inline const byte	*GetBoneTableSortedByName() const { return (byte *)this + bonetablebynameindex; }
	'
	'	// used by tools only that don't cache, but persist mdl's peer data
	'	// engine uses virtualModel to back link to cache pointers
	'	void				*pVertexBase;
	'	void				*pIndexBase;
	'
	'	// if STUDIOHDR_FLAGS_CONSTANT_DIRECTIONAL_LIGHT_DOT is set,
	'	// this value is used to calculate directional components of lighting 
	'	// on static props
	'	byte				constdirectionallightdot;
	'
	'	// set during load of mdl data to track *desired* lod configuration (not actual)
	'	// the *actual* clamped root lod is found in studiohwdata
	'	// this is stored here as a global store to ensure the staged loading matches the rendering
	'	byte				rootLOD;

	'	byte				unused[2];

	'	int					zeroframecacheindex;
	'	byte				*pZeroframeCache( int i ) const { if (zeroframecacheindex) return (byte *)this + ((int *)(((byte *)this) + zeroframecacheindex))[i]; else return NULL; }

	'	int					unused2[6];		// remove as appropriate

	'	studiohdr_t() {}
	'
	'private:
	'	// No copy constructors allowed
	'	studiohdr_t(const studiohdr_t& vOther);
	'
	'	friend struct virtualmodel_t;
	'};



	'Public id(3) As Char
	'Public version As Integer
	'Public checksum As Integer
	Public name(63) As Char
	'Public fileSize As Integer

	'Public eyePositionX As Single
	'Public eyePositionY As Single
	'Public eyePositionZ As Single
	Public eyePosition As New SourceVector()
	'Public illuminationPositionX As Single
	'Public illuminationPositionY As Single
	'Public illuminationPositionZ As Single
	Public illuminationPosition As New SourceVector()

	'Public hullMinPositionX As Single
	'Public hullMinPositionY As Single
	'Public hullMinPositionZ As Single
	'Public hullMaxPositionX As Single
	'Public hullMaxPositionY As Single
	'Public hullMaxPositionZ As Single
	Public hullMinPosition As New SourceVector()
	Public hullMaxPosition As New SourceVector()

	'Public viewBoundingBoxMinPositionX As Single
	'Public viewBoundingBoxMinPositionY As Single
	'Public viewBoundingBoxMinPositionZ As Single
	'Public viewBoundingBoxMaxPositionX As Single
	'Public viewBoundingBoxMaxPositionY As Single
	'Public viewBoundingBoxMaxPositionZ As Single
	Public viewBoundingBoxMinPosition As New SourceVector()
	Public viewBoundingBoxMaxPosition As New SourceVector()

	Public flags As Integer

	Public boneCount As Integer
	Public boneOffset As Integer
	Public boneControllerCount As Integer
	Public boneControllerOffset As Integer

	Public hitboxSetCount As Integer
	Public hitboxSetOffset As Integer

	Public localAnimationCount As Integer
	Public localAnimationOffset As Integer
	Public localSequenceCount As Integer
	Public localSequenceOffset As Integer
	Public activityListVersion As Integer
	Public eventsIndexed As Integer

	Public textureCount As Integer
	Public textureOffset As Integer
	Public texturePathCount As Integer
	Public texturePathOffset As Integer
	Public skinReferenceCount As Integer
	Public skinFamilyCount As Integer
	Public skinFamilyOffset As Integer

	Public bodyPartCount As Integer
	Public bodyPartOffset As Integer

	Public localAttachmentCount As Integer
	Public localAttachmentOffset As Integer

	Public localNodeCount As Integer
	Public localNodeOffset As Integer
	Public localNodeNameOffset As Integer

	Public flexDescCount As Integer
	Public flexDescOffset As Integer
	Public flexControllerCount As Integer
	Public flexControllerOffset As Integer
	Public flexRuleCount As Integer
	Public flexRuleOffset As Integer

	Public ikChainCount As Integer
	Public ikChainOffset As Integer

	Public mouthCount As Integer
	Public mouthOffset As Integer

	Public localPoseParamaterCount As Integer
	Public localPoseParameterOffset As Integer

	Public surfacePropOffset As Integer

	Public keyValueOffset As Integer
	Public keyValueSize As Integer

	Public localIkAutoPlayLockCount As Integer
	Public localIkAutoPlayLockOffset As Integer

	Public mass As Double
	Public contents As Integer

	Public includeModelCount As Integer
	Public includeModelOffset As Integer

	Public virtualModelP As Integer

	Public animBlockNameOffset As Integer
	Public animBlockCount As Integer
	Public animBlockOffset As Integer
	Public animBlockModelP As Integer

	Public boneTableByNameOffset As Integer

	Public vertexBaseP As Integer
	Public indexBaseP As Integer

	Public directionalLightDot As Byte

	Public rootLod As Byte

	' VERSION 44: 
	'	byte				unused[2];
	' Read into v48 and ignore for v44.
	'Public unused(1) As Byte
	'------------
	' VERSION 48:
	'	byte				numAllowedRootLODs;
	'	byte				unused[1];
	Public allowedRootLodCount_VERSION48 As Byte
	Public unused As Byte

	' VERSION 44 to 47: 
	'	int					zeroframecacheindex;
	Public zeroframecacheindex_VERSION44to47 As Integer
	'------------
	' VERSION 48:
	'	int					unused4; // zero out if version < 47
	' Read into v44to47 and ignore for v48.
	'Public unused4 As Integer

	' VERSION 44: 
	' Although source code shows unused2[6], at least one example (HL2 dog.mdl) has the flexControllerUi and header2 fields.
	'	int					unused2[6];		// remove as appropriate
	'Public unused2(5) As Integer
	'------------
	' VERSION 44 [Vindictus]: 
	'Public unknown01 As Integer
	'Public unknown02 As Integer
	'NOTE: The unknown02 = 1279345491 (ascii for "SCAL") seems to be in every MDL file in Vindictus.
	Public Const text_SCAL_VERSION44Vindictus As Integer = 1279345491
	'Public unknown03 As Integer
	'Public unknown04 As Integer
	'Public unknown05 As Integer
	'------------
	' VERSION 44 to 48:
	'	int					numflexcontrollerui;
	'	int					flexcontrolleruiindex;
	'	int					unused3[2];
	'	int					studiohdr2index;
	'	int					unused2[1];
	Public flexControllerUiCount As Integer
	Public flexControllerUiOffset As Integer
	Public unused3(1) As Integer
	Public studioHeader2Offset As Integer
	Public unused2 As Integer

	' VERSION 44: 
	' Unallocated fields.
	'------------
	' VERSION 45 to 48:
	' studiohdr2:
	Public sourceBoneTransformCount As Integer
	Public sourceBoneTransformOffset As Integer
	Public illumPositionAttachmentIndex As Integer
	Public maxEyeDeflection As Double
	Public linearBoneOffset As Integer
	Public nameCopyOffset As Integer
	Public reserved(57) As Integer



	Public theAnimationDescs As List(Of SourceMdlAnimationDesc44)
	Public theAnimBlocks As List(Of SourceMdlAnimBlock)
	Public theAnimBlockRelativePathFileName As String
	Public theAttachments As List(Of SourceMdlAttachment)
	Public theBodyParts As List(Of SourceMdlBodyPart)
	Public theBoneControllers As List(Of SourceMdlBoneController)
	Public theBones As List(Of SourceMdlBone)
	Public theBoneTableByName As List(Of Integer)
	Public theBoneTransforms As List(Of SourceMdlBoneTransform)
	Public theFlexControllers As List(Of SourceMdlFlexController)
	Public theFlexControllerUis As List(Of SourceMdlFlexControllerUi)
	Public theFlexDescs As List(Of SourceMdlFlexDesc)
	Public theFlexRules As List(Of SourceMdlFlexRule)
	Public theHitboxSets As List(Of SourceMdlHitboxSet)
	Public theIkChains As List(Of SourceMdlIkChain)
	Public theIkLocks As List(Of SourceMdlIkLock)
	Public theKeyValuesText As String
	Public theLinearBoneTable As SourceMdlLinearBone
	Public theLocalNodeNames As List(Of String)
	Public theModelGroups As List(Of SourceMdlModelGroup)
	Public theMouths As List(Of SourceMdlMouth)
	Public thePoseParamDescs As List(Of SourceMdlPoseParamDesc)
	Public theSequenceDescs As List(Of SourceMdlSequenceDesc)
	Public theSkinFamilies As List(Of List(Of Short))
	Public theSurfacePropName As String
	Public theTexturePaths As List(Of String)
	Public theTextures As List(Of SourceMdlTexture)
	Public theVindictusTextSCAL As String

	Public theSectionFrameCount As Integer
	Public theSectionFrameMinFrameCount As Integer

	'Public theActualFileSize As Long
	Public theModelCommandIsUsed As Boolean
	Public theBodyPartIndexThatShouldUseModelCommand As Integer
	Public theFlexFrames As List(Of FlexFrame)
	Public theEyelidFlexFrameIndexes As List(Of Integer)
	'Public theUpperEyelidFlexFrameIndexes As List(Of Integer)

	Public theFirstAnimationDesc As SourceMdlAnimationDesc44
	Public theFirstAnimationDescFrameLines As New SortedList(Of Integer, AnimationFrameLine)()
	Public theProceduralBonesCommandIsUsed As Boolean
	Public theWeightLists As New List(Of SourceMdlWeightList)()





	'// This flag is set if no hitbox information was specified
	'#define STUDIOHDR_FLAGS_AUTOGENERATED_HITBOX	( 1 << 0 )

	'// NOTE:  This flag is set at loadtime, not mdl build time so that we don't have to rebuild
	'// models when we change materials.
	'#define STUDIOHDR_FLAGS_USES_ENV_CUBEMAP		( 1 << 1 )

	'// Use this when there are translucent parts to the model but we're not going to sort it 
	'#define STUDIOHDR_FLAGS_FORCE_OPAQUE			( 1 << 2 )

	'// Use this when we want to render the opaque parts during the opaque pass
	'// and the translucent parts during the translucent pass
	'#define STUDIOHDR_FLAGS_TRANSLUCENT_TWOPASS		( 1 << 3 )

	'// This is set any time the .qc files has $staticprop in it
	'// Means there's no bones and no transforms
	'#define STUDIOHDR_FLAGS_STATIC_PROP				( 1 << 4 )

	'// NOTE:  This flag is set at loadtime, not mdl build time so that we don't have to rebuild
	'// models when we change materials.
	'#define STUDIOHDR_FLAGS_USES_FB_TEXTURE		    ( 1 << 5 )

	'// This flag is set by studiomdl.exe if a separate "$shadowlod" entry was present
	'//  for the .mdl (the shadow lod is the last entry in the lod list if present)
	'#define STUDIOHDR_FLAGS_HASSHADOWLOD			( 1 << 6 )

	'// NOTE:  This flag is set at loadtime, not mdl build time so that we don't have to rebuild
	'// models when we change materials.
	'#define STUDIOHDR_FLAGS_USES_BUMPMAPPING		( 1 << 7 )

	'// NOTE:  This flag is set when we should use the actual materials on the shadow LOD
	'// instead of overriding them with the default one (necessary for translucent shadows)
	'#define STUDIOHDR_FLAGS_USE_SHADOWLOD_MATERIALS	( 1 << 8 )

	'// NOTE:  This flag is set when we should use the actual materials on the shadow LOD
	'// instead of overriding them with the default one (necessary for translucent shadows)
	'#define STUDIOHDR_FLAGS_OBSOLETE				( 1 << 9 )

	'#define STUDIOHDR_FLAGS_UNUSED					( 1 << 10 )

	'// NOTE:  This flag is set at mdl build time
	'#define STUDIOHDR_FLAGS_NO_FORCED_FADE			( 1 << 11 )

	'// NOTE:  The npc will lengthen the viseme check to always include two phonemes
	'#define STUDIOHDR_FLAGS_FORCE_PHONEME_CROSSFADE	( 1 << 12 )

	'// This flag is set when the .qc has $constantdirectionallight in it
	'// If set, we use constantdirectionallightdot to calculate light intensity
	'// rather than the normal directional dot product
	'// only valid if STUDIOHDR_FLAGS_STATIC_PROP is also set
	'#define STUDIOHDR_FLAGS_CONSTANT_DIRECTIONAL_LIGHT_DOT ( 1 << 13 )

	'// Flag to mark delta flexes as already converted from disk format to memory format
	'#define STUDIOHDR_FLAGS_FLEXES_CONVERTED		( 1 << 14 )

	' VERSION 44:
	'NOTE: Based on the comment below and the value for the constant being different for v48, this might not have been used until v48.
	'// Flag to mark delta flexes as already converted from disk format to memory format
	'#define STUDIOHDR_FLAGS_AMBIENT_BOOST			( 1 << 15 )


	Public Const STUDIOHDR_FLAGS_AUTOGENERATED_HITBOX As Integer = 1 << 0
	'Public Const STUDIOHDR_FLAGS_USES_ENV_CUBEMAP As Integer = 1 << 1
	Public Const STUDIOHDR_FLAGS_FORCE_OPAQUE As Integer = 1 << 2
	Public Const STUDIOHDR_FLAGS_TRANSLUCENT_TWOPASS As Integer = 1 << 3
	Public Const STUDIOHDR_FLAGS_STATIC_PROP As Integer = 1 << 4
	'Public Const STUDIOHDR_FLAGS_USES_FB_TEXTURE As Integer = 1 << 5
	Public Const STUDIOHDR_FLAGS_HASSHADOWLOD As Integer = 1 << 6
	'Public Const STUDIOHDR_FLAGS_USES_BUMPMAPPING As Integer = 1 << 7
	Public Const STUDIOHDR_FLAGS_USE_SHADOWLOD_MATERIALS As Integer = 1 << 8
	Public Const STUDIOHDR_FLAGS_OBSOLETE As Integer = 1 << 9
	'Public Const STUDIOHDR_FLAGS_UNUSED As Integer = 1 << 10
	Public Const STUDIOHDR_FLAGS_NO_FORCED_FADE As Integer = 1 << 11
	Public Const STUDIOHDR_FLAGS_FORCE_PHONEME_CROSSFADE As Integer = 1 << 12
	Public Const STUDIOHDR_FLAGS_CONSTANT_DIRECTIONAL_LIGHT_DOT As Integer = 1 << 13
	'Public Const STUDIOHDR_FLAGS_FLEXES_CONVERTED As Integer = 1 << 14
	'Public Const STUDIOHDR_FLAGS_AMBIENT_BOOST As Integer = 1 << 15

End Class
