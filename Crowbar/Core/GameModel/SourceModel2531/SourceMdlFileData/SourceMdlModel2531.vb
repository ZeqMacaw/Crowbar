Public Class SourceMdlModel2531

	Public Sub New()
		'MyBase.New()

		Me.theSmdFileNames = New List(Of String)(MAX_NUM_LODS)
		For i As Integer = 0 To MAX_NUM_LODS - 1
			Me.theSmdFileNames.Add("")
		Next
	End Sub

	'FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
	'struct mstudiomodel_t
	'{
	'	char				name[128];	// f64: ~
	'
	'	int					type;
	'
	'	float				boundingradius;
	'
	'	int					nummeshes;	
	'	int					meshindex;
	'	inline mstudiomesh_t *pMesh( int i ) const { return (mstudiomesh_t *)(((byte *)this) + meshindex) + i; };
	'
	'	// cache purposes
	'	int					numvertices;		// number of unique vertices/normals/texcoords
	'	int					vertexindex;		// vertex Vector
	'	int					tangentsindex;		// tangents Vector
	'
	'	int					filetype;	// f64: for type vertex
	'
	'	Vector				*Position( int i ) const;
	'	Vector				*Normal( int i ) const;
	'	Vector4D			*TangentS( int i ) const;
	'	Vector2D			*Texcoord( int i ) const;
	'	mstudioboneweight_t *BoneWeights( int i ) const;
	'	mstudiovertex_t		*Vertex( int i ) const;
	'
	'// f64: add new structs..
	'	mstudiovertex0_t	*pVertex0( int i ) const;	// behar: +
	'	mstudiovertex1_t	*pVertex1( int i ) const;
	'	mstudiovertex2_t	*pVertex2( int i ) const;
	'
	'	float				unkvect[6];
	'
	'	int					unk[2];
	'
	'	int					unknum;
	'	int					unkindex;
	'
	'	int					hz[2];
	'// ----
	'	int					numattachments;
	'	int					attachmentindex;
	'
	'	int					numeyeballs;
	'	int					eyeballindex;
	'	inline  mstudioeyeball_t *pEyeball( int i ) { return (mstudioeyeball_t *)(((byte *)this) + eyeballindex) + i; };
	'
	'//	int					unused[8];		// remove as appropriate	// f64: -
	'};

	Public name(127) As Char
	Public type As Integer
	Public boundingRadius As Double

	Public meshCount As Integer
	Public meshOffset As Integer
	Public vertexCount As Integer
	Public vertexOffset As Integer
	Public tangentOffset As Integer

	Public vertexListType As Integer

	Public unknown01(2) As Double
	'Public unknownCount As Integer
	'Public unknownOffset As Integer

	Public unknown02(2) As Double
	'Public unknown03(5) As Integer

	Public attachmentCount As Integer
	Public attachmentOffset As Integer
	Public eyeballCount As Integer
	Public eyeballOffset As Integer
	Public unknown03(1) As Integer

	Public unknown01Count As Integer
	Public unknown01Offset As Integer
	Public unknown02Count As Integer
	Public unknown02Offset As Integer


	Public theSmdFileNames As List(Of String)
	Public theName As String
	Public theEyeballs As List(Of SourceMdlEyeball2531)
	Public theMeshes As List(Of SourceMdlMesh2531)
	Public theTangents As List(Of SourceMdlTangent2531)
	Public theVertexesType0 As List(Of SourceMdlType0Vertex2531)
	Public theVertexesType1 As List(Of SourceMdlType1Vertex2531)
	Public theVertexesType2 As List(Of SourceMdlType2Vertex2531)


	'FROM: VAMPTools-master from atrblizzard\VAMPTools-master\MDLConverter\inc\external\studio.h
	' For vertexListType.
	'enum
	'{
	'	VLIST_TYPE_SKINNED = 0,
	'	VLIST_TYPE_UNSKINNED = 1,
	'	VLIST_TYPE_COMPRESSED = 2,
	'};
	Public Const VLIST_TYPE_SKINNED As Integer = &H0
	Public Const VLIST_TYPE_UNSKINNED As Integer = &H1
	Public Const VLIST_TYPE_COMPRESSED As Integer = &H2

End Class
