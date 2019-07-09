Imports System.IO

Public Class HelpUserControl

#Region "Creation and Destruction"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        'NOTE: Try-Catch is needed so that widget will be shown in MainForm without raising exception.
        Try
            Me.Init()
        Catch
        End Try
    End Sub

#End Region

#Region "Init and Free"

    Private Sub Init()
        Me.TutorialLinkLabel.Links.Add(0, Me.TutorialLinkLabel.Text.Length(), AppConstants.HelpTutorialLink)
        Me.ContentsLinkLabel.Links.Add(0, Me.ContentsLinkLabel.Text.Length(), AppConstants.HelpContentsLink)
        Me.IndexLinkLabel.Links.Add(0, Me.IndexLinkLabel.Text.Length(), AppConstants.HelpIndexLink)
        Me.TipsLinkLabel.Links.Add(0, Me.TipsLinkLabel.Text.Length(), AppConstants.HelpTipsLink)
    End Sub

    'Private Sub Free()

    'End Sub

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

#End Region

#Region "Child Widget Event Handlers"

	Private Sub CrowbarGuideButton_Click(sender As Object, e As EventArgs) Handles CrowbarGuideButton.Click
		System.Diagnostics.Process.Start(My.Resources.Help_CrowbarGuideLink)
	End Sub

    Private Sub LinkLabel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles TutorialLinkLabel.LinkClicked, ContentsLinkLabel.LinkClicked, IndexLinkLabel.LinkClicked, TipsLinkLabel.LinkClicked
        Dim aLinkLabel As LinkLabel
        aLinkLabel = CType(sender, LinkLabel)

        If e.Button = Windows.Forms.MouseButtons.Left Then
            aLinkLabel.LinkVisited = True
            Dim target As String = CType(e.Link.LinkData, String)
            Try
                System.Diagnostics.Process.Start(target)
            Catch ex As Exception
                'TODO: Tell user what went wrong.
            End Try
        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
            'TODO: Show context menu with: Copy Link, Copy Text
        End If
    End Sub

#End Region

#Region "Core Event Handlers"

#End Region

#Region "Private Methods"

#End Region

#Region "Data"

#End Region

End Class
