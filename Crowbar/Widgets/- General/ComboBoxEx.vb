Imports System.Windows.Forms.VisualStyles

Public Class ComboBoxEx
	Inherits ComboBox

	Public Sub New()
		MyBase.New()

		Me.DrawMode = DrawMode.OwnerDrawFixed
		Me.theComboBoxIsHot = False
	End Sub

	' Force the OnPaint() to be called so the focus rectangle can be drawn.
	Private Sub ComboBoxEx_Enter(sender As Object, e As EventArgs) Handles Me.Enter
		If Me.theOriginalFontToUseInManualPainting IsNot Nothing Then
			Me.Invalidate()
		End If
	End Sub

	' Draw the text in the dropdown portion of the ComboBox.
	Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
		MyBase.OnDrawItem(e)

		Dim currentValueText As String = ""
		If e.Index >= 0 AndAlso (e.State And DrawItemState.ComboBoxEdit) = 0 Then
			currentValueText = GetItemText(Me.Items(e.Index))

			e.DrawBackground()
			TextRenderer.DrawText(e.Graphics, currentValueText, Me.theOriginalFontToUseInManualPainting, e.Bounds, e.ForeColor, e.BackColor, Me.theTextFormatFlags)
			Me.DrawGameSetupNameAddedText(e.Graphics, e.Index, currentValueText, e.Bounds, e.ForeColor, e.BackColor)
			e.DrawFocusRectangle()
		End If
	End Sub

	Protected Overrides Sub OnMouseEnter(e As EventArgs)
		MyBase.OnMouseEnter(e)
		Me.theComboBoxIsHot = True
	End Sub

	Protected Overrides Sub OnMouseLeave(e As EventArgs)
		MyBase.OnMouseLeave(e)
		Me.theComboBoxIsHot = False
	End Sub

	' Draw the TextBox and dropdown-arrow portion of the ComboBox.
	Protected Overrides Sub OnPaint(e As PaintEventArgs)
		MyBase.OnPaint(e)

		Dim selectedValueText As String = ""
		If Me.SelectedIndex >= 0 Then
			selectedValueText = Me.SelectedValue.ToString()
		End If

		' Use Control Location and Size instead of the ClipRectangle to prevent paint problem when this ComboBox is focused and app is activated via switching from another app.
		Dim textAndArrowRectangle As New Rectangle(0, 0, Me.Width, Me.Height)
		textAndArrowRectangle.Inflate(1, 1)

		Dim buttonState As PushButtonState = PushButtonState.Normal
		Dim buttonElement As VisualStyleElement = VisualStyleElement.ComboBox.DropDownButton.Normal
		If Me.theComboBoxIsHot Then
			buttonState = PushButtonState.Hot
			buttonElement = VisualStyleElement.ComboBox.DropDownButton.Hot
		End If

		ButtonRenderer.DrawButton(e.Graphics, textAndArrowRectangle, selectedValueText, Me.theOriginalFontToUseInManualPainting, Me.theTextFormatFlags, False, buttonState)
		Me.DrawGameSetupNameAddedText(e.Graphics, Me.SelectedIndex, selectedValueText, textAndArrowRectangle, Me.ForeColor, Color.Transparent)

		'ComboBoxRenderer.DrawDropDownButton(e.Graphics, New Rectangle(e.ClipRectangle.Width - 19, e.ClipRectangle.Y, 19, e.ClipRectangle.Height), ComboBoxState.Normal)
		'======
		' Draw DropDownButton without its borders.
		Dim r As New VisualStyleRenderer(buttonElement)
		Dim buttonBounds As New Rectangle(Me.Width - Me.Height, 0, Me.Height, Me.Height)
		Dim buttonContentBounds As Rectangle = r.GetBackgroundContentRectangle(e.Graphics, buttonBounds)
		'NOTE: This Inflate() is here because GetBackgroundContentRectangle() for ComboBox.DropDownButton seems to include the borders.
		buttonContentBounds.Inflate(-1, -1)
		r.DrawBackground(e.Graphics, buttonBounds, buttonContentBounds)

		If Me.Focused Then
			Dim focusRectangle As New Rectangle(0, 0, Me.Width - Me.Height, Me.Height)
			focusRectangle.Inflate(-4, -2)
			ControlPaint.DrawFocusRectangle(e.Graphics, focusRectangle, Me.ForeColor, Me.BackColor)
		End If
	End Sub

	' Prevent insufficient painting when enlarging.
	Protected Overrides Sub OnResize(e As EventArgs)
		MyBase.OnResize(e)
		If Me.theOriginalFontToUseInManualPainting IsNot Nothing Then
			Me.Invalidate()
		End If
	End Sub

	Protected Overrides Sub OnVisibleChanged(e As EventArgs)
		MyBase.OnVisibleChanged(e)

		If Me.theOriginalFontToUseInManualPainting Is Nothing AndAlso Me.DropDownStyle = ComboBoxStyle.DropDownList Then
			If Application.RenderWithVisualStyles AndAlso VisualStyleRenderer.IsElementDefined(VisualStyleElement.ComboBox.DropDownButton.Hot) AndAlso VisualStyleRenderer.IsElementDefined(VisualStyleElement.ComboBox.DropDownButton.Normal) Then
				'NOTE: Font gets changed at some point after changing style, messing up when cue banner is turned off, 
				'      so save the Font after widget is visible for first time, but before changing style within the widget.
				Me.theOriginalFontToUseInManualPainting = New System.Drawing.Font(Me.Font.FontFamily, Me.Font.Size, Me.Font.Style, Me.Font.Unit)
				Me.theOriginalFontInItalicToUseInManualPainting = New Font(Me.theOriginalFontToUseInManualPainting, FontStyle.Italic)

				SetStyle(ControlStyles.AllPaintingInWmPaint, True)
				SetStyle(ControlStyles.DoubleBuffer, True)
				SetStyle(ControlStyles.UserPaint, True)
			End If
		End If
	End Sub

	Private Sub DrawGameSetupNameAddedText(ByVal g As Graphics, ByVal currentIndex As Integer, ByVal currentValueText As String, ByVal bounds As Rectangle, ByVal foreColor As Color, ByVal backColor As Color)
		If currentIndex >= 0 AndAlso currentValueText <> "" Then
			If TypeOf Me.Items(currentIndex) Is GameSetup Then
				Dim currentGameSetup As GameSetup = CType(Me.Items(currentIndex), GameSetup)
				If currentGameSetup.WasScanned Then
					Dim currentValueTextWidth As Double
					currentValueTextWidth = g.MeasureString(currentValueText, Me.theOriginalFontToUseInManualPainting).Width
					Dim italicTextBounds As New Rectangle(bounds.X + CInt(currentValueTextWidth), bounds.Y, bounds.Width - CInt(currentValueTextWidth), bounds.Height)
					TextRenderer.DrawText(g, " [scanned]", Me.theOriginalFontInItalicToUseInManualPainting, italicTextBounds, foreColor, backColor, Me.theTextFormatFlags)
				End If
			End If
		End If
	End Sub

	Private theOriginalFontToUseInManualPainting As Font
	Private theOriginalFontInItalicToUseInManualPainting As Font
	Private theComboBoxIsHot As Boolean
	Private theTextFormatFlags As TextFormatFlags = TextFormatFlags.NoPrefix Or TextFormatFlags.VerticalCenter

End Class
