Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.VisualStyles

Public Class ButtonEx
	Inherits Button

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.theButtonCanBeFocused = True
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

	Public Property ButtonCanBeFocused() As Boolean
		Get
			Return Me.theButtonCanBeFocused
		End Get
		Set(ByVal value As Boolean)
			If Me.theButtonCanBeFocused <> value Then
				Me.theButtonCanBeFocused = value
			End If
		End Set
	End Property

	Public Property SpecialImage() As ButtonEx.SpecialImageType
		Get
			Return Me.theSpecialImage
		End Get
		Set(ByVal value As ButtonEx.SpecialImageType)
			If Me.theSpecialImage <> value Then
				Me.theSpecialImage = value
			End If
		End Set
	End Property

#End Region

#Region "Enums"

	Public Enum SpecialImageType
		None
		DownArrow
		RightArrow
	End Enum

#End Region

#Region "Methods"

#End Region

#Region "Widget Event Handlers"

	Protected Overrides Sub OnHandleCreated(ByVal e As System.EventArgs)
		MyBase.OnHandleCreated(e)
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
	Protected Overrides Sub OnPaint(e As PaintEventArgs)
		MyBase.OnPaint(e)

		Dim g As Graphics = e.Graphics

		Dim theme As ButtonTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.ButtonTheme
		End If
		If theme IsNot Nothing Then
			Dim backColor1 As Color
			Dim backColor2 As Color
			Dim textColor As Color
			Dim textBackColor As Color

			If Me.Enabled Then
				If Me.theButtonCanBeFocused AndAlso Me.theMouseIsOverButton Then
					' Focus
					backColor1 = theme.FocusBackColor
					backColor2 = theme.FocusBackColor
					'backColor1 = theme.FocusTopBackColor
					'backColor2 = theme.FocusBottomBackColor
					textColor = theme.FocusForeColor
					textBackColor = Color.Transparent
				Else
					backColor1 = theme.EnabledBackColor
					backColor2 = theme.EnabledBackColor
					textColor = theme.EnabledForeColor
					textBackColor = Color.Transparent
				End If
			Else
				backColor1 = theme.DisabledBackColor
				backColor2 = theme.DisabledBackColor
				textColor = theme.DisabledForeColor
				textBackColor = Color.Transparent
			End If
			'Else
			'	If Me.Enabled Then
			'		If Me.theMouseIsOverButton OrElse Me.theButtonShouldBeHighlighted Then
			'			' Focus
			'			'backColor1 = Color.Green
			'			'backColor2 = WidgetHighBackColor
			'			'textColor = WidgetTextColor
			'			backColor1 = Me.BackColor
			'			backColor2 = Me.BackColor
			'			textColor = Me.ForeColor
			'			textBackColor = Me.BackColor
			'			borderColor = Me.BackColor
			'		Else
			'			backColor1 = Me.BackColor
			'			backColor2 = Me.BackColor
			'			textColor = Me.ForeColor
			'			textBackColor = Me.BackColor
			'			borderColor = Me.BackColor
			'		End If
			'	Else
			'		backColor1 = Me.BackColor
			'		backColor2 = Me.BackColor
			'		textColor = Me.ForeColor
			'		textBackColor = Me.BackColor
			'		borderColor = Me.BackColor
			'	End If

			Dim clientRectangle As Rectangle = Me.ClientRectangle

			' Draw background.
			Using aColorBrush As New LinearGradientBrush(clientRectangle, backColor1, backColor2, LinearGradientMode.Vertical)
				g.FillRectangle(aColorBrush, clientRectangle)
			End Using
			'' Draw border.
			'Using borderColorPen As New Pen(borderColor)
			'	'NOTE: DrawRectangle width and height are interpreted as the right and bottom pixels to draw.
			'	g.DrawRectangle(borderColorPen, clientRectangle.Left, clientRectangle.Top, clientRectangle.Width - 1, clientRectangle.Height - 1)
			'End Using

			' Draw text or image.
			If Me.Image Is Nothing Then
				If Me.theSpecialImage = SpecialImageType.None Then
					TextRenderer.DrawText(g, Me.Text, Me.Font, clientRectangle, textColor, textBackColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.WordBreak)
				ElseIf Me.theSpecialImage = SpecialImageType.DownArrow Then
					' Draw drop-down arrow.
					Dim middle As New Point(CInt((clientRectangle.Left + clientRectangle.Width) * 0.5), CInt((clientRectangle.Top + clientRectangle.Height) * 0.5))
					Dim arrow As Point() = {New Point(middle.X - 3, middle.Y - 2), New Point(middle.X + 4, middle.Y - 2), New Point(middle.X, middle.Y + 2)}
					Using backColorBrush As New SolidBrush(WidgetDisabledTextColor)
						e.Graphics.FillPolygon(backColorBrush, arrow)
					End Using
				ElseIf Me.theSpecialImage = SpecialImageType.RightArrow Then
				End If
			Else
				g.DrawImage(Me.Image, New Point(CInt(Me.Width * 0.5 - Me.Image.Width * 0.5), CInt(Me.Height * 0.5 - Me.Image.Height * 0.5)))
			End If
		Else
			If Me.theSpecialImage = SpecialImageType.DownArrow Then
				' Draw drop-down arrow.
				Dim aRect As Rectangle = Rectangle.Truncate(g.VisibleClipBounds)
				' Inflate to hide the button border.
				aRect.Inflate(1, 1)
				If VisualStyleRenderer.IsSupported Then
					ComboBoxRenderer.DrawDropDownButton(g, aRect, ComboBoxState.Normal)
				Else
					ControlPaint.DrawComboButton(g, aRect, ButtonState.Normal)
				End If
			ElseIf Me.theSpecialImage = SpecialImageType.RightArrow Then
			End If
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
		Dim theme As ButtonTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.ButtonTheme
		End If
		If theme IsNot Nothing Then
			Dim borderColor As Color
			Dim borderWidth As Integer
			If Me.Enabled Then
				If Me.theButtonCanBeFocused AndAlso Me.theMouseIsOverButton Then
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
		'Dim theme As ButtonTheme = Nothing
		'If TheApp IsNot Nothing Then
		'	theme = TheApp.Settings.SelectedAppTheme.ButtonTheme
		'End If
		'If theme IsNot Nothing Then
		'Else
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

		Dim theme As ButtonTheme = Nothing
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.ButtonTheme
		End If
		If theme IsNot Nothing Then
			Dim borderWidth As Integer
			If Me.Enabled Then
				If Me.theButtonCanBeFocused AndAlso Me.theMouseIsOverButton Then
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

	Private theButtonCanBeFocused As Boolean
	Private theSpecialImage As ButtonEx.SpecialImageType

	Private theMouseIsOverButton As Boolean

#End Region

End Class
