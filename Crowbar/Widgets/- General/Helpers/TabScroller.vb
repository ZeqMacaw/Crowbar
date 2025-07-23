'FROM: Mick Doherty's TabControl Tips
'      Add a custom Scroller to Tabcontrol.
'      https://dotnetrix.co.uk/tabcontrol.htm#tip15
Public Class TabScroller
	Inherits System.Windows.Forms.Control

#Region " Windows Form Designer generated code "

	Public Sub New()
		MyBase.New()

		'This call is required by the Windows Form Designer.
		InitializeComponent()

		'Add any initialization after the InitializeComponent() call

	End Sub

	'UserControl overrides dispose to clean up the component list.
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Me.LeftScroller = New ButtonEx
		Me.RightScroller = New ButtonEx
		'Me.CloseButton = New ButtonEx
		Me.SuspendLayout()
		'
		'LeftScroller
		'
		Me.LeftScroller.Dock = System.Windows.Forms.DockStyle.Right
		' Using this prevents ButtonEx.OnPaint from being called.
		'Me.LeftScroller.FlatStyle = System.Windows.Forms.FlatStyle.System
		Me.LeftScroller.Location = New System.Drawing.Point(0, 0)
		Me.LeftScroller.Name = "LeftScroller"
		Me.LeftScroller.Size = New System.Drawing.Size(40, 40)
		Me.LeftScroller.TabIndex = 0
		' "3" in Marlett font is a left arrow.
		Me.LeftScroller.Text = "3"
		'Me.LeftScroller.UseVisualStyleBackColor = True
		'
		'RightScroller
		'
		Me.RightScroller.Dock = System.Windows.Forms.DockStyle.Right
		' Using this prevents ButtonEx.OnPaint from being called.
		'Me.RightScroller.FlatStyle = System.Windows.Forms.FlatStyle.System
		Me.RightScroller.Location = New System.Drawing.Point(40, 0)
		Me.RightScroller.Name = "RightScroller"
		Me.RightScroller.Size = New System.Drawing.Size(40, 40)
		Me.RightScroller.TabIndex = 1
		' "4" in Marlett font is a right arrow.
		Me.RightScroller.Text = "4"
		''
		''CloseButton
		''
		'Me.CloseButton.Dock = System.Windows.Forms.DockStyle.Right
		'Me.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.System
		'Me.CloseButton.Location = New System.Drawing.Point(80, 0)
		'Me.CloseButton.Name = "CloseButton"
		'Me.CloseButton.Size = New System.Drawing.Size(40, 40)
		'Me.CloseButton.TabIndex = 2
		'Me.CloseButton.Text = "r"
		'
		'TabScroller
		'
		Me.Controls.Add(Me.LeftScroller)
		Me.Controls.Add(Me.RightScroller)
		'Me.Controls.Add(Me.CloseButton)
		Me.Font = New System.Drawing.Font("Marlett", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
		Me.Name = "TabScroller"
		Me.Size = New System.Drawing.Size(120, 40)
		Me.ResumeLayout(False)
	End Sub

	Friend WithEvents LeftScroller As ButtonEx
	Friend WithEvents RightScroller As ButtonEx
	'Friend WithEvents CloseButton As ButtonEx

#End Region

	Private Sub TabScroller_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
		LeftScroller.Width = Me.Width \ 2
		RightScroller.Width = Me.Width \ 2
		'LeftScroller.Width = Me.Width \ 3
		'RightScroller.Width = Me.Width \ 3
		'CloseButton.Width = Me.Width \ 3
	End Sub

	Private Sub LeftScroller_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LeftScroller.Click
		RaiseEvent ScrollLeft(Me, EventArgs.Empty)
	End Sub

	Private Sub RightScroller_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RightScroller.Click
		RaiseEvent ScrollRight(Me, EventArgs.Empty)
	End Sub

	'Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
	'	RaiseEvent TabClose(Me, EventArgs.Empty)
	'End Sub

	Public Event ScrollLeft As EventHandler
	Public Event ScrollRight As EventHandler
	'Public Event TabClose As EventHandler

End Class
