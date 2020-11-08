Public Class SourceMdlEyeball2531

	'struct mstudioeyeball_t
	'{
	'	int					sznameindex;
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	'	int		bone;
	'	Vector	org;
	'	float	zoffset;
	'	float	radius;
	'	Vector	up;
	'	Vector	forward;
	'	int		texture;
	'
	'	int		iris_material;
	'	float	iris_scale;
	'	int		glint_material;	// !!!
	'
	'	int		upperflexdesc[3];	// index of raiser, neutral, and lowerer flexdesc that is set by flex controllers
	'	int		lowerflexdesc[3];
	'	float	uppertarget[3];		// angle (radians) of raised, neutral, and lowered lid positions
	'	float	lowertarget[3];
	'	//int		upperflex;	// index of actual flex
	'	//int		lowerflex;
	'
	'	int		upperlidflexdesc;	// index of flex desc that actual lid flexes look to
	'	int		lowerlidflexdesc;
	'
	'	float	pitch[2];	// min/max pitch
	'	float	yaw[2];		// min/max yaw
	'};

	Public nameOffset As Integer

	Public boneIndex As Integer
	Public org As New SourceVector()
	Public zOffset As Double
	Public radius As Double
	Public up As New SourceVector()
	Public forward As New SourceVector()

	Public texture As Integer
	Public iris_material As Integer
	Public iris_scale As Double
	Public glint_material As Integer

	Public upperFlexDesc(2) As Integer
	Public lowerFlexDesc(2) As Integer
	Public upperTarget(2) As Double
	Public lowerTarget(2) As Double

	Public upperLidFlexDesc As Integer
	Public lowerLidFlexDesc As Integer

	Public minPitch As Double
	Public maxPitch As Double
	Public minYaw As Double
	Public maxYaw As Double


	Public theName As String

End Class
