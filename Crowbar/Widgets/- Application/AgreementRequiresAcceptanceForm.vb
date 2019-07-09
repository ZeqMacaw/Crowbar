Public Class AgreementRequiresAcceptanceForm

	Private Sub OpenSteamSubscriberAgreementButton_Click(sender As Object, e As EventArgs) Handles OpenSteamSubscriberAgreementButton.Click
		Me.DialogResult = DialogResult.OK
		Me.Close()
	End Sub

	Private Sub OpenWorkshopPageButton_Click(sender As Object, e As EventArgs) Handles OpenWorkshopPageButton.Click
		Me.DialogResult = DialogResult.Ignore
		Me.Close()
	End Sub

	Private Sub CancelDeleteButton_Click(sender As Object, e As EventArgs) Handles CancelDeleteButton.Click
		Me.DialogResult = DialogResult.Cancel
		Me.Close()
	End Sub

End Class