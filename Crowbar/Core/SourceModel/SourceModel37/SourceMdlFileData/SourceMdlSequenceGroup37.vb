Public Class SourceMdlSequenceGroup37

	'struct mstudioseqgroup_t
	'{
	'	int	szlabelindex;	// textual name
	'	inline char * const pszLabel( void ) const { return ((char *)this) + szlabelindex; }
	'
	'	int	sznameindex;	// file name
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	'	
	'	int					cache;			// cache index in the shared model cache
	'	int					data;			// hack for group 0
	'};

	Public nameOffset As Integer
	Public fileNameOffset As Integer

	Public cacheOffset As Integer
	Public data As Integer

	'For MDL v35.
	Public unknown(7) As Integer

	Public theName As String
	Public theFileName As String

End Class
