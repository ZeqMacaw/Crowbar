Public Class SourceMdlHitboxSet2531

	'FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
	'struct mstudiohitboxset_t
	'{
	'	int					sznameindex;
	'	inline char * const	pszName( void ) const { return ((char *)this) + sznameindex; }
	'	int					numhitboxes;
	'	int					hitboxindex;
	'	inline mstudiobbox_t *pHitbox( int i ) const { return (mstudiobbox_t *)(((byte *)this) + hitboxindex) + i; };
	'};

	Public nameOffset As Integer
	Public hitboxCount As Integer
	Public hitboxOffset As Integer


	Public theName As String
	Public theHitboxes As List(Of SourceMdlHitbox2531)

End Class
