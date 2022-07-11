Public Class ListViewEx
	Inherits ListView

	Public Sub New()
		MyBase.New()

		Me.ForeColor = WidgetTextColor
		Me.BackColor = WidgetDeepBackColor
		Me.OwnerDraw = True
	End Sub

	Protected Overrides Sub OnDrawColumnHeader(e As DrawListViewColumnHeaderEventArgs)
		'MyBase.OnDrawColumnHeader(e)
		Using aBrush As New SolidBrush(WidgetHighBackColor)
			Dim aRect As Rectangle = e.Bounds
			aRect.Inflate(-1, 0)
			e.Graphics.FillRectangle(aBrush, aRect)
		End Using
		Dim textRect As Rectangle = e.Bounds
		textRect.X += 1
		TextRenderer.DrawText(e.Graphics, e.Header.Text, Me.Font, textRect, WidgetTextColor, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine Or TextFormatFlags.EndEllipsis)
	End Sub

	Protected Overrides Sub OnDrawItem(e As DrawListViewItemEventArgs)
		e.DrawDefault = True
		MyBase.OnDrawItem(e)
	End Sub

	Protected Overrides Sub OnDrawSubItem(e As DrawListViewSubItemEventArgs)
		e.DrawDefault = True
		MyBase.OnDrawSubItem(e)
	End Sub

End Class
