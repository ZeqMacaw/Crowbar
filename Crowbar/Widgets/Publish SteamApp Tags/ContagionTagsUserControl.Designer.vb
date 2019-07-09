<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ContagionTagsUserControl
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
		Me.EscapeCoopCheckBox = New CheckBoxEx()
		Me.ExtractionCoopCheckBox = New CheckBoxEx()
		Me.HuntedPvpCheckBox = New CheckBoxEx()
		Me.ContagionPanicClassicCheckBox = New CheckBoxEx()
		Me.ContagionPanicObjectiveCheckBox = New CheckBoxEx()
		Me.WeaponsModelTextureCheckBox = New CheckBoxEx()
		Me.SurvivorsModelTextureCheckBox = New CheckBoxEx()
		Me.ZombiesModelTextureCheckBox = New CheckBoxEx()
		Me.UserInterfaceCheckBox = New CheckBoxEx()
		Me.SoundsCheckBox = New CheckBoxEx()
		Me.FlashlightCheckBox = New CheckBoxEx()
		Me.SmartphoneWallpapersCheckBox = New CheckBoxEx()
		Me.MiscCheckBox = New CheckBoxEx()
		Me.FlatlineCheckBox = New CheckBoxEx()
		Me.SuspendLayout()
		'
		'EscapeCoopCheckBox
		'
		Me.EscapeCoopCheckBox.AutoSize = True
		Me.EscapeCoopCheckBox.Location = New System.Drawing.Point(3, 3)
		Me.EscapeCoopCheckBox.Name = "EscapeCoopCheckBox"
		Me.EscapeCoopCheckBox.Size = New System.Drawing.Size(100, 17)
		Me.EscapeCoopCheckBox.TabIndex = 0
		Me.EscapeCoopCheckBox.Text = "Escape (Co-op)"
		Me.EscapeCoopCheckBox.Tag = "Escape (Co-op)"
		Me.EscapeCoopCheckBox.UseVisualStyleBackColor = True
		'
		'ExtractionCoopCheckBox
		'
		Me.ExtractionCoopCheckBox.AutoSize = True
		Me.ExtractionCoopCheckBox.Location = New System.Drawing.Point(3, 26)
		Me.ExtractionCoopCheckBox.Name = "ExtractionCoopCheckBox"
		Me.ExtractionCoopCheckBox.Size = New System.Drawing.Size(115, 17)
		Me.ExtractionCoopCheckBox.TabIndex = 1
		Me.ExtractionCoopCheckBox.Text = "Extraction (Co-op)"
		Me.ExtractionCoopCheckBox.Tag = "Extraction (Co-op)"
		Me.ExtractionCoopCheckBox.UseVisualStyleBackColor = True
		'
		'HuntedPvpCheckBox
		'
		Me.HuntedPvpCheckBox.AutoSize = True
		Me.HuntedPvpCheckBox.Location = New System.Drawing.Point(3, 49)
		Me.HuntedPvpCheckBox.Name = "HuntedPvpCheckBox"
		Me.HuntedPvpCheckBox.Size = New System.Drawing.Size(90, 17)
		Me.HuntedPvpCheckBox.TabIndex = 2
		Me.HuntedPvpCheckBox.Text = "Hunted (PVP)"
		Me.HuntedPvpCheckBox.Tag = "Hunted (PVP)"
		Me.HuntedPvpCheckBox.UseVisualStyleBackColor = True
		'
		'ContagionPanicClassicCheckBox
		'
		Me.ContagionPanicClassicCheckBox.AutoSize = True
		Me.ContagionPanicClassicCheckBox.Location = New System.Drawing.Point(3, 95)
		Me.ContagionPanicClassicCheckBox.Name = "ContagionPanicClassicCheckBox"
		Me.ContagionPanicClassicCheckBox.Size = New System.Drawing.Size(169, 17)
		Me.ContagionPanicClassicCheckBox.TabIndex = 4
		Me.ContagionPanicClassicCheckBox.Text = "Contagion Panic Classic (CPC)"
		Me.ContagionPanicClassicCheckBox.Tag = "Contagion Panic Classic (CPC)"
		Me.ContagionPanicClassicCheckBox.UseVisualStyleBackColor = True
		'
		'ContagionPanicObjectiveCheckBox
		'
		Me.ContagionPanicObjectiveCheckBox.AutoSize = True
		Me.ContagionPanicObjectiveCheckBox.Location = New System.Drawing.Point(3, 118)
		Me.ContagionPanicObjectiveCheckBox.Name = "ContagionPanicObjectiveCheckBox"
		Me.ContagionPanicObjectiveCheckBox.Size = New System.Drawing.Size(184, 17)
		Me.ContagionPanicObjectiveCheckBox.TabIndex = 5
		Me.ContagionPanicObjectiveCheckBox.Text = "Contagion Panic Objective (CPO)"
		Me.ContagionPanicObjectiveCheckBox.Tag = "Contagion Panic Objective (CPO)"
		Me.ContagionPanicObjectiveCheckBox.UseVisualStyleBackColor = True
		'
		'WeaponsModelTextureCheckBox
		'
		Me.WeaponsModelTextureCheckBox.AutoSize = True
		Me.WeaponsModelTextureCheckBox.Location = New System.Drawing.Point(3, 141)
		Me.WeaponsModelTextureCheckBox.Name = "WeaponsModelTextureCheckBox"
		Me.WeaponsModelTextureCheckBox.Size = New System.Drawing.Size(155, 17)
		Me.WeaponsModelTextureCheckBox.TabIndex = 6
		Me.WeaponsModelTextureCheckBox.Text = "Weapons (Model/Texture) "
		Me.WeaponsModelTextureCheckBox.Tag = "Weapons (Model/Texture) "
		Me.WeaponsModelTextureCheckBox.UseVisualStyleBackColor = True
		'
		'SurvivorsModelTextureCheckBox
		'
		Me.SurvivorsModelTextureCheckBox.AutoSize = True
		Me.SurvivorsModelTextureCheckBox.Location = New System.Drawing.Point(3, 164)
		Me.SurvivorsModelTextureCheckBox.Name = "SurvivorsModelTextureCheckBox"
		Me.SurvivorsModelTextureCheckBox.Size = New System.Drawing.Size(155, 17)
		Me.SurvivorsModelTextureCheckBox.TabIndex = 7
		Me.SurvivorsModelTextureCheckBox.Text = "Survivors (Model/Texture) "
		Me.SurvivorsModelTextureCheckBox.Tag = "Survivors (Model/Texture) "
		Me.SurvivorsModelTextureCheckBox.UseVisualStyleBackColor = True
		'
		'ZombiesModelTextureCheckBox
		'
		Me.ZombiesModelTextureCheckBox.AutoSize = True
		Me.ZombiesModelTextureCheckBox.Location = New System.Drawing.Point(3, 187)
		Me.ZombiesModelTextureCheckBox.Name = "ZombiesModelTextureCheckBox"
		Me.ZombiesModelTextureCheckBox.Size = New System.Drawing.Size(146, 17)
		Me.ZombiesModelTextureCheckBox.TabIndex = 8
		Me.ZombiesModelTextureCheckBox.Text = "Zombies (Model/Texture)"
		Me.ZombiesModelTextureCheckBox.Tag = "Zombies (Model/Texture)"
		Me.ZombiesModelTextureCheckBox.UseVisualStyleBackColor = True
		'
		'UserInterfaceCheckBox
		'
		Me.UserInterfaceCheckBox.AutoSize = True
		Me.UserInterfaceCheckBox.Location = New System.Drawing.Point(3, 210)
		Me.UserInterfaceCheckBox.Name = "UserInterfaceCheckBox"
		Me.UserInterfaceCheckBox.Size = New System.Drawing.Size(96, 17)
		Me.UserInterfaceCheckBox.TabIndex = 9
		Me.UserInterfaceCheckBox.Text = "User Interface"
		Me.UserInterfaceCheckBox.Tag = "User Interface"
		Me.UserInterfaceCheckBox.UseVisualStyleBackColor = True
		'
		'SoundsCheckBox
		'
		Me.SoundsCheckBox.AutoSize = True
		Me.SoundsCheckBox.Location = New System.Drawing.Point(3, 233)
		Me.SoundsCheckBox.Name = "SoundsCheckBox"
		Me.SoundsCheckBox.Size = New System.Drawing.Size(61, 17)
		Me.SoundsCheckBox.TabIndex = 10
		Me.SoundsCheckBox.Text = "Sounds"
		Me.SoundsCheckBox.Tag = "Sounds"
		Me.SoundsCheckBox.UseVisualStyleBackColor = True
		'
		'FlashlightCheckBox
		'
		Me.FlashlightCheckBox.AutoSize = True
		Me.FlashlightCheckBox.Location = New System.Drawing.Point(3, 256)
		Me.FlashlightCheckBox.Name = "FlashlightCheckBox"
		Me.FlashlightCheckBox.Size = New System.Drawing.Size(71, 17)
		Me.FlashlightCheckBox.TabIndex = 11
		Me.FlashlightCheckBox.Text = "Flashlight"
		Me.FlashlightCheckBox.Tag = "Flashlight"
		Me.FlashlightCheckBox.UseVisualStyleBackColor = True
		'
		'SmartphoneWallpapersCheckBox
		'
		Me.SmartphoneWallpapersCheckBox.AutoSize = True
		Me.SmartphoneWallpapersCheckBox.Location = New System.Drawing.Point(3, 279)
		Me.SmartphoneWallpapersCheckBox.Name = "SmartphoneWallpapersCheckBox"
		Me.SmartphoneWallpapersCheckBox.Size = New System.Drawing.Size(140, 17)
		Me.SmartphoneWallpapersCheckBox.TabIndex = 12
		Me.SmartphoneWallpapersCheckBox.Text = "Smartphone Wallpapers"
		Me.SmartphoneWallpapersCheckBox.Tag = "Smartphone Wallpapers"
		Me.SmartphoneWallpapersCheckBox.UseVisualStyleBackColor = True
		'
		'MiscCheckBox
		'
		Me.MiscCheckBox.AutoSize = True
		Me.MiscCheckBox.Location = New System.Drawing.Point(3, 302)
		Me.MiscCheckBox.Name = "MiscCheckBox"
		Me.MiscCheckBox.Size = New System.Drawing.Size(50, 17)
		Me.MiscCheckBox.TabIndex = 13
		Me.MiscCheckBox.Text = "Misc."
		Me.MiscCheckBox.Tag = "Misc"
		Me.MiscCheckBox.UseVisualStyleBackColor = True
		'
		'FlatlineCheckBox
		'
		Me.FlatlineCheckBox.AutoSize = True
		Me.FlatlineCheckBox.Location = New System.Drawing.Point(3, 72)
		Me.FlatlineCheckBox.Name = "FlatlineCheckBox"
		Me.FlatlineCheckBox.Size = New System.Drawing.Size(60, 17)
		Me.FlatlineCheckBox.TabIndex = 3
		Me.FlatlineCheckBox.Text = "Flatline"
		Me.FlatlineCheckBox.Tag = "Flatline"
		Me.FlatlineCheckBox.UseVisualStyleBackColor = True
		'
		'ContagionTagsUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoScroll = True
		Me.Controls.Add(Me.FlatlineCheckBox)
		Me.Controls.Add(Me.MiscCheckBox)
		Me.Controls.Add(Me.SmartphoneWallpapersCheckBox)
		Me.Controls.Add(Me.FlashlightCheckBox)
		Me.Controls.Add(Me.SoundsCheckBox)
		Me.Controls.Add(Me.UserInterfaceCheckBox)
		Me.Controls.Add(Me.ZombiesModelTextureCheckBox)
		Me.Controls.Add(Me.SurvivorsModelTextureCheckBox)
		Me.Controls.Add(Me.WeaponsModelTextureCheckBox)
		Me.Controls.Add(Me.ContagionPanicObjectiveCheckBox)
		Me.Controls.Add(Me.ContagionPanicClassicCheckBox)
		Me.Controls.Add(Me.HuntedPvpCheckBox)
		Me.Controls.Add(Me.ExtractionCoopCheckBox)
		Me.Controls.Add(Me.EscapeCoopCheckBox)
		Me.Name = "ContagionTagsUserControl"
		Me.Size = New System.Drawing.Size(188, 351)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents EscapeCoopCheckBox As CheckBoxEx
	Friend WithEvents ExtractionCoopCheckBox As CheckBoxEx
	Friend WithEvents HuntedPvpCheckBox As CheckBoxEx
	Friend WithEvents ContagionPanicClassicCheckBox As CheckBoxEx
	Friend WithEvents ContagionPanicObjectiveCheckBox As CheckBoxEx
	Friend WithEvents WeaponsModelTextureCheckBox As CheckBoxEx
	Friend WithEvents SurvivorsModelTextureCheckBox As CheckBoxEx
	Friend WithEvents ZombiesModelTextureCheckBox As CheckBoxEx
	Friend WithEvents UserInterfaceCheckBox As CheckBoxEx
	Friend WithEvents SoundsCheckBox As CheckBoxEx
	Friend WithEvents FlashlightCheckBox As CheckBoxEx
	Friend WithEvents SmartphoneWallpapersCheckBox As CheckBoxEx
	Friend WithEvents MiscCheckBox As CheckBoxEx
	Friend WithEvents FlatlineCheckBox As CheckBoxEx
End Class
