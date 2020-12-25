Public Class SourceMdlJiggleBone

	'FROM: SourceEngineXXXX_source\public\studio.h
	'struct mstudiojigglebone_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();

	'	int				flags;

	'	// general params
	'	float			length;					// how from from bone base, along bone, is tip
	'	float			tipMass;

	'	// flexible params
	'	float			yawStiffness;
	'	float			yawDamping;	
	'	float			pitchStiffness;
	'	float			pitchDamping;	
	'	float			alongStiffness;
	'	float			alongDamping;	

	'	// angle constraint
	'	float			angleLimit;				// maximum deflection of tip in radians

	'	// yaw constraint
	'	float			minYaw;					// in radians
	'	float			maxYaw;					// in radians
	'	float			yawFriction;
	'	float			yawBounce;

	'	// pitch constraint
	'	float			minPitch;				// in radians
	'	float			maxPitch;				// in radians
	'	float			pitchFriction;
	'	float			pitchBounce;

	'	// base spring
	'	float			baseMass;
	'	float			baseStiffness;
	'	float			baseDamping;	
	'	float			baseMinLeft;
	'	float			baseMaxLeft;
	'	float			baseLeftFriction;
	'	float			baseMinUp;
	'	float			baseMaxUp;
	'	float			baseUpFriction;
	'	float			baseMinForward;
	'	float			baseMaxForward;
	'	float			baseForwardFriction;

	'FROM: https://github.com/ValveSoftware/source-sdk-2013/blob/master/mp/src/public/studio.h
	'	// boing
	'	float			boingImpactSpeed;
	'	float			boingImpactAngle;
	'	float			boingDampingRate;
	'	float			boingFrequency;
	'	float			boingAmplitude;

	'private:
	'	// No copy constructors allowed
	'	//mstudiojigglebone_t(const mstudiojigglebone_t& vOther);
	'};



	Public flags As Integer
	Public length As Double
	Public tipMass As Double

	Public yawStiffness As Double
	Public yawDamping As Double
	Public pitchStiffness As Double
	Public pitchDamping As Double
	Public alongStiffness As Double
	Public alongDamping As Double

	Public angleLimit As Double

	Public minYaw As Double
	Public maxYaw As Double
	Public yawFriction As Double
	Public yawBounce As Double

	Public minPitch As Double
	Public maxPitch As Double
	Public pitchFriction As Double
	Public pitchBounce As Double

	Public baseMass As Double
	Public baseStiffness As Double
	Public baseDamping As Double
	Public baseMinLeft As Double
	Public baseMaxLeft As Double
	Public baseLeftFriction As Double
	Public baseMinUp As Double
	Public baseMaxUp As Double
	Public baseUpFriction As Double
	Public baseMinForward As Double
	Public baseMaxForward As Double
	Public baseForwardFriction As Double

	'NOTE: These fields seem to be only in models compiled with Source SDK Base 2013 MP and SP.
	Public boingImpactSpeed As Double
	Public boingImpactAngle As Double
	Public boingDampingRate As Double
	Public boingFrequency As Double
	Public boingAmplitude As Double



	' flags values:
	'#define JIGGLE_IS_FLEXIBLE				0x01
	'#define JIGGLE_IS_RIGID					0x02
	'#define JIGGLE_HAS_YAW_CONSTRAINT		0x04
	'#define JIGGLE_HAS_PITCH_CONSTRAINT		0x08
	'#define JIGGLE_HAS_ANGLE_CONSTRAINT		0x10
	'#define JIGGLE_HAS_LENGTH_CONSTRAINT	0x20
	'#define JIGGLE_HAS_BASE_SPRING			0x40
	'======
	'FROM: VERSION 48 - https://github.com/ValveSoftware/source-sdk-2013/blob/master/mp/src/public/studio.h
	'FROM: VERSION 49 - Not found in studiomdl.exe of CSGO, L4D2, or SFM, but probably safe to check for flag value.
	'#define JIGGLE_IS_BOING					0x80		// simple squash and stretch sinusoid "boing"
	Public Const JIGGLE_IS_FLEXIBLE As Integer = &H1
	Public Const JIGGLE_IS_RIGID As Integer = &H2
	Public Const JIGGLE_HAS_YAW_CONSTRAINT As Integer = &H4
	Public Const JIGGLE_HAS_PITCH_CONSTRAINT As Integer = &H8
	Public Const JIGGLE_HAS_ANGLE_CONSTRAINT As Integer = &H10
	Public Const JIGGLE_HAS_LENGTH_CONSTRAINT As Integer = &H20
	Public Const JIGGLE_HAS_BASE_SPRING As Integer = &H40
	Public Const JIGGLE_IS_BOING As Integer = &H80

End Class
