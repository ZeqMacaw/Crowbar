Public Class Popup
	Inherits ToolStripDropDown

	Public Sub New(ByVal content As Control)
		MyBase.New()

		Me.theHost = New ToolStripControlHost(content)
		'Me.theHost.BackColor = Color.Red
		Me.theHost.Margin = Padding.Empty
		Me.theHost.Padding = Padding.Empty
		'IMPORTANT: Prevent showing space at the top and bottom of the host control.
		Me.theHost.AutoSize = False

		Me.ResizeRedraw = True
		'Me.BackColor = Color.Green
		Me.Margin = Padding.Empty
		Me.Padding = Padding.Empty
		Me.Items.Add(Me.theHost)
	End Sub

	Public ReadOnly Property Host As ToolStripControlHost
		Get
			Return theHost
		End Get
	End Property

	Private theHost As ToolStripControlHost = Nothing

End Class
