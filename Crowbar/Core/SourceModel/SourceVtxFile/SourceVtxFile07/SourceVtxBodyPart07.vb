Public Class SourceVtxBodyPart07

	'FROM: src/public/optimize.h
	'struct BodyPartHeader_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int numModels;
	'	int modelOffset;
	'	inline ModelHeader_t *pModel( int i ) const 
	'	{ 
	'		ModelHeader_t *pDebug = (ModelHeader_t *)(((byte *)this) + modelOffset) + i;
	'		return pDebug;
	'	};
	'};

	Public modelCount As Integer
	Public modelOffset As Integer



	Public theVtxModels As List(Of SourceVtxModel07)

End Class
