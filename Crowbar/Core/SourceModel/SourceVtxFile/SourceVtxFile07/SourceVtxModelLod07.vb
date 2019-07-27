Public Class SourceVtxModelLod07

	'FROM: src/public/optimize.h
	'struct ModelLODHeader_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
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
	' Is this the "threshold" value for the QC file's $lod command? Seems to be, based on MDLDecompiler's conversion of producer.mdl.
	Public switchPoint As Single



	Public theVtxMeshes As List(Of SourceVtxMesh07)
	Public theVtxModelLodUsesFacial As Boolean

End Class
