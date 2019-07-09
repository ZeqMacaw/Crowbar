<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReleaseUserControl
	Inherits BaseUserControl

	'UserControl overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.TextBoxEx4 = New Crowbar.TextBoxEx()
		Me.Panel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.TextBoxEx4)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(776, 536)
		Me.Panel1.TabIndex = 0
		'
		'TextBoxEx4
		'
		Me.TextBoxEx4.Location = New System.Drawing.Point(3, 3)
		Me.TextBoxEx4.Name = "TextBoxEx4"
		Me.TextBoxEx4.ReadOnly = True
		Me.TextBoxEx4.Size = New System.Drawing.Size(778, 22)
		Me.TextBoxEx4.TabIndex = 2
		Me.TextBoxEx4.Text = "Not implemented yet."
		'
		'ReleaseUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel1)
		Me.Name = "ReleaseUserControl"
		Me.Size = New System.Drawing.Size(776, 536)
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents Panel1 As System.Windows.Forms.Panel
	Friend WithEvents TextBoxEx4 As Crowbar.TextBoxEx

End Class
