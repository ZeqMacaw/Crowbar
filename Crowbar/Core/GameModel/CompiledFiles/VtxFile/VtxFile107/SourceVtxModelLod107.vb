Public Class SourceVtxModelLod107

	'FROM: SourceEngine2003_source HL2 Beta 2003\src_main\common\optimize.h
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
	' Is this the "threshold" value for the QC file's $lod command? Seems to be, based on MDLDecompiler's conversion of producer.mdl.
	Public switchPoint As Double



	Public theVtxMeshes As List(Of SourceVtxMesh107)

End Class
