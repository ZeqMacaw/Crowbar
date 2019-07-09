Imports System.ComponentModel
Imports System.IO
Imports Steamworks

Public Class BlackMesaSteamAppInfo
	Inherits SteamAppInfoBase

	Public Sub New()
		MyBase.New()

		Me.ID = New AppId_t(362890)
		Me.Name = "Black Mesa"
		Me.UsesSteamUGC = True
		Me.CanUseContentFolderOrFile = False
		'Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
		Me.TagsControlType = GetType(BlackMesaTagsUserControl)
	End Sub

End Class
