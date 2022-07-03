Public Class SourceMdlAnimTag

	'FROM: [49] csgo_studiomdl\public\studio.h
	'// animtags
	'struct mstudioanimtag_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int					tag;
	'	float				cycle;
	'	
	'	int					sztagindex;
	'	inline char * const pszTagName( void ) const { return ((char *)this) + sztagindex; }
	'};

	Public tagIndex As Integer
	Public cycle As Double
	Public nameOffset As Long

	Public theName As String

End Class
