<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SourceFilmmakerTagsUserControl
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
		Me.ComboBox1 = New System.Windows.Forms.ComboBox()
		Me.ComboBox2 = New System.Windows.Forms.ComboBox()
		Me.ComboBox3 = New System.Windows.Forms.ComboBox()
		Me.SuspendLayout()
		'
		'ComboBox1
		'
		Me.ComboBox1.FormattingEnabled = True
		Me.ComboBox1.Location = New System.Drawing.Point(3, 3)
		Me.ComboBox1.Name = "ComboBox1"
		Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
		Me.ComboBox1.TabIndex = 0
		'
		'ComboBox2
		'
		Me.ComboBox2.FormattingEnabled = True
		Me.ComboBox2.Location = New System.Drawing.Point(3, 30)
		Me.ComboBox2.Name = "ComboBox2"
		Me.ComboBox2.Size = New System.Drawing.Size(121, 21)
		Me.ComboBox2.TabIndex = 1
		'
		'ComboBox3
		'
		Me.ComboBox3.FormattingEnabled = True
		Me.ComboBox3.Location = New System.Drawing.Point(3, 57)
		Me.ComboBox3.Name = "ComboBox3"
		Me.ComboBox3.Size = New System.Drawing.Size(121, 21)
		Me.ComboBox3.TabIndex = 2
		'
		'SourceFilmmakerTagsUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.ComboBox3)
		Me.Controls.Add(Me.ComboBox2)
		Me.Controls.Add(Me.ComboBox1)
		Me.Name = "SourceFilmmakerTagsUserControl"
		Me.Size = New System.Drawing.Size(292, 418)
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents ComboBox1 As ComboBox
	Friend WithEvents ComboBox2 As ComboBox
	Friend WithEvents ComboBox3 As ComboBox
End Class
