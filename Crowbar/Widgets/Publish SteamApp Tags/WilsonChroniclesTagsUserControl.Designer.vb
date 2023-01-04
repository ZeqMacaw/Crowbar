<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WilsonChroniclesTagsUserControl
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
		Me.ParticlesCheckBox = New Crowbar.CheckBoxEx()
		Me.PropsCheckBox = New Crowbar.CheckBoxEx()
		Me.WeaponsCheckBox = New Crowbar.CheckBoxEx()
		Me.NPCsCheckBox = New Crowbar.CheckBoxEx()
		Me.CampaignCheckBox = New Crowbar.CheckBoxEx()
		Me.MapCheckBox = New Crowbar.CheckBoxEx()
		Me.FixesCheckBox = New Crowbar.CheckBoxEx()
		Me.CheckBoxEx1 = New Crowbar.CheckBoxEx()
		Me.SuspendLayout()
		'
		'ParticlesCheckBox
		'
		Me.ParticlesCheckBox.AutoSize = True
		Me.ParticlesCheckBox.IsReadOnly = False
		Me.ParticlesCheckBox.Location = New System.Drawing.Point(3, 72)
		Me.ParticlesCheckBox.Name = "ParticlesCheckBox"
		Me.ParticlesCheckBox.Size = New System.Drawing.Size(68, 17)
		Me.ParticlesCheckBox.TabIndex = 1
		Me.ParticlesCheckBox.Tag = "Particles"
		Me.ParticlesCheckBox.Text = "Particles"
		Me.ParticlesCheckBox.UseVisualStyleBackColor = True
		'
		'PropsCheckBox
		'
		Me.PropsCheckBox.AutoSize = True
		Me.PropsCheckBox.IsReadOnly = False
		Me.PropsCheckBox.Location = New System.Drawing.Point(3, 49)
		Me.PropsCheckBox.Name = "PropsCheckBox"
		Me.PropsCheckBox.Size = New System.Drawing.Size(55, 17)
		Me.PropsCheckBox.TabIndex = 0
		Me.PropsCheckBox.Tag = "Props"
		Me.PropsCheckBox.Text = "Props"
		Me.PropsCheckBox.UseVisualStyleBackColor = True
		'
		'WeaponsCheckBox
		'
		Me.WeaponsCheckBox.AutoSize = True
		Me.WeaponsCheckBox.IsReadOnly = False
		Me.WeaponsCheckBox.Location = New System.Drawing.Point(3, 3)
		Me.WeaponsCheckBox.Name = "WeaponsCheckBox"
		Me.WeaponsCheckBox.Size = New System.Drawing.Size(75, 17)
		Me.WeaponsCheckBox.TabIndex = 3
		Me.WeaponsCheckBox.Tag = "Weapons"
		Me.WeaponsCheckBox.Text = "Weapons"
		Me.WeaponsCheckBox.UseVisualStyleBackColor = True
		'
		'NPCsCheckBox
		'
		Me.NPCsCheckBox.AutoSize = True
		Me.NPCsCheckBox.IsReadOnly = False
		Me.NPCsCheckBox.Location = New System.Drawing.Point(3, 26)
		Me.NPCsCheckBox.Name = "NPCsCheckBox"
		Me.NPCsCheckBox.Size = New System.Drawing.Size(52, 17)
		Me.NPCsCheckBox.TabIndex = 2
		Me.NPCsCheckBox.Tag = "NPCs"
		Me.NPCsCheckBox.Text = "NPCs"
		Me.NPCsCheckBox.UseVisualStyleBackColor = True
		'
		'CampaignCheckBox
		'
		Me.CampaignCheckBox.AutoSize = True
		Me.CampaignCheckBox.IsReadOnly = False
		Me.CampaignCheckBox.Location = New System.Drawing.Point(3, 118)
		Me.CampaignCheckBox.Name = "CampaignCheckBox"
		Me.CampaignCheckBox.Size = New System.Drawing.Size(78, 17)
		Me.CampaignCheckBox.TabIndex = 1
		Me.CampaignCheckBox.Tag = "Campaign"
		Me.CampaignCheckBox.Text = "Campaign"
		Me.CampaignCheckBox.UseVisualStyleBackColor = True
		'
		'MapCheckBox
		'
		Me.MapCheckBox.AutoSize = True
		Me.MapCheckBox.IsReadOnly = False
		Me.MapCheckBox.Location = New System.Drawing.Point(3, 95)
		Me.MapCheckBox.Name = "MapCheckBox"
		Me.MapCheckBox.Size = New System.Drawing.Size(49, 17)
		Me.MapCheckBox.TabIndex = 0
		Me.MapCheckBox.Tag = "Map"
		Me.MapCheckBox.Text = "Map"
		Me.MapCheckBox.UseVisualStyleBackColor = True
		'
		'FixesCheckBox
		'
		Me.FixesCheckBox.AutoSize = True
		Me.FixesCheckBox.IsReadOnly = False
		Me.FixesCheckBox.Location = New System.Drawing.Point(3, 141)
		Me.FixesCheckBox.Name = "FixesCheckBox"
		Me.FixesCheckBox.Size = New System.Drawing.Size(51, 17)
		Me.FixesCheckBox.TabIndex = 4
		Me.FixesCheckBox.Tag = "Fixes"
		Me.FixesCheckBox.Text = "Fixes"
		Me.FixesCheckBox.UseVisualStyleBackColor = True
		'
		'CheckBoxEx1
		'
		Me.CheckBoxEx1.AutoSize = True
		Me.CheckBoxEx1.IsReadOnly = False
		Me.CheckBoxEx1.Location = New System.Drawing.Point(3, 141)
		Me.CheckBoxEx1.Name = "CheckBoxEx1"
		Me.CheckBoxEx1.Size = New System.Drawing.Size(51, 17)
		Me.CheckBoxEx1.TabIndex = 4
		Me.CheckBoxEx1.Tag = "Fixes"
		Me.CheckBoxEx1.Text = "Fixes"
		Me.CheckBoxEx1.UseVisualStyleBackColor = True
		'
		'WilsonChroniclesTagsUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.FixesCheckBox)
		Me.Controls.Add(Me.WeaponsCheckBox)
		Me.Controls.Add(Me.ParticlesCheckBox)
		Me.Controls.Add(Me.NPCsCheckBox)
		Me.Controls.Add(Me.PropsCheckBox)
		Me.Controls.Add(Me.CampaignCheckBox)
		Me.Controls.Add(Me.MapCheckBox)
		Me.Name = "WilsonChroniclesTagsUserControl"
		Me.Size = New System.Drawing.Size(87, 166)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents ParticlesCheckBox As CheckBoxEx
	Friend WithEvents PropsCheckBox As CheckBoxEx
	Friend WithEvents WeaponsCheckBox As CheckBoxEx
	Friend WithEvents NPCsCheckBox As CheckBoxEx
	Friend WithEvents CampaignCheckBox As CheckBoxEx
	Friend WithEvents MapCheckBox As CheckBoxEx
	Friend WithEvents FixesCheckBox As CheckBoxEx
	Friend WithEvents CheckBoxEx1 As CheckBoxEx
End Class
