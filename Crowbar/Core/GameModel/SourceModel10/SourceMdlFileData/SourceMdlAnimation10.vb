Public Class SourceMdlAnimation10

	Public Sub New()
		MyBase.New()

		For offsetIndex As Integer = 0 To Me.animationValueOffsets.Length - 1
			Me.theAnimationValues(offsetIndex) = New List(Of SourceMdlAnimationValue10)()
		Next
	End Sub

	'FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
	'typedef struct
	'{
	'	unsigned short	offset[6];
	'} mstudioanim_t;

	Public animationValueOffsets(5) As UShort



	Public theAnimationValues(5) As List(Of SourceMdlAnimationValue10)

End Class
