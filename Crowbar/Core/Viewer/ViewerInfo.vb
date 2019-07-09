Public Class ViewerInfo

	Public viewerAction As ViewerActionType
    Public gameSetupSelectedIndex As Integer
	Public mdlPathFileName As String
	Public viewAsReplacement As Boolean
	Public viewAsReplacementExtraSubfolder As String

	Public Enum ViewerActionType
		GetData
		ViewModel
		OpenViewer
	End Enum

End Class
