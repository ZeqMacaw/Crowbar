Imports System.ComponentModel

Public Class ObsidianConflictKeyValuesUserControl

	Public Sub New()
		MyBase.New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
	End Sub

	'Protected Overrides Sub Init()
	'	MyBase.Init()
	'End Sub

	<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	Public Overrides Property ItemKeyValues As SortedList(Of String, List(Of String))
		Get
			Return MyBase.ItemKeyValues
		End Get
		Set
			MyBase.ItemKeyValues = Value
		End Set
	End Property

End Class
