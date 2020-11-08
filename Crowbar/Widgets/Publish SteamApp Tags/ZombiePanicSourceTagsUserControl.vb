Public Class ZombiePanicSourceTagsUserControl

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
			Return MyBase.ItemTags
		End Get
		Set
			MyBase.ItemTags = Value

			Me.theCheckBoxesAreChangingViaMe = True

			Dim sectionIsSet As Boolean = False
			sectionIsSet = Me.SetSection(sectionIsSet, Me.GameModeRadioButton, Me.GameModePanel)
			If Not sectionIsSet Then
				sectionIsSet = Me.SetSection(sectionIsSet, Me.CustomModelsRadioButton, Me.CustomModelsPanel)
				If Not sectionIsSet Then
					sectionIsSet = Me.SetSection(sectionIsSet, Me.CustomSoundsRadioButton, Me.CustomSoundsPanel)
					If Not sectionIsSet Then
						sectionIsSet = Me.SetSection(sectionIsSet, Me.MiscellaneousRadioButton, Me.MiscellaneousPanel)
					End If
				End If
			End If
			If Not sectionIsSet Then
				Me.GameModeRadioButton.Checked = True
			End If

			Me.theCheckBoxesAreChangingViaMe = False
		End Set
	End Property

	Private Sub GameModeRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles GameModeRadioButton.CheckedChanged
		If CType(sender, RadioButton).Checked Then
			Me.ClearAllPanelsExceptGivenPanel(Me.GameModePanel)
		End If
	End Sub

	Private Sub CustomModelsRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles CustomModelsRadioButton.CheckedChanged
		If CType(sender, RadioButton).Checked Then
			Me.ClearAllPanelsExceptGivenPanel(Me.CustomModelsPanel)
		End If
	End Sub

	Private Sub CustomSoundsRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles CustomSoundsRadioButton.CheckedChanged
		If CType(sender, RadioButton).Checked Then
			Me.ClearAllPanelsExceptGivenPanel(Me.CustomSoundsPanel)
		End If
	End Sub

	Private Sub MiscellaneousRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles MiscellaneousRadioButton.CheckedChanged
		If CType(sender, RadioButton).Checked Then
			Me.ClearAllPanelsExceptGivenPanel(Me.MiscellaneousPanel)
		End If
	End Sub

	Private Function SetSection(ByVal sectionIsSet As Boolean, ByVal givenRadioButton As RadioButton, ByVal givenPanel As Panel) As Boolean
		For Each aCheckBox As CheckBox In givenPanel.Controls
			aCheckBox.Checked = MyBase.ItemTags.Contains(CType(aCheckBox.Tag, String))
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
