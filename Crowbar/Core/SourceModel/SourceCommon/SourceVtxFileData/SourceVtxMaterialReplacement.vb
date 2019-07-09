Public Class SourceVtxMaterialReplacement

	'FROM: src/public/optimize.h
	'struct MaterialReplacementHeader_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	short materialID;
	'	int replacementMaterialNameOffset;
	'	inline const char *pMaterialReplacementName( void )
	'	{
	'		const char *pDebug = (const char *)(((byte *)this) + replacementMaterialNameOffset); 
	'		return pDebug;
	'	}
	'};

	Public materialId As Short
	Public replacementMaterialNameOffset As Integer

	Public replacementMaterialName As String

End Class
