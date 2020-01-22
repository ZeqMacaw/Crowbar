Public Class SourceMdlHitbox32

	'struct mstudiobbox_t
	'{
	'	int	bone;
	'	int	group;			// intersection group
	'	Vector	bbmin;			// bounding box
	'	Vector	bbmax;	
	'	int	szhitboxnameindex;	// offset to the name of the hitbox.
	'	char	padding[32];		// future expansion.
	'
	'	char* pszHitboxName()
	'	{
	'		if( szhitboxnameindex == 0 )
	'			return "";
	'
	'		return ((char*)this) + szhitboxnameindex;
	'	}
	'};

	Public boneIndex As Integer
	Public groupIndex As Integer
	Public boundingBoxMin As New SourceVector()
	Public boundingBoxMax As New SourceVector()
	'Public nameOffset As Integer
	'Public unused(31) As Byte

	Public theName As String

End Class
