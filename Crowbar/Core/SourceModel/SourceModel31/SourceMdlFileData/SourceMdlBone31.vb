Public Class SourceMdlBone31

	Public nameOffset As Integer
	Public parentBoneIndex As Integer

	Public boneControllerIndex(5) As Integer

	Public position As New SourceVector()
	Public rotation As New SourceVector()
	Public positionScale As New SourceVector()
	Public rotationScale As New SourceVector()

	Public poseToBoneColumn0 As New SourceVector()
	Public poseToBoneColumn1 As New SourceVector()
	Public poseToBoneColumn2 As New SourceVector()
	Public poseToBoneColumn3 As New SourceVector()

	'Public qAlignment As SourceQuaternion

	Public flags As Integer

	Public proceduralRuleType As Integer
	Public proceduralRuleOffset As Integer
	Public physicsBoneIndex As Integer
	Public surfacePropNameOffset As Integer

	'Public quat As SourceQuaternion

	'Public contents As Integer

	'Public unused(2) As Integer

	Public theAxisInterpBone As SourceMdlAxisInterpBone
	Public theName As String
	Public theQuatInterpBone As SourceMdlQuatInterpBone
	Public theSurfacePropName As String


	'#define STUDIO_PROC_AXISINTERP	1
	'#define STUDIO_PROC_QUATINTERP	2
	Public Const STUDIO_PROC_AXISINTERP As Integer = 1
	Public Const STUDIO_PROC_QUATINTERP As Integer = 2

End Class
