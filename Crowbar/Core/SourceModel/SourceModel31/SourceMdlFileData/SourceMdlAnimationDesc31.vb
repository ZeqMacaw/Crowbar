Public Class SourceMdlAnimationDesc31
	Inherits SourceMdlAnimationDescBase

	Public nameOffset As Integer
	Public fps As Double
	Public flags As Integer
	Public frameCount As Integer
	Public movementCount As Integer
	Public movementOffset As Integer

	Public bbMin As New SourceVector()
	Public bbMax As New SourceVector()

	Public animOffset As Integer

	Public ikRuleCount As Integer
	Public ikRuleOffset As Integer

	'Public unused(7) As Integer

	Public theAnimations As List(Of SourceMdlAnimation31)
	'Public theIkRules As List(Of SourceMdlIkRule37)
	Public theMovements As List(Of SourceMdlMovement)
	''Public theName As String

	'Public theAnimIsLinkedToSequence As Boolean = False
	'Public theLinkedSequences As New List(Of SourceMdlSequenceDesc37)()

End Class
