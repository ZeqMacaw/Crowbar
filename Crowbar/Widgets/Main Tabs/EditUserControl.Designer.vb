<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditUserControl
	Inherits BaseUserControl

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
		Me.components = New System.ComponentModel.Container()
		Me.Panel1 = New Crowbar.PanelEx()
		Me.GotoQcButton = New Crowbar.ButtonEx()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.QcPathFileNameTextBox = New Crowbar.RichTextBoxEx()
		Me.BrowseForQcPathFolderOrFileNameButton = New Crowbar.ButtonEx()
		Me.UseInCompileButton = New Crowbar.ButtonEx()
		Me.Panel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'Panel1
		'
		Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.Panel1.Controls.Add(Me.GotoQcButton)
		Me.Panel1.Controls.Add(Me.Label6)
		Me.Panel1.Controls.Add(Me.QcPathFileNameTextBox)
		Me.Panel1.Controls.Add(Me.BrowseForQcPathFolderOrFileNameButton)
		Me.Panel1.Controls.Add(Me.UseInCompileButton)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.SelectedIndex = -1
		Me.Panel1.SelectedValue = Nothing
		Me.Panel1.Size = New System.Drawing.Size(776, 536)
		Me.Panel1.TabIndex = 16
		'
		'GotoQcButton
		'
		Me.GotoQcButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoQcButton.Location = New System.Drawing.Point(733, 3)
		Me.GotoQcButton.Name = "GotoQcButton"
		Me.GotoQcButton.Size = New System.Drawing.Size(40, 23)
		Me.GotoQcButton.TabIndex = 29
		Me.GotoQcButton.Text = "Goto"
		Me.GotoQcButton.UseVisualStyleBackColor = True
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(3, 8)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(92, 13)
		Me.Label6.TabIndex = 26
		Me.Label6.Text = "QC file or folder:"
		'
		'QcPathFileNameTextBox
		'
		Me.QcPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.QcPathFileNameTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.QcPathFileNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.QcPathFileNameTextBox.CueBannerText = ""
		Me.QcPathFileNameTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.QcPathFileNameTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		Me.QcPathFileNameTextBox.Location = New System.Drawing.Point(91, 5)
		Me.QcPathFileNameTextBox.Multiline = False
		Me.QcPathFileNameTextBox.Name = "QcPathFileNameTextBox"
		Me.QcPathFileNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.QcPathFileNameTextBox.Size = New System.Drawing.Size(555, 21)
		Me.QcPathFileNameTextBox.TabIndex = 27
		Me.QcPathFileNameTextBox.Text = ""
		'
		'BrowseForQcPathFolderOrFileNameButton
		'
		Me.BrowseForQcPathFolderOrFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForQcPathFolderOrFileNameButton.Location = New System.Drawing.Point(652, 3)
		Me.BrowseForQcPathFolderOrFileNameButton.Name = "BrowseForQcPathFolderOrFileNameButton"
		Me.BrowseForQcPathFolderOrFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForQcPathFolderOrFileNameButton.TabIndex = 28
		Me.BrowseForQcPathFolderOrFileNameButton.Text = "Browse..."
		Me.BrowseForQcPathFolderOrFileNameButton.UseVisualStyleBackColor = True
		'
		'UseInCompileButton
		'
		Me.UseInCompileButton.Enabled = False
		Me.UseInCompileButton.Location = New System.Drawing.Point(3, 54)
		Me.UseInCompileButton.Name = "UseInCompileButton"
		Me.UseInCompileButton.Size = New System.Drawing.Size(90, 23)
		Me.UseInCompileButton.TabIndex = 25
		Me.UseInCompileButton.Text = "Use in Compile"
		Me.UseInCompileButton.UseVisualStyleBackColor = True
		'
		'EditUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel1)
		Me.Name = "EditUserControl"
		Me.Size = New System.Drawing.Size(776, 536)
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents Panel1 As PanelEx
	Friend WithEvents UseInCompileButton As ButtonEx
	Friend WithEvents GotoQcButton As ButtonEx
	Friend WithEvents Label6 As System.Windows.Forms.Label
	Friend WithEvents QcPathFileNameTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents BrowseForQcPathFolderOrFileNameButton As ButtonEx

End Class
