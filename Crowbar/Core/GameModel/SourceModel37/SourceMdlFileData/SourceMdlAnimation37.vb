Public Class SourceMdlAnimation37

	'// per bone per animation DOF and weight pointers
	'struct mstudioanim_t
	'{
	'	// float		weight;		// bone influence
	'	int	flags;		// weighing options
	'	union
	'	{
	'		int	offset[6];	// pointers to animation 
	'		struct
	'		{
	'			float	pos[3];
	'			float	q[4];
	'		} pose;
	'	} u;
	'	inline mstudioanimvalue_t *pAnimvalue( int i ) const { return  (mstudioanimvalue_t *)(((byte *)this) + u.offset[i]); };
	'};

	Public flags As Integer

	Public animationValueOffsets(5) As Integer
	Public unused As Integer
	'---
	Public position As SourceVector
	Public rotationQuat As SourceQuaternion

	Public theAnimationValues(5) As List(Of SourceMdlAnimationValue10)

	'//=============================================================================
	'// Animation flag macros
	'//=============================================================================
	'#define STUDIO_POS_ANIMATED		0x0001
	'#define STUDIO_ROT_ANIMATED		0x0002
	Public Const STUDIO_POS_ANIMATED As Integer = &H1
	Public Const STUDIO_ROT_ANIMATED As Integer = &H2

End Class
