<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SynergyTagsUserControl
	Inherits Base_TagsUserControl

	'UserControl overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()>
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
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Me.AddonRadioButton = New System.Windows.Forms.RadioButton()
		Me.ItemReplacementRadioButton = New System.Windows.Forms.RadioButton()
		Me.SuspendLayout()
		'
		'AddonRadioButton
		'
		Me.AddonRadioButton.AutoSize = True
		Me.AddonRadioButton.Location = New System.Drawing.Point(3, 3)
		Me.AddonRadioButton.Name = "AddonRadioButton"
		Me.AddonRadioButton.Size = New System.Drawing.Size(64, 17)
		Me.AddonRadioButton.TabIndex = 0
		Me.AddonRadioButton.TabStop = True
		Me.AddonRadioButton.Tag = "Add-on"
		Me.AddonRadioButton.Text = "Add-on"
		Me.AddonRadioButton.UseVisualStyleBackColor = True
		'
		'ItemReplacementRadioButton
		'
		Me.ItemReplacementRadioButton.AutoSize = True
		Me.ItemReplacementRadioButton.Location = New System.Drawing.Point(3, 26)
		Me.ItemReplacementRadioButton.Name = "ItemReplacementRadioButton"
		Me.ItemReplacementRadioButton.Size = New System.Drawing.Size(116, 17)
		Me.ItemReplacementRadioButton.TabIndex = 1
		Me.ItemReplacementRadioButton.TabStop = True
		Me.ItemReplacementRadioButton.Tag = "Item Replacement"
		Me.ItemReplacementRadioButton.Text = "Item Replacement"
		Me.ItemReplacementRadioButton.UseVisualStyleBackColor = True
		'
		'SynergyTagsUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.ItemReplacementRadioButton)
		Me.Controls.Add(Me.AddonRadioButton)
		Me.Name = "SynergyTagsUserControl"
		Me.Size = New System.Drawing.Size(135, 50)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents AddonRadioButton As RadioButton
	Friend WithEvents ItemReplacementRadioButton As RadioButton
End Class
