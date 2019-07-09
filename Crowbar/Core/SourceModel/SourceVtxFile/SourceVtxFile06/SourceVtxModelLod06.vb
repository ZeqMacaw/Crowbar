Public Class SourceVtxModelLod06

	'FROM: The Axel Project - source [MDL v37]\TAPSRC\src\public\optimize.h
	'struct ModelLODHeader_t
	'{
	'	int numMeshes;
	'	int meshOffset;
	'	float switchPoint;
	'	inline MeshHeader_t *pMesh( int i ) const 
	'	{ 
	'		MeshHeader_t *pDebug = (MeshHeader_t *)(((byte *)this) + meshOffset) + i; 
	'		return pDebug;
	'	};
	'};

	Public meshCount As Integer
	Public meshOffset As Integer
	Public switchPoint As Single

	Public theVtxMeshes As List(Of SourceVtxMesh06)
	Public theVtxModelLodUsesFacial As Boolean

End Class
