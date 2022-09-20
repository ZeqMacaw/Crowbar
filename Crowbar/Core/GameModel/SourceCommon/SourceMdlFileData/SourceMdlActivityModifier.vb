Public Class SourceMdlActivityModifier

	'FROM: AlienSwarm_source\src\public\studio.h
	'struct mstudioactivitymodifier_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();

	'	int					sznameindex;
	'	inline char			*pszName() { return (sznameindex) ? (char *)(((byte *)this) + sznameindex ) : NULL; }
	'};



	Public nameOffset As Integer

	' V53 has an int here.
	Public unkV53 As Integer

	Public theName As String

End Class
