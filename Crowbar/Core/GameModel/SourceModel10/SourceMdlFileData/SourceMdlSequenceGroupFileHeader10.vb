Public Class SourceMdlSequenceGroupFileHeader10

	'FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
	'// header for demand loaded sequence group data
	'typedef struct 
	'{
	'	int					id;
	'	int					version;
	'
	'	char				name[64];
	'	int					length;
	'} studioseqhdr_t;

	Public id(3) As Char
	Public version As Integer
	Public name(63) As Char
	Public fileSize As Integer

	Public theActualFileSize As Long

End Class
