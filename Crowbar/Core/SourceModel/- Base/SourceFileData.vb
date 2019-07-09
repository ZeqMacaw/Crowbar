' Base class for Source___FileData classes.
Public Class SourceFileData

#Region "Creation and Destruction"

	Public Sub New()
		Me.theFileSeekLog = New FileSeekLog()
		Me.theUnknownValues = New List(Of UnknownValue)()
	End Sub

#End Region

#Region "Data"

	Public theFileSeekLog As FileSeekLog
	Public theUnknownValues As List(Of UnknownValue)

#End Region

End Class
