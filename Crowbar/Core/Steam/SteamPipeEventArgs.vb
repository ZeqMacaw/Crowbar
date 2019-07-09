Imports System.Collections.ObjectModel
Imports Crowbar

Public Class SteamPipeEventArgs
	Inherits EventArgs

	Public Sub New(ByVal message As String)
		theMessage = message
	End Sub

	Public ReadOnly Property Message As String
		Get
			Return theMessage
		End Get
	End Property

	Private theMessage As String

	Public Shared Widening Operator CType(v As SteamPipeEventArgs) As String
		Throw New NotImplementedException()
	End Operator
End Class
