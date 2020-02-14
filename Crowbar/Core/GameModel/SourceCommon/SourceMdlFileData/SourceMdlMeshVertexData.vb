Public Class SourceMdlMeshVertexData

	'struct mstudio_meshvertexdata_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	Vector				*Position( int i ) const;
	'	Vector				*Normal( int i ) const;
	'	Vector4D			*TangentS( int i ) const;
	'	Vector2D			*Texcoord( int i ) const;
	'	mstudioboneweight_t *BoneWeights( int i ) const;
	'	mstudiovertex_t		*Vertex( int i ) const;
	'	bool				HasTangentData( void ) const;
	'	int					GetModelVertexIndex( int i ) const;
	'	int					GetGlobalVertexIndex( int i ) const;

	'	// indirection to this mesh's model's vertex data
	'	const mstudio_modelvertexdata_t	*modelvertexdata;

	'	// used for fixup calcs when culling top level lods
	'	// expected number of mesh verts at desired lod
	'	int					numLODVertexes[MAX_NUM_LODS];
	'};

	'	const mstudio_modelvertexdata_t	*modelvertexdata;
	Public modelVertexDataP As Integer

	'	int					numLODVertexes[MAX_NUM_LODS];
	'  MAX_NUM_LODS = 8
	Public lodVertexCount(MAX_NUM_LODS - 1) As Integer

End Class
