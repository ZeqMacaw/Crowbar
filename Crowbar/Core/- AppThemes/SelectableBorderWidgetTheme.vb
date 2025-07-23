Imports System.Xml.Serialization

Public Class SelectableBorderWidgetTheme
    Inherits SelectableWidgetTheme

#Region "Create and Destroy"

    Public Sub New()
        MyBase.New()

        Me.theSelectedBorderColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theSelectedBorderWidth = 1
    End Sub

#End Region

#Region "Init and Free"

    'Public Sub Init()
    'End Sub

    'Private Sub Free()
    'End Sub

#End Region

#Region "Properties"

    Public Property SelectedBorderColor As XmlColor
        Get
            Return Me.theSelectedBorderColor
        End Get
        Set(value As XmlColor)
            Me.theSelectedBorderColor = value
        End Set
    End Property

    Public Property SelectedBorderWidth As Integer
        Get
            Return Me.theSelectedBorderWidth
        End Get
        Set(value As Integer)
            Me.theSelectedBorderWidth = value
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

    Protected theSelectedBorderColor As XmlColor
    Protected theSelectedBorderWidth As Integer

#End Region

End Class
