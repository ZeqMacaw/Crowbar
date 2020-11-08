Public Class SourceMdlFileData06
	Inherits SourceMdlFileDataBase

	Public Sub New()
		MyBase.New()

		Me.theChecksumIsValid = False
	End Sub

	'Public Sub New()
	'	Me.eyePosition = New SourceVector()
	'	Me.hullMinPosition = New SourceVector()
	'	Me.hullMaxPosition = New SourceVector()
	'	Me.viewBoundingBoxMinPosition = New SourceVector()
	'	Me.viewBoundingBoxMaxPosition = New SourceVector()
	'End Sub

	'FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
	'#define STUDIO_VERSION		6
	'// little-endian "IDST"
	'#define IDSTUDIOHEADER	(('T'<<24)+('S'<<16)+('D'<<8)+'I')
	'typedef struct
	'{
	'	int					id;
	'	int					version;
	'
	'	char				name[64];
	'	// 48h
	'	int					length;
	'
	'	// 4Ch
	'	int					numbones;				// bones
	'	int					boneindex;				// (->BCh)
	'
	'	// 54h
	'	int					numbonecontrollers;		// bone controllers
	'	// TOMAS: turret.mdl has 2
	'	int					bonecontrollerindex;	// if num == 0 then this points to bones! not controlers!
	'
	'	// 5Ch
	'	int					numseq;					// animation sequences
	'	int					seqindex;
	'
	'	// 64h
	'	int					numtextures;			// raw textures
	'	int					textureindex;
	'	int					texturedataindex;
	'
	'	// 70h
	'	int					numskinref;				// replaceable textures
	'	int					numskinfamilies;
	'	int					skinindex;
	'
	'	// 7Ch
	'	int					numbodyparts;
	'	int					bodypartindex;			// (->mstudiobodyparts_t)
	'
	'	int					unused[14];				// TOMAS: UNUSED (checked)
	'
	'} studiohdr_t;

	'Public id(3) As Char
	'Public version As Integer
	Public name(63) As Char
	'' length of mdl file in bytes
	'Public fileSize As Integer

	Public boneCount As Integer
	Public boneOffset As Integer

	Public boneControllerCount As Integer
	Public boneControllerOffset As Integer

	Public sequenceCount As Integer
	Public sequenceOffset As Integer

	Public textureCount As Integer
	Public textureOffset As Integer
	Public textureDataOffset As Integer

	Public skinReferenceCount As Integer
	Public skinFamilyCount As Integer
	Public skinOffset As Integer

	Public bodyPartCount As Integer
	Public bodyPartOffset As Integer

	Public unused(13) As Integer


	'Public theID As String
	'Public theName As String

	Public theBodyParts As List(Of SourceMdlBodyPart06)
	Public theBones As List(Of SourceMdlBone06)
	Public theBoneControllers As List(Of SourceMdlBoneController06)
	Public theSequences As List(Of SourceMdlSequenceDesc06)
	Public theSkins As List(Of List(Of Short))
	Public theTextures As List(Of SourceMdlTexture06)

	Public theBoneTransforms As List(Of SourceBoneTransform06)

End Class
