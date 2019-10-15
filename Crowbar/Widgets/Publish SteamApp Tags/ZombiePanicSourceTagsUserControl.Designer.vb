<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZombiePanicSourceTagsUserControl
	Inherits Base_TagsUserControl

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
		Me.SurvivalCheckBox = New CheckBoxEx()
		Me.ObjectiveCheckBox = New CheckBoxEx()
		Me.HardcoreCheckBox = New CheckBoxEx()
		Me.CustomCheckBox = New CheckBoxEx()
		Me.GameModeRadioButton = New System.Windows.Forms.RadioButton()
		Me.GUIsCheckBox = New CheckBoxEx()
		Me.WeaponsCheckBox = New CheckBoxEx()
		Me.PropsCheckBox = New CheckBoxEx()
		Me.CharactersCheckBox = New CheckBoxEx()
		Me.CustomModelsRadioButton = New System.Windows.Forms.RadioButton()
		Me.SoundPackCheckBox = New CheckBoxEx()
		Me.ModelPackCheckBox = New CheckBoxEx()
		Me.WeaponSoundsCheckBox = New CheckBoxEx()
		Me.CharacterSoundsCheckBox = New CheckBoxEx()
		Me.CustomSoundsRadioButton = New System.Windows.Forms.RadioButton()
		Me.MiscellaneousRadioButton = New System.Windows.Forms.RadioButton()
		Me.GameModePanel = New System.Windows.Forms.Panel()
		Me.CustomModelsPanel = New System.Windows.Forms.Panel()
		Me.CustomSoundsPanel = New System.Windows.Forms.Panel()
		Me.MiscellaneousPanel = New System.Windows.Forms.Panel()
		Me.GameModePanel.SuspendLayout()
		Me.CustomModelsPanel.SuspendLayout()
		Me.CustomSoundsPanel.SuspendLayout()
		Me.MiscellaneousPanel.SuspendLayout()
		Me.SuspendLayout()
		'
		'SurvivalCheckBox
		'
		Me.SurvivalCheckBox.AutoSize = True
		Me.SurvivalCheckBox.Location = New System.Drawing.Point(0, 0)
		Me.SurvivalCheckBox.Name = "SurvivalCheckBox"
		Me.SurvivalCheckBox.Size = New System.Drawing.Size(64, 17)
		Me.SurvivalCheckBox.TabIndex = 0
		Me.SurvivalCheckBox.Tag = "Survival"
		Me.SurvivalCheckBox.Text = "Survival"
		Me.SurvivalCheckBox.UseVisualStyleBackColor = True
		'
		'ObjectiveCheckBox
		'
		Me.ObjectiveCheckBox.AutoSize = True
		Me.ObjectiveCheckBox.Location = New System.Drawing.Point(0, 23)
		Me.ObjectiveCheckBox.Name = "ObjectiveCheckBox"
		Me.ObjectiveCheckBox.Size = New System.Drawing.Size(72, 17)
		Me.ObjectiveCheckBox.TabIndex = 1
		Me.ObjectiveCheckBox.Tag = "Objective"
		Me.ObjectiveCheckBox.Text = "Objective"
		Me.ObjectiveCheckBox.UseVisualStyleBackColor = True
		'
		'HardcoreCheckBox
		'
		Me.HardcoreCheckBox.AutoSize = True
		Me.HardcoreCheckBox.Location = New System.Drawing.Point(0, 46)
		Me.HardcoreCheckBox.Name = "HardcoreCheckBox"
		Me.HardcoreCheckBox.Size = New System.Drawing.Size(70, 17)
		Me.HardcoreCheckBox.TabIndex = 2
		Me.HardcoreCheckBox.Tag = "Hardcore"
		Me.HardcoreCheckBox.Text = "Hardcore"
		Me.HardcoreCheckBox.UseVisualStyleBackColor = True
		'
		'CustomCheckBox
		'
		Me.CustomCheckBox.AutoSize = True
		Me.CustomCheckBox.Location = New System.Drawing.Point(0, 69)
		Me.CustomCheckBox.Name = "CustomCheckBox"
		Me.CustomCheckBox.Size = New System.Drawing.Size(62, 17)
		Me.CustomCheckBox.TabIndex = 3
		Me.CustomCheckBox.Tag = "Custom"
		Me.CustomCheckBox.Text = "Custom"
		Me.CustomCheckBox.UseVisualStyleBackColor = True
		'
		'GameModeRadioButton
		'
		Me.GameModeRadioButton.AutoSize = True
		Me.GameModeRadioButton.Checked = True
		Me.GameModeRadioButton.Location = New System.Drawing.Point(3, 3)
		Me.GameModeRadioButton.Name = "GameModeRadioButton"
		Me.GameModeRadioButton.Size = New System.Drawing.Size(85, 17)
		Me.GameModeRadioButton.TabIndex = 4
		Me.GameModeRadioButton.TabStop = True
		Me.GameModeRadioButton.Text = "Game Mode:"
		Me.GameModeRadioButton.UseVisualStyleBackColor = True
		'
		'GUIsCheckBox
		'
		Me.GUIsCheckBox.AutoSize = True
		Me.GUIsCheckBox.Location = New System.Drawing.Point(0, 0)
		Me.GUIsCheckBox.Name = "GUIsCheckBox"
		Me.GUIsCheckBox.Size = New System.Drawing.Size(49, 17)
		Me.GUIsCheckBox.TabIndex = 7
		Me.GUIsCheckBox.Tag = "GUIs"
		Me.GUIsCheckBox.Text = "GUIs"
		Me.GUIsCheckBox.UseVisualStyleBackColor = True
		'
		'WeaponsCheckBox
		'
		Me.WeaponsCheckBox.AutoSize = True
		Me.WeaponsCheckBox.Location = New System.Drawing.Point(0, 46)
		Me.WeaponsCheckBox.Name = "WeaponsCheckBox"
		Me.WeaponsCheckBox.Size = New System.Drawing.Size(71, 17)
		Me.WeaponsCheckBox.TabIndex = 6
		Me.WeaponsCheckBox.Tag = "Weapons"
		Me.WeaponsCheckBox.Text = "Weapons"
		Me.WeaponsCheckBox.UseVisualStyleBackColor = True
		'
		'PropsCheckBox
		'
		Me.PropsCheckBox.AutoSize = True
		Me.PropsCheckBox.Location = New System.Drawing.Point(0, 23)
		Me.PropsCheckBox.Name = "PropsCheckBox"
		Me.PropsCheckBox.Size = New System.Drawing.Size(53, 17)
		Me.PropsCheckBox.TabIndex = 5
		Me.PropsCheckBox.Tag = "Props"
		Me.PropsCheckBox.Text = "Props"
		Me.PropsCheckBox.UseVisualStyleBackColor = True
		'
		'CharactersCheckBox
		'
		Me.CharactersCheckBox.AutoSize = True
		Me.CharactersCheckBox.Location = New System.Drawing.Point(0, 0)
		Me.CharactersCheckBox.Name = "CharactersCheckBox"
		Me.CharactersCheckBox.Size = New System.Drawing.Size(79, 17)
		Me.CharactersCheckBox.TabIndex = 4
		Me.CharactersCheckBox.Tag = "Characters"
		Me.CharactersCheckBox.Text = "Characters"
		Me.CharactersCheckBox.UseVisualStyleBackColor = True
		'
		'CustomModelsRadioButton
		'
		Me.CustomModelsRadioButton.AutoSize = True
		Me.CustomModelsRadioButton.Location = New System.Drawing.Point(3, 118)
		Me.CustomModelsRadioButton.Name = "CustomModelsRadioButton"
		Me.CustomModelsRadioButton.Size = New System.Drawing.Size(101, 17)
		Me.CustomModelsRadioButton.TabIndex = 7
		Me.CustomModelsRadioButton.Text = "Custom Models:"
		Me.CustomModelsRadioButton.UseVisualStyleBackColor = True
		'
		'SoundPackCheckBox
		'
		Me.SoundPackCheckBox.AutoSize = True
		Me.SoundPackCheckBox.Location = New System.Drawing.Point(1, 46)
		Me.SoundPackCheckBox.Name = "SoundPackCheckBox"
		Me.SoundPackCheckBox.Size = New System.Drawing.Size(81, 17)
		Me.SoundPackCheckBox.TabIndex = 11
		Me.SoundPackCheckBox.Tag = "Sound Pack"
		Me.SoundPackCheckBox.Text = "Sound Pack"
		Me.SoundPackCheckBox.UseVisualStyleBackColor = True
		'
		'ModelPackCheckBox
		'
		Me.ModelPackCheckBox.AutoSize = True
		Me.ModelPackCheckBox.Location = New System.Drawing.Point(1, 23)
		Me.ModelPackCheckBox.Name = "ModelPackCheckBox"
		Me.ModelPackCheckBox.Size = New System.Drawing.Size(79, 17)
		Me.ModelPackCheckBox.TabIndex = 10
		Me.ModelPackCheckBox.Tag = "Model Pack"
		Me.ModelPackCheckBox.Text = "Model Pack"
		Me.ModelPackCheckBox.UseVisualStyleBackColor = True
		'
		'WeaponSoundsCheckBox
		'
		Me.WeaponSoundsCheckBox.AutoSize = True
		Me.WeaponSoundsCheckBox.Location = New System.Drawing.Point(0, 23)
		Me.WeaponSoundsCheckBox.Name = "WeaponSoundsCheckBox"
		Me.WeaponSoundsCheckBox.Size = New System.Drawing.Size(104, 17)
		Me.WeaponSoundsCheckBox.TabIndex = 9
		Me.WeaponSoundsCheckBox.Tag = "Weapon Sounds"
		Me.WeaponSoundsCheckBox.Text = "Weapon Sounds"
		Me.WeaponSoundsCheckBox.UseVisualStyleBackColor = True
		'
		'CharacterSoundsCheckBox
		'
		Me.CharacterSoundsCheckBox.AutoSize = True
		Me.CharacterSoundsCheckBox.Location = New System.Drawing.Point(0, 0)
		Me.CharacterSoundsCheckBox.Name = "CharacterSoundsCheckBox"
		Me.CharacterSoundsCheckBox.Size = New System.Drawing.Size(112, 17)
		Me.CharacterSoundsCheckBox.TabIndex = 8
		Me.CharacterSoundsCheckBox.Tag = "Characters Sounds"
		Me.CharacterSoundsCheckBox.Text = "Character Sounds"
		Me.CharacterSoundsCheckBox.UseVisualStyleBackColor = True
		'
		'CustomSoundsRadioButton
		'
		Me.CustomSoundsRadioButton.AutoSize = True
		Me.CustomSoundsRadioButton.Location = New System.Drawing.Point(3, 210)
		Me.CustomSoundsRadioButton.Name = "CustomSoundsRadioButton"
		Me.CustomSoundsRadioButton.Size = New System.Drawing.Size(103, 17)
		Me.CustomSoundsRadioButton.TabIndex = 12
		Me.CustomSoundsRadioButton.Text = "Custom Sounds:"
		Me.CustomSoundsRadioButton.UseVisualStyleBackColor = True
		'
		'MiscellaneousRadioButton
		'
		Me.MiscellaneousRadioButton.AutoSize = True
		Me.MiscellaneousRadioButton.Location = New System.Drawing.Point(3, 279)
		Me.MiscellaneousRadioButton.Name = "MiscellaneousRadioButton"
		Me.MiscellaneousRadioButton.Size = New System.Drawing.Size(94, 17)
		Me.MiscellaneousRadioButton.TabIndex = 13
		Me.MiscellaneousRadioButton.Text = "Miscellaneous:"
		Me.MiscellaneousRadioButton.UseVisualStyleBackColor = True
		'
		'GameModePanel
		'
		Me.GameModePanel.Controls.Add(Me.SurvivalCheckBox)
		Me.GameModePanel.Controls.Add(Me.ObjectiveCheckBox)
		Me.GameModePanel.Controls.Add(Me.HardcoreCheckBox)
		Me.GameModePanel.Controls.Add(Me.CustomCheckBox)
		Me.GameModePanel.Location = New System.Drawing.Point(22, 26)
		Me.GameModePanel.Name = "GameModePanel"
		Me.GameModePanel.Size = New System.Drawing.Size(150, 86)
		Me.GameModePanel.TabIndex = 14
		'
		'CustomModelsPanel
		'
		Me.CustomModelsPanel.Controls.Add(Me.CharactersCheckBox)
		Me.CustomModelsPanel.Controls.Add(Me.PropsCheckBox)
		Me.CustomModelsPanel.Controls.Add(Me.WeaponsCheckBox)
		Me.CustomModelsPanel.Enabled = False
		Me.CustomModelsPanel.Location = New System.Drawing.Point(22, 141)
		Me.CustomModelsPanel.Name = "CustomModelsPanel"
		Me.CustomModelsPanel.Size = New System.Drawing.Size(150, 63)
		Me.CustomModelsPanel.TabIndex = 0
		'
		'CustomSoundsPanel
		'
		Me.CustomSoundsPanel.Controls.Add(Me.CharacterSoundsCheckBox)
		Me.CustomSoundsPanel.Controls.Add(Me.WeaponSoundsCheckBox)
		Me.CustomSoundsPanel.Enabled = False
		Me.CustomSoundsPanel.Location = New System.Drawing.Point(22, 233)
		Me.CustomSoundsPanel.Name = "CustomSoundsPanel"
		Me.CustomSoundsPanel.Size = New System.Drawing.Size(150, 40)
		Me.CustomSoundsPanel.TabIndex = 0
		'
		'MiscellaneousPanel
		'
		Me.MiscellaneousPanel.Controls.Add(Me.GUIsCheckBox)
		Me.MiscellaneousPanel.Controls.Add(Me.ModelPackCheckBox)
		Me.MiscellaneousPanel.Controls.Add(Me.SoundPackCheckBox)
		Me.MiscellaneousPanel.Enabled = False
		Me.MiscellaneousPanel.Location = New System.Drawing.Point(22, 302)
		Me.MiscellaneousPanel.Name = "MiscellaneousPanel"
		Me.MiscellaneousPanel.Size = New System.Drawing.Size(150, 63)
		Me.MiscellaneousPanel.TabIndex = 0
		'
		'ZombiePanicSourceTagsUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoScroll = True
		Me.Controls.Add(Me.CustomModelsPanel)
		Me.Controls.Add(Me.CustomSoundsPanel)
		Me.Controls.Add(Me.MiscellaneousPanel)
		Me.Controls.Add(Me.GameModePanel)
		Me.Controls.Add(Me.MiscellaneousRadioButton)
		Me.Controls.Add(Me.CustomSoundsRadioButton)
		Me.Controls.Add(Me.CustomModelsRadioButton)
		Me.Controls.Add(Me.GameModeRadioButton)
		Me.Name = "ZombiePanicSourceTagsUserControl"
		Me.Size = New System.Drawing.Size(350, 481)
		Me.GameModePanel.ResumeLayout(False)
		Me.GameModePanel.PerformLayout()
		Me.CustomModelsPanel.ResumeLayout(False)
		Me.CustomModelsPanel.PerformLayout()
		Me.CustomSoundsPanel.ResumeLayout(False)
		Me.CustomSoundsPanel.PerformLayout()
		Me.MiscellaneousPanel.ResumeLayout(False)
		Me.MiscellaneousPanel.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents CustomCheckBox As CheckBoxEx
	Friend WithEvents HardcoreCheckBox As CheckBoxEx
	Friend WithEvents ObjectiveCheckBox As CheckBoxEx
	Friend WithEvents SurvivalCheckBox As CheckBoxEx
	Friend WithEvents GameModeRadioButton As RadioButton
	Friend WithEvents GUIsCheckBox As CheckBoxEx
	Friend WithEvents WeaponsCheckBox As CheckBoxEx
	Friend WithEvents PropsCheckBox As CheckBoxEx
	Friend WithEvents CharactersCheckBox As CheckBoxEx
	Friend WithEvents CustomModelsRadioButton As RadioButton
	Friend WithEvents SoundPackCheckBox As CheckBoxEx
	Friend WithEvents ModelPackCheckBox As CheckBoxEx
	Friend WithEvents WeaponSoundsCheckBox As CheckBoxEx
	Friend WithEvents CharacterSoundsCheckBox As CheckBoxEx
	Friend WithEvents CustomSoundsRadioButton As RadioButton
	Friend WithEvents MiscellaneousRadioButton As RadioButton
	Friend WithEvents GameModePanel As Panel
	Friend WithEvents CustomModelsPanel As Panel
	Friend WithEvents CustomSoundsPanel As Panel
	Friend WithEvents MiscellaneousPanel As Panel
End Class
