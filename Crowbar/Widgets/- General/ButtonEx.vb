Public Class ButtonEx
	Inherits Button

	Public Sub New()
		MyBase.New()
	End Sub

	Protected Overrides Sub OnPaint(e As PaintEventArgs)
		MyBase.OnPaint(e)

		Dim g As Graphics = e.Graphics
		Dim clientRectangle As Rectangle = Me.ClientRectangle

		If Me.Image Is Nothing Then
			If Me.Enabled Then
				Using aColorBrush As New Drawing2D.LinearGradientBrush(clientRectangle, WidgetHighBackColor, WidgetHighBackColor, Drawing2D.LinearGradientMode.Vertical)
					g.FillRectangle(aColorBrush, clientRectangle)
				End Using
				TextRenderer.DrawText(g, Me.Text, Me.Font, clientRectangle, WidgetTextColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.WordBreak)
			Else
				Using aColorBrush As New Drawing2D.LinearGradientBrush(clientRectangle, WidgetDeepBackColor, WidgetDeepBackColor, Drawing2D.LinearGradientMode.Vertical)
					g.FillRectangle(aColorBrush, clientRectangle)
				End Using
				TextRenderer.DrawText(g, Me.Text, Me.Font, clientRectangle, WidgetDisabledTextColor)
			End If
		End If
	End Sub

End Class
