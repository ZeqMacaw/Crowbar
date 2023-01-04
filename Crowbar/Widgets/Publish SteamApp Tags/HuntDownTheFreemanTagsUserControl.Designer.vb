<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HuntDownTheFreemanTagsUserControl
	Inherits Base_TagsUserControl

	''UserControl overrides dispose to clean up the component list.
	'<System.Diagnostics.DebuggerNonUserCode()> _
	'Protected Overrides Sub Dispose(ByVal disposing As Boolean)
	'	Try
	'		If disposing AndAlso components IsNot Nothing Then
	'			components.Dispose()
	'		End If
	'	Finally
	'		MyBase.Dispose(disposing)
	'	End Try
	'End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Me.AlternateStoryCheckBox = New Crowbar.CheckBoxEx()
		Me.ParticlesCheckBox = New Crowbar.CheckBoxEx()
		Me.MaterialsCheckBox = New Crowbar.CheckBoxEx()
		Me.ModelsCheckBox = New Crowbar.CheckBoxEx()
		Me.SoundsCheckBox = New Crowbar.CheckBoxEx()
		Me.SuspendLayout()
		'
		'AlternateStoryCheckBox
		'
		Me.AlternateStoryCheckBox.AutoSize = True
		Me.AlternateStoryCheckBox.IsReadOnly = False
		Me.AlternateStoryCheckBox.Location = New System.Drawing.Point(3, 95)
		Me.AlternateStoryCheckBox.Name = "AlternateStoryCheckBox"
		Me.AlternateStoryCheckBox.Size = New System.Drawing.Size(102, 17)
		Me.AlternateStoryCheckBox.TabIndex = 10
		Me.AlternateStoryCheckBox.Tag = "Alternate Story"
		Me.AlternateStoryCheckBox.Text = "Alternate Story"
		Me.AlternateStoryCheckBox.UseVisualStyleBackColor = True
		'
		'ParticlesCheckBox
		'
		Me.ParticlesCheckBox.AutoSize = True
		Me.ParticlesCheckBox.IsReadOnly = False
		Me.ParticlesCheckBox.Location = New System.Drawing.Point(3, 49)
		Me.ParticlesCheckBox.Name = "ParticlesCheckBox"
		Me.ParticlesCheckBox.Size = New System.Drawing.Size(68, 17)
		Me.ParticlesCheckBox.TabIndex = 9
		Me.ParticlesCheckBox.Tag = "Particles"
		Me.ParticlesCheckBox.Text = "Particles"
		Me.ParticlesCheckBox.UseVisualStyleBackColor = True
		'
		'MaterialsCheckBox
		'
		Me.MaterialsCheckBox.AutoSize = True
		Me.MaterialsCheckBox.IsReadOnly = False
		Me.MaterialsCheckBox.Location = New System.Drawing.Point(3, 26)
		Me.MaterialsCheckBox.Name = "MaterialsCheckBox"
		Me.MaterialsCheckBox.Size = New System.Drawing.Size(73, 17)
		Me.MaterialsCheckBox.TabIndex = 8
		Me.MaterialsCheckBox.Tag = "Materials"
		Me.MaterialsCheckBox.Text = "Materials"
		Me.MaterialsCheckBox.UseVisualStyleBackColor = True
		'
		'ModelsCheckBox
		'
		Me.ModelsCheckBox.AutoSize = True
		Me.ModelsCheckBox.IsReadOnly = False
		Me.ModelsCheckBox.Location = New System.Drawing.Point(3, 3)
		Me.ModelsCheckBox.Name = "ModelsCheckBox"
		Me.ModelsCheckBox.Size = New System.Drawing.Size(64, 17)
		Me.ModelsCheckBox.TabIndex = 6
		Me.ModelsCheckBox.Tag = "Models"
		Me.ModelsCheckBox.Text = "Models"
		Me.ModelsCheckBox.UseVisualStyleBackColor = True
		'
		'SoundsCheckBox
		'
		Me.SoundsCheckBox.AutoSize = True
		Me.SoundsCheckBox.IsReadOnly = False
		Me.SoundsCheckBox.Location = New System.Drawing.Point(3, 72)
		Me.SoundsCheckBox.Name = "SoundsCheckBox"
		Me.SoundsCheckBox.Size = New System.Drawing.Size(65, 17)
		Me.SoundsCheckBox.TabIndex = 4
		Me.SoundsCheckBox.Tag = "Sounds"
		Me.SoundsCheckBox.Text = "Sounds"
		Me.SoundsCheckBox.UseVisualStyleBackColor = True
		'
		'HuntDownTheFreemanTagsUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.AlternateStoryCheckBox)
		Me.Controls.Add(Me.ParticlesCheckBox)
		Me.Controls.Add(Me.MaterialsCheckBox)
		Me.Controls.Add(Me.SoundsCheckBox)
		Me.Controls.Add(Me.ModelsCheckBox)
		Me.Name = "HuntDownTheFreemanTagsUserControl"
		Me.Size = New System.Drawing.Size(114, 117)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents AlternateStoryCheckBox As CheckBoxEx
	Friend WithEvents ParticlesCheckBox As CheckBoxEx
	Friend WithEvents MaterialsCheckBox As CheckBoxEx
	Friend WithEvents ModelsCheckBox As CheckBoxEx
	Friend WithEvents SoundsCheckBox As CheckBoxEx
End Class
