<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TESTFORM
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
		Me.TabControlEx1 = New Crowbar.TabControlEx()
		Me.TabPage1 = New System.Windows.Forms.TabPage()
		Me.PanelEx1 = New Crowbar.PanelEx()
		Me.CheckBoxEx4 = New Crowbar.CheckBoxEx()
		Me.CheckBoxEx3 = New Crowbar.CheckBoxEx()
		Me.CheckBoxEx2 = New Crowbar.CheckBoxEx()
		Me.CheckBoxEx1 = New Crowbar.CheckBoxEx()
		Me.TabPage2 = New System.Windows.Forms.TabPage()
		Me.TabControl1 = New System.Windows.Forms.TabControl()
		Me.TabPage3 = New System.Windows.Forms.TabPage()
		Me.DataGridView1 = New System.Windows.Forms.DataGridView()
		Me.TextBox1 = New System.Windows.Forms.TextBox()
		Me.Button1 = New System.Windows.Forms.Button()
		Me.ComboBox1 = New System.Windows.Forms.ComboBox()
		Me.TabPage4 = New System.Windows.Forms.TabPage()
		Me.TreeView1 = New System.Windows.Forms.TreeView()
		Me.ListView1 = New System.Windows.Forms.ListView()
		Me.TabControlEx1.SuspendLayout()
		Me.TabPage1.SuspendLayout()
		Me.PanelEx1.SuspendLayout()
		Me.TabControl1.SuspendLayout()
		Me.TabPage3.SuspendLayout()
		CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'TabControlEx1
		'
		Me.TabControlEx1.Controls.Add(Me.TabPage1)
		Me.TabControlEx1.Controls.Add(Me.TabPage2)
		Me.TabControlEx1.Dock = System.Windows.Forms.DockStyle.Top
		Me.TabControlEx1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.TabControlEx1.HotTrack = True
		Me.TabControlEx1.Location = New System.Drawing.Point(0, 0)
		Me.TabControlEx1.Name = "TabControlEx1"
		Me.TabControlEx1.SelectedIndex = 0
		Me.TabControlEx1.ShowToolTips = True
		Me.TabControlEx1.Size = New System.Drawing.Size(800, 225)
		Me.TabControlEx1.TabIndex = 0
		'
		'TabPage1
		'
		Me.TabPage1.Controls.Add(Me.PanelEx1)
		Me.TabPage1.Location = New System.Drawing.Point(4, 22)
		Me.TabPage1.Name = "TabPage1"
		Me.TabPage1.Size = New System.Drawing.Size(792, 199)
		Me.TabPage1.TabIndex = 0
		Me.TabPage1.Text = "TabPage1"
		Me.TabPage1.UseVisualStyleBackColor = True
		'
		'PanelEx1
		'
		Me.PanelEx1.AutoScroll = True
		Me.PanelEx1.Controls.Add(Me.CheckBoxEx4)
		Me.PanelEx1.Controls.Add(Me.CheckBoxEx3)
		Me.PanelEx1.Controls.Add(Me.CheckBoxEx2)
		Me.PanelEx1.Controls.Add(Me.CheckBoxEx1)
		Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.PanelEx1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
		Me.PanelEx1.Name = "PanelEx1"
		Me.PanelEx1.Size = New System.Drawing.Size(792, 199)
		Me.PanelEx1.TabIndex = 0
		'
		'CheckBoxEx4
		'
		Me.CheckBoxEx4.AutoSize = True
		Me.CheckBoxEx4.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.CheckBoxEx4.IsReadOnly = False
		Me.CheckBoxEx4.Location = New System.Drawing.Point(688, 364)
		Me.CheckBoxEx4.Name = "CheckBoxEx4"
		Me.CheckBoxEx4.Size = New System.Drawing.Size(93, 17)
		Me.CheckBoxEx4.TabIndex = 11
		Me.CheckBoxEx4.Text = "CheckBoxEx4"
		Me.CheckBoxEx4.UseVisualStyleBackColor = True
		'
		'CheckBoxEx3
		'
		Me.CheckBoxEx3.AutoSize = True
		Me.CheckBoxEx3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.CheckBoxEx3.IsReadOnly = False
		Me.CheckBoxEx3.Location = New System.Drawing.Point(27, 364)
		Me.CheckBoxEx3.Name = "CheckBoxEx3"
		Me.CheckBoxEx3.Size = New System.Drawing.Size(93, 17)
		Me.CheckBoxEx3.TabIndex = 10
		Me.CheckBoxEx3.Text = "CheckBoxEx3"
		Me.CheckBoxEx3.UseVisualStyleBackColor = True
		'
		'CheckBoxEx2
		'
		Me.CheckBoxEx2.AutoSize = True
		Me.CheckBoxEx2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.CheckBoxEx2.IsReadOnly = False
		Me.CheckBoxEx2.Location = New System.Drawing.Point(688, 33)
		Me.CheckBoxEx2.Name = "CheckBoxEx2"
		Me.CheckBoxEx2.Size = New System.Drawing.Size(93, 17)
		Me.CheckBoxEx2.TabIndex = 9
		Me.CheckBoxEx2.Text = "CheckBoxEx2"
		Me.CheckBoxEx2.UseVisualStyleBackColor = True
		'
		'CheckBoxEx1
		'
		Me.CheckBoxEx1.AutoSize = True
		Me.CheckBoxEx1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.CheckBoxEx1.IsReadOnly = False
		Me.CheckBoxEx1.Location = New System.Drawing.Point(27, 33)
		Me.CheckBoxEx1.Name = "CheckBoxEx1"
		Me.CheckBoxEx1.Size = New System.Drawing.Size(93, 17)
		Me.CheckBoxEx1.TabIndex = 8
		Me.CheckBoxEx1.Text = "CheckBoxEx1"
		Me.CheckBoxEx1.UseVisualStyleBackColor = True
		'
		'TabPage2
		'
		Me.TabPage2.Location = New System.Drawing.Point(4, 22)
		Me.TabPage2.Name = "TabPage2"
		Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage2.Size = New System.Drawing.Size(792, 199)
		Me.TabPage2.TabIndex = 1
		Me.TabPage2.Text = "TabPage2"
		Me.TabPage2.UseVisualStyleBackColor = True
		'
		'TabControl1
		'
		Me.TabControl1.Controls.Add(Me.TabPage3)
		Me.TabControl1.Controls.Add(Me.TabPage4)
		Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.TabControl1.Location = New System.Drawing.Point(0, 246)
		Me.TabControl1.Name = "TabControl1"
		Me.TabControl1.SelectedIndex = 0
		Me.TabControl1.Size = New System.Drawing.Size(800, 204)
		Me.TabControl1.TabIndex = 1
		'
		'TabPage3
		'
		Me.TabPage3.Controls.Add(Me.ListView1)
		Me.TabPage3.Controls.Add(Me.TreeView1)
		Me.TabPage3.Controls.Add(Me.DataGridView1)
		Me.TabPage3.Controls.Add(Me.TextBox1)
		Me.TabPage3.Controls.Add(Me.Button1)
		Me.TabPage3.Controls.Add(Me.ComboBox1)
		Me.TabPage3.Location = New System.Drawing.Point(4, 22)
		Me.TabPage3.Name = "TabPage3"
		Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage3.Size = New System.Drawing.Size(792, 178)
		Me.TabPage3.TabIndex = 0
		Me.TabPage3.Text = "TabPage3"
		Me.TabPage3.UseVisualStyleBackColor = True
		'
		'DataGridView1
		'
		Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.DataGridView1.Location = New System.Drawing.Point(214, 6)
		Me.DataGridView1.Name = "DataGridView1"
		Me.DataGridView1.Size = New System.Drawing.Size(240, 150)
		Me.DataGridView1.TabIndex = 3
		'
		'TextBox1
		'
		Me.TextBox1.Location = New System.Drawing.Point(6, 33)
		Me.TextBox1.Name = "TextBox1"
		Me.TextBox1.Size = New System.Drawing.Size(100, 22)
		Me.TextBox1.TabIndex = 2
		'
		'Button1
		'
		Me.Button1.Location = New System.Drawing.Point(133, 6)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(75, 23)
		Me.Button1.TabIndex = 1
		Me.Button1.Text = "Button1"
		Me.Button1.UseVisualStyleBackColor = True
		'
		'ComboBox1
		'
		Me.ComboBox1.FormattingEnabled = True
		Me.ComboBox1.Location = New System.Drawing.Point(6, 6)
		Me.ComboBox1.Name = "ComboBox1"
		Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
		Me.ComboBox1.TabIndex = 0
		'
		'TabPage4
		'
		Me.TabPage4.Location = New System.Drawing.Point(4, 22)
		Me.TabPage4.Name = "TabPage4"
		Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage4.Size = New System.Drawing.Size(792, 178)
		Me.TabPage4.TabIndex = 1
		Me.TabPage4.Text = "TabPage4"
		Me.TabPage4.UseVisualStyleBackColor = True
		'
		'TreeView1
		'
		Me.TreeView1.Location = New System.Drawing.Point(460, 6)
		Me.TreeView1.Name = "TreeView1"
		Me.TreeView1.Size = New System.Drawing.Size(121, 97)
		Me.TreeView1.TabIndex = 4
		'
		'ListView1
		'
		Me.ListView1.HideSelection = False
		Me.ListView1.Location = New System.Drawing.Point(587, 6)
		Me.ListView1.Name = "ListView1"
		Me.ListView1.Size = New System.Drawing.Size(121, 97)
		Me.ListView1.TabIndex = 5
		Me.ListView1.UseCompatibleStateImageBehavior = False
		'
		'TESTFORM
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(800, 450)
		Me.Controls.Add(Me.TabControl1)
		Me.Controls.Add(Me.TabControlEx1)
		Me.Location = New System.Drawing.Point(0, 0)
		Me.Name = "TESTFORM"
		Me.Text = "TESTFORM"
		Me.TabControlEx1.ResumeLayout(False)
		Me.TabPage1.ResumeLayout(False)
		Me.PanelEx1.ResumeLayout(False)
		Me.PanelEx1.PerformLayout()
		Me.TabControl1.ResumeLayout(False)
		Me.TabPage3.ResumeLayout(False)
		Me.TabPage3.PerformLayout()
		CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents TabControlEx1 As TabControlEx
	Friend WithEvents TabPage1 As TabPage
	Friend WithEvents TabPage2 As TabPage
	Friend WithEvents PanelEx1 As PanelEx
	Friend WithEvents CheckBoxEx4 As CheckBoxEx
	Friend WithEvents CheckBoxEx3 As CheckBoxEx
	Friend WithEvents CheckBoxEx2 As CheckBoxEx
	Friend WithEvents CheckBoxEx1 As CheckBoxEx
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Button1 As Button
    Friend WithEvents ComboBox1 As ComboBox
	Friend WithEvents TextBox1 As TextBox
	Friend WithEvents DataGridView1 As DataGridView
	Friend WithEvents ListView1 As ListView
	Friend WithEvents TreeView1 As TreeView
End Class
