Imports System.ComponentModel
Imports System.IO
Imports Steamworks

Public Class Left4Dead2SteamAppInfo
	Inherits SteamAppInfoBase

	Public Sub New()
		MyBase.New()

		Me.ID = New AppId_t(550)
		Me.Name = "Left 4 Dead 2"
		Me.UsesSteamUGC = False
		Me.CanUseContentFolderOrFile = False
		Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
		Me.TagsControlType = GetType(Left4Dead2TagsUserControl)
	End Sub

End Class
