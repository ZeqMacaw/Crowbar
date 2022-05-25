Imports System.Windows.Forms.VisualStyles

Public Class ComboBoxEx
	Inherits ComboBox

#Region "Creation and Destruction"

	Public Sub New()
		MyBase.New()

		'Me.SetStyle(ControlStyles.UserPaint, True)

		Me.CreateContextMenu()

		Me.theControlIsReadOnly = False
		Me.theTextIsAllowingMultiplePathFileNames = False
		Me.theTextHistoryIsKept = False
		Me.theTextHistory = New List(Of String)()
		Me.theTextHistoryMaxSize = 15
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

	Public Property TextIsAllowingMultiplePathFileNames() As Boolean
		Get
			Return Me.theTextIsAllowingMultiplePathFileNames
		End Get
		Set(ByVal value As Boolean)
			If Me.theTextIsAllowingMultiplePathFileNames <> value Then
				Me.theTextIsAllowingMultiplePathFileNames = value

				'If Me.theTextIsAllowingMultiplePathFileNames Then
				'	Me.SetStyle(ControlStyles.Opaque Or ControlStyles.UserPaint, True)
				'	'Me.DrawMode = DrawMode.OwnerDrawFixed
				'Else
				'	'Me.DrawMode = DrawMode.Normal
				'End If
			End If
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

	Public Property TextHistory() As List(Of String)
		Get
			Return Me.theTextHistory
		End Get
		Set(ByVal value As List(Of String))
			If Me.theTextHistory IsNot value Then
				Me.theTextHistory = value
				Me.DataSource = Nothing
				Me.DataSource = Me.theTextHistory
			End If
		End Set
	End Property

	Public Property TextHistoryMaxSize() As Integer
		Get
			Return Me.theTextHistoryMaxSize
		End Get
		Set(ByVal value As Integer)
			If Me.theTextHistoryMaxSize <> value Then
				Me.theTextHistoryMaxSize = value

				For i As Integer = Me.theTextHistory.Count - 1 To Me.theTextHistoryMaxSize Step -1
					Me.theTextHistory.RemoveAt(i)
				Next
			End If
		End Set
	End Property

#End Region

#Region "Events and Delegates"

	Public Delegate Sub DropdownItemSelectedEventHandler(ByVal sender As Object, ByVal e As DropdownItemSelectedEventArgs)
	Public Event DropDownItemSelected As DropdownItemSelectedEventHandler
	'Public Delegate Sub DropdownRButtonDownEventHandler(ByVal sender As Object, ByVal e As EventArgs)
	'Public Event DropDownRButtonDown As DropdownRButtonDownEventHandler

#End Region

