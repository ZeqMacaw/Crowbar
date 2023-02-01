Imports System.ComponentModel
Imports System.IO
Imports Steamworks

Public Class InsurgencySteamAppInfo
	Inherits SteamAppInfoBase

	Public Sub New()
		MyBase.New()

		Me.ID = New AppId_t(222880)
		Me.Name = "Insurgency"
		Me.UsesSteamUGC = True
		Me.CanUseContentFolderOrFile = False
		'Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
		Me.TagsControlType = GetType(InsurgencyTagsUserControl)
	End Sub

End Class
