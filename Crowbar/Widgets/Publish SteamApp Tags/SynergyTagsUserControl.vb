Public Class SynergyTagsUserControl

	Public Sub New()
		MyBase.New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		Me.theRadioButtonsAreBeingChangedViaMe = False
	End Sub

	'Protected Overrides Sub Init()
	'	MyBase.Init()

	'End Sub

	Public Overrides Property ItemTags As BindingListEx(Of String)
		Get
			Dim tags As BindingListEx(Of String) = MyBase.ItemTags
			Dim aRadioButton As RadioButton
			For Each widget As Control In Me.Controls
				If TypeOf widget Is RadioButton Then
					aRadioButton = CType(widget, RadioButton)
					If aRadioButton.Checked Then
						tags.Insert(0, CType(aRadioButton.Tag, String))
						Exit For
					End If
				End If
			Next
			Return tags
			'Return MyBase.ItemTags
		End Get
		Set
			'Change all tags to proper casing, in case another tool has published tags with different casing.
			Dim tempTag As String
			For i As Integer = 0 To Value.Count - 1
				tempTag = Value(i).ToLower
				If tempTag = "item replacement" Then
					Value(i) = "Item Replacement"
				Else
					Value(i) = Value(i).Substring(0, 1).ToUpper + Value(i).Substring(1).ToLower
				End If
			Next

			MyBase.ItemTags = Value

			Me.theRadioButtonsAreBeingChangedViaMe = True

			Dim sectionIsSet As Boolean = False
			Dim tags As BindingListEx(Of String) = Value
			Dim aRadioButton As RadioButton
			For Each tag As String In Value
				For Each widget As Control In Me.Controls
					If TypeOf widget Is RadioButton Then
						aRadioButton = CType(widget, RadioButton)
						If tag = CType(aRadioButton.Tag, String) Then
							aRadioButton.Checked = True
							sectionIsSet = True
							Exit For
						End If
					End If
				Next
			Next
			If Not sectionIsSet Then
				Me.ItemReplacementRadioButton.Checked = True
				MyBase.RaiseTagsPropertyChanged()
			End If

			Me.theRadioButtonsAreBeingChangedViaMe = False
		End Set
	End Property

	Private Sub AddonRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles AddonRadioButton.CheckedChanged
		If CType(sender, RadioButton).Checked AndAlso Not Me.theRadioButtonsAreBeingChangedViaMe Then
			MyBase.RaiseTagsPropertyChanged()
		End If
	End Sub

	Private Sub ItemReplacementRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles ItemReplacementRadioButton.CheckedChanged
		If CType(sender, RadioButton).Checked AndAlso Not Me.theRadioButtonsAreBeingChangedViaMe Then
			MyBase.RaiseTagsPropertyChanged()
		End If
	End Sub

	Private theRadioButtonsAreBeingChangedViaMe As Boolean

End Class
