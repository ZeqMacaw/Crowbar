Public Class SourceMdlModel37

	Public Sub New()
		'MyBase.New()

		Me.theSmdFileNames = New List(Of String)(MAX_NUM_LODS)
		For i As Integer = 0 To MAX_NUM_LODS - 1
			Me.theSmdFileNames.Add("")
		Next
	End Sub

	'struct mstudiomodel_t
	'{
	'	char		name[64];
	'
	'	int		type;
	'
	'	float		boundingradius;
	'
	'	int		nummeshes;
	'	int		meshindex;
	'	inline mstudiomesh_t *pMesh( int i ) const { return (mstudiomesh_t *)(((byte *)this) + meshindex) + i; };
	'
	'	// cache purposes
	'	int		numvertices;		// number of unique vertices/normals/texcoords
	'	int		vertexindex;		// vertex Vector
	'	int		tangentsindex;		// tangents Vector
	'
	'	Vector		*Position( int i ) const;
	'	Vector		*Normal( int i ) const;
	'	Vector4D	*TangentS( int i ) const;
	'	Vector2D	*Texcoord( int i ) const;
	'	mstudioboneweight_t 	*BoneWeights( int i ) const;
	'	mstudiovertex_t		*Vertex( int i ) const;
	'
	'	int		numattachments;
	'	int		attachmentindex;
	'
	'	int		numeyeballs;
	'	int		eyeballindex;
	'	inline  mstudioeyeball_t *pEyeball( int i ) { return (mstudioeyeball_t *)(((byte *)this) + eyeballindex) + i; };
	'
	'	int		unused[8];		// remove as appropriate
	'};

	Public name(63) As Char
	Public type As Integer
	Public boundingRadius As Double
	Public meshCount As Integer
	Public meshOffset As Integer

	Public vertexCount As Integer
	Public vertexOffset As Integer
	Public tangentOffset As Integer

	Public attachmentCount As Integer
	Public attachmentOffset As Integer
	Public eyeballCount As Integer
	Public eyeballOffset As Integer

	Public unused(7) As Integer

	Public theSmdFileNames As List(Of String)
	Public theEyeballs As List(Of SourceMdlEyeball37)
	Public theMeshes As List(Of SourceMdlMesh37)
	Public theName As String
	Public theTangents As List(Of SourceVector4D)
	Public theVertexes As List(Of SourceMdlVertex37)

End Class
