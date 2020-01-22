Public Class SourceMdlAxisInterpBone

	'FROM: SourceEngineXXXX_source\public\studio.h
	'struct mstudioaxisinterpbone_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int				control;// local transformation of this bone used to calc 3 point blend
	'	int				axis;	// axis to check
	'	Vector			pos[6];	// X+, X-, Y+, Y-, Z+, Z-
	'	Quaternion		quat[6];// X+, X-, Y+, Y-, Z+, Z-
	'
	'	mstudioaxisinterpbone_t(){}
	'private:
	'	// No copy constructors allowed
	'	mstudioaxisinterpbone_t(const mstudioaxisinterpbone_t& vOther);
	'};



	Public Sub New()
		For i As Integer = 0 To pos.Length - 1
			pos(i) = New SourceVector()
		Next
		For i As Integer = 0 To quat.Length - 1
			quat(i) = New SourceQuaternion()
		Next
	End Sub



	Public control As Integer
	Public axis As Integer
	Public pos(5) As SourceVector
	Public quat(5) As SourceQuaternion

End Class
