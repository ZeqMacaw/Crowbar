Imports System.ComponentModel
Imports System.Runtime.Serialization
Imports System.Xml.Serialization

'<XmlRoot("BindigList")> _
Public Class BindingListEx(Of T)
	Inherits BindingList(Of T)
	Implements IEquatable(Of BindingListEx(Of T))

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		'failedToAdd = False
	End Sub

#End Region

#Region "Operators"

	Public Overrides Function Equals(ByVal otherObject As Object) As Boolean
		Dim otherList As BindingListEx(Of T) = CType(otherObject, BindingListEx(Of T))
		If otherList IsNot Nothing Then
			Return Me.Equals(otherList)
		Else
			Return False
		End If
	End Function

	Public Overloads Function Equals(ByVal otherList As BindingListEx(Of T)) As Boolean Implements IEquatable(Of BindingListEx(Of T)).Equals
		If otherList Is Nothing Then
			Return False
		End If
		Dim result As Boolean = False
		If Me.Count = otherList.Count Then
			result = True
			For i As Integer = 0 To Me.Count - 1
				If Not Me(i).Equals(otherList(i)) Then
					Return False
				End If
			Next
		End If
		Return result
	End Function

#End Region

#Region "Properties"

	<XmlIgnore()> _
	Public Property Container() As Object
		Get
			Return theContainer
		End Get
		Set(ByVal value As Object)
			theContainer = value
		End Set
	End Property

#End Region

#Region "Methods"

	'Public Function GetItemByName(ByVal name As String) As T
	'	Dim sortedProperty As PropertyDescriptor = FindPropertyDescriptor("Name")
	'	If sortedProperty IsNot Nothing Then
	'		Dim items As List(Of T) = CType(Me.Items, List(Of T))
	'		If items IsNot Nothing Then
	'			Dim pc As PropertyComparer(Of T) = New PropertyComparer(Of T)(sortedProperty, ListSortDirection.Ascending)
	'			Dim itemIndex As Integer = items.BinarySearch(Item, pc)
	'			If itemIndex >= 0 Then
	'				Return items(itemIndex)
	'			End If
	'		End If
	'	End If
	'	Return Nothing
	'End Function

	Public Sub InsertItemSorted(ByVal index As Integer, ByVal item As T, ByVal nProperty As String)
		_sort = FindPropertyDescriptor(nProperty)
		Me.InsertItemSorted(index, item, _sort)
	End Sub

	Public Sub InsertItemSorted(ByVal index As Integer, ByVal item As T, ByVal sortedProperty As PropertyDescriptor)
		index = Me.FindInsertionIndex(index, item, sortedProperty)
		MyBase.InsertItem(index, item)
	End Sub

	Public Overloads Sub Sort()
		ApplySortCore(_sort, _dir)
	End Sub

	Public Overloads Sub Sort(ByVal nProperty As String)
		_sort = FindPropertyDescriptor(nProperty)
		ApplySortCore(_sort, _dir)
	End Sub

	Public Overloads Sub Sort(ByVal nProperty As String, ByVal direction As ListSortDirection)
		_sort = FindPropertyDescriptor(nProperty)
		_dir = direction
		ApplySortCore(_sort, _dir)
	End Sub

	Public Overrides Sub EndNew(ByVal itemIndex As Integer)
		If _sort IsNot Nothing AndAlso itemIndex = Me.Count - 1 Then
			ApplySortCore(_sort, _dir)
		End If
		MyBase.EndNew(itemIndex)
	End Sub

#End Region

#Region "Event Handlers"

#End Region

