Imports System.Runtime.InteropServices

Public Class GroupBoxEx
	Inherits GroupBox

	'NOTE: DataBind a group of RadioButtons to a DataSource property that is an Enum.
	'thisGroupBoxEx.DataBindings.Add("SelectedValue", theDataSourceThatHasTheEnumProperty, "NameOfEnumProperty", False, DataSourceUpdateMode.OnPropertyChanged)


#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		'Me.theSelectedIndex = -1
		'Me.ForeColor = WidgetTextColor
		'Me.BackColor = WidgetBackColor

		'Me.SetStyle(ControlStyles.UserPaint, True)

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

	'Public Property DataSource() As Object
	'	Get
	'		Return Me.theDataSource
	'	End Get
	'	Set(ByVal value As Object)
	'		If value Is Nothing Then
	'			If Me.theDataSource IsNot Nothing Then
	'				Me.theDataSource = Nothing
	'			End If
	'		ElseIf TypeOf value Is List(Of KeyValuePair(Of System.Enum, String)) Then
	'			Me.theDataSource = CType(value, List(Of KeyValuePair(Of System.Enum, String)))
	'			If Me.theDataSource.Count > 0 Then
	'				'Me.theSelectedIndex = 0
	'				Me.theSelectedValue = Me.theDataSource(0).Key()
	'				'For Each binding As Binding In Me.DataBindings
	'				'	binding.ReadValue()
	'				'Next
	'			End If
	'		End If
	'	End Set
	'End Property

	'Public Property DisplayMember() As String
	'	Get
	'		Return Me.theDisplayMember
	'	End Get
	'	Set(ByVal value As String)
	'		Me.theDisplayMember = value
	'	End Set
	'End Property

	'Public Property ValueMember() As String
	'	Get
	'		Return Me.theValueMember
	'	End Get
	'	Set(ByVal value As String)
	'		Me.theValueMember = value
	'	End Set
	'End Property

	Public Property IsReadOnly() As Boolean
		Get
			Return Me.theControlIsReadOnly
		End Get
		Set(ByVal value As Boolean)
			If Me.theControlIsReadOnly <> value Then
				Me.theControlIsReadOnly = value
			End If
		End Set
	End Property

	Public ReadOnly Property RadioButtons() As RadioButton()
		Get
			'If Me.theDataSource IsNot Nothing Then
			'	Return theRadioButtonList.ToArray()
			'Else
			'	Return Nothing
			'End If
			Return Me.theRadioButtonList.ToArray()
		End Get
	End Property

	'Public Property SelectedIndex() As Integer
	'	Get
	'		For i As Integer = 0 To list.Count - 1
	'			If list(i).Checked Then
	'				Return i
	'			End If
	'		Next
	'		Return -1
	'	End Get
	'	Set(ByVal value As Integer)
	'		If value <> SelectedIndex Then
	'			If value = -1 Then
	'				list(SelectedIndex).Checked = False
	'			Else
	'				list(value).Checked = True
	'			End If

	'			OnSelectedIndexChanged(New EventArgs())
	'		End If
	'	End Set
	'End Property

	Public Property SelectedValue() As System.Enum
		Get
			'If Me.theDataSource Is Nothing Then
			'	Return Nothing
			'End If
			'If Me.theSelectedIndex > -1 Then
			'	'For Each binding As Binding In Me.DataBindings
			'	'	If binding.PropertyName = "SelectedValue" Then
			'	'		binding.ReadValue()
			'	'	End If
			'	'Next
			'	Me.theRadioButtonList(Me.theSelectedIndex).Checked = True
			'	Return Me.theDataSource(Me.theSelectedIndex).Key
			'Else
			'	Return Nothing
			'End If
			Return Me.theSelectedValue
		End Get
		Set(ByVal value As System.Enum)
			'NOTE: This test is needed because Visual Studio Designer sets the property to Nothing in InitializeComponent().
			If value Is Nothing Then
				Return
			End If
			Me.SetValue(value)
			Dim radioButton As RadioButton
			For i As Integer = 0 To Me.theRadioButtonList.Count - 1
				radioButton = Me.theRadioButtonList(i)
				If value.Equals(radioButton.Tag) Then
					radioButton.Checked = True
				End If
			Next
		End Set
	End Property

#End Region

#Region "Methods"

#End Region

