Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices

Public Class CheckBoxEx
	Inherits CheckBox

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.theMouseIsOverButton = False
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

	Public Property IsReadOnly() As Boolean
		Get
			Return Me.theControlIsReadOnly
		End Get
		Set(ByVal value As Boolean)
			If Me.theControlIsReadOnly <> value Then
				Me.theControlIsReadOnly = value

				Dim theme As CheckBoxTheme = Nothing
				' This check prevents problems with viewing and saving Forms in VS Designer.
				If TheApp IsNot Nothing Then
					theme = TheApp.Settings.SelectedAppTheme.CheckBoxTheme
				End If
				If theme IsNot Nothing Then
					If Me.theControlIsReadOnly Then
						Me.ForeColor = theme.DisabledForeColor
						Me.BackColor = theme.DisabledBackColor
					Else
						Me.ForeColor = theme.EnabledForeColor
						Me.BackColor = theme.EnabledBackColor
					End If
				End If
			End If
		End Set
	End Property

#End Region

#Region "Methods"

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
	End Sub

	Protected Overrides Sub OnHandleDestroyed(e As EventArgs)
		Me.Free()
		MyBase.OnHandleDestroyed(e)
	End Sub

	Protected Overrides Sub OnMouseEnter(e As EventArgs)
		MyBase.OnMouseEnter(e)
		Me.theMouseIsOverButton = True
		'NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
		Win32Api.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
	End Sub

	Protected Overrides Sub OnMouseLeave(e As EventArgs)
		MyBase.OnMouseLeave(e)
		Me.theMouseIsOverButton = False
		'NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
		Win32Api.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
	End Sub

	' Works without needing to call SetStyle.
	Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
		Dim theme As CheckBoxTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.CheckBoxTheme
		End If
		If theme IsNot Nothing Then
			Dim backColor1 As Color
			Dim backColor2 As Color
			Dim textColor As Color
			Dim textBackColor As Color
			Dim checkmarkColor As Color
			Dim boxBackgroundColor As Color
			Dim boxBorderColor As Color

			'If (Enabled) Then
			'	If (Focused) Then
			'		borderColor = Colors.BlueHighlight
			'		fillColor = Colors.BlueSelection
			'	End If
			'	If (_controlState == DarkControlState.Hover) Then
			'		borderColor = Colors.BlueHighlight
			'		fillColor = Colors.BlueSelection
			'	ElseIf (_controlState == DarkControlState.Pressed) Then
			'		borderColor = Colors.GreyHighlight
			'		fillColor = Colors.GreySelection
			'	End If
			'Else
			'	textColor = Colors.DisabledText
			'	borderColor = Colors.GreyHighlight
			'	fillColor = Colors.GreySelection
			'End If
			If Me.Enabled Then
				If Me.Focused OrElse Me.theMouseIsOverButton Then
					' Focus
					backColor1 = theme.FocusBackColor
					backColor2 = theme.FocusBackColor
					'backColor1 = theme.FocusTopBackColor
					'backColor2 = theme.FocusBottomBackColor
					textColor = theme.FocusForeColor
					textBackColor = Color.Transparent
					If Me.Checked Then
						checkmarkColor = theme.TickedBoxFocusCheckmarkColor
						boxBackgroundColor = theme.TickedBoxFocusBackColor
						boxBorderColor = theme.TickedBoxFocusBorderColor
					Else
						boxBackgroundColor = theme.UntickedBoxFocusBackColor
						boxBorderColor = theme.UntickedBoxFocusBorderColor
					End If
				Else
					backColor1 = theme.EnabledBackColor
					backColor2 = theme.EnabledBackColor
					textColor = theme.EnabledForeColor
					textBackColor = Color.Transparent
					If Me.Checked Then
						checkmarkColor = theme.TickedBoxEnabledCheckmarkColor
						boxBackgroundColor = theme.TickedBoxEnabledBackColor
						boxBorderColor = theme.TickedBoxEnabledBorderColor
					Else
						boxBackgroundColor = theme.UntickedBoxEnabledBackColor
						boxBorderColor = theme.UntickedBoxEnabledBorderColor
					End If
				End If
			Else
				backColor1 = theme.DisabledBackColor
				backColor2 = theme.DisabledBackColor
				textColor = theme.DisabledForeColor
				textBackColor = Color.Transparent
				If Me.Checked Then
					checkmarkColor = theme.TickedBoxDisabledCheckmarkColor
					boxBackgroundColor = theme.TickedBoxDisabledBackColor
					boxBorderColor = theme.TickedBoxDisabledBorderColor
				Else
					boxBackgroundColor = theme.UntickedBoxDisabledBackColor
					boxBorderColor = theme.UntickedBoxDisabledBorderColor
				End If
			End If

			Dim g As Graphics = e.Graphics
			Dim clientRectangle As Rectangle = Me.ClientRectangle

			' Draw background of entire checkbox widget.
			Using b As New LinearGradientBrush(clientRectangle, backColor1, backColor2, LinearGradientMode.Vertical)
				g.FillRectangle(b, clientRectangle)
			End Using

			Dim boxWidth As Integer = 12
			Dim boxRect As New Rectangle(0, CInt((clientRectangle.Height * 0.5) - (boxWidth * 0.5)), boxWidth, boxWidth)

			' Draw box background.
			Using b As New SolidBrush(boxBackgroundColor)
				g.FillRectangle(b, boxRect)
			End Using
			' Draw box border.
			Using p As New Pen(boxBorderColor)
				g.DrawRectangle(p, boxRect)
			End Using

			' Draw checkmark.
			If Me.Checked Then
				Dim originalSmoothingMode As SmoothingMode = g.SmoothingMode
				g.SmoothingMode = SmoothingMode.AntiAlias

				Dim left As Integer = boxRect.Left
				Dim top As Integer = boxRect.Top

				'TODO: If checkbox is smaller, draw a filled rectangle instead of checkmark.
				' Checkmark is 9 pixels wide, 6 pixels high.
				Using checkmarkPen As New Pen(checkmarkColor)
					'checkmarkPen.Width = 2
					Dim pt1 As New Point(left + 2, top + 6)
					Dim pt2 As New Point(left + 4, top + 8)
					g.DrawLine(checkmarkPen, pt1, pt2)
					pt1 = New Point(left + 2, top + 7)
					pt2 = New Point(left + 4, top + 9)
					g.DrawLine(checkmarkPen, pt1, pt2)
					'checkmarkPen.Width = 1
					pt1 = New Point(left + 5, top + 8)
					pt2 = New Point(left + 10, top + 3)
					g.DrawLine(checkmarkPen, pt1, pt2)
					pt1 = New Point(left + 5, top + 9)
					pt2 = New Point(left + 10, top + 4)
					g.DrawLine(checkmarkPen, pt1, pt2)
				End Using

				g.SmoothingMode = originalSmoothingMode
			End If

			' Draw text.
			Dim textRect As New Rectangle(boxWidth + 4, 0, clientRectangle.Width - boxWidth - 4, clientRectangle.Height)
			Dim formatFlags As TextFormatFlags = TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter
			TextRenderer.DrawText(g, Me.Text, Me.theOriginalFont, textRect, textColor, textBackColor, formatFlags)
		Else
			MyBase.OnPaint(e)
		End If
	End Sub

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
		Dim theme As CheckBoxTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.CheckBoxTheme
		End If
		If theme IsNot Nothing Then
			Dim borderColor As Color
			Dim borderWidth As Integer
			If Me.Enabled Then
				If Me.Focused OrElse Me.theMouseIsOverButton Then
					borderColor = theme.FocusBorderColor
					borderWidth = theme.FocusBorderWidth
				Else
					borderColor = theme.EnabledBorderColor
					borderWidth = theme.EnabledBorderWidth
				End If
			Else
				borderColor = theme.DisabledBorderColor
				borderWidth = theme.DisabledBorderWidth
			End If

			Dim hDC As IntPtr = Win32Api.GetWindowDC(Me.Handle)
			Try
				Using g As Graphics = Graphics.FromHdc(hDC)
					' Draw border.
					Using borderColorPen As New Pen(borderColor, borderWidth)
						borderColorPen.Alignment = Drawing2D.PenAlignment.Inset
						Dim aRect As Rectangle = Rectangle.Truncate(g.VisibleClipBounds)
						If borderWidth = 1 Then
							'NOTE: DrawRectangle width and height are interpreted as the right and bottom pixels to draw when pen width is 1.
							aRect.Width -= 1
							aRect.Height -= 1
						End If
						g.DrawRectangle(borderColorPen, aRect)
					End Using
				End Using
			Finally
				Win32Api.ReleaseDC(Me.Handle, hDC)
			End Try
			m.Result = IntPtr.Zero
		End If
	End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub AppSettings_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		If e.PropertyName = "AppThemeName" Then
			Me.UpdateTheme()
			Me.Refresh()
		End If
	End Sub

