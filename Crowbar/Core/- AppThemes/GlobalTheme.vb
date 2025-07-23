Imports System.Xml.Serialization

Public Class GlobalTheme
    Inherits WidgetTheme

#Region "Create and Destroy"

    Public Sub New()
        MyBase.New()

        Me.theHighEnabledForeColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theHighEnabledBackColor = New XmlColor(Color.FromArgb(&HFF4B4B4B))
        Me.theHighEnabledBorderColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theHighEnabledBorderWidth = 1

        Me.theHighDisabledForeColor = New XmlColor(Color.FromArgb(&HFF808080))
        Me.theHighDisabledBackColor = New XmlColor(Color.FromArgb(&HFF4B4B4B))
        Me.theHighDisabledBorderColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theHighDisabledBorderWidth = 1

        Me.theHighFocusForeColor = New XmlColor("WindowColorizationColor")
        Me.theHighFocusBackColor = New XmlColor(Color.FromArgb(&HFF4B4B4B))
        Me.theHighFocusBorderColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theHighFocusBorderWidth = 1

        Me.theHighSelectedForeColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theHighSelectedBackColor = New XmlColor("WindowColorizationColor")
        Me.theHighSelectedBorderColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theHighSelectedBorderWidth = 1

        Me.theDeepEnabledForeColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theDeepEnabledBackColor = New XmlColor(Color.FromArgb(&HFF1E1E1E))
        Me.theDeepEnabledBorderColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theDeepEnabledBorderWidth = 1

        Me.theDeepDisabledForeColor = New XmlColor(Color.FromArgb(&HFF808080))
        Me.theDeepDisabledBackColor = New XmlColor(Color.FromArgb(&HFF2D2D2D))
        Me.theDeepDisabledBorderColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theDeepDisabledBorderWidth = 1

        Me.theDeepFocusForeColor = New XmlColor("WindowColorizationColor")
        Me.theDeepFocusBackColor = New XmlColor(Color.FromArgb(&HFF4B4B4B))
        Me.theDeepFocusBorderColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theDeepFocusBorderWidth = 1

        Me.theDeepSelectedForeColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theDeepSelectedBackColor = New XmlColor("WindowColorizationColor")
        Me.theDeepSelectedBorderColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theDeepSelectedBorderWidth = 1
    End Sub

#End Region

#Region "Init and Free"

    'Public Sub Init()
    'End Sub

    'Private Sub Free()
    'End Sub

#End Region

