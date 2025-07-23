Imports System.IO

Public Class HelpUserControl

#Region "Creation and Destruction"

    Public Sub New()
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                Me.Free()
                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

#End Region

#Region "Init and Free"

    Protected Overrides Sub Init()
        MyBase.Init()

        ' [04-Feb-2026] Because Me.DesignMode is unreliable in nested widgets, must do this check to prevent a crash.
        If TheApp Is Nothing Then
            Exit Sub
        End If

        Me.TutorialLinkLabel.Links.Add(0, Me.TutorialLinkLabel.Text.Length(), AppConstants.HelpTutorialLink)
        Me.ContentsLinkLabel.Links.Add(0, Me.ContentsLinkLabel.Text.Length(), AppConstants.HelpContentsLink)
        Me.IndexLinkLabel.Links.Add(0, Me.IndexLinkLabel.Text.Length(), AppConstants.HelpIndexLink)
        Me.TipsLinkLabel.Links.Add(0, Me.TipsLinkLabel.Text.Length(), AppConstants.HelpTipsLink)
    End Sub

    Protected Overrides Sub Free()
        MyBase.Free()

        ' [04-Feb-2026] Because Me.DesignMode is unreliable in nested widgets, must do this check to prevent a crash.
        If Not Me.InitHasBeenCalled OrElse TheApp Is Nothing Then
            Exit Sub
        End If
    End Sub

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

    Private Sub HelpUserControl_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' [04-Feb-2026] Me.DesignMode is unreliable in nested widgets.
        'If Not Me.DesignMode Then
        Me.Init()
        'End If
    End Sub

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