#End Region

#Region "Events"

#End Region

#Region "Private Methods"

	Private Sub UpdateTheme()
		'Dim theme As CheckBoxTheme = Nothing
		'' This check prevents problems with viewing and saving Forms in VS Designer.
		'If TheApp IsNot Nothing Then
		'	theme = TheApp.Settings.SelectedAppTheme.CheckBoxTheme
		'End If
		'If theme IsNot Nothing Then
		'	'Me.FlatStyle = FlatStyle.Flat
		'	'Me.FlatAppearance.BorderColor = Color.LightGray
		'	'Me.FlatAppearance.BorderSize = 2
		'	'Me.FlatAppearance.CheckedBackColor = Color.Red
		'	'Me.FlatAppearance.MouseDownBackColor = Color.Gray
		'	'Me.FlatAppearance.MouseOverBackColor = Color.Green
		'Else
		'	'Me.FlatStyle = FlatStyle.Standard
		'End If
		'NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
		Win32Api.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
	End Sub

	Private Sub UpdateNonClientPadding()
		If Me.DesignMode Then
			Exit Sub
		End If

		Dim left As Integer = 0
		Dim top As Integer = 0
		Dim right As Integer = 0
		Dim bottom As Integer = 0

		Dim theme As CheckBoxTheme = Nothing
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.CheckBoxTheme
		End If
		If theme IsNot Nothing Then
			Dim borderWidth As Integer
			If Me.Enabled Then
				If Me.theMouseIsOverButton Then
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

		Me.NonClientPadding = New Padding(left, top, right, bottom)
	End Sub

	Private Sub ResizeClientRect(ByVal padding As Padding, ByRef rect As Win32Api.RECT)
		rect.Left += padding.Left
		rect.Top += padding.Top
		rect.Right -= padding.Right
		rect.Bottom -= padding.Bottom
	End Sub

#End Region

#Region "Data"

	Private NonClientPadding As Padding

	Protected theControlIsReadOnly As Boolean
	Private theOriginalFont As Font

	Private theMouseIsOverButton As Boolean

#End Region

End Class
