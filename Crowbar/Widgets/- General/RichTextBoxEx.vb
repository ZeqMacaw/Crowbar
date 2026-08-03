Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.VisualStyles

Public Class RichTextBoxEx
	Inherits RichTextBox

#Region "Creation and Destruction"

	Public Sub New()
		MyBase.New()

		MyBase.DetectUrls = True
		'NOTE: Make sure MultiLine is True because single-line is visually glitched.
		MyBase.Multiline = True

		'NOTE: Disable to use custom. Must have these 2 lines here to prevent exception at startup.
		MyBase.BorderStyle = BorderStyle.None
		MyBase.ScrollBars = RichTextBoxScrollBars.None

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

		'NOTE: Use NoPadding to avoid incorrect caret placement.
		Me.theTextFormatFlags = TextFormatFlags.ExpandTabs Or TextFormatFlags.NoPadding Or TextFormatFlags.NoPrefix Or TextFormatFlags.PreserveGraphicsClipping
		'If Me.theControlIsBehavingAsMultiLine AndAlso Me.WordWrap Then
		'	'DEBUG: I suspect that word-breaks in this OnPaint are not the same as the underlying RichTextBox.
		'	formatFlags = formatFlags Or TextFormatFlags.WordBreak
		'	'formatFlags = formatFlags Or TextFormatFlags.WordBreak Or TextFormatFlags.TextBoxControl
		'	'formatFlags = formatFlags Or TextFormatFlags.TextBoxControl
		'End If

		'NOTE: Set each of these to the default value used by RichTextBox because Visual Studio Designer will not set the value if default is used.
		' Internal var for RichTextBox.MultiLine.
		Me.theControlIsBehavingAsMultiLine = True

		Me.theSelectionIsEnabled = True
		Me.theCueBannerText = ""
		Me.theTextAlignment = HorizontalAlignment.Left

		Me.theLineCount = 0
		Me.theScrollingIsActive = False
	End Sub

#End Region

#Region "Init and Free"

	Private Sub Init()
		' [04-Feb-2026] Because Me.DesignMode is unreliable in nested widgets, must do this check to prevent a crash.
		If TheApp IsNot Nothing Then
			Me.UpdateTheme()
			AddHandler TheApp.Settings.PropertyChanged, AddressOf Me.AppSettings_PropertyChanged
		End If
	End Sub

	Private Sub Free()
		' [04-Feb-2026] Because Me.DesignMode is unreliable in nested widgets, must do this check to prevent a crash.
		If TheApp IsNot Nothing Then
			RemoveHandler TheApp.Settings.PropertyChanged, AddressOf Me.AppSettings_PropertyChanged
		End If
	End Sub

#End Region

