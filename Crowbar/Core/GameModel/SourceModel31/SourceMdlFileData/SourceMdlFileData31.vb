Public Class SourceMdlFileData31
	Inherits SourceMdlFileDataBase

	Public Sub New()
		MyBase.New()

		Me.theModelCommandIsUsed = False
		'Me.theProceduralBonesCommandIsUsed = False
	End Sub

	'FROM: The Axel Project - source [MDL v37]\TAPSRC\src\Public\studio.h
	'struct studiohdr_t
	'{
	'	int	id;
	'	int	version;
	'
	'	long checksum;	// this has to be the same in the phy and vtx files to load!
	'
	'	char name[64];
	'	int	length;
	'
	'	Vector	eyeposition;	// ideal eye position
	'
	'	Vector	illumposition;	// illumination center
	'
	'	Vector	hull_min;	// ideal movement hull size
	'	Vector	hull_max;			
	'
	'	Vector	view_bbmin;	// clipping bounding box
	'	Vector	view_bbmax;	
	'
	'	int	flags;
	'
	'	int	numbones;	// bones
	'	int	boneindex;
	'	inline mstudiobone_t *pBone( int i ) const { return (mstudiobone_t *)(((byte *)this) + boneindex) + i; };
	'
	'	int	numbonecontrollers;	// bone controllers
	'	int	bonecontrollerindex;
	'	inline mstudiobonecontroller_t *pBonecontroller( int i ) const { return (mstudiobonecontroller_t *)(((byte *)this) + bonecontrollerindex) + i; };
	'
	'	int	numhitboxsets;
	'	int	hitboxsetindex; 
	'
	'	// Look up hitbox set by index
	'	mstudiohitboxset_t  *pHitboxSet( int i ) const 
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
	'	inline int  iHitboxCount( int set ) const
	'	{
	'		mstudiohitboxset_t const *s = pHitboxSet( set );
	'		if ( !s )
	'			return 0;
	'
	'		return s->numhitboxes;
	'	};
	'
	'	int	numanim;	// animations/poses
	'	int	animdescindex;	// animation descriptions
	'	inline mstudioanimdesc_t *pAnimdesc( int i ) const { return (mstudioanimdesc_t *)(((byte *)this) + animdescindex) + i; };
	'
	'	int 	numanimgroup;
	'	int 	animgroupindex;
	'	inline  mstudioanimgroup_t *pAnimGroup(int i) const	{ return (mstudioanimgroup_t *)(((byte *)this) + animgroupindex) + i; };
	'
	'	int 	numbonedesc;
	'	int 	bonedescindex;
	'	inline  mstudiobonedesc_t *pBoneDesc(int i) const { return (mstudiobonedesc_t *)(((byte *)this) + bonedescindex) + i; };
	'
	'	int	numseq;		// sequences
	'	int	seqindex;
	'	inline mstudioseqdesc_t *pSeqdesc( int i ) const { if (i < 0 || i >= numseq) i = 0; return (mstudioseqdesc_t *)(((byte *)this) + seqindex) + i; };
	'	int	sequencesindexed;	// initialization flag - have the sequences been indexed?
	'
	'	int	numseqgroups;		// demand loaded sequences
	'	int	seqgroupindex;
	'	inline  mstudioseqgroup_t *pSeqgroup(int i) const { return (mstudioseqgroup_t *)(((byte *)this) + seqgroupindex) + i; };
	'
	'	int	numtextures;		// raw textures
	'	int	textureindex;
	'	inline mstudiotexture_t *pTexture( int i ) const { return (mstudiotexture_t *)(((byte *)this) + textureindex) + i; }; 
	'
	'	int	numcdtextures;		// raw textures search paths
	'	int	cdtextureindex;
	'	inline char			*pCdtexture( int i ) const { return (((char *)this) + *((int *)(((byte *)this) + cdtextureindex) + i)); };
	'
	'	int	numskinref;		// replaceable textures tables
	'	int	numskinfamilies;
	'	int	skinindex;
	'	inline short		*pSkinref( int i ) const { return (short *)(((byte *)this) + skinindex) + i; };
	'
	'	int	numbodyparts;		
	'	int	bodypartindex;
	'	inline mstudiobodyparts_t	*pBodypart( int i ) const { return (mstudiobodyparts_t *)(((byte *)this) + bodypartindex) + i; };
	'
	'	int	numattachments;		// queryable attachable points
	'	int	attachmentindex;
	'	inline mstudioattachment_t	*pAttachment( int i ) const { return (mstudioattachment_t *)(((byte *)this) + attachmentindex) + i; };
	'
	'	int	numtransitions;		// animation node to animation node transition graph
	'	int	transitionindex;
	'	inline byte	*pTransition( int i ) const { return (byte *)(((byte *)this) + transitionindex) + i; };
	'
	'	int	numflexdesc;
	'	int	flexdescindex;
	'	inline mstudioflexdesc_t *pFlexdesc( int i ) const { return (mstudioflexdesc_t *)(((byte *)this) + flexdescindex) + i; };
	'
	'	int	numflexcontrollers;
	'	int	flexcontrollerindex;
	'	inline mstudioflexcontroller_t *pFlexcontroller( int i ) const { return (mstudioflexcontroller_t *)(((byte *)this) + flexcontrollerindex) + i; };
	'
	'	int	numflexrules;
	'	int	flexruleindex;
	'	inline mstudioflexrule_t *pFlexRule( int i ) const { return (mstudioflexrule_t *)(((byte *)this) + flexruleindex) + i; };
	'
	'	int	numikchains;
	'	int	ikchainindex;
	'	inline mstudioikchain_t *pIKChain( int i ) const { return (mstudioikchain_t *)(((byte *)this) + ikchainindex) + i; };
	'
	'	int	nummouths;
	'	int	mouthindex;
	'	inline mstudiomouth_t *pMouth( int i ) const { return (mstudiomouth_t *)(((byte *)this) + mouthindex) + i; };
	'
	'	int	numposeparameters;
	'	int	poseparamindex;
	'	inline mstudioposeparamdesc_t *pPoseParameter( int i ) const { return (mstudioposeparamdesc_t *)(((byte *)this) + poseparamindex) + i; };
	'
	'	int	surfacepropindex;
	'	inline char * const pszSurfaceProp( void ) const { return ((char *)this) + surfacepropindex; }
	'
	'	// Key values
	'	int	keyvalueindex;
	'	int	keyvaluesize;
	'	inline const char * KeyValueText( void ) const { return keyvaluesize != 0 ? ((char *)this) + keyvalueindex : NULL; }
	'
	'	int	numikautoplaylocks;
	'	int	ikautoplaylockindex;
	'	inline mstudioiklock_t *pIKAutoplayLock( int i ) const { return (mstudioiklock_t *)(((byte *)this) + ikautoplaylockindex) + i; };
	'
	'	float mass;		// The collision model mass that jay wanted
	'	int	contents;
	'	int	unused[9];	// remove as appropriate
	'};

	Public name(63) As Char

	Public eyePosition As New SourceVector()
	Public illuminationPosition As New SourceVector()

	Public hullMinPosition As New SourceVector()
	Public hullMaxPosition As New SourceVector()
	Public viewBoundingBoxMinPosition As New SourceVector()
	Public viewBoundingBoxMaxPosition As New SourceVector()

	Public flags As Integer

	Public boneCount As Integer
	Public boneOffset As Integer
	Public boneControllerCount As Integer
	Public boneControllerOffset As Integer

	' MDL27to29
	Public hitboxCount_MDL27to30 As Integer
	Public hitboxOffset_MDL27to30 As Integer
	'------
	' MDL31
	Public hitboxSetCount As Integer
	Public hitboxSetOffset As Integer

	Public animationCount As Integer
	Public animationOffset As Integer
	'Public animationGroupCount As Integer
	'Public animationGroupOffset As Integer

	'Public boneDescCount As Integer
	'Public boneDescOffset As Integer

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

	'Public keyValueOffset As Integer
	'Public keyValueSize As Integer

	'Public localIkAutoPlayLockCount As Integer
	'Public localIkAutoPlayLockOffset As Integer

	'Public mass As Double
	'Public contents As Integer

	'Public unused(8) As Integer

	Public theAnimationDescs As List(Of SourceMdlAnimationDesc31)
	'Public theAnimGroups As List(Of SourceMdlAnimGroup31)
	'Public theAttachments As List(Of SourceMdlAttachment37)
	Public theBodyParts As List(Of SourceMdlBodyPart31)
	'Public theBoneControllers As List(Of SourceMdlBoneController37)
	'Public theBoneDescs As List(Of SourceMdlBoneDesc37)
	Public theBones As List(Of SourceMdlBone31)
	'Public theFlexControllers As List(Of SourceMdlFlexController)
	'Public theFlexDescs As List(Of SourceMdlFlexDesc)
	'Public theFlexRules As List(Of SourceMdlFlexRule)
	Public theHitboxes_MDL27to30 As List(Of SourceMdlHitbox31)
	Public theHitboxSets As List(Of SourceMdlHitboxSet31)
	'Public theIkChains As List(Of SourceMdlIkChain37)
	'Public theIkLocks As List(Of SourceMdlIkLock37)
	'Public theMouths As List(Of SourceMdlMouth)
	'Public thePoseParamDescs As List(Of SourceMdlPoseParamDesc)
	Public theSequenceDescs As List(Of SourceMdlSequenceDesc31)
	Public theSequenceGroups As List(Of SourceMdlSequenceGroup31)
	Public theSkinFamilies As List(Of List(Of Short))
	Public theSurfacePropName As String
	Public theTexturePaths As List(Of String)
	Public theTextures As List(Of SourceMdlTexture31)
	'Public theTransitions As List(Of List(Of Integer))

	Public theModelCommandIsUsed As Boolean
	'Public theProceduralBonesCommandIsUsed As Boolean

	Public theBoneNameToBoneIndexMap As New SortedList(Of String, Integer)()
	'Public theEyelidFlexFrameIndexes As List(Of Integer)
	Public theFirstAnimationDesc As SourceMdlAnimationDesc31
	Public theFirstAnimationDescFrameLines As New SortedList(Of Integer, AnimationFrameLine)()
	'Public theFlexFrames As List(Of FlexFrame)
	'Public theWeightLists As New List(Of SourceMdlWeightList)()

End Class
