Public Class IntermediateExpression

	Public Sub New(ByVal iExpression As String, ByVal iPrecedence As Integer)
		Me.theExpression = iExpression
		Me.thePrecedence = iPrecedence
	End Sub

	Public theExpression As String
	Public thePrecedence As Integer

End Class
