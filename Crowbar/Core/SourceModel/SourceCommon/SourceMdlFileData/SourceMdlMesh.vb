Public Class SourceMdlMesh

	'struct mstudiomesh_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int					material;

	'	int					modelindex;
	'	mstudiomodel_t *pModel() const; 

	'	int					numvertices;		// number of unique vertices/normals/texcoords
	'	int					vertexoffset;		// vertex mstudiovertex_t

	'	// Access thin/fat mesh vertex data (only one will return a non-NULL result)
	'	const mstudio_meshvertexdata_t	*GetVertexData(		void *pModelData = NULL );
	'	const thinModelVertices_t		*GetThinVertexData(	void *pModelData = NULL );

	'	int					numflexes;			// vertex animation
	'	int					flexindex;
	'	inline mstudioflex_t *pFlex( int i ) const { return (mstudioflex_t *)(((byte *)this) + flexindex) + i; };

	'	// special codes for material operations
	'	int					materialtype;
	'	int					materialparam;

	'	// a unique ordinal for this mesh
	'	int					meshid;

	'	Vector				center;

	'	mstudio_meshvertexdata_t vertexdata;

	'	int					unused[8]; // remove as appropriate

	'	mstudiomesh_t(){}
	'private:
	'	// No copy constructors allowed
	'	mstudiomesh_t(const mstudiomesh_t& vOther);
	'};

	'	int					material;
	Public materialIndex As Integer

	'	int					modelindex;
	Public modelOffset As Integer

	'	int					numvertices;		// number of unique vertices/normals/texcoords
	Public vertexCount As Integer
	'	int					vertexoffset;		// vertex mstudiovertex_t
	Public vertexIndexStart As Integer

	'	int					numflexes;			// vertex animation
	Public flexCount As Integer
	'	int					flexindex;
	Public flexOffset As Integer

	'	int					materialtype;
	Public materialType As Integer
	'	int					materialparam;
	Public materialParam As Integer

	'	int					meshid;
	Public id As Integer

	'	Vector				center;
	Public centerX As Single
	Public centerY As Single
	Public centerZ As Single

	'	mstudio_meshvertexdata_t vertexdata;
	Public vertexData As SourceMdlMeshVertexData

	'	int					unused[8]; // remove as appropriate
	Public unused(7) As Integer


	Public theFlexes As List(Of SourceMdlFlex)
	'Public theVertexIds As List(Of Byte)
	'Public theVertexes As List(Of StudioVertex)

End Class
