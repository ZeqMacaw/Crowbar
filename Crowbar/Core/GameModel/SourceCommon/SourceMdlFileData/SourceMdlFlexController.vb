Public Class SourceMdlFlexController

	'struct mstudioflexcontroller_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int					sztypeindex;
	'	inline char * const pszType( void ) const { return ((char *)this) + sztypeindex; }
	'	int					sznameindex;
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	'	mutable int			localToGlobal;	// remapped at load time to master list
	'	float				min;
	'	float				max;
	'};

	'	int					sztypeindex;
	Public typeOffset As Integer
	'	int					sznameindex;
	Public nameOffset As Integer
	'	mutable int			localToGlobal;	// remapped at load time to master list
	Public localToGlobal As Integer
	'	float				min;
	Public min As Single
	'	float				max;
	Public max As Single

	Public theName As String
	Public theType As String

End Class
