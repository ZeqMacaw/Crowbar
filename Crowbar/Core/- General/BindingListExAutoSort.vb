Imports System.ComponentModel

Public Class BindingListExAutoSort(Of T)
	Inherits BindingListEx(Of T)

	Public Sub New(ByVal nProperty As String)
		Me.theSortedPropertyName = nProperty
		Me.theSortedProperty = FindPropertyDescriptor(nProperty)
	End Sub

	Protected Overrides Sub InsertItem(ByVal index As Integer, ByVal item As T)
		MyBase.InsertItemSorted(index, item, Me.theSortedProperty)
	End Sub

	'Public Overloads Sub ResetItem(ByVal index As Integer)
	'	MyBase.ResetItem(index)
	'End Sub

	Protected Overrides Sub OnListChanged(ByVal e As System.ComponentModel.ListChangedEventArgs)
		'If e.ListChangedType = ListChangedType.ItemChanged AndAlso e.PropertyDescriptor IsNot Nothing AndAlso e.PropertyDescriptor.Name = Me.theSortedPropertyName Then
		'	Dim obj As Object = Me.Items(e.NewIndex)
		'	MyBase.ApplySortCore(Me.theSortedProperty, ListSortDirection.Ascending)
		'	Dim aEventArgs As New ListChangedEventArgs(ListChangedType.ItemMoved, Me.IndexOf(CType(obj, T)), e.NewIndex)
		'	MyBase.OnListChanged(aEventArgs)
		'Else
		'	MyBase.OnListChanged(e)
		'End If
		'======
		'NOTE: Raise an extra new event, ItemMoved, so that widgets can know when an item moved because of auto-sorting.
		If e.ListChangedType = ListChangedType.ItemChanged AndAlso e.PropertyDescriptor IsNot Nothing AndAlso e.PropertyDescriptor.Name = Me.theSortedPropertyName Then
			Dim obj As Object = Me.Items(e.NewIndex)
			Me.Items.RemoveAt(e.NewIndex)
			Dim insertionIndex As Integer
			insertionIndex = Me.FindInsertionIndex(0, CType(obj, T), Me.theSortedProperty)
			Me.Items.Insert(insertionIndex, CType(obj, T))
			Dim aEventArgs As New ListChangedEventArgs(ListChangedType.ItemMoved, insertionIndex, e.NewIndex)
			MyBase.OnListChanged(aEventArgs)
			'Else
			'	MyBase.OnListChanged(e)
		End If
		MyBase.OnListChanged(e)
	End Sub

	Private theSortedPropertyName As String
	Private theSortedProperty As PropertyDescriptor

End Class
