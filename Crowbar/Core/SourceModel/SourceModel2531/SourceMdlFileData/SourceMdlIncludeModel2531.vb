Public Class SourceMdlIncludeModel2531

	'FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
	'struct mstudiomodelgroup_t	// f64: +
	'{
	'	int					szlabelindex;
	'	inline char * const	pszLabel( void ) const { return ((char *)this) + szlabelindex; }
	'
	'	int					unk;
	'	int					unk2;
	'
	'	int					unknum;
	'	int					unkindex;
	'
	'	int					minone[24];
	'};

	Public fileNameOffset As Integer
	Public unknown(27) As Integer

	Public theFileName As String
	Public theLabel As String

End Class
