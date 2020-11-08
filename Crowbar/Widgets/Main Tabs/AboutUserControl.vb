Public Class AboutUserControl

#Region "Creation and Destruction"

	Public Sub New()
		' This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub

#End Region

#Region "Init and Free"

	Private Sub Init()
		'NOTE: Customize the application's assembly information in the "Application" pane of the project 
		'    properties dialog (under the "Project" menu).

		Me.ProductNameLinkLabel.Text = My.Application.Info.ProductName
		Me.ProductNameLinkLabel.Links.Add(0, My.Application.Info.ProductName.Length(), My.Resources.About_ProductLink)

		Me.GotoSteamGroupLinkLabel.Text = My.Resources.About_GotoSteamGroupText
		Me.GotoSteamGroupLinkLabel.Links.Add(0, My.Resources.About_GotoSteamGroupText.Length(), My.Resources.About_ProductLink)

		Me.ProductInfoTextBox.Text = "Version " + My.Application.Info.Version.ToString(2) + vbCrLf
		Me.ProductInfoTextBox.Text += My.Application.Info.Copyright + vbCrLf
		Me.ProductInfoTextBox.Text += My.Application.Info.CompanyName

		Me.AuthorLinkLabel.Text = My.Application.Info.CompanyName
		Me.AuthorLinkLabel.Links.Add(0, My.Application.Info.CompanyName.Length(), My.Resources.About_AuthorLink)

		Me.GotoSteamProfileLinkLabel.Text = My.Resources.About_GotoSteamProfileText
		Me.GotoSteamProfileLinkLabel.Links.Add(0, My.Resources.About_GotoSteamProfileText.Length(), My.Resources.About_AuthorLink)

		Me.ProductDescriptionTextBox.Text = My.Resources.About_ProductDescription

		'Me.Panel1.DataBindings.Add("BackColor", TheApp.Settings, "AboutTabBackgroundColor", False, DataSourceUpdateMode.OnPropertyChanged)
	End Sub

	'Private Sub Free()

	'End Sub

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

	Private Sub AboutUserControl_Load(sender As Object, e As EventArgs) Handles Me.Load
		If Not Me.DesignMode Then
			Me.Init()
		End If
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub ProductLogoButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductLogoButton.Click
		System.Diagnostics.Process.Start(My.Resources.About_ProductLink)
	End Sub

	Private Sub AuthorIconButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AuthorIconButton.Click
		System.Diagnostics.Process.Start(My.Resources.About_AuthorLink)
	End Sub

	Private Sub LinkLabel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles ProductNameLinkLabel.LinkClicked, AuthorLinkLabel.LinkClicked, GotoSteamGroupLinkLabel.LinkClicked, GotoSteamProfileLinkLabel.LinkClicked
		Dim aLinkLabel As LinkLabel
		aLinkLabel = CType(sender, LinkLabel)

		If e.Button = Windows.Forms.MouseButtons.Left Then
			aLinkLabel.LinkVisited = True
			Dim target As String = CType(e.Link.LinkData, String)
			System.Diagnostics.Process.Start(target)
		ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
			'TODO: Show context menu with: Copy Link, Copy Text
		End If
	End Sub

	Private Sub PayPalPictureBox_Click(sender As Object, e As EventArgs) Handles PayPalPictureBox.Click
		System.Diagnostics.Process.Start(My.Resources.About_PayPalLink)
	End Sub

#End Region

#Region "Core Event Handlers"

#End Region

#Region "Private Methods"

#End Region

#Region "Data"

#End Region

End Class
