Imports System.ComponentModel

Public Class CodenameCureTagsUserControl

	Public Sub New()
		MyBase.New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
	End Sub

	'NOTE: Make the Miscellaneous tag the default tag, because there should always be one selected.
	<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	Public Overrides Property ItemTags As BindingListEx(Of String)
		Get
			Return MyBase.ItemTags
		End Get
		Set
			MyBase.ItemTags = Value

			Dim tags As BindingListEx(Of String) = MyBase.ItemTags
			If tags.Count = 0 Then
				tags.Add("Miscellaneous")
				Me.MiscellaneousRadioButton.Checked = True
				MyBase.RaiseTagsPropertyChanged()
			End If
		End Set
	End Property

End Class
