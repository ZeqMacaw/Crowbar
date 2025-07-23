<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ComboUserControl
	Inherits BaseUserControl

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
		Me.components = New System.ComponentModel.Container()
		Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Me.ComboTextBox = New Crowbar.RichTextBoxEx()
		Me.MultipleInputsDropDownButton = New Crowbar.ButtonEx()
		Me.TextHistoryDropDownButton = New Crowbar.ButtonEx()
		Me.DropDownPanel = New Crowbar.PanelEx()
		Me.MultipleInputsDataGridView = New Crowbar.DataGridViewEx()
		Me.TextHistoryDataGridView = New Crowbar.DataGridViewEx()
		Me.VScrollBar1 = New System.Windows.Forms.VScrollBar()
		Me.DropDownPanel.SuspendLayout()
		CType(Me.MultipleInputsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.TextHistoryDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'ComboTextBox
		'
		Me.ComboTextBox.CueBannerText = ""
		Me.ComboTextBox.DetectUrls = False
		Me.ComboTextBox.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		Me.ComboTextBox.HideSelection = False
		Me.ComboTextBox.Location = New System.Drawing.Point(0, -1)
		Me.ComboTextBox.Margin = New System.Windows.Forms.Padding(0)
		Me.ComboTextBox.Multiline = False
		Me.ComboTextBox.Name = "ComboTextBox"
		Me.ComboTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
		Me.ComboTextBox.SelectionEnabled = True
		Me.ComboTextBox.Size = New System.Drawing.Size(124, 22)
		Me.ComboTextBox.TabIndex = 0
		Me.ComboTextBox.Text = ""
		Me.ComboTextBox.WordWrap = False
		'
		'MultipleInputsDropDownButton
		'
		Me.MultipleInputsDropDownButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.MultipleInputsDropDownButton.ButtonCanBeFocused = True
		Me.MultipleInputsDropDownButton.Location = New System.Drawing.Point(124, 0)
		Me.MultipleInputsDropDownButton.Margin = New System.Windows.Forms.Padding(0)
		Me.MultipleInputsDropDownButton.Name = "MultipleInputsDropDownButton"
		Me.MultipleInputsDropDownButton.Size = New System.Drawing.Size(18, 22)
		Me.MultipleInputsDropDownButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.MultipleInputsDropDownButton.TabIndex = 1
		Me.MultipleInputsDropDownButton.Text = "…"
		Me.MultipleInputsDropDownButton.UseVisualStyleBackColor = False
		'
		'TextHistoryDropDownButton
		'
		Me.TextHistoryDropDownButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.TextHistoryDropDownButton.ButtonCanBeFocused = True
		Me.TextHistoryDropDownButton.FlatAppearance.BorderSize = 0
		Me.TextHistoryDropDownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.TextHistoryDropDownButton.Location = New System.Drawing.Point(124, 0)
		Me.TextHistoryDropDownButton.Margin = New System.Windows.Forms.Padding(0)
		Me.TextHistoryDropDownButton.Name = "TextHistoryDropDownButton"
		Me.TextHistoryDropDownButton.Size = New System.Drawing.Size(18, 22)
		Me.TextHistoryDropDownButton.SpecialImage = Crowbar.ButtonEx.SpecialImageType.None
		Me.TextHistoryDropDownButton.TabIndex = 1
		Me.TextHistoryDropDownButton.UseVisualStyleBackColor = False
		'
		'DropDownPanel
		'
		Me.DropDownPanel.Controls.Add(Me.MultipleInputsDataGridView)
		Me.DropDownPanel.Controls.Add(Me.TextHistoryDataGridView)
		Me.DropDownPanel.Controls.Add(Me.VScrollBar1)
		Me.DropDownPanel.Location = New System.Drawing.Point(0, 22)
		Me.DropDownPanel.Name = "DropDownPanel"
		Me.DropDownPanel.Size = New System.Drawing.Size(142, 76)
		Me.DropDownPanel.TabIndex = 7
		'
		'MultipleInputsDataGridView
		'
		Me.MultipleInputsDataGridView.AllowUserToAddRows = False
		Me.MultipleInputsDataGridView.AllowUserToDeleteRows = False
		Me.MultipleInputsDataGridView.AllowUserToResizeRows = False
		Me.MultipleInputsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.MultipleInputsDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
		Me.MultipleInputsDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
		DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.MultipleInputsDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
		Me.MultipleInputsDataGridView.ColumnHeadersVisible = False
		DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
		DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
		DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.MultipleInputsDataGridView.DefaultCellStyle = DataGridViewCellStyle2
		Me.MultipleInputsDataGridView.EnableHeadersVisualStyles = False
		Me.MultipleInputsDataGridView.Location = New System.Drawing.Point(0, 0)
		Me.MultipleInputsDataGridView.MultiSelect = False
		Me.MultipleInputsDataGridView.Name = "MultipleInputsDataGridView"
		Me.MultipleInputsDataGridView.ReadOnly = True
		Me.MultipleInputsDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
		DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.MultipleInputsDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
		Me.MultipleInputsDataGridView.RowHeadersVisible = False
		Me.MultipleInputsDataGridView.RowTemplate.Height = 17
		Me.MultipleInputsDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.MultipleInputsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
		Me.MultipleInputsDataGridView.ShowCellErrors = False
		Me.MultipleInputsDataGridView.ShowEditingIcon = False
		Me.MultipleInputsDataGridView.ShowRowErrors = False
		Me.MultipleInputsDataGridView.Size = New System.Drawing.Size(142, 56)
		Me.MultipleInputsDataGridView.TabIndex = 8
		'
		'TextHistoryDataGridView
		'
		Me.TextHistoryDataGridView.AllowUserToAddRows = False
		Me.TextHistoryDataGridView.AllowUserToDeleteRows = False
		Me.TextHistoryDataGridView.AllowUserToResizeRows = False
		Me.TextHistoryDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.TextHistoryDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
		Me.TextHistoryDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
		DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.TextHistoryDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
		Me.TextHistoryDataGridView.ColumnHeadersVisible = False
		DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
		DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
		DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.TextHistoryDataGridView.DefaultCellStyle = DataGridViewCellStyle5
		Me.TextHistoryDataGridView.EnableHeadersVisualStyles = False
		Me.TextHistoryDataGridView.Location = New System.Drawing.Point(0, 0)
		Me.TextHistoryDataGridView.MultiSelect = False
		Me.TextHistoryDataGridView.Name = "TextHistoryDataGridView"
		Me.TextHistoryDataGridView.ReadOnly = True
		Me.TextHistoryDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
		DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 8.25!)
		DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.TextHistoryDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
		Me.TextHistoryDataGridView.RowHeadersVisible = False
		Me.TextHistoryDataGridView.RowTemplate.Height = 17
		Me.TextHistoryDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.TextHistoryDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
		Me.TextHistoryDataGridView.ShowCellErrors = False
		Me.TextHistoryDataGridView.ShowEditingIcon = False
		Me.TextHistoryDataGridView.ShowRowErrors = False
		Me.TextHistoryDataGridView.Size = New System.Drawing.Size(142, 56)
		Me.TextHistoryDataGridView.TabIndex = 9
		'
		'VScrollBar1
		'
		Me.VScrollBar1.Location = New System.Drawing.Point(124, 0)
		Me.VScrollBar1.Name = "VScrollBar1"
		Me.VScrollBar1.Size = New System.Drawing.Size(18, 80)
		Me.VScrollBar1.TabIndex = 7
		'
		'ComboUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.ComboTextBox)
		Me.Controls.Add(Me.TextHistoryDropDownButton)
		Me.Controls.Add(Me.MultipleInputsDropDownButton)
		Me.Controls.Add(Me.DropDownPanel)
		Me.Name = "ComboUserControl"
		Me.Size = New System.Drawing.Size(142, 22)
		Me.DropDownPanel.ResumeLayout(False)
		CType(Me.MultipleInputsDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.TextHistoryDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents ComboTextBox As RichTextBoxEx
	Friend WithEvents MultipleInputsDropDownButton As ButtonEx
    Friend WithEvents DropDownPanel As PanelEx
    Friend WithEvents VScrollBar1 As VScrollBar
	Friend WithEvents TextHistoryDropDownButton As ButtonEx
	Friend WithEvents MultipleInputsDataGridView As DataGridViewEx
	Friend WithEvents TextHistoryDataGridView As DataGridViewEx
End Class
