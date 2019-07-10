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
		Me.CanUseContentFolderOrFile = True
		Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
		Me.TagsControlType = GetType(ZombiePanicSourceTagsUserControl)
	End Sub

	Public Enum ZombiePanicSourceGameModeTypeTags
		<Description("None")> None
		<Description("Hardcore")> Hardcore
		<Description("Objective")> Objective
		<Description("Survival")> Survival
		<Description("Custom")> Custom
	End Enum

	Public Enum ZombiePanicSourceCustomModelsTypeTags
		<Description("None")> None
		<Description("Characters")> Characters
		<Description("Props")> Props
		<Description("Weapons")> Weapons
	End Enum

	Public Enum ZombiePanicSourceCustomSoundsTypeTags
		<Description("None")> None
		<Description("Characters Sounds")> CharactersSounds
		<Description("Weapons Sounds")> WeaponsSounds
	End Enum

	Public Enum ZombiePanicSourceMiscellaneousTypeTags
		<Description("None")> None
		<Description("GUIs")> GUIs
		<Description("Model Pack")> ModelPack
		<Description("Sound Pack")> SoundPack
	End Enum

End Class
