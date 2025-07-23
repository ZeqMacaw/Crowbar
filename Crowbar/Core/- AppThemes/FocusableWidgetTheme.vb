Imports System.Xml.Serialization

Public Class FocusableWidgetTheme
    Inherits WidgetTheme

#Region "Create and Destroy"

    Public Sub New()
        'MyBase.New()

        Me.theFocusForeColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theFocusBackColor = New XmlColor(Color.FromArgb(&HFF4B4B4B))
        'Me.theFocusTopBackColor = New XmlColor("WindowColorizationColor")
        'Me.theFocusBottomBackColor = New XmlColor(Color.FromArgb(&HFF4B4B4B))
        Me.theFocusBorderColor = New XmlColor("WindowColorizationColor")
        Me.theFocusBorderWidth = 1
    End Sub

#End Region

#Region "Init and Free"

    'Public Sub Init()
    'End Sub

    'Private Sub Free()
    'End Sub

#End Region

#Region "Properties"

    Public Property FocusForeColor As XmlColor
        Get
            Return Me.theFocusForeColor
        End Get
        Set(value As XmlColor)
            Me.theFocusForeColor = value
        End Set
    End Property

    Public Property FocusBackColor As XmlColor
        Get
            Return Me.theFocusBackColor
        End Get
        Set(value As XmlColor)
            Me.theFocusBackColor = value
        End Set
    End Property

    Public Property FocusBorderColor As XmlColor
        Get
            Return Me.theFocusBorderColor
        End Get
        Set(value As XmlColor)
            Me.theFocusBorderColor = value
        End Set
    End Property

    Public Property FocusBorderWidth As Integer
        Get
            Return Me.theFocusBorderWidth
        End Get
        Set(value As Integer)
            Me.theFocusBorderWidth = value
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

    Protected theFocusForeColor As XmlColor
    Protected theFocusBackColor As XmlColor
    Protected theFocusBorderColor As XmlColor
    Protected theFocusBorderWidth As Integer

#End Region

End Class
