<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BlackMesaTagsUserControl
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
		Me.MapTagsGroupBox = New System.Windows.Forms.GroupBox()
		Me.MultiplayerTagCheckBox = New Crowbar.CheckBoxEx()
		Me.SingleplayerTagCheckBox = New Crowbar.CheckBoxEx()
		Me.ModelTagsGroupBox = New System.Windows.Forms.GroupBox()
		Me.WeaponTagCheckBox = New Crowbar.CheckBoxEx()
		Me.NPCTagCheckBox = New Crowbar.CheckBoxEx()
		Me.EnvironmentTagCheckBox = New Crowbar.CheckBoxEx()
		Me.CreatureTagCheckBox = New Crowbar.CheckBoxEx()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.ExtraTag1TextBox = New System.Windows.Forms.TextBox()
		Me.ExtraTag2TextBox = New System.Windows.Forms.TextBox()
		Me.ExtraTag3TextBox = New System.Windows.Forms.TextBox()
		Me.ExtraTag4TextBox = New System.Windows.Forms.TextBox()
		Me.ExtraTag5TextBox = New System.Windows.Forms.TextBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.MapTagsGroupBox.SuspendLayout()
		Me.ModelTagsGroupBox.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'MapTagsGroupBox
		'
		Me.MapTagsGroupBox.Controls.Add(Me.MultiplayerTagCheckBox)
		Me.MapTagsGroupBox.Controls.Add(Me.SingleplayerTagCheckBox)
		Me.MapTagsGroupBox.Location = New System.Drawing.Point(3, 0)
		Me.MapTagsGroupBox.Name = "MapTagsGroupBox"
		Me.MapTagsGroupBox.Size = New System.Drawing.Size(114, 77)
		Me.MapTagsGroupBox.TabIndex = 0
		Me.MapTagsGroupBox.TabStop = False
		Me.MapTagsGroupBox.Text = "Map"
		'
		'MultiplayerTagCheckBox
		'
		Me.MultiplayerTagCheckBox.AutoSize = True
		Me.MultiplayerTagCheckBox.IsReadOnly = False
		Me.MultiplayerTagCheckBox.Location = New System.Drawing.Point(6, 43)
		Me.MultiplayerTagCheckBox.Name = "MultiplayerTagCheckBox"
		Me.MultiplayerTagCheckBox.Size = New System.Drawing.Size(78, 17)
		Me.MultiplayerTagCheckBox.TabIndex = 1
		Me.MultiplayerTagCheckBox.Tag = "Multiplayer"
		Me.MultiplayerTagCheckBox.Text = "Multiplayer"
		Me.MultiplayerTagCheckBox.UseVisualStyleBackColor = True
		'
		'SingleplayerTagCheckBox
		'
		Me.SingleplayerTagCheckBox.AutoSize = True
		Me.SingleplayerTagCheckBox.IsReadOnly = False
		Me.SingleplayerTagCheckBox.Location = New System.Drawing.Point(6, 20)
		Me.SingleplayerTagCheckBox.Name = "SingleplayerTagCheckBox"
		Me.SingleplayerTagCheckBox.Size = New System.Drawing.Size(84, 17)
		Me.SingleplayerTagCheckBox.TabIndex = 0
		Me.SingleplayerTagCheckBox.Tag = "Singleplayer"
		Me.SingleplayerTagCheckBox.Text = "Singleplayer"
		Me.SingleplayerTagCheckBox.UseVisualStyleBackColor = True
		'
		'ModelTagsGroupBox
		'
		Me.ModelTagsGroupBox.Controls.Add(Me.WeaponTagCheckBox)
		Me.ModelTagsGroupBox.Controls.Add(Me.NPCTagCheckBox)
		Me.ModelTagsGroupBox.Controls.Add(Me.EnvironmentTagCheckBox)
		Me.ModelTagsGroupBox.Controls.Add(Me.CreatureTagCheckBox)
		Me.ModelTagsGroupBox.Location = New System.Drawing.Point(3, 83)
		Me.ModelTagsGroupBox.Name = "ModelTagsGroupBox"
		Me.ModelTagsGroupBox.Size = New System.Drawing.Size(114, 123)
		Me.ModelTagsGroupBox.TabIndex = 1
		Me.ModelTagsGroupBox.TabStop = False
		Me.ModelTagsGroupBox.Text = "Model"
		'
		'WeaponTagCheckBox
		'
		Me.WeaponTagCheckBox.AutoSize = True
		Me.WeaponTagCheckBox.IsReadOnly = False
		Me.WeaponTagCheckBox.Location = New System.Drawing.Point(6, 89)
		Me.WeaponTagCheckBox.Name = "WeaponTagCheckBox"
		Me.WeaponTagCheckBox.Size = New System.Drawing.Size(66, 17)
		Me.WeaponTagCheckBox.TabIndex = 3
		Me.WeaponTagCheckBox.Tag = "Weapon"
		Me.WeaponTagCheckBox.Text = "Weapon"
		Me.WeaponTagCheckBox.UseVisualStyleBackColor = True
		'
		'NPCTagCheckBox
		'
		Me.NPCTagCheckBox.AutoSize = True
		Me.NPCTagCheckBox.IsReadOnly = False
		Me.NPCTagCheckBox.Location = New System.Drawing.Point(6, 66)
		Me.NPCTagCheckBox.Name = "NPCTagCheckBox"
		Me.NPCTagCheckBox.Size = New System.Drawing.Size(46, 17)
		Me.NPCTagCheckBox.TabIndex = 2
		Me.NPCTagCheckBox.Tag = "NPC"
		Me.NPCTagCheckBox.Text = "NPC"
		Me.NPCTagCheckBox.UseVisualStyleBackColor = True
		'
		'EnvironmentTagCheckBox
		'
		Me.EnvironmentTagCheckBox.AutoSize = True
		Me.EnvironmentTagCheckBox.IsReadOnly = False
		Me.EnvironmentTagCheckBox.Location = New System.Drawing.Point(6, 43)
		Me.EnvironmentTagCheckBox.Name = "EnvironmentTagCheckBox"
		Me.EnvironmentTagCheckBox.Size = New System.Drawing.Size(86, 17)
		Me.EnvironmentTagCheckBox.TabIndex = 1
		Me.EnvironmentTagCheckBox.Tag = "Environment"
		Me.EnvironmentTagCheckBox.Text = "Environment"
		Me.EnvironmentTagCheckBox.UseVisualStyleBackColor = True
		'
		'CreatureTagCheckBox
		'
		Me.CreatureTagCheckBox.AutoSize = True
		Me.CreatureTagCheckBox.IsReadOnly = False
		Me.CreatureTagCheckBox.Location = New System.Drawing.Point(6, 20)
		Me.CreatureTagCheckBox.Name = "CreatureTagCheckBox"
		Me.CreatureTagCheckBox.Size = New System.Drawing.Size(69, 17)
		Me.CreatureTagCheckBox.TabIndex = 0
		Me.CreatureTagCheckBox.Tag = "Creature"
		Me.CreatureTagCheckBox.Text = "Creature"
		Me.CreatureTagCheckBox.UseVisualStyleBackColor = True
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.ExtraTag1TextBox)
		Me.GroupBox1.Controls.Add(Me.ExtraTag2TextBox)
		Me.GroupBox1.Controls.Add(Me.ExtraTag3TextBox)
		Me.GroupBox1.Controls.Add(Me.ExtraTag4TextBox)
		Me.GroupBox1.Controls.Add(Me.ExtraTag5TextBox)
		Me.GroupBox1.Controls.Add(Me.Label5)
		Me.GroupBox1.Controls.Add(Me.Label4)
		Me.GroupBox1.Controls.Add(Me.Label3)
		Me.GroupBox1.Controls.Add(Me.Label2)
		Me.GroupBox1.Controls.Add(Me.Label1)
		Me.GroupBox1.Location = New System.Drawing.Point(123, 0)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(167, 206)
		Me.GroupBox1.TabIndex = 1
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Extras"
		'
		'ExtraTag1TextBox
		'
		Me.ExtraTag1TextBox.Location = New System.Drawing.Point(58, 20)
		Me.ExtraTag1TextBox.Name = "ExtraTag1TextBox"
		Me.ExtraTag1TextBox.Size = New System.Drawing.Size(100, 21)
		Me.ExtraTag1TextBox.TabIndex = 1
		Me.ExtraTag1TextBox.Tag = "TagsEnabled"
		'
		'ExtraTag2TextBox
		'
		Me.ExtraTag2TextBox.Location = New System.Drawing.Point(58, 47)
		Me.ExtraTag2TextBox.Name = "ExtraTag2TextBox"
		Me.ExtraTag2TextBox.Size = New System.Drawing.Size(100, 21)
		Me.ExtraTag2TextBox.TabIndex = 3
		Me.ExtraTag2TextBox.Tag = "TagsEnabled"
		'
		'ExtraTag3TextBox
		'
		Me.ExtraTag3TextBox.Location = New System.Drawing.Point(58, 74)
		Me.ExtraTag3TextBox.Name = "ExtraTag3TextBox"
		Me.ExtraTag3TextBox.Size = New System.Drawing.Size(100, 21)
		Me.ExtraTag3TextBox.TabIndex = 5
		Me.ExtraTag3TextBox.Tag = "TagsEnabled"
		'
		'ExtraTag4TextBox
		'
		Me.ExtraTag4TextBox.Location = New System.Drawing.Point(58, 101)
		Me.ExtraTag4TextBox.Name = "ExtraTag4TextBox"
		Me.ExtraTag4TextBox.Size = New System.Drawing.Size(100, 21)
		Me.ExtraTag4TextBox.TabIndex = 7
		Me.ExtraTag4TextBox.Tag = "TagsEnabled"
		'
		'ExtraTag5TextBox
		'
		Me.ExtraTag5TextBox.Location = New System.Drawing.Point(58, 128)
		Me.ExtraTag5TextBox.Name = "ExtraTag5TextBox"
		Me.ExtraTag5TextBox.Size = New System.Drawing.Size(100, 21)
		Me.ExtraTag5TextBox.TabIndex = 9
		Me.ExtraTag5TextBox.Tag = "TagsEnabled"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(6, 131)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(46, 13)
		Me.Label5.TabIndex = 8
		Me.Label5.Text = "Extra 5:"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(6, 104)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(46, 13)
		Me.Label4.TabIndex = 6
		Me.Label4.Text = "Extra 4:"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(6, 77)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(46, 13)
		Me.Label3.TabIndex = 4
		Me.Label3.Text = "Extra 3:"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(6, 50)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(46, 13)
		Me.Label2.TabIndex = 2
		Me.Label2.Text = "Extra 2:"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(6, 23)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(46, 13)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Extra 1:"
		'
		'BlackMesaTagsUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.ModelTagsGroupBox)
		Me.Controls.Add(Me.MapTagsGroupBox)
		Me.Name = "BlackMesaTagsUserControl"
		Me.Size = New System.Drawing.Size(318, 215)
		Me.MapTagsGroupBox.ResumeLayout(False)
		Me.MapTagsGroupBox.PerformLayout()
		Me.ModelTagsGroupBox.ResumeLayout(False)
		Me.ModelTagsGroupBox.PerformLayout()
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents MapTagsGroupBox As GroupBox
	Friend WithEvents MultiplayerTagCheckBox As CheckBoxEx
	Friend WithEvents SingleplayerTagCheckBox As CheckBoxEx
	Friend WithEvents ModelTagsGroupBox As GroupBox
	Friend WithEvents WeaponTagCheckBox As CheckBoxEx
	Friend WithEvents NPCTagCheckBox As CheckBoxEx
	Friend WithEvents EnvironmentTagCheckBox As CheckBoxEx
	Friend WithEvents CreatureTagCheckBox As CheckBoxEx
	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents ExtraTag5TextBox As TextBox
	Friend WithEvents ExtraTag4TextBox As TextBox
	Friend WithEvents ExtraTag3TextBox As TextBox
	Friend WithEvents ExtraTag2TextBox As TextBox
	Friend WithEvents ExtraTag1TextBox As TextBox
	Friend WithEvents Label5 As Label
	Friend WithEvents Label4 As Label
	Friend WithEvents Label3 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents Label1 As Label
End Class
