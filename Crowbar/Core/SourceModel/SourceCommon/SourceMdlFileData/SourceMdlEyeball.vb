Public Class SourceMdlEyeball

	'FROM: SourceEngine2006+_source\public\studio.h
	'// eyeball
	'struct mstudioeyeball_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int					sznameindex;
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	'	int		bone;
	'	Vector	org;
	'	float	zoffset;
	'	float	radius;
	'	Vector	up;
	'	Vector	forward;
	'	int		texture;

	'	int		unused1;
	'	float	iris_scale;
	'	int		unused2;

	'	int		upperflexdesc[3];	// index of raiser, neutral, and lowerer flexdesc that is set by flex controllers
	'	int		lowerflexdesc[3];
	'	float	uppertarget[3];		// angle (radians) of raised, neutral, and lowered lid positions
	'	float	lowertarget[3];

	'	int		upperlidflexdesc;	// index of flex desc that actual lid flexes look to
	'	int		lowerlidflexdesc;
	'	int		unused[4];			// These were used before, so not guaranteed to be 0
	'	bool	m_bNonFACS;			// Never used before version 44
	'	char	unused3[3];
	'	int		unused4[7];

	'	mstudioeyeball_t(){}
	'private:
	'	// No copy constructors allowed
	'	mstudioeyeball_t(const mstudioeyeball_t& vOther);
	'};



	Public nameOffset As Integer
	Public boneIndex As Integer
	Public org As SourceVector
	Public zOffset As Double
	Public radius As Double
	Public up As SourceVector
	Public forward As SourceVector
	'NOTE: Called mesh in one version, but seems to be only used internally by studiomdl.
	Public texture As Integer

	'FROM: SourceEngine2006_source\public\studio.h
	'int		iris_material;
	Public unused1 As Integer
	Public irisScale As Double
	'FROM: SourceEngine2006_source\public\studio.h
	'int		glint_material;	// !!!
	Public unused2 As Integer

	Public upperFlexDesc(2) As Integer
	Public lowerFlexDesc(2) As Integer
	Public upperTarget(2) As Double
	Public lowerTarget(2) As Double

	Public upperLidFlexDesc As Integer
	Public lowerLidFlexDesc As Integer
	Public unused(3) As Integer
	Public eyeballIsNonFacs As Byte
	Public unused3(2) As Char
	Public unused4(6) As Integer



	Public theName As String
	Public theTextureIndex As Integer

End Class
