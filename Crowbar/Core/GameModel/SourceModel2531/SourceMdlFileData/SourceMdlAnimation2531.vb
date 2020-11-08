Public Class SourceMdlAnimation2531

	'FROM: SourceEngine2003_source HL2 Beta 2003\src_main\Public\studio.h
	'// per bone per animation DOF and weight pointers
	'struct mstudioanim_t
	'{
	'	// float			weight;		// bone influence
	'	int				flags;		// weighing options
	'	union
	'	{
	'		int				offset[6];	// pointers to animation 
	'		struct
	'		{
	'			float			pos[3];
	'			float			q[4];
	'		} pose;
	'	} u;
	'	inline mstudioanimvalue_t *pAnimvalue( int i ) const { return  (mstudioanimvalue_t *)(((byte *)this) + u.offset[i]); };
	'};

	'Public flags As Integer
	Public unknown As Double

	Public theOffsets(6) As Integer
	Public thePositionAnimationXValues As New List(Of SourceMdlAnimationValue2531)()
	Public thePositionAnimationYValues As New List(Of SourceMdlAnimationValue2531)()
	Public thePositionAnimationZValues As New List(Of SourceMdlAnimationValue2531)()
	Public theRotationAnimationXValues As New List(Of SourceMdlAnimationValue2531)()
	Public theRotationAnimationYValues As New List(Of SourceMdlAnimationValue2531)()
	Public theRotationAnimationZValues As New List(Of SourceMdlAnimationValue2531)()
	Public theRotationAnimationWValues As New List(Of SourceMdlAnimationValue2531)()

	'Public thePosition As SourceVector
	'Public theRotation As SourceQuaternion



	' For the flags field.
	'FROM: SourceEngine2003_source HL2 Beta 2003\src_main\Public\studio.h
	'#define STUDIO_POS_ANIMATED		0x0001
	'#define STUDIO_ROT_ANIMATED		0x0002
	Public Const STUDIO_POS_ANIMATED As Integer = &H1
	Public Const STUDIO_ROT_ANIMATED As Integer = &H2

End Class
