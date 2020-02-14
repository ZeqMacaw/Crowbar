Public Class SourceMdlEvent

	'FROM: SourceEngineXXXX_source\public\studio.h
	'// events
	'struct mstudioevent_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	float				cycle;
	'	int					event;
	'	int					type;
	'	inline const char * pszOptions( void ) const { return options; }
	'	char				options[64];

	'	int					szeventindex;
	'	inline char * const pszEventName( void ) const { return ((char *)this) + szeventindex; }
	'};


	Public cycle As Double
	Public eventIndex As Integer
	Public eventType As Integer
	Public options(63) As Char
	Public nameOffset As Integer



	Public theName As String



	'FROM: SourceEngineXXXX_source\public\studio.h
	'#define NEW_EVENT_STYLE ( 1 << 10 )
	Public Const NEW_EVENT_STYLE As Integer = 1 << 10

End Class
