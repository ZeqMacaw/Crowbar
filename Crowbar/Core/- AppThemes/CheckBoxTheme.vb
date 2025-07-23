Imports System.Xml.Serialization

Public Class CheckBoxTheme
    Inherits FocusableWidgetTheme

#Region "Create and Destroy"

    Public Sub New()
        MyBase.New()

        Me.theEnabledBorderWidth = 0
        Me.theDisabledBorderWidth = 0

        Me.theUntickedBoxEnabledBackColor = New XmlColor(Color.FromArgb(&HFF4B4B4B))
        Me.theUntickedBoxEnabledBorderColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))

        Me.theUntickedBoxDisabledBackColor = New XmlColor(Color.FromArgb(&HFF4B4B4B))
        Me.theUntickedBoxDisabledBorderColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))

        Me.theUntickedBoxFocusBackColor = New XmlColor(Color.FromArgb(&HFF4B4B4B))
        Me.theUntickedBoxFocusBorderColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))

        Me.theTickedBoxEnabledCheckmarkColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theTickedBoxEnabledBackColor = New XmlColor(Color.FromArgb(&HFF4B4B4B))
        Me.theTickedBoxEnabledBorderColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))

        Me.theTickedBoxDisabledCheckmarkColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))
        Me.theTickedBoxDisabledBackColor = New XmlColor(Color.FromArgb(&HFF4B4B4B))
        Me.theTickedBoxDisabledBorderColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))

        Me.theTickedBoxFocusCheckmarkColor = New XmlColor("WindowColorizationColor")
        Me.theTickedBoxFocusBackColor = New XmlColor(Color.FromArgb(&HFF4B4B4B))
        Me.theTickedBoxFocusBorderColor = New XmlColor(Color.FromArgb(&HFFF1F1F1))

    End Sub

#End Region

#Region "Init and Free"

    'Public Sub Init()
    'End Sub

    'Private Sub Free()
    'End Sub

#End Region

#Region "Properties"

    Public Property UntickedBoxEnabledBackColor As XmlColor
        Get
            Return Me.theUntickedBoxEnabledBackColor
        End Get
        Set(value As XmlColor)
            Me.theUntickedBoxEnabledBackColor = value
        End Set
    End Property

    Public Property UntickedBoxEnabledBorderColor As XmlColor
        Get
            Return Me.theUntickedBoxEnabledBorderColor
        End Get
        Set(value As XmlColor)
            Me.theUntickedBoxEnabledBorderColor = value
        End Set
    End Property

    Public Property UntickedBoxDisabledBackColor As XmlColor
        Get
            Return Me.theUntickedBoxDisabledBackColor
        End Get
        Set(value As XmlColor)
            Me.theUntickedBoxDisabledBackColor = value
        End Set
    End Property

    Public Property UntickedBoxDisabledBorderColor As XmlColor
        Get
            Return Me.theUntickedBoxDisabledBorderColor
        End Get
        Set(value As XmlColor)
            Me.theUntickedBoxDisabledBorderColor = value
        End Set
    End Property

    Public Property UntickedBoxFocusBackColor As XmlColor
        Get
            Return Me.theUntickedBoxFocusBackColor
        End Get
        Set(value As XmlColor)
            Me.theUntickedBoxFocusBackColor = value
        End Set
    End Property

    Public Property UntickedBoxFocusBorderColor As XmlColor
        Get
            Return Me.theUntickedBoxFocusBorderColor
        End Get
        Set(value As XmlColor)
            Me.theUntickedBoxFocusBorderColor = value
        End Set
    End Property

    Public Property TickedBoxEnabledCheckmarkColor As XmlColor
        Get
            Return Me.theTickedBoxEnabledCheckmarkColor
        End Get
        Set(value As XmlColor)
            Me.theTickedBoxEnabledCheckmarkColor = value
        End Set
    End Property

    Public Property TickedBoxEnabledBackColor As XmlColor
        Get
            Return Me.theTickedBoxEnabledBackColor
        End Get
        Set(value As XmlColor)
            Me.theTickedBoxEnabledBackColor = value
        End Set
    End Property

    Public Property TickedBoxEnabledBorderColor As XmlColor
        Get
            Return Me.theTickedBoxEnabledBorderColor
        End Get
        Set(value As XmlColor)
            Me.theTickedBoxEnabledBorderColor = value
        End Set
    End Property

    Public Property TickedBoxDisabledCheckmarkColor As XmlColor
        Get
            Return Me.theTickedBoxDisabledCheckmarkColor
        End Get
        Set(value As XmlColor)
            Me.theTickedBoxDisabledCheckmarkColor = value
        End Set
    End Property

    Public Property TickedBoxDisabledBackColor As XmlColor
        Get
            Return Me.theTickedBoxDisabledBackColor
        End Get
        Set(value As XmlColor)
            Me.theTickedBoxDisabledBackColor = value
        End Set
    End Property

    Public Property TickedBoxDisabledBorderColor As XmlColor
        Get
            Return Me.theTickedBoxDisabledBorderColor
        End Get
        Set(value As XmlColor)
            Me.theTickedBoxDisabledBorderColor = value
        End Set
    End Property

    Public Property TickedBoxFocusCheckmarkColor As XmlColor
        Get
            Return Me.theTickedBoxFocusCheckmarkColor
        End Get
        Set(value As XmlColor)
            Me.theTickedBoxFocusCheckmarkColor = value
        End Set
    End Property

    Public Property TickedBoxFocusBackColor As XmlColor
        Get
            Return Me.theTickedBoxFocusBackColor
        End Get
        Set(value As XmlColor)
            Me.theTickedBoxFocusBackColor = value
        End Set
    End Property

    Public Property TickedBoxFocusBorderColor As XmlColor
        Get
            Return Me.theTickedBoxFocusBorderColor
        End Get
        Set(value As XmlColor)
            Me.theTickedBoxFocusBorderColor = value
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

    Private theUntickedBoxEnabledBackColor As XmlColor
    Private theUntickedBoxEnabledBorderColor As XmlColor

    Private theUntickedBoxDisabledBackColor As XmlColor
    Private theUntickedBoxDisabledBorderColor As XmlColor

    Private theUntickedBoxFocusBackColor As XmlColor
    Private theUntickedBoxFocusBorderColor As XmlColor

    Private theTickedBoxEnabledCheckmarkColor As XmlColor
    Private theTickedBoxEnabledBackColor As XmlColor
    Private theTickedBoxEnabledBorderColor As XmlColor

    Private theTickedBoxDisabledCheckmarkColor As XmlColor
    Private theTickedBoxDisabledBackColor As XmlColor
    Private theTickedBoxDisabledBorderColor As XmlColor

    Private theTickedBoxFocusCheckmarkColor As XmlColor
    Private theTickedBoxFocusBackColor As XmlColor
    Private theTickedBoxFocusBorderColor As XmlColor

#End Region

End Class
