Imports System.ComponentModel
Imports System.Runtime.InteropServices

Public Class DataGridViewEx
	Inherits DataGridView

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		' Override BorderStyle to allow custom border width.
		MyBase.BorderStyle = BorderStyle.None
		'Me.theBorderStyle = BorderStyle.FixedSingle
		'Me.theBorderWidth = 1

		Me.CustomHorizontalScrollbar = New ScrollBarEx()
		Me.Controls.Add(Me.CustomHorizontalScrollbar)
		Me.CustomHorizontalScrollbar.Location = New System.Drawing.Point(0, Me.ClientRectangle.Height)
		Me.CustomHorizontalScrollbar.Name = "CustomHorizontalScrollbar"
		Me.CustomHorizontalScrollbar.Size = New System.Drawing.Size(Me.Width, ScrollBarEx.Consts.ScrollBarSize)
		Me.CustomHorizontalScrollbar.ScrollOrientation = ScrollBarEx.DarkScrollOrientation.Horizontal
		Me.CustomHorizontalScrollbar.TabIndex = 7
		Me.CustomHorizontalScrollbar.Text = "CustomHorizontalScrollbar"
		Me.CustomHorizontalScrollbar.Visible = False

		Me.CustomVerticalScrollBar = New ScrollBarEx()
		Me.Controls.Add(Me.CustomVerticalScrollBar)
		Me.CustomVerticalScrollBar.Location = New System.Drawing.Point(Me.ClientRectangle.Width, 0)
		Me.CustomVerticalScrollBar.Name = "CustomVerticalScrollBar"
		Me.CustomVerticalScrollBar.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize, Me.Height)
		Me.CustomVerticalScrollBar.ScrollOrientation = ScrollBarEx.DarkScrollOrientation.Vertical
		Me.CustomVerticalScrollBar.TabIndex = 7
		Me.CustomVerticalScrollBar.Text = "CustomVerticalScrollBar"
		Me.CustomVerticalScrollBar.Visible = False

		Me.ScrollbarCornerPanel = New ScrollBarPanel()
		Me.Controls.Add(Me.ScrollbarCornerPanel)
		Me.ScrollbarCornerPanel.Name = "ScrollbarCornerPanel"
		Me.ScrollbarCornerPanel.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize, ScrollBarEx.Consts.ScrollBarSize)
		Me.ScrollbarCornerPanel.Visible = False

		Me.theControlHasShown = False

		'NOTE: Need these settings so that ColumnHeadersDefaultCellStyle, DefaultCellStyle, and GridColor properties are used.
		'      Might affect other properties, too.
		'Me.EnableHeadersVisualStyles = False
		'Me.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
		'Me.CellBorderStyle = DataGridViewCellBorderStyle.Single
		'Me.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single

		'Me.ColumnHeadersDefaultCellStyle.ForeColor = WidgetConstants.WidgetTextColor
		'Me.ColumnHeadersDefaultCellStyle.BackColor = WidgetConstants.WidgetBackColor
		'Me.ColumnHeadersDefaultCellStyle.SelectionForeColor = WidgetConstants.WidgetTextColor
		'Me.ColumnHeadersDefaultCellStyle.SelectionBackColor = WidgetConstants.WidgetSelectedBackColor

		'Me.DefaultCellStyle.ForeColor = WidgetConstants.WidgetTextColor
		'Me.DefaultCellStyle.BackColor = WidgetConstants.WidgetBackColor
		'Me.DefaultCellStyle.SelectionForeColor = WidgetConstants.WidgetTextColor
		'Me.DefaultCellStyle.SelectionBackColor = WidgetConstants.WidgetSelectedBackColor

		'Me.RowHeadersDefaultCellStyle.ForeColor = WidgetConstants.WidgetTextColor
		'Me.RowHeadersDefaultCellStyle.BackColor = WidgetConstants.WidgetBackColor
		'Me.RowHeadersDefaultCellStyle.SelectionForeColor = WidgetConstants.WidgetTextColor
		'Me.RowHeadersDefaultCellStyle.SelectionBackColor = WidgetConstants.WidgetSelectedBackColor

		'Me.BorderStyle = BorderStyle.None

		Me.theCurrentCellIsChangingBecauseOfMe = False
		Me.theSelectionIsChangingBecauseOfMe = False

		Me.theCellWherePointerIs = Nothing
	End Sub

#End Region

#Region "Init and Free"

	Protected Sub Init()
		' [04-Feb-2026] Because Me.DesignMode is unreliable in nested widgets, must do this check to prevent a crash.
		If TheApp IsNot Nothing Then
			Me.UpdateTheme()
			AddHandler TheApp.Settings.PropertyChanged, AddressOf Me.AppSettings_PropertyChanged
		End If
	End Sub

	Protected Sub Free()
		' [04-Feb-2026] Because Me.DesignMode is unreliable in nested widgets, must do this check to prevent a crash.
		If TheApp IsNot Nothing Then
			RemoveHandler TheApp.Settings.PropertyChanged, AddressOf Me.AppSettings_PropertyChanged
		End If
	End Sub

#End Region

#Region "Properties"

	Public Overloads Property AutoGenerateColumns() As Boolean
		Get
			Return MyBase.AutoGenerateColumns
		End Get
		Set(ByVal value As Boolean)
			MyBase.AutoGenerateColumns = value
		End Set
	End Property

	'<Browsable(True)>
	'<Category("Appearance")>
	'<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	'Public Overloads Property BorderColor As Color
	'	Get
	'		Return Me.theBorderColor
	'	End Get
	'	Set
	'		Me.theBorderColor = Value
	'	End Set
	'End Property

	'<Browsable(True)>
	'<Category("Appearance")>
	'<Description("Colorable BorderStyle.")>
	'<DefaultValue(BorderStyle.FixedSingle)>
	'<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	'Public Overloads Property BorderStyle As BorderStyle
	'	Get
	'		Return Me.theBorderStyle
	'	End Get
	'	Set
	'		Me.theBorderStyle = Value

	'		If Me.theBorderStyle = Windows.Forms.BorderStyle.None Then
	'			Me.theBorderWidth = 0
	'		ElseIf Me.theBorderStyle = Windows.Forms.BorderStyle.FixedSingle Then
	'			Me.theBorderWidth = 1
	'		End If
	'	End Set
	'End Property

	Public Overloads Property DataSource() As Object
		Get
			Return MyBase.DataSource
		End Get
		Set(ByVal value As Object)
			If Me.DesignMode Then
				Exit Property
			End If
			If Me.IsCurrentCellInEditMode Then
				Me.EndEdit()
			End If
			MyBase.DataSource = value
		End Set
	End Property

	'<Browsable(False)>
	'Public Overloads ReadOnly Property HorizontalScrollBar() As ScrollBar
	'	Get
	'		Return MyBase.HorizontalScrollBar
	'	End Get
	'End Property

	Public Overloads Property [ReadOnly]() As Boolean
		Get
			Return MyBase.ReadOnly
		End Get
		Set(ByVal value As Boolean)
			If MyBase.ReadOnly <> value Then
				MyBase.ReadOnly = value

				'If MyBase.ReadOnly Then
				'    Me.DefaultCellStyle.BackColor = WidgetConstants.WidgetBackColor
				'Else
				'    Me.DefaultCellStyle.BackColor = WidgetConstants.WidgetDisabledTextColor
				'End If
			End If
		End Set
	End Property

	'<Browsable(True)>
	'<Category("Layout")>
	'<Description("Colorable scrollbars.")>
	'Public Overloads Property ScrollBars As ScrollBars
	'    Get
	'        Return Me.theScrollBars
	'    End Get
	'    Set
	'        Me.theScrollBars = Value
	'    End Set
	'End Property

