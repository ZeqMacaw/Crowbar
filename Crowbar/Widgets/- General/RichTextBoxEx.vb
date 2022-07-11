Imports System.ComponentModel
Imports System.Runtime.InteropServices

Public Class RichTextBoxEx
	Inherits RichTextBox

#Region "Creation and Destruction"

	Public Sub New()
		MyBase.New()

		'NOTE: Make sure MultiLine is True because single-line is visually glitched.
		MyBase.Multiline = True
		'NOTE: Disable to use custom.
		MyBase.BorderStyle = BorderStyle.None
		MyBase.ScrollBars = RichTextBoxScrollBars.None
		'MyBase.WordWrap = False

		Me.ForeColor = WidgetTextColor
		Me.BackColor = WidgetDeepBackColor
		Me.SelectionBackColor = WidgetDeepBackColor
		'Me.thePaddingColor = WidgetDeepBackColor
		'TEST:
		Me.thePaddingColor = Color.Pink

		Me.HorizontalScrollbar = New ScrollBarEx()
		Me.Controls.Add(Me.HorizontalScrollbar)
		Me.HorizontalScrollbar.Location = New System.Drawing.Point(0, Me.ClientRectangle.Height)
		Me.HorizontalScrollbar.Name = "HorizontalScrollbar"
		Me.HorizontalScrollbar.Size = New System.Drawing.Size(Me.Width, ScrollBarEx.Consts.ScrollBarSize)
		Me.HorizontalScrollbar.ScrollOrientation = ScrollBarEx.DarkScrollOrientation.Horizontal
		Me.HorizontalScrollbar.TabIndex = 7
		Me.HorizontalScrollbar.Text = "HorizontalScrollbar"
		Me.HorizontalScrollbar.Visible = False

		Me.VerticalScrollbar = New ScrollBarEx()
		Me.Controls.Add(Me.VerticalScrollbar)
		Me.VerticalScrollbar.Location = New System.Drawing.Point(Me.ClientRectangle.Width, 0)
		Me.VerticalScrollbar.Name = "VerticalScrollbar"
		Me.VerticalScrollbar.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize, Me.Height)
		Me.VerticalScrollbar.ScrollOrientation = ScrollBarEx.DarkScrollOrientation.Vertical
		Me.VerticalScrollbar.TabIndex = 7
		Me.VerticalScrollbar.Text = "VerticalScrollbar"
		Me.VerticalScrollbar.Visible = False

		Me.CustomMenu = New ContextMenuStrip()
		Me.CustomMenu.Items.Add(Me.UndoToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.RedoToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.Separator0ToolStripSeparator)
		Me.CustomMenu.Items.Add(Me.CutToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.CopyToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.PasteToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.DeleteToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.Separator1ToolStripSeparator)
		Me.CustomMenu.Items.Add(Me.SelectAllToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.CopyAllToolStripMenuItem)
		Me.ContextMenuStrip = Me.CustomMenu

		Me.theControlHasShown = False

		'NOTE: Set each of these to the default value used by RichTextBox because Visual Studio Designer will not set the value if default is used.
		Me.theControlIsBehavingAsMultiLine = True
		'Me.theControlIsWordWrapping = True

		Me.theCueBannerText = ""
		Me.theTextAlignment = HorizontalAlignment.Left

		Me.theLineCount = 0
		Me.theScrollingIsActive = False
	End Sub

#End Region

#Region "Init and Free"

	'Private Sub Init()
	'End Sub

	'Private Sub Free()
	'End Sub

#End Region

