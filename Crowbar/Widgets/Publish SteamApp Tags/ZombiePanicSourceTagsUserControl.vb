Public Class ZombiePanicSourceTagsUserControl

	Public Sub New()
		MyBase.New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
	End Sub

	Protected Overrides Sub Init()
		MyBase.Init()

		Dim anEnumList As IList
		' GameMode
		anEnumList = EnumHelper.ToList(GetType(ZombiePanicSourceSteamAppInfo.ZombiePanicSourceGameModeTypeTags))
		Me.ComboBox_GameMode.DisplayMember = "Value"
		Me.ComboBox_GameMode.ValueMember = "Key"
		Me.ComboBox_GameMode.DataSource = anEnumList
		Me.ComboBox_GameMode.SelectedValue = ZombiePanicSourceSteamAppInfo.ZombiePanicSourceGameModeTypeTags.None

		' Custom models
		anEnumList = EnumHelper.ToList(GetType(ZombiePanicSourceSteamAppInfo.ZombiePanicSourceCustomModelsTypeTags))
		Me.ComboBox_CustomModels.DisplayMember = "Value"
		Me.ComboBox_CustomModels.ValueMember = "Key"
		Me.ComboBox_CustomModels.DataSource = anEnumList
		Me.ComboBox_CustomModels.SelectedValue = ZombiePanicSourceSteamAppInfo.ZombiePanicSourceCustomModelsTypeTags.None

		' Custom sounds
		anEnumList = EnumHelper.ToList(GetType(ZombiePanicSourceSteamAppInfo.ZombiePanicSourceCustomSoundsTypeTags))
		Me.ComboBox_CustomSounds.DisplayMember = "Value"
		Me.ComboBox_CustomSounds.ValueMember = "Key"
		Me.ComboBox_CustomSounds.DataSource = anEnumList
		Me.ComboBox_CustomSounds.SelectedValue = ZombiePanicSourceSteamAppInfo.ZombiePanicSourceCustomSoundsTypeTags.None

		' Miscellaneous
		anEnumList = EnumHelper.ToList(GetType(ZombiePanicSourceSteamAppInfo.ZombiePanicSourceMiscellaneousTypeTags))
		Me.ComboBox_Miscellaneous.DisplayMember = "Value"
		Me.ComboBox_Miscellaneous.ValueMember = "Key"
		Me.ComboBox_Miscellaneous.DataSource = anEnumList
		Me.ComboBox_Miscellaneous.SelectedValue = ZombiePanicSourceSteamAppInfo.ZombiePanicSourceMiscellaneousTypeTags.None

	End Sub

End Class
