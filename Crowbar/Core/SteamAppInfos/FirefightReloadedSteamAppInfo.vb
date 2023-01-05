Imports System.ComponentModel
Imports System.IO
Imports Steamworks

Public Class FirefightReloadedSteamAppInfo
	Inherits SteamAppInfoBase

	Public Sub New()
		MyBase.New()

		Me.ID = New AppId_t(397680)
		Me.Name = "FIREFIGHT RELOADED"
		Me.UsesSteamUGC = True
		Me.CanUseContentFolderOrFile = False
		'Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
		Me.TagsControlType = GetType(FirefightReloadedTagsUserControl)
	End Sub

End Class