#End Region

#Region "Methods"

#End Region

#Region "Widget Event Handlers"

	Protected Overrides Sub OnHandleCreated(e As EventArgs)
		MyBase.OnHandleCreated(e)
		' [04-Feb-2026] Me.DesignMode is unreliable in nested widgets.
		'If Not Me.DesignMode Then
		Me.Init()
		'End If
	End Sub

	Protected Overrides Sub OnHandleDestroyed(e As EventArgs)
		Me.Free()
		MyBase.OnHandleDestroyed(e)
	End Sub

	Protected Overrides Sub OnCellClick(ByVal e As DataGridViewCellEventArgs)
		If Not Me.ReadOnly AndAlso Me.Enabled AndAlso (e.RowIndex > -1) AndAlso (e.ColumnIndex > -1) Then
			Dim cell As DataGridViewCell = Me(e.ColumnIndex, e.RowIndex)
			If TypeOf cell.OwningColumn Is DataGridViewRadioButtonColumn Then
				If cell.FormattedValue.ToString().Length = 0 Then
					For i As Integer = 0 To Me.RowCount - 1
						Me(e.ColumnIndex, i).Value = String.Empty
					Next
					cell.Value = "Selected"
				End If
			End If
		End If

		MyBase.OnCellClick(e)
	End Sub

	'NOTE: This works for avoiding read-only cells, but it completely disallows entering the cells, so can't copy values if wanted.
	'Protected Overrides Sub OnCellEnter(ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
	'	Dim dgc As DataGridViewCell = TryCast(Me.Item(e.ColumnIndex, e.RowIndex), DataGridViewCell)
	'	If dgc IsNot Nothing AndAlso dgc.ReadOnly Then
	'		SendKeys.Send("{Tab}")
	'	End If
	'	MyBase.OnCellEnter(e)
	'End Sub

	'Private theMouseIsDown As Boolean = False

	'Protected Overrides Sub OnCellMouseDown(ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
	'	Me.theMouseIsDown = True
	'	MyBase.OnCellMouseDown(e)
	'End Sub

	'Protected Overrides Sub OnCellMouseMove(ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
	'	If Me.theMouseIsDown AndAlso Me.AllowUserToAddRows = True AndAlso e.RowIndex <> Me.NewRowIndex Then
	'		Me.theWidgetTempOfAllowUserToAddRows = Me.AllowUserToAddRows
	'		Me.AllowUserToAddRows = False
	'	End If
	'	MyBase.OnCellMouseMove(e)
	'End Sub

	'Protected Overrides Sub OnCellMouseUp(ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
	'	MyBase.OnCellMouseUp(e)
	'	Me.AllowUserToAddRows = Me.theWidgetTempOfAllowUserToAddRows
	'	Me.theMouseIsDown = False
	'End Sub

	Protected Overrides Sub OnCellMouseEnter(e As DataGridViewCellEventArgs)
		MyBase.OnCellMouseEnter(e)
		If (e.RowIndex > -1) AndAlso (e.ColumnIndex > -1) Then
			If Me.theCellWherePointerIs IsNot Nothing Then
				Me.InvalidateCell(Me.theCellWherePointerIs)
			End If
			Me.theCellWherePointerIs = Me.Rows(e.RowIndex).Cells(e.ColumnIndex)
			Me.InvalidateCell(Me.theCellWherePointerIs)
		End If
	End Sub

	Protected Overrides Sub OnCellMouseLeave(e As DataGridViewCellEventArgs)
		MyBase.OnCellMouseLeave(e)
		If (e.RowIndex > -1) AndAlso (e.ColumnIndex > -1) Then
			Me.theCellWherePointerIs = Nothing
			Me.InvalidateCell(e.ColumnIndex, e.RowIndex)
		End If
	End Sub

	Protected Overrides Sub OnCellEnter(ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
		MyBase.OnCellEnter(e)
		If (e.RowIndex > -1) AndAlso (e.ColumnIndex > -1) Then
			If Me.theCellWherePointerIs IsNot Nothing Then
				Me.InvalidateCell(Me.theCellWherePointerIs)
			End If
			Me.theCellWherePointerIs = Me.Rows(e.RowIndex).Cells(e.ColumnIndex)
			Me.InvalidateCell(Me.theCellWherePointerIs)
		End If
	End Sub

	Protected Overrides Sub OnCellLeave(ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
		MyBase.OnCellLeave(e)
		If (e.RowIndex > -1) AndAlso (e.ColumnIndex > -1) Then
			Me.theCellWherePointerIs = Nothing
			Me.InvalidateCell(e.ColumnIndex, e.RowIndex)
		End If
	End Sub

	Protected Overrides Sub OnCellPainting(ByVal e As DataGridViewCellPaintingEventArgs)
		Dim g As Graphics = e.Graphics
		Dim cellRect As Rectangle = e.CellBounds

		Dim theme As DataGridViewTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.DataGridViewTheme
		End If
		If theme IsNot Nothing Then
			'' RowIndex = -1 for header cell.
			'If e.RowIndex = -1 AndAlso e.ColumnIndex > -1 Then
			'    Me.ColumnHeadersDefaultCellStyle.ForeColor = theme.EnabledForeColor
			'    Me.ColumnHeadersDefaultCellStyle.BackColor = theme.EnabledBackColor

			'    '' Set settings for cell borders.
			'    'Me.GridColor = Color.Red
			'    'Me.EnableHeadersVisualStyles = False
			'    'Me.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
			'    'Me.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
			'    'Me.CellBorderStyle = DataGridViewCellBorderStyle.Single

			'    '' Draw header cell border.
			'    'Dim headerBorderColor As Color = Color.Red
			'    'Using borderColorPen As New Pen(headerBorderColor, 1)
			'    '    ' Draw the left border.
			'    '    e.Graphics.DrawLine(borderColorPen, cellRect.Left - 1, cellRect.Top, cellRect.Left - 1, cellRect.Bottom)
			'    '    ' Draw the bottom border.
			'    '    e.Graphics.DrawLine(borderColorPen, cellRect.Left, cellRect.Bottom - 1, cellRect.Right, cellRect.Bottom - 1)
			'    'End Using
			'End If
			If (e.RowIndex > -1) AndAlso (e.ColumnIndex > -1) Then
				If TypeOf Me.Columns(e.ColumnIndex) Is DataGridViewRadioButtonColumn Then
					e.PaintBackground(e.CellBounds, False)
					DataGridViewRadioButtonColumn.Paint(e.Graphics, e.CellBounds, (e.FormattedValue.ToString().Length > 0))
					e.Handled = True
				ElseIf TypeOf Me.Columns(e.ColumnIndex) Is DataGridViewButtonColumn Then
					'Dim column As DataGridViewButtonColumn = CType(Me.Columns(e.ColumnIndex), DataGridViewButtonColumn)
					Dim cell As DataGridViewCell = Me.Rows(e.RowIndex).Cells(e.ColumnIndex)

					Dim backColor1 As Color
					Dim backColor2 As Color
					Dim textColor As Color
					Dim textBackColor As Color
					Dim borderColor As Color

					If Me.Enabled Then
						If cell.Selected Then
							backColor1 = theme.SelectedBackColor
							backColor2 = theme.SelectedBackColor
							textColor = theme.SelectedForeColor
							textBackColor = Color.Transparent
							borderColor = theme.SelectedBorderColor
						ElseIf cell Is Me.theCellWherePointerIs Then
							backColor1 = theme.FocusBackColor
							backColor2 = theme.FocusBackColor
							textColor = theme.FocusForeColor
							textBackColor = Color.Transparent
							borderColor = theme.FocusBorderColor
						Else
							backColor1 = theme.EnabledBackColor
							backColor2 = theme.EnabledBackColor
							textColor = theme.EnabledForeColor
							textBackColor = Color.Transparent
							borderColor = theme.EnabledBorderColor
						End If
					Else
						backColor1 = theme.DisabledBackColor
						backColor2 = theme.DisabledBackColor
						textColor = theme.DisabledForeColor
						textBackColor = Color.Transparent
						borderColor = theme.DisabledBorderColor
					End If

					' Draw background.
					Using aColorBrush As New Drawing2D.LinearGradientBrush(cellRect, backColor1, backColor2, Drawing2D.LinearGradientMode.Vertical)
						g.FillRectangle(aColorBrush, cellRect)
					End Using
					TextRenderer.DrawText(g, CType(cell.Value, String), Me.Font, cellRect, textColor, textBackColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.WordBreak)

					'' Draw cell border.
					'Dim headerBorderColor As Color = Color.Red
					'Using borderColorPen As New Pen(headerBorderColor, 1)
					'    ' Draw the left border.
					'    e.Graphics.DrawLine(borderColorPen, cellRect.Left - 1, cellRect.Top, cellRect.Left - 1, cellRect.Bottom)
					'    ' Draw the bottom border.
					'    e.Graphics.DrawLine(borderColorPen, cellRect.Left, cellRect.Bottom - 1, cellRect.Right, cellRect.Bottom - 1)
					'End Using

					' Draw button border.
					Using borderColorPen As New Pen(borderColor, 1)
						'NOTE: DrawRectangle width and height are interpreted as the right and bottom pixels to draw.
						g.DrawRectangle(borderColorPen, cellRect.Left, cellRect.Top + 1, cellRect.Width - 1, cellRect.Height - 2)
					End Using

					'' Draw the grid lines (only the right and bottom lines;
					'' DataGridView takes care of the others).
					'Dim gridBrush As New SolidBrush(Me.GridColor)
					'Dim gridLinePen As New Pen(gridBrush)
					'e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1)
					'e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1)

					e.Handled = True
				ElseIf TypeOf Me.Columns(e.ColumnIndex) Is DataGridViewTextBoxColumn Then
					Dim cell As DataGridViewCell = Me.Rows(e.RowIndex).Cells(e.ColumnIndex)

					Dim backColor1 As Color
					Dim backColor2 As Color
					Dim textColor As Color
					Dim textBackColor As Color
					'Dim borderColor As Color

					If Me.Enabled Then
						If cell.Selected Then
							backColor1 = theme.SelectedBackColor
							backColor2 = theme.SelectedBackColor
							textColor = theme.SelectedForeColor
							textBackColor = Color.Transparent
							'borderColor = theme.SelectedBorderColor
						ElseIf cell Is Me.theCellWherePointerIs Then
							backColor1 = theme.FocusBackColor
							backColor2 = theme.FocusBackColor
							textColor = theme.FocusForeColor
							textBackColor = Color.Transparent
							'borderColor = theme.FocusBorderColor
						Else
							backColor1 = theme.EnabledBackColor
							backColor2 = theme.EnabledBackColor
							textColor = theme.EnabledForeColor
							textBackColor = Color.Transparent
							'borderColor = theme.EnabledBorderColor
						End If
					Else
						backColor1 = theme.DisabledBackColor
						backColor2 = theme.DisabledBackColor
						textColor = theme.DisabledForeColor
						textBackColor = Color.Transparent
						'borderColor = theme.DisabledBorderColor
					End If

					' Draw background.
					Using aColorBrush As New Drawing2D.LinearGradientBrush(cellRect, backColor1, backColor2, Drawing2D.LinearGradientMode.Vertical)
						g.FillRectangle(aColorBrush, cellRect)
					End Using
					Dim textFormat As TextFormatFlags = TextFormatFlags.Default
					If cell.InheritedStyle.WrapMode = DataGridViewTriState.True Then
						textFormat = textFormat Or TextFormatFlags.WordBreak
					End If
					TextRenderer.DrawText(g, CType(cell.Value, String), Me.Font, cellRect, textColor, textBackColor, TextFormatFlags.VerticalCenter)

					e.Handled = True
				End If
			End If
		Else
			' Highlight when using "Windows Default" or no theme.
			If (e.RowIndex > -1) AndAlso (e.ColumnIndex > -1) Then
				If TypeOf Me.Columns(e.ColumnIndex) Is DataGridViewTextBoxColumn Then
					Dim cell As DataGridViewCell = Me.Rows(e.RowIndex).Cells(e.ColumnIndex)

					If Me.Enabled Then
						If cell Is Me.theCellWherePointerIs Then
							cell.Style.BackColor = SystemColors.Control
						Else
							cell.Style.BackColor = DefaultCellStyle.BackColor
						End If
					End If
				End If
			End If
		End If

		MyBase.OnCellPainting(e)
	End Sub

	' When new row is added, commit it by tricking the datagridview.
	Protected Overrides Sub OnCurrentCellChanged(ByVal e As System.EventArgs)
		Try
			If Me.CurrentCell IsNot Nothing Then
				If Not Me.theCurrentCellIsChangingBecauseOfMe AndAlso Me.CurrentRow.IsNewRow AndAlso TypeOf Me.DataSource Is System.ComponentModel.IBindingList Then
					'Grab the object bound to the new row of the grid. We have to
					' access this object from the underlying BindingContext because
					' the DataBoundItem of the grid's new row returns nothing. This
					' is because the new object hasn't been added to the bound list,
					' so it isn't technically data-bound to the grid.

					Dim newObject As Object = Me.BindingContext(Me.DataSource).Current
					Dim curCell As Point = Me.CurrentCellAddress

					Me.theCurrentCellIsChangingBecauseOfMe = True
					Me.CancelEdit()
					'NOTE: This line raises an exception if there are no rows already in the DataSource.
					Me.CurrentCell = Nothing
					'Programmatically add the new object to the bound list.
					' We're assuming the bound list is an implementation of the
					' IBindingList interface. 
					DirectCast(Me.DataSource, System.ComponentModel.IBindingList).Add(newObject)
					Me.CurrentCell = Me(curCell.X, curCell.Y)
					Me.theCurrentCellIsChangingBecauseOfMe = False
				End If
			End If

			MyBase.OnCurrentCellChanged(e)
		Catch ex As Exception
		Finally
		End Try
	End Sub

	Protected Overrides Sub OnCurrentCellDirtyStateChanged(ByVal e As System.EventArgs)
		' Force immediate update when clicking a checkbox cell or selecting a value in combobox cell.
		If TypeOf Me.CurrentCell Is DataGridViewCheckBoxCell OrElse TypeOf Me.CurrentCell Is DataGridViewComboBoxCell Then
			Me.CommitEdit(DataGridViewDataErrorContexts.Commit)
		End If

		MyBase.OnCurrentCellDirtyStateChanged(e)
	End Sub

	Protected Overrides Sub OnDataError(ByVal displayErrorDialogIfNoHandler As Boolean, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs)
		' Disable default error window.
		'e.Cancel = False
		displayErrorDialogIfNoHandler = False

		MyBase.OnDataError(displayErrorDialogIfNoHandler, e)
	End Sub

	Protected Overrides Sub OnColumnAdded(e As DataGridViewColumnEventArgs)
		MyBase.OnColumnAdded(e)
		Dim theme As DataGridViewTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.DataGridViewTheme
		End If
		If theme IsNot Nothing Then
			If TypeOf e.Column Is DataGridViewButtonColumn Then
				Dim buttonColumn As DataGridViewButtonColumn = CType(e.Column, DataGridViewButtonColumn)
				buttonColumn.FlatStyle = FlatStyle.Flat
				'buttonColumn.FlatStyle = FlatStyle.Popup
				'buttonColumn.DefaultCellStyle.ForeColor = WidgetConstants.WidgetTextColor
				'buttonColumn.DefaultCellStyle.BackColor = WidgetConstants.WidgetHighBackColor
				'buttonColumn.DefaultCellStyle.SelectionForeColor = Color.Red
				'buttonColumn.DefaultCellStyle.SelectionBackColor = Color.Green
			End If
		End If
	End Sub

	Protected Overrides Sub OnColumnWidthChanged(e As DataGridViewColumnEventArgs)
		MyBase.OnColumnWidthChanged(e)
		'NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
		Win32Api.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
		Me.Invalidate()
		Me.UpdateScrollbars()
	End Sub

	Protected Overrides Sub OnRowsAdded(e As DataGridViewRowsAddedEventArgs)
		MyBase.OnRowsAdded(e)
		'NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
		Win32Api.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
		Me.Invalidate()
		Me.UpdateScrollbars()
	End Sub

	Protected Overrides Sub OnRowsRemoved(e As DataGridViewRowsRemovedEventArgs)
		MyBase.OnRowsRemoved(e)
		' Prevent an exception when closing app.
		If Me.Parent IsNot Nothing Then
			'NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
			Win32Api.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
			Me.Invalidate()
			Me.UpdateScrollbars()
		End If
	End Sub

	Protected Overrides Sub OnSelectionChanged(ByVal e As System.EventArgs)
		'NOTE: Prevent the "New" row being selected so it doesn't cause problems.
		If Not Me.theSelectionIsChangingBecauseOfMe AndAlso Me.NewRowIndex >= 0 Then
			Me.theSelectionIsChangingBecauseOfMe = True
			Me.Rows(Me.NewRowIndex).Selected = False
			Me.theSelectionIsChangingBecauseOfMe = False
		End If

		MyBase.OnSelectionChanged(e)
	End Sub

	Protected Overrides Sub OnScroll(e As ScrollEventArgs)
		MyBase.OnScroll(e)
		Me.Invalidate()
		Me.UpdateScrollbars()
	End Sub

	Protected Overrides Sub OnSizeChanged(e As EventArgs)
		' This check prevents incorrect painting due to premature creation of Handle.
		'    Also, prevents unneeded resizing and painting when scrolling.
		If Me.theControlHasShown Then
			MyBase.OnSizeChanged(e)
			If Not Me.theScrollingIsActive Then
				'NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
				Win32Api.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
				Me.Invalidate()
				Me.UpdateScrollbars()
				'Me.Refresh()
			End If
		End If
	End Sub

	Protected Overrides Sub OnVisibleChanged(e As EventArgs)
		MyBase.OnVisibleChanged(e)

		If Me.Visible Then
			If Not Me.theControlHasShown Then
				Me.theControlHasShown = True

				'NOTE: Raise the OnNonClientCalcSize and OnNonClientPaint "events".
				Win32Api.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32Api.SWP.SWP_FRAMECHANGED Or Win32Api.SWP.SWP_NOMOVE Or Win32Api.SWP.SWP_NOSIZE Or Win32Api.SWP.SWP_NOZORDER)
			End If

			Me.Invalidate()
			Me.UpdateScrollbars()
		End If
	End Sub

	Protected Function MyProcessTabKey(ByVal keysPressed As Keys) As Boolean
		Dim retValue As Boolean = MyBase.ProcessTabKey(keysPressed)

		'While Me.CurrentCell.[ReadOnly]
		'	retValue = MyBase.ProcessTabKey(keysPressed)
		'End While
		'------
		Dim previousCell As DataGridViewCell = Nothing
		While Me.CurrentCell.[ReadOnly] AndAlso previousCell IsNot Me.CurrentCell
			previousCell = Me.CurrentCell
			retValue = MyBase.ProcessTabKey(keysPressed)
		End While

		'' Reverse direction of tabbing in case at end of grid and on a read-only cell.
		'keysPressed = keysPressed Xor Keys.Shift
		'previousCell = Nothing
		''While Me.CurrentCell.[ReadOnly] AndAlso previousCell IsNot Me.CurrentCell AndAlso (Me.CurrentCell.RowIndex = 0 AndAlso Me.CurrentCell.ColumnIndex = 0) OrElse (Me.CurrentCell.RowIndex = Me.RowCount - 1 AndAlso Me.CurrentCell.ColumnIndex = Me.ColumnCount - 1)
		'While Me.CurrentCell.[ReadOnly] AndAlso previousCell IsNot Me.CurrentCell
		'	previousCell = Me.CurrentCell
		'	retValue = MyBase.ProcessTabKey(keysPressed)
		'End While

		Return retValue
	End Function

	Protected Overloads Overrides Function ProcessDataGridViewKey(ByVal e As KeyEventArgs) As Boolean
		If e.KeyCode = Keys.Tab Then
			Dim keysPressed As Keys
			If e.Shift Then
				keysPressed = (Keys.Shift Or Keys.Tab)
			Else
				keysPressed = Keys.Tab
			End If
			MyProcessTabKey(keysPressed)
			Return True
		End If
		'If e.KeyCode = Keys.Enter Then
		'	' Instead of moving down to next row, begin editing of cell.
		'	Me.BeginEdit(True)
		'	Return True
		'End If
		Return MyBase.ProcessDataGridViewKey(e)
	End Function

	Protected Overloads Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
		'NOTE: If this 'if' block is used, then tabbing in and out of datagridview won't work.
		'If keyData = Keys.Tab Then
		'	'Dim keysPressed As Keys
		'	'If e.Shift Then
		'	'	keysPressed = (Keys.Shift Or Keys.Tab)
		'	'Else
		'	'	keysPressed = Keys.Tab
		'	'End If
		'	'MyProcessTabKey(keysPressed)
		'	MyProcessTabKey(Keys.Tab)
		'	Return True
		'End If
		Dim key As Keys = (keyData And Keys.KeyCode)
		If key = Keys.Enter Then
			' Instead of moving down to next row, validate the cell (by changing the CurrentCell), which effectively ends editing of cell.
			Dim savedCurrentCell As DataGridViewCell = Me.CurrentCell
			Me.CurrentCell = Nothing
			Me.CurrentCell = savedCurrentCell
			'If savedCurrentCell.IsInEditMode Then
			'	Me.EndEdit()
			'End If
			Return True
		End If
		Return MyBase.ProcessDialogKey(keyData)
	End Function

	Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
		MyBase.OnMouseWheel(e)

		If Me.CustomVerticalScrollBar.Visible Then
			'NOTE: Scroll by 3 rows.
			Dim rowCount As Integer = Me.RowCount
			If rowCount > 0 Then
				Dim upOrDownValue As Integer = Me.Rows(0).Height * 3
				If e.Delta > 0 Then
					' Moving wheel away from user = up.
					Me.CustomVerticalScrollBar.Value -= upOrDownValue
				Else
					' Moving wheel toward user = down.
					Me.CustomVerticalScrollBar.Value += upOrDownValue
				End If
			End If
		End If
	End Sub

	'Protected Overrides Sub OnPaint(e As PaintEventArgs)
	'    Dim theme As DataGridViewTheme = Nothing
	'    ' This check prevents problems with viewing and saving Forms in VS Designer.
	'    If TheApp IsNot Nothing Then
	'        theme = TheApp.Settings.SelectedAppTheme.DataGridViewTheme
	'    End If
	'    If theme IsNot Nothing Then
	'        If Me.Enabled Then
	'            Me.GridColor = theme.EnabledForeColor
	'            'Me.BackgroundColor = theme.EnabledBackColor
	'            Me.BackgroundColor = Color.Red
	'        Else
	'            Me.GridColor = theme.DisabledForeColor
	'            'Me.BackgroundColor = theme.DisabledBackColor
	'            Me.BackgroundColor = Color.Red
	'        End If
	'    End If

	'    MyBase.OnPaint(e)

	'    ''Dim theme As DataGridViewTheme = Nothing
	'    ''' This check prevents problems with viewing and saving Forms in VS Designer.
	'    ''If TheApp IsNot Nothing Then
	'    ''    theme = TheApp.Settings.SelectedAppTheme.DataGridViewTheme
	'    ''End If
	'    'If theme IsNot Nothing Then
	'    '    Dim g As Graphics = e.Graphics
	'    '    Dim clientRectangle As Rectangle = Me.ClientRectangle
	'    '    ' Draw outer border.
	'    '    If Me.BorderStyle <> BorderStyle.None Then
	'    '        Dim borderColor As Color
	'    '        borderColor = theme.EnabledBorderColor
	'    '        Using borderColorPen As New Pen(borderColor)
	'    '            g.DrawRectangle(borderColorPen, clientRectangle.Left, clientRectangle.Top, clientRectangle.Width - 1, clientRectangle.Height - 1)
	'    '        End Using
	'    '    End If
	'    'End If
	'End Sub

	Protected Overrides Sub WndProc(ByRef m As Message)
		Select Case m.Msg
			Case Win32Api.WindowsMessages.WM_NCCALCSIZE
				Me.OnNonClientCalcSize(m)
			Case Win32Api.WindowsMessages.WM_NCPAINT
				Me.OnNonClientPaint(m)
		End Select

		MyBase.WndProc(m)
	End Sub

	Private Sub OnNonClientCalcSize(ByRef m As Message)
		Dim theme As DataGridViewTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.DataGridViewTheme
		End If
		If theme IsNot Nothing Then
			Me.UpdateNonClientPadding()
			If CInt(m.WParam) = 0 Then
				Dim rect As Win32Api.RECT = CType(Marshal.PtrToStructure(m.LParam, GetType(Win32Api.RECT)), Win32Api.RECT)
				Me.ResizeClientRect(Me.NonClientPadding, rect)
				Marshal.StructureToPtr(rect, m.LParam, False)
				m.Result = IntPtr.Zero
			ElseIf CInt(m.WParam) = 1 Then
				Dim nccsp As Win32Api.NCCALCSIZE_PARAMS = CType(Marshal.PtrToStructure(m.LParam, GetType(Win32Api.NCCALCSIZE_PARAMS)), Win32Api.NCCALCSIZE_PARAMS)
				Me.ResizeClientRect(Me.NonClientPadding, nccsp.rect0)
				Marshal.StructureToPtr(nccsp, m.LParam, False)
				m.Result = IntPtr.Zero
			End If
		End If
	End Sub

	Private Sub OnNonClientPaint(ByRef m As Message)
		Dim theme As DataGridViewTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.DataGridViewTheme
		End If
		If theme IsNot Nothing Then
			Dim borderColor As Color
			Dim borderWidth As Integer
			If Me.Enabled Then
				'If Me.theButtonCanBeFocused AndAlso Me.theMouseIsOverButton Then
				'	borderColor = theme.FocusBorderColor
				'	borderWidth = theme.FocusBorderWidth
				'Else
				borderColor = theme.EnabledBorderColor
				borderWidth = theme.EnabledBorderWidth
				'End If
			Else
				borderColor = theme.DisabledBorderColor
				borderWidth = theme.DisabledBorderWidth
			End If

			Dim hDC As IntPtr = Win32Api.GetWindowDC(Me.Handle)
			Try
				Using g As Graphics = Graphics.FromHdc(hDC)
					' Draw border.
					Using borderColorPen As New Pen(borderColor, borderWidth)
						borderColorPen.Alignment = Drawing2D.PenAlignment.Inset
						Dim aRect As Rectangle = Rectangle.Truncate(g.VisibleClipBounds)
						If borderWidth = 1 Then
							'NOTE: DrawRectangle width and height are interpreted as the right and bottom pixels to draw when pen width is 1.
							aRect.Width -= 1
							aRect.Height -= 1
						End If
						g.DrawRectangle(borderColorPen, aRect)
					End Using
				End Using
			Finally
				Win32Api.ReleaseDC(Me.Handle, hDC)
			End Try
			m.Result = IntPtr.Zero
		End If
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub CustomHorizontalScrollbar_ValueChanged(ByVal sender As Object, ByVal e As ScrollValueEventArgs) Handles CustomHorizontalScrollbar.ValueChanged
		'Me.UpdateScrolling(e.Value, 0)
		If Not Me.theScrollingIsActive Then
			Me.theScrollingIsActive = True

			Me.HorizontalScrollingOffset = e.Value
			'Me.Invalidate()

			Me.theScrollingIsActive = False
		End If
	End Sub

	Private Sub CustomVerticalScrollBar_ValueChanged(ByVal sender As Object, ByVal e As ScrollValueEventArgs) Handles CustomVerticalScrollBar.ValueChanged
		'Me.UpdateScrolling(0, e.Value)
		If Not Me.theScrollingIsActive Then
			Me.theScrollingIsActive = True

			If Me.RowCount > 0 Then
				Dim rowHeight As Integer = Me.Rows(0).Height
				Dim rowIndex As Integer = CInt(e.Value / rowHeight)
				If rowIndex > Me.RowCount - 1 Then
					rowIndex = Me.RowCount - 1
				End If
				Me.FirstDisplayedScrollingRowIndex = rowIndex
			End If
			'Me.Invalidate()

			Me.theScrollingIsActive = False
		End If
	End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub AppSettings_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		If e.PropertyName = "AppThemeName" Then
			Me.UpdateTheme()
			Me.Refresh()
		End If
	End Sub

#End Region

#Region "Private Methods"

	Private Sub UpdateTheme()
		Dim theme As DataGridViewTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.DataGridViewTheme
		End If
		If theme IsNot Nothing Then
			'Me.theBorderColor = theme.EnabledBorderColor

			If Me.Enabled Then
				Me.GridColor = theme.EnabledBackColor
				Me.BackgroundColor = theme.EnabledBackColor
				Me.ColumnHeadersDefaultCellStyle.ForeColor = theme.FocusForeColor
				Me.ColumnHeadersDefaultCellStyle.BackColor = theme.FocusBackColor
				Me.DefaultCellStyle.ForeColor = theme.EnabledForeColor
				Me.DefaultCellStyle.BackColor = theme.EnabledBackColor
			Else
				Me.GridColor = theme.DisabledBackColor
				Me.BackgroundColor = theme.DisabledBackColor
				Me.ColumnHeadersDefaultCellStyle.ForeColor = theme.DisabledForeColor
				Me.ColumnHeadersDefaultCellStyle.BackColor = theme.DisabledBackColor
				Me.DefaultCellStyle.ForeColor = theme.DisabledForeColor
				Me.DefaultCellStyle.BackColor = theme.DisabledBackColor
			End If

			MyBase.ScrollBars = Windows.Forms.ScrollBars.None
			Me.CustomHorizontalScrollbar.BackColor = theme.EnabledBackColor
			Me.CustomHorizontalScrollbar.RightAndBottomBorderColor = theme.EnabledBorderColor
			Me.CustomVerticalScrollBar.BackColor = theme.EnabledBackColor
			Me.CustomVerticalScrollBar.RightAndBottomBorderColor = theme.EnabledBorderColor
			Me.ScrollbarCornerPanel.BackColor = Me.BackColor
			Me.ScrollbarCornerPanel.RightAndBottomBorderColor = theme.EnabledBorderColor
		Else
			MyBase.ScrollBars = Windows.Forms.ScrollBars.Both
			Me.GridColor = SystemColors.ControlDark
			Me.BackgroundColor = SystemColors.Control
			Me.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.WindowText
			Me.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control
			Me.DefaultCellStyle.ForeColor = SystemColors.WindowText
			Me.DefaultCellStyle.BackColor = SystemColors.Control
		End If
	End Sub

	Private Sub UpdateNonClientPadding()
		If Me.DesignMode Then
			Exit Sub
		End If

		Dim left As Integer = 0
		Dim top As Integer = 0
		Dim right As Integer = 0
		Dim bottom As Integer = 0

		If Me.AutoSizeColumnsMode <> DataGridViewAutoSizeColumnsMode.Fill Then
			Dim contentWidth As Integer = Me.Columns.GetColumnsWidth(DataGridViewElementStates.Visible)
			'Dim clientWidth As Integer = Math.Min(Me.ClientRectangle.Width, Me.Width)
			Dim clientWidth As Integer = Me.Width
			If contentWidth > clientWidth Then
				bottom += ScrollBarEx.Consts.ScrollBarSize
			End If
		End If
		Dim rowCount As Integer = Me.RowCount
		If rowCount > 0 Then
			'Dim contentHeight As Integer = rowCount * Me.Rows(0).Height
			Dim contentHeight As Integer = Me.Rows.GetRowsHeight(DataGridViewElementStates.Visible)
			'Dim clientHeight As Integer = Math.Min(Me.ClientRectangle.Height, Me.Height)
			Dim clientHeight As Integer = Me.ClientRectangle.Height
			If Me.ColumnHeadersVisible Then
				'contentHeight += Me.ColumnHeadersHeight
				' No idea why, but the "* 2" makes this work.
				contentHeight += Me.ColumnHeadersHeight * 2
			End If
			If contentHeight > clientHeight Then
				right += ScrollBarEx.Consts.ScrollBarSize
			End If
		End If

		Dim theme As DataGridViewTheme = Nothing
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.DataGridViewTheme
		End If
		If theme IsNot Nothing Then
			Dim borderWidth As Integer
			If Me.Enabled Then
				'If Me.theButtonCanBeFocused AndAlso Me.theMouseIsOverButton Then
				'	borderWidth = theme.FocusBorderWidth
				'Else
				borderWidth = theme.EnabledBorderWidth
				'End If
			Else
				borderWidth = theme.DisabledBorderWidth
			End If
			left += borderWidth
			top += borderWidth
			right += borderWidth
			bottom += borderWidth
		End If

		Me.NonClientPadding = New Padding(left, top, right, bottom)
	End Sub

	Private Sub ResizeClientRect(ByVal padding As Padding, ByRef rect As Win32Api.RECT)
		rect.Left += padding.Left
		rect.Top += padding.Top
		rect.Right -= padding.Right
		rect.Bottom -= padding.Bottom
	End Sub

	'Private Sub UpdateScrolling(ByVal leftOrRightValue As Integer, ByVal upOrDownValue As Integer)
	'    If Not Me.theScrollingIsActive Then
	'        Me.theScrollingIsActive = True

	'        If leftOrRightValue <> 0 Then
	'            Me.HorizontalScrollingOffset = leftOrRightValue
	'        End If
	'        'If upOrDownValue <> 0 Then
	'        '    Me.VerticalScrollingOffset += upOrDownValue
	'        'End If
	'        If upOrDownValue <> 0 AndAlso Me.RowCount > 0 Then
	'            Dim rowHeight As Integer = Me.Rows(0).Height
	'            Dim rowIndex As Integer = CInt(upOrDownValue / rowHeight)
	'            If rowIndex > Me.RowCount - 1 Then
	'                rowIndex = Me.RowCount - 1
	'            End If
	'            Me.FirstDisplayedScrollingRowIndex = rowIndex
	'        End If
	'        Me.Invalidate()

	'        Me.theScrollingIsActive = False
	'    End If
	'End Sub

	Private Sub UpdateScrollbars()
		If Me.DesignMode Then
			Exit Sub
		End If

		Dim theme As DataGridViewTheme = Nothing
		' This check prevents problems with viewing and saving Forms in VS Designer.
		If TheApp IsNot Nothing Then
			theme = TheApp.Settings.SelectedAppTheme.DataGridViewTheme
		End If
		If theme IsNot Nothing Then
			If Me.AutoSizeColumnsMode <> DataGridViewAutoSizeColumnsMode.Fill Then
				Me.UpdateHorizontalScrollbar()
			End If
			Me.UpdateVerticalScrollbar()

			If Me.CustomHorizontalScrollbar.Visible AndAlso Me.CustomVerticalScrollBar.Visible Then
				'If Me.theBorderStyle = Windows.Forms.BorderStyle.FixedSingle Then
				'	Me.ScrollbarCornerPanel.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize + Me.theBorderWidth, ScrollBarEx.Consts.ScrollBarSize + Me.theBorderWidth)
				'	Me.ScrollbarCornerPanel.RightAndBottomBorderWidth = 1
				'Else
				'	Me.ScrollbarCornerPanel.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize, ScrollBarEx.Consts.ScrollBarSize)
				'	Me.ScrollbarCornerPanel.RightAndBottomBorderWidth = 0
				'End If
				'If theme.EnabledBorderWidth > 0 Then
				Me.ScrollbarCornerPanel.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize + theme.EnabledBorderWidth, ScrollBarEx.Consts.ScrollBarSize + theme.EnabledBorderWidth)
				Me.ScrollbarCornerPanel.RightAndBottomBorderWidth = theme.EnabledBorderWidth
				'Else
				'	Me.ScrollbarCornerPanel.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize, ScrollBarEx.Consts.ScrollBarSize)
				'	Me.ScrollbarCornerPanel.RightAndBottomBorderWidth = 0
				'End If
				'NOTE: Assign to Parent so it can draw over non-client area.
				Me.ScrollbarCornerPanel.Parent = Me.Parent
				Me.ScrollbarCornerPanel.BringToFront()
				Dim aPoint As New Point(Me.ClientRectangle.Width, Me.ClientRectangle.Height)
				'NOTE: Location must be relative to Parent.
				aPoint = Me.PointToScreen(aPoint)
				aPoint = Me.ScrollbarCornerPanel.Parent.PointToClient(aPoint)
				Me.ScrollbarCornerPanel.Location = aPoint
				Me.ScrollbarCornerPanel.Show()
			Else
				Me.ScrollbarCornerPanel.Hide()
			End If
		Else
			Me.theScrollingIsActive = True
			Me.CustomHorizontalScrollbar.Hide()
			Me.CustomVerticalScrollBar.Hide()
			Me.ScrollbarCornerPanel.Hide()
			Me.theScrollingIsActive = False
		End If
	End Sub

	Private Sub UpdateHorizontalScrollbar()
		'NOTE: Parent can be Nothing on exiting. Prevent the exception with this check.
		If Not Me.theScrollingIsActive AndAlso Me.Parent IsNot Nothing Then
			Dim contentWidth As Integer = Me.Columns.GetColumnsWidth(DataGridViewElementStates.Visible)
			Dim clientWidth As Integer = Me.ClientRectangle.Width
			'Dim clientWidth As Integer = Me.Width
			If contentWidth > clientWidth Then
				Me.theScrollingIsActive = True

				Me.CustomHorizontalScrollbar.Minimum = 0
				Me.CustomHorizontalScrollbar.Maximum = contentWidth
				Me.CustomHorizontalScrollbar.Value = Me.HorizontalScrollingOffset
				Me.CustomHorizontalScrollbar.ViewSize = clientWidth
				Me.CustomHorizontalScrollbar.SmallChange = 100
				Me.CustomHorizontalScrollbar.LargeChange = clientWidth

				'NOTE: Assign to Parent so it can draw over non-client area.
				Me.CustomHorizontalScrollbar.Parent = Me.Parent
				Me.CustomHorizontalScrollbar.BringToFront()
				'NOTE: Point is relative to Me.ClientRectangle.
				'Dim aPoint As New Point(Me.ClientRectangle.Left - Me.NonClientPadding.Left, Me.ClientRectangle.Height + Me.NonClientPadding.Bottom - ScrollBarEx.Consts.ScrollBarSize)
				Dim aPoint As New Point(Me.ClientRectangle.Left, Me.ClientRectangle.Height)
				'NOTE: Location must be relative to Parent.
				aPoint = Me.PointToScreen(aPoint)
				aPoint = Me.CustomHorizontalScrollbar.Parent.PointToClient(aPoint)
				Me.CustomHorizontalScrollbar.Location = aPoint
				'If Me.theBorderStyle = Windows.Forms.BorderStyle.FixedSingle Then
				'	Me.CustomHorizontalScrollbar.Size = New System.Drawing.Size(Me.ClientRectangle.Width, ScrollBarEx.Consts.ScrollBarSize + Me.theBorderWidth)
				'	Me.CustomHorizontalScrollbar.RightAndBottomBorderWidth = 1
				'Else
				'	Me.CustomHorizontalScrollbar.Size = New System.Drawing.Size(Me.ClientRectangle.Width, ScrollBarEx.Consts.ScrollBarSize)
				'	Me.CustomHorizontalScrollbar.RightAndBottomBorderWidth = 0
				'End If
				Dim theme As DataGridViewTheme = TheApp.Settings.SelectedAppTheme.DataGridViewTheme
				'If theme.EnabledBorderWidth > 0 Then
				Me.CustomHorizontalScrollbar.Size = New System.Drawing.Size(Me.ClientRectangle.Width, ScrollBarEx.Consts.ScrollBarSize + theme.EnabledBorderWidth)
				Me.CustomHorizontalScrollbar.RightAndBottomBorderWidth = theme.EnabledBorderWidth
				'Else
				'	Me.CustomHorizontalScrollbar.Size = New System.Drawing.Size(Me.ClientRectangle.Width, ScrollBarEx.Consts.ScrollBarSize)
				'	Me.CustomHorizontalScrollbar.RightAndBottomBorderWidth = 0
				'End If
				Me.CustomHorizontalScrollbar.Show()

				Me.theScrollingIsActive = False
			Else
				Me.theScrollingIsActive = True
				Me.CustomHorizontalScrollbar.Hide()
				Me.theScrollingIsActive = False
			End If
		End If
	End Sub

	Private Sub UpdateVerticalScrollbar()
		'NOTE: Parent can be Nothing on exiting. Prevent the exception with this check.
		If Not Me.theScrollingIsActive AndAlso Me.Parent IsNot Nothing Then
			Dim rowCount As Integer = Me.RowCount
			If rowCount > 0 Then
				Dim rowHeight As Integer = Me.Rows(0).Height
				'Dim contentHeight As Integer = rowCount * rowHeight
				Dim contentHeight As Integer = Me.Rows.GetRowsHeight(DataGridViewElementStates.Visible)
				Dim clientHeight As Integer = Me.ClientRectangle.Height
				'Dim clientHeight As Integer = Me.Height
				If Me.ColumnHeadersVisible Then
					contentHeight += Me.ColumnHeadersHeight
					clientHeight -= Me.ColumnHeadersHeight
				End If
				If contentHeight > clientHeight Then
					Me.theScrollingIsActive = True

					Me.CustomVerticalScrollBar.Minimum = 0
					Me.CustomVerticalScrollBar.Maximum = contentHeight
					Me.CustomVerticalScrollBar.Value = Me.VerticalScrollingOffset
					Me.CustomVerticalScrollBar.ViewSize = clientHeight
					Me.CustomVerticalScrollBar.SmallChange = rowHeight
					Me.CustomVerticalScrollBar.LargeChange = clientHeight - rowHeight * 2

					'NOTE: Assign to Parent so it can draw over non-client area.
					Me.CustomVerticalScrollBar.Parent = Me.Parent
					Me.CustomVerticalScrollBar.BringToFront()
					'NOTE: Point is relative to Me.ClientRectangle.
					'Dim aPoint As New Point(Me.ClientRectangle.Width + Me.NonClientPadding.Right - ScrollBarEx.Consts.ScrollBarSize, Me.ClientRectangle.Top - Me.NonClientPadding.Top)
					Dim aPoint As New Point(Me.ClientRectangle.Width, Me.ClientRectangle.Top)
					'NOTE: Location must be relative to Parent.
					aPoint = Me.PointToScreen(aPoint)
					aPoint = Me.CustomVerticalScrollBar.Parent.PointToClient(aPoint)
					Me.CustomVerticalScrollBar.Location = aPoint
					'If Me.theBorderStyle = Windows.Forms.BorderStyle.FixedSingle Then
					'	Me.CustomVerticalScrollBar.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize + Me.theBorderWidth, Me.ClientRectangle.Height)
					'	Me.CustomVerticalScrollBar.RightAndBottomBorderWidth = 1
					'Else
					'	Me.CustomVerticalScrollBar.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize, Me.ClientRectangle.Height)
					'	Me.CustomVerticalScrollBar.RightAndBottomBorderWidth = 0
					'End If
					Dim theme As DataGridViewTheme = TheApp.Settings.SelectedAppTheme.DataGridViewTheme
					'If theme.EnabledBorderWidth > 0 Then
					Me.CustomVerticalScrollBar.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize + theme.EnabledBorderWidth, Me.ClientRectangle.Height)
					Me.CustomVerticalScrollBar.RightAndBottomBorderWidth = theme.EnabledBorderWidth
					'Else
					'	Me.CustomVerticalScrollBar.Size = New System.Drawing.Size(ScrollBarEx.Consts.ScrollBarSize, Me.ClientRectangle.Height)
					'	Me.CustomVerticalScrollBar.RightAndBottomBorderWidth = 0
					'End If
					Me.CustomVerticalScrollBar.Show()

					Me.theScrollingIsActive = False
				Else
					Me.theScrollingIsActive = True
					Me.CustomVerticalScrollBar.Hide()
					Me.theScrollingIsActive = False
				End If
			End If
		End If
	End Sub

#End Region

#Region "Data"

	'Private theScrollBars As ScrollBars

	'Private theBorderColor As Color
	'Private theBorderStyle As BorderStyle
	'Private theBorderWidth As Integer
	Private NonClientPadding As Padding
	'Private theNonClientPaddingColor As Color

	Private WithEvents CustomHorizontalScrollbar As ScrollBarEx
	Private WithEvents CustomVerticalScrollBar As ScrollBarEx
	Private ScrollbarCornerPanel As ScrollBarPanel
	Private theControlHasShown As Boolean
	Private theScrollingIsActive As Boolean

	Private theCurrentCellIsChangingBecauseOfMe As Boolean
	Private theSelectionIsChangingBecauseOfMe As Boolean
	'Private theWidgetTempOfAllowUserToAddRows As Boolean

	Private theCellWherePointerIs As DataGridViewCell

#End Region

End Class
