Imports System.Xml.Serialization

Public Class AppTheme

#Region "Create and Destroy"

    Public Sub New()
        'MyBase.New()

        Me.theName = "Windows Default"
    End Sub

#End Region

#Region "Init and Free"

    'Public Sub Init()
    'End Sub

    'Private Sub Free()
    'End Sub

#End Region

#Region "Properties"

    Public Property Name() As String
        Get
            Return Me.theName
        End Get
        Set(ByVal value As String)
            Me.theName = value
        End Set
    End Property

    Public Property GlobalTheme() As GlobalTheme
        Get
            Return Me.theGlobalTheme
        End Get
        Set(ByVal value As GlobalTheme)
            Me.theGlobalTheme = value
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

    Public Property CheckBoxTheme() As CheckBoxTheme
        Get
            Return Me.theCheckBoxTheme
        End Get
        Set(ByVal value As CheckBoxTheme)
            Me.theCheckBoxTheme = value
        End Set
    End Property

    Public Property ComboUserControlTheme() As ComboUserControlTheme
        Get
            Return Me.theComboUserControlTheme
        End Get
        Set(ByVal value As ComboUserControlTheme)
            Me.theComboUserControlTheme = value
        End Set
    End Property

    Public Property DataGridViewTheme() As DataGridViewTheme
        Get
            Return Me.theDataGridViewTheme
        End Get
        Set(ByVal value As DataGridViewTheme)
            Me.theDataGridViewTheme = value
        End Set
    End Property

    Public Property DateTimeTextBoxTheme() As WidgetTheme
        Get
            Return Me.theDateTimeTextBoxTheme
        End Get
        Set(ByVal value As WidgetTheme)
            Me.theDateTimeTextBoxTheme = value
        End Set
    End Property

    Public Property GroupBoxTheme() As GroupBoxTheme
        Get
            Return Me.theGroupBoxTheme
        End Get
        Set(ByVal value As GroupBoxTheme)
            Me.theGroupBoxTheme = value
        End Set
    End Property

    Public Property PanelTheme() As PanelTheme
        Get
            Return Me.thePanelTheme
        End Get
        Set(ByVal value As PanelTheme)
            Me.thePanelTheme = value
        End Set
    End Property

    Public Property ProgressBarTheme() As ProgressBarTheme
        Get
            Return Me.theProgressBarTheme
        End Get
        Set(ByVal value As ProgressBarTheme)
            Me.theProgressBarTheme = value
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

    Public Property ScrollBarTheme() As WidgetTheme
        Get
            Return Me.theScrollBarTheme
        End Get
        Set(ByVal value As WidgetTheme)
            Me.theScrollBarTheme = value
        End Set
    End Property

    Public Property SplitContainerTheme() As SplitContainerTheme
        Get
            Return Me.theSplitContainerTheme
        End Get
        Set(ByVal value As SplitContainerTheme)
            Me.theSplitContainerTheme = value
        End Set
    End Property

    Public Property TabControlTheme() As TabControlTheme
        Get
            Return Me.theTabControlTheme
        End Get
        Set(ByVal value As TabControlTheme)
            Me.theTabControlTheme = value
        End Set
    End Property

    Public Property TabPageTheme() As WidgetTheme
        Get
            Return Me.theTabPageTheme
        End Get
        Set(ByVal value As WidgetTheme)
            Me.theTabPageTheme = value
        End Set
    End Property

    Public Property TabScrollerTheme() As WidgetTheme
        Get
            Return Me.theTabScrollerTheme
        End Get
        Set(ByVal value As WidgetTheme)
            Me.theTabScrollerTheme = value
        End Set
    End Property

    Public Property ToolStripTheme() As ToolStripTheme
        Get
            Return Me.theToolStripTheme
        End Get
        Set(ByVal value As ToolStripTheme)
            Me.theToolStripTheme = value
        End Set
    End Property

    Public Property TreeViewTheme() As TreeViewTheme
        Get
            Return Me.theTreeViewTheme
        End Get
        Set(ByVal value As TreeViewTheme)
            Me.theTreeViewTheme = value
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

    Private theName As String
    Private theGlobalTheme As GlobalTheme

    Private theButtonTheme As ButtonTheme
    Private theCheckBoxTheme As CheckBoxTheme
    Private theComboUserControlTheme As ComboUserControlTheme
    Private theDataGridViewTheme As DataGridViewTheme
    Private theDateTimeTextBoxTheme As WidgetTheme
    Private theGroupBoxTheme As GroupBoxTheme
    Private thePanelTheme As PanelTheme
    Private theProgressBarTheme As ProgressBarTheme
    Private theRadioButtonTheme As RadioButtonTheme
    Private theRichTextBoxTheme As RichTextBoxTheme
    Private theScrollBarTheme As WidgetTheme
    Private theSplitContainerTheme As SplitContainerTheme
    Private theTabControlTheme As TabControlTheme
    Private theTabPageTheme As WidgetTheme
    Private theTabScrollerTheme As WidgetTheme
    Private theToolStripTheme As ToolStripTheme
    Private theTreeViewTheme As TreeViewTheme

#End Region

End Class
