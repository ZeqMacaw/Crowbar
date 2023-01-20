Imports System.ComponentModel

Public Class Base_TagsUserControl

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		'TEST: See if this prevents the overlapping or larger text on Chinese Windows.
		' This should allow Forms that inherit from this class and their widgets to use the system font instead of Visual Studio's default of Microsoft Sans Serif.
		Me.Font = New Font(SystemFonts.MessageBoxFont.Name, 8.25)

		' This call is required by the designer.
		InitializeComponent()
	End Sub

#End Region

#Region "Init and Free"

	Protected Overrides Sub Init()
		Me.theWidgets = New List(Of Control)()
		Me.GetAllWidgets(Me.Controls)

		Me.theCheckBoxesAreChangingViaMe = False
		Me.theComboBoxesAreChangingViaMe = False
		Me.theRadioButtonsAreChangingViaMe = False
		Me.theTextBoxesAreChangingViaMe = False
	End Sub

	Private Sub GetAllWidgets(ByVal iWidgets As ControlCollection)
		For Each widget As Control In iWidgets
			If TypeOf widget.Tag Is String Then
				If TypeOf widget Is CheckBoxEx Then
					Me.theWidgets.Add(widget)
				ElseIf TypeOf widget Is ComboBox Then
					Dim aComboBoxTag As String = CType(widget.Tag, String)
					If aComboBoxTag = "TagsEnabled" Then
						Me.theWidgets.Add(widget)
					End If
				ElseIf TypeOf widget Is RadioButton Then
					Me.theWidgets.Add(widget)
				ElseIf TypeOf widget Is TextBox Then
					Dim aTextBoxTag As String = CType(widget.Tag, String)
					If aTextBoxTag = "TagsEnabled" Then
						Me.theWidgets.Add(widget)
					End If
				End If
			ElseIf TypeOf widget Is GroupBox OrElse TypeOf widget Is Panel Then
				Me.GetAllWidgets(widget.Controls)
			End If
		Next
	End Sub

	Protected Overrides Sub Free()
		Dim aCheckBox As CheckBoxEx
		Dim aComboBox As ComboBox
		Dim aRadioButton As RadioButton
		Dim aTextBox As TextBox
		If Me.theWidgets IsNot Nothing Then
			For Each widget As Control In Me.theWidgets
				If TypeOf widget.Tag Is String Then
					If TypeOf widget Is CheckBoxEx Then
						aCheckBox = CType(widget, CheckBoxEx)
						RemoveHandler aCheckBox.CheckedChanged, AddressOf Me.CheckBox_CheckedChanged
					ElseIf TypeOf widget Is ComboBox Then
						Dim aComboBoxTag As String = CType(widget.Tag, String)
						If aComboBoxTag = "TagsEnabled" Then
							aComboBox = CType(widget, ComboBox)
							RemoveHandler aComboBox.SelectedIndexChanged, AddressOf Me.ComboBox_SelectedIndexChanged
						End If
					ElseIf TypeOf widget Is RadioButton Then
						aRadioButton = CType(widget, RadioButton)
						RemoveHandler aRadioButton.CheckedChanged, AddressOf Me.RadioButton_CheckedChanged
					ElseIf TypeOf widget Is TextBox Then
						Dim aTextBoxTag As String = CType(widget.Tag, String)
						If aTextBoxTag = "TagsEnabled" Then
							aTextBox = CType(widget, TextBox)
							RemoveHandler aTextBox.TextChanged, AddressOf Me.TextBox_TextChanged
						End If
					End If
				End If
			Next
		End If
	End Sub

#End Region

