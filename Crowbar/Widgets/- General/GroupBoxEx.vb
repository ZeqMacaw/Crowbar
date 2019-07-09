Public Class GroupBoxEx
	Inherits GroupBox

	'NOTE: DataBind a group of RadioButtons to a DataSource property that is an Enum.
	'thisGroupBoxEx.DataBindings.Add("SelectedValue", theDataSourceThatHasTheEnumProperty, "NameOfEnumProperty", False, DataSourceUpdateMode.OnPropertyChanged)


	Public Sub New()
		MyBase.New()

		'Me.theSelectedIndex = -1
	End Sub

	Public Event SelectedValueChanged As EventHandler

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

				If Me.theControlIsReadOnly Then
					Me.ForeColor = SystemColors.GrayText
				Else
					Me.ForeColor = SystemColors.ControlText
				End If
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

	Protected Overridable Sub OnSelectedValueChanged(ByVal e As EventArgs)
		RaiseEvent SelectedValueChanged(Me, e)
	End Sub

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

	Protected theControlIsReadOnly As Boolean
	'Private theDataSource As List(Of KeyValuePair(Of System.Enum, String))
	'Private theDisplayMember As String
	'Private theValueMember As String
	Private theRadioButtonList As New System.Collections.Generic.List(Of RadioButton)()
	'Private theSelectedIndex As Integer
	Private theSelectedValue As System.Enum

End Class