#Region "Properties"

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
	<Category("Behavior")>
	<Description("Disables changing of text.")>
	Public Overloads Property [ReadOnly] As Boolean
		Get
			Return MyBase.ReadOnly
		End Get
		Set
			MyBase.ReadOnly = Value
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

	Public Property SelectionEnabled As Boolean
		Get
			Return Me.theSelectionIsEnabled
		End Get
		Set
			Me.theSelectionIsEnabled = Value
		End Set
	End Property

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

	Protected Overrides Sub OnHandleCreated(e As EventArgs)
		MyBase.OnHandleCreated(e)

		If Me.theOriginalFont Is Nothing Then
			Me.Font = New Font(SystemFonts.MessageBoxFont.Name, 8.25)
			'NOTE: Font gets changed at some point after changing style, messing up when cue banner is turned off, 
			'      so save the Font before changing style.
			Me.theOriginalFont = New System.Drawing.Font(Me.Font.FontFamily, Me.Font.Size, Me.Font.Style, Me.Font.Unit)
		End If

		' [04-Feb-2026] Me.DesignMode is unreliable in nested widgets.
		'If Not Me.DesignMode Then
		Me.Init()
		'End If

		' Sometimes this is True for unknown reason, so force it to False.
		MyBase.AutoWordSelection = False
	End Sub

	Protected Overrides Sub OnHandleDestroyed(e As EventArgs)
		Me.Free()
		MyBase.OnHandleDestroyed(e)
	End Sub

	Protected Overrides Sub OnGotFocus(e As EventArgs)
		' This 'If' block prevents selection of text, as wanted in ComboUserControl.
		If Not Me.theSelectionIsEnabled Then
			MyBase.Enabled = False
			MyBase.Enabled = True
		End If
		MyBase.OnGotFocus(e)
		Me.Invalidate()
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

	'Protected Overrides Sub OnLinkClicked(ByVal e As LinkClickedEventArgs)
	'	MyBase.OnLinkClicked(e)
	'	If Me.[ReadOnly] Then
	'		Process.Start(e.LinkText)
	'	End If
	'End Sub

	'Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
	'	MyBase.OnMouseDown(e)
	'	Me.Invalidate()
	'End Sub

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
				Me.VerticalScrollbar.Value -= upOrDownValue
			Else
				' Moving wheel toward user = down.
				Me.VerticalScrollbar.Value += upOrDownValue
			End If
		End If
	End Sub

	'NOTE: Single-line caret is visually glitched so always use Multiline.
	'      Fake the single-line.
	'NOTE: This all works by working with the underlying RTB positioning of text and caret.
	' Need the following line for OnPaint() to be called by Windows:
	'	Me.SetStyle(ControlStyles.UserPaint, True)
	Protected Overrides Sub OnPaint(e As PaintEventArgs)
		'NOTE: Completely override painting by OS.
		'MyBase.OnPaint(e)

		'Dim theme As RichTextBoxTheme = Nothing
		'' This check prevents problems with viewing and saving Forms in VS Designer.
		'If TheApp IsNot Nothing Then
		'	theme = TheApp.Settings.SelectedAppTheme.RichTextBoxTheme
		'End If
		'If theme IsNot Nothing AndAlso Me.theThemeIsUsed Then
		'	'IMPORTANT: Only assign ForeColor and BackColor once in OnPaint();
		'	'           otherwise OnPaint will be called over 100 times
		'	'           and much of the window will not be painted.
		'	If Me.Enabled Then
		'		Me.ForeColor = theme.EnabledForeColor
		'	Else
		'		Me.ForeColor = theme.DisabledForeColor
		'	End If
		'	If MyBase.[ReadOnly] Then
		'		MyBase.BackColor = theme.DisabledBackColor
		'	Else
		'		MyBase.BackColor = theme.EnabledBackColor
		'	End If
		'End If

		Dim g As Graphics = e.Graphics

		' Draw text.
		If Me.Text <> "" AndAlso Me.theOriginalFont IsNot Nothing Then
			If Not Me.theControlIsBehavingAsMultiLine AndAlso Not Me.WordWrap Then
				' Draw full text.

				TextRenderer.DrawText(g, Me.Text, Me.theOriginalFont, Me.GetPositionFromCharIndex(0), Me.ForeColor, Me.BackColor, Me.theTextFormatFlags)
				'TextRenderer.DrawText(g, Me.Text, Me.theOriginalFont, clientRectangle, Me.ForeColor, Me.BackColor, Me.theTextFormatFlags)
				'Dim textLinePositionRect As Rectangle = Me.ClientRectangle
				'textLinePositionRect.Location = Me.GetPositionFromCharIndex(0)
				'TextRenderer.DrawText(g, Me.Text, Me.theOriginalFont, textLinePositionRect, Me.ForeColor, MyBase.BackColor, Me.theTextFormatFlags)
				'Dim endCharIndex As Integer = Me.GetCharIndexFromPosition(New Point(clipRectangle.Right, clipRectangle.Bottom))
				'Me.DrawNormalText(g, 0, 0, endCharIndex)

				If Me.SelectionLength > 0 Then
					Me.DrawSelectedText(g, Me.SelectionStart, Me.GetFirstCharIndexFromLine(0), Me.SelectionStart + Me.SelectionLength - 1)
				End If
			Else
				Dim clipRectangle As Rectangle = e.ClipRectangle
				'DEBUG: Color the clip rectangle.
				'If Me.theTestColorIsBlue Then
				'	Using backColorBrush As New SolidBrush(Color.Blue)
				'		Dim aRect As Rectangle = e.ClipRectangle
				'		e.Graphics.FillRectangle(backColorBrush, aRect)
				'	End Using
				'	Me.theTestColorIsBlue = False
				'Else
				'	Using backColorBrush As New SolidBrush(Color.Red)
				'		Dim aRect As Rectangle = e.ClipRectangle
				'		e.Graphics.FillRectangle(backColorBrush, aRect)
				'	End Using
				'	Me.theTestColorIsBlue = True
				'End If

				'======

				' All of the Get* functions return values based on what is displayed, not what is assigned (Lines property).
				Dim startCharIndex As Integer = Me.GetCharIndexFromPosition(clipRectangle.Location)
				Dim endCharIndex As Integer = Me.GetCharIndexFromPosition(New Point(clipRectangle.Right, clipRectangle.Bottom))

				Dim firstUnselectedStartCharIndex As Integer = startCharIndex
				Dim firstUnselectedEndCharIndex As Integer = endCharIndex
				Dim selectedStartCharIndex As Integer = -1
				Dim selectedLineStartCharIndex As Integer = -1
				Dim selectedEndCharIndex As Integer = -1
				Dim secondUnselectedStartCharIndex As Integer = -1
				Dim secondUnselectedLineStartCharIndex As Integer = -1
				Dim secondUnselectedEndCharIndex As Integer = -1
				Dim lineIndex As Integer

				If Me.SelectionLength > 0 AndAlso startCharIndex <= Me.SelectionStart + Me.SelectionLength - 1 AndAlso endCharIndex >= Me.SelectionStart Then
					selectedStartCharIndex = Me.SelectionStart
					If selectedStartCharIndex <= startCharIndex Then
						firstUnselectedStartCharIndex = -1
						firstUnselectedEndCharIndex = -1
						selectedStartCharIndex = startCharIndex
					Else
						firstUnselectedEndCharIndex = selectedStartCharIndex - 1
					End If

					lineIndex = Me.GetLineFromCharIndex(selectedStartCharIndex)
					selectedLineStartCharIndex = Me.GetFirstCharIndexFromLine(lineIndex)

					selectedEndCharIndex = Me.SelectionStart + Me.SelectionLength - 1
					If selectedEndCharIndex > endCharIndex Then
						selectedEndCharIndex = endCharIndex
					Else
						secondUnselectedStartCharIndex = selectedEndCharIndex + 1
						secondUnselectedEndCharIndex = endCharIndex

						lineIndex = Me.GetLineFromCharIndex(secondUnselectedStartCharIndex)
						secondUnselectedLineStartCharIndex = Me.GetFirstCharIndexFromLine(lineIndex)
					End If
				End If

				Dim startOfLineCharIndex As Integer
				Dim endOfLineCharIndex As Integer

				If firstUnselectedStartCharIndex >= 0 Then
					' Draw normal (unselected) text in first line that is before any selected text.
					lineIndex = Me.GetLineFromCharIndex(firstUnselectedStartCharIndex)
					startOfLineCharIndex = Me.GetFirstCharIndexFromLine(lineIndex)
					lineIndex += 1
					endOfLineCharIndex = Me.GetFirstCharIndexFromLine(lineIndex) - 1
					'NOTE: If lineIndex is greater than the last line, then GetFirstCharIndexFromLine() returns -1.
					If endOfLineCharIndex < 0 OrElse endOfLineCharIndex > firstUnselectedEndCharIndex Then
						endOfLineCharIndex = firstUnselectedEndCharIndex
					End If
					Me.DrawNormalText(g, firstUnselectedStartCharIndex, startOfLineCharIndex, endOfLineCharIndex)

					' Draw remaining normal (unselected) text lines that are before any selected text.
					While endOfLineCharIndex <> firstUnselectedEndCharIndex AndAlso (startOfLineCharIndex < selectedStartCharIndex OrElse selectedStartCharIndex = -1)
						startOfLineCharIndex = Me.GetFirstCharIndexFromLine(lineIndex)
						lineIndex += 1
						endOfLineCharIndex = Me.GetFirstCharIndexFromLine(lineIndex) - 1
						'NOTE: If lineIndex is greater than the last line, then GetFirstCharIndexFromLine() returns -1.
						If endOfLineCharIndex < 0 OrElse endOfLineCharIndex > firstUnselectedEndCharIndex Then
							endOfLineCharIndex = firstUnselectedEndCharIndex
						End If
						Me.DrawNormalText(g, startOfLineCharIndex, startOfLineCharIndex, endOfLineCharIndex)
					End While
				End If

				If selectedStartCharIndex >= 0 Then
					' Draw selected text in first line that has a selection.
					lineIndex = Me.GetLineFromCharIndex(selectedStartCharIndex)
					startOfLineCharIndex = Me.GetFirstCharIndexFromLine(lineIndex)
					lineIndex += 1
					endOfLineCharIndex = Me.GetFirstCharIndexFromLine(lineIndex) - 1
					'NOTE: If lineIndex is greater than the last line, then GetFirstCharIndexFromLine() returns -1.
					If endOfLineCharIndex < 0 OrElse endOfLineCharIndex > selectedEndCharIndex Then
						endOfLineCharIndex = selectedEndCharIndex
					End If
					If endOfLineCharIndex <> selectedEndCharIndex AndAlso (startOfLineCharIndex < secondUnselectedStartCharIndex OrElse secondUnselectedStartCharIndex = -1) Then
						Me.DrawSelectedText(g, selectedStartCharIndex, startOfLineCharIndex, endOfLineCharIndex)
					Else
						Me.DrawSelectedText(g, selectedStartCharIndex, startOfLineCharIndex, endOfLineCharIndex, True)
					End If

					' Draw selected text in remaining lines that have a selection.
					While endOfLineCharIndex <> selectedEndCharIndex AndAlso (startOfLineCharIndex < secondUnselectedStartCharIndex OrElse secondUnselectedStartCharIndex = -1)
						startOfLineCharIndex = Me.GetFirstCharIndexFromLine(lineIndex)
						lineIndex += 1
						endOfLineCharIndex = Me.GetFirstCharIndexFromLine(lineIndex) - 1
						'NOTE: If lineIndex is greater than the last line, then GetFirstCharIndexFromLine() returns -1.
						If endOfLineCharIndex < 0 OrElse endOfLineCharIndex > selectedEndCharIndex Then
							endOfLineCharIndex = selectedEndCharIndex
						End If
						Me.DrawSelectedText(g, startOfLineCharIndex, startOfLineCharIndex, endOfLineCharIndex)
					End While

					If secondUnselectedStartCharIndex >= 0 Then
						' Draw normal (unselected) text in first line that is after any selected text.
						lineIndex = Me.GetLineFromCharIndex(secondUnselectedStartCharIndex)
						startOfLineCharIndex = Me.GetFirstCharIndexFromLine(lineIndex)
						lineIndex += 1
						endOfLineCharIndex = Me.GetFirstCharIndexFromLine(lineIndex) - 1
						'NOTE: If lineIndex is greater than the last line, then GetFirstCharIndexFromLine() returns -1.
						If endOfLineCharIndex < 0 OrElse endOfLineCharIndex > secondUnselectedEndCharIndex Then
							endOfLineCharIndex = secondUnselectedEndCharIndex
						End If
						Me.DrawNormalText(g, secondUnselectedStartCharIndex, startOfLineCharIndex, endOfLineCharIndex)

						' Draw remaining normal (unselected) text lines that are after any selected text.
						While endOfLineCharIndex <> secondUnselectedEndCharIndex
							startOfLineCharIndex = Me.GetFirstCharIndexFromLine(lineIndex)
							lineIndex += 1
							endOfLineCharIndex = Me.GetFirstCharIndexFromLine(lineIndex) - 1
							'NOTE: If lineIndex is greater than the last line, then GetFirstCharIndexFromLine() returns -1.
							If endOfLineCharIndex < 0 OrElse endOfLineCharIndex > secondUnselectedEndCharIndex Then
								endOfLineCharIndex = secondUnselectedEndCharIndex
							End If
							Me.DrawNormalText(g, startOfLineCharIndex, startOfLineCharIndex, endOfLineCharIndex)
						End While
					End If
				End If
			End If
		End If

		' Draw cue banner text.
		If Me.theCueBannerText <> "" AndAlso Me.Text = "" AndAlso Me.theOriginalFont IsNot Nothing Then
			Dim drawFont As System.Drawing.Font = New System.Drawing.Font(Me.theOriginalFont.FontFamily, Me.theOriginalFont.Size, FontStyle.Italic, Me.theOriginalFont.Unit)
			Dim clientRectangle As Rectangle = Me.ClientRectangle
			' Add top and bottom padding.
			clientRectangle.Inflate(0, -1)
			TextRenderer.DrawText(g, Me.theCueBannerText, drawFont, clientRectangle, WidgetDisabledTextColor, WidgetDeepBackColor, TextFormatFlags.Left)
		End If
	End Sub

	'TEST 1 from OnPaint.
	' Draw text.
	'' All of the Get* functions return values based on what is displayed, not what is assigned (Lines property).
	'Dim startCharIndex As Integer = Me.GetCharIndexFromPosition(clipRectangle.Location)
	'Dim endCharIndex As Integer = Me.GetCharIndexFromPosition(New Point(clipRectangle.Right, clipRectangle.Bottom))
	''Dim textPositionRect As Rectangle = clipRectangle
	''For charIndex As Integer = startCharIndex To endCharIndex
	''	textPositionRect.Location = Me.GetPositionFromCharIndex(charIndex)
	''	TextRenderer.DrawText(g, Me.Text(charIndex), Me.theOriginalFont, textPositionRect, WidgetTextColor, WidgetDeepBackColor, formatFlags)
	''Next
	'Dim textPositionRect As Rectangle
	'If Me.TextAlign = HorizontalAlignment.Center Then
	'	formatFlags = formatFlags Or TextFormatFlags.HorizontalCenter
	'	textPositionRect = clientRectangle
	'	TextRenderer.DrawText(g, Me.Text.Substring(startCharIndex, endCharIndex - startCharIndex + 1), Me.theOriginalFont, textPositionRect, WidgetTextColor, WidgetDeepBackColor, formatFlags)
	'Else
	'	textPositionRect = clipRectangle
	'	textPositionRect.Location = Me.GetPositionFromCharIndex(startCharIndex)
	'	TextRenderer.DrawText(g, Me.Text.Substring(startCharIndex, endCharIndex - startCharIndex + 1), Me.theOriginalFont, textPositionRect, WidgetTextColor, WidgetDeepBackColor, formatFlags)
	'End If
	'
	'If Me.SelectionLength > 0 AndAlso startCharIndex <= Me.SelectionStart + Me.SelectionLength - 1 AndAlso endCharIndex >= Me.SelectionStart Then
	'	Dim selectionPositionRect As Rectangle = e.ClipRectangle
	'	'======
	'	'Dim selectionEndCharIndex As Integer = Me.SelectionStart + Me.SelectionLength - 1
	'	' This 'For' loop causes extra spacing between some selected characters.
	'	'For selectionCharIndex As Integer = Me.SelectionStart To selectionEndCharIndex
	'	'	selectionPositionRect.Location = Me.GetPositionFromCharIndex(selectionCharIndex)
	'	'	'TextRenderer.DrawText(g, Me.SelectedText(selectionCharIndex - Me.SelectionStart), Me.theOriginalFont, selectionPositionRect, WidgetTextColor, WidgetDeepSelectedBackColor, formatFlags)
	'	'	TextRenderer.DrawText(g, Me.Text(selectionCharIndex), Me.theOriginalFont, selectionPositionRect, WidgetTextColor, WidgetDeepSelectedBackColor, formatFlags)
	'	'Next
	'	'------
	'	'' This location setting is correct only for first line of selection.
	'	'selectionPositionRect.Location = Me.GetPositionFromCharIndex(Me.SelectionStart)
	'	'TextRenderer.DrawText(g, Me.Text.Substring(Me.SelectionStart, Me.SelectionLength), Me.theOriginalFont, selectionPositionRect, WidgetTextColor, WidgetDeepSelectedBackColor, formatFlags)
	'	'------
	'	'Dim selectionPositionRect As Rectangle
	'	'If Me.TextAlign = HorizontalAlignment.Center Then
	'	'	selectionPositionRect = clientRectangle
	'	'Else
	'	'	selectionPositionRect = clientRectangle
	'	'	'selectionPositionRect = clipRectangle
	'	'	'selectionPositionRect.Location = Me.GetPositionFromCharIndex(Me.SelectionStart)
	'	'End If
	'	'TextRenderer.DrawText(g, Me.Text.Substring(Me.SelectionStart, Me.SelectionLength), Me.theOriginalFont, selectionPositionRect, WidgetTextColor, WidgetDeepSelectedBackColor, formatFlags)
	'	'------
	'	Dim selectionStartLineIndex As Integer = Me.GetLineFromCharIndex(Me.SelectionStart)
	'	Dim selectionEndLineIndex As Integer = Me.GetLineFromCharIndex(Me.SelectionStart + Me.SelectionLength - 1)
	'	Dim startOfSecondLineCharIndex As Integer = Me.GetFirstCharIndexFromLine(selectionStartLineIndex + 1)
	'	If Me.TextAlign = HorizontalAlignment.Center Then
	'		selectionPositionRect = clientRectangle
	'		selectionPositionRect.Location = Me.GetPositionFromCharIndex(Me.SelectionStart)
	'		selectionPositionRect.X = clientRectangle.X
	'	Else
	'		selectionPositionRect.Location = Me.GetPositionFromCharIndex(Me.SelectionStart)
	'	End If
	'	If selectionStartLineIndex < selectionEndLineIndex Then
	'		'selectionPositionRect.Location = Me.GetPositionFromCharIndex(Me.SelectionStart)
	'		TextRenderer.DrawText(g, Me.Text.Substring(Me.SelectionStart, startOfSecondLineCharIndex - Me.SelectionStart), Me.theOriginalFont, selectionPositionRect, WidgetTextColor, WidgetDeepSelectedBackColor, formatFlags)
	'		selectionPositionRect = clientRectangle
	'		selectionPositionRect.Location = Me.GetPositionFromCharIndex(startOfSecondLineCharIndex)
	'		TextRenderer.DrawText(g, Me.Text.Substring(startOfSecondLineCharIndex, Me.SelectionLength - (startOfSecondLineCharIndex - Me.SelectionStart)), Me.theOriginalFont, selectionPositionRect, WidgetTextColor, WidgetDeepSelectedBackColor, formatFlags)
	'	Else
	'		'selectionPositionRect.Location = Me.GetPositionFromCharIndex(Me.SelectionStart)
	'		TextRenderer.DrawText(g, Me.Text.Substring(Me.SelectionStart, Me.SelectionLength), Me.theOriginalFont, selectionPositionRect, WidgetTextColor, Color.Blue, formatFlags)
	'	End If
	'End If

	'TEST 2 from OnPaint.
	' Draw text.
	'' All of the Get* functions return values based on what is displayed, not what is assigned (Lines property).
	'Dim startCharIndex As Integer = Me.GetCharIndexFromPosition(clipRectangle.Location)
	'Dim endCharIndex As Integer = Me.GetCharIndexFromPosition(New Point(clipRectangle.Right, clipRectangle.Bottom))
	'
	'Dim firstUnselectedStartCharIndex As Integer = startCharIndex
	'Dim firstUnselectedEndCharIndex As Integer = endCharIndex
	'Dim selectedStartCharIndex As Integer = -1
	'Dim selectedLineStartCharIndex As Integer = -1
	'Dim selectedEndCharIndex As Integer = -1
	'Dim secondUnselectedStartCharIndex As Integer = -1
	'Dim secondUnselectedLineStartCharIndex As Integer = -1
	'Dim secondUnselectedEndCharIndex As Integer = -1
	'
	'If Me.SelectionLength > 0 AndAlso startCharIndex <= Me.SelectionStart + Me.SelectionLength - 1 AndAlso endCharIndex >= Me.SelectionStart Then
	'	selectedStartCharIndex = Me.SelectionStart
	'	If selectedStartCharIndex <= startCharIndex Then
	'		firstUnselectedStartCharIndex = -1
	'		firstUnselectedEndCharIndex = -1
	'		selectedStartCharIndex = startCharIndex
	'	Else
	'		firstUnselectedEndCharIndex = selectedStartCharIndex - 1
	'	End If
	'
	'	Dim lineIndex As Integer = Me.GetLineFromCharIndex(selectedStartCharIndex)
	'	selectedLineStartCharIndex = Me.GetFirstCharIndexFromLine(lineIndex)
	'
	'	selectedEndCharIndex = Me.SelectionStart + Me.SelectionLength - 1
	'	If selectedEndCharIndex > endCharIndex Then
	'		selectedEndCharIndex = endCharIndex
	'	Else
	'		secondUnselectedStartCharIndex = selectedEndCharIndex + 1
	'		secondUnselectedEndCharIndex = endCharIndex

	'		lineIndex = Me.GetLineFromCharIndex(secondUnselectedStartCharIndex)
	'		secondUnselectedLineStartCharIndex = Me.GetFirstCharIndexFromLine(lineIndex)
	'	End If
	'End If
	'
	'Dim textPositionRect As Rectangle
	'Dim textLinePositionRect As Rectangle
	'If Me.TextAlign = HorizontalAlignment.Center Then
	'	'TODO: This handling of HorizontalAlignment.Center probably needs to merge with the 'Else' block below.
	'	formatFlags = formatFlags Or TextFormatFlags.HorizontalCenter
	'	textPositionRect = clientRectangle
	'	TextRenderer.DrawText(g, Me.Text.Substring(startCharIndex, endCharIndex - startCharIndex + 1), Me.theOriginalFont, textPositionRect, WidgetTextColor, WidgetDeepBackColor, formatFlags)
	'Else
	'	Dim graphicsClipBounds As Rectangle = Rectangle.Round(g.ClipBounds)
	'	Dim textPosition As Point
	'	Dim startOfLineCharIndex As Integer
	'	Dim lineIndex As Integer
	'	Dim endOfLineCharIndex As Integer
	'
	'	'' Draw initial text lines that do not include selection, which can be all lines when no selection in widget.
	'	'Dim textPosition As Point = Me.GetPositionFromCharIndex(firstNonSelectedStartCharIndex)
	'	''TEST: This line is probably not needed.
	'	''textLinePositionRect = graphicsClipBounds
	'	'textLinePositionRect.Location = textPosition
	'	''DEBUG: Extending the width here allows more of the first line of wrapped text to show, but do not know why last character seems to be clipped.
	'	'textLinePositionRect.Width = clientRectangle.Width
	'	'' Give plenty of height to prevent vertical clipping of text lines.
	'	''textLinePositionRect.Height += Screen.FromControl(Me).Bounds.Height
	'	'textLinePositionRect.Height = Screen.FromControl(Me).Bounds.Height
	'	'''DEBUG: 
	'	''Using backColorBrush As New SolidBrush(Color.Red)
	'	''	g.FillRectangle(backColorBrush, textLinePositionRect)
	'	''End Using
	'	'TextRenderer.DrawText(g, Me.Text.Substring(firstNonSelectedStartCharIndex, firstNonSelectedEndCharIndex - firstNonSelectedStartCharIndex + 1), Me.theOriginalFont, textLinePositionRect, WidgetTextColor, WidgetDeepBackColor, formatFlags)
	'	'------
	'	If firstUnselectedStartCharIndex >= 0 Then
	'		' Draw normal (unselected) text in first line that is before any selected text.
	'		startOfLineCharIndex = firstUnselectedStartCharIndex
	'		lineIndex = Me.GetLineFromCharIndex(startOfLineCharIndex)
	'		lineIndex += 1
	'		endOfLineCharIndex = Me.GetFirstCharIndexFromLine(lineIndex) - 1
	'		'NOTE: If lineIndex is greater than the last line, then GetFirstCharIndexFromLine() returns -1.
	'		If endOfLineCharIndex < 0 OrElse endOfLineCharIndex > firstUnselectedEndCharIndex Then
	'			endOfLineCharIndex = firstUnselectedEndCharIndex
	'		End If
	'		Me.DrawNormalText(g, startOfLineCharIndex, endOfLineCharIndex)
	'
	'		' Draw remaining normal (unselected) text lines that are before any selected text.
	'		While endOfLineCharIndex <> firstUnselectedEndCharIndex AndAlso (startOfLineCharIndex < selectedStartCharIndex OrElse selectedStartCharIndex = -1)
	'			startOfLineCharIndex = Me.GetFirstCharIndexFromLine(lineIndex)
	'			lineIndex += 1
	'			endOfLineCharIndex = Me.GetFirstCharIndexFromLine(lineIndex) - 1
	'			'NOTE: If lineIndex is greater than the last line, then GetFirstCharIndexFromLine() returns -1.
	'			If endOfLineCharIndex < 0 OrElse endOfLineCharIndex > firstUnselectedEndCharIndex Then
	'				endOfLineCharIndex = firstUnselectedEndCharIndex
	'			End If
	'			Me.DrawNormalText(g, startOfLineCharIndex, endOfLineCharIndex)
	'		End While
	'	End If
	'
	'	If selectedStartCharIndex >= 0 Then
	'		'' Draw selected text in first line that has a selection.
	'		'textPositionRect = clipRectangle
	'		'textPositionRect.Location = Me.GetPositionFromCharIndex(selectionStartCharIndex)
	'		'g.IntersectClip(textPositionRect)
	'		'textPosition = Me.GetPositionFromCharIndex(selectionLineStartCharIndex)
	'		'textLinePositionRect = graphicsClipBounds
	'		'textLinePositionRect.Location = textPosition
	'		'textLinePositionRect.Width = clientRectangle.Width
	'		'TextRenderer.DrawText(g, Me.Text.Substring(selectionLineStartCharIndex, selectionEndCharIndex - selectionLineStartCharIndex + 1), Me.theOriginalFont, textLinePositionRect, WidgetTextColor, WidgetDeepSelectedBackColor, formatFlags)
	'		'g.ResetClip()
	'		'------
	'		' Draw selected text in first line that has a selection.
	'		'textPositionRect = clipRectangle
	'		'textPositionRect.Location = Me.GetPositionFromCharIndex(selectionStartCharIndex)
	'		'g.IntersectClip(textPositionRect)
	'		startOfLineCharIndex = selectedStartCharIndex
	'		lineIndex = Me.GetLineFromCharIndex(startOfLineCharIndex)
	'		lineIndex += 1
	'		endOfLineCharIndex = Me.GetFirstCharIndexFromLine(lineIndex) - 1
	'		'NOTE: If lineIndex is greater than the last line, then GetFirstCharIndexFromLine() returns -1.
	'		If endOfLineCharIndex < 0 OrElse endOfLineCharIndex > selectedEndCharIndex Then
	'			endOfLineCharIndex = selectedEndCharIndex
	'		End If
	'		Me.DrawSelectedText(g, startOfLineCharIndex, endOfLineCharIndex)
	'		'g.ResetClip()
	'
	'		'' Draw selected text in remaining lines that have a selection.
	'		'Dim selectionStartLineIndex As Integer = Me.GetLineFromCharIndex(selectionStartCharIndex)
	'		'Dim selectionEndLineIndex As Integer = Me.GetLineFromCharIndex(selectionEndCharIndex)
	'		'Dim startOfSecondLineCharIndex As Integer = Me.GetFirstCharIndexFromLine(selectionStartLineIndex + 1)
	'		'If selectionStartLineIndex <> selectionEndLineIndex Then
	'		'	textPosition = Me.GetPositionFromCharIndex(startOfSecondLineCharIndex)
	'		'	textLinePositionRect = graphicsClipBounds
	'		'	textLinePositionRect.Location = textPosition
	'		'	textLinePositionRect.Width = clientRectangle.Width
	'		'	textLinePositionRect.Height += Screen.FromControl(Me).Bounds.Height
	'		'	TextRenderer.DrawText(g, Me.Text.Substring(startOfSecondLineCharIndex, selectionEndCharIndex - startOfSecondLineCharIndex + 1), Me.theOriginalFont, textLinePositionRect, WidgetTextColor, WidgetDeepSelectedBackColor, formatFlags)
	'
	'		'	'DEBUG: 
	'		'	'Dim temp As Integer = Me.SelectionLength - (startOfSecondLineCharIndex - Me.SelectionStart)
	'		'	'TextRenderer.DrawText(g, startOfSecondLineCharIndex.ToString() + ", " + temp.ToString(), Me.theOriginalFont, clientRectangle.Location, WidgetTextColor, Color.Blue, formatFlags)
	'		'	'TextRenderer.DrawText(g, graphicsClipBounds.ToString(), Me.theOriginalFont, clientRectangle.Location, WidgetTextColor, Color.Blue, formatFlags)
	'		'End If
	'		'------
	'		' Draw selected text in remaining lines that have a selection.
	'		While endOfLineCharIndex <> selectedEndCharIndex AndAlso (startOfLineCharIndex >= selectedStartCharIndex AndAlso startOfLineCharIndex <= selectedEndCharIndex)
	'			startOfLineCharIndex = Me.GetFirstCharIndexFromLine(lineIndex)
	'			lineIndex += 1
	'			endOfLineCharIndex = Me.GetFirstCharIndexFromLine(lineIndex) - 1
	'			'NOTE: If lineIndex is greater than the last line, then GetFirstCharIndexFromLine() returns -1.
	'			If endOfLineCharIndex < 0 OrElse endOfLineCharIndex > selectedEndCharIndex Then
	'				endOfLineCharIndex = selectedEndCharIndex
	'			End If
	'			Me.DrawSelectedText(g, startOfLineCharIndex, endOfLineCharIndex)
	'		End While
	'
	'		If secondUnselectedStartCharIndex >= 0 Then
	'			' Draw the non-selected ending part of text line that has a selection.
	'			textPositionRect = clipRectangle
	'			textPositionRect.Location = Me.GetPositionFromCharIndex(secondUnselectedStartCharIndex)
	'			g.IntersectClip(textPositionRect)
	'			textPosition = Me.GetPositionFromCharIndex(secondUnselectedLineStartCharIndex)
	'			textLinePositionRect = graphicsClipBounds
	'			textLinePositionRect.Location = textPosition
	'			textLinePositionRect.Width = clientRectangle.Width
	'			''DEBUG: 
	'			'Using backColorBrush As New SolidBrush(Color.Red)
	'			'	g.FillRectangle(backColorBrush, textLinePositionRect)
	'			'End Using
	'			TextRenderer.DrawText(g, Me.Text.Substring(secondUnselectedLineStartCharIndex, secondUnselectedEndCharIndex - secondUnselectedLineStartCharIndex + 1), Me.theOriginalFont, textLinePositionRect, WidgetTextColor, WidgetDeepBackColor, formatFlags)
	'			g.ResetClip()
	'			'------
	'			' Draw the non-selected ending part of text line that has a selection.
	'
	'			' Draw remaining non-selected text lines.
	'			Dim nonSelectedStartLineIndex As Integer = Me.GetLineFromCharIndex(secondUnselectedStartCharIndex)
	'			Dim nonSelectedEndLineIndex As Integer = Me.GetLineFromCharIndex(secondUnselectedEndCharIndex)
	'			Dim startOfSecondNonSelectedLineCharIndex As Integer = Me.GetFirstCharIndexFromLine(nonSelectedStartLineIndex + 1)
	'			If nonSelectedStartLineIndex <> nonSelectedEndLineIndex Then
	'				textPosition = Me.GetPositionFromCharIndex(startOfSecondNonSelectedLineCharIndex)
	'				textLinePositionRect = graphicsClipBounds
	'				textLinePositionRect.Location = textPosition
	'				textLinePositionRect.Width = clientRectangle.Width
	'				textLinePositionRect.Height += Screen.FromControl(Me).Bounds.Height
	'				TextRenderer.DrawText(g, Me.Text.Substring(startOfSecondNonSelectedLineCharIndex, secondUnselectedEndCharIndex - startOfSecondNonSelectedLineCharIndex + 1), Me.theOriginalFont, textLinePositionRect, WidgetTextColor, WidgetDeepBackColor, formatFlags)
	'			End If
	'			'------
	'			' Draw remaining non-selected text lines.
	'		End If
	'	End If
	'End If

	Protected Sub DrawNormalText(ByVal g As Graphics, ByVal startCharIndex As Integer, ByVal startOfLineCharIndex As Integer, ByVal endOfLineCharIndex As Integer, Optional ByVal firstOfManyLines As Boolean = False)
		Dim textLinePositionRect As Rectangle = Me.ClientRectangle
		textLinePositionRect.Location = Me.GetPositionFromCharIndex(startOfLineCharIndex)
		'textLinePositionRect.X -= 1
		Dim testStartOfLineCharIndex As Integer = Me.GetCharIndexFromPosition(textLinePositionRect.Location)
		If testStartOfLineCharIndex <> startOfLineCharIndex Then
			Dim debug As Integer = 4242
		End If

		Dim textPositionRect As Rectangle = Me.ClientRectangle
		textPositionRect.Location = Me.GetPositionFromCharIndex(startCharIndex)
		'textPositionRect.X -= 1
		Dim testStartCharIndex As Integer = Me.GetCharIndexFromPosition(textPositionRect.Location)
		If testStartCharIndex <> startCharIndex Then
			Dim debug As Integer = 4242
		End If

		g.IntersectClip(textPositionRect)

		'Dim formatFlags As TextFormatFlags = Me.theFormatFlags
		'If Me.TextAlign = HorizontalAlignment.Center Then
		'	formatFlags = formatFlags Or TextFormatFlags.HorizontalCenter
		'End If
		Dim normalTextFormatFlags As TextFormatFlags = Me.theTextFormatFlags
		If firstOfManyLines Then
			normalTextFormatFlags = normalTextFormatFlags Or TextFormatFlags.GlyphOverhangPadding
		End If

		TextRenderer.DrawText(g, Me.Text.Substring(startOfLineCharIndex, endOfLineCharIndex - startOfLineCharIndex + 1), Me.theOriginalFont, textLinePositionRect, Me.ForeColor, MyBase.BackColor, normalTextFormatFlags)
		g.ResetClip()
	End Sub

	Protected Sub DrawSelectedText(ByVal g As Graphics, ByVal startCharIndex As Integer, ByVal startOfLineCharIndex As Integer, ByVal endOfLineCharIndex As Integer, Optional ByVal firstOfManyLines As Boolean = False)
		Dim textLinePositionRect As Rectangle = Me.ClientRectangle
		textLinePositionRect.Location = Me.GetPositionFromCharIndex(startOfLineCharIndex)
		Dim testStartOfLineCharIndex As Integer = Me.GetCharIndexFromPosition(textLinePositionRect.Location)
		If testStartOfLineCharIndex <> startOfLineCharIndex Then
			Dim debug As Integer = 4242
		End If

		Dim textPositionRect As Rectangle = Me.ClientRectangle
		textPositionRect.Location = Me.GetPositionFromCharIndex(startCharIndex)
		'textPositionRect.X -= 1
		Dim testStartCharIndex As Integer = Me.GetCharIndexFromPosition(textPositionRect.Location)
		If testStartCharIndex <> startCharIndex Then
			Dim debug As Integer = 4242
		End If

		g.IntersectClip(textPositionRect)

		'Dim selectedTextForeColor As Color = WidgetConstants.WidgetTextColor
		'Dim selectedTextBackColor As Color = WidgetConstants.WidgetDeepSelectedBackColor
		''If [ReadOnly] Then
		''	backgroundColor = WidgetConstants.WidgetDeepDisabledBackColor
		''End If
		''If Not Me.Enabled Then
		''	textColor = WidgetConstants.WidgetDisabledTextColor
		''End If
		'------
		Dim selectedTextForeColor As Color = Me.ForeColor
		Dim selectedTextBackColor As Color = SystemColors.Highlight
		Dim theme As RichTextBoxTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.RichTextBoxTheme
		End If
		If theme IsNot Nothing Then
			selectedTextForeColor = theme.SelectedForeColor
			selectedTextBackColor = theme.SelectedBackColor
		End If

		'Dim formatFlags As TextFormatFlags = Me.theFormatFlags
		'If Me.TextAlign = HorizontalAlignment.Center Then
		'	formatFlags = formatFlags Or TextFormatFlags.HorizontalCenter
		'End If
		Dim selectedTextFormatFlags As TextFormatFlags = Me.theTextFormatFlags
		If firstOfManyLines Then
			selectedTextFormatFlags = selectedTextFormatFlags Or TextFormatFlags.GlyphOverhangPadding
		End If

		TextRenderer.DrawText(g, Me.Text.Substring(startOfLineCharIndex, endOfLineCharIndex - startOfLineCharIndex + 1), Me.theOriginalFont, textLinePositionRect, selectedTextForeColor, selectedTextBackColor, selectedTextFormatFlags)
		g.ResetClip()
	End Sub

	' Need the following line for OnPaint() to be called by Windows:
	'	Me.SetStyle(ControlStyles.UserPaint, True)
	Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
		'NOTE: Completely override painting by OS.
		'MyBase.OnPaintBackground(e)

		'' Draw background border.
		'Using borderColorPen As New Pen(WidgetDisabledTextColor)
		'	'Using borderColorPen As New Pen(Color.Green)
		'	Dim aRect As Rectangle = Me.ClientRectangle
		'	'NOTE: DrawRectangle width and height are interpreted as the right and bottom pixels to draw.
		'	aRect.Width -= 1
		'	aRect.Height -= 1
		'	e.Graphics.DrawRectangle(borderColorPen, aRect)
		'End Using

		' Draw background.
		Using backColorBrush As New SolidBrush(MyBase.BackColor)
			Dim aRect As Rectangle = Me.ClientRectangle
			e.Graphics.FillRectangle(backColorBrush, aRect)
		End Using
	End Sub

	Protected Overrides Sub OnSizeChanged(e As EventArgs)
		MyBase.OnSizeChanged(e)

		' This check prevents incorrect painting due to premature creation of Handle.
		If Me.theControlHasShown Then
			If Me.theControlIsBehavingAsMultiLine AndAlso Me.theLineCount <> Me.GetLineFromCharIndex(Me.TextLength - 1) - 1 Then
				'NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
				Win32Api.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
			End If
			Me.Invalidate()
			Me.UpdateScrollbars()
		End If
	End Sub

	Protected Overrides Sub OnTextChanged(e As EventArgs)
		MyBase.OnTextChanged(e)

		If Me.theControlIsBehavingAsMultiLine AndAlso Me.theLineCount <> Me.GetLineFromCharIndex(Me.TextLength - 1) - 1 Then
			'NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
			Win32Api.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
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
				Win32Api.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
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

		'If m.Msg = Win32Api.WindowsMessages.WM_PAINT Then
		'	Using graphic As Graphics = Me.CreateGraphics()
		'		OnPaint(New PaintEventArgs(graphic, Me.ClientRectangle))
		'	End Using
		'End If
	End Sub

	Private Sub OnNonClientCalcSize(ByRef m As Message)
		Me.UpdateNonClientPadding()
		If CInt(m.WParam) = 0 Then
			Dim rect As Win32Api.RECT = CType(Marshal.PtrToStructure(m.LParam, GetType(Win32Api.RECT)), Win32Api.RECT)
			Me.ResizeClientRect(Me.NonClientPadding, rect)
			Marshal.StructureToPtr(rect, m.LParam, False)
			m.Result = IntPtr.Zero
		ElseIf CInt(m.WParam) = 1 Then
			Dim nccsp As Win32Api.NCCALCSIZE_PARAMS = CType(Marshal.PtrToStructure(m.LParam, GetType(Win32Api.NCCALCSIZE_PARAMS)), Win32Api.NCCALCSIZE_PARAMS)
			Me.ResizeClientRect(Me.NonClientPadding, nccsp.rect0)
			Marshal.StructureToPtr(nccsp, m.LParam, False)
			m.Result = IntPtr.Zero
		End If
	End Sub

	Private Sub OnNonClientPaint(ByRef m As Message)
		Dim hDC As IntPtr = Win32Api.GetWindowDC(Me.Handle)
		Try
			Using g As Graphics = Graphics.FromHdc(hDC)
				Dim aRectF As RectangleF = g.VisibleClipBounds
				' Important to clip away the client rectangle to prevent text painting issues when scrolling widget into view.
				Dim textPositionRect As Rectangle = Me.ClientRectangle
				textPositionRect.X = Me.NonClientPadding.Left
				textPositionRect.Y = Me.NonClientPadding.Top
				g.ExcludeClip(textPositionRect)

				' Draw background.
				Using backColorBrush As New SolidBrush(MyBase.BackColor)
					g.FillRectangle(backColorBrush, aRectF)
				End Using

				' Draw border.
				Dim theme As RichTextBoxTheme = Nothing
				' This check prevents problems with viewing and saving Forms in VS Designer.
				If TheApp IsNot Nothing Then
					theme = TheApp.Settings.SelectedAppTheme.RichTextBoxTheme
				End If
				If theme IsNot Nothing Then
					Dim borderColor As Color
					Dim borderWidth As Integer
					If Me.Enabled Then
						'If Me.theButtonCanBeFocused AndAlso Me.theMouseIsOverButton Then
						'	borderColor = theme.FocusBorderColor
						'	borderWidth = theme.FocusBorderWidth
						'Else
						borderColor = theme.EnabledBorderColor
						borderWidth = theme.EnabledBorderWidth
						'End If
					Else
						borderColor = theme.DisabledBorderColor
						borderWidth = theme.DisabledBorderWidth
					End If
					Me.SelectionColor = theme.SelectedForeColor
					Me.SelectionBackColor = theme.SelectedBackColor

					Using borderColorPen As New Pen(borderColor, borderWidth)
						borderColorPen.Alignment = Drawing2D.PenAlignment.Inset
						If borderWidth = 1 Then
							'NOTE: DrawRectangle width and height are interpreted as the right and bottom pixels to draw.
							aRectF.Width -= 1
							aRectF.Height -= 1
						End If
						g.DrawRectangle(borderColorPen, aRectF.Left, aRectF.Top, aRectF.Width, aRectF.Height)
					End Using
					'Else
					'	If MyBase.BorderStyle = BorderStyle.FixedSingle Then
					'		Using borderColorPen As New Pen(Me.theBorderColor)
					'			'NOTE: DrawRectangle width and height are interpreted as the right and bottom pixels to draw.
					'			aRectF.Width -= 1
					'			aRectF.Height -= 1
					'			g.DrawRectangle(borderColorPen, aRectF.Left, aRectF.Top, aRectF.Width, aRectF.Height)
					'		End Using
					'	End If
				End If

				g.ResetClip()
			End Using
		Finally
			Win32Api.ReleaseDC(Me.Handle, hDC)
		End Try

		m.Result = IntPtr.Zero
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub HorizontalScrollbar_ValueChanged(ByVal sender As Object, ByVal e As ScrollValueEventArgs) Handles HorizontalScrollbar.ValueChanged
		Me.UpdateScrolling(e.Value, Me.VerticalScrollbar.Value)
	End Sub

	Private Sub VerticalScrollBar_ValueChanged(ByVal sender As Object, ByVal e As ScrollValueEventArgs) Handles VerticalScrollbar.ValueChanged
		Me.UpdateScrolling(Me.HorizontalScrollbar.Value, e.Value)
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

	Private Sub AppSettings_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		If e.PropertyName = "AppThemeName" Then
			Me.UpdateTheme()
			Me.Refresh()
		End If
	End Sub