#Region "Properties"

	<Browsable(False)>
	Public Overridable Property ItemTags As BindingListEx(Of String)
		Get
			'NOTE: Prevent Visual Studio Designer from complaining about changes to tag widgets.
			If Me.DesignMode OrElse Me.theControlIsInDesignMode Then
				Return Nothing
			End If

			Dim aCheckBox As CheckBoxEx
			Dim aComboBox As ComboBox
			Dim aRadioButton As RadioButton
			Dim aTextBox As TextBox
			Dim anEnumList As IList
			Dim itemTagsList As New BindingListEx(Of String)()
			For Each widget As Control In Me.theWidgets
				If TypeOf widget.Tag Is String Then
					If TypeOf widget Is CheckBoxEx Then
						aCheckBox = CType(widget, CheckBoxEx)
						If aCheckBox.Checked Then
							itemTagsList.Add(CType(aCheckBox.Tag, String))
						End If
					ElseIf TypeOf widget Is ComboBox Then
						Dim aComboBoxTag As String = CType(widget.Tag, String)
						If aComboBoxTag = "TagsEnabled" Then
							aComboBox = CType(widget, ComboBox)
							If aComboBox.DataSource IsNot Nothing Then
								anEnumList = CType(aComboBox.DataSource, IList)
								itemTagsList.Add(aComboBox.SelectedValue.ToString())
							End If
						End If
					ElseIf TypeOf widget Is RadioButton Then
						aRadioButton = CType(widget, RadioButton)
						If aRadioButton.Checked Then
							itemTagsList.Add(CType(aRadioButton.Tag, String))
						End If
					ElseIf TypeOf widget Is TextBox Then
						aTextBox = CType(widget, TextBox)
						Dim aTextBoxTag As String = CType(widget.Tag, String)
						If aTextBoxTag = "TagsEnabled" AndAlso aTextBox.Text <> "" Then
							itemTagsList.Add(CType(aTextBox.Text, String))
						End If
					End If
				End If
			Next
			Return itemTagsList
		End Get
		Set
			'NOTE: Prevent Visual Studio Designer from complaining about changes to tag widgets.
			If Me.DesignMode OrElse Me.theControlIsInDesignMode Then
				Exit Property
			End If

			' Because tags widget might not be shown (such as when on unseen TabPage), must call Init() here.
			If Me.theWidgets Is Nothing Then
				Me.Init()
			End If

			Me.theCheckBoxesAreChangingViaMe = True
			Me.theComboBoxesAreChangingViaMe = True
			Me.theRadioButtonsAreChangingViaMe = True
			Me.theTextBoxesAreChangingViaMe = True

			Dim aCheckBox As CheckBoxEx
			Dim aComboBox As ComboBox
			Dim aRadioButton As RadioButton
			Dim aTextBox As TextBox

			For Each widget As Control In Me.theWidgets
				If TypeOf widget.Tag Is String Then
					If TypeOf widget Is CheckBoxEx Then
						aCheckBox = CType(widget, CheckBoxEx)
						aCheckBox.Checked = False
						RemoveHandler aCheckBox.CheckedChanged, AddressOf Me.CheckBox_CheckedChanged
						AddHandler aCheckBox.CheckedChanged, AddressOf Me.CheckBox_CheckedChanged
					ElseIf TypeOf widget Is ComboBox Then
						Dim aComboBoxTag As String = CType(widget.Tag, String)
						If aComboBoxTag = "TagsEnabled" Then
							aComboBox = CType(widget, ComboBox)
							RemoveHandler aComboBox.SelectedIndexChanged, AddressOf Me.ComboBox_SelectedIndexChanged
							AddHandler aComboBox.SelectedIndexChanged, AddressOf Me.ComboBox_SelectedIndexChanged
						End If
					ElseIf TypeOf widget Is RadioButton Then
						aRadioButton = CType(widget, RadioButton)
						aRadioButton.Checked = False
						RemoveHandler aRadioButton.CheckedChanged, AddressOf Me.RadioButton_CheckedChanged
						AddHandler aRadioButton.CheckedChanged, AddressOf Me.RadioButton_CheckedChanged
					ElseIf TypeOf widget Is TextBox Then
						Dim aTextBoxTag As String = CType(widget.Tag, String)
						If aTextBoxTag = "TagsEnabled" Then
							aTextBox = CType(widget, TextBox)
							aTextBox.Text = ""
							RemoveHandler aTextBox.TextChanged, AddressOf Me.TextBox_TextChanged
							AddHandler aTextBox.TextChanged, AddressOf Me.TextBox_TextChanged
						End If
					End If
				End If
			Next

			Dim anEnumList As IList
			Dim tagHasBeenAssigned As Boolean

			For Each tag As String In Value
				tagHasBeenAssigned = False

				For Each widget As Control In Me.theWidgets
					If TypeOf widget.Tag Is String Then
						If TypeOf widget Is CheckBoxEx Then
							aCheckBox = CType(widget, CheckBoxEx)
							If tag = CType(aCheckBox.Tag, String) Then
								aCheckBox.Checked = True
								tagHasBeenAssigned = True
								Exit For
							End If
						ElseIf TypeOf widget Is ComboBox Then
							Dim aComboBoxTag As String = CType(widget.Tag, String)
							If aComboBoxTag = "TagsEnabled" Then
								aComboBox = CType(widget, ComboBox)
								If aComboBox.DataSource IsNot Nothing Then
									anEnumList = CType(aComboBox.DataSource, IList)
									Dim index As Integer = EnumHelper.IndexOfKeyAsCaseInsensitiveString(tag, anEnumList)
									If index <> -1 Then
										aComboBox.SelectedIndex = index
										tagHasBeenAssigned = True
										Exit For
									End If
								End If
							End If
						ElseIf TypeOf widget Is RadioButton Then
							aRadioButton = CType(widget, RadioButton)
							If tag = CType(aRadioButton.Tag, String) Then
								aRadioButton.Checked = True
								tagHasBeenAssigned = True
								Exit For
							End If
						End If
					End If
				Next
				'Loop through TextBoxes last because they will be filled with tags that do not belong to any other widget.
				If Not tagHasBeenAssigned Then
					For Each widget As Control In Me.theWidgets
						If TypeOf widget.Tag Is String Then
							If TypeOf widget Is TextBox Then
								Dim aTextBoxTag As String = CType(widget.Tag, String)
								If aTextBoxTag = "TagsEnabled" Then
									aTextBox = CType(widget, TextBox)
									If aTextBox.Text = "" Then
										aTextBox.Text = tag
										Exit For
									End If
								End If
							End If
						End If
					Next
				End If
			Next

			Me.theCheckBoxesAreChangingViaMe = False
			Me.theComboBoxesAreChangingViaMe = False
			Me.theRadioButtonsAreChangingViaMe = False
			Me.theTextBoxesAreChangingViaMe = False
		End Set
	End Property