#Region "Properties"

	<Browsable(True)>
	<Category("Appearance")>
	<Description("Colorable BorderStyle.")>
	Public Overloads Property BorderStyle As BorderStyle
		Get
			Return Me.theBorderStyle
		End Get
		Set
			Me.theBorderStyle = Value
		End Set
	End Property

	<Browsable(True)>
	<Category("Behavior")>
	<Description("Allows multiple lines of text.")>
	Public Overrides Property Multiline As Boolean
		Get
			Return Me.theControlIsBehavingAsMultiLine
		End Get
		Set
			Me.theControlIsBehavingAsMultiLine = Value
		End Set
	End Property

	<Browsable(True)>
	<Category("Layout")>
	<Description("Colorable scrollbars.")>
	Public Overloads Property ScrollBars As RichTextBoxScrollBars
		Get
			Return Me.theScrollBars
		End Get
		Set
			Me.theScrollBars = Value
		End Set
	End Property

	'<Browsable(True)>
	'<Category("Behavior")>
	'<Description("Wrap a text line when it extends past control width.")>
	'Public Overloads Property WordWrap As Boolean
	'	Get
	'		Return Me.theControlIsWordWrapping
	'	End Get
	'	Set
	'		Me.theControlIsWordWrapping = Value
	'	End Set
	'End Property

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

	<Browsable(True)>
	<Category("Appearance")>
	<Description("Left-align, center, or right-aligned.")>
	<DefaultValue(HorizontalAlignment.Left)>
	Public Property TextAlign As HorizontalAlignment
		Get
			Return Me.theTextAlignment
		End Get
		Set
			If Me.theTextAlignment <> Value Then
				Me.theTextAlignment = Value
			End If
		End Set
	End Property

#End Region

