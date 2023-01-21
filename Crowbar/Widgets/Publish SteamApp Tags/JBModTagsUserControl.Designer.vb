<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class JBModTagsUserControl
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
		Me.GameTypeGroupBox = New System.Windows.Forms.GroupBox()
		Me.MultiplayerTagCheckBox = New Crowbar.CheckBoxEx()
		Me.SingleplayerTagCheckBox = New Crowbar.CheckBoxEx()
		Me.ContentTypeGroupBox = New System.Windows.Forms.GroupBox()
		Me.SkinTagCheckBox = New Crowbar.CheckBoxEx()
		Me.ScriptTagCheckBox = New Crowbar.CheckBoxEx()
		Me.ModelTagCheckBox = New Crowbar.CheckBoxEx()
		Me.MapTagCheckBox = New Crowbar.CheckBoxEx()
		Me.GameTypeGroupBox.SuspendLayout()
		Me.ContentTypeGroupBox.SuspendLayout()
		Me.SuspendLayout()
		'
		'GameTypeGroupBox
		'
		Me.GameTypeGroupBox.Controls.Add(Me.MultiplayerTagCheckBox)
		Me.GameTypeGroupBox.Controls.Add(Me.SingleplayerTagCheckBox)
		Me.GameTypeGroupBox.Location = New System.Drawing.Point(3, 0)
		Me.GameTypeGroupBox.Name = "GameTypeGroupBox"
		Me.GameTypeGroupBox.Size = New System.Drawing.Size(114, 77)
		Me.GameTypeGroupBox.TabIndex = 0
		Me.GameTypeGroupBox.TabStop = False
		Me.GameTypeGroupBox.Text = "GAME TYPE"
		'
		'MultiplayerTagCheckBox
		'
		Me.MultiplayerTagCheckBox.AutoSize = True
		Me.MultiplayerTagCheckBox.IsReadOnly = False
		Me.MultiplayerTagCheckBox.Location = New System.Drawing.Point(6, 43)
		Me.MultiplayerTagCheckBox.Name = "MultiplayerTagCheckBox"
		Me.MultiplayerTagCheckBox.Size = New System.Drawing.Size(84, 17)
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
		Me.SingleplayerTagCheckBox.Size = New System.Drawing.Size(89, 17)
		Me.SingleplayerTagCheckBox.TabIndex = 0
		Me.SingleplayerTagCheckBox.Tag = "Singleplayer"
		Me.SingleplayerTagCheckBox.Text = "Singleplayer"
		Me.SingleplayerTagCheckBox.UseVisualStyleBackColor = True
		'
		'ContentTypeGroupBox
		'
		Me.ContentTypeGroupBox.Controls.Add(Me.SkinTagCheckBox)
		Me.ContentTypeGroupBox.Controls.Add(Me.ScriptTagCheckBox)
		Me.ContentTypeGroupBox.Controls.Add(Me.ModelTagCheckBox)
		Me.ContentTypeGroupBox.Controls.Add(Me.MapTagCheckBox)
		Me.ContentTypeGroupBox.Location = New System.Drawing.Point(3, 83)
		Me.ContentTypeGroupBox.Name = "ContentTypeGroupBox"
		Me.ContentTypeGroupBox.Size = New System.Drawing.Size(114, 123)
		Me.ContentTypeGroupBox.TabIndex = 1
		Me.ContentTypeGroupBox.TabStop = False
		Me.ContentTypeGroupBox.Text = "CONTENT TYPE"
		'
		'SkinTagCheckBox
		'
		Me.SkinTagCheckBox.AutoSize = True
		Me.SkinTagCheckBox.IsReadOnly = False
		Me.SkinTagCheckBox.Location = New System.Drawing.Point(6, 66)
		Me.SkinTagCheckBox.Name = "SkinTagCheckBox"
		Me.SkinTagCheckBox.Size = New System.Drawing.Size(48, 17)
		Me.SkinTagCheckBox.TabIndex = 3
		Me.SkinTagCheckBox.Tag = "Skin"
		Me.SkinTagCheckBox.Text = "Skin"
		Me.SkinTagCheckBox.UseVisualStyleBackColor = True
		'
		'ScriptTagCheckBox
		'
		Me.ScriptTagCheckBox.AutoSize = True
		Me.ScriptTagCheckBox.IsReadOnly = False
		Me.ScriptTagCheckBox.Location = New System.Drawing.Point(6, 89)
		Me.ScriptTagCheckBox.Name = "ScriptTagCheckBox"
		Me.ScriptTagCheckBox.Size = New System.Drawing.Size(55, 17)
		Me.ScriptTagCheckBox.TabIndex = 2
		Me.ScriptTagCheckBox.Tag = "Script"
		Me.ScriptTagCheckBox.Text = "Script"
		Me.ScriptTagCheckBox.UseVisualStyleBackColor = True
		'
		'ModelTagCheckBox
		'
		Me.ModelTagCheckBox.AutoSize = True
		Me.ModelTagCheckBox.IsReadOnly = False
		Me.ModelTagCheckBox.Location = New System.Drawing.Point(6, 43)
		Me.ModelTagCheckBox.Name = "ModelTagCheckBox"
		Me.ModelTagCheckBox.Size = New System.Drawing.Size(59, 17)
		Me.ModelTagCheckBox.TabIndex = 0
		Me.ModelTagCheckBox.Tag = "Model"
		Me.ModelTagCheckBox.Text = "Model"
		Me.ModelTagCheckBox.UseVisualStyleBackColor = True
		'
		'MapTagCheckBox
		'
		Me.MapTagCheckBox.AutoSize = True
		Me.MapTagCheckBox.IsReadOnly = False
		Me.MapTagCheckBox.Location = New System.Drawing.Point(6, 20)
		Me.MapTagCheckBox.Name = "MapTagCheckBox"
		Me.MapTagCheckBox.Size = New System.Drawing.Size(49, 17)
		Me.MapTagCheckBox.TabIndex = 0
		Me.MapTagCheckBox.Tag = "Map"
		Me.MapTagCheckBox.Text = "Map"
		Me.MapTagCheckBox.UseVisualStyleBackColor = True
		'
		'JBModTagsUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.ContentTypeGroupBox)
		Me.Controls.Add(Me.GameTypeGroupBox)
		Me.Name = "JBModTagsUserControl"
		Me.Size = New System.Drawing.Size(124, 215)
		Me.GameTypeGroupBox.ResumeLayout(False)
		Me.GameTypeGroupBox.PerformLayout()
		Me.ContentTypeGroupBox.ResumeLayout(False)
		Me.ContentTypeGroupBox.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents GameTypeGroupBox As GroupBox
    Friend WithEvents MultiplayerTagCheckBox As CheckBoxEx
    Friend WithEvents SingleplayerTagCheckBox As CheckBoxEx
    Friend WithEvents ContentTypeGroupBox As GroupBox
    Friend WithEvents SkinTagCheckBox As CheckBoxEx
    Friend WithEvents ScriptTagCheckBox As CheckBoxEx
    Friend WithEvents ModelTagCheckBox As CheckBoxEx
    Friend WithEvents MapTagCheckBox As CheckBoxEx

End Class
