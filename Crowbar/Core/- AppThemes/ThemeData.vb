Imports System.Xml.Serialization

Public Class ThemeData

#Region "Create and Destroy"

    Public Sub New()
        'MyBase.New()

        Me.theAppThemes = New BindingListExAutoSort(Of AppTheme)("Name")

        'Me.Init()
    End Sub

#End Region

#Region "Init and Free"

    'Public Sub Init()
    'End Sub

    'Private Sub Free()
    'End Sub

#End Region

#Region "Properties"

    Public Property AppThemes() As BindingListExAutoSort(Of AppTheme)
        Get
            Return Me.theAppThemes
        End Get
        Set(ByVal value As BindingListExAutoSort(Of AppTheme))
            Me.theAppThemes = value
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

    Private theAppThemes As BindingListExAutoSort(Of AppTheme)

#End Region


End Class
