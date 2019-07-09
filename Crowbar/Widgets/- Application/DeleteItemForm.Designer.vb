<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DeleteItemForm
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
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
		Me.TextBox1 = New Crowbar.TextBoxEx()
		Me.DeleteButton = New System.Windows.Forms.Button()
		Me.CancelDeleteButton = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		'
		'TextBox1
		'
		Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.TextBox1.CueBannerText = ""
		Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TextBox1.Location = New System.Drawing.Point(12, 12)
		Me.TextBox1.Multiline = True
		Me.TextBox1.Name = "TextBox1"
		Me.TextBox1.ReadOnly = True
		Me.TextBox1.Size = New System.Drawing.Size(420, 50)
		Me.TextBox1.TabIndex = 0
		Me.TextBox1.TabStop = False
		Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'DeleteButton
		'
		Me.DeleteButton.Location = New System.Drawing.Point(144, 68)
		Me.DeleteButton.Name = "DeleteButton"
		Me.DeleteButton.Size = New System.Drawing.Size(75, 23)
		Me.DeleteButton.TabIndex = 1
		Me.DeleteButton.Text = "Delete"
		Me.DeleteButton.UseVisualStyleBackColor = True
		'
		'CancelDeleteButton
		'
		Me.CancelDeleteButton.Location = New System.Drawing.Point(225, 68)
		Me.CancelDeleteButton.Name = "CancelDeleteButton"
		Me.CancelDeleteButton.Size = New System.Drawing.Size(75, 23)
		Me.CancelDeleteButton.TabIndex = 2
		Me.CancelDeleteButton.Text = "Cancel"
		Me.CancelDeleteButton.UseVisualStyleBackColor = True
		'
		'DeleteItemForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(444, 113)
		Me.ControlBox = False
		Me.Controls.Add(Me.CancelDeleteButton)
		Me.Controls.Add(Me.DeleteButton)
		Me.Controls.Add(Me.TextBox1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Name = "DeleteItemForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Delete Item"
		Me.TopMost = True
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents TextBox1 As TextBoxEx
	Friend WithEvents DeleteButton As Button
	Friend WithEvents CancelDeleteButton As Button
End Class
