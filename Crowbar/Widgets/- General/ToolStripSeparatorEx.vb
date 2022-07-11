Public Class ToolStripSeparatorEx
	Inherits ToolStripSeparator

	Public Sub New()
		MyBase.New()

		Me.ForeColor = WidgetTextColor
		Me.BackColor = WidgetHighBackColor
	End Sub

	Protected Overrides Sub OnPaint(e As PaintEventArgs)
		Dim aPen As New Pen(WidgetTextColor)
		e.Graphics.DrawLine(aPen, 3, 5, 3, 19)
	End Sub

End Class
