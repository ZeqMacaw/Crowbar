Public Class SourceMdlBodyPart

	'struct mstudiobodyparts_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int					sznameindex;
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	'	int					nummodels;
	'	int					base;
	'	int					modelindex; // index into models array
	'	inline mstudiomodel_t *pModel( int i ) const { return (mstudiomodel_t *)(((byte *)this) + modelindex) + i; };
	'};

	'   offset from start of this struct
	'	int					sznameindex;
	Public nameOffset As Integer
	'	int					nummodels;
	Public modelCount As Integer
	'	int					base;
	Public base As Integer
	'	int					modelindex; // index into models array
	Public modelOffset As Integer

	Public theName As String
	Public theModels As List(Of SourceMdlModel)
	Public theModelCommandIsUsed As Boolean
	Public theEyeballOptionIsUsed As Boolean
	Public theFlexFrames As List(Of FlexFrame)

End Class
