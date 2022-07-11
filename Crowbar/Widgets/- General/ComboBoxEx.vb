Public Class ComboBoxEx
	Inherits ComboBox

	Public Sub New()
		MyBase.New()

		Me.ForeColor = WidgetTextColor
		Me.BackColor = WidgetHighBackColor

		Me.DrawMode = DrawMode.OwnerDrawFixed
		Me.SetStyle(ControlStyles.UserPaint, True)
	End Sub

	Public Property IsReadOnly() As Boolean
		Get
			Return Me.theControlIsReadOnly
		End Get
		Set(ByVal value As Boolean)
			If Me.theControlIsReadOnly <> value Then
				Me.theControlIsReadOnly = value

				'TODO: Somehow disable value selection (i.e. no dropdown)
				'Me.Enabled = Not Me.theControlIsReadOnly
				If Me.theControlIsReadOnly Then
					Me.ForeColor = WidgetDisabledTextColor
					Me.BackColor = WidgetHighDisabledBackColor
				Else
					Me.ForeColor = WidgetTextColor
					Me.BackColor = WidgetHighBackColor
				End If
			End If
		End Set
	End Property

	Protected theControlIsReadOnly As Boolean

	Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
		'MyBase.OnDrawItem(e)

		If e.Index >= 0 Then
			' Draw drop-down-list text background.
			If (e.State And DrawItemState.Focus) > 0 Then
				Using backColorBrush As New SolidBrush(WidgetHighSelectedBackColor)
					e.Graphics.FillRectangle(backColorBrush, e.Bounds)
				End Using
			Else
				Using backColorBrush As New SolidBrush(WidgetHighBackColor)
					e.Graphics.FillRectangle(backColorBrush, e.Bounds)
				End Using
			End If

			' Draw drop-down-list text.
			TextRenderer.DrawText(e.Graphics, Me.GetItemText(Me.Items(e.Index)), e.Font, e.Bounds, WidgetTextColor, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter)
		End If
	End Sub

	Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
		If Me.theControlIsReadOnly Then
			e.Handled = True
		End If
		MyBase.OnKeyDown(e)
	End Sub

	Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
		If Me.theControlIsReadOnly Then
			e.Handled = True
		End If
		MyBase.OnKeyPress(e)
	End Sub

	Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
		MyBase.OnPaint(e)

		' Draw textbox text.
		'TextRenderer.DrawText(e.Graphics, Me.Text, Me.Font, e.ClipRectangle, WidgetTextColor, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.LeftAndRightPadding)
		TextRenderer.DrawText(e.Graphics, Me.Text, Me.Font, Me.ClientRectangle, WidgetTextColor, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.LeftAndRightPadding)

		' Draw drop-down arrow.
		Dim dropDownRect As New Rectangle(Me.ClientRectangle.Right - SystemInformation.HorizontalScrollBarArrowWidth, Me.ClientRectangle.Y, SystemInformation.HorizontalScrollBarArrowWidth, Me.ClientRectangle.Height)
		Dim middle As New Point(CInt(dropDownRect.Left + dropDownRect.Width / 2), CInt(dropDownRect.Top + dropDownRect.Height / 2))
		Dim arrow As Point() = {New Point(middle.X - 3, middle.Y - 2), New Point(middle.X + 4, middle.Y - 2), New Point(middle.X, middle.Y + 2)}
		Using backColorBrush As New SolidBrush(WidgetDisabledTextColor)
			e.Graphics.FillPolygon(backColorBrush, arrow)
		End Using
	End Sub

	Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
		'MyBase.OnPaintBackground(e)

		' Draw textbox background.
		Using backColorBrush As New SolidBrush(WidgetHighBackColor)
			e.Graphics.FillRectangle(backColorBrush, Me.ClientRectangle)
		End Using

		' Draw textbox border.
		Using thinBorderPen As New Pen(WidgetDisabledTextColor, 1)
			Dim thinBorderRect As Rectangle = Me.ClientRectangle
			'thinBorderRect.Inflate(2, 2)
			e.Graphics.DrawRectangle(thinBorderPen, thinBorderRect)
		End Using
	End Sub

	'Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
	'	If (m.Msg <> Win32Api.WindowsMessages.WM_LBUTTONDOWN And m.Msg <> Win32Api.WindowsMessages.WM_LBUTTONDBLCLK) Or Not Me.theControlIsReadOnly Then
	'		MyBase.WndProc(m)
	'	End If
	'End Sub

	Private listControl As ListNativeWindow = Nothing

	'Public Property ListBorderColor As Color
	'	Get
	'		Return m_ListBorderColor
	'	End Get
	'	Set
	'		m_ListBorderColor = Value
	'		If listControl IsNot Nothing Then
	'			listControl.BorderColor = m_ListBorderColor
	'		End If
	'	End Set
	'End Property

	Protected Overrides Sub OnHandleCreated(e As EventArgs)
		MyBase.OnHandleCreated(e)
		listControl = New ListNativeWindow(Win32Api.GetComboBoxListInternal(Me.Handle))
	End Sub

	Protected Overrides Sub OnHandleDestroyed(e As EventArgs)
		listControl.ReleaseHandle()
		MyBase.OnHandleDestroyed(e)
	End Sub

	Public Class ListNativeWindow
		Inherits NativeWindow

		Public Sub New()
			Me.New(IntPtr.Zero)
		End Sub

		Public Sub New(hWnd As IntPtr)
			If hWnd <> IntPtr.Zero Then AssignHandle(hWnd)
		End Sub

		Protected Overrides Sub WndProc(ByRef m As Message)
			MyBase.WndProc(m)

			' Draw drop-down-list border.
			If m.Msg = Win32Api.WindowsMessages.WM_NCPAINT Then
				Dim hDC As IntPtr = Win32Api.GetWindowDC(Me.Handle)
				Try
					Using g As Graphics = Graphics.FromHdc(hDC)
						Using aPen As New Pen(WidgetHighSelectedBackColor)
							Dim aRect As RectangleF = g.VisibleClipBounds
							g.DrawRectangle(aPen, 0, 0, aRect.Width - 1, aRect.Height - 1)
						End Using
					End Using
				Finally
					Win32Api.ReleaseDC(Me.Handle, hDC)
				End Try
				m.Result = IntPtr.Zero
			End If
		End Sub

	End Class

End Class
