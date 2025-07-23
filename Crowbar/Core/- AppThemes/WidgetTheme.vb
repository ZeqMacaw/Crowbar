Imports System.Xml.Serialization

Public Class WidgetTheme

#Region "Create and Destroy"

    Public Sub New()
        'MyBase.New()

        Me.theEnabledForeColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theEnabledBackColor = New XmlColor(Color.FromArgb(&HFF2D2D2D))
        Me.theEnabledBorderColor = New XmlColor(Color.FromArgb(&HFF555555))
        Me.theEnabledBorderWidth = 1

        Me.theDisabledForeColor = New XmlColor(Color.FromArgb(&HFF808080))
        Me.theDisabledBackColor = New XmlColor(Color.FromArgb(&HFF2D2D2D))
        Me.theDisabledBorderColor = New XmlColor(Color.FromArgb(&HFF454545))
        Me.theDisabledBorderWidth = 1
    End Sub

#End Region

#Region "Init and Free"

    'Public Sub Init()
    'End Sub

    'Private Sub Free()
    'End Sub

#End Region

#Region "Properties"

    Public Property EnabledForeColor As XmlColor
        Get
            Return Me.theEnabledForeColor
        End Get
        Set(value As XmlColor)
            Me.theEnabledForeColor = value
        End Set
    End Property

    Public Property EnabledBackColor As XmlColor
        Get
            Return Me.theEnabledBackColor
        End Get
        Set(value As XmlColor)
            Me.theEnabledBackColor = value
        End Set
    End Property

    Public Property EnabledBorderColor As XmlColor
        Get
            Return Me.theEnabledBorderColor
        End Get
        Set(value As XmlColor)
            Me.theEnabledBorderColor = value
        End Set
    End Property

    Public Property EnabledBorderWidth As Integer
        Get
            Return Me.theEnabledBorderWidth
        End Get
        Set(value As Integer)
            Me.theEnabledBorderWidth = value
        End Set
    End Property

    Public Property DisabledForeColor As XmlColor
        Get
            Return Me.theDisabledForeColor
        End Get
        Set(value As XmlColor)
            Me.theDisabledForeColor = value
        End Set
    End Property

    Public Property DisabledBackColor As XmlColor
        Get
            Return Me.theDisabledBackColor
        End Get
        Set(value As XmlColor)
            Me.theDisabledBackColor = value
        End Set
    End Property

    Public Property DisabledBorderColor As XmlColor
        Get
            Return Me.theDisabledBorderColor
        End Get
        Set(value As XmlColor)
            Me.theDisabledBorderColor = value
        End Set
    End Property

    Public Property DisabledBorderWidth As Integer
        Get
            Return Me.theDisabledBorderWidth
        End Get
        Set(value As Integer)
            Me.theDisabledBorderWidth = value
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

    Protected theEnabledForeColor As XmlColor
    Protected theEnabledBackColor As XmlColor
    Protected theEnabledBorderColor As XmlColor
    Protected theEnabledBorderWidth As Integer

    Protected theDisabledForeColor As XmlColor
    Protected theDisabledBackColor As XmlColor
    Protected theDisabledBorderColor As XmlColor
    Protected theDisabledBorderWidth As Integer

#End Region

End Class
