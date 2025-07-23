Imports System.Xml.Serialization

Public Class DataGridViewTheme
    Inherits SelectableBorderWidgetTheme

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

    Public Property ButtonTheme() As ButtonTheme
        Get
            Return Me.theButtonTheme
        End Get
        Set(ByVal value As ButtonTheme)
            Me.theButtonTheme = value
        End Set
    End Property

    Public Property RadioButtonTheme() As RadioButtonTheme
        Get
            Return Me.theRadioButtonTheme
        End Get
        Set(ByVal value As RadioButtonTheme)
            Me.theRadioButtonTheme = value
        End Set
    End Property

    Public Property RichTextBoxTheme() As RichTextBoxTheme
        Get
            Return Me.theRichTextBoxTheme
        End Get
        Set(ByVal value As RichTextBoxTheme)
            Me.theRichTextBoxTheme = value
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

    Private theButtonTheme As ButtonTheme
    Private theRadioButtonTheme As RadioButtonTheme
    Private theRichTextBoxTheme As RichTextBoxTheme

#End Region

End Class