#End Region

#Region "Widget Event Handlers"

	'Private Sub TagsBaseUserControl_Load(sender As Object, e As EventArgs) Handles Me.Load
	'	Me.Init()
	'End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub CheckBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Me.OnCheckBox_CheckedChanged(sender, e)
	End Sub

	Private Sub ComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		If Not Me.theComboBoxesAreChangingViaMe Then
			RaiseEvent TagsPropertyChanged(Me, New EventArgs())
		End If
	End Sub

	Private Sub RadioButton_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Me.OnRadioButton_CheckedChanged(sender, e)
	End Sub

	Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
		If Not Me.theTextBoxesAreChangingViaMe Then
			RaiseEvent TagsPropertyChanged(Me, New EventArgs())
		End If
	End Sub

#End Region

#Region "Events"

	Public Event TagsPropertyChanged As EventHandler

#End Region

#Region "Private Methods"

	Protected Overridable Sub OnCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		If Not Me.theCheckBoxesAreChangingViaMe Then
			RaiseEvent TagsPropertyChanged(Me, New EventArgs())
		End If
	End Sub

	Protected Overridable Sub OnRadioButton_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		If Not Me.theRadioButtonsAreChangingViaMe Then
			RaiseEvent TagsPropertyChanged(Me, New EventArgs())
		End If
	End Sub

	Protected Sub RaiseTagsPropertyChanged()
		RaiseEvent TagsPropertyChanged(Me, New EventArgs())
	End Sub

#End Region

#Region "Data"

	Private theWidgets As List(Of Control)
	Protected theCheckBoxesAreChangingViaMe As Boolean
	Private theComboBoxesAreChangingViaMe As Boolean
	Private theRadioButtonsAreChangingViaMe As Boolean
	Private theTextBoxesAreChangingViaMe As Boolean

#End Region

End Class
