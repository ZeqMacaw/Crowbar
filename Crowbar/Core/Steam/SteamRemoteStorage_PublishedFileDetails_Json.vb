Public Class SteamRemoteStorage_PublishedFileDetails_Json

	Public Property response As SteamRemoteStorage_PublishedFileDetails_Response

	Public Class SteamRemoteStorage_PublishedFileDetails_Response
		Public Property result As Integer
		Public Property resultcount As Integer
		Public Property publishedfiledetails As List(Of SteamRemoteStorage_PublishedFileDetails_ItemDetail)
	End Class

	Public Class SteamRemoteStorage_PublishedFileDetails_ItemDetail
		Public Property publishedfileid As String
		Public Property result As Integer
		Public Property creator As String
		Public Property creator_app_id As Integer
		Public Property consumer_app_id As Integer
		Public Property filename As String
		Public Property file_size As Integer
		Public Property file_url As String
		Public Property hcontent_file As String
		Public Property preview_url As String
		Public Property hcontent_preview As String
		Public Property title As String
		Public Property description As String
		Public Property time_created As Integer
		Public Property time_updated As Integer
		Public Property visibility As Integer
		Public Property banned As Integer
		Public Property ban_reason As String
		Public Property subscriptions As Integer
		Public Property favorited As Integer
		Public Property lifetime_subscriptions As Integer
		Public Property lifetime_favorited As Integer
		Public Property views As Integer
		Public Property tags() As Object
	End Class

End Class
