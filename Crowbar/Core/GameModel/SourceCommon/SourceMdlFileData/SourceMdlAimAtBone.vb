Public Class SourceMdlAimAtBone

	'struct mstudioaimatbone_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'
	'	int				parent;
	'	int				aim;		// Might be bone or attach
	'	Vector			aimvector;
	'	Vector			upvector;
	'	Vector			basepos;
	'
	'	mstudioaimatbone_t() {}
	'private:
	'	// No copy constructors allowed
	'	mstudioaimatbone_t(const mstudioaimatbone_t& vOther);
	'};

	Public parentBoneIndex As Integer
	Public aimBoneOrAttachmentIndex As Integer

	Public aim As SourceVector
	Public up As SourceVector
	Public basePos As SourceVector

End Class
