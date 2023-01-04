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
		Me.MultiplayerTagCheckBox = New Crowbar.CheckBoxEx()
		Me.SingleplayerTagCheckBox = New Crowbar.CheckBoxEx()
		Me.WeaponTagCheckBox = New Crowbar.CheckBoxEx()
		Me.NPCTagCheckBox = New Crowbar.CheckBoxEx()
		Me.EnvironmentTagCheckBox = New Crowbar.CheckBoxEx()
		Me.CreatureTagCheckBox = New Crowbar.CheckBoxEx()
		Me.FixesCheckBoxEx = New Crowbar.CheckBoxEx()
		Me.CheckBoxEx1 = New Crowbar.CheckBoxEx()
		Me.SuspendLayout()
		'
		'MultiplayerTagCheckBox
		'
		Me.MultiplayerTagCheckBox.AutoSize = True
		Me.MultiplayerTagCheckBox.IsReadOnly = False
		Me.MultiplayerTagCheckBox.Location = New System.Drawing.Point(3, 72)
		Me.MultiplayerTagCheckBox.Name = "MultiplayerTagCheckBox"
		Me.MultiplayerTagCheckBox.Size = New System.Drawing.Size(68, 17)
		Me.MultiplayerTagCheckBox.TabIndex = 1
		Me.MultiplayerTagCheckBox.Tag = "Particles"
		Me.MultiplayerTagCheckBox.Text = "Particles"
		Me.MultiplayerTagCheckBox.UseVisualStyleBackColor = True
		'
		'SingleplayerTagCheckBox
		'
		Me.SingleplayerTagCheckBox.AutoSize = True
		Me.SingleplayerTagCheckBox.IsReadOnly = False
		Me.SingleplayerTagCheckBox.Location = New System.Drawing.Point(3, 49)
		Me.SingleplayerTagCheckBox.Name = "SingleplayerTagCheckBox"
		Me.SingleplayerTagCheckBox.Size = New System.Drawing.Size(55, 17)
		Me.SingleplayerTagCheckBox.TabIndex = 0
		Me.SingleplayerTagCheckBox.Tag = "Props"
		Me.SingleplayerTagCheckBox.Text = "Props"
		Me.SingleplayerTagCheckBox.UseVisualStyleBackColor = True
		'
		'WeaponTagCheckBox
		'
		Me.WeaponTagCheckBox.AutoSize = True
		Me.WeaponTagCheckBox.IsReadOnly = False
		Me.WeaponTagCheckBox.Location = New System.Drawing.Point(3, 3)
		Me.WeaponTagCheckBox.Name = "WeaponTagCheckBox"
		Me.WeaponTagCheckBox.Size = New System.Drawing.Size(75, 17)
		Me.WeaponTagCheckBox.TabIndex = 3
		Me.WeaponTagCheckBox.Tag = "Weapons"
		Me.WeaponTagCheckBox.Text = "Weapons"
		Me.WeaponTagCheckBox.UseVisualStyleBackColor = True
		'
		'NPCTagCheckBox
		'
		Me.NPCTagCheckBox.AutoSize = True
		Me.NPCTagCheckBox.IsReadOnly = False
		Me.NPCTagCheckBox.Location = New System.Drawing.Point(3, 26)
		Me.NPCTagCheckBox.Name = "NPCTagCheckBox"
		Me.NPCTagCheckBox.Size = New System.Drawing.Size(52, 17)
		Me.NPCTagCheckBox.TabIndex = 2
		Me.NPCTagCheckBox.Tag = "NPCs"
		Me.NPCTagCheckBox.Text = "NPCs"
		Me.NPCTagCheckBox.UseVisualStyleBackColor = True
		'
		'EnvironmentTagCheckBox
		'
		Me.EnvironmentTagCheckBox.AutoSize = True
		Me.EnvironmentTagCheckBox.IsReadOnly = False
		Me.EnvironmentTagCheckBox.Location = New System.Drawing.Point(3, 118)
		Me.EnvironmentTagCheckBox.Name = "EnvironmentTagCheckBox"
		Me.EnvironmentTagCheckBox.Size = New System.Drawing.Size(78, 17)
		Me.EnvironmentTagCheckBox.TabIndex = 1
		Me.EnvironmentTagCheckBox.Tag = "Campaign"
		Me.EnvironmentTagCheckBox.Text = "Campaign"
		Me.EnvironmentTagCheckBox.UseVisualStyleBackColor = True
		'
		'CreatureTagCheckBox
		'
		Me.CreatureTagCheckBox.AutoSize = True
		Me.CreatureTagCheckBox.IsReadOnly = False
		Me.CreatureTagCheckBox.Location = New System.Drawing.Point(3, 95)
		Me.CreatureTagCheckBox.Name = "CreatureTagCheckBox"
		Me.CreatureTagCheckBox.Size = New System.Drawing.Size(49, 17)
		Me.CreatureTagCheckBox.TabIndex = 0
		Me.CreatureTagCheckBox.Tag = "Map"
		Me.CreatureTagCheckBox.Text = "Map"
		Me.CreatureTagCheckBox.UseVisualStyleBackColor = True
		'
		'FixesCheckBoxEx
		'
		Me.FixesCheckBoxEx.AutoSize = True
		Me.FixesCheckBoxEx.IsReadOnly = False
		Me.FixesCheckBoxEx.Location = New System.Drawing.Point(3, 141)
		Me.FixesCheckBoxEx.Name = "FixesCheckBoxEx"
		Me.FixesCheckBoxEx.Size = New System.Drawing.Size(51, 17)
		Me.FixesCheckBoxEx.TabIndex = 4
		Me.FixesCheckBoxEx.Tag = "Fixes"
		Me.FixesCheckBoxEx.Text = "Fixes"
		Me.FixesCheckBoxEx.UseVisualStyleBackColor = True
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
		Me.Controls.Add(Me.FixesCheckBoxEx)
		Me.Controls.Add(Me.WeaponTagCheckBox)
		Me.Controls.Add(Me.MultiplayerTagCheckBox)
		Me.Controls.Add(Me.NPCTagCheckBox)
		Me.Controls.Add(Me.SingleplayerTagCheckBox)
		Me.Controls.Add(Me.EnvironmentTagCheckBox)
		Me.Controls.Add(Me.CreatureTagCheckBox)
		Me.Name = "WilsonChroniclesTagsUserControl"
		Me.Size = New System.Drawing.Size(87, 166)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents MultiplayerTagCheckBox As CheckBoxEx
	Friend WithEvents SingleplayerTagCheckBox As CheckBoxEx
	Friend WithEvents WeaponTagCheckBox As CheckBoxEx
	Friend WithEvents NPCTagCheckBox As CheckBoxEx
	Friend WithEvents EnvironmentTagCheckBox As CheckBoxEx
	Friend WithEvents CreatureTagCheckBox As CheckBoxEx
	Friend WithEvents FixesCheckBoxEx As CheckBoxEx
	Friend WithEvents CheckBoxEx1 As CheckBoxEx
End Class
