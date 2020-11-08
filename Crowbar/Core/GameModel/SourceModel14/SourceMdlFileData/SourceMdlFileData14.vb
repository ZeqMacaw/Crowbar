Public Class SourceMdlFileData14
	Inherits SourceMdlFileDataBase

	Public Sub New()
		Me.theChecksumIsValid = False

		Me.eyePosition = New SourceVector()
		Me.hullMinPosition = New SourceVector()
		Me.hullMaxPosition = New SourceVector()
		Me.viewBoundingBoxMinPosition = New SourceVector()
		Me.viewBoundingBoxMaxPosition = New SourceVector()
	End Sub

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

	Public unknown01 As Integer

	Public subModelCount As Integer

	Public vertexCount As Integer
	Public indexCount As Integer
	Public indexOffset As Integer
	Public vertexOffset As Integer
	Public normalOffset As Integer
	Public uvOffset As Integer
	Public unknown08 As Integer
	'Public unknown09 As Integer
	Public weightingWeightOffset As Integer
	'Public unknown10 As Integer
	Public weightingBoneOffset As Integer
	Public unknown11 As Integer

	Public subModelOffsets(47) As Integer


	'Public theID As String
	'Public theName As String

	Public theAttachments As List(Of SourceMdlAttachment10)
	Public theBodyParts As List(Of SourceMdlBodyPart14)
	Public theBones As List(Of SourceMdlBone10)
	'Public theBones As List(Of SourceMdlBone10Single)
	Public theBoneControllers As List(Of SourceMdlBoneController10)
	Public theHitboxes As List(Of SourceMdlHitbox10)
	Public theIndexes As List(Of UInt16)
	Public theNormals As List(Of SourceVector)
	Public theSequences As List(Of SourceMdlSequenceDesc10)
	Public theSequenceGroupFileHeaders As List(Of SourceMdlSequenceGroupFileHeader10)
	Public theSequenceGroups As List(Of SourceMdlSequenceGroup10)
	Public theSkinFamilies As List(Of List(Of Short))
	Public theTextures As List(Of SourceMdlTexture14)
	Public theTransitions As List(Of List(Of Byte))
	Public theUVs As List(Of SourceVector)
	Public theVertexes As List(Of SourceVector)
	Public theWeightings As List(Of SourceMdlWeighting14)

	Public theBoneTransforms As List(Of SourceBoneTransform10)
	'Public theBoneTransforms As List(Of SourceBoneTransform10Single)

	Public theSmdFileNames As List(Of String)

End Class
