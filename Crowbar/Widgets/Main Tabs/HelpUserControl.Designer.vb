<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HelpUserControl
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
		Me.TutorialLinkLabel = New System.Windows.Forms.LinkLabel()
		Me.ContentsLinkLabel = New System.Windows.Forms.LinkLabel()
		Me.IndexLinkLabel = New System.Windows.Forms.LinkLabel()
		Me.TipsLinkLabel = New System.Windows.Forms.LinkLabel()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.CrowbarGuideButton = New System.Windows.Forms.Button()
		Me.GroupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'TutorialLinkLabel
		'
		Me.TutorialLinkLabel.ActiveLinkColor = System.Drawing.Color.Lime
		Me.TutorialLinkLabel.AutoSize = True
		Me.TutorialLinkLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TutorialLinkLabel.LinkColor = System.Drawing.Color.Green
		Me.TutorialLinkLabel.Location = New System.Drawing.Point(6, 16)
		Me.TutorialLinkLabel.Name = "TutorialLinkLabel"
		Me.TutorialLinkLabel.Size = New System.Drawing.Size(84, 25)
		Me.TutorialLinkLabel.TabIndex = 1
		Me.TutorialLinkLabel.TabStop = True
		Me.TutorialLinkLabel.Text = "Tutorial"
		Me.TutorialLinkLabel.VisitedLinkColor = System.Drawing.Color.Green
		'
		'ContentsLinkLabel
		'
		Me.ContentsLinkLabel.ActiveLinkColor = System.Drawing.Color.Lime
		Me.ContentsLinkLabel.AutoSize = True
		Me.ContentsLinkLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ContentsLinkLabel.LinkColor = System.Drawing.Color.Green
		Me.ContentsLinkLabel.Location = New System.Drawing.Point(6, 41)
		Me.ContentsLinkLabel.Name = "ContentsLinkLabel"
		Me.ContentsLinkLabel.Size = New System.Drawing.Size(98, 25)
		Me.ContentsLinkLabel.TabIndex = 2
		Me.ContentsLinkLabel.TabStop = True
		Me.ContentsLinkLabel.Text = "Contents"
		Me.ContentsLinkLabel.VisitedLinkColor = System.Drawing.Color.Green
		'
		'IndexLinkLabel
		'
		Me.IndexLinkLabel.ActiveLinkColor = System.Drawing.Color.Lime
		Me.IndexLinkLabel.AutoSize = True
		Me.IndexLinkLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.IndexLinkLabel.LinkColor = System.Drawing.Color.Green
		Me.IndexLinkLabel.Location = New System.Drawing.Point(6, 66)
		Me.IndexLinkLabel.Name = "IndexLinkLabel"
		Me.IndexLinkLabel.Size = New System.Drawing.Size(64, 25)
		Me.IndexLinkLabel.TabIndex = 3
		Me.IndexLinkLabel.TabStop = True
		Me.IndexLinkLabel.Text = "Index"
		Me.IndexLinkLabel.VisitedLinkColor = System.Drawing.Color.Green
		'
		'TipsLinkLabel
		'
		Me.TipsLinkLabel.ActiveLinkColor = System.Drawing.Color.Lime
		Me.TipsLinkLabel.AutoSize = True
		Me.TipsLinkLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TipsLinkLabel.LinkColor = System.Drawing.Color.Green
		Me.TipsLinkLabel.Location = New System.Drawing.Point(6, 91)
		Me.TipsLinkLabel.Name = "TipsLinkLabel"
		Me.TipsLinkLabel.Size = New System.Drawing.Size(53, 25)
		Me.TipsLinkLabel.TabIndex = 4
		Me.TipsLinkLabel.TabStop = True
		Me.TipsLinkLabel.Text = "Tips"
		Me.TipsLinkLabel.VisitedLinkColor = System.Drawing.Color.Green
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.Label4)
		Me.GroupBox1.Controls.Add(Me.Label3)
		Me.GroupBox1.Controls.Add(Me.Label2)
		Me.GroupBox1.Controls.Add(Me.Label1)
		Me.GroupBox1.Controls.Add(Me.TutorialLinkLabel)
		Me.GroupBox1.Controls.Add(Me.TipsLinkLabel)
		Me.GroupBox1.Controls.Add(Me.ContentsLinkLabel)
		Me.GroupBox1.Controls.Add(Me.IndexLinkLabel)
		Me.GroupBox1.Location = New System.Drawing.Point(49, 388)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(640, 132)
		Me.GroupBox1.TabIndex = 5
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Visible = False
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Location = New System.Drawing.Point(110, 92)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(407, 24)
		Me.Label4.TabIndex = 8
		Me.Label4.Text = "Ways to use Crowbar that might not be obvious."
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Location = New System.Drawing.Point(110, 67)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(462, 24)
		Me.Label3.TabIndex = 7
		Me.Label3.Text = "Links to where important words and phrases are used."
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Location = New System.Drawing.Point(110, 42)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(317, 24)
		Me.Label2.TabIndex = 6
		Me.Label2.Text = "Documentation arranged in sections."
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(110, 17)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(464, 24)
		Me.Label1.TabIndex = 5
		Me.Label1.Text = "Step-by-step guide on how to use most of the features."
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Location = New System.Drawing.Point(49, 337)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(577, 16)
		Me.Label5.TabIndex = 7
		Me.Label5.Text = "Crowbar allows you to quickly access many tools for modding models for Source-eng" & _
	"ine games."
		Me.Label5.Visible = False
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label6.Location = New System.Drawing.Point(49, 361)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(419, 16)
		Me.Label6.TabIndex = 8
		Me.Label6.Text = "(The links below will open as web pages in your default web browser.)"
		Me.Label6.Visible = False
		'
		'CrowbarGuideButton
		'
		Me.CrowbarGuideButton.Cursor = System.Windows.Forms.Cursors.Hand
		Me.CrowbarGuideButton.Image = Global.Crowbar.My.Resources.Resources.CrowbarGuideBanner
		Me.CrowbarGuideButton.Location = New System.Drawing.Point(3, 3)
		Me.CrowbarGuideButton.Name = "CrowbarGuideButton"
		Me.CrowbarGuideButton.Size = New System.Drawing.Size(530, 147)
		Me.CrowbarGuideButton.TabIndex = 9
		Me.CrowbarGuideButton.UseVisualStyleBackColor = True
		'
		'HelpUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.CrowbarGuideButton)
		Me.Controls.Add(Me.Label6)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.GroupBox1)
		Me.Name = "HelpUserControl"
		Me.Size = New System.Drawing.Size(776, 536)
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents TutorialLinkLabel As System.Windows.Forms.LinkLabel
	Friend WithEvents ContentsLinkLabel As System.Windows.Forms.LinkLabel
	Friend WithEvents IndexLinkLabel As System.Windows.Forms.LinkLabel
	Friend WithEvents TipsLinkLabel As System.Windows.Forms.LinkLabel
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents Label6 As System.Windows.Forms.Label
	Friend WithEvents CrowbarGuideButton As System.Windows.Forms.Button

End Class
