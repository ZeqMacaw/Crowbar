Public Class CheckBoxEx
	Inherits CheckBox

	Public Sub New()
		MyBase.New()

	End Sub

	Public Property IsReadOnly() As Boolean
		Get
			Return Me.theControlIsReadOnly
		End Get
		Set(ByVal value As Boolean)
			If Me.theControlIsReadOnly <> value Then
				Me.theControlIsReadOnly = value

				If Me.theControlIsReadOnly Then
					Me.ForeColor = SystemColors.ControlText
					Me.BackColor = SystemColors.Control
					' [CheckBoxEx] Maybe: Somehow change backcolor of the box.
					' [CheckBoxEx] Maybe: Somehow disable checkmarking of the box.
				Else
					Me.ForeColor = SystemColors.ControlText
					Me.BackColor = SystemColors.Window
				End If
			End If
		End Set
	End Property

	Protected theControlIsReadOnly As Boolean

End Class
