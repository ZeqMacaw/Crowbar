Public Class SourceMdlFlexRule

	'FROM: SourceEngineXXXX_source\public\studio.h
	'struct mstudioflexrule_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int					flex;
	'	int					numops;
	'	int					opindex;
	'	inline mstudioflexop_t *iFlexOp( int i ) const { return  (mstudioflexop_t *)(((byte *)this) + opindex) + i; };
	'};

	'	int					flex;
	Public flexIndex As Integer
	'	int					numops;
	Public opCount As Integer
	'	int					opindex;
	Public opOffset As Integer



	Public theFlexOps As List(Of SourceMdlFlexOp)

End Class
