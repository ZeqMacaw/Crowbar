Public Class SourceMdlFileData2531
	Inherits SourceMdlFileDataBase

	Public Sub New()
		MyBase.New()

		Me.theModelCommandIsUsed = False
		Me.theProceduralBonesCommandIsUsed = False
	End Sub

	'FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
	'#define STUDIO_VERSION		2531	// READ ABOVE!!!	// f64: ~
	'#define MAXSTUDIOTRIANGLES	25000	// TODO: tune this
	'#define MAXSTUDIOVERTS		25000	// TODO: tune this
	'#define MAXSTUDIOSKINS		32		// total textures
	'#define MAXSTUDIOBONES		1024	// total bones actually used (Psycho-A: < 128)
	'#define MAXSTUDIOBLENDS		16		// f64: ~New
	'#define MAXSTUDIOFLEXDESC	128
	'#define MAXSTUDIOFLEXCTRL	64
	'#define MAXSTUDIOPOSEPARAM	24
	'#define MAXSTUDIOBONECTRLS	4
	'#define MAXSTUDIOBONEBITS	10		// NOTE: MUST MATCH MAXSTUDIOBONES (Psycho-A: < 7)
	'struct studiohdr_t
	'{
	'	int					id;
	'	int					version;
	'
	'	long				checksum;		// this has to be the same in the phy and vtx files to load!
	'	
	'	char				name[128];		// f64: ~
	'	int					length;
	'
	'	Vector				eyeposition;	// ideal eye position
	'
	'	Vector				illumposition;	// illumination center
	'
	'// f64: add new vars
	'	int					gh[2];
	'
	'	float				PosZ; // 176
	'	float				PosX; // 180
	'	float				PosY; // 184
	'
	'	float				RotX; // 188
	'	float				RotY; // 192
	'	float				RotZ; // 196
	'
	'//	Vector				unkvec;
	'// ---
	'	
	'//	Vector				hull_min;			// f64: - // ideal movement hull size
	'//	Vector				hull_max;			// f64: - 
	'
	'	Vector				view_bbmin;			// clipping bounding box
	'	Vector				view_bbmax;		
	'
	'	int					yty;	// f64: +
	'	
	'	int					flags;	// f64: 228
	'	
	'	int					unz[2];	// f64: +
	'
	'	int					numbones;			// bones
	'	int					boneindex;
	'	inline mstudiobone_t *pBone( int i ) const { return (mstudiobone_t *)(((byte *)this) + boneindex) + i; };
	'
	'	int					numbonecontrollers;		// bone controllers
	'	int					bonecontrollerindex;
	'	inline mstudiobonecontroller_t *pBonecontroller( int i ) const { return (mstudiobonecontroller_t *)(((byte *)this) + bonecontrollerindex) + i; };
	'
	'	int					numhitboxsets;
	'	int					hitboxsetindex;
	'
	'
	'
	'	// Look up hitbox set by index
	'	mstudiohitboxset_t	*pHitboxSet( int i ) const 
	'	{ 
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
	'	/*
	'	int					numhitboxes;			// complex bounding boxes
	'	int					hitboxindex;			
	'	inline mstudiobbox_t *pHitbox( int i ) const { return (mstudiobbox_t *)(((byte *)this) + hitboxindex) + i; };
	'	*/
	'	
	'	int					numanim;			// animations/poses
	'	int					animdescindex;		// animation descriptions
	'	inline mstudioanimdesc_t *pAnimdesc( int i ) const { return (mstudioanimdesc_t *)(((byte *)this) + animdescindex) + i; };
	'
	'	int					numseq;				// sequences
	'	int					seqindex;
	'	inline mstudioseqdesc_t *pSeqdesc( int i ) const { if (i < 0 || i >= numseq) i = 0; return (mstudioseqdesc_t *)(((byte *)this) + seqindex) + i; };
	'	int					sequencesindexed;	// initialization flag - have the sequences been indexed?
	'
	'	int					numseqgroups;		// demand loaded sequences
	'	int					seqgroupindex;
	'
	'	int					numtextures;		// raw textures
	'	int					textureindex;
	'	inline mstudiotexture_t *pTexture( int i ) const { return (mstudiotexture_t *)(((byte *)this) + textureindex) + i; }; 
	'
	'	int					numcdtextures;		// raw textures search paths
	'	int					cdtextureindex;
	'	inline char			*pCdtexture( int i ) const { return (((char *)this) + *((int *)(((byte *)this) + cdtextureindex) + i)); };
	'
	'	int					numskinref;			// replaceable textures tables
	'	int					numskinfamilies;
	'	int					skinindex;
	'	inline short		*pSkinref( int i ) const { return (short *)(((byte *)this) + skinindex) + i; };
	'
	'	int					numbodyparts;		
	'	int					bodypartindex;
	'	inline mstudiobodyparts_t	*pBodypart( int i ) const { return (mstudiobodyparts_t *)(((byte *)this) + bodypartindex) + i; };
	'
	'	int					numattachments;		// queryable attachable points
	'	int					attachmentindex;
	'	inline mstudioattachment_t	*pAttachment( int i ) const { return (mstudioattachment_t *)(((byte *)this) + attachmentindex) + i; };
	'
	'	int					numtransitions;		// animation node to animation node transition graph
	'	int					transitionindex;
	'	inline byte	*pTransition( int i ) const { return (byte *)(((byte *)this) + transitionindex) + i; };
	'
	'	int					numflexdesc;
	'	int					flexdescindex;
	'	inline mstudioflexdesc_t *pFlexdesc( int i ) const { return (mstudioflexdesc_t *)(((byte *)this) + flexdescindex) + i; };
	'
	'	int					numflexcontrollers;
	'	int					flexcontrollerindex;
	'	inline mstudioflexcontroller_t *pFlexcontroller( int i ) const { return (mstudioflexcontroller_t *)(((byte *)this) + flexcontrollerindex) + i; };
	'
	'	int					numflexrules;
	'	int					flexruleindex;
	'	inline mstudioflexrule_t *pFlexRule( int i ) const { return (mstudioflexrule_t *)(((byte *)this) + flexruleindex) + i; };
	'
	'	int					numikchains;
	'	int					ikchainindex;
	'	inline mstudioikchain_t *pIKChain( int i ) const { return (mstudioikchain_t *)(((byte *)this) + ikchainindex) + i; };
	'
	'	int					nummouths;
	'	int					mouthindex;
	'	inline mstudiomouth_t *pMouth( int i ) const { return (mstudiomouth_t *)(((byte *)this) + mouthindex) + i; };
	'
	'	int					numposeparameters;
	'	int					poseparamindex;
	'	inline mstudioposeparamdesc_t *pPoseParameter( int i ) const { return (mstudioposeparamdesc_t *)(((byte *)this) + poseparamindex) + i; };
	'
	'	int					surfacepropindex;
	'	inline char * const pszSurfaceProp( void ) const { return ((char *)this) + surfacepropindex; }
	'
	'// f64: Unknown
	'	int					numunk;
	'	int					unkindex;
	'	inline mstudiounk_t *pUnk( int i ) const { return (mstudiounk_t *)(((byte *)this) + unkindex) + i; };
	'
	'	// external animations, models, etc. 
	'	int					numincludemodels;
	'	int					includemodelindex;
	'	inline mstudiomodelgroup_t *pModelGroup( int i ) const { return (mstudiomodelgroup_t *)(((byte *)this) + includemodelindex) + i; };
	'
	'
	'	int					unhz[3];
	'// ----
	'
	'// f64: -
	'//	// Key values
	'//	int					keyvalueindex;
	'//	int					keyvaluesize;
	'//	inline const char * KeyValueText( void ) const { return keyvaluesize != 0 ? ((char *)this) + keyvalueindex : NULL; }
	'
	'//	int					numikautoplaylocks;
	'//	int					ikautoplaylockindex;
	'//	inline mstudioiklock_t *pIKAutoplayLock( int i ) const { return (mstudioiklock_t *)(((byte *)this) + ikautoplaylockindex) + i; };
	'
	'//	float				mass;				// The collision model mass that jay wanted
	'//	int					contents;
	'//	int					unused[9];		// remove as appropriate
	'};


	'Public id(3) As Char
	'Public version As Integer
	'Public checksum As Integer
	Public name(127) As Char
	'Public fileSize As Integer

	Public eyePosition As New SourceVector()
	Public illuminationPosition As New SourceVector()

	Public unknown01 As Double
	Public unknown02 As Double
	Public unknown03 As Double

	Public hullMinPosition As New SourceVector()
	Public hullMaxPosition As New SourceVector()
	Public viewBoundingBoxMinPosition As New SourceVector()
	Public viewBoundingBoxMaxPosition As New SourceVector()

	Public flags As Integer

	Public unknown04 As Integer
	Public unknown05 As Integer

	Public boneCount As Integer
	Public boneOffset As Integer
	Public boneControllerCount As Integer
	Public boneControllerOffset As Integer

	Public hitBoxSetCount As Integer
	Public hitBoxSetOffset As Integer

	Public localAnimationCount As Integer
	Public localAnimationOffset As Integer
	Public localSequenceCount As Integer
	Public localSequenceOffset As Integer
	Public sequencesIndexedFlag As Integer
	Public sequenceGroupCount As Integer
	Public sequenceGroupOffset As Integer

	Public textureCount As Integer
	Public textureOffset As Integer
	Public texturePathCount As Integer
	Public texturePathOffset As Integer
	Public skinReferenceCount As Integer
	Public skinFamilyCount As Integer
	Public skinOffset As Integer

	Public bodyPartCount As Integer
	Public bodyPartOffset As Integer

	Public localAttachmentCount As Integer
	Public localAttachmentOffset As Integer

	Public transitionCount As Integer
	Public transitionOffset As Integer

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

	Public unknownCount As Integer
	Public unknownOffset As Integer

	'Public localIkAutoPlayLockCount As Integer
	'Public localIkAutoPlayLockOffset As Integer

	''	int AnimblockNameIndex;
	'Public animBlockNameOffset As Integer

	''	int NumFlexControllerUI;
	''	int FlexControllerUIIndex;
	'Public flexControllerUiCount As Integer
	'Public flexControllerUiOffset As Integer

	Public includeModelCount As Integer
	Public includeModelOffset As Integer

	Public unknown06 As Integer
	Public unknown07 As Integer
	Public unknown08 As Integer

	'Public boneTableByNameOffset As Integer

	''	int NumAnimblocks;
	''	int AnimblockIndex;
	'Public animBlockCount As Integer
	'Public animBlockOffset As Integer

	''	byte ConstDirectionalLightDOT;
	''	byte RootLOD;
	''	byte NumAllowedRootLODs;
	''	byte				unused[1];
	'Public directionalLightDot As Byte
	'Public rootLod As Byte
	'Public allowedRootLodCount As Byte
	'Public unused As Byte



	'Public theID As String
	'Public theName As String

	Public theAnimationDescs As List(Of SourceMdlAnimationDesc2531)
	Public theAttachments As List(Of SourceMdlAttachment2531)
	Public theBodyParts As List(Of SourceMdlBodyPart2531)
	Public theBones As List(Of SourceMdlBone2531)
	Public theBoneControllers As List(Of SourceMdlBoneController2531)
	Public theFlexDescs As List(Of SourceMdlFlexDesc)
	Public theFlexControllers As List(Of SourceMdlFlexController)
	'Public theFlexControllerUis As List(Of SourceMdlFlexControllerUi)
	Public theFlexRules As List(Of SourceMdlFlexRule)
	Public theHitboxSets As List(Of SourceMdlHitboxSet2531)
	Public theIncludeModels As List(Of SourceMdlIncludeModel2531)
	'Public theNodes As List(Of SourceMdlNode2531)
	Public thePoseParamDescs As List(Of SourceMdlPoseParamDesc2531)
	'Public theSequenceGroupFileHeaders As List(Of SourceMdlSequenceGroupFileHeader2531)
	Public theSequenceGroups As List(Of SourceMdlSequenceGroup2531)
	Public theSequences As List(Of SourceMdlSequenceDesc2531)
	Public theSkinFamilies As List(Of List(Of Short))
	Public theSurfacePropName As String
	Public theTexturePaths As List(Of String)
	Public theTextures As List(Of SourceMdlTexture2531)

	Public theFlexFrames As List(Of FlexFrame2531)
	Public theModelCommandIsUsed As Boolean
	Public theProceduralBonesCommandIsUsed As Boolean

	Public theBoneNameToBoneIndexMap As New SortedList(Of String, Integer)()

	'#define STUDIOHDR_FLAGS_STATIC_PROP				( 1 << 4 )
	Public Const STUDIOHDR_FLAGS_STATIC_PROP As Integer = 1 << 4

End Class
