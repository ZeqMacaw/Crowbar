Public Class SourceMdlMesh2531

	'FROM: VAMPTools-master\MDLConverter\inc\external\studio.h
	'struct StudioMesh
	'{
	'	int	Material;
	'
	'	int ModelIndex;
	'	StudioModel *GetModel() const; 
	'
	'	// number of unique vertices/normals/texcoords
	'	int NumVertices;
	'	// vertex mstudiovertex_t
	'	int VertexOffset;
	'
	'	// Access thin/fat mesh vertex data (only one will return a non-NULL result)
	'	const MeshVertexData *GetVertexData( void *ModelData = 0 );
	'	const ThinModelVertices *GetThinVertexData(	void *ModelData = 0 );
	'
	'	// vertex animation
	'	int	NumFlexes;
	'	int FlexIndex;
	'	StudioFlex *GetFlex( int Index ) const;
	'
	'	// - BKH - Sep 7, 2012 - Best guess at which vars aren't used?
	'	// special codes for material operations
	'	//int MaterialType;
	'	//int MaterialParam;
	'
	'	// a unique ordinal for this mesh
	'	//int MeshID;
	'
	'	//Vector Center;
	'
	'	MeshVertexData VertexData;
	'
	'	// BKH - Offsets don't match in data
	'	//int	unused[8]; // remove as appropriate
	'};

	Public materialIndex As Integer
	Public modelOffset As Integer

	Public vertexCount As Integer
	Public vertexIndexStart As Integer

	Public flexCount As Integer
	Public flexOffset As Integer

	'Public materialType As Integer
	'Public materialParam As Integer

	'Public id As Integer

	'Public centerX As Single
	'Public centerY As Single
	'Public centerZ As Single

	Public vertexData As SourceMdlMeshVertexData

	'Public unused(7) As Integer


	Public theFlexes As List(Of SourceMdlFlex2531)

End Class
