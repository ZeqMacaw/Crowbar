Public Class SourceMdlIkLink

	'// ikinfo
	'struct mstudioiklink_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int		bone;
	'	Vector	kneeDir;	// ideal bending direction (per link, if applicable)
	'	Vector	unused0;	// unused

	'	mstudioiklink_t(){}
	'private:
	'	// No copy constructors allowed
	'	mstudioiklink_t(const mstudioiklink_t& vOther);
	'};


	'	int		bone;
	Public boneIndex As Integer
	'	Vector	kneeDir;	// ideal bending direction (per link, if applicable)
	Public idealBendingDirection As New SourceVector()
	'	Vector	unused0;	// unused
	Public unused0 As New SourceVector()

End Class
