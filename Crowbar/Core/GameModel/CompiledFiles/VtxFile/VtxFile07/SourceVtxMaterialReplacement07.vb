Public Class SourceVtxMaterialReplacement07

	'FROM: [48] SourceEngine2007_source se2007_src\src_main\public\optimize.h
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

	Public materialIndex As Short
	Public nameOffset As Integer

	Public theName As String

End Class