#End Region

#Region "Private Methods"

	Private Sub UpdateTheme()
		Dim theme As RichTextBoxTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.RichTextBoxTheme
		End If
		If theme IsNot Nothing Then
			If MyBase.[ReadOnly] Then
				Me.ForeColor = theme.DisabledForeColor
				Me.BackColor = theme.DisabledBackColor
			Else
				Me.ForeColor = theme.EnabledForeColor
				Me.BackColor = theme.EnabledBackColor
			End If

			Me.SelectionColor = theme.SelectedForeColor
			Me.SelectionBackColor = theme.SelectedBackColor

			'NOTE: Disable to use custom.
			Me.BorderStyle = BorderStyle.None
			MyBase.ScrollBars = RichTextBoxScrollBars.None

			'Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
			'Me.SetStyle(ControlStyles.DoubleBuffer, True)
			'Me.SetStyle(ControlStyles.UserPaint, True)
		Else
			Me.ForeColor = SystemColors.WindowText
			If MyBase.[ReadOnly] Then
				Me.BackColor = SystemColors.Control
			Else
				Me.BackColor = SystemColors.Window
			End If

			' Draw background here because the OnPaintBackground will not be called. 
			Dim hDC As IntPtr = Win32Api.GetWindowDC(Me.Handle)
			Try
				Using g As Graphics = Graphics.FromHdc(hDC)
					Using backColorBrush As New SolidBrush(MyBase.BackColor)
						Dim aRect As Rectangle = Me.ClientRectangle
						g.FillRectangle(backColorBrush, aRect)
					End Using
				End Using
			Finally
				Win32Api.ReleaseDC(Me.Handle, hDC)
			End Try

			'If Me.theControlIsBehavingAsMultiLine Then
			'	'MyBase.BorderStyle = BorderStyle.None
			'	MyBase.ScrollBars = RichTextBoxScrollBars.Both
			'End If

			Me.SetStyle(ControlStyles.AllPaintingInWmPaint, False)
			Me.SetStyle(ControlStyles.DoubleBuffer, False)
			Me.SetStyle(ControlStyles.UserPaint, False)
		End If

		Me.Font = Me.theOriginalFont

		'NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
		Win32Api.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
	End Sub

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

	Private Sub UpdateNonClientPadding()
		Dim left As Integer = 0
		Dim top As Integer = 0
		Dim right As Integer = 0
		Dim bottom As Integer = 0
		Dim textSize As Size = TextRenderer.MeasureText("Wy", Me.theOriginalFont)

		'If Not Me.theControlIsBehavingAsMultiLine Then
		'	top = CInt(Me.Height * 0.5 - textSize.Height * 0.5)
		'Else
		If Me.theControlIsBehavingAsMultiLine Then
			left = 2
			top = 2
			right = 2
			bottom = 2

			If Not Me.WordWrap Then
				Dim contentWidth As Integer = Me.GetContentWidthWithNoWordWrap()
				If contentWidth > Me.ClientRectangle.Width Then
					bottom += ScrollBarEx.Consts.ScrollBarSize
				End If
			End If

			Dim lineCount As Integer = Me.GetLineFromCharIndex(Me.TextLength - 1) + 1
			Dim contentHeight As Integer = lineCount * textSize.Height
			If contentHeight > Me.ClientRectangle.Height Then
				right += ScrollBarEx.Consts.ScrollBarSize
			End If
		End If

		Dim theme As RichTextBoxTheme = Nothing
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.RichTextBoxTheme
		End If
		If theme IsNot Nothing Then
			Dim borderWidth As Integer
			If Me.Enabled Then
				If Me.Focused Then
					borderWidth = theme.FocusBorderWidth
				Else
					borderWidth = theme.EnabledBorderWidth
				End If
			Else
				borderWidth = theme.DisabledBorderWidth
			End If
			left += borderWidth
			top += borderWidth
			right += borderWidth
			bottom += borderWidth
		End If

		If Not Me.theControlIsBehavingAsMultiLine Then
			top = CInt(Me.Height * 0.5 - textSize.Height * 0.5)
		End If

		Me.NonClientPadding = New Padding(left, top, right, bottom)
	End Sub

	Private Sub ResizeClientRect(ByVal padding As Padding, ByRef rect As Win32Api.RECT)
		rect.Left += padding.Left
		rect.Top += padding.Top
		rect.Right -= padding.Right
		rect.Bottom -= padding.Bottom
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
			Me.HorizontalScrollbar.Size = New System.Drawing.Size(Me.Width - 2 - ScrollBarEx.Consts.ScrollBarSize, ScrollBarEx.Consts.ScrollBarSize)
			Me.VerticalScrollbar.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize, Me.Height - 2 - ScrollBarEx.Consts.ScrollBarSize)
		End If
	End Sub

	Private Sub UpdateHorizontalScrollbar()
		'NOTE: Parent can be Nothing on exiting. Prevent the exception with this check.
		If Not Me.theScrollingIsActive AndAlso Not Me.WordWrap AndAlso Me.theControlIsBehavingAsMultiLine AndAlso Me.Parent IsNot Nothing Then
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

				'Me.HorizontalScrollbar.Show()

				'NOTE: Assign to Parent so it can draw over non-client area of RichTextBoxEx.
				Me.HorizontalScrollbar.Parent = Me.Parent
				Me.HorizontalScrollbar.BringToFront()
				'NOTE: Location must be relative to Parent.
				'Dim aPoint As New Point(Me.ClientRectangle.Left - Me.NonClientPadding.Left, Me.ClientRectangle.Height + Me.NonClientPadding.Top)
				'Dim aPoint As New Point(Me.ClientRectangle.Left - 1, Me.ClientRectangle.Height + 1)
				Dim aPoint As New Point(Me.ClientRectangle.Left, Me.ClientRectangle.Height)
				aPoint = Me.PointToScreen(aPoint)
				aPoint = Me.HorizontalScrollbar.Parent.PointToClient(aPoint)
				Me.HorizontalScrollbar.Location = aPoint
				'Me.HorizontalScrollbar.Size = New System.Drawing.Size(Me.Width - 2, ScrollBarEx.Consts.ScrollBarSize)
				Me.HorizontalScrollbar.Size = New System.Drawing.Size(Me.ClientRectangle.Width, ScrollBarEx.Consts.ScrollBarSize)

				Me.HorizontalScrollbar.Show()

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

				'NOTE: Assign to Parent so it can draw over non-client area.
				Me.VerticalScrollbar.Parent = Me.Parent
				Me.VerticalScrollbar.BringToFront()
				'NOTE: Location must be relative to Parent.
				'Dim aPoint As New Point(Me.ClientRectangle.Width + Me.NonClientPadding.Left, Me.ClientRectangle.Top - Me.NonClientPadding.Top)
				'Dim aPoint As New Point(Me.ClientRectangle.Width + 1, Me.ClientRectangle.Top - 1)
				Dim aPoint As New Point(Me.ClientRectangle.Width, Me.ClientRectangle.Top)
				aPoint = Me.PointToScreen(aPoint)
				aPoint = Me.VerticalScrollbar.Parent.PointToClient(aPoint)
				Me.VerticalScrollbar.Location = aPoint
				'Me.VerticalScrollbar.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize, Me.Height - 2)
				Me.VerticalScrollbar.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize, Me.ClientRectangle.Height)
				Me.VerticalScrollbar.Show()

				Me.theScrollingIsActive = False
			Else
				Me.VerticalScrollbar.Hide()
			End If
		End If
	End Sub

#End Region

#Region "Data"

	Private NonClientPadding As Padding

	Private theControlIsBehavingAsMultiLine As Boolean
	Private theSelectionIsEnabled As Boolean
	Private theScrollBars As RichTextBoxScrollBars

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
	Private theTextFormatFlags As TextFormatFlags
	Private theCueBannerText As String
	Private theOriginalFont As Font
	Private theTextAlignment As HorizontalAlignment

	Private WithEvents HorizontalScrollbar As ScrollBarEx
	Private WithEvents VerticalScrollbar As ScrollBarEx
	Private theLineCount As Integer
	Private theScrollingIsActive As Boolean

	'Private theTestColorIsBlue As Boolean

#End Region

End Class
