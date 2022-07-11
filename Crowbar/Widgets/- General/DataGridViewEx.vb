Imports System.ComponentModel

Public Class DataGridViewEx
	Inherits DataGridView

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		'NOTE: Disable to use custom.
		MyBase.ScrollBars = Windows.Forms.ScrollBars.None

		'NOTE: Need these settings so that ColumnHeadersDefaultCellStyle, DefaultCellStyle, and GridColor properties are used.
		'      Might affect other properties, too.
		Me.EnableHeadersVisualStyles = False
		Me.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
		Me.CellBorderStyle = DataGridViewCellBorderStyle.Single
		Me.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single

		Me.ForeColor = WidgetConstants.WidgetTextColor
		Me.BackgroundColor = WidgetConstants.WidgetBackColor

		Me.ColumnHeadersDefaultCellStyle.ForeColor = WidgetConstants.WidgetTextColor
		Me.ColumnHeadersDefaultCellStyle.BackColor = WidgetConstants.WidgetBackColor
		Me.ColumnHeadersDefaultCellStyle.SelectionForeColor = WidgetConstants.WidgetTextColor
		Me.ColumnHeadersDefaultCellStyle.SelectionBackColor = WidgetConstants.WidgetSelectedBackColor

		Me.DefaultCellStyle.ForeColor = WidgetConstants.WidgetTextColor
		Me.DefaultCellStyle.BackColor = WidgetConstants.WidgetBackColor
		Me.DefaultCellStyle.SelectionForeColor = WidgetConstants.WidgetTextColor
		Me.DefaultCellStyle.SelectionBackColor = WidgetConstants.WidgetSelectedBackColor

		Me.RowHeadersDefaultCellStyle.ForeColor = WidgetConstants.WidgetTextColor
		Me.RowHeadersDefaultCellStyle.BackColor = WidgetConstants.WidgetBackColor
		Me.RowHeadersDefaultCellStyle.SelectionForeColor = WidgetConstants.WidgetTextColor
		Me.RowHeadersDefaultCellStyle.SelectionBackColor = WidgetConstants.WidgetSelectedBackColor

		Me.GridColor = WidgetConstants.WidgetDisabledTextColor
		'Me.GridColor = Color.Green
		Me.BorderStyle = BorderStyle.None

		Me.theCurrentCellIsChangingBecauseOfMe = False
		Me.theSelectionIsChangingBecauseOfMe = False
	End Sub

#End Region

#Region "Init and Free"

#End Region

#Region "Properties"

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

	<Browsable(False)>
	Public Overloads ReadOnly Property HorizontalScrollBar() As ScrollBar
		Get
			Return MyBase.HorizontalScrollBar
		End Get
	End Property

	Public Overloads Property [ReadOnly]() As Boolean
		Get
			Return MyBase.ReadOnly
		End Get
		Set(ByVal value As Boolean)
			If MyBase.ReadOnly <> value Then
				MyBase.ReadOnly = value

				If MyBase.ReadOnly Then
					Me.DefaultCellStyle.BackColor = WidgetConstants.WidgetBackColor
				Else
					Me.DefaultCellStyle.BackColor = WidgetConstants.WidgetDisabledTextColor
				End If
			End If
		End Set
	End Property

	<Browsable(True)>
	<Category("Layout")>
	<Description("Colorable scrollbars.")>
	Public Overloads Property ScrollBars As ScrollBars
		Get
			Return Me.theScrollBars
		End Get
		Set
			Me.theScrollBars = Value
		End Set
	End Property

#End Region

#Region "Methods"

#End Region

#Region "Widget Event Handlers"

	Protected Overrides Sub OnPaint(e As PaintEventArgs)
		MyBase.OnPaint(e)

		' Draw outer border.
		Using backColorPen As New Pen(WidgetConstants.WidgetDisabledTextColor)
			Dim aRect As Rectangle = Me.ClientRectangle
			aRect.Width -= 1
			aRect.Height -= 1
			e.Graphics.DrawRectangle(backColorPen, aRect)
		End Using
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

	Protected Overrides Sub OnCellPainting(ByVal e As DataGridViewCellPaintingEventArgs)
		If (e.RowIndex > -1) AndAlso (e.ColumnIndex > -1) AndAlso (TypeOf Me.Columns(e.ColumnIndex) Is DataGridViewRadioButtonColumn) Then
			e.PaintBackground(e.CellBounds, False)
			DataGridViewRadioButtonColumn.Paint(e.Graphics, e.CellBounds, (e.FormattedValue.ToString().Length > 0))
			e.Handled = True
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

	Protected Overrides Sub OnSelectionChanged(ByVal e As System.EventArgs)
		'NOTE: Prevent the "New" row being selected so it doesn't cause problems.
		If Not Me.theSelectionIsChangingBecauseOfMe AndAlso Me.NewRowIndex >= 0 Then
			Me.theSelectionIsChangingBecauseOfMe = True
			Me.Rows(Me.NewRowIndex).Selected = False
			Me.theSelectionIsChangingBecauseOfMe = False
		End If

		MyBase.OnSelectionChanged(e)
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

#End Region

#Region "Data"

	Private theScrollBars As ScrollBars

	Private theCurrentCellIsChangingBecauseOfMe As Boolean
	Private theSelectionIsChangingBecauseOfMe As Boolean
	Private theWidgetTempOfAllowUserToAddRows As Boolean

#End Region

End Class
