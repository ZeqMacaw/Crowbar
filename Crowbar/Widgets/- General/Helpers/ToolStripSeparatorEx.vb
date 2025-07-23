Public Class ToolStripSeparatorEx
	Inherits ToolStripSeparator

	Public Sub New()
		MyBase.New()

	End Sub

	Protected Overrides Sub OnPaint(e As PaintEventArgs)
		Dim theme As ToolStripTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.ToolStripTheme
		End If
		If theme IsNot Nothing Then
			Dim aPen As New Pen(WidgetTextColor)
			e.Graphics.DrawLine(aPen, 3, 5, 3, 19)
		Else
			MyBase.OnPaint(e)
		End If
	End Sub

End Class