#Region "Widget Event Handlers"

	'Protected Overrides Sub OnPaint(e As PaintEventArgs)
	'	'MyBase.OnPaint(e)

	'	If ComboBoxRenderer.IsSupported Then
	'		ComboBoxRenderer.DrawTextBox(e.Graphics, e.ClipRectangle, System.Windows.Forms.VisualStyles.ComboBoxState.Disabled)
	'	Else
	'		Dim rect As Rectangle = e.ClipRectangle
	'		rect.Width -= 1
	'		rect.Height -= 1

	'		' Draw textbox background.
	'		Using backColorBrush As New SolidBrush(Color.Red)
	'			e.Graphics.FillRectangle(backColorBrush, rect)
	'		End Using

	'		' Draw textbox border.
	'		Using thinBorderPen As New Pen(Color.Green, 1)
	'			e.Graphics.DrawRectangle(thinBorderPen, rect)
	'		End Using
	'	End If

	'	If Me.theOriginalFont IsNot Nothing Then
	'		' Draw textbox text.
	'		TextRenderer.DrawText(e.Graphics, Me.Text, Me.theOriginalFont, e.ClipRectangle, Me.ForeColor, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.LeftAndRightPadding)
	'	Else
	'		' Draw textbox text.
	'		TextRenderer.DrawText(e.Graphics, Me.Text, Me.Font, e.ClipRectangle, Me.ForeColor, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.LeftAndRightPadding)
	'	End If

	'	''' Draw drop-down arrow.
	'	''Dim dropDownRect As New Rectangle(Me.ClientRectangle.Right - SystemInformation.HorizontalScrollBarArrowWidth, Me.ClientRectangle.Y, SystemInformation.HorizontalScrollBarArrowWidth, Me.ClientRectangle.Height)
	'	''Dim middle As New Point(CInt(dropDownRect.Left + dropDownRect.Width / 2), CInt(dropDownRect.Top + dropDownRect.Height / 2))
	'	''Dim arrow As Point() = {New Point(middle.X - 3, middle.Y - 2), New Point(middle.X + 4, middle.Y - 2), New Point(middle.X, middle.Y + 2)}
	'	''Using backColorBrush As New SolidBrush(Me.ForeColor)
	'	''	e.Graphics.FillPolygon(backColorBrush, arrow)
	'	''End Using
	'	'If Me.Visible Then
	'	'	If ComboBoxRenderer.IsSupported Then
	'	'		ComboBoxRenderer.DrawDropDownButton(e.Graphics, e.ClipRectangle, System.Windows.Forms.VisualStyles.ComboBoxState.Disabled)
	'	'	Else
	'	'		ControlPaint.DrawComboButton(e.Graphics, e.ClipRectangle, ButtonState.Inactive)
	'	'	End If
	'	'End If

	'	''Dim buttonRect As Rectangle = e.Bounds
	'	''buttonRect.X = e.Bounds.Width - 20
	'	''buttonRect.Width = 20
	'	''ButtonRenderer.DrawButton(e.Graphics, buttonRect, VisualStyles.PushButtonState.Default)

	'	''Dim rect As Rectangle = e.Bounds
	'	''rect.Width -= buttonRect.Width
	'	''TextRenderer.DrawText(e.Graphics, Me.Text, Me.Font, rect, Me.ForeColor)
	'End Sub

	'Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
	'	MyBase.OnPaintBackground(e)

	'	Dim rect As Rectangle = e.ClipRectangle
	'	rect.Width -= 1
	'	rect.Height -= 1

	'	' Draw textbox background.
	'	Using backColorBrush As New SolidBrush(Me.BackColor)
	'		e.Graphics.FillRectangle(backColorBrush, rect)
	'	End Using

	'	' Draw textbox border.
	'	Using thinBorderPen As New Pen(Me.ForeColor, 1)
	'		e.Graphics.DrawRectangle(thinBorderPen, rect)
	'	End Using

	'	'Dim rect As Rectangle = e.ClipRectangle
	'	'rect.Inflate(4, 4)
	'	'If Me.DroppedDown Then
	'	'	ButtonRenderer.DrawButton(e.Graphics, rect, PushButtonState.Pressed)
	'	'Else
	'	'	ButtonRenderer.DrawButton(e.Graphics, rect, PushButtonState.Normal)
	'	'End If
	'End Sub

	Protected Overrides Sub OnHandleCreated(e As EventArgs)
		MyBase.OnHandleCreated(e)

		Dim textBoxHandle As IntPtr
		Dim listBoxHandle As IntPtr
		Win32Api.GetComboBoxInternalControlHandles(Me.Handle, textBoxHandle, listBoxHandle)

		Me.InternalTextBox = New TextBoxWindow()
		Me.InternalTextBox.AssignHandle(textBoxHandle)
		AddHandler Me.InternalTextBox.OnEditBoxPaint, AddressOf InternalTextBox_OnEditBoxPaint

		Me.InternalListBox = New ListBoxNativeWindow(Me)
		Me.InternalListBox.AssignHandle(listBoxHandle)
	End Sub

	Protected Overrides Sub OnHandleDestroyed(e As EventArgs)
		Me.InternalTextBox.ReleaseHandle()
		Me.InternalTextBox = Nothing

		Me.InternalListBox.ReleaseHandle()
		Me.InternalListBox = Nothing

		MyBase.OnHandleDestroyed(e)
	End Sub

	Protected Overrides Sub OnDropDown(e As EventArgs)
		Dim maxWidth As Integer = 0
		Dim temp As Integer = 0
		For Each obj As Object In Me.Items
			temp = TextRenderer.MeasureText(Me.GetItemText(obj), Me.Font).Width
			If temp > maxWidth Then
				maxWidth = temp
			End If
		Next
		If maxWidth > 0 Then
			Me.DropDownWidth = maxWidth
		End If

		MyBase.OnDropDown(e)
	End Sub

	Protected Overrides Sub OnDropDownClosed(ByVal e As EventArgs)
		'Me.ContextMenuStrip.Close()
		MyBase.OnDropDownClosed(e)
		NotifyDropdownItemSelected(-1, Rectangle.Empty, True)
	End Sub

	Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
		If Me.theControlIsReadOnly Then
			e.Handled = True
		End If
		MyBase.OnKeyDown(e)
	End Sub

	Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
		If Me.theControlIsReadOnly Then
			e.Handled = True
		End If
		MyBase.OnKeyPress(e)
	End Sub

	' Untried: Keep DropDownListBox open when showing a ContextMenu.
	'Protected Overrides Sub OnLostFocus(e As EventArgs)
	'	If Me.DropdownListBox IsNot Nothing Then
	'		Focus()
	'	Else
	'		MyBase.OnLostFocus(e)
	'	End If
	'End Sub

	'Protected Overrides Sub OnMouseClick(ByVal e As System.Windows.Forms.MouseEventArgs)
	'	If Not Me.theControlIsReadOnly Then
	'		MyBase.OnMouseClick(e)
	'	End If
	'End Sub

	'Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
	'	If Not Me.theControlIsReadOnly Then
	'		MyBase.OnMouseDown(e)
	'	End If
	'End Sub

	Protected Overrides Sub OnValidated(e As EventArgs)
		MyBase.OnValidated(e)

		'NOTE: Using OnTextChanged() or OnTextUpdate() instead of OnValidated() causes every character change to be stored and user typing is reversed due to cursor reset.
		If Me.theTextHistoryIsKept AndAlso Me.DropDownStyle = ComboBoxStyle.DropDown AndAlso Me.Text <> "" AndAlso Not Me.theTextHistory.Contains(Me.Text) Then
			Me.theTextHistory.Insert(0, Me.Text)
			If Me.theTextHistory.Count > Me.theTextHistoryMaxSize Then
				Me.theTextHistory.RemoveAt(Me.theTextHistory.Count - 1)
			End If
			Me.DataSource = Nothing
			Me.DataSource = Me.theTextHistory
		End If
	End Sub

	'Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
	'	If (m.Msg <> Win32Api.WindowsMessages.WM_LBUTTONDOWN And m.Msg <> Win32Api.WindowsMessages.WM_LBUTTONDBLCLK) Or Not Me.theControlIsReadOnly Then
	'		MyBase.WndProc(m)
	'	End If
	'End Sub

	Protected Overrides Sub OnVisibleChanged(e As EventArgs)
		MyBase.OnVisibleChanged(e)

		'If Me.theOriginalFont Is Nothing Then
		'	'NOTE: Font gets changed at some point after changing style, messing up when cue banner is turned off, 
		'	'      so save the Font after widget is visible for first time, but before changing style within the widget.
		'	Me.theOriginalFont = New System.Drawing.Font(Me.Font.FontFamily, Me.Font.Size, Me.Font.Style, Me.Font.Unit)
		'	'Me.DrawMode = DrawMode.OwnerDrawFixed
		'	Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
		'	Me.SetStyle(ControlStyles.DoubleBuffer, True)
		'	Me.SetStyle(ControlStyles.UserPaint, True)
		'End If
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub InternalTextBox_OnEditBoxPaint(ByVal sender As Object, ByVal e As PaintEventArgs)
		Dim rect As Rectangle = e.ClipRectangle
		'rect.Inflate(1, 1)
		'' Draw textbox background.
		'Using backColorBrush As New SolidBrush(Color.Red)
		'	e.Graphics.FillRectangle(backColorBrush, rect)
		'End Using
		'If Me.theOriginalFont IsNot Nothing Then
		'	' Draw textbox text.
		'	TextRenderer.DrawText(e.Graphics, Me.Text, Me.theOriginalFont, rect, Color.Red, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.LeftAndRightPadding)
		'	'TextRenderer.DrawText(e.Graphics, Me.Text, Me.theOriginalFont, Me.ClientRectangle, Color.Red, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.LeftAndRightPadding)
		'Else
		'	' Draw textbox text.
		TextRenderer.DrawText(e.Graphics, Me.Text, Me.Font, rect, Color.Green, TextFormatFlags.NoPadding)
		'	'TextRenderer.DrawText(e.Graphics, Me.Text, Me.Font, Me.ClientRectangle, Color.Green, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.LeftAndRightPadding)
		'End If
	End Sub

	Private Sub CustomMenu_Opening(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomMenu.Opening
		Me.UndoToolStripMenuItem.Enabled = Not Me.IsReadOnly AndAlso Me.InternalTextBox.CanUndo
		'Me.RedoToolStripMenuItem.Enabled = Not Me.IsReadOnly AndAlso Me.InternalTextBox.CanRedo
		Me.CutToolStripMenuItem.Enabled = Not Me.IsReadOnly AndAlso Me.SelectionLength > 0
		Me.CopyToolStripMenuItem.Enabled = Me.SelectionLength > 0
		Me.PasteToolStripMenuItem.Enabled = Not Me.IsReadOnly AndAlso Clipboard.ContainsText()
		Me.DeleteToolStripMenuItem.Enabled = Not Me.IsReadOnly AndAlso Me.SelectionLength > 0
		Me.SelectAllToolStripMenuItem.Enabled = Me.Text.Length > 0 AndAlso Me.SelectionLength < Me.Text.Length
		Me.CopyAllToolStripMenuItem.Enabled = Me.Text.Length > 0 AndAlso Me.SelectionLength < Me.Text.Length
	End Sub

	Private Sub UndoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoToolStripMenuItem.Click
		Me.InternalTextBox.Undo()
	End Sub

	'Private Sub RedoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedoToolStripMenuItem.Click
	'	Me.InternalTextBox.Redo()
	'End Sub

	Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click
		Me.InternalTextBox.Cut()
	End Sub

	Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
		Me.InternalTextBox.Copy()
	End Sub

	Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
		Me.InternalTextBox.Paste()
	End Sub

	Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
		Me.InternalTextBox.Delete()
	End Sub

	Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
		Me.SelectAll()
	End Sub

	Private Sub CopyAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyAllToolStripMenuItem.Click
		Me.SelectAll()
		Me.InternalTextBox.Copy()
	End Sub

	Private Sub ClearTextHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearTextHistoryToolStripMenuItem.Click
		Me.ClearTextHistory()
	End Sub

#End Region

#Region "Core Event Handlers"

#End Region

#Region "Private Methods"

	Friend Sub NotifyDropdownItemSelected(ByVal item As Integer, ByVal screenPos As Rectangle, ByVal listIsScrolled As Boolean)
		Dim clientPos As Rectangle = Me.RectangleToClient(screenPos)
		RaiseEvent DropDownItemSelected(Me, New DropdownItemSelectedEventArgs(item, clientPos, listIsScrolled))
	End Sub

	'TEST: Show context menu with right-click in dropdown area. 
	'      FAILED. Menu shows at last place shown via right-click in text area or at screen (0,0).
	'      Also, hovering over menu item does not show highlight as it does when shown via text area.
	'Friend Sub NotifyDropdownRButtonDown()
	'	Me.ContextMenuStrip.Show()
	'	'RaiseEvent DropDownRButtonDown(Me, New EventArgs())
	'End Sub

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
	End Sub

	Private Sub ClearTextHistory()
		' Save and later restore current text because inexplicably it is deleted when the history is cleared.
		Dim currentText As String = Me.Text

		Me.theTextHistory.Clear()
		Me.DataSource = Nothing
		Me.DataSource = Me.theTextHistory

		Me.Text = currentText
	End Sub

#End Region

#Region "Data"

	Protected theControlIsReadOnly As Boolean
	Protected theTextIsAllowingMultiplePathFileNames As Boolean
	Protected theTextHistoryIsKept As Boolean
	Protected theTextHistory As List(Of String)
	Protected theTextHistoryMaxSize As Integer

	Private theOriginalFont As Font
	'Private InternalTextBox As TextBox
	Private InternalTextBox As TextBoxWindow
	Private InternalListBox As ListBoxNativeWindow

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

#End Region

	Public Class DropdownItemSelectedEventArgs
		Inherits EventArgs

		Private mItem As Integer
		Private mPos As Rectangle
		Private mScroll As Boolean

		Public Sub New(ByVal item As Integer, ByVal pos As Rectangle, ByVal scroll As Boolean)
			mItem = item
			mPos = pos
			mScroll = scroll
		End Sub

		Public ReadOnly Property SelectedItem As Integer
			Get
				Return mItem
			End Get
		End Property

		Public ReadOnly Property Bounds As Rectangle
			Get
				Return mPos
			End Get
		End Property

		Public ReadOnly Property IsScrolled As Boolean
			Get
				Return mScroll
			End Get
		End Property
	End Class

	Private Class TextBoxWindow
		Inherits NativeWindow

		Public Function CanUndo() As Boolean
			Dim textBoxCanUndo As Boolean
			Dim bool As Int32 = Win32Api.SendMessage(Me.Handle, Win32Api.EditControlMessage.EM_CANUNDO, IntPtr.Zero, IntPtr.Zero).ToInt32()
			If bool = 0 Then
				textBoxCanUndo = False
			ElseIf bool = 1 Then
				textBoxCanUndo = True
			End If
			Return textBoxCanUndo
		End Function

		Public Sub Undo()
			Dim bool As Int32 = Win32Api.SendMessage(Me.Handle, Win32Api.EditControlMessage.EM_UNDO, IntPtr.Zero, IntPtr.Zero).ToInt32()
		End Sub

		'Public Sub Redo()

		'End Sub

		Public Sub Cut()
			Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_CUT, IntPtr.Zero, IntPtr.Zero).ToInt32()
		End Sub

		Public Sub Copy()
			Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_COPY, IntPtr.Zero, IntPtr.Zero).ToInt32()
		End Sub

		Public Sub Paste()
			Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_PASTE, IntPtr.Zero, IntPtr.Zero).ToInt32()
		End Sub

		Public Sub Delete()
			'Win32Api.SendMessage(Me.Handle, Win32Api.WindowsMessages.WM_CLEAR, IntPtr.Zero, IntPtr.Zero).ToInt32()
		End Sub

		Protected Overrides Sub WndProc(ByRef windowsMessage As Message)
			MyBase.WndProc(windowsMessage)

			If windowsMessage.Msg = Win32Api.WindowsMessages.WM_PAINT Then
				Me.Paint()
				windowsMessage.Result = CType(1, IntPtr)
			ElseIf windowsMessage.Msg = Win32Api.WindowsMessages.WM_LBUTTONDOWN Then
				Me.Paint()
			ElseIf windowsMessage.Msg = Win32Api.WindowsMessages.WM_KEYDOWN Then
				Me.Paint()
			ElseIf windowsMessage.Msg = Win32Api.WindowsMessages.WM_MOUSEMOVE Then
				Me.Paint()
			ElseIf windowsMessage.Msg = Win32Api.WindowsMessages.WM_SETFOCUS Then
				Me.Paint()
			End If
		End Sub

		Private Sub Paint()
			Dim rect As Win32Api.RECT
			'Win32Api.GetUpdateRect(Me.Handle, rect, True)
			Win32Api.GetClientRect(Me.Handle, rect)
			Dim clipRect As New Rectangle(rect.Location, rect.Size)
			Using g As Graphics = Graphics.FromHwnd(Me.Handle)
				RaiseEvent OnEditBoxPaint(Me, New PaintEventArgs(g, clipRect))
			End Using
		End Sub

		Public Delegate Sub EditBoxPaintEventHandler(ByVal sender As Object, ByVal e As PaintEventArgs)
		Public Event OnEditBoxPaint As EditBoxPaintEventHandler

	End Class

	Private Class ListBoxNativeWindow
		Inherits NativeWindow

		Public Sub New(ByVal parent As ComboBoxEx)
			Me.theParentComboBox = parent
			Me.theSelectedItemIndex = -1
		End Sub

		Protected Overrides Sub WndProc(ByRef windowsMessage As Message)
			MyBase.WndProc(windowsMessage)

			If windowsMessage.Msg = Win32Api.WindowsMessages.WM_MOUSEMOVE Then
				Dim selectedItem As Integer = CInt(Win32Api.SendMessage(Me.Handle, Win32Api.ListBoxMessages.LB_GETCURSEL, IntPtr.Zero, IntPtr.Zero))

				If selectedItem <> Me.theSelectedItemIndex Then
					Me.theSelectedItemIndex = selectedItem
					OnSelect(False)
				End If
				'ElseIf windowsMessage.Msg = Win32Api.WindowsMessages.WM_RBUTTONDOWN Then
				'	Me.OnRButtonDown()
			End If

			If windowsMessage.Msg = Win32Api.WindowsMessages.WM_VSCROLL Then
				OnSelect(True)
			End If
		End Sub

		'Private Sub OnRButtonDown()
		'	Me.theParentComboBox.NotifyDropdownRButtonDown()
		'End Sub

		Private Sub OnSelect(ByVal listIsScrolled As Boolean)
			Dim rectangle As New Win32Api.RECT()
			Win32Api.SendMessage(Me.Handle, Win32Api.ListBoxMessages.LB_GETITEMRECT, CType(Me.theSelectedItemIndex, IntPtr), rectangle)
			Win32Api.MapWindowPoints(Me.Handle, IntPtr.Zero, rectangle, 2)
			Me.theParentComboBox.NotifyDropdownItemSelected(Me.theSelectedItemIndex, Drawing.Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom), listIsScrolled)
		End Sub

		Private theParentComboBox As ComboBoxEx
		Private theSelectedItemIndex As Integer

	End Class

End Class
