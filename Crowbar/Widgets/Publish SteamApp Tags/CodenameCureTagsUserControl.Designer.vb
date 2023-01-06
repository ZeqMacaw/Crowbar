<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CodenameCureTagsUserControl
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
        Me.LevelsTagsGroupBox = New System.Windows.Forms.GroupBox()
        Me.SurvivalMapTagCheckBox = New Crowbar.CheckBoxEx()
        Me.CustomTagCheckBox = New Crowbar.CheckBoxEx()
        Me.BombEscapeTagCheckBox = New Crowbar.CheckBoxEx()
        Me.PvPTagCheckBox = New Crowbar.CheckBoxEx()
        Me.PackTagCheckBox = New Crowbar.CheckBoxEx()
        Me.ModsTagCheckBox = New System.Windows.Forms.GroupBox()
        Me.SoundsTagCheckBox = New Crowbar.CheckBoxEx()
        Me.MiscTagCheckBox = New Crowbar.CheckBoxEx()
        Me.ModelsTagCheckBox = New Crowbar.CheckBoxEx()
        Me.SkinsTagCheckBox = New Crowbar.CheckBoxEx()
        Me.LevelsTagsGroupBox.SuspendLayout()
        Me.ModsTagCheckBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'LevelsTagsGroupBox
        '
        Me.LevelsTagsGroupBox.Controls.Add(Me.SurvivalMapTagCheckBox)
        Me.LevelsTagsGroupBox.Controls.Add(Me.CustomTagCheckBox)
        Me.LevelsTagsGroupBox.Controls.Add(Me.BombEscapeTagCheckBox)
        Me.LevelsTagsGroupBox.Controls.Add(Me.PvPTagCheckBox)
        Me.LevelsTagsGroupBox.Controls.Add(Me.PackTagCheckBox)
        Me.LevelsTagsGroupBox.Location = New System.Drawing.Point(3, 0)
        Me.LevelsTagsGroupBox.Name = "LevelsTagsGroupBox"
        Me.LevelsTagsGroupBox.Size = New System.Drawing.Size(114, 120)
        Me.LevelsTagsGroupBox.TabIndex = 0
        Me.LevelsTagsGroupBox.TabStop = False
        Me.LevelsTagsGroupBox.Text = "LEVELS"

        Me.BombEscapeTagCheckBox.AutoSize = True
        Me.BombEscapeTagCheckBox.IsReadOnly = False
        Me.BombEscapeTagCheckBox.Location = New System.Drawing.Point(6, 20)
        Me.BombEscapeTagCheckBox.Name = "BombEscapeTagCheckBox"
        Me.BombEscapeTagCheckBox.Size = New System.Drawing.Size(84, 17)
        Me.BombEscapeTagCheckBox.TabIndex = 0
        Me.BombEscapeTagCheckBox.Tag = "Bomb Escape"
        Me.BombEscapeTagCheckBox.Text = "Bomb Escape"
        Me.BombEscapeTagCheckBox.UseVisualStyleBackColor = True
        '
        'SurvivalMapTagCheckBox
        '
        Me.SurvivalMapTagCheckBox.AutoSize = True
        Me.SurvivalMapTagCheckBox.IsReadOnly = False
        Me.SurvivalMapTagCheckBox.Location = New System.Drawing.Point(6, 40)
        Me.SurvivalMapTagCheckBox.Name = "SurvivalMapTagCheckBox"
        Me.SurvivalMapTagCheckBox.Size = New System.Drawing.Size(78, 17)
        Me.SurvivalMapTagCheckBox.TabIndex = 1
        Me.SurvivalMapTagCheckBox.Tag = "Survival"
        Me.SurvivalMapTagCheckBox.Text = "Survival"
        Me.SurvivalMapTagCheckBox.UseVisualStyleBackColor = True
        '
        'CustomTagCheckBox
        '
        Me.CustomTagCheckBox.AutoSize = True
        Me.CustomTagCheckBox.IsReadOnly = False
        Me.CustomTagCheckBox.Location = New System.Drawing.Point(6, 60)
        Me.CustomTagCheckBox.Name = "CustomTagCheckBox"
        Me.CustomTagCheckBox.Size = New System.Drawing.Size(84, 17)
        Me.CustomTagCheckBox.TabIndex = 0
        Me.CustomTagCheckBox.Tag = "Custom"
        Me.CustomTagCheckBox.Text = "Custom"
        Me.CustomTagCheckBox.UseVisualStyleBackColor = True

        Me.PvPTagCheckBox.AutoSize = True
        Me.PvPTagCheckBox.IsReadOnly = False
        Me.PvPTagCheckBox.Location = New System.Drawing.Point(6, 80)
        Me.PvPTagCheckBox.Name = "PvPTagCheckBox"
        Me.PvPTagCheckBox.Size = New System.Drawing.Size(84, 17)
        Me.PvPTagCheckBox.TabIndex = 0
        Me.PvPTagCheckBox.Tag = "PvP"
        Me.PvPTagCheckBox.Text = "PvP"
        Me.PvPTagCheckBox.UseVisualStyleBackColor = True

        Me.PackTagCheckBox.AutoSize = True
        Me.PackTagCheckBox.IsReadOnly = False
        Me.PackTagCheckBox.Location = New System.Drawing.Point(6, 100)
        Me.PackTagCheckBox.Name = "PackTagCheckBox"
        Me.PackTagCheckBox.Size = New System.Drawing.Size(84, 17)
        Me.PackTagCheckBox.TabIndex = 0
        Me.PackTagCheckBox.Tag = "Packs"
        Me.PackTagCheckBox.Text = "Packs"
        Me.PackTagCheckBox.UseVisualStyleBackColor = True

        '
        'ModsTagCheckBox
        '
        Me.ModsTagCheckBox.Controls.Add(Me.SoundsTagCheckBox)
        Me.ModsTagCheckBox.Controls.Add(Me.MiscTagCheckBox)
        Me.ModsTagCheckBox.Controls.Add(Me.ModelsTagCheckBox)
        Me.ModsTagCheckBox.Controls.Add(Me.SkinsTagCheckBox)
        Me.ModsTagCheckBox.Location = New System.Drawing.Point(133, 0)
        Me.ModsTagCheckBox.Name = "ModsTagCheckBox"
        Me.ModsTagCheckBox.Size = New System.Drawing.Size(114, 100)
        Me.ModsTagCheckBox.TabIndex = 1
        Me.ModsTagCheckBox.TabStop = False
        Me.ModsTagCheckBox.Text = "MODS"

        Me.SkinsTagCheckBox.AutoSize = True
        Me.SkinsTagCheckBox.IsReadOnly = False
        Me.SkinsTagCheckBox.Location = New System.Drawing.Point(6, 20)
        Me.SkinsTagCheckBox.Name = "SkinsTagCheckBox"
        Me.SkinsTagCheckBox.Size = New System.Drawing.Size(69, 17)
        Me.SkinsTagCheckBox.TabIndex = 0
        Me.SkinsTagCheckBox.Tag = "Skins"
        Me.SkinsTagCheckBox.Text = "Skins"
        Me.SkinsTagCheckBox.UseVisualStyleBackColor = True

        Me.ModelsTagCheckBox.AutoSize = True
        Me.ModelsTagCheckBox.IsReadOnly = False
        Me.ModelsTagCheckBox.Location = New System.Drawing.Point(6, 40)
        Me.ModelsTagCheckBox.Name = "ModelsTagCheckBox"
        Me.ModelsTagCheckBox.Size = New System.Drawing.Size(69, 17)
        Me.ModelsTagCheckBox.TabIndex = 0
        Me.ModelsTagCheckBox.Tag = "Models"
        Me.ModelsTagCheckBox.Text = "Models"
        Me.ModelsTagCheckBox.UseVisualStyleBackColor = True

        '
        'SoundsTagCheckBox
        '
        Me.SoundsTagCheckBox.AutoSize = True
        Me.SoundsTagCheckBox.IsReadOnly = False
        Me.SoundsTagCheckBox.Location = New System.Drawing.Point(6, 60)
        Me.SoundsTagCheckBox.Name = "SoundsTagCheckBox"
        Me.SoundsTagCheckBox.Size = New System.Drawing.Size(66, 17)
        Me.SoundsTagCheckBox.TabIndex = 3
        Me.SoundsTagCheckBox.Tag = "Sounds"
        Me.SoundsTagCheckBox.Text = "Sounds"
        Me.SoundsTagCheckBox.UseVisualStyleBackColor = True
        '
        'MiscTagCheckBox
        '
        Me.MiscTagCheckBox.AutoSize = True
        Me.MiscTagCheckBox.IsReadOnly = False
        Me.MiscTagCheckBox.Location = New System.Drawing.Point(6, 80)
        Me.MiscTagCheckBox.Name = "MiscTagCheckBox"
        Me.MiscTagCheckBox.Size = New System.Drawing.Size(46, 17)
        Me.MiscTagCheckBox.TabIndex = 2
        Me.MiscTagCheckBox.Tag = "Miscellaneous"
        Me.MiscTagCheckBox.Text = "Miscellaneous"
        Me.MiscTagCheckBox.UseVisualStyleBackColor = True

        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ModsTagCheckBox)
        Me.Controls.Add(Me.LevelsTagsGroupBox)
        Me.Name = "CodenameCureTagsUserControl"
        Me.Size = New System.Drawing.Size(318, 215)
        Me.LevelsTagsGroupBox.ResumeLayout(False)
        Me.LevelsTagsGroupBox.PerformLayout()
        Me.ModsTagCheckBox.ResumeLayout(False)
        Me.ModsTagCheckBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LevelsTagsGroupBox As GroupBox
    Friend WithEvents SurvivalMapTagCheckBox As CheckBoxEx
    Friend WithEvents CustomTagCheckBox As CheckBoxEx
    Friend WithEvents PvPTagCheckBox As CheckBoxEx
    Friend WithEvents PackTagCheckBox As CheckBoxEx
    Friend WithEvents BombEscapeTagCheckBox As CheckBoxEx
    Friend WithEvents ModsTagCheckBox As GroupBox
    Friend WithEvents SoundsTagCheckBox As CheckBoxEx
    Friend WithEvents MiscTagCheckBox As CheckBoxEx
    Friend WithEvents ModelsTagCheckBox As CheckBoxEx
    Friend WithEvents SkinsTagCheckBox As CheckBoxEx

End Class