#Region "Properties"

    Public Property HighEnabledForeColor As XmlColor
        Get
            Return Me.theHighEnabledForeColor
        End Get
        Set(value As XmlColor)
            Me.theHighEnabledForeColor = value
        End Set
    End Property

    Public Property HighEnabledBackColor As XmlColor
        Get
            Return Me.theHighEnabledBackColor
        End Get
        Set(value As XmlColor)
            Me.theHighEnabledBackColor = value
        End Set
    End Property

    Public Property HighEnabledBorderColor As XmlColor
        Get
            Return Me.theHighEnabledBorderColor
        End Get
        Set(value As XmlColor)
            Me.theHighEnabledBorderColor = value
        End Set
    End Property

    Public Property HighEnabledBorderWidth As Integer
        Get
            Return Me.theHighEnabledBorderWidth
        End Get
        Set(value As Integer)
            Me.theHighEnabledBorderWidth = value
        End Set
    End Property

    Public Property HighDisabledForeColor As XmlColor
        Get
            Return Me.theHighDisabledForeColor
        End Get
        Set(value As XmlColor)
            Me.theHighDisabledForeColor = value
        End Set
    End Property

    Public Property HighDisabledBackColor As XmlColor
        Get
            Return Me.theHighDisabledBackColor
        End Get
        Set(value As XmlColor)
            Me.theHighDisabledBackColor = value
        End Set
    End Property

    Public Property HighDisabledBorderColor As XmlColor
        Get
            Return Me.theHighDisabledBorderColor
        End Get
        Set(value As XmlColor)
            Me.theHighDisabledBorderColor = value
        End Set
    End Property

    Public Property HighDisabledBorderWidth As Integer
        Get
            Return Me.theHighDisabledBorderWidth
        End Get
        Set(value As Integer)
            Me.theHighDisabledBorderWidth = value
        End Set
    End Property

    Public Property HighFocusForeColor As XmlColor
        Get
            Return Me.theHighFocusForeColor
        End Get
        Set(value As XmlColor)
            Me.theHighFocusForeColor = value
        End Set
    End Property

    Public Property HighFocusBackColor As XmlColor
        Get
            Return Me.theHighFocusBackColor
        End Get
        Set(value As XmlColor)
            Me.theHighFocusBackColor = value
        End Set
    End Property

    Public Property HighFocusBorderColor As XmlColor
        Get
            Return Me.theHighFocusBorderColor
        End Get
        Set(value As XmlColor)
            Me.theHighFocusBorderColor = value
        End Set
    End Property

    Public Property HighFocusBorderWidth As Integer
        Get
            Return Me.theHighFocusBorderWidth
        End Get
        Set(value As Integer)
            Me.theHighFocusBorderWidth = value
        End Set
    End Property

    Public Property HighSelectedForeColor As XmlColor
        Get
            Return Me.theHighSelectedForeColor
        End Get
        Set(value As XmlColor)
            Me.theHighSelectedForeColor = value
        End Set
    End Property

    Public Property HighSelectedBackColor As XmlColor
        Get
            Return Me.theHighSelectedBackColor
        End Get
        Set(value As XmlColor)
            Me.theHighSelectedBackColor = value
        End Set
    End Property

    Public Property HighSelectedBorderColor As XmlColor
        Get
            Return Me.theHighSelectedBorderColor
        End Get
        Set(value As XmlColor)
            Me.theHighSelectedBorderColor = value
        End Set
    End Property

    Public Property HighSelectedBorderWidth As Integer
        Get
            Return Me.theHighSelectedBorderWidth
        End Get
        Set(value As Integer)
            Me.theHighSelectedBorderWidth = value
        End Set
    End Property

    Public Property DeepEnabledForeColor As XmlColor
        Get
            Return Me.theDeepEnabledForeColor
        End Get
        Set(value As XmlColor)
            Me.theDeepEnabledForeColor = value
        End Set
    End Property

    Public Property DeepEnabledBackColor As XmlColor
        Get
            Return Me.theDeepEnabledBackColor
        End Get
        Set(value As XmlColor)
            Me.theDeepEnabledBackColor = value
        End Set
    End Property

    Public Property DeepEnabledBorderColor As XmlColor
        Get
            Return Me.theDeepEnabledBorderColor
        End Get
        Set(value As XmlColor)
            Me.theDeepEnabledBorderColor = value
        End Set
    End Property

    Public Property DeepEnabledBorderWidth As Integer
        Get
            Return Me.theDeepEnabledBorderWidth
        End Get
        Set(value As Integer)
            Me.theDeepEnabledBorderWidth = value
        End Set
    End Property

    Public Property DeepDisabledForeColor As XmlColor
        Get
            Return Me.theDeepDisabledForeColor
        End Get
        Set(value As XmlColor)
            Me.theDeepDisabledForeColor = value
        End Set
    End Property

    Public Property DeepDisabledBackColor As XmlColor
        Get
            Return Me.theDeepDisabledBackColor
        End Get
        Set(value As XmlColor)
            Me.theDeepDisabledBackColor = value
        End Set
    End Property

    Public Property DeepDisabledBorderColor As XmlColor
        Get
            Return Me.theDeepDisabledBorderColor
        End Get
        Set(value As XmlColor)
            Me.theDeepDisabledBorderColor = value
        End Set
    End Property

    Public Property DeepDisabledBorderWidth As Integer
        Get
            Return Me.theDeepDisabledBorderWidth
        End Get
        Set(value As Integer)
            Me.theDeepDisabledBorderWidth = value
        End Set
    End Property

    Public Property DeepFocusForeColor As XmlColor
        Get
            Return Me.theDeepFocusForeColor
        End Get
        Set(value As XmlColor)
            Me.theDeepFocusForeColor = value
        End Set
    End Property

    Public Property DeepFocusBackColor As XmlColor
        Get
            Return Me.theDeepFocusBackColor
        End Get
        Set(value As XmlColor)
            Me.theDeepFocusBackColor = value
        End Set
    End Property

    Public Property DeepFocusBorderColor As XmlColor
        Get
            Return Me.theDeepFocusBorderColor
        End Get
        Set(value As XmlColor)
            Me.theDeepFocusBorderColor = value
        End Set
    End Property

    Public Property DeepFocusBorderWidth As Integer
        Get
            Return Me.theDeepFocusBorderWidth
        End Get
        Set(value As Integer)
            Me.theDeepFocusBorderWidth = value
        End Set
    End Property

    Public Property DeepSelectedForeColor As XmlColor
        Get
            Return Me.theDeepSelectedForeColor
        End Get
        Set(value As XmlColor)
            Me.theDeepSelectedForeColor = value
        End Set
    End Property

    Public Property DeepSelectedBackColor As XmlColor
        Get
            Return Me.theDeepSelectedBackColor
        End Get
        Set(value As XmlColor)
            Me.theDeepSelectedBackColor = value
        End Set
    End Property

    Public Property DeepSelectedBorderColor As XmlColor
        Get
            Return Me.theDeepSelectedBorderColor
        End Get
        Set(value As XmlColor)
            Me.theDeepSelectedBorderColor = value
        End Set
    End Property

    Public Property DeepSelectedBorderWidth As Integer
        Get
            Return Me.theDeepSelectedBorderWidth
        End Get
        Set(value As Integer)
            Me.theDeepSelectedBorderWidth = value
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

    Protected theHighEnabledForeColor As XmlColor
    Protected theHighEnabledBackColor As XmlColor
    Protected theHighEnabledBorderColor As XmlColor
    Protected theHighEnabledBorderWidth As Integer

    Protected theHighDisabledForeColor As XmlColor
    Protected theHighDisabledBackColor As XmlColor
    Protected theHighDisabledBorderColor As XmlColor
    Protected theHighDisabledBorderWidth As Integer

    Protected theHighFocusForeColor As XmlColor
    Protected theHighFocusBackColor As XmlColor
    Protected theHighFocusBorderColor As XmlColor
    Protected theHighFocusBorderWidth As Integer

    Protected theHighSelectedForeColor As XmlColor
    Protected theHighSelectedBackColor As XmlColor
    Protected theHighSelectedBorderColor As XmlColor
    Protected theHighSelectedBorderWidth As Integer

    Protected theDeepEnabledForeColor As XmlColor
    Protected theDeepEnabledBackColor As XmlColor
    Protected theDeepEnabledBorderColor As XmlColor
    Protected theDeepEnabledBorderWidth As Integer

    Protected theDeepDisabledForeColor As XmlColor
    Protected theDeepDisabledBackColor As XmlColor
    Protected theDeepDisabledBorderColor As XmlColor
    Protected theDeepDisabledBorderWidth As Integer

    Protected theDeepFocusForeColor As XmlColor
    Protected theDeepFocusBackColor As XmlColor
    Protected theDeepFocusBorderColor As XmlColor
    Protected theDeepFocusBorderWidth As Integer

    Protected theDeepSelectedForeColor As XmlColor
    Protected theDeepSelectedBackColor As XmlColor
    Protected theDeepSelectedBorderColor As XmlColor
    Protected theDeepSelectedBorderWidth As Integer

#End Region

End Class
