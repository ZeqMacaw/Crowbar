Public Class ToolStripComboBoxEx
	Inherits ToolStripControlHost

	Public Sub New()
		MyBase.New(New ComboBoxEx())

		Me.theComboBox = CType(Me.Control, ComboBoxEx)

		'Me.ForeColor = WidgetTextColor
		'Me.BackColor = WidgetHighBackColor
	End Sub

	Public ReadOnly Property ComboBox As ComboBoxEx
		Get
			Return Me.theComboBox
		End Get
	End Property

	Public Property DropDownStyle() As ComboBoxStyle
		Get
			Return Me.theComboBox.DropDownStyle
		End Get
		Set
			Me.theComboBox.DropDownStyle = Value
		End Set
	End Property

	Public ReadOnly Property Items As ComboBox.ObjectCollection
		Get
			Return Me.theComboBox.Items
		End Get
	End Property

	Private theComboBox As ComboBoxEx

End Class
