Imports System.ComponentModel

Public Class Base_KeyValuesUserControl

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
		If Me.theWidgets Is Nothing Then
			Me.theWidgets = New List(Of Control)()
			Me.GetAllWidgets(Me.Controls)

			Me.theCheckBoxesAreChangingViaMe = False
			Me.theTextBoxesAreChangingViaMe = False
		End If
	End Sub

	Private Sub GetAllWidgets(ByVal iWidgets As ControlCollection)
		For Each widget As Control In iWidgets
			If TypeOf widget.Tag Is String Then
				If TypeOf widget Is CheckBoxEx Then
					Me.theWidgets.Add(widget)
				ElseIf TypeOf widget Is TextBox Then
					Dim aTextBoxTag As String = CType(widget.Tag, String)
					If aTextBoxTag.EndsWith("=") Then
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
		Dim aTextBox As TextBox
		If Me.theWidgets IsNot Nothing Then
			For Each widget As Control In Me.theWidgets
				If TypeOf widget.Tag Is String Then
					If TypeOf widget Is CheckBoxEx Then
						aCheckBox = CType(widget, CheckBoxEx)
						RemoveHandler aCheckBox.CheckedChanged, AddressOf Me.CheckBox_CheckedChanged
					ElseIf TypeOf widget Is TextBox Then
						Dim aTextBoxTag As String = CType(widget.Tag, String)
						If aTextBoxTag.EndsWith("=") Then
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

	' Example Control.Tag values: 
	'    override=true
	'    mount=hl2
	Public Overridable Property ItemKeyValues As SortedList(Of String, List(Of String))
		Get
			Dim aCheckBox As CheckBoxEx
			Dim aTextBox As TextBox
			Dim itemKeyValuesList As New SortedList(Of String, List(Of String))()
			For Each widget As Control In Me.theWidgets
				If TypeOf widget Is CheckBoxEx Then
					aCheckBox = CType(widget, CheckBoxEx)
					If aCheckBox.Checked Then
						Me.AddKeyValue(CType(aCheckBox.Tag, String), itemKeyValuesList)
					End If
				ElseIf TypeOf widget Is TextBox Then
					aTextBox = CType(widget, TextBox)
					Dim aTextBoxTag As String = CType(widget.Tag, String)
					If aTextBoxTag.EndsWith("=") AndAlso aTextBox.Text <> "" Then
						Me.AddKeyValue(aTextBoxTag + aTextBox.Text, itemKeyValuesList)
					End If
				End If
			Next
			Return itemKeyValuesList
		End Get
		Set
			' Because tags widget might not be shown (such as when on unseen TabPage), must call Init() here.
			Me.Init()

			Me.theCheckBoxesAreChangingViaMe = True
			Me.theTextBoxesAreChangingViaMe = True

			Dim aCheckBox As CheckBoxEx
			Dim aTextBox As TextBox

			For Each widget As Control In Me.theWidgets
				If TypeOf widget Is CheckBoxEx Then
					aCheckBox = CType(widget, CheckBoxEx)
					aCheckBox.Checked = False
					RemoveHandler aCheckBox.CheckedChanged, AddressOf Me.CheckBox_CheckedChanged
					AddHandler aCheckBox.CheckedChanged, AddressOf Me.CheckBox_CheckedChanged
				ElseIf TypeOf widget Is TextBox Then
					Dim aTextBoxTag As String = CType(widget.Tag, String)
					If aTextBoxTag.EndsWith("=") Then
						aTextBox = CType(widget, TextBox)
						aTextBox.Text = ""
						RemoveHandler aTextBox.TextChanged, AddressOf Me.TextBox_TextChanged
						AddHandler aTextBox.TextChanged, AddressOf Me.TextBox_TextChanged
					End If

#If DEBUG Then
					'TEST: Use this only for testing and remove for release version.
					If aTextBoxTag = "youtube=test" Then
						aTextBox = CType(widget, TextBox)
						aTextBox.Text = ""
						RemoveHandler aTextBox.TextChanged, AddressOf Me.TextBox_TextChanged
						AddHandler aTextBox.TextChanged, AddressOf Me.TextBox_TextChanged
					End If
#End If
				End If
			Next

			Dim tag As String
			Dim aKey As String

			If Value IsNot Nothing Then
				For keyIndex As Integer = 0 To Value.Keys.Count - 1
					aKey = Value.Keys(keyIndex)
					aKey = aKey.ToLower()

					For Each aValue As String In Value.Values(keyIndex)
						For Each widget As Control In Me.theWidgets
							If TypeOf widget Is CheckBoxEx Then
								aCheckBox = CType(widget, CheckBoxEx)
								tag = CType(aCheckBox.Tag, String)
								If tag = (aKey + "=" + aValue) Then
									aCheckBox.Checked = True
									Exit For
								End If

								''TEST: It looks like some video and social media options are stored as keyvalues, so ignore these.
								''      They are available via other Steamworks functions.
								'If tag = "youtube=" AndAlso aKey = "youtube" Then
								'	aCheckBox.Checked = True
								'	Exit For
								'ElseIf tag = "twitter=" AndAlso aKey = "twitter" Then
								'	aCheckBox.Checked = True
								'	Exit For
								'End If
							ElseIf TypeOf widget Is TextBox Then
								Dim aTextBoxTag As String = CType(widget.Tag, String)
								If aTextBoxTag.EndsWith("=") Then
									aTextBox = CType(widget, TextBox)
									If aTextBox.Text = "" Then
										aTextBox.Text = aValue
										Exit For
									End If
								End If
							End If
						Next
					Next
				Next
			End If

			Me.theCheckBoxesAreChangingViaMe = False
			Me.theTextBoxesAreChangingViaMe = False
		End Set
	End Property

#End Region

#Region "Widget Event Handlers"

	Private Sub KeyValuesBaseUserControl_Load(sender As Object, e As EventArgs) Handles Me.Load
		Me.Init()
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub CheckBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Me.OnCheckBox_CheckedChanged(sender, e)
	End Sub

	Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
		If Not Me.theTextBoxesAreChangingViaMe Then
			RaiseEvent KeyValuesPropertyChanged(Me, New EventArgs())
		End If
	End Sub

#End Region

#Region "Events"

	Public Event KeyValuesPropertyChanged As EventHandler

#End Region

#Region "Private Methods"

	Protected Overridable Sub OnCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		If Not Me.theCheckBoxesAreChangingViaMe Then
			RaiseEvent KeyValuesPropertyChanged(Me, New EventArgs())
		End If
	End Sub

	Protected Sub RaiseKeyValuesPropertyChanged()
		RaiseEvent KeyValuesPropertyChanged(Me, New EventArgs())
	End Sub

	Protected Sub AddKeyValue(ByVal keyValuePair As String, ByRef itemKeyValuesList As SortedList(Of String, List(Of String)))
		Dim tokens As String() = keyValuePair.Split("="c)
		Dim tagKey As String
		Dim tagValue As String
		Dim tagValueList As List(Of String)

		tagKey = tokens(0)
		tagValue = tokens(1)

		If itemKeyValuesList.ContainsKey(tagKey) Then
			tagValueList = itemKeyValuesList(tagKey)
			tagValueList.Add(tagValue)
		Else
			tagValueList = New List(Of String)()
			tagValueList.Add(tagValue)
			itemKeyValuesList.Add(tagKey, tagValueList)
		End If
	End Sub

#End Region

#Region "Data"

	Private theWidgets As List(Of Control)
	Protected theCheckBoxesAreChangingViaMe As Boolean
	Private theTextBoxesAreChangingViaMe As Boolean

#End Region

End Class
