Public Class DataGridViewRadioButtonColumn
	Inherits DataGridViewTextBoxColumn

	Friend Const radioButtonSize As Integer = 14

	Public Sub New()
		MyBase.New()

		'NOTE: Prevents occasionally showing "Selected" when clicked.
		Me.ReadOnly = True
	End Sub

	Friend Shared Sub Paint(ByVal g As Graphics, ByVal cellBounds As Rectangle, ByVal state As Boolean)
		'Dim drawBrsh As Brush = New SolidBrush(SystemColors.Control)
		'cellBounds.Inflate(-1, -1)
		'g.FillRectangle(drawBrsh, cellBounds)
		'cellBounds.Inflate(0, CInt(-((cellBounds.Height - radioButtonSize) / 2)))

		'Dim aButtonState As ButtonState
		'If state Then
		'	aButtonState = ButtonState.Checked
		'Else
		'	aButtonState = ButtonState.Normal
		'End If

		'ControlPaint.DrawRadioButton(g, cellBounds, aButtonState)
		''RadioButtonRenderer.DrawRadioButton(g, cellBounds, aButtonState)

		'drawBrsh.Dispose()

		'======

		'Dim rectRadioButton As Rectangle

		'rectRadioButton.Width = radioButtonSize
		'rectRadioButton.Height = radioButtonSize
		'rectRadioButton.X = CInt(cellBounds.X + (cellBounds.Width - rectRadioButton.Width) / 2)
		'rectRadioButton.Y = CInt(cellBounds.Y + (cellBounds.Height - rectRadioButton.Height) / 2)

		'If state Then
		'	ControlPaint.DrawRadioButton(g, rectRadioButton, ButtonState.Checked)
		'Else
		'	ControlPaint.DrawRadioButton(g, rectRadioButton, ButtonState.Normal)
		'End If

		'======

		Dim p As Point
		p.X = CInt(cellBounds.X + (cellBounds.Width - radioButtonSize) / 2)
		p.Y = CInt(cellBounds.Y + (cellBounds.Height - radioButtonSize) / 2)

		If state Then
			RadioButtonRenderer.DrawRadioButton(g, p, VisualStyles.RadioButtonState.CheckedNormal)
		Else
			RadioButtonRenderer.DrawRadioButton(g, p, VisualStyles.RadioButtonState.UncheckedNormal)
		End If
	End Sub

End Class
