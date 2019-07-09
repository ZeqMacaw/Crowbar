Public Class SteamAppUserInfo

    'NOTE: Only here because XMLSerializer uses it.
    Public Sub New()
        MyBase.New()
        Me.Create()
    End Sub

    Public Sub New(ByVal iAppID As UInteger)
        Me.theAppID = iAppID
        Me.Create()
    End Sub

    Private Sub Create()
        Me.theDraftItems = New BindingListEx(Of WorkshopItem)()
    End Sub

    Public Property AppID() As UInteger
        Get
            Return Me.theAppID
        End Get
        Set(ByVal value As UInteger)
            Me.theAppID = value
        End Set
    End Property

	Public Property DraftTemplateAndChangedItems() As BindingListEx(Of WorkshopItem)
		Get
			Return Me.theDraftItems
		End Get
		Set(ByVal value As BindingListEx(Of WorkshopItem))
			Me.theDraftItems = value
		End Set
	End Property

	Private theAppID As UInteger
    Private theDraftItems As BindingListEx(Of WorkshopItem)

End Class
