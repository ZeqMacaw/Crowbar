Imports System.ComponentModel
Imports System.IO
Imports Steamworks

Public Class JBModSteamAppInfo
	Inherits SteamAppInfoBase

	Public Sub New()
		MyBase.New()

		Me.ID = New AppId_t(2158860)
		Me.Name = "JBMod"
		Me.UsesSteamUGC = True
		Me.CanUseContentFolderOrFile = False
		'Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
		Me.TagsControlType = GetType(JBModTagsUserControl)
	End Sub

End Class
