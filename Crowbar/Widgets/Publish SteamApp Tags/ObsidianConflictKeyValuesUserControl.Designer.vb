<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ObsidianConflictKeyValuesUserControl
	Inherits Base_KeyValuesUserControl

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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ObsidianConflictKeyValuesUserControl))
		Me.MountDependenciesGroupBox = New Crowbar.GroupBoxEx()
		Me.MapPanel = New System.Windows.Forms.Panel()
		Me.DoDCheckBox = New Crowbar.CheckBoxEx()
		Me.TFCheckBox = New Crowbar.CheckBoxEx()
		Me.HL2MPCheckBox = New Crowbar.CheckBoxEx()
		Me.CStrikeCheckBox = New Crowbar.CheckBoxEx()
		Me.EpisodicCheckBox = New Crowbar.CheckBoxEx()
		Me.MountDependencyHL2CheckBox = New Crowbar.CheckBoxEx()
		Me.LostCoastCheckBox = New Crowbar.CheckBoxEx()
		Me.Ep2CheckBox = New Crowbar.CheckBoxEx()
		Me.VersionLabel = New System.Windows.Forms.Label()
		Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.VersionKeyValueTextBox = New System.Windows.Forms.TextBox()
		Me.CheckBoxEx1 = New Crowbar.CheckBoxEx()
		Me.CheckBoxEx2 = New Crowbar.CheckBoxEx()
		Me.CheckBoxEx3 = New Crowbar.CheckBoxEx()
		Me.OverrideModeCheckBox = New Crowbar.CheckBoxEx()
		Me.MountDependenciesGroupBox.SuspendLayout()
		Me.MapPanel.SuspendLayout()
		Me.SuspendLayout()
		'
		'MountDependenciesGroupBox
		'
		Me.MountDependenciesGroupBox.Controls.Add(Me.MapPanel)
		Me.MountDependenciesGroupBox.IsReadOnly = False
		Me.MountDependenciesGroupBox.Location = New System.Drawing.Point(6, 60)
		Me.MountDependenciesGroupBox.Name = "MountDependenciesGroupBox"
		Me.MountDependenciesGroupBox.SelectedValue = Nothing
		Me.MountDependenciesGroupBox.Size = New System.Drawing.Size(225, 119)
		Me.MountDependenciesGroupBox.TabIndex = 13
		Me.MountDependenciesGroupBox.TabStop = False
		Me.MountDependenciesGroupBox.Text = "Mount Dependencies"
		'
		'MapPanel
		'
		Me.MapPanel.Controls.Add(Me.DoDCheckBox)
		Me.MapPanel.Controls.Add(Me.TFCheckBox)
		Me.MapPanel.Controls.Add(Me.HL2MPCheckBox)
		Me.MapPanel.Controls.Add(Me.CStrikeCheckBox)
		Me.MapPanel.Controls.Add(Me.EpisodicCheckBox)
		Me.MapPanel.Controls.Add(Me.MountDependencyHL2CheckBox)
		Me.MapPanel.Controls.Add(Me.LostCoastCheckBox)
		Me.MapPanel.Controls.Add(Me.Ep2CheckBox)
		Me.MapPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.MapPanel.Enabled = False
		Me.MapPanel.Location = New System.Drawing.Point(3, 18)
		Me.MapPanel.Name = "MapPanel"
		Me.MapPanel.Size = New System.Drawing.Size(219, 98)
		Me.MapPanel.TabIndex = 1
		'
		'DoDCheckBox
		'
		Me.DoDCheckBox.AutoSize = True
		Me.DoDCheckBox.IsReadOnly = False
		Me.DoDCheckBox.Location = New System.Drawing.Point(112, 49)
		Me.DoDCheckBox.Name = "DoDCheckBox"
		Me.DoDCheckBox.Size = New System.Drawing.Size(47, 17)
		Me.DoDCheckBox.TabIndex = 14
		Me.DoDCheckBox.Tag = "mount=dod"
		Me.DoDCheckBox.Text = "dod"
		Me.DoDCheckBox.UseVisualStyleBackColor = True
		'
		'TFCheckBox
		'
		Me.TFCheckBox.AutoSize = True
		Me.TFCheckBox.IsReadOnly = False
		Me.TFCheckBox.Location = New System.Drawing.Point(112, 72)
		Me.TFCheckBox.Name = "TFCheckBox"
		Me.TFCheckBox.Size = New System.Drawing.Size(34, 17)
		Me.TFCheckBox.TabIndex = 15
		Me.TFCheckBox.Tag = "mount=tf"
		Me.TFCheckBox.Text = "tf"
		Me.TFCheckBox.UseVisualStyleBackColor = True
		'
		'HL2MPCheckBox
		'
		Me.HL2MPCheckBox.AutoSize = True
		Me.HL2MPCheckBox.IsReadOnly = False
		Me.HL2MPCheckBox.Location = New System.Drawing.Point(112, 3)
		Me.HL2MPCheckBox.Name = "HL2MPCheckBox"
		Me.HL2MPCheckBox.Size = New System.Drawing.Size(58, 17)
		Me.HL2MPCheckBox.TabIndex = 12
		Me.HL2MPCheckBox.Tag = "mount=hl2mp"
		Me.HL2MPCheckBox.Text = "hl2mp"
		Me.HL2MPCheckBox.UseVisualStyleBackColor = True
		'
		'CStrikeCheckBox
		'
		Me.CStrikeCheckBox.AutoSize = True
		Me.CStrikeCheckBox.IsReadOnly = False
		Me.CStrikeCheckBox.Location = New System.Drawing.Point(112, 26)
		Me.CStrikeCheckBox.Name = "CStrikeCheckBox"
		Me.CStrikeCheckBox.Size = New System.Drawing.Size(59, 17)
		Me.CStrikeCheckBox.TabIndex = 13
		Me.CStrikeCheckBox.Tag = "mount=cstrike"
		Me.CStrikeCheckBox.Text = "cstrike"
		Me.CStrikeCheckBox.UseVisualStyleBackColor = True
		'
		'EpisodicCheckBox
		'
		Me.EpisodicCheckBox.AutoSize = True
		Me.EpisodicCheckBox.IsReadOnly = False
		Me.EpisodicCheckBox.Location = New System.Drawing.Point(3, 49)
		Me.EpisodicCheckBox.Name = "EpisodicCheckBox"
		Me.EpisodicCheckBox.Size = New System.Drawing.Size(69, 17)
		Me.EpisodicCheckBox.TabIndex = 7
		Me.EpisodicCheckBox.Tag = "mount=episodic"
		Me.EpisodicCheckBox.Text = "episodic"
		Me.EpisodicCheckBox.UseVisualStyleBackColor = True
		'
		'MountDependencyHL2CheckBox
		'
		Me.MountDependencyHL2CheckBox.AutoSize = True
		Me.MountDependencyHL2CheckBox.IsReadOnly = False
		Me.MountDependencyHL2CheckBox.Location = New System.Drawing.Point(3, 3)
		Me.MountDependencyHL2CheckBox.Name = "MountDependencyHL2CheckBox"
		Me.MountDependencyHL2CheckBox.Size = New System.Drawing.Size(42, 17)
		Me.MountDependencyHL2CheckBox.TabIndex = 4
		Me.MountDependencyHL2CheckBox.Tag = "mount=hl2"
		Me.MountDependencyHL2CheckBox.Text = "hl2"
		Me.MountDependencyHL2CheckBox.UseVisualStyleBackColor = True
		'
		'LostCoastCheckBox
		'
		Me.LostCoastCheckBox.AutoSize = True
		Me.LostCoastCheckBox.IsReadOnly = False
		Me.LostCoastCheckBox.Location = New System.Drawing.Point(3, 26)
		Me.LostCoastCheckBox.Name = "LostCoastCheckBox"
		Me.LostCoastCheckBox.Size = New System.Drawing.Size(72, 17)
		Me.LostCoastCheckBox.TabIndex = 5
		Me.LostCoastCheckBox.Tag = "mount=lostcoast"
		Me.LostCoastCheckBox.Text = "lostcoast"
		Me.LostCoastCheckBox.UseVisualStyleBackColor = True
		'
		'Ep2CheckBox
		'
		Me.Ep2CheckBox.AutoSize = True
		Me.Ep2CheckBox.IsReadOnly = False
		Me.Ep2CheckBox.Location = New System.Drawing.Point(3, 72)
		Me.Ep2CheckBox.Name = "Ep2CheckBox"
		Me.Ep2CheckBox.Size = New System.Drawing.Size(45, 17)
		Me.Ep2CheckBox.TabIndex = 6
		Me.Ep2CheckBox.Tag = "mount=ep2"
		Me.Ep2CheckBox.Text = "ep2"
		Me.Ep2CheckBox.UseVisualStyleBackColor = True
		'
		'VersionLabel
		'
		Me.VersionLabel.AutoSize = True
		Me.VersionLabel.Location = New System.Drawing.Point(3, 13)
		Me.VersionLabel.Name = "VersionLabel"
		Me.VersionLabel.Size = New System.Drawing.Size(48, 13)
		Me.VersionLabel.TabIndex = 12
		Me.VersionLabel.Text = "Version:"
		'
		'LinkLabel1
		'
		Me.LinkLabel1.AutoSize = True
		Me.LinkLabel1.Location = New System.Drawing.Point(542, 13)
		Me.LinkLabel1.Name = "LinkLabel1"
		Me.LinkLabel1.Size = New System.Drawing.Size(44, 13)
		Me.LinkLabel1.TabIndex = 10
		Me.LinkLabel1.TabStop = True
		Me.LinkLabel1.Text = "SemVer"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(165, 13)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(371, 13)
		Me.Label1.TabIndex = 9
		Me.Label1.Text = "Recommended format: <major>.<minor>.<patch>[.<build>][-<meta>]"
		'
		'VersionKeyValueTextBox
		'
		Me.VersionKeyValueTextBox.Location = New System.Drawing.Point(59, 9)
		Me.VersionKeyValueTextBox.Name = "VersionKeyValueTextBox"
		Me.VersionKeyValueTextBox.Size = New System.Drawing.Size(100, 22)
		Me.VersionKeyValueTextBox.TabIndex = 8
		Me.VersionKeyValueTextBox.Tag = "version="
		'
		'CheckBoxEx1
		'
		Me.CheckBoxEx1.AutoSize = True
		Me.CheckBoxEx1.IsReadOnly = False
		Me.CheckBoxEx1.Location = New System.Drawing.Point(234, 81)
		Me.CheckBoxEx1.Name = "CheckBoxEx1"
		Me.CheckBoxEx1.Size = New System.Drawing.Size(103, 17)
		Me.CheckBoxEx1.TabIndex = 16
		Me.CheckBoxEx1.Tag = ""
		Me.CheckBoxEx1.Text = "TEST - youtube"
		Me.CheckBoxEx1.UseVisualStyleBackColor = True
		Me.CheckBoxEx1.Visible = False
		'
		'CheckBoxEx2
		'
		Me.CheckBoxEx2.AutoSize = True
		Me.CheckBoxEx2.IsReadOnly = False
		Me.CheckBoxEx2.Location = New System.Drawing.Point(234, 104)
		Me.CheckBoxEx2.Name = "CheckBoxEx2"
		Me.CheckBoxEx2.Size = New System.Drawing.Size(94, 17)
		Me.CheckBoxEx2.TabIndex = 17
		Me.CheckBoxEx2.Tag = "twitter="
		Me.CheckBoxEx2.Text = "TEST - twitter"
		Me.CheckBoxEx2.UseVisualStyleBackColor = True
		Me.CheckBoxEx2.Visible = False
		'
		'CheckBoxEx3
		'
		Me.CheckBoxEx3.AutoSize = True
		Me.CheckBoxEx3.IsReadOnly = False
		Me.CheckBoxEx3.Location = New System.Drawing.Point(234, 127)
		Me.CheckBoxEx3.Name = "CheckBoxEx3"
		Me.CheckBoxEx3.Size = New System.Drawing.Size(103, 17)
		Me.CheckBoxEx3.TabIndex = 18
		Me.CheckBoxEx3.Tag = "youtube=test"
		Me.CheckBoxEx3.Text = "TEST - youtube"
		Me.CheckBoxEx3.UseVisualStyleBackColor = True
		Me.CheckBoxEx3.Visible = False
		'
		'OverrideModeCheckBox
		'
		Me.OverrideModeCheckBox.AutoSize = True
		Me.OverrideModeCheckBox.IsReadOnly = False
		Me.OverrideModeCheckBox.Location = New System.Drawing.Point(6, 37)
		Me.OverrideModeCheckBox.Name = "OverrideModeCheckBox"
		Me.OverrideModeCheckBox.Size = New System.Drawing.Size(102, 17)
		Me.OverrideModeCheckBox.TabIndex = 19
		Me.OverrideModeCheckBox.Tag = "override=true"
		Me.OverrideModeCheckBox.Text = "Override mode"
		Me.OverrideModeCheckBox.UseVisualStyleBackColor = True
		'
		'ObsidianConflictKeyValuesUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoScroll = True
		Me.Controls.Add(Me.OverrideModeCheckBox)
		Me.Controls.Add(Me.CheckBoxEx3)
		Me.Controls.Add(Me.CheckBoxEx2)
		Me.Controls.Add(Me.CheckBoxEx1)
		Me.Controls.Add(Me.MountDependenciesGroupBox)
		Me.Controls.Add(Me.VersionLabel)
		Me.Controls.Add(Me.LinkLabel1)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.VersionKeyValueTextBox)
		Me.ItemKeyValues = CType(resources.GetObject("$this.ItemKeyValues"), System.Collections.Generic.SortedList(Of String, System.Collections.Generic.List(Of String)))
		Me.Name = "ObsidianConflictKeyValuesUserControl"
		Me.Size = New System.Drawing.Size(631, 344)
		Me.MountDependenciesGroupBox.ResumeLayout(False)
		Me.MapPanel.ResumeLayout(False)
		Me.MapPanel.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents MountDependenciesGroupBox As GroupBoxEx
	Friend WithEvents MapPanel As Panel
	Friend WithEvents DoDCheckBox As CheckBoxEx
	Friend WithEvents TFCheckBox As CheckBoxEx
	Friend WithEvents HL2MPCheckBox As CheckBoxEx
	Friend WithEvents CStrikeCheckBox As CheckBoxEx
	Friend WithEvents EpisodicCheckBox As CheckBoxEx
	Friend WithEvents MountDependencyHL2CheckBox As CheckBoxEx
	Friend WithEvents LostCoastCheckBox As CheckBoxEx
	Friend WithEvents Ep2CheckBox As CheckBoxEx
	Friend WithEvents VersionLabel As Label
	Friend WithEvents LinkLabel1 As LinkLabel
	Friend WithEvents Label1 As Label
	Friend WithEvents VersionKeyValueTextBox As TextBox
	Friend WithEvents CheckBoxEx1 As CheckBoxEx
	Friend WithEvents CheckBoxEx2 As CheckBoxEx
	Friend WithEvents CheckBoxEx3 As CheckBoxEx
	Friend WithEvents OverrideModeCheckBox As CheckBoxEx
End Class
