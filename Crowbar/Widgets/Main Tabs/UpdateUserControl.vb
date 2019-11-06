Public Class UpdateUserControl

#Region "Creation and Destruction"

	Public Sub New()
		MyBase.New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		Me.theUpdater = New Updater()
	End Sub

#End Region

#Region "Init and Free"

	Private Sub Init()
		Me.CheckForUpdateTextBox.DataBindings.Add("Text", Me.theUpdater, "UpdateCheckMessage", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.ChangelogTextBox.DataBindings.Add("Text", Me.theUpdater, "Changelog", False, DataSourceUpdateMode.OnPropertyChanged)
	End Sub

#End Region

#Region "Widget Event Handlers"

	Private Sub UpdateUserControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		If Not Me.DesignMode Then
			Me.Init()
		End If
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub CheckForUpdateButton_Click(sender As Object, e As EventArgs) Handles CheckForUpdateButton.Click
		Me.CheckForUpdate()
	End Sub

	Private Sub DownloadButton_Click(sender As Object, e As EventArgs) Handles DownloadButton.Click
		Me.DownloadApp()
	End Sub

	Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
		Me.UpdateApp()
	End Sub

#End Region

#Region "Core Event Handlers"

#End Region

#Region "Private Methods"

	Private Sub CheckForUpdate()
		Me.theUpdater.CheckForUpdate()
	End Sub

	Private Sub DownloadApp()
		Me.theUpdater.DownloadNewVersion()
	End Sub

	Private Sub UpdateApp()
		Me.theUpdater.DecompressAndRunNewVersion()
	End Sub

#End Region

#Region "Data"

	Private theUpdater As Updater

#End Region

End Class
