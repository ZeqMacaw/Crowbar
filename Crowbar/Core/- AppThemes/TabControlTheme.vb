Imports System.Xml.Serialization

Public Class TabControlTheme
    Inherits SelectableWidgetTheme

#Region "Create and Destroy"

    Public Sub New()
        MyBase.New()

        Me.theEnabledBackColor = New XmlColor(Color.FromArgb(&HFF4B4B4B))
        Me.theDisabledBackColor = New XmlColor(Color.FromArgb(&HFF4B4B4B))
    End Sub

#End Region

#Region "Init and Free"

    'Public Sub Init()
    'End Sub

    'Private Sub Free()
    'End Sub

#End Region

#Region "Properties"

#End Region

#Region "Methods"

#End Region

#Region "Events"

#End Region

#Region "Private Methods"

#End Region

#Region "Data"

#End Region

End Class
