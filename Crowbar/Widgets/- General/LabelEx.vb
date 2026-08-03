Imports System.Runtime.InteropServices

Public Class LabelEx
	Inherits Label

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()
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
		Dim theme As LabelTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.LabelTheme
		End If
		If theme IsNot Nothing Then
			Dim borderColor As Color
			Dim borderWidth As Integer
			If Me.Enabled Then
				borderColor = theme.EnabledBorderColor
				borderWidth = theme.EnabledBorderWidth
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
		Dim theme As LabelTheme = Nothing
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.LabelTheme
		End If
		If theme IsNot Nothing Then
			If Me.Enabled Then
				Me.ForeColor = theme.EnabledForeColor
				Me.BackColor = theme.EnabledBackColor
			Else
				Me.ForeColor = theme.DisabledForeColor
				Me.BackColor = theme.DisabledBackColor
			End If
		Else
			Me.ForeColor = Control.DefaultForeColor
			Me.BackColor = Control.DefaultBackColor
		End If
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

		Dim theme As LabelTheme = Nothing
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.LabelTheme
		End If
		If theme IsNot Nothing Then
			Dim borderWidth As Integer
			If Me.Enabled Then
				borderWidth = theme.EnabledBorderWidth
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

#End Region

End Class
