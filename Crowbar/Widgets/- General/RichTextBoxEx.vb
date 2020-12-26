Imports System.ComponentModel

Public Class RichTextBoxEx
	Inherits RichTextBox

#Region "Creation and Destruction"

	Public Sub New()
		MyBase.New()

		Me.CustomMenu = New ContextMenuStrip()
		Me.CustomMenu.Items.Add(Me.UndoToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.RedoToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.Separator0ToolStripSeparator)
		Me.CustomMenu.Items.Add(Me.CutToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.CopyToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.PasteToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.DeleteToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.Separator1ToolStripSeparator)
		Me.CustomMenu.Items.Add(Me.SelectAllToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.CopyAllToolStripMenuItem)

		Me.ContextMenuStrip = Me.CustomMenu

		Me.theCueBannerText = ""
	End Sub

#End Region

#Region "Init and Free"

#End Region

#Region "Properties"

	<Browsable(True)>
	<Category("Appearance")>
	<Description("Sets the text of the cue (dimmed text that only shows when Text property is empty).")>
	Public Property CueBannerText As String
		Get
			Return Me.theCueBannerText
		End Get
		Set
			Me.theCueBannerText = Value
		End Set
	End Property

#End Region

#Region "Widget Event Handlers"

	'Protected Overrides Sub OnHandleCreated(e As EventArgs)
	'	'If Me.theOriginalFont Is Nothing Then
	'	'	'NOTE: Font gets changed at some point after changing style, messing up when cue banner is turned off, 
	'	'	'      so save the Font after widget is visible for first time, but before changing style within the widget.
	'	'	Me.theOriginalFont = New System.Drawing.Font(Me.Font.FontFamily, Me.Font.Size, Me.Font.Style, Me.Font.Unit)

	'	'	'SetStyle(ControlStyles.UserPaint, Me.theCueBannerText <> "")
	'	'	SetStyle(ControlStyles.AllPaintingInWmPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
	'	'	SetStyle(ControlStyles.DoubleBuffer, Me.theCueBannerText <> "" AndAlso Me.Text = "")
	'	'	SetStyle(ControlStyles.UserPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
	'	'End If

	'	Me.AutoWordSelection = True
	'	Me.AutoWordSelection = False
	'End Sub

	Protected Overrides Sub OnPaint(e As PaintEventArgs)
		MyBase.OnPaint(e)

		If Me.theCueBannerText <> "" AndAlso Me.Text = "" AndAlso Me.theOriginalFont IsNot Nothing Then
			'Dim drawFont As System.Drawing.Font = New System.Drawing.Font(Me.theOriginalFont.FontFamily, Me.theOriginalFont.Size, Me.theOriginalFont.Style, Me.theOriginalFont.Unit)
			Dim drawFont As System.Drawing.Font = New System.Drawing.Font(Me.theOriginalFont.FontFamily, Me.theOriginalFont.Size, FontStyle.Italic, Me.theOriginalFont.Unit)

			Dim drawForeColor As Color = SystemColors.GrayText
			Dim drawBackColor As Color = SystemColors.Control
			If drawForeColor = drawBackColor Then
				drawForeColor = Me.ForeColor
				drawBackColor = Me.BackColor
			End If
			TextRenderer.DrawText(e.Graphics, Me.theCueBannerText, drawFont, New Point(1, 0), drawForeColor, drawBackColor)
			'======
			'' Draw higlight.
			'Dim higlightForeColor As Color = SystemColors.ControlLightLight
			''Dim higlightBackColor As Color = SystemColors.Control
			''If higlightForeColor = higlightBackColor Then
			''	higlightForeColor = Me.ForeColor
			''	higlightBackColor = Me.BackColor
			''End If
			''TextRenderer.DrawText(e.Graphics, Me.theCueBannerText, drawFont, New Point(1, 1), higlightForeColor, higlightBackColor)
			'TextRenderer.DrawText(e.Graphics, Me.theCueBannerText, drawFont, New Point(1, 1), higlightForeColor)
			'' Draw shadow.
			'Dim shadowForeColor As Color = SystemColors.ControlDark
			''Dim shadowBackColor As Color = SystemColors.Control
			''If shadowForeColor = shadowBackColor Then
			''	shadowForeColor = Me.ForeColor
			''	shadowBackColor = Me.BackColor
			''End If
			''TextRenderer.DrawText(e.Graphics, Me.theCueBannerText, drawFont, New Point(-1, -1), shadowForeColor, shadowBackColor)
			'TextRenderer.DrawText(e.Graphics, Me.theCueBannerText, drawFont, New Point(-1, -1), shadowForeColor)
		End If
	End Sub

	Protected Overrides Sub OnTextChanged(e As EventArgs)
		MyBase.OnTextChanged(e)

		' This did not solve the bug.
		'If Not Me.Visible Then
		'	Exit Sub
		'End If

		' This did not solve the bug.
		If Me.theOriginalFont Is Nothing Then
			Exit Sub
		End If

		If GetStyle(ControlStyles.UserPaint) <> (Me.theCueBannerText <> "" AndAlso Me.Text = "") Then
			SetStyle(ControlStyles.AllPaintingInWmPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
			SetStyle(ControlStyles.DoubleBuffer, Me.theCueBannerText <> "" AndAlso Me.Text = "")
			SetStyle(ControlStyles.UserPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
			If Me.theOriginalFont IsNot Nothing Then
				Me.Font = New System.Drawing.Font(Me.theOriginalFont.FontFamily, Me.theOriginalFont.Size, Me.theOriginalFont.Style, Me.theOriginalFont.Unit)
			End If
			Me.Invalidate()
		End If
	End Sub

	Protected Overrides Sub OnVisibleChanged(e As EventArgs)
		MyBase.OnVisibleChanged(e)

		If Me.theOriginalFont Is Nothing Then
			'NOTE: Font gets changed at some point after changing style, messing up when cue banner is turned off, 
			'      so save the Font after widget is visible for first time, but before changing style within the widget.
			Me.theOriginalFont = New System.Drawing.Font(Me.Font.FontFamily, Me.Font.Size, Me.Font.Style, Me.Font.Unit)

			'SetStyle(ControlStyles.UserPaint, Me.theCueBannerText <> "")
			SetStyle(ControlStyles.AllPaintingInWmPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
			SetStyle(ControlStyles.DoubleBuffer, Me.theCueBannerText <> "" AndAlso Me.Text = "")
			SetStyle(ControlStyles.UserPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")

			'WORKAROUND - Without these two lines, selecting individual characters with the mouse often selects to end of words.
			Me.AutoWordSelection = True
			Me.AutoWordSelection = False
		End If
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub CustomMenu_Opening(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomMenu.Opening
		Me.UndoToolStripMenuItem.Enabled = Not Me.ReadOnly AndAlso Me.CanUndo
		Me.RedoToolStripMenuItem.Enabled = Not Me.ReadOnly AndAlso Me.CanRedo
		Me.CutToolStripMenuItem.Enabled = Not Me.ReadOnly AndAlso Me.SelectionLength > 0
		Me.CopyToolStripMenuItem.Enabled = Me.SelectionLength > 0
		Me.PasteToolStripMenuItem.Enabled = Not Me.ReadOnly AndAlso Clipboard.ContainsText()
		Me.DeleteToolStripMenuItem.Enabled = Not Me.ReadOnly AndAlso Me.SelectionLength > 0
		Me.SelectAllToolStripMenuItem.Enabled = Me.TextLength > 0 AndAlso Me.SelectionLength < Me.TextLength
		Me.CopyAllToolStripMenuItem.Enabled = Me.TextLength > 0 AndAlso Me.SelectionLength < Me.TextLength
	End Sub

	Private Sub UndoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoToolStripMenuItem.Click
		Me.Undo()
	End Sub

	Private Sub RedoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedoToolStripMenuItem.Click
		Me.Redo()
	End Sub

	Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click
		Me.Cut()
	End Sub

	Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
		Me.Copy()
	End Sub

	Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
		Me.Paste()
	End Sub

	Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
		Me.SelectedText = ""
	End Sub

	Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
		Me.SelectAll()
	End Sub

	Private Sub CopyAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyAllToolStripMenuItem.Click
		Me.SelectAll()
		Me.Copy()
		'Me.SelectionLength = 0
	End Sub

#End Region

#Region "Core Event Handlers"

#End Region

#Region "Private Methods"

#End Region

#Region "Data"

	Private WithEvents CustomMenu As ContextMenuStrip

	Private WithEvents UndoToolStripMenuItem As New ToolStripMenuItem("&Undo")
	Private WithEvents RedoToolStripMenuItem As New ToolStripMenuItem("&Redo")
	Private WithEvents Separator0ToolStripSeparator As New ToolStripSeparator()
	Private WithEvents CutToolStripMenuItem As New ToolStripMenuItem("Cu&t")
	Private WithEvents CopyToolStripMenuItem As New ToolStripMenuItem("&Copy")
	Private WithEvents PasteToolStripMenuItem As New ToolStripMenuItem("&Paste")
	Private WithEvents DeleteToolStripMenuItem As New ToolStripMenuItem("&Delete")
	Private WithEvents Separator1ToolStripSeparator As New ToolStripSeparator()
	Private WithEvents SelectAllToolStripMenuItem As New ToolStripMenuItem("Select &All")
	Private WithEvents CopyAllToolStripMenuItem As New ToolStripMenuItem("Copy A&ll")

	Private theCueBannerText As String
	Private theOriginalFont As Font

#End Region

End Class
