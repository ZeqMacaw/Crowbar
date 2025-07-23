Imports System.Xml.Serialization

Public Class ComboUserControlTheme
    Inherits FocusableWidgetTheme

#Region "Create and Destroy"

    Public Sub New()
        MyBase.New()

        Me.theReadOnlyForeColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theReadOnlyBackColor = New XmlColor(Color.FromArgb(&HFF4B4B4B))
        Me.theReadOnlyBorderColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
    End Sub

#End Region

#Region "Init and Free"

    'Public Sub Init()
    'End Sub

    'Private Sub Free()
    'End Sub

#End Region

#Region "Properties"

    Public Property ReadOnlyForeColor As XmlColor
        Get
            Return Me.theReadOnlyForeColor
        End Get
        Set(value As XmlColor)
            Me.theReadOnlyForeColor = value
        End Set
    End Property

    Public Property ReadOnlyBackColor As XmlColor
        Get
            Return Me.theReadOnlyBackColor
        End Get
        Set(value As XmlColor)
            Me.theReadOnlyBackColor = value
        End Set
    End Property

    Public Property ReadOnlyBorderColor As XmlColor
        Get
            Return Me.theReadOnlyBorderColor
        End Get
        Set(value As XmlColor)
            Me.theReadOnlyBorderColor = value
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

    Private theReadOnlyForeColor As XmlColor
    Private theReadOnlyBackColor As XmlColor
    Private theReadOnlyBorderColor As XmlColor

#End Region

End Class
