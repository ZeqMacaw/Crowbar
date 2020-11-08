Public Class SourceMdlEvent10

	'FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
	'// events
	'typedef struct 
	'{
	'	int 				frame;
	'	int					event;
	'	int					type;
	'	char				options[64];
	'} mstudioevent_t;

	Public frameIndex As Integer
	Public eventIndex As Integer
	'NOTE: Based on the studiomdl.exe source code, this does not seem to be used.
	Public eventType As Integer
	Public options(63) As Char



	Public theOptions As String

End Class
