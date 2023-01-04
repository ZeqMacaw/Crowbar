<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MilitaryConflictVietnamTagsUserControl
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
		Me.ModelTagsGroupBox = New System.Windows.Forms.GroupBox()
		Me.WeaponsCheckBox = New Crowbar.CheckBoxEx()
		Me.DeathmatchCheckBox = New Crowbar.CheckBoxEx()
		Me.ConquestCheckBox = New Crowbar.CheckBoxEx()
		Me.CaptureTheFlagCheckBox = New Crowbar.CheckBoxEx()
		Me.MapTagsGroupBox = New System.Windows.Forms.GroupBox()
		Me.ItemsCheckBox = New Crowbar.CheckBoxEx()
		Me.MapsCheckBox = New Crowbar.CheckBoxEx()
		Me.ModelsCheckBox = New Crowbar.CheckBoxEx()
		Me.ScriptsCheckBox = New Crowbar.CheckBoxEx()
		Me.SoundsCheckBox = New Crowbar.CheckBoxEx()
		Me.MapsPortCheckBox = New Crowbar.CheckBoxEx()
		Me.MiscellaneousCheckBox = New Crowbar.CheckBoxEx()
		Me.UICheckBox = New Crowbar.CheckBoxEx()
		Me.TexturesCheckBox = New Crowbar.CheckBoxEx()
		Me.TeamDeathmatchCheckBox = New Crowbar.CheckBoxEx()
		Me.GunGameCheckBox = New Crowbar.CheckBoxEx()
		Me.GunGameDeathmatchCheckBox = New Crowbar.CheckBoxEx()
		Me.CustomCheckBox = New Crowbar.CheckBoxEx()
		Me.ModelTagsGroupBox.SuspendLayout()
		Me.MapTagsGroupBox.SuspendLayout()
		Me.SuspendLayout()
		'
		'ModelTagsGroupBox
		'
		Me.ModelTagsGroupBox.Controls.Add(Me.CustomCheckBox)
		Me.ModelTagsGroupBox.Controls.Add(Me.GunGameDeathmatchCheckBox)
		Me.ModelTagsGroupBox.Controls.Add(Me.GunGameCheckBox)
		Me.ModelTagsGroupBox.Controls.Add(Me.TeamDeathmatchCheckBox)
		Me.ModelTagsGroupBox.Controls.Add(Me.DeathmatchCheckBox)
		Me.ModelTagsGroupBox.Controls.Add(Me.ConquestCheckBox)
		Me.ModelTagsGroupBox.Controls.Add(Me.CaptureTheFlagCheckBox)
		Me.ModelTagsGroupBox.Location = New System.Drawing.Point(123, 3)
		Me.ModelTagsGroupBox.Name = "ModelTagsGroupBox"
		Me.ModelTagsGroupBox.Size = New System.Drawing.Size(161, 253)
		Me.ModelTagsGroupBox.TabIndex = 3
		Me.ModelTagsGroupBox.TabStop = False
		Me.ModelTagsGroupBox.Text = "Game Modes"
		'
		'WeaponsCheckBox
		'
		Me.WeaponsCheckBox.AutoSize = True
		Me.WeaponsCheckBox.IsReadOnly = False
		Me.WeaponsCheckBox.Location = New System.Drawing.Point(6, 43)
		Me.WeaponsCheckBox.Name = "WeaponsCheckBox"
		Me.WeaponsCheckBox.Size = New System.Drawing.Size(75, 17)
		Me.WeaponsCheckBox.TabIndex = 3
		Me.WeaponsCheckBox.Tag = "Weapons"
		Me.WeaponsCheckBox.Text = "Weapons"
		Me.WeaponsCheckBox.UseVisualStyleBackColor = True
		'
		'DeathmatchCheckBox
		'
		Me.DeathmatchCheckBox.AutoSize = True
		Me.DeathmatchCheckBox.IsReadOnly = False
		Me.DeathmatchCheckBox.Location = New System.Drawing.Point(6, 66)
		Me.DeathmatchCheckBox.Name = "DeathmatchCheckBox"
		Me.DeathmatchCheckBox.Size = New System.Drawing.Size(88, 17)
		Me.DeathmatchCheckBox.TabIndex = 2
		Me.DeathmatchCheckBox.Tag = "Deathmatch"
		Me.DeathmatchCheckBox.Text = "Deathmatch"
		Me.DeathmatchCheckBox.UseVisualStyleBackColor = True
		'
		'ConquestCheckBox
		'
		Me.ConquestCheckBox.AutoSize = True
		Me.ConquestCheckBox.IsReadOnly = False
		Me.ConquestCheckBox.Location = New System.Drawing.Point(6, 43)
		Me.ConquestCheckBox.Name = "ConquestCheckBox"
		Me.ConquestCheckBox.Size = New System.Drawing.Size(76, 17)
		Me.ConquestCheckBox.TabIndex = 1
		Me.ConquestCheckBox.Tag = "Conquest"
		Me.ConquestCheckBox.Text = "Conquest"
		Me.ConquestCheckBox.UseVisualStyleBackColor = True
		'
		'CaptureTheFlagCheckBox
		'
		Me.CaptureTheFlagCheckBox.AutoSize = True
		Me.CaptureTheFlagCheckBox.IsReadOnly = False
		Me.CaptureTheFlagCheckBox.Location = New System.Drawing.Point(6, 20)
		Me.CaptureTheFlagCheckBox.Name = "CaptureTheFlagCheckBox"
		Me.CaptureTheFlagCheckBox.Size = New System.Drawing.Size(114, 17)
		Me.CaptureTheFlagCheckBox.TabIndex = 0
		Me.CaptureTheFlagCheckBox.Tag = "Capture The Flag"
		Me.CaptureTheFlagCheckBox.Text = "Capture The Flag"
		Me.CaptureTheFlagCheckBox.UseVisualStyleBackColor = True
		'
		'MapTagsGroupBox
		'
		Me.MapTagsGroupBox.Controls.Add(Me.MiscellaneousCheckBox)
		Me.MapTagsGroupBox.Controls.Add(Me.UICheckBox)
		Me.MapTagsGroupBox.Controls.Add(Me.TexturesCheckBox)
		Me.MapTagsGroupBox.Controls.Add(Me.MapsPortCheckBox)
		Me.MapTagsGroupBox.Controls.Add(Me.ModelsCheckBox)
		Me.MapTagsGroupBox.Controls.Add(Me.ScriptsCheckBox)
		Me.MapTagsGroupBox.Controls.Add(Me.SoundsCheckBox)
		Me.MapTagsGroupBox.Controls.Add(Me.WeaponsCheckBox)
		Me.MapTagsGroupBox.Controls.Add(Me.ItemsCheckBox)
		Me.MapTagsGroupBox.Controls.Add(Me.MapsCheckBox)
		Me.MapTagsGroupBox.Location = New System.Drawing.Point(3, 3)
		Me.MapTagsGroupBox.Name = "MapTagsGroupBox"
		Me.MapTagsGroupBox.Size = New System.Drawing.Size(114, 253)
		Me.MapTagsGroupBox.TabIndex = 2
		Me.MapTagsGroupBox.TabStop = False
		Me.MapTagsGroupBox.Text = "Game Content"
		'
		'ItemsCheckBox
		'
		Me.ItemsCheckBox.AutoSize = True
		Me.ItemsCheckBox.IsReadOnly = False
		Me.ItemsCheckBox.Location = New System.Drawing.Point(6, 66)
		Me.ItemsCheckBox.Name = "ItemsCheckBox"
		Me.ItemsCheckBox.Size = New System.Drawing.Size(53, 17)
		Me.ItemsCheckBox.TabIndex = 1
		Me.ItemsCheckBox.Tag = "Items"
		Me.ItemsCheckBox.Text = "Items"
		Me.ItemsCheckBox.UseVisualStyleBackColor = True
		'
		'MapsCheckBox
		'
		Me.MapsCheckBox.AutoSize = True
		Me.MapsCheckBox.IsReadOnly = False
		Me.MapsCheckBox.Location = New System.Drawing.Point(6, 20)
		Me.MapsCheckBox.Name = "MapsCheckBox"
		Me.MapsCheckBox.Size = New System.Drawing.Size(54, 17)
		Me.MapsCheckBox.TabIndex = 0
		Me.MapsCheckBox.Tag = "Maps"
		Me.MapsCheckBox.Text = "Maps"
		Me.MapsCheckBox.UseVisualStyleBackColor = True
		'
		'ModelsCheckBox
		'
		Me.ModelsCheckBox.AutoSize = True
		Me.ModelsCheckBox.IsReadOnly = False
		Me.ModelsCheckBox.Location = New System.Drawing.Point(6, 135)
		Me.ModelsCheckBox.Name = "ModelsCheckBox"
		Me.ModelsCheckBox.Size = New System.Drawing.Size(64, 17)
		Me.ModelsCheckBox.TabIndex = 6
		Me.ModelsCheckBox.Tag = "Models"
		Me.ModelsCheckBox.Text = "Models"
		Me.ModelsCheckBox.UseVisualStyleBackColor = True
		'
		'ScriptsCheckBox
		'
		Me.ScriptsCheckBox.AutoSize = True
		Me.ScriptsCheckBox.IsReadOnly = False
		Me.ScriptsCheckBox.Location = New System.Drawing.Point(6, 112)
		Me.ScriptsCheckBox.Name = "ScriptsCheckBox"
		Me.ScriptsCheckBox.Size = New System.Drawing.Size(60, 17)
		Me.ScriptsCheckBox.TabIndex = 5
		Me.ScriptsCheckBox.Tag = "Scripts"
		Me.ScriptsCheckBox.Text = "Scripts"
		Me.ScriptsCheckBox.UseVisualStyleBackColor = True
		'
		'SoundsCheckBox
		'
		Me.SoundsCheckBox.AutoSize = True
		Me.SoundsCheckBox.IsReadOnly = False
		Me.SoundsCheckBox.Location = New System.Drawing.Point(6, 89)
		Me.SoundsCheckBox.Name = "SoundsCheckBox"
		Me.SoundsCheckBox.Size = New System.Drawing.Size(65, 17)
		Me.SoundsCheckBox.TabIndex = 4
		Me.SoundsCheckBox.Tag = "Sounds"
		Me.SoundsCheckBox.Text = "Sounds"
		Me.SoundsCheckBox.UseVisualStyleBackColor = True
		'
		'MapsPortCheckBox
		'
		Me.MapsPortCheckBox.AutoSize = True
		Me.MapsPortCheckBox.IsReadOnly = False
		Me.MapsPortCheckBox.Location = New System.Drawing.Point(6, 227)
		Me.MapsPortCheckBox.Name = "MapsPortCheckBox"
		Me.MapsPortCheckBox.Size = New System.Drawing.Size(90, 17)
		Me.MapsPortCheckBox.TabIndex = 7
		Me.MapsPortCheckBox.Tag = "Maps ( Port )"
		Me.MapsPortCheckBox.Text = "Maps ( Port )"
		Me.MapsPortCheckBox.UseVisualStyleBackColor = True
		'
		'MiscellaneousCheckBox
		'
		Me.MiscellaneousCheckBox.AutoSize = True
		Me.MiscellaneousCheckBox.IsReadOnly = False
		Me.MiscellaneousCheckBox.Location = New System.Drawing.Point(6, 204)
		Me.MiscellaneousCheckBox.Name = "MiscellaneousCheckBox"
		Me.MiscellaneousCheckBox.Size = New System.Drawing.Size(99, 17)
		Me.MiscellaneousCheckBox.TabIndex = 10
		Me.MiscellaneousCheckBox.Tag = "Miscellaneous"
		Me.MiscellaneousCheckBox.Text = "Miscellaneous"
		Me.MiscellaneousCheckBox.UseVisualStyleBackColor = True
		'
		'UICheckBox
		'
		Me.UICheckBox.AutoSize = True
		Me.UICheckBox.IsReadOnly = False
		Me.UICheckBox.Location = New System.Drawing.Point(6, 181)
		Me.UICheckBox.Name = "UICheckBox"
		Me.UICheckBox.Size = New System.Drawing.Size(37, 17)
		Me.UICheckBox.TabIndex = 9
		Me.UICheckBox.Tag = "UI"
		Me.UICheckBox.Text = "UI"
		Me.UICheckBox.UseVisualStyleBackColor = True
		'
		'TexturesCheckBox
		'
		Me.TexturesCheckBox.AutoSize = True
		Me.TexturesCheckBox.IsReadOnly = False
		Me.TexturesCheckBox.Location = New System.Drawing.Point(6, 158)
		Me.TexturesCheckBox.Name = "TexturesCheckBox"
		Me.TexturesCheckBox.Size = New System.Drawing.Size(68, 17)
		Me.TexturesCheckBox.TabIndex = 8
		Me.TexturesCheckBox.Tag = "Textures"
		Me.TexturesCheckBox.Text = "Textures"
		Me.TexturesCheckBox.UseVisualStyleBackColor = True
		'
		'TeamDeathmatchCheckBox
		'
		Me.TeamDeathmatchCheckBox.AutoSize = True
		Me.TeamDeathmatchCheckBox.IsReadOnly = False
		Me.TeamDeathmatchCheckBox.Location = New System.Drawing.Point(6, 89)
		Me.TeamDeathmatchCheckBox.Name = "TeamDeathmatchCheckBox"
		Me.TeamDeathmatchCheckBox.Size = New System.Drawing.Size(117, 17)
		Me.TeamDeathmatchCheckBox.TabIndex = 3
		Me.TeamDeathmatchCheckBox.Tag = "Team Deathmatch"
		Me.TeamDeathmatchCheckBox.Text = "Team Deathmatch"
		Me.TeamDeathmatchCheckBox.UseVisualStyleBackColor = True
		'
		'GunGameCheckBox
		'
		Me.GunGameCheckBox.AutoSize = True
		Me.GunGameCheckBox.IsReadOnly = False
		Me.GunGameCheckBox.Location = New System.Drawing.Point(6, 112)
		Me.GunGameCheckBox.Name = "GunGameCheckBox"
		Me.GunGameCheckBox.Size = New System.Drawing.Size(80, 17)
		Me.GunGameCheckBox.TabIndex = 4
		Me.GunGameCheckBox.Tag = "Gun Game"
		Me.GunGameCheckBox.Text = "Gun Game"
		Me.GunGameCheckBox.UseVisualStyleBackColor = True
		'
		'GunGameDeathmatchCheckBox
		'
		Me.GunGameDeathmatchCheckBox.AutoSize = True
		Me.GunGameDeathmatchCheckBox.IsReadOnly = False
		Me.GunGameDeathmatchCheckBox.Location = New System.Drawing.Point(6, 135)
		Me.GunGameDeathmatchCheckBox.Name = "GunGameDeathmatchCheckBox"
		Me.GunGameDeathmatchCheckBox.Size = New System.Drawing.Size(145, 17)
		Me.GunGameDeathmatchCheckBox.TabIndex = 5
		Me.GunGameDeathmatchCheckBox.Tag = "Gun Game Deathmatch"
		Me.GunGameDeathmatchCheckBox.Text = "Gun Game Deathmatch"
		Me.GunGameDeathmatchCheckBox.UseVisualStyleBackColor = True
		'
		'CustomCheckBox
		'
		Me.CustomCheckBox.AutoSize = True
		Me.CustomCheckBox.IsReadOnly = False
		Me.CustomCheckBox.Location = New System.Drawing.Point(6, 158)
		Me.CustomCheckBox.Name = "CustomCheckBox"
		Me.CustomCheckBox.Size = New System.Drawing.Size(65, 17)
		Me.CustomCheckBox.TabIndex = 6
		Me.CustomCheckBox.Tag = "Custom"
		Me.CustomCheckBox.Text = "Custom"
		Me.CustomCheckBox.UseVisualStyleBackColor = True
		'
		'MilitaryConflictVietnamTagsUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.ModelTagsGroupBox)
		Me.Controls.Add(Me.MapTagsGroupBox)
		Me.Name = "MilitaryConflictVietnamTagsUserControl"
		Me.Size = New System.Drawing.Size(334, 457)
		Me.ModelTagsGroupBox.ResumeLayout(False)
		Me.ModelTagsGroupBox.PerformLayout()
		Me.MapTagsGroupBox.ResumeLayout(False)
		Me.MapTagsGroupBox.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents ModelTagsGroupBox As GroupBox
	Friend WithEvents DeathmatchCheckBox As CheckBoxEx
	Friend WithEvents ConquestCheckBox As CheckBoxEx
	Friend WithEvents CaptureTheFlagCheckBox As CheckBoxEx
	Friend WithEvents WeaponsCheckBox As CheckBoxEx
	Friend WithEvents MapTagsGroupBox As GroupBox
	Friend WithEvents ItemsCheckBox As CheckBoxEx
	Friend WithEvents MapsCheckBox As CheckBoxEx
	Friend WithEvents MiscellaneousCheckBox As CheckBoxEx
	Friend WithEvents UICheckBox As CheckBoxEx
	Friend WithEvents TexturesCheckBox As CheckBoxEx
	Friend WithEvents MapsPortCheckBox As CheckBoxEx
	Friend WithEvents ModelsCheckBox As CheckBoxEx
	Friend WithEvents ScriptsCheckBox As CheckBoxEx
	Friend WithEvents SoundsCheckBox As CheckBoxEx
	Friend WithEvents CustomCheckBox As CheckBoxEx
	Friend WithEvents GunGameDeathmatchCheckBox As CheckBoxEx
	Friend WithEvents GunGameCheckBox As CheckBoxEx
	Friend WithEvents TeamDeathmatchCheckBox As CheckBoxEx
End Class