#Region "Events"

	Public Event SelectedValueChanged As EventHandler

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

	Protected Overloads Overrides Sub OnControlAdded(ByVal e As ControlEventArgs)
		If TypeOf e.Control Is RadioButton Then
			Dim radioButton As RadioButton = CType(e.Control, RadioButton)
			Me.theRadioButtonList.Add(radioButton)
			AddHandler radioButton.CheckedChanged, AddressOf Me.RadioButton_CheckedChanged
		End If
		MyBase.OnControlAdded(e)
	End Sub

	Protected Overloads Overrides Sub OnControlRemoved(ByVal e As ControlEventArgs)
		If TypeOf e.Control Is RadioButton Then
			Dim radioButton As RadioButton = CType(e.Control, RadioButton)
			Me.theRadioButtonList.Remove(radioButton)
			RemoveHandler radioButton.CheckedChanged, AddressOf Me.RadioButton_CheckedChanged
		End If
		MyBase.OnControlRemoved(e)
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
		Dim theme As GroupBoxTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.GroupBoxTheme
		End If
		If theme IsNot Nothing Then
			Dim borderColor As Color
			Dim borderWidth As Integer
			If Me.Enabled Then
				If Me.theMouseIsOverButton Then
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

			Dim g As Graphics = e.Graphics
			Dim clientRectangle As Rectangle = Me.ClientRectangle

			' Draw background.
			Using backColorBrush As New SolidBrush(Me.BackColor)
				g.FillRectangle(backColorBrush, clientRectangle)
			End Using

			Dim stringSize As SizeF = TextRenderer.MeasureText(Me.Text, Me.Font)

			' Draw groupbox border.
			Using borderPen As New Pen(borderColor, borderWidth)
				Dim borderRect As New Rectangle(0, CInt(stringSize.Height / 2), clientRectangle.Width - 1, clientRectangle.Height - CInt(stringSize.Height / 2) - 1)
				g.DrawRectangle(borderPen, borderRect)
			End Using

			' Draw text background and text.
			Dim textIndent As Integer = 6
			Dim textRect As New Rectangle(clientRectangle.X + textIndent, clientRectangle.Y, clientRectangle.Width - (textIndent * 2), CInt(stringSize.Height))
			TextRenderer.DrawText(g, Me.Text, Me.Font, textRect, Me.ForeColor, Me.BackColor, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.WordEllipsis Or TextFormatFlags.LeftAndRightPadding)
		Else
			MyBase.OnPaint(e)
		End If
	End Sub

	Protected Overridable Sub OnSelectedValueChanged(ByVal e As EventArgs)
		RaiseEvent SelectedValueChanged(Me, e)
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub RadioButton_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Dim radioButton As RadioButton = CType(sender, RadioButton)
		If radioButton.Checked Then
			'For i As Integer = 0 To Me.theRadioButtonList.Count - 1
			'	If radioButton Is Me.theRadioButtonList(i) Then
			'		'Me.SelectedValue = Me.theDataSource(i).Key
			'		''For Each binding As Binding In Me.DataBindings
			'		''	If radioButton Is binding.Control Then
			'		''		binding.WriteValue()
			'		''	End If
			'		''Next
			'		'Me.SetValue(Me.theDataSource(i).Key)
			'		Return
			'	End If
			'Next
			Me.SetValue(CType(radioButton.Tag, System.Enum))
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

#Region "Private Methods"

	Private Sub UpdateTheme()
		Dim theme As GroupBoxTheme = Nothing
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.GroupBoxTheme
		End If
		If theme IsNot Nothing Then
			If Me.theControlIsReadOnly Then
				Me.ForeColor = theme.EnabledForeColor
			Else
				Me.ForeColor = theme.EnabledForeColor
			End If
			Me.BackColor = theme.EnabledBackColor
		Else
			Me.ForeColor = Control.DefaultForeColor
			Me.BackColor = Control.DefaultBackColor
		End If
	End Sub

	Private Sub SetValue(ByVal value As System.Enum)
		'If Me.theDataSource Is Nothing Then
		'	Return
		'End If
		'For i As Integer = 0 To Me.theDataSource.Count - 1
		'	If Me.theDataSource(i).Key.Equals(value) Then
		'		Me.theSelectedIndex = i
		'		OnSelectedValueChanged(New EventArgs())
		'		'For Each binding As Binding In Me.DataBindings
		'		'	If binding.PropertyName = "SelectedValue" Then
		'		'		binding.WriteValue()
		'		'	End If
		'		'Next
		'	End If
		'Next
		Me.theSelectedValue = value
		Me.OnSelectedValueChanged(New EventArgs())
	End Sub

#End Region

#Region "Data"

	Protected theControlIsReadOnly As Boolean
	'Private theDataSource As List(Of KeyValuePair(Of System.Enum, String))
	'Private theDisplayMember As String
	'Private theValueMember As String
	Private theRadioButtonList As New System.Collections.Generic.List(Of RadioButton)()
	'Private theSelectedIndex As Integer
	Private theSelectedValue As System.Enum

	Private theMouseIsOverButton As Boolean

#End Region

End Class
