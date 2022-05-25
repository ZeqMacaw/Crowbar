<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ComboUserControl
	Inherits System.Windows.Forms.UserControl

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
		Me.ComboTextBox = New System.Windows.Forms.TextBox()
		Me.MultipleInputsDropDownButton = New Crowbar.ComboUserControl.DropDownButton()
		Me.TextHistoryDropDownButton = New Crowbar.ComboUserControl.DropDownButton()
		Me.TextHistoryListBox = New Crowbar.ListBoxEx()
		Me.ComboPanel = New System.Windows.Forms.Panel()
		Me.MultipleInputsListBox = New Crowbar.ListBoxEx()
		Me.SuspendLayout()
		'
		'ComboTextBox
		'
		Me.ComboTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.ComboTextBox.HideSelection = False
		Me.ComboTextBox.Location = New System.Drawing.Point(3, 3)
		Me.ComboTextBox.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
		Me.ComboTextBox.Name = "ComboTextBox"
		Me.ComboTextBox.Size = New System.Drawing.Size(96, 13)
		Me.ComboTextBox.TabIndex = 0
		'
		'MultipleInputsDropDownButton
		'
		Me.MultipleInputsDropDownButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.MultipleInputsDropDownButton.Location = New System.Drawing.Point(106, 0)
		Me.MultipleInputsDropDownButton.Margin = New System.Windows.Forms.Padding(0)
		Me.MultipleInputsDropDownButton.Name = "MultipleInputsDropDownButton"
		Me.MultipleInputsDropDownButton.OuterPopup = Nothing
		Me.MultipleInputsDropDownButton.Size = New System.Drawing.Size(18, 21)
		Me.MultipleInputsDropDownButton.TabIndex = 1
		Me.MultipleInputsDropDownButton.Text = "…"
		'
		'TextHistoryDropDownButton
		'
		Me.TextHistoryDropDownButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.TextHistoryDropDownButton.Location = New System.Drawing.Point(124, 0)
		Me.TextHistoryDropDownButton.Margin = New System.Windows.Forms.Padding(0)
		Me.TextHistoryDropDownButton.Name = "TextHistoryDropDownButton"
		Me.TextHistoryDropDownButton.OuterPopup = Nothing
		Me.TextHistoryDropDownButton.Size = New System.Drawing.Size(18, 21)
		Me.TextHistoryDropDownButton.TabIndex = 3
		Me.TextHistoryDropDownButton.Text = "v"
		'
		'TextHistoryListBox
		'
		Me.TextHistoryListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TextHistoryListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.TextHistoryListBox.FormattingEnabled = True
		Me.TextHistoryListBox.Location = New System.Drawing.Point(0, 39)
		Me.TextHistoryListBox.Margin = New System.Windows.Forms.Padding(0)
		Me.TextHistoryListBox.Name = "TextHistoryListBox"
		Me.TextHistoryListBox.Size = New System.Drawing.Size(120, 54)
		Me.TextHistoryListBox.TabIndex = 4
		'
		'ComboPanel
		'
		Me.ComboPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.ComboPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ComboPanel.Location = New System.Drawing.Point(0, 0)
		Me.ComboPanel.Name = "ComboPanel"
		Me.ComboPanel.Size = New System.Drawing.Size(142, 21)
		Me.ComboPanel.TabIndex = 5
		'
		'MultipleInputsListBox
		'
		Me.MultipleInputsListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.MultipleInputsListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.MultipleInputsListBox.FormattingEnabled = True
		Me.MultipleInputsListBox.Location = New System.Drawing.Point(0, 39)
		Me.MultipleInputsListBox.Margin = New System.Windows.Forms.Padding(0)
		Me.MultipleInputsListBox.Name = "MultipleInputsListBox"
		Me.MultipleInputsListBox.Size = New System.Drawing.Size(120, 54)
		Me.MultipleInputsListBox.TabIndex = 6
		'
		'ComboUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Window
		Me.Controls.Add(Me.ComboTextBox)
		Me.Controls.Add(Me.MultipleInputsDropDownButton)
		Me.Controls.Add(Me.TextHistoryDropDownButton)
		Me.Controls.Add(Me.MultipleInputsListBox)
		Me.Controls.Add(Me.TextHistoryListBox)
		Me.Controls.Add(Me.ComboPanel)
		Me.Name = "ComboUserControl"
		Me.Size = New System.Drawing.Size(142, 21)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents ComboTextBox As TextBox
	Friend WithEvents MultipleInputsDropDownButton As DropDownButton
	Friend WithEvents TextHistoryDropDownButton As DropDownButton
	Friend WithEvents TextHistoryListBox As ListBoxEx
	Friend WithEvents ComboPanel As Panel
	Friend WithEvents MultipleInputsListBox As ListBoxEx
End Class
