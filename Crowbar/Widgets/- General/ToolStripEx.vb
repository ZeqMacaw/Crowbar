Imports System.ComponentModel

Public Class ToolStripEx
	Inherits ToolStrip

	Public Sub New()
		MyBase.New()

		Me.Renderer = New ToolStripRendererOverride()
	End Sub

	<Browsable(True)>
	<Category("Appearance")>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	Public Overloads Property ForeColor As Color
		Get
			Return MyBase.ForeColor
		End Get
		Set
			MyBase.ForeColor = Value
		End Set
	End Property

	<Browsable(True)>
	<Category("Appearance")>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	Public Overloads Property BackColor As Color
		Get
			Return MyBase.BackColor
		End Get
		Set
			MyBase.BackColor = Value
		End Set
	End Property

	'Public Overloads Property Renderer() As ToolStripRenderer
	'	Get
	'		Return MyBase.Renderer
	'	End Get
	'	Set
	'		MyBase.Renderer = Value
	'	End Set
	'End Property

	'' This is called without needing any other code to enable it.
	'Protected Overrides Sub OnPaint(e As PaintEventArgs)
	'	'Dim theme As ToolStripTheme = Nothing
	'	'' This check prevents problems with viewing and saving Forms in VS Designer.
	'	'If TheApp IsNot Nothing Then
	'	'	theme = TheApp.Settings.SelectedAppTheme.ToolStripTheme
	'	'End If
	'	'If theme IsNot Nothing Then
	'	'	Me.BackColor = theme.EnabledBackColor
	'	'Else
	'	'	Me.BackColor = DefaultBackColor
	'	'End If
	'	MyBase.OnPaint(e)
	'End Sub

	Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
		Dim theme As ToolStripTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.ToolStripTheme
		End If
		If theme IsNot Nothing Then
			Me.BackColor = theme.EnabledBackColor
		Else
			Me.BackColor = DefaultBackColor
		End If
		MyBase.OnPaintBackground(e)
	End Sub

	Public Class ToolStripRendererOverride
		'' Without explicitly setting any colors, this seems to use Windows theme colors on Win11.
		'Inherits ToolStripSystemRenderer
		Inherits ToolStripProfessionalRenderer
		'Inherits ToolStripRenderer

		Public Sub New()
			MyBase.New()
		End Sub

		'NOTE: Intentionally do nothing to remove the incomplete border.
		Protected Overrides Sub OnRenderToolStripBorder(ByVal e As ToolStripRenderEventArgs)
			Dim theme As PanelTheme = Nothing
			' This check prevents problems with viewing and saving Forms in VS Designer.
			If TheApp IsNot Nothing Then
				theme = TheApp.Settings.SelectedAppTheme.PanelTheme
			End If
			If theme IsNot Nothing Then
				If TypeOf e.ToolStrip IsNot ToolStrip Then
					MyBase.OnRenderToolStripBorder(e)
					'Else
					'	Using backColorPen As New Pen(Color.Red)
					'		Dim aRect As Rectangle = e.AffectedBounds
					'		aRect.X += 1
					'		aRect.Width -= 3
					'		aRect.Height -= 2
					'		e.Graphics.DrawRectangle(backColorPen, aRect)
					'	End Using
				End If
			End If
		End Sub

		'Protected Overrides Sub OnRenderLabelBackground(ByVal e As ToolStripItemRenderEventArgs)
		'	Using brush As New SolidBrush(e.Item.BackColor)
		'		e.Graphics.FillRectangle(brush, New Rectangle(Point.Empty, e.Item.Size))
		'	End Using
		'End Sub

	End Class

End Class
