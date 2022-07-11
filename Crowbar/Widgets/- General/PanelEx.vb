Public Class PanelEx
	Inherits Panel

	'NOTE: DataBind a group of RadioButtons to a DataSource property that is an Integer or an Enum.
	'thisPanelEx.DataBindings.Add("SelectedIndex", theDataSourceThatHasTheIntegerProperty, "NameOfIntegerProperty", False, DataSourceUpdateMode.OnPropertyChanged)
	'thisPanelEx.DataBindings.Add("SelectedValue", theDataSourceThatHasTheEnumProperty, "NameOfEnumProperty", False, DataSourceUpdateMode.OnPropertyChanged)


	Public Sub New()
		MyBase.New()

		Me.theSelectedIndex = -1
		Me.ForeColor = WidgetTextColor
		Me.BackColor = WidgetBackColor
	End Sub

	Public Event SelectedValueChanged As EventHandler
	Public Event SelectedIndexChanged As EventHandler

	'Public Property IsReadOnly() As Boolean
	'	Get
	'		Return Me.theControlIsReadOnly
	'	End Get
	'	Set(ByVal value As Boolean)
	'		If Me.theControlIsReadOnly <> value Then
	'			Me.theControlIsReadOnly = value

	'			If Me.theControlIsReadOnly Then
	'				Me.ForeColor = SystemColors.GrayText
	'			Else
	'				Me.ForeColor = SystemColors.ControlText
	'			End If
	'		End If
	'	End Set
	'End Property

	Public ReadOnly Property RadioButtons() As RadioButton()
		Get
			Return Me.theRadioButtonList.ToArray()
		End Get
	End Property

	Public Property SelectedIndex() As Integer
		Get
			Return Me.theSelectedIndex
		End Get
		Set(ByVal value As Integer)
			If value < 0 OrElse value >= Me.theRadioButtonList.Count Then
				Return
			End If
			If value <> Me.theSelectedIndex Then
				Dim radioButton As RadioButton
				radioButton = Me.theRadioButtonList(value)
				radioButton.Checked = True
				Me.SetIndex(value)
			End If
		End Set
	End Property

	Public Property SelectedValue() As System.Enum
		Get
			Return Me.theSelectedValue
		End Get
		Set(ByVal value As System.Enum)
			'NOTE: This test is needed because Visual Studio Designer sets the property to Nothing in InitializeComponent().
			If value Is Nothing Then
				Return
			End If
			If Not value.Equals(Me.theSelectedValue) Then
				Dim radioButton As RadioButton
				For i As Integer = 0 To Me.theRadioButtonList.Count - 1
					radioButton = Me.theRadioButtonList(i)
					If value.Equals(radioButton.Tag) Then
						radioButton.Checked = True
						Me.SetValue(CType(radioButton.Tag, System.Enum))
						Exit For
					End If
				Next
			End If
		End Set
	End Property

	Protected Overloads Overrides Sub OnControlAdded(ByVal e As ControlEventArgs)
		If TypeOf e.Control Is RadioButton Then
			Dim radioButton As RadioButton = CType(e.Control, RadioButton)
			Me.theRadioButtonList.Add(radioButton)
			AddHandler radioButton.CheckedChanged, AddressOf Me.RadioButton_CheckedChanged

			If Me.theRadioButtonList.Count = 1 Then
				Me.SelectedIndex = 0
				If radioButton.Tag IsNot Nothing Then
					Me.theSelectedValue = CType(radioButton.Tag, System.Enum)
				End If
			End If
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

	Protected Overridable Sub OnSelectedIndexChanged(ByVal e As EventArgs)
		RaiseEvent SelectedIndexChanged(Me, e)
	End Sub

	Protected Overridable Sub OnSelectedValueChanged(ByVal e As EventArgs)
		RaiseEvent SelectedValueChanged(Me, e)
	End Sub

	Private Sub RadioButton_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Dim radioButton As RadioButton = CType(sender, RadioButton)
		If radioButton.Checked Then
			Me.SetIndex(Me.theRadioButtonList.IndexOf(radioButton))
			Me.SetValue(CType(radioButton.Tag, System.Enum))
		End If
	End Sub

	Private Sub SetIndex(ByVal index As Integer)
		Me.theSelectedIndex = index
		Me.OnSelectedIndexChanged(New EventArgs())
	End Sub

	Private Sub SetValue(ByVal enumValue As System.Enum)
		Me.theSelectedValue = enumValue
		Me.OnSelectedValueChanged(New EventArgs())
	End Sub

	'Protected Overrides Sub OnPaint(e As PaintEventArgs)
	'	MyBase.OnPaint(e)

	'	' Draw outer border.
	'	'Using backColorPen As New Pen(Color.Red)
	'	'	Dim aRect As Rectangle = Me.ClientRectangle
	'	'	aRect.Inflate(1, 0)
	'	'	e.Graphics.DrawRectangle(backColorPen, aRect)
	'	'End Using
	'End Sub

	'Protected theControlIsReadOnly As Boolean
	Private theRadioButtonList As New System.Collections.Generic.List(Of RadioButton)()
	Private theSelectedIndex As Integer
	Private theSelectedValue As System.Enum

End Class
