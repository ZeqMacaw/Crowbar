Public Class SourceMdlBodyPart2531

	'FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
	'struct mstudiobodyparts_t
	'{
	'	int					sznameindex;
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	'	int					nummodels;
	'	int					base;
	'	int					modelindex; // index into models array
	'	inline mstudiomodel_t *pModel( int i ) const { return (mstudiomodel_t *)(((byte *)this) + modelindex) + i; };
	'};

	Public nameOffset As Integer
	Public modelCount As Integer
	Public base As Integer
	Public modelOffset As Integer


	Public theName As String
	Public theModels As List(Of SourceMdlModel2531)

End Class
