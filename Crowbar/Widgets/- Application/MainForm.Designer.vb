<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
	Inherits BaseForm

	'Form overrides dispose to clean up the component list.
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
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.AboutCrowbarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
		Me.AboutCrowbarToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
		Me.MainTabControl = New Crowbar.TabControlEx()
		Me.SetUpGamesTabPage = New System.Windows.Forms.TabPage()
		Me.SetUpGamesUserControl1 = New Crowbar.SetUpGamesUserControl()
		Me.DownloadTabPage = New System.Windows.Forms.TabPage()
		Me.DownloadUserControl1 = New Crowbar.DownloadUserControl()
		Me.UnpackTabPage = New System.Windows.Forms.TabPage()
		Me.UnpackUserControl1 = New Crowbar.UnpackUserControl()
		Me.PreviewTabPage = New System.Windows.Forms.TabPage()
		Me.PreviewViewUserControl = New Crowbar.ViewUserControl()
		Me.DecompileTabPage = New System.Windows.Forms.TabPage()
		Me.DecompilerUserControl1 = New Crowbar.DecompileUserControl()
		Me.CompileTabPage = New System.Windows.Forms.TabPage()
		Me.CompilerUserControl1 = New Crowbar.CompileUserControl()
		Me.ViewTabPage = New System.Windows.Forms.TabPage()
		Me.ViewViewUserControl = New Crowbar.ViewUserControl()
		Me.PackTabPage = New System.Windows.Forms.TabPage()
		Me.PackUserControl1 = New Crowbar.PackUserControl()
		Me.PublishTabPage = New System.Windows.Forms.TabPage()
		Me.PublishUserControl1 = New Crowbar.PublishUserControl()
		Me.OptionsTabPage = New System.Windows.Forms.TabPage()
		Me.OptionsUserControl1 = New Crowbar.OptionsUserControl()
		Me.HelpTabPage = New System.Windows.Forms.TabPage()
		Me.HelpUserControl1 = New Crowbar.HelpUserControl()
		Me.AboutTabPage = New System.Windows.Forms.TabPage()
		Me.AboutUserControl1 = New Crowbar.AboutUserControl()
		Me.UpdateTabPage = New System.Windows.Forms.TabPage()
		Me.UpdateUserControl1 = New Crowbar.UpdateUserControl()
		Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
		Me.AboutCrowbarToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
		Me.MainToolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.MainTabControl.SuspendLayout()
		Me.SetUpGamesTabPage.SuspendLayout()
		Me.DownloadTabPage.SuspendLayout()
		Me.UnpackTabPage.SuspendLayout()
		Me.PreviewTabPage.SuspendLayout()
		Me.DecompileTabPage.SuspendLayout()
		Me.CompileTabPage.SuspendLayout()
		Me.ViewTabPage.SuspendLayout()
		Me.PackTabPage.SuspendLayout()
		Me.PublishTabPage.SuspendLayout()
		Me.OptionsTabPage.SuspendLayout()
		Me.HelpTabPage.SuspendLayout()
		Me.AboutTabPage.SuspendLayout()
		Me.UpdateTabPage.SuspendLayout()
		Me.SuspendLayout()
		'
		'AboutCrowbarToolStripMenuItem
		'
		Me.AboutCrowbarToolStripMenuItem.Name = "AboutCrowbarToolStripMenuItem"
		Me.AboutCrowbarToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
		Me.AboutCrowbarToolStripMenuItem.Text = "About Crowbar"
		'
		'ToolStripMenuItem1
		'
		Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutCrowbarToolStripMenuItem1})
		Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
		Me.ToolStripMenuItem1.Size = New System.Drawing.Size(40, 20)
		Me.ToolStripMenuItem1.Text = "Help"
		'
		'AboutCrowbarToolStripMenuItem1
		'
		Me.AboutCrowbarToolStripMenuItem1.Name = "AboutCrowbarToolStripMenuItem1"
		Me.AboutCrowbarToolStripMenuItem1.Size = New System.Drawing.Size(155, 22)
		Me.AboutCrowbarToolStripMenuItem1.Text = "About Crowbar"
		'
		'MainTabControl
		'
		Me.MainTabControl.Controls.Add(Me.SetUpGamesTabPage)
		Me.MainTabControl.Controls.Add(Me.DownloadTabPage)
		Me.MainTabControl.Controls.Add(Me.UnpackTabPage)
		Me.MainTabControl.Controls.Add(Me.PreviewTabPage)
		Me.MainTabControl.Controls.Add(Me.DecompileTabPage)
		Me.MainTabControl.Controls.Add(Me.CompileTabPage)
		Me.MainTabControl.Controls.Add(Me.ViewTabPage)
		Me.MainTabControl.Controls.Add(Me.PackTabPage)
		Me.MainTabControl.Controls.Add(Me.PublishTabPage)
		Me.MainTabControl.Controls.Add(Me.OptionsTabPage)
		Me.MainTabControl.Controls.Add(Me.HelpTabPage)
		Me.MainTabControl.Controls.Add(Me.AboutTabPage)
		Me.MainTabControl.Controls.Add(Me.UpdateTabPage)
		Me.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill
		Me.MainTabControl.HotTrack = True
		Me.MainTabControl.Location = New System.Drawing.Point(0, 0)
		Me.MainTabControl.Name = "MainTabControl"
		Me.MainTabControl.Padding = New System.Drawing.Point(2, 3)
		Me.MainTabControl.SelectedIndex = 0
		Me.MainTabControl.ShowToolTips = True
		Me.MainTabControl.Size = New System.Drawing.Size(792, 572)
		Me.MainTabControl.TabBackColor1 = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
		Me.MainTabControl.TabBackColor2 = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
		Me.MainTabControl.TabIndex = 12
		Me.MainTabControl.TabPageBackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
		Me.MainTabControl.TabPageForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
		'
		'SetUpGamesTabPage
		'
		Me.SetUpGamesTabPage.BackColor = System.Drawing.SystemColors.Control
		Me.SetUpGamesTabPage.Controls.Add(Me.SetUpGamesUserControl1)
		Me.SetUpGamesTabPage.Location = New System.Drawing.Point(4, 25)
		Me.SetUpGamesTabPage.Name = "SetUpGamesTabPage"
		Me.SetUpGamesTabPage.Size = New System.Drawing.Size(784, 543)
		Me.SetUpGamesTabPage.TabIndex = 15
		Me.SetUpGamesTabPage.Text = "Set Up Games"
		'
		'SetUpGamesUserControl1
		'
		Me.SetUpGamesUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.SetUpGamesUserControl1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.SetUpGamesUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.SetUpGamesUserControl1.Name = "SetUpGamesUserControl1"
		Me.SetUpGamesUserControl1.Size = New System.Drawing.Size(784, 543)
		Me.SetUpGamesUserControl1.TabIndex = 0
		'
		'DownloadTabPage
		'
		Me.DownloadTabPage.BackColor = System.Drawing.SystemColors.Control
		Me.DownloadTabPage.Controls.Add(Me.DownloadUserControl1)
		Me.DownloadTabPage.Location = New System.Drawing.Point(4, 25)
		Me.DownloadTabPage.Name = "DownloadTabPage"
		Me.DownloadTabPage.Size = New System.Drawing.Size(784, 543)
		Me.DownloadTabPage.TabIndex = 0
		Me.DownloadTabPage.Text = "Download"
		'
		'DownloadUserControl1
		'
		Me.DownloadUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.DownloadUserControl1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.DownloadUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.DownloadUserControl1.Name = "DownloadUserControl1"
		Me.DownloadUserControl1.Size = New System.Drawing.Size(784, 543)
		Me.DownloadUserControl1.TabIndex = 0
		'
		'UnpackTabPage
		'
		Me.UnpackTabPage.BackColor = System.Drawing.SystemColors.Control
		Me.UnpackTabPage.Controls.Add(Me.UnpackUserControl1)
		Me.UnpackTabPage.Location = New System.Drawing.Point(4, 25)
		Me.UnpackTabPage.Name = "UnpackTabPage"
		Me.UnpackTabPage.Size = New System.Drawing.Size(784, 543)
		Me.UnpackTabPage.TabIndex = 13
		Me.UnpackTabPage.Text = "Unpack"
		'
		'UnpackUserControl1
		'
		Me.UnpackUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.UnpackUserControl1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.UnpackUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.UnpackUserControl1.Name = "UnpackUserControl1"
		Me.UnpackUserControl1.Size = New System.Drawing.Size(784, 543)
		Me.UnpackUserControl1.TabIndex = 0
		'
		'PreviewTabPage
		'
		Me.PreviewTabPage.BackColor = System.Drawing.SystemColors.Control
		Me.PreviewTabPage.Controls.Add(Me.PreviewViewUserControl)
		Me.PreviewTabPage.Location = New System.Drawing.Point(4, 25)
		Me.PreviewTabPage.Name = "PreviewTabPage"
		Me.PreviewTabPage.Size = New System.Drawing.Size(784, 543)
		Me.PreviewTabPage.TabIndex = 12
		Me.PreviewTabPage.Text = "Preview"
		'
		'PreviewViewUserControl
		'
		Me.PreviewViewUserControl.BackColor = System.Drawing.SystemColors.Control
		Me.PreviewViewUserControl.Dock = System.Windows.Forms.DockStyle.Fill
		Me.PreviewViewUserControl.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.PreviewViewUserControl.Location = New System.Drawing.Point(0, 0)
		Me.PreviewViewUserControl.Name = "PreviewViewUserControl"
		Me.PreviewViewUserControl.Size = New System.Drawing.Size(784, 543)
		Me.PreviewViewUserControl.TabIndex = 1
		Me.PreviewViewUserControl.ViewerType = Crowbar.AppEnums.ViewerType.Preview
		'
		'DecompileTabPage
		'
		Me.DecompileTabPage.BackColor = System.Drawing.SystemColors.Control
		Me.DecompileTabPage.Controls.Add(Me.DecompilerUserControl1)
		Me.DecompileTabPage.Location = New System.Drawing.Point(4, 25)
		Me.DecompileTabPage.Name = "DecompileTabPage"
		Me.DecompileTabPage.Size = New System.Drawing.Size(784, 543)
		Me.DecompileTabPage.TabIndex = 0
		Me.DecompileTabPage.Text = "Decompile"
		'
		'DecompilerUserControl1
		'
		Me.DecompilerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.DecompilerUserControl1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.DecompilerUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.DecompilerUserControl1.Name = "DecompilerUserControl1"
		Me.DecompilerUserControl1.Size = New System.Drawing.Size(784, 543)
		Me.DecompilerUserControl1.TabIndex = 0
		'
		'CompileTabPage
		'
		Me.CompileTabPage.BackColor = System.Drawing.SystemColors.Control
		Me.CompileTabPage.Controls.Add(Me.CompilerUserControl1)
		Me.CompileTabPage.Location = New System.Drawing.Point(4, 25)
		Me.CompileTabPage.Name = "CompileTabPage"
		Me.CompileTabPage.Size = New System.Drawing.Size(784, 543)
		Me.CompileTabPage.TabIndex = 1
		Me.CompileTabPage.Text = "Compile"
		'
		'CompilerUserControl1
		'
		Me.CompilerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.CompilerUserControl1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.CompilerUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.CompilerUserControl1.Name = "CompilerUserControl1"
		Me.CompilerUserControl1.Size = New System.Drawing.Size(784, 543)
		Me.CompilerUserControl1.TabIndex = 0
		'
		'ViewTabPage
		'
		Me.ViewTabPage.BackColor = System.Drawing.SystemColors.Control
		Me.ViewTabPage.Controls.Add(Me.ViewViewUserControl)
		Me.ViewTabPage.Location = New System.Drawing.Point(4, 25)
		Me.ViewTabPage.Name = "ViewTabPage"
		Me.ViewTabPage.Size = New System.Drawing.Size(784, 543)
		Me.ViewTabPage.TabIndex = 5
		Me.ViewTabPage.Text = "View"
		'
		'ViewViewUserControl
		'
		Me.ViewViewUserControl.BackColor = System.Drawing.SystemColors.Control
		Me.ViewViewUserControl.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ViewViewUserControl.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.ViewViewUserControl.Location = New System.Drawing.Point(0, 0)
		Me.ViewViewUserControl.Name = "ViewViewUserControl"
		Me.ViewViewUserControl.Size = New System.Drawing.Size(784, 543)
		Me.ViewViewUserControl.TabIndex = 0
		Me.ViewViewUserControl.ViewerType = Crowbar.AppEnums.ViewerType.View
		'
		'PackTabPage
		'
		Me.PackTabPage.BackColor = System.Drawing.SystemColors.Control
		Me.PackTabPage.Controls.Add(Me.PackUserControl1)
		Me.PackTabPage.Location = New System.Drawing.Point(4, 25)
		Me.PackTabPage.Name = "PackTabPage"
		Me.PackTabPage.Size = New System.Drawing.Size(784, 543)
		Me.PackTabPage.TabIndex = 16
		Me.PackTabPage.Text = "Pack"
		'
		'PackUserControl1
		'
		Me.PackUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.PackUserControl1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.PackUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.PackUserControl1.Name = "PackUserControl1"
		Me.PackUserControl1.Size = New System.Drawing.Size(784, 543)
		Me.PackUserControl1.TabIndex = 0
		'
		'PublishTabPage
		'
		Me.PublishTabPage.BackColor = System.Drawing.SystemColors.Control
		Me.PublishTabPage.Controls.Add(Me.PublishUserControl1)
		Me.PublishTabPage.Location = New System.Drawing.Point(4, 25)
		Me.PublishTabPage.Name = "PublishTabPage"
		Me.PublishTabPage.Size = New System.Drawing.Size(784, 543)
		Me.PublishTabPage.TabIndex = 1
		Me.PublishTabPage.Text = "Publish"
		'
		'PublishUserControl1
		'
		Me.PublishUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.PublishUserControl1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.PublishUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.PublishUserControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.PublishUserControl1.Name = "PublishUserControl1"
		Me.PublishUserControl1.Size = New System.Drawing.Size(784, 543)
		Me.PublishUserControl1.TabIndex = 0
		'
		'OptionsTabPage
		'
		Me.OptionsTabPage.BackColor = System.Drawing.SystemColors.Control
		Me.OptionsTabPage.Controls.Add(Me.OptionsUserControl1)
		Me.OptionsTabPage.Location = New System.Drawing.Point(4, 25)
		Me.OptionsTabPage.Name = "OptionsTabPage"
		Me.OptionsTabPage.Size = New System.Drawing.Size(784, 543)
		Me.OptionsTabPage.TabIndex = 10
		Me.OptionsTabPage.Text = "Options"
		'
		'OptionsUserControl1
		'
		Me.OptionsUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.OptionsUserControl1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.OptionsUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.OptionsUserControl1.Name = "OptionsUserControl1"
		Me.OptionsUserControl1.Size = New System.Drawing.Size(784, 543)
		Me.OptionsUserControl1.TabIndex = 0
		'
		'HelpTabPage
		'
		Me.HelpTabPage.BackColor = System.Drawing.SystemColors.Control
		Me.HelpTabPage.Controls.Add(Me.HelpUserControl1)
		Me.HelpTabPage.Location = New System.Drawing.Point(4, 25)
		Me.HelpTabPage.Name = "HelpTabPage"
		Me.HelpTabPage.Size = New System.Drawing.Size(784, 543)
		Me.HelpTabPage.TabIndex = 14
		Me.HelpTabPage.Text = "Help"
		'
		'HelpUserControl1
		'
		Me.HelpUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.HelpUserControl1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.HelpUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.HelpUserControl1.Name = "HelpUserControl1"
		Me.HelpUserControl1.Size = New System.Drawing.Size(784, 543)
		Me.HelpUserControl1.TabIndex = 0
		'
		'AboutTabPage
		'
		Me.AboutTabPage.BackColor = System.Drawing.SystemColors.Control
		Me.AboutTabPage.Controls.Add(Me.AboutUserControl1)
		Me.AboutTabPage.Location = New System.Drawing.Point(4, 25)
		Me.AboutTabPage.Name = "AboutTabPage"
		Me.AboutTabPage.Size = New System.Drawing.Size(784, 543)
		Me.AboutTabPage.TabIndex = 11
		Me.AboutTabPage.Text = "About"
		'
		'AboutUserControl1
		'
		Me.AboutUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.AboutUserControl1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.AboutUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.AboutUserControl1.Name = "AboutUserControl1"
		Me.AboutUserControl1.Size = New System.Drawing.Size(784, 543)
		Me.AboutUserControl1.TabIndex = 1
		'
		'UpdateTabPage
		'
		Me.UpdateTabPage.BackColor = System.Drawing.SystemColors.Control
		Me.UpdateTabPage.Controls.Add(Me.UpdateUserControl1)
		Me.UpdateTabPage.Location = New System.Drawing.Point(4, 25)
		Me.UpdateTabPage.Name = "UpdateTabPage"
		Me.UpdateTabPage.Size = New System.Drawing.Size(784, 543)
		Me.UpdateTabPage.TabIndex = 19
		Me.UpdateTabPage.Text = "Update"
		'
		'UpdateUserControl1
		'
		Me.UpdateUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.UpdateUserControl1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.UpdateUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.UpdateUserControl1.Name = "UpdateUserControl1"
		Me.UpdateUserControl1.Size = New System.Drawing.Size(784, 543)
		Me.UpdateUserControl1.TabIndex = 0
		'
		'ToolStripMenuItem2
		'
		Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
		Me.ToolStripMenuItem2.Size = New System.Drawing.Size(40, 20)
		Me.ToolStripMenuItem2.Text = "Help"
		'
		'AboutCrowbarToolStripMenuItem2
		'
		Me.AboutCrowbarToolStripMenuItem2.Name = "AboutCrowbarToolStripMenuItem2"
		Me.AboutCrowbarToolStripMenuItem2.Size = New System.Drawing.Size(152, 22)
		Me.AboutCrowbarToolStripMenuItem2.Text = "About Crowbar"
		'
		'MainForm
		'
		Me.AllowDrop = True
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(792, 572)
		Me.Controls.Add(Me.MainTabControl)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Location = New System.Drawing.Point(0, 0)
		Me.MinimumSize = New System.Drawing.Size(800, 600)
		Me.Name = "MainForm"
		Me.Text = "Crowbar"
		Me.MainTabControl.ResumeLayout(False)
		Me.SetUpGamesTabPage.ResumeLayout(False)
		Me.DownloadTabPage.ResumeLayout(False)
		Me.UnpackTabPage.ResumeLayout(False)
		Me.PreviewTabPage.ResumeLayout(False)
		Me.DecompileTabPage.ResumeLayout(False)
		Me.CompileTabPage.ResumeLayout(False)
		Me.ViewTabPage.ResumeLayout(False)
		Me.PackTabPage.ResumeLayout(False)
		Me.PublishTabPage.ResumeLayout(False)
		Me.OptionsTabPage.ResumeLayout(False)
		Me.HelpTabPage.ResumeLayout(False)
		Me.AboutTabPage.ResumeLayout(False)
		Me.UpdateTabPage.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents MainTabControl As Crowbar.TabControlEx
	Friend WithEvents MainToolTip As System.Windows.Forms.ToolTip
	Friend WithEvents SetUpGamesTabPage As System.Windows.Forms.TabPage
	Friend WithEvents SetUpGamesUserControl1 As Crowbar.SetUpGamesUserControl
	Friend WithEvents DownloadTabPage As TabPage
	Friend WithEvents DownloadUserControl1 As DownloadUserControl
	Friend WithEvents UnpackTabPage As System.Windows.Forms.TabPage
	Friend WithEvents UnpackUserControl1 As Crowbar.UnpackUserControl
	Friend WithEvents PreviewTabPage As System.Windows.Forms.TabPage
	Friend WithEvents PreviewViewUserControl As Crowbar.ViewUserControl
	Friend WithEvents DecompileTabPage As System.Windows.Forms.TabPage
	Friend WithEvents DecompilerUserControl1 As Crowbar.DecompileUserControl
	Friend WithEvents CompileTabPage As System.Windows.Forms.TabPage
	Friend WithEvents CompilerUserControl1 As Crowbar.CompileUserControl
	Friend WithEvents ViewTabPage As System.Windows.Forms.TabPage
	Friend WithEvents ViewViewUserControl As Crowbar.ViewUserControl
	Friend WithEvents PackTabPage As TabPage
	Friend WithEvents PackUserControl1 As PackUserControl
	Friend WithEvents PublishTabPage As TabPage
	Friend WithEvents PublishUserControl1 As PublishUserControl
	Friend WithEvents OptionsTabPage As System.Windows.Forms.TabPage
	Friend WithEvents OptionsUserControl1 As Crowbar.OptionsUserControl
	Friend WithEvents HelpTabPage As System.Windows.Forms.TabPage
	Friend WithEvents HelpUserControl1 As Crowbar.HelpUserControl
	Friend WithEvents AboutTabPage As System.Windows.Forms.TabPage
	Friend WithEvents AboutUserControl1 As Crowbar.AboutUserControl
	Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
	Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents AboutCrowbarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents AboutCrowbarToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents AboutCrowbarToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents UpdateTabPage As TabPage
	Friend WithEvents UpdateUserControl1 As UpdateUserControl
End Class