#Region "Private Methods"

	' Override so that an extra ListChanged event with ListChangedType.ItemDeleted  
	' is raised BEFORE the item is deleted. 
	'NOTE: Can't set NewIndex to negative number, so change OldIndex to something other than -1.
	' NewIndex = item's index that will be deleted.
	' OldIndex = -2 (to distinguish from normal ListChangedType.ItemDeleted event).
	Protected Overrides Sub RemoveItem(ByVal index As Integer)
		Try
			Me.OnListChanged(New ListChangedEventArgs(ListChangedType.ItemDeleted, index, -2))
		Catch
		End Try
		Dim removedItem As T = Items(index)
		'NOTE: The base class RemoveItem raises the expected ListChanged event with 
		'      ListChangedType.ItemDeleted and the already-deleted item's index.
		MyBase.RemoveItem(index)
	End Sub

	Protected Function FindInsertionIndex(ByVal index As Integer, ByVal item As T, ByVal sortedProperty As PropertyDescriptor) As Integer
		Dim insertionIndex As Integer
		Dim items As List(Of T) = CType(Me.Items, List(Of T))
		If items IsNot Nothing And sortedProperty IsNot Nothing Then
			Dim pc As PropertyComparer(Of T) = New PropertyComparer(Of T)(sortedProperty, ListSortDirection.Ascending)
			Dim itemIndex As Integer = items.BinarySearch(item, pc)
			If itemIndex < 0 Then
				insertionIndex = itemIndex Xor -1
			Else
				' Find last (instead of arbitrary) index with the given value.
				insertionIndex = itemIndex + 1
				While insertionIndex < items.Count
					itemIndex = items.BinarySearch(insertionIndex, items.Count - insertionIndex, item, pc)
					If itemIndex < 0 Then
						Exit While
					End If
					'insertionIndex += 1
					insertionIndex = itemIndex + 1
				End While
			End If
		End If
		Return insertionIndex
	End Function

	Protected Overrides ReadOnly Property SupportsSortingCore() As Boolean
		Get
			Return True
		End Get
	End Property

	Protected Overrides ReadOnly Property IsSortedCore() As Boolean
		Get
			Return _isSorted
		End Get
	End Property

	Protected Overrides ReadOnly Property SortDirectionCore() As ListSortDirection
		Get
			Return _dir
		End Get
	End Property

	Protected Overrides Sub ApplySortCore(ByVal nProperty As PropertyDescriptor, ByVal direction As ListSortDirection)
		Dim items As List(Of T) = CType(Me.Items, List(Of T))
		If items IsNot Nothing And nProperty IsNot Nothing Then
			Dim pc As PropertyComparer(Of T) = New PropertyComparer(Of T)(nProperty, direction)
			items.Sort(pc)
			_isSorted = True
			'NOTE: Although this is convention to raise a "Reset" event, require code to manually call a reset instead.
			'' Raise the ListChanged Reset event so bound controls refresh their values.
			'Me.OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
		Else
			_isSorted = False
		End If
	End Sub

	Protected Overrides Sub RemoveSortCore()
		_isSorted = False
	End Sub

	Protected Function FindPropertyDescriptor(ByVal nProperty As String) As PropertyDescriptor
		Dim pdc As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(T))
		Dim prop As PropertyDescriptor = Nothing
		If pdc IsNot Nothing Then
			prop = pdc.Find(nProperty, True)
		End If
		Return prop
	End Function

	Protected Overrides ReadOnly Property SupportsSearchingCore() As Boolean
		Get
			Return True
		End Get
	End Property

	Protected Overrides Function FindCore(ByVal propertyDesc As PropertyDescriptor, ByVal key As Object) As Integer
		Dim i As Integer
		Dim propInfo As Reflection.PropertyInfo = GetType(T).GetProperty(propertyDesc.Name)
		Dim item As T
		If key IsNot Nothing Then
			For i = 0 To Count - 1
				item = CType(Items(i), T)
				If propInfo.GetValue(item, Nothing).Equals(key) Then
					Return i
				End If
			Next
		End If
		Return -1
	End Function

	'' Override so that an extra ListChanged event with ListChangedType.Reset  
	'' is raised BEFORE the items are cleared. 
	'Protected Overrides Sub ClearItems()
	'	'Try
	'	'	Me.OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, 0, 0))
	'	'Catch
	'	'End Try
	'	MyBase.ClearItems()
	'End Sub

	'Protected Overrides Sub SetItem(ByVal index As Integer, ByVal item As T)
	'	MyBase.SetItem(index, item)
	'End Sub

	'Protected Overrides Function AddNewCore() As Object
	'	Dim index As Integer
	'	Dim obj As Object
	'	obj = MyBase.AddNewCore()
	'	index = Me.Items.Count - 1
	'	If Me.RaiseListChangedEvents Then
	'		Me.OnListChanged(New ListChangedEventArgs(ListChangedType.ItemAdded, index, -1))
	'	End If
	'	Return obj
	'End Function

	'Protected Overrides Sub InsertItem(ByVal index As Integer, ByVal item As T)
	'	MyBase.InsertItem(index, item)
	'End Sub

	'<OnDeserialized()> _
	'Private Sub OnDeserialized(ByVal context As StreamingContext)
	'	Dim items As List(Of t) = New List(Of t)(Me.Items)
	'	Dim index As Integer = 0
	'	'// call SetItem again on each item  
	'	'// to re-establish event hookups
	'	For Each item As t In items
	'		'// explicitly call the base version 
	'		'// in case SetItem is overridden
	'		MyBase.SetItem(index, item)
	'		index += 1
	'	Next
	'End Sub

#End Region

#Region "Data"

	Private theContainer As Object
	'Private failedToAdd As Boolean

	Private _isSorted As Boolean
	Private _dir As ListSortDirection = ListSortDirection.Ascending
	Private _sort As PropertyDescriptor = Nothing

#End Region

End Class
