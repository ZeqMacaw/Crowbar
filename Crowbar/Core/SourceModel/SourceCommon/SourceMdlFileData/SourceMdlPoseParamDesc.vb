Public Class SourceMdlPoseParamDesc

	'struct mstudioposeparamdesc_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int					sznameindex;
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	'	int					flags;	// ????
	'	float				start;	// starting value
	'	float				end;	// ending value
	'	float				loop;	// looping range, 0 for no looping, 360 for rotations, etc.
	'};

	'	int					sznameindex;
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	Public nameOffset As Integer
	'	int					flags;	// ????
	Public flags As Integer
	'	float				start;	// starting value
	Public startingValue As Single
	'	float				end;	// ending value
	Public endingValue As Single
	'	float				loop;	// looping range, 0 for no looping, 360 for rotations, etc.
	Public loopingRange As Single

	Public theName As String

End Class
