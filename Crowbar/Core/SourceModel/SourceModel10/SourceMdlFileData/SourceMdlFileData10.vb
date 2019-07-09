Public Class SourceMdlFileData10
	Inherits SourceMdlFileDataBase

	Public Sub New()
		Me.theChecksumIsValid = False

		Me.eyePosition = New SourceVector()
		Me.hullMinPosition = New SourceVector()
		Me.hullMaxPosition = New SourceVector()
		Me.viewBoundingBoxMinPosition = New SourceVector()
		Me.viewBoundingBoxMaxPosition = New SourceVector()
	End Sub

	'FROM: GoldSourceEngine2002_source\halflife-master\utils\studiomdl\studiomdl.h
	'#define STUDIO_VERSION	10
	'#define IDSTUDIOHEADER	(('T'<<24)+('S'<<16)+('D'<<8)+'I')
	'														// little-endian "IDST"

	'FROM: GoldSourceEngine2002_source\halflife-master\engine\studio.h
	'#define MAXSTUDIOTRIANGLES	20000	// TODO: tune this
	'#define MAXSTUDIOVERTS		2048	// TODO: tune this
	'#define MAXSTUDIOSEQUENCES	2048	// total animation sequences -- KSH incremented
	'#define MAXSTUDIOSKINS		100		// total textures
	'#define MAXSTUDIOSRCBONES	512		// bones allowed at source movement
	'#define MAXSTUDIOBONES		128		// total bones actually used
	'#define MAXSTUDIOMODELS		32		// sub-models per model
	'#define MAXSTUDIOBODYPARTS	32
	'#define MAXSTUDIOGROUPS		16
	'#define MAXSTUDIOANIMATIONS	2048		
	'#define MAXSTUDIOMESHES		256
	'#define MAXSTUDIOEVENTS		1024
	'#define MAXSTUDIOPIVOTS		256
	'#define MAXSTUDIOCONTROLLERS 8

	'FROM: GoldSourceEngine2002_source\halflife-master\engine\studio.h
	'typedef struct 
	'{
	'	int					id;
	'	int					version;
	'
	'	char				name[64];
	'	int					length;
	'
	'	vec3_t				eyeposition;	// ideal eye position
	'	vec3_t				min;			// ideal movement hull size
	'	vec3_t				max;			
	'
	'	vec3_t				bbmin;			// clipping bounding box
	'	vec3_t				bbmax;		
	'
	'	int					flags;
	'
	'	int					numbones;			// bones
	'	int					boneindex;
	'
	'	int					numbonecontrollers;		// bone controllers
	'	int					bonecontrollerindex;
	'
	'	int					numhitboxes;			// complex bounding boxes
	'	int					hitboxindex;			
	'
	'	int					numseq;				// animation sequences
	'	int					seqindex;
	'
	'	int					numseqgroups;		// demand loaded sequences
	'	int					seqgroupindex;
	'
	'	int					numtextures;		// raw textures
	'	int					textureindex;
	'	int					texturedataindex;
	'
	'	int					numskinref;			// replaceable textures
	'	int					numskinfamilies;
	'	int					skinindex;
	'
	'	int					numbodyparts;		
	'	int					bodypartindex;
	'
	'	int					numattachments;		// queryable attachable points
	'	int					attachmentindex;
	'
	'	int					soundtable;
	'	int					soundindex;
	'	int					soundgroups;
	'	int					soundgroupindex;
	'
	'	int					numtransitions;		// animation node to animation node transition graph
	'	int					transitionindex;
	'} studiohdr_t;



	'Public id(3) As Char
	'Public version As Integer

	Public name(63) As Char
	'Public fileSize As Integer

	'	vec3_t				eyeposition;	// ideal eye position
	Public eyePosition As SourceVector
	'	vec3_t				min;			// ideal movement hull size
	Public hullMinPosition As SourceVector
	'	vec3_t				max;			
	Public hullMaxPosition As SourceVector

	'	vec3_t				bbmin;			// clipping bounding box
	Public viewBoundingBoxMinPosition As SourceVector
	'	vec3_t				bbmax;		
	Public viewBoundingBoxMaxPosition As SourceVector

	'	int					flags;
	Public flags As Integer

	'	int					numbones;			// bones
	Public boneCount As Integer
	'	int					boneindex;
	Public boneOffset As Integer

	'	int					numbonecontrollers;		// bone controllers
	Public boneControllerCount As Integer
	'	int					bonecontrollerindex;
	Public boneControllerOffset As Integer

	'	int					numhitboxes;			// complex bounding boxes
	Public hitboxCount As Integer
	'	int					hitboxindex;			
	Public hitboxOffset As Integer

	'	int					numseq;				// animation sequences
	Public sequenceCount As Integer
	'	int					seqindex;
	Public sequenceOffset As Integer

	'	int					numseqgroups;		// demand loaded sequences
	Public sequenceGroupCount As Integer
	'	int					seqgroupindex;
	Public sequenceGroupOffset As Integer

	'	int					numtextures;		// raw textures
	Public textureCount As Integer
	'	int					textureindex;
	Public textureOffset As Integer
	'	int					texturedataindex;
	Public textureDataOffset As Integer

	'	int					numskinref;			// replaceable textures
	Public skinReferenceCount As Integer
	'	int					numskinfamilies;
	Public skinFamilyCount As Integer
	'	int					skinindex;
	Public skinOffset As Integer

	'	int					numbodyparts;		
	Public bodyPartCount As Integer
	'	int					bodypartindex;
	Public bodyPartOffset As Integer

	'	int					numattachments;		// queryable attachable points
	Public attachmentCount As Integer
	'	int					attachmentindex;
	Public attachmentOffset As Integer

	'	int					soundtable;
	'	int					soundindex;
	'	int					soundgroups;
	'	int					soundgroupindex;
	Public soundTable As Integer
	Public soundOffset As Integer
	Public soundGroups As Integer
	Public soundGroupOffset As Integer

	'	int					numtransitions;		// animation node to animation node transition graph
	'	int					transitionindex;
	Public transitionCount As Integer
	Public transitionOffset As Integer



	'Public theID As String
	'Public theName As String

	Public theAttachments As List(Of SourceMdlAttachment10)
	Public theBodyParts As List(Of SourceMdlBodyPart10)
	Public theBones As List(Of SourceMdlBone10)
	'Public theBones As List(Of SourceMdlBone10Single)
	Public theBoneControllers As List(Of SourceMdlBoneController10)
	Public theHitboxes As List(Of SourceMdlHitbox10)
	Public theSequences As List(Of SourceMdlSequenceDesc10)
	Public theSequenceGroupFileHeaders As List(Of SourceMdlSequenceGroupFileHeader10)
	Public theSequenceGroups As List(Of SourceMdlSequenceGroup10)
	Public theSkinFamilies As List(Of List(Of Short))
	Public theTextures As List(Of SourceMdlTexture10)
	Public theTransitions As List(Of List(Of Byte))

	Public theBoneTransforms As List(Of SourceBoneTransform10)
	'Public theBoneTransforms As List(Of SourceBoneTransform10Single)

	Public theSmdFileNames As List(Of String)

End Class
