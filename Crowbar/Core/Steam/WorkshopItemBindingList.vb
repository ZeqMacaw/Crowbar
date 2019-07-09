Public Class WorkshopItemBindingList
	Inherits BindingListEx(Of WorkshopItem)

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.theDraftItemCount = 0
		Me.theTemplateItemCount = 0
		Me.theChangedItemCount = 0
		Me.thePublishedItemCount = 0
	End Sub

#End Region

#Region "Properties"

	Public ReadOnly Property DraftItemCount() As UInteger
		Get
			Return Me.theDraftItemCount
		End Get
	End Property

	Public ReadOnly Property TemplateItemCount() As UInteger
		Get
			Return Me.theTemplateItemCount
		End Get
	End Property

	Public ReadOnly Property ChangedItemCount() As UInteger
		Get
			Return Me.theChangedItemCount
		End Get
	End Property

	Public ReadOnly Property PublishedItemCount() As UInteger
		Get
			Return Me.thePublishedItemCount
		End Get
	End Property

#End Region

#Region "Event Handlers"

	Protected Overrides Sub OnListChanged(e As System.ComponentModel.ListChangedEventArgs)
		MyBase.OnListChanged(e)

		If e.ListChangedType = System.ComponentModel.ListChangedType.ItemAdded Then
			Dim item As WorkshopItem = Me(e.NewIndex)
			If item.IsDraft Then
				Me.theDraftItemCount += 1UI
			ElseIf item.IsTemplate Then
				Me.theTemplateItemCount += 1UI
			ElseIf item.IsChanged Then
				Me.theChangedItemCount += 1UI
			Else
				Me.thePublishedItemCount += 1UI
			End If
		ElseIf e.ListChangedType = System.ComponentModel.ListChangedType.ItemDeleted AndAlso e.OldIndex = -2 Then
			Dim item As WorkshopItem = Me(e.NewIndex)
			If item.IsDraft Then
				Me.theDraftItemCount -= 1UI
			ElseIf item.IsTemplate Then
				Me.theTemplateItemCount -= 1UI
			ElseIf item.IsChanged Then
				Me.theChangedItemCount -= 1UI
			Else
				Me.thePublishedItemCount -= 1UI
			End If
			'ElseIf e.ListChangedType = System.ComponentModel.ListChangedType.ItemDeleted AndAlso e.OldIndex = -1 Then
		ElseIf e.ListChangedType = System.ComponentModel.ListChangedType.Reset Then
			Me.theDraftItemCount = 0
			Me.theTemplateItemCount = 0
			Me.theChangedItemCount = 0
			Me.thePublishedItemCount = 0
		End If
	End Sub

#End Region

#Region "Data"

	Private theDraftItemCount As UInteger
	Private theTemplateItemCount As UInteger
	Private theChangedItemCount As UInteger
	Private thePublishedItemCount As UInteger

#End Region

End Class
