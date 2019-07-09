Public Class SourceMdlHitboxSet37

	'struct mstudiohitboxset_t
	'{
	'	int	sznameindex;
	'	inline char * const	pszName( void ) const { return ((char *)this) + sznameindex; }
	'	int	numhitboxes;
	'	int	hitboxindex;
	'	inline mstudiobbox_t *pHitbox( int i ) const { return (mstudiobbox_t *)(((byte *)this) + hitboxindex) + i; };
	'};

	Public nameOffset As Integer
	Public hitboxCount As Integer
	Public hitboxOffset As Integer

	Public theName As String
	Public theHitboxes As List(Of SourceMdlHitbox37)

End Class
