Public Class SourceMdlIkChain37

	'struct mstudioikchain_t
	'{
	'	int	sznameindex;
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	'	int	linktype;
	'	int	numlinks;
	'	int	linkindex;
	'	inline mstudioiklink_t *pLink( int i ) const { return (mstudioiklink_t *)(((byte *)this) + linkindex) + i; };
	'};

	Public nameOffset As Integer
	Public linkType As Integer
	Public linkCount As Integer
	Public linkOffset As Integer

	Public theLinks As List(Of SourceMdlIkLink37)
	Public theName As String

End Class
