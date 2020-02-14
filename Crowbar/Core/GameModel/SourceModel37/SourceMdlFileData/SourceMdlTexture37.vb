Public Class SourceMdlTexture37

	'FROM: The Axel Project - source [MDL v37]\TAPSRC\src\Public\studio.h
	'struct mstudiotexture_t
	'{
	'	int	sznameindex;
	'	inline char * const		pszName( void ) const { return ((char *)this) + sznameindex; }
	'	int	flags;
	'	float	width;		// portion used
	'	float	height;		// portion used
	'	mutable IMaterial  *material;  // fixme: this needs to go away . .isn't used by the engine, but is used by studiomdl
	'	mutable void	   *clientmaterial;	// gary, replace with client material pointer if used
	'	float	dPdu;		// world units per u
	'	float	dPdv;		// world units per v
	'};

	Public fileNameOffset As Integer
	Public flags As Integer
	Public width As Double
	Public height As Double
	Public worldUnitsPerU As Double
	Public worldUnitsPerV As Double
	Public unknown(1) As Integer

	Public theFileName As String

End Class
