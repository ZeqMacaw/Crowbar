Imports System.ComponentModel

Public Class ToolStripTextBoxEx
	'Inherits ToolStripTextBox
	Inherits ToolStripControlHost

	Public Sub New()
		'MyBase.New()
		MyBase.New(New Crowbar.RichTextBoxEx())

		Me.theTextBox = CType(Me.Control, RichTextBoxEx)
		'NOTE: Force to False to allow it to be default value, because RichTextBoxEx default is True.
		Me.theTextBox.Multiline = False

		Me.BackColor = WidgetConstants.WidgetBackColor
	End Sub

	'Public ReadOnly Property TextBox As Crowbar.RichTextBoxEx
	'	Get
	'		Return Me.theTextBox
	'	End Get
	'End Property

	Public ReadOnly Property DataBindings As ControlBindingsCollection
		Get
			Return Me.theTextBox.DataBindings
		End Get
	End Property

	<Category("Behavior")>
	<Description("Allows multiple lines of text.")>
	<DefaultValue(False)>
	Public Property Multiline() As Boolean
		Get
			Return Me.theTextBox.Multiline
		End Get
		Set
			Me.theTextBox.Multiline = Value
		End Set
	End Property

	Private theTextBox As Crowbar.RichTextBoxEx

End Class
