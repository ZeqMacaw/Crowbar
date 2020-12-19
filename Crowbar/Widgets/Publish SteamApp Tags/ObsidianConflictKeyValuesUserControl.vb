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
			Dim tempItemKeyValues As SortedList(Of String, List(Of String)) = MyBase.ItemKeyValues

			If Not Me.OverrideModeCheckBox.Checked Then
				Me.AddKeyValue("override=false", tempItemKeyValues)
			End If

			Return tempItemKeyValues
		End Get
		Set
			Me.DebugTextBox.Text = ""
			Dim aKey As String
			If Value IsNot Nothing Then
				For keyIndex As Integer = 0 To Value.Keys.Count - 1
					aKey = Value.Keys(keyIndex)
					aKey = aKey.ToLower()

					For Each aValue As String In Value.Values(keyIndex)
						Me.DebugTextBox.Text += aKey + vbCrLf
						Me.DebugTextBox.Text += aValue + vbCrLf
					Next
				Next
			End If

			MyBase.ItemKeyValues = Value
		End Set
	End Property

End Class
