Imports System.Xml.Serialization

Public Class SelectableWidgetTheme
    Inherits FocusableWidgetTheme

#Region "Create and Destroy"

    Public Sub New()
        MyBase.New()

        Me.theSelectedForeColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theSelectedBackColor = New XmlColor("WindowColorizationColor")
    End Sub

#End Region

#Region "Init and Free"

    'Public Sub Init()
    'End Sub

    'Private Sub Free()
    'End Sub

#End Region

#Region "Properties"

    Public Property SelectedForeColor As XmlColor
        Get
            Return Me.theSelectedForeColor
        End Get
        Set(value As XmlColor)
            Me.theSelectedForeColor = value
        End Set
    End Property

    Public Property SelectedBackColor As XmlColor
        Get
            Return Me.theSelectedBackColor
        End Get
        Set(value As XmlColor)
            Me.theSelectedBackColor = value
        End Set
    End Property

#End Region

#Region "Methods"

#End Region

#Region "Events"


#End Region

#Region "Private Methods"

#End Region

#Region "Data"

    Protected theSelectedForeColor As XmlColor
    Protected theSelectedBackColor As XmlColor

#End Region

End Class
