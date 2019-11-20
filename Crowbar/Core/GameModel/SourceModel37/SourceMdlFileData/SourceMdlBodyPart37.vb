Public Class SourceMdlBodyPart37

	'struct mstudiobodyparts_t
	'{
	'	int	sznameindex;
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	'	int	nummodels;
	'	int	base;
	'	int	modelindex; // index into models array
	'	inline mstudiomodel_t *pModel( int i ) const { return (mstudiomodel_t *)(((byte *)this) + modelindex) + i; };
	'};

	Public nameOffset As Integer
	Public modelCount As Integer
	Public base As Integer
	Public modelOffset As Integer

	Public theModels As List(Of SourceMdlModel37)
	Public theName As String

End Class
