Imports System.ComponentModel
Imports System.IO
Imports Steamworks

Public Class HuntDownTheFreemanSteamAppInfo
	Inherits SteamAppInfoBase

	Public Sub New()
		MyBase.New()

		Me.ID = New AppId_t(723390)
		Me.Name = "Hunt Down The Freeman"
		Me.UsesSteamUGC = True
		Me.CanUseContentFolderOrFile = False
		'Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
		Me.TagsControlType = GetType(HuntDownTheFreemanTagsUserControl)
	End Sub

End Class
