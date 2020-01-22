Public Class SourceMdlSequenceGroup10

	'FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
	'#ifndef ZONE_H
	'typedef void *cache_user_t;
	'#endif
	'
	'// demand loaded sequence groups
	'typedef struct
	'{
	'	char				label[32];	// textual name
	'	char				name[64];	// file name
	'	cache_user_t		cache;		// cache index pointer
	'	int					data;		// hack for group 0
	'} mstudioseqgroup_t;

	Public name(31) As Char
	Public fileName(63) As Char

	'NOTE: Based on the studiomdl.exe source code, these fields do not seem to be used.
	Public cacheOffset As Integer
	Public data As Integer


	Public theName As String
	Public theFileName As String

End Class
