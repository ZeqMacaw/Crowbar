Imports System.ComponentModel
Imports System.Runtime.InteropServices

Public Class TextBoxEx
	Inherits TextBox

	Public Sub New()
		MyBase.New()

		Me.BorderStyle = BorderStyle.FixedSingle
		Me.ForeColor = WidgetTextColor
		Me.BackColor = WidgetDeepBackColor
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

	Protected Overrides Sub OnGotFocus(e As EventArgs)
		MyBase.OnGotFocus(e)
		'NOTE: Prevent white background flicker.
		Me.Invalidate()
	End Sub

	Protected Overrides Sub OnHandleCreated(e As EventArgs)
		MyBase.OnHandleCreated(e)
		If Me.theOriginalFont Is Nothing Then
			'NOTE: Font gets changed at some point after changing style, messing up when cue banner is turned off, 
			'      so save the Font before changing style.
			Me.theOriginalFont = New System.Drawing.Font(Me.Font.FontFamily, Me.Font.Size, Me.Font.Style, Me.Font.Unit)

			SetStyle(ControlStyles.AllPaintingInWmPaint, True)
			SetStyle(ControlStyles.DoubleBuffer, True)
			SetStyle(ControlStyles.UserPaint, True)
		End If
	End Sub

	Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
		If e.Control AndAlso e.KeyCode = Keys.A Then
			Me.SelectAll()
		End If
		MyBase.OnKeyDown(e)

		'NOTE: Prevent white background flicker.
		Me.Invalidate()
	End Sub

	'Protected Overrides Sub OnKeyUp(ByVal e As System.Windows.Forms.KeyEventArgs)
	'	MyBase.OnKeyUp(e)
	'	Me.Invalidate()
	'End Sub

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

	Protected Overrides Sub OnPaint(e As PaintEventArgs)
		'MyBase.OnPaint(e)

		' Draw text.
		If Me.Text <> "" AndAlso Me.theOriginalFont IsNot Nothing Then
			Dim g As Graphics = e.Graphics
			Dim clipRectangle As Rectangle = e.ClipRectangle

			Dim beginCharIndex As Integer = Me.GetCharIndexFromPosition(clipRectangle.Location)
			Dim endCharPoint As New Point(clipRectangle.Right, clipRectangle.Bottom)
			Dim endCharIndex As Integer = Me.GetCharIndexFromPosition(endCharPoint)
			If beginCharIndex > 0 Then
				Dim debug As Integer = 4242
			End If
			Dim aRect As Rectangle = clipRectangle
			aRect.Inflate(1, 0)
			If Me.Multiline Then
				TextRenderer.DrawText(g, Me.Text, Me.theOriginalFont, aRect, WidgetTextColor, WidgetDeepBackColor)
			Else
				TextRenderer.DrawText(g, Me.Text, Me.theOriginalFont, aRect, WidgetTextColor, WidgetDeepBackColor, TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine)
			End If

			Dim positionRect As Rectangle = clipRectangle
			Dim selectionEndCharIndex As Integer = Me.SelectionStart + Me.SelectionLength - 1
			For selectionCharIndex As Integer = Me.SelectionStart To selectionEndCharIndex
				positionRect.Location = Me.GetPositionFromCharIndex(selectionCharIndex)
				positionRect.X -= 3
				If Me.Multiline Then
					TextRenderer.DrawText(g, Me.SelectedText(selectionCharIndex - Me.SelectionStart), Me.theOriginalFont, positionRect, WidgetTextColor, WidgetDeepSelectedBackColor)
				Else
					TextRenderer.DrawText(g, Me.SelectedText(selectionCharIndex - Me.SelectionStart), Me.theOriginalFont, positionRect, WidgetTextColor, WidgetDeepSelectedBackColor, TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine)
				End If
			Next
		End If

		' Draw cue banner text.
		If Me.theCueBannerText <> "" AndAlso Me.Text = "" AndAlso Me.theOriginalFont IsNot Nothing Then
			Dim drawFont As System.Drawing.Font = New System.Drawing.Font(Me.theOriginalFont.FontFamily, Me.theOriginalFont.Size, FontStyle.Italic, Me.theOriginalFont.Unit)
			'TextRenderer.DrawText(e.Graphics, Me.theCueBannerText, drawFont, New Point(1, 0), WidgetDisabledTextColor, WidgetDeepBackColor)
			TextRenderer.DrawText(e.Graphics, Me.theCueBannerText, drawFont, e.ClipRectangle, WidgetDisabledTextColor, WidgetDeepBackColor, TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine)
		End If
	End Sub

	Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
		'MyBase.OnPaintBackground(e)

		' Draw background border.
		Using backColorBrush As New SolidBrush(WidgetDeepDisabledBackColor)
			Dim aRect As Rectangle = e.ClipRectangle
			e.Graphics.FillRectangle(backColorBrush, aRect)
		End Using

		' Draw background.
		Using backColorBrush As New SolidBrush(WidgetDeepBackColor)
			Dim aRect As Rectangle = e.ClipRectangle
			aRect.Inflate(-1, -1)
			e.Graphics.FillRectangle(backColorBrush, aRect)
		End Using
	End Sub

	'Protected Overrides Sub OnTextChanged(e As EventArgs)
	'	'MyBase.OnTextChanged(e)

	'	If Me.theOriginalFont Is Nothing Then
	'		Exit Sub
	'	End If

	'	'If GetStyle(ControlStyles.UserPaint) <> (Me.theCueBannerText <> "" AndAlso Me.Text = "") Then
	'	'	'SetStyle(ControlStyles.AllPaintingInWmPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
	'	'	'SetStyle(ControlStyles.DoubleBuffer, Me.theCueBannerText <> "" AndAlso Me.Text = "")
	'	'	'SetStyle(ControlStyles.UserPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
	'	'	If Me.theOriginalFont IsNot Nothing Then
	'	'		Me.Font = New System.Drawing.Font(Me.theOriginalFont.FontFamily, Me.theOriginalFont.Size, Me.theOriginalFont.Style, Me.theOriginalFont.Unit)
	'	'	End If
	'	'	Me.Invalidate()
	'	'End If
	'	If GetStyle(ControlStyles.UserPaint) Then
	'		If Me.theOriginalFont IsNot Nothing Then
	'			Me.Font = New System.Drawing.Font(Me.theOriginalFont.FontFamily, Me.theOriginalFont.Size, Me.theOriginalFont.Style, Me.theOriginalFont.Unit)
	'		End If
	'		Me.Invalidate()
	'		'Me.Refresh()
	'	End If

	'	MyBase.OnTextChanged(e)
	'End Sub

	'Protected Overrides Sub OnVisibleChanged(e As EventArgs)
	'	MyBase.OnVisibleChanged(e)

	'	If Me.theOriginalFont Is Nothing Then
	'		'NOTE: Font gets changed at some point after changing style, messing up when cue banner is turned off, 
	'		'      so save the Font after widget is visible for first time, but before changing style within the widget.
	'		Me.theOriginalFont = New System.Drawing.Font(Me.Font.FontFamily, Me.Font.Size, Me.Font.Style, Me.Font.Unit)

	'		'SetStyle(ControlStyles.AllPaintingInWmPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
	'		'SetStyle(ControlStyles.DoubleBuffer, Me.theCueBannerText <> "" AndAlso Me.Text = "")
	'		'SetStyle(ControlStyles.UserPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
	'		'SetStyle(ControlStyles.AllPaintingInWmPaint, True)
	'		'SetStyle(ControlStyles.DoubleBuffer, True)
	'		'SetStyle(ControlStyles.UserPaint, True)
	'	End If
	'End Sub

	Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
		MyBase.OnMouseDown(e)
		'NOTE: Prevent white background flicker.
		Me.Invalidate()
	End Sub

	'NOTE: With RePaint() method, this prevents white background flicker, but hides caret too long when using arrow keys to move through text.
	'NOTE: With OnPaint() method, shows white background flicker, but caret moves smoothly. [THIS IS PREFFERED METHOD.]
	'NOTE: With OnPaint() method, tried using Invalidate() to prevent white background flicker when holding a key down. Does not prevent the flicker.
	'Protected Overrides Sub WndProc(ByRef m As Message)
	'	MyBase.WndProc(m)

	'	Select Case m.Msg
	'		'Case Win32Api.WindowsMessages.WM_NCPAINT
	'		'Case Win32Api.WindowsMessages.WM_PAINT
	'		'	RePaint()
	'		'Case Win32Api.WindowsMessages.WM_SETFOCUS, Win32Api.WindowsMessages.WM_KILLFOCUS
	'		'	RePaint()
	'		'Case Win32Api.WindowsMessages.WM_LBUTTONDOWN, Win32Api.WindowsMessages.WM_RBUTTONDOWN, Win32Api.WindowsMessages.WM_MBUTTONDOWN
	'		'	RePaint()
	'		'Case Win32Api.WindowsMessages.WM_LBUTTONUP, Win32Api.WindowsMessages.WM_RBUTTONUP, Win32Api.WindowsMessages.WM_MBUTTONUP
	'		'	RePaint()
	'		'Case Win32Api.WindowsMessages.WM_LBUTTONDBLCLK, Win32Api.WindowsMessages.WM_RBUTTONDBLCLK, Win32Api.WindowsMessages.WM_MBUTTONDBLCLK
	'		'	RePaint()
	'		'Case Win32Api.WindowsMessages.WM_KEYDOWN, Win32Api.WindowsMessages.WM_KEYUP
	'		'	'RePaint()
	'		'	Invalidate()
	'		Case Win32Api.WindowsMessages.WM_CHAR
	'			Invalidate()
	'			'Case Win32Api.WindowsMessages.WM_MOUSEMOVE
	'			'	If Not (m.WParam.Equals(IntPtr.Zero)) Then
	'			'		RePaint()
	'			'	End If
	'	End Select
	'End Sub

	'Protected Sub RePaint()
	'	'Invalidate()

	'	Dim hDC As IntPtr = Win32Api.GetWindowDC(Me.Handle)
	'	Try
	'		Using g As Graphics = Graphics.FromHdc(hDC)
	'			Dim clipRectangle As Rectangle = Rectangle.Round(g.VisibleClipBounds)

	'			' Draw background border.
	'			Using backColorBrush As New SolidBrush(WidgetDeepDisabledBackColor)
	'				Dim aRect As Rectangle = clipRectangle
	'				g.FillRectangle(backColorBrush, aRect)
	'			End Using

	'			' Draw background.
	'			Using backColorBrush As New SolidBrush(WidgetDeepBackColor)
	'				Dim aRect As Rectangle = clipRectangle
	'				aRect.Inflate(-1, -1)
	'				g.FillRectangle(backColorBrush, aRect)
	'			End Using

	'			' Draw text.
	'			If Me.Text <> "" AndAlso Me.theOriginalFont IsNot Nothing Then
	'				Dim beginCharIndex As Integer = Me.GetCharIndexFromPosition(clipRectangle.Location)
	'				Dim endCharPoint As New Point(clipRectangle.Right, clipRectangle.Bottom)
	'				Dim endCharIndex As Integer = Me.GetCharIndexFromPosition(endCharPoint)
	'				If beginCharIndex > 0 Then
	'					Dim debug As Integer = 4242
	'				End If
	'				Dim aRect As Rectangle = clipRectangle
	'				aRect.Inflate(1, 0)
	'				If Me.Multiline Then
	'					TextRenderer.DrawText(g, Me.Text, Me.theOriginalFont, aRect, WidgetTextColor, WidgetDeepBackColor)
	'				Else
	'					TextRenderer.DrawText(g, Me.Text, Me.theOriginalFont, aRect, WidgetTextColor, WidgetDeepBackColor, TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine)
	'				End If

	'				Dim positionRect As Rectangle = clipRectangle
	'				Dim selectionEndCharIndex As Integer = Me.SelectionStart + Me.SelectionLength - 1
	'				For selectionCharIndex As Integer = Me.SelectionStart To selectionEndCharIndex
	'					positionRect.Location = Me.GetPositionFromCharIndex(selectionCharIndex)
	'					positionRect.X -= 3
	'					If Me.Multiline Then
	'						TextRenderer.DrawText(g, Me.SelectedText(selectionCharIndex - Me.SelectionStart), Me.theOriginalFont, positionRect, WidgetTextColor, WidgetDeepSelectedBackColor)
	'					Else
	'						TextRenderer.DrawText(g, Me.SelectedText(selectionCharIndex - Me.SelectionStart), Me.theOriginalFont, positionRect, WidgetTextColor, WidgetDeepSelectedBackColor, TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine)
	'					End If
	'				Next
	'			End If

	'			' Draw cue banner text.
	'			If Me.theCueBannerText <> "" AndAlso Me.Text = "" AndAlso Me.theOriginalFont IsNot Nothing Then
	'				Dim drawFont As System.Drawing.Font = New System.Drawing.Font(Me.theOriginalFont.FontFamily, Me.theOriginalFont.Size, FontStyle.Italic, Me.theOriginalFont.Unit)
	'				TextRenderer.DrawText(g, Me.theCueBannerText, drawFont, clipRectangle, WidgetDisabledTextColor, WidgetDeepBackColor, TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine)
	'			End If
	'		End Using
	'	Finally
	'		Win32Api.ReleaseDC(Me.Handle, hDC)
	'	End Try
	'End Sub

	Private theCueBannerText As String
	Private theOriginalFont As Font

End Class
