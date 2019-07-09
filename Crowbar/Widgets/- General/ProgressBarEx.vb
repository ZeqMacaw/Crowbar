Imports System.Drawing.Drawing2D

Public Class ProgressBarEx
	Inherits ProgressBar

	'      Note that Paint() is not called unless UserPaint style is True.
	'      Also OnPaint() is not called unless Paint() is called, but then bar is not drawn.
	'      Overriding and then calling MyBase.OnPaint() does not draw bar.
	'      Conclusion: Must override OnPaint() to draw text and bar. 

	Public Sub New()
		MyBase.New()

		Me.theText = ""
		Me.SetStyle(ControlStyles.UserPaint, True)
		Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
		Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
	End Sub

	Public Overrides Property Text As String
		Get
			Return Me.theText
		End Get
		Set(value As String)
			If Me.theText <> value Then
				Me.theText = value
			End If
		End Set
	End Property

	Public Overloads Property [Value]() As Integer
		Get
			Return MyBase.Value
		End Get
		Set(ByVal value As Integer)
			If value < MyBase.Minimum Then
				value = MyBase.Minimum
			ElseIf value > MyBase.Maximum Then
				value = MyBase.Maximum
			End If
			MyBase.Value = value
			'NOTE: Do this so bar is re-painted when Value changes.
			Me.Invalidate()
		End Set
	End Property

	Protected Overrides Sub OnPaint(e As PaintEventArgs)
		Dim g As Graphics = e.Graphics
		Dim range As Integer = Maximum - Minimum
		Dim percent As Double = CDbl(Value - Minimum) / CDbl(range)
		Dim rect As Rectangle = Me.ClientRectangle
		Dim bounds As Rectangle = e.ClipRectangle
		If rect.Width > 0 AndAlso percent > 0 Then
			If ProgressBarRenderer.IsSupported Then
				ProgressBarRenderer.DrawHorizontalBar(g, Me.DisplayRectangle)
				rect.Inflate(-2, -2)
				rect.Width = CInt(rect.Width * percent)
				If rect.Width = 0 Then
					rect.Width = 1
				End If
				'NOTE: This always draws with Green color, so use other code to draw with widget's colors.
				'ProgressBarRenderer.DrawHorizontalChunks(e.Graphics, rect)
				Dim gradientBrush As LinearGradientBrush = New LinearGradientBrush(rect, BackColor, ForeColor, LinearGradientMode.Vertical)
				e.Graphics.FillRectangle(gradientBrush, rect)
			Else
				Dim barWidth As Double = percent * bounds.Width
				Using backBrush As New SolidBrush(BackColor)
					g.FillRectangle(backBrush, bounds)
				End Using
				Using foreBrush As New SolidBrush(ForeColor)
					g.FillRectangle(foreBrush, New RectangleF(0, 0, CSng(barWidth), bounds.Height))
				End Using
				ControlPaint.DrawBorder(g, bounds, Color.Black, ButtonBorderStyle.Solid)
			End If
		Else
			If ProgressBarRenderer.IsSupported Then
				ProgressBarRenderer.DrawHorizontalBar(g, Me.DisplayRectangle)
			Else
				ControlPaint.DrawBorder(g, bounds, Color.Black, ButtonBorderStyle.Solid)
			End If
		End If

		If Me.theText <> "" Then
			Dim x As Double
			Dim y As Double
			x = Me.Width * 0.5 - (g.MeasureString(Me.theText, Me.Font).Width * 0.5)
			y = Me.Height * 0.5 - (g.MeasureString(Me.theText, Me.Font).Height * 0.5)
			TextRenderer.DrawText(g, Me.theText, Me.Font, New Point(CInt(x), CInt(y)), Me.ForeColor, Me.BackColor)
		End If

	End Sub

	Private theText As String

End Class
