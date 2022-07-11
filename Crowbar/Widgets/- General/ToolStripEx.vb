Public Class ToolStripEx
	Inherits ToolStrip

	Public Sub New()
		MyBase.New()

		Me.BackColor = WidgetConstants.WidgetBackColor

		'Me.Renderer = New ToolStripProfessionalRenderer(New CustomProfessionalColors())
		Me.Renderer = New ToolStripOverride()
	End Sub

	Private Class ToolStripOverride
		Inherits ToolStripProfessionalRenderer

		Public Sub New()
		End Sub

		'NOTE: Intentionally do nothing to remove the incomplete border.
		Protected Overrides Sub OnRenderToolStripBorder(ByVal e As ToolStripRenderEventArgs)
			'Using backColorPen As New Pen(Color.Red)
			'	Dim aRect As Rectangle = e.AffectedBounds
			'	aRect.X += 1
			'	aRect.Width -= 3
			'	aRect.Height -= 2
			'	e.Graphics.DrawRectangle(backColorPen, aRect)
			'End Using
		End Sub

	End Class

	'Class CustomProfessionalColors
	'	Inherits ProfessionalColorTable

	'	Public Overrides ReadOnly Property ToolStripBorder() As Color
	'		Get
	'			Return Color.Red
	'		End Get
	'	End Property

	'	Public Overrides ReadOnly Property RaftingContainerGradientBegin() As Color
	'		Get
	'			Return Color.Red
	'		End Get
	'	End Property

	'	Public Overrides ReadOnly Property RaftingContainerGradientEnd() As Color
	'		Get
	'			Return Color.Red
	'		End Get
	'	End Property

	'	'Public Overrides ReadOnly Property ToolStripGradientBegin() As Color
	'	'	Get
	'	'		Return Color.BlueViolet
	'	'	End Get
	'	'End Property

	'	'Public Overrides ReadOnly Property ToolStripGradientMiddle() As Color
	'	'	Get
	'	'		Return Color.CadetBlue
	'	'	End Get
	'	'End Property

	'	'Public Overrides ReadOnly Property ToolStripGradientEnd() As Color
	'	'	Get
	'	'		Return Color.CornflowerBlue
	'	'	End Get
	'	'End Property

	'	'Public Overrides ReadOnly Property MenuStripGradientBegin() As Color
	'	'	Get
	'	'		Return Color.Salmon
	'	'	End Get
	'	'End Property

	'	'Public Overrides ReadOnly Property MenuStripGradientEnd() As Color
	'	'	Get
	'	'		Return Color.OrangeRed
	'	'	End Get
	'	'End Property

	'End Class

End Class
