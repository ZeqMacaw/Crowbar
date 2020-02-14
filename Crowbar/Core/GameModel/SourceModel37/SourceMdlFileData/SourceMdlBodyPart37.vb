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

	Public theName As String
	Public theModels As List(Of SourceMdlModel37)
	Public theModelCommandIsUsed As Boolean
	Public theEyeballOptionIsUsed As Boolean
	Public theFlexFrames As List(Of FlexFrame37)

End Class
