Imports System.ComponentModel

Public Class ComboUserControl

#Region "Creation and Destruction"

	Public Sub New()

		' This call is required by the designer.
		InitializeComponent()

		Me.theControlIsReadOnly = False
		Me.theComboPanelBorderColor = Color.Black
		Me.CreateContextMenu()

		Me.theMultipleInputsIsAllowed = False
		Me.MultipleInputsDropDownButton.Enabled = Me.theMultipleInputsIsAllowed

		Me.theTextHistoryIsKept = False
		Me.theTextHistoryMaxSize = 15
		Me.theTextIsValidatingViaMe = False
		Me.theSelectedIndexIsChangingViaMe = False
	End Sub

#End Region

#Region "Init and Free"

#End Region

#Region "Properties"

	Public Property IsReadOnly() As Boolean
		Get
			Return Me.theControlIsReadOnly
		End Get
		Set(ByVal value As Boolean)
			If Me.theControlIsReadOnly <> value Then
				Me.theControlIsReadOnly = value

				'TODO: Somehow disable value selection (i.e. no dropdown)
				'Me.Enabled = Not Me.theControlIsReadOnly
				If Me.theControlIsReadOnly Then
					Me.ForeColor = SystemColors.ControlText
					Me.BackColor = SystemColors.Control
				Else
					Me.ForeColor = SystemColors.ControlText
					Me.BackColor = SystemColors.Window
				End If
			End If
		End Set
	End Property

	Public Overrides Property Text() As String
		Get
			Return Me.ComboTextBox.Text
		End Get
		Set(ByVal value As String)
			Me.ComboTextBox.Text = value
		End Set
	End Property

	Public Property MultipleInputsIsAllowed() As Boolean
		Get
			Return Me.theMultipleInputsIsAllowed
		End Get
		Set(ByVal value As Boolean)
			If Me.theMultipleInputsIsAllowed <> value Then
				Me.theMultipleInputsIsAllowed = value
				Me.MultipleInputsDropDownButton.Enabled = Me.theMultipleInputsIsAllowed
			End If
		End Set
	End Property

	Public Property MultipleInputs() As BindingListEx(Of String)
		Get
			Return CType(Me.MultipleInputsListBox.DataSource, BindingListEx(Of String))
		End Get
		Set(ByVal value As BindingListEx(Of String))
			Me.MultipleInputsListBox.DataSource = value
		End Set
	End Property

	Public Property TextHistoryIsKept() As Boolean
		Get
			Return Me.theTextHistoryIsKept
		End Get
		Set(ByVal value As Boolean)
			If Me.theTextHistoryIsKept <> value Then
				Me.theTextHistoryIsKept = value

				If Me.theTextHistoryIsKept Then
					Me.CustomMenu.Items.Add(Me.Separator2ToolStripSeparator)
					Me.CustomMenu.Items.Add(Me.ClearTextHistoryToolStripMenuItem)
				Else
					Me.CustomMenu.Items.Remove(Me.Separator2ToolStripSeparator)
					Me.CustomMenu.Items.Remove(Me.ClearTextHistoryToolStripMenuItem)
				End If
			End If
		End Set
	End Property

	Public Property TextHistory() As BindingListEx(Of String)
		Get
			'Return Me.theTextHistory
			Return CType(Me.TextHistoryListBox.DataSource, BindingListEx(Of String))
		End Get
		Set(ByVal value As BindingListEx(Of String))
			Me.theSelectedIndexIsChangingViaMe = True
			Me.TextHistoryListBox.DataSource = value
			Me.theSelectedIndexIsChangingViaMe = False
		End Set
	End Property

	Public Property TextHistoryMaxSize() As Integer
		Get
			Return Me.theTextHistoryMaxSize
		End Get
		Set(ByVal value As Integer)
			If Me.theTextHistoryMaxSize <> value Then
				Me.theTextHistoryMaxSize = value

				Dim textHistory As List(Of String) = CType(Me.TextHistoryListBox.DataSource, List(Of String))
				For i As Integer = textHistory.Count - 1 To TextHistoryMaxSize Step -1
					textHistory.RemoveAt(i)
				Next
			End If
		End Set
	End Property

