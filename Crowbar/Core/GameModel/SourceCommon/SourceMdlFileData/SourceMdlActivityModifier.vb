Public Class SourceMdlActivityModifier

	'FROM: AlienSwarm_source\src\public\studio.h
	'struct mstudioactivitymodifier_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();

	'	int					sznameindex;
	'	inline char			*pszName() { return (sznameindex) ? (char *)(((byte *)this) + sznameindex ) : NULL; }
	'};



	Public nameOffset As Integer

	' V53/V52 has an int here that needs to be read for proper output. 0 or 1.
	Public unk As Integer

	Public theName As String

End Class
