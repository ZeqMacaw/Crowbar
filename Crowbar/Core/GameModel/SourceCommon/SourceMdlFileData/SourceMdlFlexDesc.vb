Public Class SourceMdlFlexDesc

	'struct mstudioflexdesc_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int					szFACSindex;
	'	inline char * const pszFACS( void ) const { return ((char *)this) + szFACSindex; }
	'};

	'	int					szFACSindex;
	Public nameOffset As Integer

	Public theName As String
	Public theDescIsUsedByFlex As Boolean = False
	Public theDescIsUsedByFlexRule As Boolean = False
	Public theDescIsUsedByEyelid As Boolean = False

	'Public theVtaFrameIndex As Integer

End Class
