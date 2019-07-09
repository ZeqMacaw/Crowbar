Imports System.ComponentModel

Public Class DateTimeTextBoxEx
	Inherits TextBoxEx

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.theTextBinding = Nothing

		AddHandler Me.DataBindings.CollectionChanged, AddressOf DataBindings_CollectionChanged
	End Sub

	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If Not Me.IsDisposed Then
			If disposing Then
				RemoveHandler Me.DataBindings.CollectionChanged, AddressOf DataBindings_CollectionChanged
				If Me.theTextBinding IsNot Nothing Then
					Me.RemoveBinding()
				End If
				'Me.Free()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

#End Region

#Region "Private Methods"

	Private Sub UpdateValue()
		Me.DataBindings("Text").ReadValue()
	End Sub

	Private Sub InsertBinding(ByVal aBinding As Binding)
		AddHandler aBinding.Format, AddressOf Me.Binding_Format
		''NOTE: Use Binding_Parse() instead of OnValidating() because OnValidating() is called after value has been saved to data source. 
		'AddHandler aBinding.Parse, AddressOf Me.Binding_Parse
		Me.theTextBinding = aBinding
		Me.UpdateValue()
	End Sub

	Private Sub RemoveBinding()
		RemoveHandler Me.theTextBinding.Format, AddressOf Me.Binding_Format
		'RemoveHandler Me.theTextBinding.Parse, AddressOf Me.Binding_Parse
		Me.theTextBinding = Nothing
	End Sub

	Private Sub Binding_Format(ByVal sender As Object, ByVal e As ConvertEventArgs)
		If e.DesiredType IsNot GetType(String) OrElse Not TypeOf e.Value Is Long Then
			Exit Sub
		End If

		Dim iUnixTimeStamp As Long
		iUnixTimeStamp = CType(e.Value, Long)

		e.Value = Me.GetFormattedValue(iUnixTimeStamp)
	End Sub

	'Private Sub Binding_Parse(ByVal sender As Object, ByVal e As ConvertEventArgs)
	'	If e.DesiredType IsNot GetType(UnitlessProperty) Then
	'		Exit Sub
	'	End If

	'	Dim aBinding As Binding = CType(sender, Binding)
	'	Dim dataSourceType As Type = aBinding.DataSource.GetType()
	'	Dim dataMember As System.Reflection.PropertyInfo = dataSourceType.GetProperty(aBinding.BindingMemberInfo.BindingField)

	'	Dim aUnitlessProperty As UnitlessProperty
	'	aUnitlessProperty = CType(dataMember.GetValue(aBinding.DataSource, Nothing), UnitlessProperty)

	'	e.Value = Me.GetParsedValue(CStr(e.Value), aUnitlessProperty)
	'End Sub

	Private Function GetFormattedValue(ByVal iUnixTimeStamp As Long) As String
		Dim valueString As String
		valueString = MathModule.UnixTimeStampToDateTime(iUnixTimeStamp).ToShortDateString() + " " + MathModule.UnixTimeStampToDateTime(iUnixTimeStamp).ToShortTimeString()
		Return valueString
	End Function

	'Private Function GetParsedValue(ByVal valueString As String, ByVal aUnitlessProperty As UnitlessProperty) As UnitlessProperty
	'	Dim aValue As Double
	'	Try
	'		aValue = Double.Parse(valueString)
	'		If aValue < aUnitlessProperty.MinimumValue Then
	'			aValue = aUnitlessProperty.MinimumValue
	'		ElseIf aValue > aUnitlessProperty.MaximumValue Then
	'			aValue = aUnitlessProperty.MaximumValue
	'		End If
	'	Catch
	'		aValue = 0
	'	End Try
	'	aUnitlessProperty.Value = aValue

	'	Return aUnitlessProperty
	'End Function

#End Region

#Region "Core Event Handlers"

#End Region

#Region "Widget Event Handlers"

	Protected Sub DataBindings_CollectionChanged(ByVal sender As Object, ByVal e As CollectionChangeEventArgs)
		If e.Action = CollectionChangeAction.Add Then
			Dim aBinding As Binding
			aBinding = CType(e.Element, Binding)
			If aBinding.PropertyName = "Text" Then
				Me.InsertBinding(aBinding)
			End If
		ElseIf e.Action = CollectionChangeAction.Remove Then
			Dim aBinding As Binding
			aBinding = CType(e.Element, Binding)
			If aBinding Is Me.theTextBinding Then
				Me.RemoveBinding()
			End If
		ElseIf e.Action = CollectionChangeAction.Refresh Then
			If Me.theTextBinding IsNot Nothing Then
				Me.RemoveBinding()
			End If
			For Each aBinding As Binding In Me.DataBindings
				If aBinding.PropertyName = "Text" Then
					Me.InsertBinding(aBinding)
				End If
			Next
		End If
	End Sub

#End Region

#Region "Data"

	Private theTextBinding As Binding

#End Region

End Class
