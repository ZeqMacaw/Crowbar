Public Class SourceMdlQuatInterpInfo2531

	'FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
	'struct mstudioquatinterpinfo_t
	'{
	'	float			inv_tolerance;	// 1 / radian angle of trigger influence
	'	Quaternion		trigger;	// angle to match
	'	Vector			pos;		// new position
	'	Quaternion		quat;		// new angle
	'};

	Public inverseToleranceAngle As Double
	Public trigger As New SourceQuaternion()
	Public pos As New SourceVector()
	Public quat As New SourceQuaternion()

End Class
