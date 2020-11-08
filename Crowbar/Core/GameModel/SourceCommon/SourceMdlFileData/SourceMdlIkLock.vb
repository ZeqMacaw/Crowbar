Public Class SourceMdlIkLock

	'FROM: SourceEngineXXXX_source\public\studio.h
	'struct mstudioiklock_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int			chain;
	'	float		flPosWeight;
	'	float		flLocalQWeight;
	'	int			flags;
	'
	'	int			unused[4];
	'};



	Public chainIndex As Integer
	Public posWeight As Double
	Public localQWeight As Double
	Public flags As Integer

	Public unused(3) As Integer

End Class
