Imports System.Collections.ObjectModel

Public Class SingleInstanceEventArgs
	Inherits EventArgs

	Public Sub New(ByVal commandLine As ReadOnlyCollection(Of String), ByVal mainForm As Form)
		_commandLine = commandLine
		_mainForm = mainForm
	End Sub

	Public ReadOnly Property CommandLine As ReadOnlyCollection(Of String)
		Get
			Return _commandLine
		End Get
	End Property

	Public ReadOnly Property MainForm As Form
		Get
			Return _mainForm
		End Get
	End Property

	Private _commandLine As ReadOnlyCollection(Of String)
	Private _mainForm As Form

End Class
