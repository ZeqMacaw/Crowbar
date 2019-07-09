Public Class SourceMdlTexture2531

	'FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
	'struct mstudiotexture_t
	'{
	'	int						sznameindex;
	'	inline char * const		pszName( void ) const { return ((char *)this) + sznameindex; }
	'	int						flags;
	'	float					width;		// portion used
	'	float					height;		// portion used
	'// f64: -
	'//	mutable IMaterial		*material;  // fixme: this needs to go away . .isn't used by the engine, but is used by studiomdl
	'//	mutable void			*clientmaterial;	// gary, replace with client material pointer if used
	'//	float					dPdu;		// world units per u
	'//	float					dPdv;		// world units per v
	'// ----
	'	float					unk;
	'};

	Public fileNameOffset As Integer
	Public flags As Integer
	Public width As Double
	Public height As Double
	Public unknown As Double


	Public theFileName As String

End Class
