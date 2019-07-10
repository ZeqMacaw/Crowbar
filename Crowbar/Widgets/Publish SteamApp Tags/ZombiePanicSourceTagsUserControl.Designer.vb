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
                Me.GroupBox_GameMode = New System.Windows.Forms.GroupBox()
                Me.ComboBox_GameMode = New System.Windows.Forms.ComboBox()
                Me.GroupBox_CustomModels = New System.Windows.Forms.GroupBox()
                Me.ComboBox_CustomModels = New System.Windows.Forms.ComboBox()
                Me.GroupBox_CustomSounds = New System.Windows.Forms.GroupBox()
                Me.ComboBox_CustomSounds = New System.Windows.Forms.ComboBox()
                Me.GroupBox_Miscellaneous = New System.Windows.Forms.GroupBox()
                Me.ComboBox_Miscellaneous = New System.Windows.Forms.ComboBox()
                Me.GroupBox_GameMode.SuspendLayout()
                Me.GroupBox_CustomModels.SuspendLayout()
                Me.GroupBox_CustomSounds.SuspendLayout()
                Me.GroupBox_Miscellaneous.SuspendLayout()
                Me.SuspendLayout()
                '
                'GroupBox_GameMode
                '
                Me.GroupBox_GameMode.Controls.Add(Me.ComboBox_GameMode)
                Me.GroupBox_GameMode.Location = New System.Drawing.Point(0, 0)
                Me.GroupBox_GameMode.Name = "GroupBox_GameMode"
                Me.GroupBox_GameMode.Size = New System.Drawing.Size(188, 47)
                Me.GroupBox_GameMode.TabIndex = 0
                Me.GroupBox_GameMode.TabStop = False
                Me.GroupBox_GameMode.Text = "Game mode"
                '
                'ComboBox_GameMode
                '
                Me.ComboBox_GameMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
                Me.ComboBox_GameMode.FormattingEnabled = True
                Me.ComboBox_GameMode.Location = New System.Drawing.Point(6, 21)
                Me.ComboBox_GameMode.Name = "ComboBox_GameMode"
                Me.ComboBox_GameMode.Size = New System.Drawing.Size(176, 21)
                Me.ComboBox_GameMode.TabIndex = 1
                Me.ComboBox_GameMode.Tag = "Game mode"
                '
                'GroupBox_CustomModels
                '
                Me.GroupBox_CustomModels.Controls.Add(Me.ComboBox_CustomModels)
                Me.GroupBox_CustomModels.Location = New System.Drawing.Point(0, 47)
                Me.GroupBox_CustomModels.Name = "GroupBox_CustomModels"
                Me.GroupBox_CustomModels.Size = New System.Drawing.Size(188, 47)
                Me.GroupBox_CustomModels.TabIndex = 2
                Me.GroupBox_CustomModels.TabStop = False
                Me.GroupBox_CustomModels.Text = "Custom models"
                '
                'ComboBox_CustomModels
                '
                Me.ComboBox_CustomModels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
                Me.ComboBox_CustomModels.FormattingEnabled = True
                Me.ComboBox_CustomModels.Location = New System.Drawing.Point(6, 21)
                Me.ComboBox_CustomModels.Name = "ComboBox_CustomModels"
                Me.ComboBox_CustomModels.Size = New System.Drawing.Size(176, 21)
                Me.ComboBox_CustomModels.TabIndex = 3
                Me.ComboBox_CustomModels.Tag = "Custom models"
                '
                'GroupBox_CustomSounds
                '
                Me.GroupBox_CustomSounds.Controls.Add(Me.ComboBox_CustomSounds)
                Me.GroupBox_CustomSounds.Location = New System.Drawing.Point(0, 94)
                Me.GroupBox_CustomSounds.Name = "GroupBox_CustomSounds"
                Me.GroupBox_CustomSounds.Size = New System.Drawing.Size(188, 47)
                Me.GroupBox_CustomSounds.TabIndex = 4
                Me.GroupBox_CustomSounds.TabStop = False
                Me.GroupBox_CustomSounds.Text = "Custom sounds"
                '
                'ComboBox_CustomSounds
                '
                Me.ComboBox_CustomSounds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
                Me.ComboBox_CustomSounds.FormattingEnabled = True
                Me.ComboBox_CustomSounds.Location = New System.Drawing.Point(6, 21)
                Me.ComboBox_CustomSounds.Name = "ComboBox_CustomSounds"
                Me.ComboBox_CustomSounds.Size = New System.Drawing.Size(176, 21)
                Me.ComboBox_CustomSounds.TabIndex = 5
                Me.ComboBox_CustomSounds.Tag = "Custom sounds"
                '
                'GroupBox_Miscellaneous
                '
                Me.GroupBox_Miscellaneous.Controls.Add(Me.ComboBox_Miscellaneous)
                Me.GroupBox_Miscellaneous.Location = New System.Drawing.Point(0, 141)
                Me.GroupBox_Miscellaneous.Name = "GroupBox_Miscellaneous"
                Me.GroupBox_Miscellaneous.Size = New System.Drawing.Size(188, 47)
                Me.GroupBox_Miscellaneous.TabIndex = 6
                Me.GroupBox_Miscellaneous.TabStop = False
                Me.GroupBox_Miscellaneous.Text = "Miscellaneous"
                '
                'ComboBox_Miscellaneous
                '
                Me.ComboBox_Miscellaneous.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
                Me.ComboBox_Miscellaneous.FormattingEnabled = True
                Me.ComboBox_Miscellaneous.Location = New System.Drawing.Point(6, 21)
                Me.ComboBox_Miscellaneous.Name = "ComboBox_Miscellaneous"
                Me.ComboBox_Miscellaneous.Size = New System.Drawing.Size(176, 21)
                Me.ComboBox_Miscellaneous.TabIndex = 7
                Me.ComboBox_Miscellaneous.Tag = "Miscellaneous"
                '
                'ZombiePanicSourceTagsUserControl
                '
                Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
                Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
                Me.AutoScroll = True
                Me.Controls.Add(Me.GroupBox_GameMode)
                Me.Controls.Add(Me.GroupBox_CustomModels)
                Me.Controls.Add(Me.GroupBox_CustomSounds)
                Me.Controls.Add(Me.GroupBox_Miscellaneous)
                Me.Name = "ZombiePanicSourceTagsUserControl"
                Me.Size = New System.Drawing.Size(188, 351)
                Me.GroupBox_GameMode.ResumeLayout(False)
                Me.GroupBox_CustomModels.ResumeLayout(False)
                Me.GroupBox_CustomSounds.ResumeLayout(False)
                Me.GroupBox_Miscellaneous.ResumeLayout(False)
                Me.ResumeLayout(False)

        End Sub

        Friend WithEvents GroupBox_GameMode As GroupBox
        Friend WithEvents ComboBox_GameMode As ComboBox
        Friend WithEvents GroupBox_CustomModels As GroupBox
        Friend WithEvents ComboBox_CustomModels As ComboBox
        Friend WithEvents GroupBox_CustomSounds As GroupBox
        Friend WithEvents ComboBox_CustomSounds As ComboBox
        Friend WithEvents GroupBox_Miscellaneous As GroupBox
        Friend WithEvents ComboBox_Miscellaneous As ComboBox
End Class
