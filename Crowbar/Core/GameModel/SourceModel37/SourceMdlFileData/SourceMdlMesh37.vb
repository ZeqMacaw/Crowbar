Public Class SourceMdlMesh37

	'struct mstudiomesh_t
	'{
	'	int		material;
	'
	'	int		modelindex;
	'	mstudiomodel_t *pModel() const; // { return (mstudiomodel_t *)(((byte *)this) + modelindex); }
	'
	'	int		numvertices;		// number of unique vertices/normals/texcoords
	'	int		vertexoffset;		// vertex mstudiovertex_t
	'
	'	Vector		*Position( int i ) const;
	'	Vector		*Normal( int i ) const;
	'	Vector4D	*TangentS( int i ) const;
	'	Vector2D	*Texcoord( int i ) const;
	'	mstudioboneweight_t 	*BoneWeights( int i ) const;
	'	mstudiovertex_t	 	*Vertex( int i ) const;
	'
	'	int		numflexes;			// vertex animation
	'	int		flexindex;
	'	inline mstudioflex_t *pFlex( int i ) const { return (mstudioflex_t *)(((byte *)this) + flexindex) + i; };
	'
	'	//int		numresolutionupdates;
	'	//int		resolutionupdateindex;
	'
	'	//int		numfaceupdates;
	'	//int		faceupdateindex;
	'
	'	// special codes for material operations
	'	int		materialtype;
	'	int		materialparam;
	'
	'	// a unique ordinal for this mesh
	'	int		meshid;
	'
	'	Vector		center;
	'
	'	int		unused[5]; // remove as appropriate
	'};

	Public materialIndex As Integer
	Public modelOffset As Integer

	Public vertexCount As Integer
	Public vertexIndexStart As Integer
	Public flexCount As Integer
	Public flexOffset As Integer
	Public materialType As Integer
	Public materialParam As Integer

	Public id As Integer
	Public center As New SourceVector()
	Public unused(4) As Integer

	Public theFlexes As List(Of SourceMdlFlex37)

End Class
