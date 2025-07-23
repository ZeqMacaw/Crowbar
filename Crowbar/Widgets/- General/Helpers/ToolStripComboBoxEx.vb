Imports System.Windows.Forms.Design

' This attribute is required for a ToolStripControlHost to appear in the ToolStrip dropdown list in the Designer.
<ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)>
Public Class ToolStripComboBoxEx
	Inherits ToolStripControlHost

	Public Sub New()
		MyBase.New(New ComboUserControl())

		' Must set to False to prevent this widget from being incorrect size.
		MyBase.AutoSize = False
		Me.theComboBox = CType(Me.Control, ComboUserControl)
	End Sub

	Public ReadOnly Property ComboBox As ComboUserControl
		Get
			Return Me.theComboBox
		End Get
	End Property

	'Public Property DropDownStyle() As ComboBoxStyle
	'	Get
	'		Return Me.theComboBox.DropDownStyle
	'	End Get
	'	Set
	'		Me.theComboBox.DropDownStyle = Value
	'	End Set
	'End Property

	'Public ReadOnly Property Items As ComboUserControl.ObjectCollection
	'	Get
	'		Return Me.theComboBox.Items
	'	End Get
	'End Property

	Private theComboBox As ComboUserControl

End Class
