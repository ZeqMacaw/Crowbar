Public Class SourceModelProgressEventArgs
	Inherits System.EventArgs

	Public Sub New(ByVal progress As ProgressOptions, ByVal message As String)
		MyBase.New()

		Me.theProgress = progress
		Me.theMessage = message
	End Sub

	Public ReadOnly Property Progress As ProgressOptions
		Get
			Return Me.theProgress
		End Get
	End Property

	Public Property Message As String
		Get
			Return Me.theMessage
		End Get
		Set(value As String)
			Me.theMessage = value
		End Set
	End Property

	Private theProgress As ProgressOptions
	Private theMessage As String

End Class