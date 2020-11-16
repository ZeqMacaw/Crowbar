Imports System.Globalization

Public Class FolderAndFileListViewItemComparer
	Implements IComparer

	'Public Sub New()
	'	col = 0
	'End Sub

	Public Sub New(ByVal column As Integer, ByVal order As SortOrder)
		Me.col = column
		Me.order = order
	End Sub

	Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
		Dim returnVal As Integer = -1
		Dim xItem As ListViewItem
		Dim yItem As ListViewItem

		xItem = CType(x, ListViewItem)
		yItem = CType(y, ListViewItem)

		'NOTE: Must use "And" so that both expressions are evaluated.
		' The SubItems(4) means "Extension" column; using the 4 here because already using several item.SubItems.Add() that are dependent on order of columns anyway.
		If xItem.SubItems(4).Text = "<Folder>" And yItem.SubItems(4).Text <> "<Folder>" Then
			returnVal = -1
		ElseIf xItem.SubItems(4).Text <> "<Folder>" And yItem.SubItems(4).Text = "<Folder>" Then
			returnVal = 1
		Else
			If Me.col = 1 AndAlso xItem.SubItems(4).Text <> "<Folder>" Then
				If Int32.Parse(xItem.SubItems(Me.col).Text, NumberStyles.Integer Or NumberStyles.AllowThousands, TheApp.InternalCultureInfo) < Int32.Parse(yItem.SubItems(Me.col).Text, NumberStyles.Integer Or NumberStyles.AllowThousands, TheApp.InternalCultureInfo) Then
					returnVal = -1
				Else
					returnVal = 1
				End If
			Else
				returnVal = String.Compare(xItem.SubItems(Me.col).Text, yItem.SubItems(Me.col).Text)
			End If
			If Me.order = SortOrder.Descending Then
				returnVal *= -1
			End If
		End If

		Return returnVal
	End Function

	Private col As Integer
	Private order As SortOrder

End Class
