Public Class SourceMdlAttachment37

	'struct mstudioattachment_t
	'{
	'	int		sznameindex;
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	'	int		type;
	'	int		bone;
	'	matrix3x4_t	local; // attachment point
	'};

	Public nameOffset As Integer
	Public type As Integer
	Public boneIndex As Integer
	Public localM11 As Single
	Public localM12 As Single
	Public localM13 As Single
	Public localM14 As Single
	Public localM21 As Single
	Public localM22 As Single
	Public localM23 As Single
	Public localM24 As Single
	Public localM31 As Single
	Public localM32 As Single
	Public localM33 As Single
	Public localM34 As Single

	Public theName As String

End Class
