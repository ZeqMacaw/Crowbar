Public Class ListBoxEx
	Inherits ListBox

	Public Sub New()
		MyBase.New()

		Me.DrawMode = DrawMode.OwnerDrawFixed
		Me.thePreviousHighlightItemIndex = -2
		Me.theHighlightItemIndex = -1
	End Sub

	Public Overloads ReadOnly Property PreferredSize() As Size
		Get
			'Return MyBase.PreferredSize
			Dim defaultSize As Size = MyBase.PreferredSize
			Dim maxWidth As Integer = defaultSize.Width
			Dim maxHeight As Integer = defaultSize.Height
			Dim itemText As String
			Dim textSize As Size
			For Each item As Object In Me.Items
				itemText = item.ToString()
				textSize = TextRenderer.MeasureText(itemText, Me.Font)
				If textSize.Width > maxWidth Then
					maxWidth = textSize.Width
				End If
				If textSize.Height > maxHeight Then
					maxHeight = textSize.Height
				End If
			Next
			Return New Size(maxWidth, maxHeight)
		End Get
	End Property

	Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
		'MyBase.OnDrawItem(e)

		If e.Index >= 0 AndAlso e.Index < Me.Items.Count Then
			e.DrawBackground()

			Dim itemIsSelected As Boolean = ((e.State And DrawItemState.Selected) > 0)
			Dim itemIsHighlighted As Boolean = (e.Index = Me.theHighlightItemIndex)
			Dim itemTextForeColor As Color
			Dim itemTextBackColor As Color

			If itemIsHighlighted OrElse (itemIsSelected AndAlso Me.theHighlightItemIndex < 0) Then
				itemTextForeColor = Color.White
				itemTextBackColor = Color.Green
			Else
				itemTextForeColor = Color.Black
				itemTextBackColor = Color.White
			End If

			Using backgroundColorBrush As New SolidBrush(itemTextBackColor)
				e.Graphics.FillRectangle(backgroundColorBrush, e.Bounds)
			End Using

			Dim itemText As String = Me.Items(e.Index).ToString()
			Dim rect As Rectangle = e.Bounds
			TextRenderer.DrawText(e.Graphics, itemText, e.Font, rect, itemTextForeColor, TextFormatFlags.Default)

			e.DrawFocusRectangle()
		End If
	End Sub

	'Protected Overrides Sub OnEnter(e As EventArgs)
	'	MyBase.OnEnter(e)

	'End Sub

	'Protected Overrides Sub OnLeave(e As EventArgs)
	'	MyBase.OnLeave(e)
	'End Sub

	Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
		MyBase.OnMouseMove(e)

		Me.thePreviousHighlightItemIndex = Me.theHighlightItemIndex

		Dim cursorPositionInClient As Point = e.Location
		Me.theHighlightItemIndex = Me.IndexFromPoint(cursorPositionInClient)

		If Me.thePreviousHighlightItemIndex <> Me.theHighlightItemIndex Then

			Dim itemRect As Rectangle
			If Me.thePreviousHighlightItemIndex >= 0 Then
				itemRect = Me.GetItemRectangle(Me.thePreviousHighlightItemIndex)
				Me.Invalidate(itemRect)
			End If
			If Me.SelectedIndex >= 0 Then
				itemRect = Me.GetItemRectangle(Me.SelectedIndex)
				Me.Invalidate(itemRect)
			End If
			If Me.theHighlightItemIndex >= 0 Then
				itemRect = Me.GetItemRectangle(Me.theHighlightItemIndex)
				Me.Invalidate(itemRect)
			End If
		End If
	End Sub

	Protected Overrides Sub OnVisibleChanged(e As EventArgs)
		MyBase.OnVisibleChanged(e)
		Me.theHighlightItemIndex = -1
	End Sub

	Private thePreviousHighlightItemIndex As Integer
	Private theHighlightItemIndex As Integer

End Class
