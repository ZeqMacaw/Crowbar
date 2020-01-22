Public Class SourceMdlMouth

	'struct mstudiomouth_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int					bone;
	'	Vector				forward;
	'	int					flexdesc;

	'	mstudiomouth_t(){}
	'private:
	'	// No copy constructors allowed
	'	mstudiomouth_t(const mstudiomouth_t& vOther);
	'};

	'	int					bone;
	Public boneIndex As Integer
	'	Vector				forward;
	Public forward As New SourceVector()
	'	int					flexdesc;
	Public flexDescIndex As Integer

End Class
