Public Class SourceVtxBodyPart107

	'FROM: SourceEngine2003_source HL2 Beta 2003\src_main\common\optimize.h
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



	Public theVtxModels As List(Of SourceVtxModel107)

End Class
