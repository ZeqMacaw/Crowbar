Public Class SourceMdlBoneDesc37

	'struct mstudiobonedesc_t
	'{
	'	int 	sznameindex;
	'	inline  char * const	pszName( void ) const {	return ((char *)this) + sznameindex; };
	'
	'	int parent;		// parent bone
	'
	'	// FIXME: remove the damn default value fields and put in pos
	'	float value[6]; // default DoF values
	'	float scale[6]; // scale for delta DoF values
	'	matrix3x4_t poseToBone;
	'
	'	float fivefloat[5];
	'//	Quaternion	qAlignment;
	'
	'//	int		unused[3];		// remove as appropriate
	'};

	Public nameOffset As Integer
	Public parentBoneIndex As Integer

	'Public value(5) As Double
	'Public scale(5) As Double
	Public position As SourceVector
	Public rotation As SourceVector
	Public positionScale As SourceVector
	Public rotationScale As SourceVector

	'	matrix3x4_t			poseToBone;
	Public poseToBoneColumn0 As SourceVector
	Public poseToBoneColumn1 As SourceVector
	Public poseToBoneColumn2 As SourceVector
	Public poseToBoneColumn3 As SourceVector

	Public unused(4) As Single

	Public theName As String

End Class
