Imports System.ComponentModel

Public Class TextBoxEx
	Inherits TextBox

	Public Sub New()
		MyBase.New()

		Me.theCueBannerText = ""
	End Sub

	<Browsable(True)>
	<Category("Appearance")>
	<Description("Sets the text of the cue (dimmed text that only shows when Text property is empty).")>
	Public Property CueBannerText As String
		Get
			Return Me.theCueBannerText
		End Get
		Set
			Me.theCueBannerText = Value
		End Set
	End Property

	Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
		If e.Control AndAlso e.KeyCode = Keys.A Then
			Me.SelectAll()
		End If
		MyBase.OnKeyDown(e)
	End Sub

	Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
		If Not Me.Multiline AndAlso e.KeyChar = ChrW(Keys.Return) Then
			Try
				' Cause validation, which means Validating and Validated events are raised.
				Me.FindForm().Validate()
				If TypeOf Me.Parent Is ContainerControl Then
					CType(Me.Parent, ContainerControl).Validate()
				End If
				'NOTE: Prevent annoying beep when textbox is single line.
				e.Handled = True
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
		MyBase.OnKeyPress(e)
	End Sub

	'Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
	'	'MyBase.OnPaintBackground(e)
	'	Using sb As New SolidBrush(Color.Red)
	'		e.Graphics.FillRectangle(sb, Me.ClientRectangle)
	'		sb.Dispose()
	'	End Using
	'End Sub

	Protected Overrides Sub OnPaint(e As PaintEventArgs)
		MyBase.OnPaint(e)

		If Me.theCueBannerText <> "" AndAlso Me.Text = "" AndAlso Me.theOriginalFont IsNot Nothing Then
			'Using sb As New SolidBrush(SystemColors.Control)
			'	e.Graphics.FillRectangle(sb, Me.ClientRectangle)
			'	sb.Dispose()
			'End Using

			'Dim drawFont As System.Drawing.Font = New System.Drawing.Font(Me.theOriginalFont.FontFamily, Me.theOriginalFont.Size, Me.theOriginalFont.Style, Me.theOriginalFont.Unit)
			Dim drawFont As System.Drawing.Font = New System.Drawing.Font(Me.theOriginalFont.FontFamily, Me.theOriginalFont.Size, FontStyle.Italic, Me.theOriginalFont.Unit)

			Dim drawForeColor As Color = SystemColors.GrayText
			Dim drawBackColor As Color = SystemColors.Control
			If drawForeColor = drawBackColor Then
				drawForeColor = Me.ForeColor
				drawBackColor = Me.BackColor
			End If
			TextRenderer.DrawText(e.Graphics, Me.theCueBannerText, drawFont, New Point(1, 0), drawForeColor, drawBackColor)
			'======
			'' Draw higlight.
			'Dim higlightForeColor As Color = SystemColors.ControlLightLight
			''Dim higlightBackColor As Color = SystemColors.Control
			''If higlightForeColor = higlightBackColor Then
			''	higlightForeColor = Me.ForeColor
			''	higlightBackColor = Me.BackColor
			''End If
			''TextRenderer.DrawText(e.Graphics, Me.theCueBannerText, drawFont, New Point(1, 1), higlightForeColor, higlightBackColor)
			'TextRenderer.DrawText(e.Graphics, Me.theCueBannerText, drawFont, New Point(1, 1), higlightForeColor)
			'' Draw shadow.
			'Dim shadowForeColor As Color = SystemColors.ControlDark
			''Dim shadowBackColor As Color = SystemColors.Control
			''If shadowForeColor = shadowBackColor Then
			''	shadowForeColor = Me.ForeColor
			''	shadowBackColor = Me.BackColor
			''End If
			''TextRenderer.DrawText(e.Graphics, Me.theCueBannerText, drawFont, New Point(-1, -1), shadowForeColor, shadowBackColor)
			'TextRenderer.DrawText(e.Graphics, Me.theCueBannerText, drawFont, New Point(-1, -1), shadowForeColor)
		End If
	End Sub

	Protected Overrides Sub OnTextChanged(e As EventArgs)
		MyBase.OnTextChanged(e)

		' This did not solve the bug.
		'If Not Me.Visible Then
		'	Exit Sub
		'End If

		' This did not solve the bug.
		If Me.theOriginalFont Is Nothing Then
			Exit Sub
		End If

		If GetStyle(ControlStyles.UserPaint) <> (Me.theCueBannerText <> "" AndAlso Me.Text = "") Then
			SetStyle(ControlStyles.AllPaintingInWmPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
			SetStyle(ControlStyles.DoubleBuffer, Me.theCueBannerText <> "" AndAlso Me.Text = "")
			SetStyle(ControlStyles.UserPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
			If Me.theOriginalFont IsNot Nothing Then
				Me.Font = New System.Drawing.Font(Me.theOriginalFont.FontFamily, Me.theOriginalFont.Size, Me.theOriginalFont.Style, Me.theOriginalFont.Unit)
			End If
			Me.Invalidate()
		End If
	End Sub

	Protected Overrides Sub OnVisibleChanged(e As EventArgs)
		MyBase.OnVisibleChanged(e)

		If Me.theOriginalFont Is Nothing Then
			'NOTE: Font gets changed at some point after changing style, messing up when cue banner is turned off, 
			'      so save the Font after widget is visible for first time, but before changing style within the widget.
			Me.theOriginalFont = New System.Drawing.Font(Me.Font.FontFamily, Me.Font.Size, Me.Font.Style, Me.Font.Unit)

			'SetStyle(ControlStyles.UserPaint, Me.theCueBannerText <> "")
			SetStyle(ControlStyles.AllPaintingInWmPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
			SetStyle(ControlStyles.DoubleBuffer, Me.theCueBannerText <> "" AndAlso Me.Text = "")
			SetStyle(ControlStyles.UserPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
			'Me.Invalidate()
		End If
	End Sub

	Private theCueBannerText As String
	Private theOriginalFont As Font

End Class
