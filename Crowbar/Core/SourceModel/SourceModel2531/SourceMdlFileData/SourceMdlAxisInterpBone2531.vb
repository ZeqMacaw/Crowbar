Public Class SourceMdlAxisInterpBone2531

	Public Sub New()
		For i As Integer = 0 To pos.Length - 1
			pos(i) = New SourceVector()
		Next
		For i As Integer = 0 To quat.Length - 1
			quat(i) = New SourceQuaternion()
		Next
	End Sub

	'FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
	'struct mstudioaxisinterpbone_t
	'{
	'	int				control;// local transformation of this bone used to calc 3 point blend
	'	int				axis;	// axis to check
	'	Vector			pos[6];	// X+, X-, Y+, Y-, Z+, Z-
	'	Quaternion		quat[6];// X+, X-, Y+, Y-, Z+, Z-
	'};

	Public controlBoneIndex As Integer
	Public axis As Integer
	Public pos(5) As SourceVector
	Public quat(5) As SourceQuaternion

End Class
