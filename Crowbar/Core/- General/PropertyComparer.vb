Imports System.ComponentModel

Class PropertyComparer(Of TKey)
	Inherits System.Collections.Generic.Comparer(Of TKey)

	Private _property As PropertyDescriptor
	Private _direction As ListSortDirection

	Public Sub New(ByVal nProperty As PropertyDescriptor, ByVal direction As ListSortDirection)
		MyBase.New()
		_property = nProperty
		_direction = direction
	End Sub

	Public Overrides Function Compare(ByVal xVal As TKey, ByVal yVal As TKey) As Integer
		Dim xValue As Object = GetPropertyValue(xVal, _property.Name)
		Dim yValue As Object = GetPropertyValue(yVal, _property.Name)
		If (_direction = ListSortDirection.Ascending) Then
			Return CompareAscending(xValue, yValue)
		Else
			Return CompareDescending(xValue, yValue)
		End If
	End Function

	Public Shadows Function Equals(ByVal xVal As TKey, ByVal yVal As TKey) As Boolean
		Return xVal.Equals(yVal)
	End Function

	Public Shadows Function GetHashCode(ByVal obj As TKey) As Integer
		Return obj.GetHashCode
	End Function

	Private Function CompareAscending(ByVal xValue As Object, ByVal yValue As Object) As Integer
		Dim result As Integer
		'If (xValue IsNot Nothing) Then
		'	result = CType(xValue, IComparable).CompareTo(yValue)
		'ElseIf xValue.Equals(yValue) Then
		'	result = 0
		'Else
		'	result = xValue.ToString.CompareTo(yValue.ToString)
		'End If
		If xValue Is Nothing Then
			If yValue Is Nothing Then
				result = 0
			Else
				result = -1
			End If
		Else
			If yValue Is Nothing Then
				result = 1
			Else
				result = CType(xValue, IComparable).CompareTo(yValue)
			End If
		End If
		Return result
	End Function

	Private Function CompareDescending(ByVal xValue As Object, ByVal yValue As Object) As Integer
		Return (CompareAscending(xValue, yValue) * -1)
	End Function

	Private Function GetPropertyValue(ByVal value As TKey, ByVal nProperty As String) As Object
		Dim propertyInfo As System.Reflection.PropertyInfo = value.GetType.GetProperty(nProperty)
		Return propertyInfo.GetValue(value, Nothing)
	End Function

End Class
