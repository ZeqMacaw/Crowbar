Public Class SourceVtxBodyPart06

	'FROM: The Axel Project - source [MDL v37]\TAPSRC\src\public\optimize.h
	'struct BodyPartHeader_t
	'{
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

	Public theVtxModels As List(Of SourceVtxModel06)

End Class
