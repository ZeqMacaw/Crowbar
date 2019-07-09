Public Class SourceMdlPoseParamDesc2531

	'FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
	'struct mstudioposeparamdesc_t
	'{
	'	int					sznameindex;
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	'	int					flags;	// ????
	'	float				start;	// starting value
	'	float				end;	// ending value
	'	float				loop;	// looping range, 0 for no looping, 360 for rotations, etc.
	'};

	Public nameOffset As Integer
	Public flags As Integer
	Public startingValue As Double
	Public endingValue As Double
	Public loopingRange As Double


	Public theName As String

End Class