#Region "Widget Event Handlers"

	Protected Overrides Sub OnGotFocus(e As EventArgs)
		MyBase.OnGotFocus(e)
		Me.Invalidate()
	End Sub

	Protected Overrides Sub OnHandleCreated(e As EventArgs)
		MyBase.OnHandleCreated(e)

		If Me.theOriginalFont Is Nothing Then
			Me.Font = New Font(SystemFonts.MessageBoxFont.Name, 8.25)
			'NOTE: Font gets changed at some point after changing style, messing up when cue banner is turned off, 
			'      so save the Font before changing style.
			Me.theOriginalFont = New System.Drawing.Font(Me.Font.FontFamily, Me.Font.Size, Me.Font.Style, Me.Font.Unit)

			SetStyle(ControlStyles.AllPaintingInWmPaint, True)
			SetStyle(ControlStyles.DoubleBuffer, True)
			SetStyle(ControlStyles.UserPaint, True)

			Me.AutoWordSelection = True
			Me.AutoWordSelection = False
		End If
	End Sub

	Protected Overrides Sub OnHScroll(e As EventArgs)
		MyBase.OnHScroll(e)
		Me.Invalidate()
		Me.UpdateHorizontalScrollbar()
	End Sub

	Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
		'NOTE: Part of faking a single-line.
		If Not Me.theControlIsBehavingAsMultiLine AndAlso e.KeyCode = Keys.Enter Then
			e.SuppressKeyPress = True
		End If
		MyBase.OnKeyDown(e)
		Me.Invalidate()
	End Sub

	Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
		'NOTE: Part of faking a single-line.
		If Not Me.theControlIsBehavingAsMultiLine AndAlso e.KeyChar = ChrW(Keys.Return) Then
			Exit Sub
		End If
		MyBase.OnKeyPress(e)
	End Sub

	Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
		MyBase.OnMouseDown(e)
		Me.Invalidate()
	End Sub

	Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
		MyBase.OnMouseMove(e)
		Me.Invalidate()
	End Sub

	Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
		MyBase.OnMouseWheel(e)

		If Me.VerticalScrollbar.Visible Then
			'NOTE: Scroll by 3 text lines.
			Dim textSize As Size = TextRenderer.MeasureText("Wy", Me.theOriginalFont)
			Dim upOrDownValue As Integer = textSize.Height * 3

			If e.Delta > 0 Then
				' Moving wheel away from user = up.
				VerticalScrollbar.Value -= upOrDownValue
			Else
				' Moving wheel toward user = down.
				VerticalScrollbar.Value += upOrDownValue
			End If
		End If
	End Sub

	'NOTE: Single-line caret is visually glitched so always use Multiline.
	'      Fake the single-line.
	'NOTE: This all works by working with the underlying RTB positioning of text and caret.
	Protected Overrides Sub OnPaint(e As PaintEventArgs)
		'NOTE: Completely override painting by OS.
		'MyBase.OnPaint(e)

		Dim g As Graphics = e.Graphics
		Dim clipRectangle As Rectangle = e.ClipRectangle
		Dim clientRectangle As Rectangle = Me.ClientRectangle

		' Draw text.
		If Me.Text <> "" AndAlso Me.theOriginalFont IsNot Nothing Then
			'NOTE: Use NoPadding to avoid incorrect caret placement.
			Dim formatFlags As TextFormatFlags = TextFormatFlags.NoPadding Or TextFormatFlags.WordBreak

			' All of the Get* functions return values based on what is displayed, not what is assigned (Lines property).
			Dim textPositionRect As Rectangle = clientRectangle
			Dim startCharIndex As Integer = Me.GetCharIndexFromPosition(clipRectangle.Location)
			Dim endCharIndex As Integer = Me.GetCharIndexFromPosition(New Point(clipRectangle.Right, clipRectangle.Bottom))
			For charIndex As Integer = startCharIndex To endCharIndex
				textPositionRect.Location = Me.GetPositionFromCharIndex(charIndex)
				TextRenderer.DrawText(g, Me.Text(charIndex), Me.theOriginalFont, textPositionRect, WidgetTextColor, WidgetDeepBackColor, formatFlags)
			Next
			'TextRenderer.DrawText(g, Me.Text.Substring(startCharIndex, endCharIndex - startCharIndex + 1), Me.theOriginalFont, Me.GetPositionFromCharIndex(startCharIndex), WidgetTextColor, WidgetDeepBackColor, formatFlags)

			Dim selectionPositionRect As Rectangle = clientRectangle
			Dim selectionEndCharIndex As Integer = Me.SelectionStart + Me.SelectionLength - 1
			For selectionCharIndex As Integer = Me.SelectionStart To selectionEndCharIndex
				selectionPositionRect.Location = Me.GetPositionFromCharIndex(selectionCharIndex)
				TextRenderer.DrawText(g, Me.SelectedText(selectionCharIndex - Me.SelectionStart), Me.theOriginalFont, selectionPositionRect, WidgetTextColor, WidgetDeepSelectedBackColor, formatFlags)
			Next
		End If

		' Draw cue banner text.
		If Me.theCueBannerText <> "" AndAlso Me.Text = "" AndAlso Me.theOriginalFont IsNot Nothing Then
			Dim drawFont As System.Drawing.Font = New System.Drawing.Font(Me.theOriginalFont.FontFamily, Me.theOriginalFont.Size, FontStyle.Italic, Me.theOriginalFont.Unit)
			' Add top and bottom padding.
			clientRectangle.Inflate(0, -1)
			TextRenderer.DrawText(g, Me.theCueBannerText, drawFont, clientRectangle, WidgetDisabledTextColor, WidgetDeepBackColor, TextFormatFlags.Left)
		End If
	End Sub

	Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
		'NOTE: Completely override painting by OS.
		'MyBase.OnPaintBackground(e)

		' Draw background border.
		'Using borderColorPen As New Pen(WidgetDisabledTextColor)
		Using borderColorPen As New Pen(Color.Green)
			Dim aRect As Rectangle = Me.ClientRectangle
			'NOTE: DrawRectangle width and height are interpreted as the right and bottom pixels to draw.
			aRect.Width += -1
			aRect.Height += -1
			e.Graphics.DrawRectangle(borderColorPen, aRect)
		End Using

		' Draw background.
		'Using backColorBrush As New SolidBrush(WidgetDeepBackColor)
		Using backColorBrush As New SolidBrush(Color.Red)
			Dim aRect As Rectangle = Me.ClientRectangle
			aRect.Inflate(-1, -1)
			e.Graphics.FillRectangle(backColorBrush, aRect)
		End Using
	End Sub

	Protected Overrides Sub OnSizeChanged(e As EventArgs)
		MyBase.OnSizeChanged(e)
		Me.Invalidate()
		Me.UpdateScrollbars()
	End Sub

	Protected Overrides Sub OnTextChanged(e As EventArgs)
		MyBase.OnTextChanged(e)

		If Me.theControlIsBehavingAsMultiLine AndAlso Me.theLineCount <> Me.GetLineFromCharIndex(Me.TextLength - 1) - 1 Then
			'NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
			Win32Api.SetWindowPos(Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
		End If
		Me.Invalidate()
		Me.UpdateScrollbars()
	End Sub

	Protected Overrides Sub OnVisibleChanged(e As EventArgs)
		MyBase.OnVisibleChanged(e)

		If Me.Visible Then
			If Not Me.theControlHasShown Then
				Me.theControlHasShown = True
				Me.SelectAll()
				Me.SelectionAlignment = Me.theTextAlignment
				Me.SelectionLength = 0

				'NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
				Win32Api.SetWindowPos(Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
			End If

			Me.Invalidate()
			Me.UpdateScrollbars()
		End If
	End Sub

	Protected Overrides Sub OnVScroll(e As EventArgs)
		MyBase.OnVScroll(e)
		Me.Invalidate()
		Me.UpdateVerticalScrollbar()
	End Sub

	'NOTE: List of Windows messages related to TextBox drawing that Windows uses internally.
	'Case Win32Api.WindowsMessages.WM_PAINT
	'Case Win32Api.WindowsMessages.WM_SETFOCUS, Win32Api.WindowsMessages.WM_KILLFOCUS
	'Case Win32Api.WindowsMessages.WM_LBUTTONDOWN, Win32Api.WindowsMessages.WM_RBUTTONDOWN, Win32Api.WindowsMessages.WM_MBUTTONDOWN
	'Case Win32Api.WindowsMessages.WM_LBUTTONUP, Win32Api.WindowsMessages.WM_RBUTTONUP, Win32Api.WindowsMessages.WM_MBUTTONUP
	'Case Win32Api.WindowsMessages.WM_LBUTTONDBLCLK, Win32Api.WindowsMessages.WM_RBUTTONDBLCLK, Win32Api.WindowsMessages.WM_MBUTTONDBLCLK
	'Case Win32Api.WindowsMessages.WM_KEYDOWN, Win32Api.WindowsMessages.WM_KEYUP, Win32Api.WindowsMessages.WM_CHAR
	'Case Win32Api.WindowsMessages.WM_MOUSEMOVE
	Protected Overrides Sub WndProc(ByRef m As Message)
		Select Case m.Msg
			Case Win32Api.WindowsMessages.WM_NCCALCSIZE
				Me.OnNonClientCalcSize(m)
			Case Win32Api.WindowsMessages.WM_NCPAINT
				Me.OnNonClientPaint(m)
		End Select

		MyBase.WndProc(m)
	End Sub

	Private Sub OnNonClientCalcSize(ByRef m As Message)
		Me.UpdatePadding()
		If CInt(m.WParam) = 0 Then
			Dim rect As Win32Api.RECT = CType(Marshal.PtrToStructure(m.LParam, GetType(Win32Api.RECT)), Win32Api.RECT)
			Me.ResizeClientRect(Me.Padding, rect)
			Marshal.StructureToPtr(rect, m.LParam, False)
			m.Result = IntPtr.Zero
		ElseIf CInt(m.WParam) = 1 Then
			Dim nccsp As Win32Api.NCCALCSIZE_PARAMS = CType(Marshal.PtrToStructure(m.LParam, GetType(Win32Api.NCCALCSIZE_PARAMS)), Win32Api.NCCALCSIZE_PARAMS)
			Me.ResizeClientRect(Me.Padding, nccsp.rect0)
			Marshal.StructureToPtr(nccsp, m.LParam, False)
			m.Result = IntPtr.Zero
		End If
	End Sub

	Private Sub OnNonClientPaint(ByRef m As Message)
		Dim hDC As IntPtr = Win32Api.GetWindowDC(Me.Handle)
		Try
			Using g As Graphics = Graphics.FromHdc(hDC)
				Using backColorBrush As New SolidBrush(Me.thePaddingColor)
					Dim aRect As RectangleF = g.VisibleClipBounds
					g.FillRectangle(backColorBrush, aRect)
				End Using
			End Using
		Finally
			Win32Api.ReleaseDC(Me.Handle, hDC)
		End Try
		m.Result = IntPtr.Zero
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub HorizontalScrollbar_ValueChanged(ByVal sender As Object, ByVal e As ScrollValueEventArgs) Handles HorizontalScrollbar.ValueChanged
		Me.UpdateScrolling(e.Value, 0)
	End Sub

	Private Sub VerticalScrollBar_ValueChanged(ByVal sender As Object, ByVal e As ScrollValueEventArgs) Handles VerticalScrollbar.ValueChanged
		Me.UpdateScrolling(0, e.Value)
	End Sub

#Region "ContextMenu Event Handlers"

	Private Sub CustomMenu_Opening(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomMenu.Opening
		Me.UndoToolStripMenuItem.Enabled = Not Me.ReadOnly AndAlso Me.CanUndo
		Me.RedoToolStripMenuItem.Enabled = Not Me.ReadOnly AndAlso Me.CanRedo
		Me.CutToolStripMenuItem.Enabled = Not Me.ReadOnly AndAlso Me.SelectionLength > 0
		Me.CopyToolStripMenuItem.Enabled = Me.SelectionLength > 0
		Me.PasteToolStripMenuItem.Enabled = Not Me.ReadOnly AndAlso Clipboard.ContainsText()
		Me.DeleteToolStripMenuItem.Enabled = Not Me.ReadOnly AndAlso Me.SelectionLength > 0
		Me.SelectAllToolStripMenuItem.Enabled = Me.TextLength > 0 AndAlso Me.SelectionLength < Me.TextLength
		Me.CopyAllToolStripMenuItem.Enabled = Me.TextLength > 0 AndAlso Me.SelectionLength < Me.TextLength
	End Sub

	Private Sub UndoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoToolStripMenuItem.Click
		Me.Undo()
	End Sub

	Private Sub RedoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedoToolStripMenuItem.Click
		Me.Redo()
	End Sub

	Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click
		Me.Cut()
	End Sub

	Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
		Me.Copy()
	End Sub

	Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
		Me.Paste()
	End Sub

	Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
		Me.SelectedText = ""
	End Sub

	Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
		Me.SelectAll()
	End Sub

	Private Sub CopyAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyAllToolStripMenuItem.Click
		Me.SelectAll()
		Me.Copy()
		'Me.SelectionLength = 0
	End Sub
#End Region

#End Region

#Region "Core Event Handlers"

#End Region

#Region "Private Methods"

	Private Function GetContentWidthWithNoWordWrap() As Integer
		Dim contentWidth As Integer = 0

		If Me.Text <> "" Then
			'Dim textSize As Size
			'Dim startCharIndex As Integer = Me.GetCharIndexFromPosition(Me.ClientRectangle.Location)
			'Dim startLineIndex As Integer = GetLineFromCharIndex(startCharIndex)
			'Dim endCharIndex As Integer = Me.GetCharIndexFromPosition(New Point(Me.ClientRectangle.Right, Me.ClientRectangle.Bottom))
			'Dim endLineIndex As Integer = GetLineFromCharIndex(endCharIndex)
			'Dim extraDisplayLineCount As Integer = 0
			'Dim firstCharIndexOfLine As Integer
			'Dim lastCharIndexOfLine As Integer
			'Dim lineText As String
			'For lineIndex As Integer = startLineIndex To endLineIndex
			'	firstCharIndexOfLine = Me.GetFirstCharIndexFromLine(lineIndex)
			'	If lineIndex < endLineIndex Then
			'		lastCharIndexOfLine = Me.GetFirstCharIndexFromLine(lineIndex + 1) - 1
			'	Else
			'		lastCharIndexOfLine = Me.TextLength - 1
			'	End If
			'	lineText = Me.Text.Substring(firstCharIndexOfLine, lastCharIndexOfLine - firstCharIndexOfLine + 1)
			'	textSize = TextRenderer.MeasureText(lineText, Me.theOriginalFont)
			'	If contentWidth < textSize.Width Then
			'		contentWidth = textSize.Width
			'	End If
			'Next
			'------
			Dim textSize As Size
			For Each textLine As String In Lines
				textSize = TextRenderer.MeasureText(textLine, Me.theOriginalFont)
				If contentWidth < textSize.Width Then
					contentWidth = textSize.Width
				End If
			Next
		End If

		Return contentWidth
	End Function

	'NOTE: Me.Padding is unused by the underlying RichTextBox, but using it in this widget.
	Private Sub UpdatePadding()
		Dim left As Integer = 2
		Dim top As Integer = 2
		Dim right As Integer = 2
		Dim bottom As Integer = 2
		Dim textSize As Size = TextRenderer.MeasureText("Wy", Me.theOriginalFont)

		If Not Me.theControlIsBehavingAsMultiLine Then
			'Me.Padding = New Padding(2, CInt(Me.Height * 0.5 - textSize.Height * 0.5), 2, 2)
			top = CInt(Me.Height * 0.5 - textSize.Height * 0.5)
		Else
			If Not Me.WordWrap Then
				'If Not Me.theControlIsWordWrapping Then
				Dim contentWidth As Integer = Me.GetContentWidthWithNoWordWrap()
				If contentWidth > Me.ClientRectangle.Width Then
					bottom += ScrollBarEx.Consts.ScrollBarSize
				End If
			End If

			Dim lineCount As Integer = Me.GetLineFromCharIndex(Me.TextLength - 1) + 1
			Dim contentHeight As Integer = lineCount * textSize.Height
			If contentHeight > Me.ClientRectangle.Height Then
				'Me.Padding = New Padding(2, 2, 2 + ScrollBarEx.Consts.ScrollBarSize, 2)
				right += ScrollBarEx.Consts.ScrollBarSize
				'Else
				'	Me.Padding = New Padding(2, 2, 2, 2)
			End If
		End If

		Me.Padding = New Padding(left, top, right, bottom)
	End Sub

	Private Sub ResizeClientRect(ByVal padding As Padding, ByRef rect As Win32Api.RECT)
		rect.Left += Me.Padding.Left
		rect.Top += Me.Padding.Top
		rect.Right -= Me.Padding.Right
		rect.Bottom -= Me.Padding.Bottom
	End Sub

	Private Sub UpdateScrolling(ByVal leftOrRightValue As Integer, ByVal upOrDownValue As Integer)
		If Not Me.theScrollingIsActive Then
			Me.theScrollingIsActive = True

			Dim scrollPosition As New Point(leftOrRightValue, upOrDownValue)
			Win32Api.RtfScroll(Me.Handle, Win32Api.WindowsMessages.EM_SETSCROLLPOS, IntPtr.Zero, scrollPosition)

			Me.theScrollingIsActive = False
		End If
	End Sub

	Private Sub UpdateScrollbars()
		Me.UpdateHorizontalScrollbar()
		Me.UpdateVerticalScrollbar()

		If Me.HorizontalScrollbar.Visible AndAlso Me.VerticalScrollbar.Visible Then
			Me.HorizontalScrollbar.Size = New System.Drawing.Size(Me.Width - ScrollBarEx.Consts.ScrollBarSize, ScrollBarEx.Consts.ScrollBarSize)
			Me.VerticalScrollbar.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize, Me.Height - ScrollBarEx.Consts.ScrollBarSize)
		End If
	End Sub

	Private Sub UpdateHorizontalScrollbar()
		'NOTE: Parent can be Nothing on exiting. Prevent the exception with this check.
		If Not Me.theScrollingIsActive AndAlso Not Me.WordWrap AndAlso Me.theControlIsBehavingAsMultiLine AndAlso Me.Parent IsNot Nothing Then
			'If Not Me.theScrollingIsActive AndAlso Not Me.theControlIsWordWrapping AndAlso Me.HorizontalScrollbar.Created AndAlso Me.Parent IsNot Nothing AndAlso Me.Parent.Created Then
			'Me.theLineCount = lineCount
			Dim contentWidth As Integer = Me.GetContentWidthWithNoWordWrap()
			If contentWidth > Me.ClientRectangle.Width Then
				Me.theScrollingIsActive = True

				Me.HorizontalScrollbar.Minimum = 0
				Me.HorizontalScrollbar.Maximum = contentWidth
				Dim scrollPosition As New Point()
				Win32Api.RtfScroll(Me.Handle, Win32Api.WindowsMessages.EM_GETSCROLLPOS, IntPtr.Zero, scrollPosition)
				Me.HorizontalScrollbar.Value = scrollPosition.X
				Me.HorizontalScrollbar.ViewSize = Me.ClientRectangle.Width
				Dim textSizeForCharWidth As Size = TextRenderer.MeasureText("T", Me.theOriginalFont)
				Me.HorizontalScrollbar.SmallChange = textSizeForCharWidth.Width
				Me.HorizontalScrollbar.LargeChange = Me.Width - textSizeForCharWidth.Width * 2

				Me.HorizontalScrollbar.Show()

				Me.HorizontalScrollbar.Size = New System.Drawing.Size(Me.Width, ScrollBarEx.Consts.ScrollBarSize)
				'NOTE: Assign to Parent so it can draw over non-client area of RichTextBoxEx.
				Me.HorizontalScrollbar.Parent = Me.Parent
				Me.HorizontalScrollbar.BringToFront()
				'NOTE: Location must be relative to Parent.
				Dim aPoint As New Point(Me.ClientRectangle.Left - Me.Padding.Left, Me.ClientRectangle.Height + Me.Padding.Top)
				aPoint = Me.PointToScreen(aPoint)
				aPoint = Me.Parent.PointToClient(aPoint)
				Me.HorizontalScrollbar.Location = aPoint

				Me.theScrollingIsActive = False
			Else
				Me.HorizontalScrollbar.Hide()
			End If
		End If
	End Sub

	Private Sub UpdateVerticalScrollbar()
		'NOTE: Parent can be Nothing on exiting. Prevent the exception with this check.
		If Not Me.theScrollingIsActive AndAlso Me.theControlIsBehavingAsMultiLine AndAlso Me.Parent IsNot Nothing Then
			Dim textSize As Size = TextRenderer.MeasureText("Wy", Me.theOriginalFont)
			'DEBUG: Using this line causes single-line boxes that do not use Parse() to have misplaced caret.
			'       This part of line causes problem: Me.GetLineFromCharIndex(Me.TextLength - 1) 
			Dim lineCount As Integer = Me.GetLineFromCharIndex(Me.TextLength - 1) + 1
			'Dim lineCount As Integer = 2
			Me.theLineCount = lineCount
			Dim contentHeight As Integer = lineCount * textSize.Height
			If contentHeight > Me.ClientRectangle.Height Then
				Me.theScrollingIsActive = True

				Me.VerticalScrollbar.Minimum = 0
				Me.VerticalScrollbar.Maximum = contentHeight
				Dim aCharIndex As Integer = Me.GetCharIndexFromPosition(New Point(0, 0))
				Dim lineIndex As Integer = Me.GetLineFromCharIndex(aCharIndex)
				Me.VerticalScrollbar.Value = lineIndex * textSize.Height
				Me.VerticalScrollbar.ViewSize = Me.ClientRectangle.Height
				Me.VerticalScrollbar.SmallChange = textSize.Height
				Me.VerticalScrollbar.LargeChange = Me.Height - textSize.Height * 2

				Me.VerticalScrollbar.Show()

				Me.VerticalScrollbar.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize, Me.Height)
				'NOTE: Assign to Parent so it can draw over non-client area of RichTextBoxEx.
				Me.VerticalScrollbar.Parent = Me.Parent
				Me.VerticalScrollbar.BringToFront()
				'NOTE: Location must be relative to Parent.
				Dim aPoint As New Point(Me.ClientRectangle.Width + Me.Padding.Left, Me.ClientRectangle.Top - Me.Padding.Top)
				aPoint = Me.PointToScreen(aPoint)
				aPoint = Me.Parent.PointToClient(aPoint)
				Me.VerticalScrollbar.Location = aPoint

				Me.theScrollingIsActive = False
			Else
				Me.VerticalScrollbar.Hide()
			End If
		End If
	End Sub

#End Region

#Region "Data"

	Private theBorderStyle As BorderStyle
	Private theControlIsBehavingAsMultiLine As Boolean
	Private theScrollBars As RichTextBoxScrollBars
	'Private theControlIsWordWrapping As Boolean

	Private WithEvents CustomMenu As ContextMenuStrip

	Private WithEvents UndoToolStripMenuItem As New ToolStripMenuItem("&Undo")
	Private WithEvents RedoToolStripMenuItem As New ToolStripMenuItem("&Redo")
	Private WithEvents Separator0ToolStripSeparator As New ToolStripSeparator()
	Private WithEvents CutToolStripMenuItem As New ToolStripMenuItem("Cu&t")
	Private WithEvents CopyToolStripMenuItem As New ToolStripMenuItem("&Copy")
	Private WithEvents PasteToolStripMenuItem As New ToolStripMenuItem("&Paste")
	Private WithEvents DeleteToolStripMenuItem As New ToolStripMenuItem("&Delete")
	Private WithEvents Separator1ToolStripSeparator As New ToolStripSeparator()
	Private WithEvents SelectAllToolStripMenuItem As New ToolStripMenuItem("Select &All")
	Private WithEvents CopyAllToolStripMenuItem As New ToolStripMenuItem("Copy A&ll")

	Private theControlHasShown As Boolean
	Private theCueBannerText As String
	Private theOriginalFont As Font
	Private theTextAlignment As HorizontalAlignment

	Private thePaddingColor As Color

	Private WithEvents HorizontalScrollbar As ScrollBarEx
	Private WithEvents VerticalScrollbar As ScrollBarEx
	Private theLineCount As Integer
	Private theScrollingIsActive As Boolean

#End Region

End Class
