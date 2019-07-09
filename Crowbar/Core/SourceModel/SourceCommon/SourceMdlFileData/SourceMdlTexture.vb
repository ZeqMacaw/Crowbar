Public Class SourceMdlTexture

	'// skin info
	'struct mstudiotexture_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int						sznameindex;
	'	inline char * const		pszName( void ) const { return ((char *)this) + sznameindex; }
	'	int						flags;
	'	int						used;
	'    int						unused1;
	'	mutable IMaterial		*material;  // fixme: this needs to go away . .isn't used by the engine, but is used by studiomdl
	'	mutable void			*clientmaterial;	// gary, replace with client material pointer if used

	'	int						unused[10];
	'};

	'	int						sznameindex;
	Public nameOffset As Integer
	'	int						flags;
	Public flags As Integer
	'	int						used;
	Public used As Integer
	'    int						unused1;
	Public unused1 As Integer
	'	mutable IMaterial		*material;  // fixme: this needs to go away . .isn't used by the engine, but is used by studiomdl
	Public materialP As Integer
	'	mutable void			*clientmaterial;	// gary, replace with client material pointer if used
	Public clientMaterialP As Integer
	'	int						unused[10];
	Public unused(9) As Integer

	'	inline char * const		pszName( void ) const { return ((char *)this) + sznameindex; }
	Public thePathFileName As String

End Class
