Public Class SourceMdlAnimation31

	'Public flags As Integer

	'Public animationValueOffsets(5) As Integer
	'Public unused As Integer
	''---
	'Public position As SourceVector
	'Public rotationQuat As SourceQuaternion

	'Public theAnimationValues(5) As List(Of SourceMdlAnimationValue)

	Public unknown As Double
	Public theOffsets(6) As Integer
	Public thePositionAnimationXValues As New List(Of SourceMdlAnimationValue2531)()
	Public thePositionAnimationYValues As New List(Of SourceMdlAnimationValue2531)()
	Public thePositionAnimationZValues As New List(Of SourceMdlAnimationValue2531)()
	Public theRotationAnimationXValues As New List(Of SourceMdlAnimationValue2531)()
	Public theRotationAnimationYValues As New List(Of SourceMdlAnimationValue2531)()
	Public theRotationAnimationZValues As New List(Of SourceMdlAnimationValue2531)()
	Public theRotationAnimationWValues As New List(Of SourceMdlAnimationValue2531)()

	'//=============================================================================
	'// Animation flag macros
	'//=============================================================================
	'#define STUDIO_POS_ANIMATED		0x0001
	'#define STUDIO_ROT_ANIMATED		0x0002
	Public Const STUDIO_POS_ANIMATED As Integer = &H1
	Public Const STUDIO_ROT_ANIMATED As Integer = &H2

End Class
