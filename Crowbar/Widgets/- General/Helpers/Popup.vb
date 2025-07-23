Public Class Popup
	Inherits ToolStripDropDown

	Public Sub New(ByVal content As Control)
		MyBase.New()

		' Wrap the content control in a Panel so the content control can use it as a Parent if needed.
		Me.theHostPanel = New Panel()
		Me.theHostPanel.BackColor = Color.Red
		Me.theHostPanel.BorderStyle = BorderStyle.None
		Me.theHostPanel.Controls.Add(content)
		'Me.theHostPanel.Dock = DockStyle.Fill
		content.Dock = DockStyle.Fill

		Me.theHost = New ToolStripControlHost(Me.theHostPanel)
		Me.theHost.BackColor = Color.Blue
		Me.theHost.Margin = Padding.Empty
		Me.theHost.Padding = Padding.Empty
		'IMPORTANT: Prevent showing space at the top and bottom of the host control.
		Me.theHost.AutoSize = False

		Me.ResizeRedraw = True
		Me.BackColor = Color.Yellow
		Me.Margin = Padding.Empty
		Me.Padding = Padding.Empty
		Me.Items.Add(Me.theHost)
	End Sub

	Public Overloads Property Height As Integer
		Get
			Return MyBase.Height
		End Get
		Set(value As Integer)
			Me.theHostPanel.Height = value
		End Set
	End Property

	Public Overloads Property Width As Integer
		Get
			Return MyBase.Width
		End Get
		Set(value As Integer)
			Me.theHostPanel.Width = value
		End Set
	End Property

	Public ReadOnly Property Host As ToolStripControlHost
		Get
			Return theHost
		End Get
	End Property

	Private theHost As ToolStripControlHost = Nothing
	Private theHostPanel As Panel = Nothing

End Class
