Public Class SourceMdlBone37

	'FROM: The Axel Project - source [MDL v37]\TAPSRC\src\Public\studio.h
	'struct mstudiobone_t
	'{
	'	int		sznameindex;
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	'	int		parent;		// parent bone
	'	int		bonecontroller[6];	// bone controller index, -1 == none
	'	// FIXME: remove the damn default value fields and put in pos
	'	float		value[6];	// default DoF values
	'	float		scale[6];   // scale for delta DoF values
	'	matrix3x4_t	poseToBone;
	'	Quaternion	qAlignment;
	'	int		flags;
	'	int		proctype;
	'	int		procindex;	// procedural rule
	'	mutable int	physicsbone;	// index into physically simulated bone
	'	inline void *pProcedure( ) const { if (procindex == 0) return NULL; else return  (void *)(((byte *)this) + procindex); };
	'	int		surfacepropidx;	// index into string table for property name
	'	inline char * const pszSurfaceProp( void ) const { return ((char *)this) + surfacepropidx; }
	'	Quaternion	quat;
	'	int		contents;		// See BSPFlags.h for the contents flags
	'	int		unused[3];		// remove as appropriate
	'};

	Public nameOffset As Integer
	Public parentBoneIndex As Integer

	Public boneControllerIndex(5) As Integer

	'Public value(6) As Double
	'Public scale(6) As Double
	Public position As New SourceVector()
	Public rotation As New SourceVector()
	Public positionScale As New SourceVector()
	Public rotationScale As New SourceVector()

	Public poseToBoneColumn0 As New SourceVector()
	Public poseToBoneColumn1 As New SourceVector()
	Public poseToBoneColumn2 As New SourceVector()
	Public poseToBoneColumn3 As New SourceVector()

	Public qAlignment As SourceQuaternion

	Public flags As Integer

	Public proceduralRuleType As Integer
	Public proceduralRuleOffset As Integer
	Public physicsBoneIndex As Integer
	Public surfacePropNameOffset As Integer

	Public quat As SourceQuaternion

	Public contents As Integer

	Public unused(2) As Integer

	Public theAxisInterpBone As SourceMdlAxisInterpBone
	Public theName As String
	Public theQuatInterpBone As SourceMdlQuatInterpBone
	Public theSurfacePropName As String


	'#define STUDIO_PROC_AXISINTERP	1
	'#define STUDIO_PROC_QUATINTERP	2
	Public Const STUDIO_PROC_AXISINTERP As Integer = 1
	Public Const STUDIO_PROC_QUATINTERP As Integer = 2

End Class
