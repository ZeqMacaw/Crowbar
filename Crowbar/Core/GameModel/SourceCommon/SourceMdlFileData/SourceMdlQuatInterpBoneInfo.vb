Public Class SourceMdlQuatInterpBoneInfo

	'FROM: SourceEngineXXXX_source\public\studio.h

	'struct mstudioquatinterpinfo_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	float			inv_tolerance;	// 1 / radian angle of trigger influence
	'	Quaternion		trigger;	// angle to match
	'	Vector			pos;		// new position
	'	Quaternion		quat;		// new angle
	'
	'	mstudioquatinterpinfo_t(){}
	'private:
	'	// No copy constructors allowed
	'	mstudioquatinterpinfo_t(const mstudioquatinterpinfo_t& vOther);
	'};



	Public inverseToleranceAngle As Double
	Public trigger As SourceQuaternion
	Public pos As SourceVector
	Public quat As SourceQuaternion

End Class
