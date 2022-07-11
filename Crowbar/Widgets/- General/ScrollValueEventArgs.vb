Public Class ScrollValueEventArgs
	Inherits EventArgs

	Public Property Value As Integer

	Public Sub New(ByVal iValue As Integer)
		Value = iValue
	End Sub

End Class
