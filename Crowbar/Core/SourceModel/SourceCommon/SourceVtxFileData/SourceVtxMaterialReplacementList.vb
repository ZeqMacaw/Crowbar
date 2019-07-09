Public Class SourceVtxMaterialReplacementList

	'FROM: src/public/optimize.h
	'struct MaterialReplacementListHeader_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int numReplacements;
	'	int replacementOffset;
	'	inline MaterialReplacementHeader_t *pMaterialReplacement( int i ) const
	'	{
	'		MaterialReplacementHeader_t *pDebug = ( MaterialReplacementHeader_t *)(((byte *)this) + replacementOffset) + i; 
	'		return pDebug;
	'	}
	'};

	Public replacementCount As Integer
	Public replacementOffset As Integer

End Class
