<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ViewUserControl
	Inherits BaseUserControl

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
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Me.ViewButton = New System.Windows.Forms.Button()
		Me.MdlPathFileNameTextBox = New Crowbar.TextBoxEx()
		Me.BrowseForMdlFileButton = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.OverrideMdlVersionLabel = New System.Windows.Forms.Label()
		Me.OverrideMdlVersionComboBox = New System.Windows.Forms.ComboBox()
		Me.GotoMdlFileButton = New System.Windows.Forms.Button()
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.InfoRichTextBox = New Crowbar.RichTextBoxEx()
		Me.GameLabel = New System.Windows.Forms.Label()
		Me.GameSetupComboBox = New System.Windows.Forms.ComboBox()
		Me.SetUpGameButton = New System.Windows.Forms.Button()
		Me.ViewAsReplacementButton = New System.Windows.Forms.Button()
		Me.UseInDecompileButton = New System.Windows.Forms.Button()
		Me.OpenViewerButton = New System.Windows.Forms.Button()
		Me.OpenMappingToolButton = New System.Windows.Forms.Button()
		Me.RunGameButton = New System.Windows.Forms.Button()
		Me.MessageTextBox = New Crowbar.TextBoxEx()
		Me.Panel2.SuspendLayout()
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer1.Panel1.SuspendLayout()
		Me.SplitContainer1.Panel2.SuspendLayout()
		Me.SplitContainer1.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'ViewButton
		'
		Me.ViewButton.Enabled = False
		Me.ViewButton.Location = New System.Drawing.Point(0, 32)
		Me.ViewButton.Name = "ViewButton"
		Me.ViewButton.Size = New System.Drawing.Size(40, 23)
		Me.ViewButton.TabIndex = 8
		Me.ViewButton.Text = "View"
		Me.ViewButton.UseVisualStyleBackColor = True
		'
		'MdlPathFileNameTextBox
		'
		Me.MdlPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.MdlPathFileNameTextBox.CueBannerText = ""
		Me.MdlPathFileNameTextBox.Location = New System.Drawing.Point(58, 3)
		Me.MdlPathFileNameTextBox.Name = "MdlPathFileNameTextBox"
		Me.MdlPathFileNameTextBox.Size = New System.Drawing.Size(596, 22)
		Me.MdlPathFileNameTextBox.TabIndex = 1
		'
		'BrowseForMdlFileButton
		'
		Me.BrowseForMdlFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForMdlFileButton.Location = New System.Drawing.Point(660, 3)
		Me.BrowseForMdlFileButton.Name = "BrowseForMdlFileButton"
		Me.BrowseForMdlFileButton.Size = New System.Drawing.Size(64, 23)
		Me.BrowseForMdlFileButton.TabIndex = 2
		Me.BrowseForMdlFileButton.Text = "Browse..."
		Me.BrowseForMdlFileButton.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(3, 8)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(52, 13)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "MDL file:"
		'
		'Panel2
		'
		Me.Panel2.Controls.Add(Me.OverrideMdlVersionLabel)
		Me.Panel2.Controls.Add(Me.Label1)
		Me.Panel2.Controls.Add(Me.MdlPathFileNameTextBox)
		Me.Panel2.Controls.Add(Me.OverrideMdlVersionComboBox)
		Me.Panel2.Controls.Add(Me.BrowseForMdlFileButton)
		Me.Panel2.Controls.Add(Me.GotoMdlFileButton)
		Me.Panel2.Controls.Add(Me.SplitContainer1)
		Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel2.Location = New System.Drawing.Point(0, 0)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(776, 536)
		Me.Panel2.TabIndex = 8
		'
		'OverrideMdlVersionLabel
		'
		Me.OverrideMdlVersionLabel.AutoSize = True
		Me.OverrideMdlVersionLabel.Location = New System.Drawing.Point(3, 36)
		Me.OverrideMdlVersionLabel.Name = "OverrideMdlVersionLabel"
		Me.OverrideMdlVersionLabel.Size = New System.Drawing.Size(120, 13)
		Me.OverrideMdlVersionLabel.TabIndex = 48
		Me.OverrideMdlVersionLabel.Text = "Override MDL version:"
		'
		'OverrideMdlVersionComboBox
		'
		Me.OverrideMdlVersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.OverrideMdlVersionComboBox.FormattingEnabled = True
		Me.OverrideMdlVersionComboBox.Location = New System.Drawing.Point(123, 32)
		Me.OverrideMdlVersionComboBox.Name = "OverrideMdlVersionComboBox"
		Me.OverrideMdlVersionComboBox.Size = New System.Drawing.Size(110, 21)
		Me.OverrideMdlVersionComboBox.TabIndex = 47
		'
		'GotoMdlFileButton
		'
		Me.GotoMdlFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoMdlFileButton.Location = New System.Drawing.Point(730, 3)
		Me.GotoMdlFileButton.Name = "GotoMdlFileButton"
		Me.GotoMdlFileButton.Size = New System.Drawing.Size(43, 23)
		Me.GotoMdlFileButton.TabIndex = 3
		Me.GotoMdlFileButton.Text = "Goto"
		Me.GotoMdlFileButton.UseVisualStyleBackColor = True
		'
		'SplitContainer1
		'
		Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SplitContainer1.Location = New System.Drawing.Point(3, 59)
		Me.SplitContainer1.Name = "SplitContainer1"
		Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
		Me.SplitContainer1.Panel1MinSize = 45
		'
		'SplitContainer1.Panel2
		'
		Me.SplitContainer1.Panel2.Controls.Add(Me.GameLabel)
		Me.SplitContainer1.Panel2.Controls.Add(Me.GameSetupComboBox)
		Me.SplitContainer1.Panel2.Controls.Add(Me.SetUpGameButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.ViewButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.ViewAsReplacementButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.UseInDecompileButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.OpenViewerButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.OpenMappingToolButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.RunGameButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.MessageTextBox)
		Me.SplitContainer1.Panel2MinSize = 45
		Me.SplitContainer1.Size = New System.Drawing.Size(770, 474)
		Me.SplitContainer1.SplitterDistance = 363
		Me.SplitContainer1.TabIndex = 13
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.InfoRichTextBox)
		Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(770, 363)
		Me.GroupBox1.TabIndex = 4
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Info"
		'
		'InfoRichTextBox
		'
		Me.InfoRichTextBox.CueBannerText = ""
		Me.InfoRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.InfoRichTextBox.Location = New System.Drawing.Point(3, 18)
		Me.InfoRichTextBox.Name = "InfoRichTextBox"
		Me.InfoRichTextBox.ReadOnly = True
		Me.InfoRichTextBox.Size = New System.Drawing.Size(764, 342)
		Me.InfoRichTextBox.TabIndex = 0
		Me.InfoRichTextBox.Text = ""
		Me.InfoRichTextBox.WordWrap = False
		'
		'GameLabel
		'
		Me.GameLabel.AutoSize = True
		Me.GameLabel.Location = New System.Drawing.Point(0, 8)
		Me.GameLabel.Name = "GameLabel"
		Me.GameLabel.Size = New System.Drawing.Size(175, 13)
		Me.GameLabel.TabIndex = 5
		Me.GameLabel.Text = "Game that has the model viewer:"
		'
		'GameSetupComboBox
		'
		Me.GameSetupComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.GameSetupComboBox.FormattingEnabled = True
		Me.GameSetupComboBox.Location = New System.Drawing.Point(181, 4)
		Me.GameSetupComboBox.Name = "GameSetupComboBox"
		Me.GameSetupComboBox.Size = New System.Drawing.Size(493, 21)
		Me.GameSetupComboBox.TabIndex = 6
		'
		'SetUpGameButton
		'
		Me.SetUpGameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SetUpGameButton.Location = New System.Drawing.Point(680, 3)
		Me.SetUpGameButton.Name = "SetUpGameButton"
		Me.SetUpGameButton.Size = New System.Drawing.Size(90, 23)
		Me.SetUpGameButton.TabIndex = 7
		Me.SetUpGameButton.Text = "Set Up Games"
		Me.SetUpGameButton.UseVisualStyleBackColor = True
		'
		'ViewAsReplacementButton
		'
		Me.ViewAsReplacementButton.Enabled = False
		Me.ViewAsReplacementButton.Location = New System.Drawing.Point(46, 32)
		Me.ViewAsReplacementButton.Name = "ViewAsReplacementButton"
		Me.ViewAsReplacementButton.Size = New System.Drawing.Size(125, 23)
		Me.ViewAsReplacementButton.TabIndex = 9
		Me.ViewAsReplacementButton.Text = "View as Replacement"
		Me.ViewAsReplacementButton.UseVisualStyleBackColor = True
		'
		'UseInDecompileButton
		'
		Me.UseInDecompileButton.Enabled = False
		Me.UseInDecompileButton.Location = New System.Drawing.Point(177, 32)
		Me.UseInDecompileButton.Name = "UseInDecompileButton"
		Me.UseInDecompileButton.Size = New System.Drawing.Size(120, 23)
		Me.UseInDecompileButton.TabIndex = 10
		Me.UseInDecompileButton.Text = "Use in Decompile"
		Me.UseInDecompileButton.UseVisualStyleBackColor = True
		'
		'OpenViewerButton
		'
		Me.OpenViewerButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OpenViewerButton.Location = New System.Drawing.Point(488, 32)
		Me.OpenViewerButton.Name = "OpenViewerButton"
		Me.OpenViewerButton.Size = New System.Drawing.Size(90, 23)
		Me.OpenViewerButton.TabIndex = 11
		Me.OpenViewerButton.Text = "Open Viewer"
		Me.OpenViewerButton.UseVisualStyleBackColor = True
		'
		'OpenMappingToolButton
		'
		Me.OpenMappingToolButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OpenMappingToolButton.Location = New System.Drawing.Point(584, 32)
		Me.OpenMappingToolButton.Name = "OpenMappingToolButton"
		Me.OpenMappingToolButton.Size = New System.Drawing.Size(90, 23)
		Me.OpenMappingToolButton.TabIndex = 14
		Me.OpenMappingToolButton.Text = "Open Mapper"
		Me.OpenMappingToolButton.UseVisualStyleBackColor = True
		'
		'RunGameButton
		'
		Me.RunGameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.RunGameButton.Location = New System.Drawing.Point(680, 32)
		Me.RunGameButton.Name = "RunGameButton"
		Me.RunGameButton.Size = New System.Drawing.Size(90, 23)
		Me.RunGameButton.TabIndex = 13
		Me.RunGameButton.Text = "Run Game"
		Me.RunGameButton.UseVisualStyleBackColor = True
		'
		'MessageTextBox
		'
		Me.MessageTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.MessageTextBox.CueBannerText = ""
		Me.MessageTextBox.Location = New System.Drawing.Point(0, 61)
		Me.MessageTextBox.Multiline = True
		Me.MessageTextBox.Name = "MessageTextBox"
		Me.MessageTextBox.ReadOnly = True
		Me.MessageTextBox.Size = New System.Drawing.Size(770, 45)
		Me.MessageTextBox.TabIndex = 12
		'
		'ViewUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel2)
		Me.Name = "ViewUserControl"
		Me.Size = New System.Drawing.Size(776, 536)
		Me.Panel2.ResumeLayout(False)
		Me.Panel2.PerformLayout()
		Me.SplitContainer1.Panel1.ResumeLayout(False)
		Me.SplitContainer1.Panel2.ResumeLayout(False)
		Me.SplitContainer1.Panel2.PerformLayout()
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer1.ResumeLayout(False)
		Me.GroupBox1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents ViewButton As System.Windows.Forms.Button
	Friend WithEvents MdlPathFileNameTextBox As TextBoxEx
	Friend WithEvents BrowseForMdlFileButton As System.Windows.Forms.Button
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Panel2 As System.Windows.Forms.Panel
	Friend WithEvents GameLabel As System.Windows.Forms.Label
	Friend WithEvents SetUpGameButton As System.Windows.Forms.Button
	Friend WithEvents GameSetupComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents GotoMdlFileButton As System.Windows.Forms.Button
	Friend WithEvents ViewAsReplacementButton As System.Windows.Forms.Button
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents InfoRichTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents UseInDecompileButton As System.Windows.Forms.Button
	Friend WithEvents OpenViewerButton As System.Windows.Forms.Button
	Friend WithEvents MessageTextBox As Crowbar.TextBoxEx
	Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
	Friend WithEvents RunGameButton As System.Windows.Forms.Button
	Friend WithEvents OpenMappingToolButton As System.Windows.Forms.Button
	Friend WithEvents OverrideMdlVersionLabel As Label
	Friend WithEvents OverrideMdlVersionComboBox As ComboBox
End Class
