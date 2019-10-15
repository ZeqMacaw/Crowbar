Imports System.ComponentModel
Imports System.IO
Imports Steamworks

Public Class ZombiePanicSourceSteamAppInfo
	Inherits SteamAppInfoBase

	Public Sub New()
		MyBase.New()

		Me.ID = New AppId_t(17500)
		Me.Name = "Zombie Panic! Source"
		Me.UsesSteamUGC = True
		Me.CanUseContentFolderOrFile = False
		'Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
		Me.TagsControlType = GetType(ZombiePanicSourceTagsUserControl)
	End Sub

	Public Enum ZombiePanicSourceTypeTags
		<Description("GameMode")> GameMode
		<Description("Custom Models")> CustomModels
		<Description("Custom Sounds")> CustomSounds
		<Description("Miscellaneous")> Miscellaneous
	End Enum

End Class
