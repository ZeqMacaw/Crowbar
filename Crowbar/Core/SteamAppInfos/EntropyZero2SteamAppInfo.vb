Imports System.ComponentModel
Imports System.IO
Imports Steamworks

Public Class EntropyZero2SteamAppInfo
	Inherits SteamAppInfoBase

	Public Sub New()
		MyBase.New()

		Me.ID = New AppId_t(1583720)
		Me.Name = "Entropy : Zero 2"
		Me.UsesSteamUGC = True
		Me.CanUseContentFolderOrFile = False
		'Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
		Me.TagsControlType = GetType(EntropyZero2TagsUserControl)
	End Sub

End Class
