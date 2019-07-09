Public Class SourceMdlModel

	Public Sub New()
		'MyBase.New()

		Me.theSmdFileNames = New List(Of String)(MAX_NUM_LODS)
		For i As Integer = 0 To MAX_NUM_LODS - 1
			Me.theSmdFileNames.Add("")
		Next
	End Sub

	'struct mstudiomodel_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	inline const char * pszName( void ) const { return name; }
	'	char				name[64];

	'	int					type;

	'	float				boundingradius;

	'	int					nummeshes;	
	'	int					meshindex;
	'	inline mstudiomesh_t *pMesh( int i ) const { return (mstudiomesh_t *)(((byte *)this) + meshindex) + i; };

	'	// cache purposes
	'	int					numvertices;		// number of unique vertices/normals/texcoords
	'	int					vertexindex;		// vertex Vector
	'	int					tangentsindex;		// tangents Vector

	'	// These functions are defined in application-specific code:
	'	const vertexFileHeader_t			*CacheVertexData(			void *pModelData );

	'	// Access thin/fat mesh vertex data (only one will return a non-NULL result)
	'	const mstudio_modelvertexdata_t		*GetVertexData(		void *pModelData = NULL );
	'	const thinModelVertices_t			*GetThinVertexData(	void *pModelData = NULL );

	'	int					numattachments;
	'	int					attachmentindex;

	'	int					numeyeballs;
	'	int					eyeballindex;
	'	inline  mstudioeyeball_t *pEyeball( int i ) { return (mstudioeyeball_t *)(((byte *)this) + eyeballindex) + i; };

	'	mstudio_modelvertexdata_t vertexdata;

	'	int					unused[8];		// remove as appropriate
	'};

	'	char				name[64];
	Public name(63) As Char
	'	int					type;
	Public type As Integer
	'	float				boundingradius;
	Public boundingRadius As Single
	'	int					nummeshes;	
	Public meshCount As Integer
	'	int					meshindex;
	Public meshOffset As Integer

	'	int					numvertices;		// number of unique vertices/normals/texcoords
	Public vertexCount As Integer
	'	int					vertexindex;		// vertex Vector
	Public vertexOffset As Integer
	'	int					tangentsindex;		// tangents Vector
	Public tangentOffset As Integer
	'	int					numattachments;
	Public attachmentCount As Integer
	'	int					attachmentindex;
	Public attachmentOffset As Integer

	'	int					numeyeballs;
	Public eyeballCount As Integer
	'	int					eyeballindex;
	Public eyeballOffset As Integer

	'	mstudio_modelvertexdata_t vertexdata;
	Public vertexData As SourceMdlModelVertexData

	'	int					unused[8];		// remove as appropriate
	Public unused(7) As Integer


	Public theSmdFileNames As List(Of String)
	Public theMeshes As List(Of SourceMdlMesh)
	Public theEyeballs As List(Of SourceMdlEyeball)

End Class
