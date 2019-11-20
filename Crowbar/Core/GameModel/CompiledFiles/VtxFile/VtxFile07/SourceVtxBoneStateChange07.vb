Public Class SourceVtxBoneStateChange07

	'FROM: src/public/optimize.h
	'struct BoneStateChangeHeader_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int hardwareID;
	'	int newBoneID;
	'};

	Public hardwareId As Integer
	Public newBoneId As Integer

End Class
