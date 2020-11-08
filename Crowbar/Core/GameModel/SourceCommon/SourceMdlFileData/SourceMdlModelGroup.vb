Public Class SourceMdlModelGroup

	'// demand loaded sequence groups
	'struct mstudiomodelgroup_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int					szlabelindex;	// textual name
	'	inline char * const pszLabel( void ) const { return ((char *)this) + szlabelindex; }
	'	int					sznameindex;	// file name
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	'};

	'	int					szlabelindex;	// textual name
	Public labelOffset As Integer
	'	int					sznameindex;	// file name
	Public fileNameOffset As Integer

	Public theLabel As String
	Public theFileName As String



	Public theMdlFileData As SourceMdlFileData

End Class
