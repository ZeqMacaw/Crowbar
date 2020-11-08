Public Class ObsidianConflictTagsUserControl

	Public Sub New()
		MyBase.New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
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
			For i As Integer = 0 To Value.Count - 1
				Value(i) = Value(i).Substring(0, 1).ToUpper + Value(i).Substring(1).ToLower
			Next

			MyBase.ItemTags = Value

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

			Me.theCheckBoxesAreChangingViaMe = True

			If Not sectionIsSet Then
				sectionIsSet = Me.SetSection(sectionIsSet, Me.PackRadioButton, Me.PackPanel)
				If Not sectionIsSet Then
					sectionIsSet = Me.SetSection(sectionIsSet, Me.MapRadioButton, Me.MapPanel)
					If Not sectionIsSet Then
						sectionIsSet = Me.SetSection(sectionIsSet, Me.PlayermodelRadioButton, Me.PlayermodelPanel)
					End If
				End If
			End If
			If Not sectionIsSet Then
				Me.PackRadioButton.Checked = True
			End If

			Me.theCheckBoxesAreChangingViaMe = False
		End Set
	End Property

	Private Sub PackRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles PackRadioButton.CheckedChanged
		If CType(sender, RadioButton).Checked Then
			Me.ClearAllPanelsExceptGivenPanel(Me.PackPanel)
			MyBase.RaiseTagsPropertyChanged()
		End If
	End Sub

	Private Sub MapRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles MapRadioButton.CheckedChanged
		If CType(sender, RadioButton).Checked Then
			Me.ClearAllPanelsExceptGivenPanel(Me.MapPanel)
			MyBase.RaiseTagsPropertyChanged()
		End If
	End Sub

	Private Sub PlayermodelRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles PlayermodelRadioButton.CheckedChanged
		If CType(sender, RadioButton).Checked Then
			Me.ClearAllPanelsExceptGivenPanel(Me.PlayermodelPanel)
			MyBase.RaiseTagsPropertyChanged()
		End If
	End Sub

	Private Function SetSection(ByVal sectionIsSet As Boolean, ByVal givenRadioButton As RadioButton, ByVal givenPanel As Panel) As Boolean
		For Each aCheckBox As CheckBox In givenPanel.Controls
			'aCheckBox.Checked = MyBase.ItemTags.Contains(CType(aCheckBox.Tag, String))
			If Not sectionIsSet AndAlso aCheckBox.Checked Then
				givenRadioButton.Checked = True
				givenPanel.Enabled = True
				sectionIsSet = True
			End If
		Next
		Return sectionIsSet
	End Function

	Private Sub ClearAllPanelsExceptGivenPanel(ByVal givenPanel As Panel)
		givenPanel.Enabled = True
		Dim aPanel As Panel
		For Each widget As Control In Me.Controls
			If TypeOf widget Is Panel Then
				aPanel = CType(widget, Panel)
				If aPanel IsNot givenPanel Then
					aPanel.Enabled = False
					For Each aCheckBox As CheckBox In aPanel.Controls
						aCheckBox.Checked = False
					Next
				End If
			End If
		Next
	End Sub

End Class
