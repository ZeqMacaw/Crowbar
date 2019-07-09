Imports System.Collections.Specialized
Imports System.IO

' Code convereted from: http://www.codeproject.com/Articles/286326/Async-Drag-n-Drop-or-Drag-n-Drop-from-external-ser
Public Class FileDragDropHelper
	Inherits DataObject

	Public Sub New(ByVal startupAction As StartupDelegate, ByVal cleanupAction As CleanupDelegate)
		Me.theStartupAction = startupAction
		Me.theCleanupAction = cleanupAction
		Me.theFunctionHasBeenCalledOnce = False
		Me.pathFileNameIndex = 0
	End Sub

	'Public Overrides Function GetDataPresent(format As String) As Boolean
	'	'Dim fileDropList As StringCollection = GetFileDropList()

	'	'If fileDropList.Count > _downloadIndex Then
	'	'	Dim fileName As String = Path.GetFileName(fileDropList(_downloadIndex))

	'	'	If String.IsNullOrEmpty(fileName) Then
	'	'		Return False
	'	'	End If
	'	'	'_downloadAction(fileDropList(_downloadIndex))
	'	'	Me.theReadFileAction.Invoke()
	'	'	_downloadIndex += 1
	'	'End If
	'	'======
	'	Dim result As Boolean

	'	'If Not Me.theFunctionHasBeenCalledOnce Then
	'	'	Me.theStartupAction.Invoke()
	'	'	Me.theFunctionHasBeenCalledOnce = True
	'	'End If

	'	result = MyBase.GetDataPresent(format)

	'	'Dim pathFileNameCollection As StringCollection = GetFileDropList()
	'	'If pathFileNameCollection.Count > Me.pathFileNameIndex Then
	'	'	Me.pathFileNameIndex += 1
	'	'End If
	'	''If Me.pathFileNameIndex = pathFileNameCollection.Count Then
	'	''	Me.theCleanupAction.Invoke()
	'	''End If

	'	Return result
	'End Function

	Public Overrides Function GetData(format As String) As Object
		Dim result As Object

		'If Not Me.theFunctionHasBeenCalledOnce Then
		'	Me.theStartupAction.Invoke()
		'	Me.theFunctionHasBeenCalledOnce = True
		'End If

		'Dim pathFileNameCollection As StringCollection = GetFileDropList()
		'Dim test As String
		'test = pathFileNameCollection(0)

		If [String].Compare(format, Win32Api.CFSTR_FILEDESCRIPTORW, StringComparison.OrdinalIgnoreCase) = 0 Then
			'MyBase.SetData(Win32Api.CFSTR_FILEDESCRIPTORW, GetFileDescriptor(m_SelectedItems))
			Dim debug As Integer = 4242
		End If

		result = MyBase.GetData(format)

		Return result
	End Function

	'Public Overrides Function GetData(format As String) As Object
	'	Dim result As Object

	'	result = MyBase.GetData(format)

	'	Dim pathFileNameCollection As StringCollection = GetFileDropList()
	'	'If Me.pathFileNameIndex = pathFileNameCollection.Count Then
	'	'	Me.theCleanupAction.Invoke()
	'	'End If

	'	Return result
	'End Function

	'Public Overrides Sub SetData(obj As Object)
	'	MyBase.SetData(obj)
	'End Sub

	'Public Overrides Sub SetData(format As String, autoConvert As Boolean, obj As Object)
	'	MyBase.SetData(format, autoConvert, obj)


	'	If Not Me.theSetDataFunctionHasBeenCalledOnce Then
	'		Me.theSetDataFunctionHasBeenCalledOnce = True
	'	Else
	'		Me.theCleanupAction.Invoke()
	'	End If
	'End Sub

	'Public Overrides Sub SetData(format As String, obj As Object)
	'	MyBase.SetData(format, obj)
	'End Sub

	'Public Overrides Sub SetData(format As Type, obj As Object)
	'	MyBase.SetData(format, obj)
	'End Sub

	Public Delegate Sub StartupDelegate()
	Public Delegate Sub CleanupDelegate()

	Private pathFileNameIndex As Integer
	Private theStartupAction As StartupDelegate
	Private theCleanupAction As CleanupDelegate
	Private theFunctionHasBeenCalledOnce As Boolean
	Private theSetDataFunctionHasBeenCalledOnce As Boolean

End Class
