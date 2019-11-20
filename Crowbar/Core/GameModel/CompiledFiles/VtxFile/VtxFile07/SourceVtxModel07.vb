Public Class SourceVtxModel07

	'FROM: src/public/optimize.h
	'// This maps one to one with models in the mdl file.
	'// There are a bunch of model LODs stored inside potentially due to the qc $lod command
	'struct ModelHeader_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int numLODs; // garymcthack - this is also specified in FileHeader_t
	'	int lodOffset;
	'	inline ModelLODHeader_t *pLOD( int i ) const 
	'	{ 
	'		ModelLODHeader_t *pDebug = ( ModelLODHeader_t *)(((byte *)this) + lodOffset) + i; 
	'		return pDebug;
	'	};
	'};

	Public lodCount As Integer
	Public lodOffset As Integer



	Public theVtxModelLods As List(Of SourceVtxModelLod07)

End Class