#End Region

#Region "Events and Delegates"

#End Region

#Region "Widget Event Handlers"

	Protected Overrides Sub OnHandleCreated(e As EventArgs)
		MyBase.OnHandleCreated(e)
		Me.InitMultipleInputsPopop()
		Me.InitTextHistoryPopop()
	End Sub

	Private Sub ComboUserControl_Resize(sender As Object, e As EventArgs) Handles Me.Resize
		Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(Me.ComboTextBox, Me.MultipleInputsDropDownButton)
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub ComboPanel_Paint(sender As Object, e As PaintEventArgs) Handles ComboPanel.Paint
		Using backgroundColorBrush As New SolidBrush(ComboPanel.BackColor)
			e.Graphics.FillRectangle(backgroundColorBrush, ComboPanel.DisplayRectangle)
		End Using

		Dim rect As Rectangle = ComboPanel.DisplayRectangle
		rect.Inflate(1, 1)
		ControlPaint.DrawBorder(e.Graphics, rect, Me.theComboPanelBorderColor, ButtonBorderStyle.Solid)
	End Sub

	Private Sub CustomMenu_Opening(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomMenu.Opening
		Me.UndoToolStripMenuItem.Enabled = Not Me.IsReadOnly AndAlso Me.ComboTextBox.CanUndo
		'Me.RedoToolStripMenuItem.Enabled = Not Me.IsReadOnly AndAlso Me.InternalTextBox.CanRedo
		Me.CutToolStripMenuItem.Enabled = Not Me.IsReadOnly AndAlso Me.ComboTextBox.SelectionLength > 0
		Me.CopyToolStripMenuItem.Enabled = Me.ComboTextBox.SelectionLength > 0
		Me.PasteToolStripMenuItem.Enabled = Not Me.IsReadOnly AndAlso Clipboard.ContainsText()
		Me.DeleteToolStripMenuItem.Enabled = Not Me.IsReadOnly AndAlso Me.ComboTextBox.SelectionLength > 0
		Me.SelectAllToolStripMenuItem.Enabled = Me.Text.Length > 0 AndAlso Me.ComboTextBox.SelectionLength < Me.Text.Length
		Me.CopyAllToolStripMenuItem.Enabled = Me.Text.Length > 0 AndAlso Me.ComboTextBox.SelectionLength < Me.Text.Length
	End Sub

	Private Sub UndoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoToolStripMenuItem.Click
		Me.ComboTextBox.Undo()
	End Sub

	'Private Sub RedoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedoToolStripMenuItem.Click
	'	Me.ComboTextBox.Redo()
	'End Sub

	Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click
		Me.ComboTextBox.Cut()
	End Sub

	Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
		Me.ComboTextBox.Copy()
	End Sub

	Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
		Me.ComboTextBox.Paste()
	End Sub

	Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
		'NOTE: Must using P/Invoke EM_REPLACESEL to allow an Undo of Delete as is done with default TextBox.
		'      https://docs.microsoft.com/en-us/windows/win32/controls/em-replacesel?redirectedfrom=MSDN
		Win32Api.SendMessage(Me.ComboTextBox.Handle, Win32Api.EditControlMessage.EM_REPLACESEL, New IntPtr(1), IntPtr.Zero)
	End Sub

	Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
		Me.ComboTextBox.SelectAll()
	End Sub

	Private Sub CopyAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyAllToolStripMenuItem.Click
		Me.ComboTextBox.SelectAll()
		Me.ComboTextBox.Copy()
	End Sub

	Private Sub ClearTextHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearTextHistoryToolStripMenuItem.Click
		Me.ClearTextHistory()
	End Sub

	'NOTE: Using TextChanged() or TextUpdate() instead of Validated() causes every character change to be stored and user typing is reversed due to cursor reset.
	Private Sub ComboTextBox_Validated(sender As Object, e As EventArgs) Handles ComboTextBox.Validated
		'IMPORTANT: This is only needed for "Text" property due to Framework doing different stuff for the property.
		Me.OnTextChanged(e)

		If Not Me.theTextIsValidatingViaMe Then
			Me.theSelectedIndexIsChangingViaMe = True
			Dim textHistory As BindingListEx(Of String) = CType(Me.TextHistoryListBox.DataSource, BindingListEx(Of String))
			If Me.theTextHistoryIsKept AndAlso Me.Text <> "" AndAlso Not textHistory.Contains(Me.Text) Then
				textHistory.Insert(0, Me.Text)
				If textHistory.Count > Me.theTextHistoryMaxSize Then
					textHistory.RemoveAt(textHistory.Count - 1)
				End If
			End If
			Me.TextHistoryListBox.Text = Me.ComboTextBox.Text
			Me.theSelectedIndexIsChangingViaMe = False
		End If
	End Sub

	Private Sub MultipleInputsButton_MouseDown(sender As Object, e As EventArgs) Handles MultipleInputsDropDownButton.MouseDown
		Me.OnMultipleInputsButton_MouseDown()
	End Sub

	Private Sub MultipleInputsPopup_VisibleChanged(sender As Object, e As EventArgs) Handles MultipleInputsPopup.VisibleChanged
		Me.OnMultipleInputsPopup_VisibleChanged()
	End Sub

	Private Sub TextHistoryDropDownButton_MouseDown(sender As Object, e As EventArgs) Handles TextHistoryDropDownButton.MouseDown
		Me.OnDropDownButton_MouseDown()
	End Sub

	Private Sub TextHistoryListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TextHistoryListBox.SelectedIndexChanged
		Me.OnTextHistoryListBox_SelectedIndexChanged()
	End Sub

	Private Sub TextHistoryPopup_VisibleChanged(sender As Object, e As EventArgs) Handles TextHistoryPopup.VisibleChanged
		Me.OnTextHistoryPopup_VisibleChanged()
	End Sub

#End Region

#Region "Core Event Handlers"

#End Region

#Region "Private Methods"

	Private Sub InitMultipleInputsPopop()
		If Me.MultipleInputsPopup Is Nothing Then
			'IMPORTANT: Avoid using an incorrect font.
			Me.MultipleInputsListBox.Font = Me.Font

			Me.MultipleInputsListBox.BindingContext = New BindingContext()

			Me.theDropDownButtonWasClickedWhenPopupShowing = False
			Me.MultipleInputsPopup = New Popup(Me.MultipleInputsListBox)
			Me.MultipleInputsPopup.Name = "MultipleInputsPopup"
			Me.MultipleInputsDropDownButton.OuterPopup = Me.MultipleInputsPopup
		End If
	End Sub

	Private Sub InitTextHistoryPopop()
		If Me.TextHistoryPopup Is Nothing Then
			'IMPORTANT: Avoid using an incorrect font.
			Me.TextHistoryListBox.Font = Me.Font

			Me.TextHistoryListBox.BindingContext = New BindingContext()

			Me.theDropDownButtonWasClickedWhenPopupShowing = False
			Me.TextHistoryPopup = New Popup(Me.TextHistoryListBox)
			Me.TextHistoryPopup.Name = "TextHistoryPopup"
			Me.TextHistoryDropDownButton.OuterPopup = Me.TextHistoryPopup
		End If
	End Sub

	Private Sub OnMultipleInputsButton_MouseDown()
		If Not Me.theDropDownButtonWasClickedWhenPopupShowing Then
			'IMPORTANT: Resize the ListBox.
			Me.MultipleInputsListBox.Size = Me.MultipleInputsListBox.PreferredSize

			Dim position As Point = New Point(0, Me.Height)
			position = Me.PointToScreen(position)
			Me.MultipleInputsPopup.Show(position)
			Me.theDropDownButtonWasClickedWhenPopupShowing = True
		Else
			'NOTE: Make sure Popup hides because TextHistoryPopup can still be visible when user clicks very quickly .
			Me.MultipleInputsPopup.Hide()
			Me.theDropDownButtonWasClickedWhenPopupShowing = False
		End If
	End Sub

	Private Sub OnMultipleInputsPopup_VisibleChanged()
		If Not Me.MultipleInputsPopup.Visible Then
			Me.MultipleInputsDropDownButton.Invalidate()
			Dim cursorPositionInClient As Point = Me.PointToClient(Cursor.Position)
			Dim aControl As Control = Me.GetChildAtPoint(cursorPositionInClient)
			If aControl IsNot Me.MultipleInputsDropDownButton Then
				Me.theDropDownButtonWasClickedWhenPopupShowing = False
			End If
		End If
	End Sub

	Private Sub OnDropDownButton_MouseDown()
		If Not Me.theDropDownButtonWasClickedWhenPopupShowing Then
			'IMPORTANT: Resize the ListBox.
			Me.TextHistoryListBox.Size = Me.TextHistoryListBox.PreferredSize

			' Select the list item with the textbox text, if it exists.
			Me.theSelectedIndexIsChangingViaMe = True
			Me.TextHistoryListBox.Text = Me.ComboTextBox.Text
			Me.theSelectedIndexIsChangingViaMe = False

			Dim position As Point = New Point(0, Me.Height)
			position = Me.PointToScreen(position)
			Me.TextHistoryPopup.Show(position)
			Me.theDropDownButtonWasClickedWhenPopupShowing = True
		Else
			'NOTE: Make sure Popup hides because TextHistoryPopup can still be visible when user clicks very quickly .
			Me.TextHistoryPopup.Hide()
			Me.theDropDownButtonWasClickedWhenPopupShowing = False
		End If
	End Sub

	Private Sub OnTextHistoryPopup_VisibleChanged()
		If Not Me.TextHistoryPopup.Visible Then
			Me.TextHistoryDropDownButton.Invalidate()
			Dim cursorPositionInClient As Point = Me.PointToClient(Cursor.Position)
			Dim aControl As Control = Me.GetChildAtPoint(cursorPositionInClient)
			If aControl IsNot Me.TextHistoryDropDownButton Then
				Me.theDropDownButtonWasClickedWhenPopupShowing = False
			End If
		End If
	End Sub

	Private Sub OnTextHistoryListBox_SelectedIndexChanged()
		If Not Me.theSelectedIndexIsChangingViaMe Then
			Me.ComboTextBox.Text = Me.TextHistoryListBox.Text
			Me.TextHistoryPopup.Hide()

			'NOTE: Do not know why OnTextChanged() does not update the databinding here like it does in Validated event handler.
			''IMPORTANT: This is only needed for "Text" property due to Framework doing different stuff for the property.
			'Me.OnTextChanged(New EventArgs())
			Me.theTextIsValidatingViaMe = True
			Me.ParentForm.ValidateChildren()
			Me.theTextIsValidatingViaMe = False
		End If
	End Sub

	Private Sub CreateContextMenu()
		Me.CustomMenu = New ContextMenuStrip()
		Me.CustomMenu.Items.Add(Me.UndoToolStripMenuItem)
		'Me.CustomMenu.Items.Add(Me.RedoToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.Separator0ToolStripSeparator)
		Me.CustomMenu.Items.Add(Me.CutToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.CopyToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.PasteToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.DeleteToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.Separator1ToolStripSeparator)
		Me.CustomMenu.Items.Add(Me.SelectAllToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.CopyAllToolStripMenuItem)
		Me.ContextMenuStrip = Me.CustomMenu
		Me.ComboTextBox.ContextMenuStrip = Me.CustomMenu
	End Sub

	Private Sub ClearTextHistory()
		'' Save and later restore current text because inexplicably it is deleted when the history is cleared.
		'Dim currentText As String = Me.Text

		Dim textHistory As List(Of String) = CType(Me.TextHistoryListBox.DataSource, List(Of String))
		textHistory.Clear()

		'Me.Text = currentText
	End Sub

#End Region

#Region "Data"

	Protected theControlIsReadOnly As Boolean
	Protected theComboPanelBorderColor As Color

	Private WithEvents CustomMenu As ContextMenuStrip
	Private WithEvents UndoToolStripMenuItem As New ToolStripMenuItem("&Undo")
	'Private WithEvents RedoToolStripMenuItem As New ToolStripMenuItem("&Redo")
	Private WithEvents Separator0ToolStripSeparator As New ToolStripSeparator()
	Private WithEvents CutToolStripMenuItem As New ToolStripMenuItem("Cu&t")
	Private WithEvents CopyToolStripMenuItem As New ToolStripMenuItem("&Copy")
	Private WithEvents PasteToolStripMenuItem As New ToolStripMenuItem("&Paste")
	Private WithEvents DeleteToolStripMenuItem As New ToolStripMenuItem("&Delete")
	Private WithEvents Separator1ToolStripSeparator As New ToolStripSeparator()
	Private WithEvents SelectAllToolStripMenuItem As New ToolStripMenuItem("Select &All")
	Private WithEvents CopyAllToolStripMenuItem As New ToolStripMenuItem("Copy A&ll")
	Private WithEvents Separator2ToolStripSeparator As New ToolStripSeparator()
	Private WithEvents ClearTextHistoryToolStripMenuItem As New ToolStripMenuItem("Clear &History")

	Protected WithEvents MultipleInputsPopup As Popup
	Protected theMultipleInputsIsAllowed As Boolean

	Protected WithEvents TextHistoryPopup As Popup
	Protected theTextHistoryIsKept As Boolean
	'Protected theTextHistory As List(Of String)
	Protected theTextHistoryMaxSize As Integer
	Protected theTextIsValidatingViaMe As Boolean
	Protected theSelectedIndexIsChangingViaMe As Boolean

	'NOTE: Need this to handle when dropdown button can show the Popup because the Popup automatically hides itself.
	Protected theDropDownButtonWasClickedWhenPopupShowing As Boolean

#End Region

	'NOTE: Using Panel instead of Button because Button has inconsistency between display and actual rectangle.
	Public Class DropDownButton
		Inherits Panel

		Public Property OuterPopup As Popup
			Get
				Return Me.theOuterPopup
			End Get
			Set(value As Popup)
				Me.theOuterPopup = value
			End Set
		End Property

		Protected Overrides Sub OnHandleCreated(e As EventArgs)
			MyBase.OnHandleCreated(e)
			'NOTE: Me.Parent is incorrect type in DesignMode.
			If Not Me.DesignMode Then
				Me.theParentComboUserControl = CType(Me.Parent, ComboUserControl)
			End If
		End Sub

		Protected Overrides Sub OnMouseCaptureChanged(e As EventArgs)
			MyBase.OnMouseCaptureChanged(e)
			If Me.Enabled AndAlso Me.theParentComboUserControl IsNot Nothing AndAlso Not Me.theOuterPopup.Visible Then
				If Not Me.Capture Then
					Me.Invalidate()
				End If
			End If
		End Sub

		Protected Overrides Sub OnMouseEnter(e As EventArgs)
			MyBase.OnMouseEnter(e)
			If Me.Enabled AndAlso Me.theParentComboUserControl IsNot Nothing AndAlso Not Me.theOuterPopup.Visible Then
				Me.Capture = True
				Me.Invalidate()
			End If
		End Sub

		Protected Overrides Sub OnMouseLeave(e As EventArgs)
			MyBase.OnMouseLeave(e)
			If Me.Enabled AndAlso Me.theParentComboUserControl IsNot Nothing AndAlso Not Me.theOuterPopup.Visible Then
				Me.Capture = False
				Me.Invalidate()
			End If
		End Sub

		Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
			MyBase.OnMouseMove(e)
			If Me.Enabled AndAlso Me.theParentComboUserControl IsNot Nothing AndAlso Not Me.theOuterPopup.Visible Then
				Dim pt As New Point(e.X, e.Y)
				If Not Me.ClientRectangle.Contains(pt) Then
					Me.Capture = False
					Me.Invalidate()
				End If
			End If
		End Sub

		Protected Overrides Sub OnPaint(e As PaintEventArgs)
			MyBase.OnPaint(e)

			Dim mouseIsOverControl As Boolean = HelperFunctions.MouseIsOverControl(Me)
			If Me.Name = "MultipleInputsDropDownButton" Then
				Dim state As VisualStyles.PushButtonState = VisualStyles.PushButtonState.Normal
				If Me.Enabled Then
					If mouseIsOverControl Then
						If MouseButtons = MouseButtons.Left Then
							state = VisualStyles.PushButtonState.Pressed
						Else
							state = VisualStyles.PushButtonState.Hot
						End If
					End If
				Else
					state = VisualStyles.PushButtonState.Disabled
				End If
				Dim rect As Rectangle = Me.DisplayRectangle
				rect.Inflate(1, 1)
				ButtonRenderer.DrawButton(e.Graphics, rect, state)
				If Me.Enabled Then
					TextRenderer.DrawText(e.Graphics, Me.Text, Me.Font, rect, Me.ForeColor)
				Else
					ControlPaint.DrawStringDisabled(e.Graphics, Me.Text, Me.Font, Me.BackColor, rect, TextFormatFlags.LeftAndRightPadding Or TextFormatFlags.VerticalCenter)
				End If
			Else
				If ComboBoxRenderer.IsSupported Then
					Dim state As VisualStyles.ComboBoxState = VisualStyles.ComboBoxState.Normal
					If mouseIsOverControl Then
						If MouseButtons = MouseButtons.Left Then
							state = VisualStyles.ComboBoxState.Pressed
						Else
							state = VisualStyles.ComboBoxState.Hot
						End If
					End If
					ComboBoxRenderer.DrawDropDownButton(e.Graphics, Me.DisplayRectangle, state)
				Else
					' Draw button without visualstyle.
					Dim state As ButtonState = ButtonState.Flat
					If mouseIsOverControl Then
						If MouseButtons = MouseButtons.Left Then
							state = ButtonState.Pushed
						Else
							state = ButtonState.Checked
						End If
					End If
					ControlPaint.DrawComboButton(e.Graphics, Me.DisplayRectangle, state)
				End If
			End If

			' Draw two or three lines around button to match border of ComboUserControl.
			If Me.theParentComboUserControl IsNot Nothing AndAlso Not mouseIsOverControl Then
				Dim rect As Rectangle = Me.DisplayRectangle
				'rect.Inflate(-1, -1)
				rect.Width -= 1
				rect.Height -= 1
				Dim blackPen As New Pen(Me.theParentComboUserControl.theComboPanelBorderColor, 1)
				e.Graphics.DrawLine(blackPen, rect.Left, rect.Top, rect.Right, rect.Top)
				e.Graphics.DrawLine(blackPen, rect.Left, rect.Bottom, rect.Right, rect.Bottom)

				'NOTE: Only draw right-side vertical line if at right edge of outer panel.
				'Dim outerPanelRect As Rectangle = Me.OuterComboUserControl.ComboPanel.DisplayRectangle
				'outerPanelRect.Inflate(-1, -1)
				If Me.Right = Me.theParentComboUserControl.ComboPanel.Right Then
					e.Graphics.DrawLine(blackPen, rect.Right, rect.Top, rect.Right, rect.Bottom)
				End If
			End If
		End Sub

		Private theParentComboUserControl As ComboUserControl
		Private theOuterPopup As Popup

	End Class

End Class
