Public Class OptionsUserControl

#Region "Creation and Destruction"

	Public Sub New()
		' This call is required by the Windows Form Designer.
		InitializeComponent()

		'NOTE: Try-Catch is needed so that widget will be shown in MainForm without raising exception.
		Try
			Me.Init()
		Catch
		End Try
	End Sub

#End Region

#Region "Init and Free"

	Private Sub Init()
		Me.SingleInstanceCheckBox.DataBindings.Add("Checked", TheApp.Settings, "AppIsSingleInstance", False, DataSourceUpdateMode.OnPropertyChanged)

		' Auto-Open

		Me.AutoOpenVpkFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsAutoOpenVpkFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.AutoOpenGmaFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsAutoOpenGmaFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.AutoOpenFpxFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsAutoOpenFpxFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.AutoOpenMdlFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsAutoOpenMdlFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.AutoOpenMdlFileForPreviewCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsAutoOpenMdlFileForPreviewIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.AutoOpenMdlFileForDecompileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsAutoOpenMdlFileForDecompileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.AutoOpenMdlFileForViewCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsAutoOpenMdlFileForViewIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.AutoOpenQcFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsAutoOpenQcFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.InitAutoOpenRadioButtons()

		' Drag and Drop

		Me.DragAndDropMdlFileForPreviewCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsDragAndDropMdlFileForPreviewIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.DragAndDropMdlFileForDecompileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsDragAndDropMdlFileForDecompileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.DragAndDropMdlFileForViewCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsDragAndDropMdlFileForViewIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.InitDragAndDropRadioButtons()

		' Context Menu

		Me.IntegrateContextMenuItemsCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsContextMenuIntegrateMenuItemsIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.IntegrateAsSubmenuCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsContextMenuIntegrateSubMenuIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		'Me.OptionsContextMenuDecompileVpkFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsUnpackVpkFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		'Me.OptionsContextMenuDecompileFolderCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsUnpackFolderIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		'Me.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsUnpackFolderAndSubfoldersIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.OptionsContextMenuOpenWithCrowbarCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsOpenWithCrowbarIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.OptionsContextMenuViewMdlFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsViewMdlFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.OptionsContextMenuDecompileMdlFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsDecompileMdlFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.OptionsContextMenuDecompileFolderCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsDecompileFolderIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsDecompileFolderAndSubfoldersIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.OptionsContextMenuCompileQcFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsCompileQcFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.OptionsContextMenuCompileFolderCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsCompileFolderIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsCompileFolderAndSubfoldersIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.UpdateApplyPanel()

		AddHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged
	End Sub

	Private Sub InitAutoOpenRadioButtons()
		Me.AutoOpenVpkFileForUnpackRadioButton.Checked = (TheApp.Settings.OptionsAutoOpenVpkFileOption = ActionType.Unpack)
		Me.AutoOpenVpkFileForPublishRadioButton.Checked = (TheApp.Settings.OptionsAutoOpenVpkFileOption = ActionType.Publish)
		Me.AutoOpenGmaFileForUnpackRadioButton.Checked = (TheApp.Settings.OptionsAutoOpenGmaFileOption = ActionType.Unpack)
		Me.AutoOpenGmaFileForPublishRadioButton.Checked = (TheApp.Settings.OptionsAutoOpenGmaFileOption = ActionType.Publish)

		Me.AutoOpenMdlFileForPreviewingRadioButton.Checked = (TheApp.Settings.OptionsAutoOpenMdlFileOption = ActionType.Preview)
		Me.AutoOpenMdlFileForDecompilingRadioButton.Checked = (TheApp.Settings.OptionsAutoOpenMdlFileOption = ActionType.Decompile)
		Me.AutoOpenMdlFileForViewingRadioButton.Checked = (TheApp.Settings.OptionsAutoOpenMdlFileOption = ActionType.View)

		Me.AutoOpenFolderForUnpackRadioButton.Checked = (TheApp.Settings.OptionsAutoOpenFolderOption = ActionType.Unpack)
		Me.AutoOpenFolderForDecompileRadioButton.Checked = (TheApp.Settings.OptionsAutoOpenFolderOption = ActionType.Decompile)
		Me.AutoOpenFolderForCompileRadioButton.Checked = (TheApp.Settings.OptionsAutoOpenFolderOption = ActionType.Compile)
		Me.AutoOpenFolderForPackRadioButton.Checked = (TheApp.Settings.OptionsAutoOpenFolderOption = ActionType.Pack)
	End Sub

	Private Sub InitDragAndDropRadioButtons()
		Me.DragAndDropVpkFileForUnpackRadioButton.Checked = (TheApp.Settings.OptionsDragAndDropVpkFileOption = ActionType.Unpack)
		Me.DragAndDropVpkFileForPublishRadioButton.Checked = (TheApp.Settings.OptionsDragAndDropVpkFileOption = ActionType.Publish)
		Me.DragAndDropGmaFileForUnpackRadioButton.Checked = (TheApp.Settings.OptionsDragAndDropGmaFileOption = ActionType.Unpack)
		Me.DragAndDropGmaFileForPublishRadioButton.Checked = (TheApp.Settings.OptionsDragAndDropGmaFileOption = ActionType.Publish)

		Me.DragAndDropMdlFileForPreviewingRadioButton.Checked = (TheApp.Settings.OptionsDragAndDropMdlFileOption = ActionType.Preview)
		Me.DragAndDropMdlFileForDecompilingRadioButton.Checked = (TheApp.Settings.OptionsDragAndDropMdlFileOption = ActionType.Decompile)
		Me.DragAndDropMdlFileForViewingRadioButton.Checked = (TheApp.Settings.OptionsDragAndDropMdlFileOption = ActionType.View)

		Me.DragAndDropFolderForUnpackRadioButton.Checked = (TheApp.Settings.OptionsDragAndDropFolderOption = ActionType.Unpack)
		Me.DragAndDropFolderForDecompileRadioButton.Checked = (TheApp.Settings.OptionsDragAndDropFolderOption = ActionType.Decompile)
		Me.DragAndDropFolderForCompileRadioButton.Checked = (TheApp.Settings.OptionsDragAndDropFolderOption = ActionType.Compile)
		Me.DragAndDropFolderForPackRadioButton.Checked = (TheApp.Settings.OptionsDragAndDropFolderOption = ActionType.Pack)
	End Sub

	Private Sub Free()
		RemoveHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged

		Me.SingleInstanceCheckBox.DataBindings.Clear()

		' Auto-Open

		Me.AutoOpenVpkFileCheckBox.DataBindings.Clear()
		Me.AutoOpenGmaFileCheckBox.DataBindings.Clear()
		Me.AutoOpenFpxFileCheckBox.DataBindings.Clear()
		Me.AutoOpenMdlFileCheckBox.DataBindings.Clear()
		Me.AutoOpenMdlFileForPreviewCheckBox.DataBindings.Clear()
		Me.AutoOpenMdlFileForDecompileCheckBox.DataBindings.Clear()
		Me.AutoOpenMdlFileForViewCheckBox.DataBindings.Clear()
		Me.AutoOpenQcFileCheckBox.DataBindings.Clear()

		' Drag and Drop

		Me.DragAndDropMdlFileForPreviewCheckBox.DataBindings.Clear()
		Me.DragAndDropMdlFileForDecompileCheckBox.DataBindings.Clear()
		Me.DragAndDropMdlFileForViewCheckBox.DataBindings.Clear()

		' Context Menu

		Me.IntegrateContextMenuItemsCheckBox.DataBindings.Clear()
		Me.IntegrateAsSubmenuCheckBox.DataBindings.Clear()

		Me.OptionsContextMenuOpenWithCrowbarCheckBox.DataBindings.Clear()
		Me.OptionsContextMenuViewMdlFileCheckBox.DataBindings.Clear()

		Me.OptionsContextMenuDecompileMdlFileCheckBox.DataBindings.Clear()
		Me.OptionsContextMenuDecompileFolderCheckBox.DataBindings.Clear()
		Me.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.DataBindings.Clear()

		Me.OptionsContextMenuCompileQcFileCheckBox.DataBindings.Clear()
		Me.OptionsContextMenuCompileFolderCheckBox.DataBindings.Clear()
		Me.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.DataBindings.Clear()
	End Sub

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

	Private Sub OptionsUserControl_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
		Me.Free()
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub AutoOpenVpkFileRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles AutoOpenVpkFileForUnpackRadioButton.CheckedChanged, AutoOpenVpkFileForPublishRadioButton.CheckedChanged
		If Me.AutoOpenVpkFileForUnpackRadioButton.Checked Then
			TheApp.Settings.OptionsAutoOpenVpkFileOption = ActionType.Unpack
		Else
			TheApp.Settings.OptionsAutoOpenVpkFileOption = ActionType.Publish
		End If
	End Sub

	Private Sub AutoOpenGmaFileRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles AutoOpenGmaFileForUnpackRadioButton.CheckedChanged, AutoOpenGmaFileForPublishRadioButton.CheckedChanged
		If Me.AutoOpenGmaFileForUnpackRadioButton.Checked Then
			TheApp.Settings.OptionsAutoOpenGmaFileOption = ActionType.Unpack
		Else
			TheApp.Settings.OptionsAutoOpenGmaFileOption = ActionType.Publish
		End If
	End Sub

	Private Sub AutoOpenMdlFileRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles AutoOpenMdlFileForPreviewingRadioButton.CheckedChanged, AutoOpenMdlFileForDecompilingRadioButton.CheckedChanged, AutoOpenMdlFileForViewingRadioButton.CheckedChanged
		If Me.AutoOpenMdlFileForPreviewingRadioButton.Checked Then
			TheApp.Settings.OptionsAutoOpenMdlFileOption = ActionType.Preview
		ElseIf Me.AutoOpenMdlFileForDecompilingRadioButton.Checked Then
			TheApp.Settings.OptionsAutoOpenMdlFileOption = ActionType.Decompile
		Else
			TheApp.Settings.OptionsAutoOpenMdlFileOption = ActionType.View
		End If
	End Sub

	Private Sub AutoOpenUseDefaultsButton_Click(sender As Object, e As EventArgs) Handles AutoOpenUseDefaultsButton.Click
		TheApp.Settings.SetDefaultOptionsAutoOpenOptions()
		Me.InitAutoOpenRadioButtons()
	End Sub

	Private Sub AutoOpenFolderRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles AutoOpenFolderForUnpackRadioButton.CheckedChanged, AutoOpenFolderForDecompileRadioButton.CheckedChanged, AutoOpenFolderForCompileRadioButton.CheckedChanged, AutoOpenFolderForPackRadioButton.CheckedChanged
		If Me.AutoOpenFolderForUnpackRadioButton.Checked Then
			TheApp.Settings.OptionsAutoOpenFolderOption = ActionType.Unpack
		ElseIf Me.AutoOpenFolderForDecompileRadioButton.Checked Then
			TheApp.Settings.OptionsAutoOpenFolderOption = ActionType.Decompile
		ElseIf Me.AutoOpenFolderForCompileRadioButton.Checked Then
			TheApp.Settings.OptionsAutoOpenFolderOption = ActionType.Compile
		Else
			TheApp.Settings.OptionsAutoOpenFolderOption = ActionType.Pack
		End If
	End Sub

	Private Sub DragAndDropVpkFileRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles DragAndDropVpkFileForUnpackRadioButton.CheckedChanged, DragAndDropVpkFileForPublishRadioButton.CheckedChanged
		If Me.DragAndDropVpkFileForUnpackRadioButton.Checked Then
			TheApp.Settings.OptionsDragAndDropVpkFileOption = ActionType.Unpack
		Else
			TheApp.Settings.OptionsDragAndDropVpkFileOption = ActionType.Publish
		End If
	End Sub

	Private Sub DragAndDropGmaFileRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles DragAndDropGmaFileForUnpackRadioButton.CheckedChanged, DragAndDropGmaFileForPublishRadioButton.CheckedChanged
		If Me.DragAndDropGmaFileForUnpackRadioButton.Checked Then
			TheApp.Settings.OptionsDragAndDropGmaFileOption = ActionType.Unpack
		Else
			TheApp.Settings.OptionsDragAndDropGmaFileOption = ActionType.Publish
		End If
	End Sub

	Private Sub DragAndDropMdlFileRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles DragAndDropMdlFileForPreviewingRadioButton.CheckedChanged, DragAndDropMdlFileForDecompilingRadioButton.CheckedChanged, DragAndDropMdlFileForViewingRadioButton.CheckedChanged
		If Me.DragAndDropMdlFileForPreviewingRadioButton.Checked Then
			TheApp.Settings.OptionsDragAndDropMdlFileOption = ActionType.Preview
		ElseIf Me.DragAndDropMdlFileForDecompilingRadioButton.Checked Then
			TheApp.Settings.OptionsDragAndDropMdlFileOption = ActionType.Decompile
		Else
			TheApp.Settings.OptionsDragAndDropMdlFileOption = ActionType.View
		End If
	End Sub

	Private Sub DragAndDropFolderRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles DragAndDropFolderForUnpackRadioButton.CheckedChanged, DragAndDropFolderForDecompileRadioButton.CheckedChanged, DragAndDropFolderForCompileRadioButton.CheckedChanged, DragAndDropFolderForPackRadioButton.CheckedChanged
		If Me.DragAndDropFolderForUnpackRadioButton.Checked Then
			TheApp.Settings.OptionsDragAndDropFolderOption = ActionType.Unpack
		ElseIf Me.DragAndDropFolderForDecompileRadioButton.Checked Then
			TheApp.Settings.OptionsDragAndDropFolderOption = ActionType.Decompile
		ElseIf Me.DragAndDropFolderForCompileRadioButton.Checked Then
			TheApp.Settings.OptionsDragAndDropFolderOption = ActionType.Compile
		Else
			TheApp.Settings.OptionsDragAndDropFolderOption = ActionType.Pack
		End If
	End Sub

	Private Sub DragAndDropUseDefaultsButton_Click(sender As Object, e As EventArgs) Handles DragAndDropUseDefaultsButton.Click
		TheApp.Settings.SetDefaultOptionsDragAndDropOptions()
		Me.InitDragAndDropRadioButtons()
	End Sub

	Private Sub ContextMenuUseDefaultsButton_Click(sender As Object, e As EventArgs) Handles ContextMenuUseDefaultsButton.Click
		TheApp.Settings.SetDefaultOptionsContextMenuOptions()
	End Sub

	Private Sub ApplyButton_Click(sender As Object, e As EventArgs) Handles ApplyButton.Click
		Me.ApplyAllAutoOpenOptions()
	End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub AppSettings_PropertyChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		If e.PropertyName = "AppIsSingleInstance" Then
			TheApp.SaveAppSettings()
		ElseIf e.PropertyName = "OptionsAutoOpenVpkFileIsChecked" Then
			Me.ApplyAutoOpenVpkFileOptions()
		ElseIf e.PropertyName = "OptionsAutoOpenGmaFileIsChecked" Then
			Me.ApplyAutoOpenGmaFileOptions()
		ElseIf e.PropertyName = "OptionsAutoOpenFpxFileIsChecked" Then
			Me.ApplyAutoOpenFpxFileOptions()
		ElseIf e.PropertyName = "OptionsAutoOpenMdlFileIsChecked" Then
			Me.ApplyAutoOpenMdlFileOptions()
		ElseIf e.PropertyName = "OptionsAutoOpenQcFileIsChecked" Then
			Me.ApplyAutoOpenQcFileOptions()
		End If
	End Sub

#End Region

#Region "Private Methods"

	Private Sub ApplyAutoOpenVpkFileOptions()
		If TheApp.Settings.OptionsAutoOpenVpkFileIsChecked Then
			Win32Api.CreateFileAssociation("vpk", "vpkFile", "VPK File", Application.ExecutablePath)
		Else
			Win32Api.DeleteFileAssociation("vpk", "vpkFile", "VPK File", Application.ExecutablePath)
		End If
	End Sub

	Private Sub ApplyAutoOpenGmaFileOptions()
		If TheApp.Settings.OptionsAutoOpenGmaFileIsChecked Then
			Win32Api.CreateFileAssociation("gma", "gmaFile", "GMA File", Application.ExecutablePath)
		Else
			Win32Api.DeleteFileAssociation("gma", "gmaFile", "GMA File", Application.ExecutablePath)
		End If
	End Sub

	Private Sub ApplyAutoOpenFpxFileOptions()
		If TheApp.Settings.OptionsAutoOpenFpxFileIsChecked Then
			Win32Api.CreateFileAssociation("fpx", "fpxFile", "FPX File", Application.ExecutablePath)
		Else
			Win32Api.DeleteFileAssociation("fpx", "fpxFile", "FPX File", Application.ExecutablePath)
		End If
	End Sub

	Private Sub ApplyAutoOpenMdlFileOptions()
		If TheApp.Settings.OptionsAutoOpenMdlFileIsChecked Then
			Win32Api.CreateFileAssociation("mdl", "mdlFile", "MDL File", Application.ExecutablePath)
		Else
			Win32Api.DeleteFileAssociation("mdl", "mdlFile", "MDL File", Application.ExecutablePath)
		End If
	End Sub

	Private Sub ApplyAutoOpenQcFileOptions()
		If TheApp.Settings.OptionsAutoOpenQcFileIsChecked Then
			Win32Api.CreateFileAssociation("qc", "qcFile", "QC File", Application.ExecutablePath)
		Else
			Win32Api.DeleteFileAssociation("qc", "qcFile", "QC File", Application.ExecutablePath)
		End If
	End Sub

	Private Sub ApplyAllAutoOpenOptions()
		Me.ApplyAutoOpenVpkFileOptions()
		Me.ApplyAutoOpenGmaFileOptions()
		Me.ApplyAutoOpenFpxFileOptions()
		Me.ApplyAutoOpenMdlFileOptions()
		Me.ApplyAutoOpenQcFileOptions()

		Me.UpdateApplyPanel()
	End Sub

	Private Sub UpdateApplyPanel()
		Dim vpkFileAssociationIsAlreadyAssigned As Boolean
		vpkFileAssociationIsAlreadyAssigned = Win32Api.FileAssociationIsAlreadyAssigned("vpk", "vpkFile", "VPK File", Application.ExecutablePath)

		Dim gmaFileAssociationIsAlreadyAssigned As Boolean
		gmaFileAssociationIsAlreadyAssigned = Win32Api.FileAssociationIsAlreadyAssigned("gma", "gmaFile", "GMA File", Application.ExecutablePath)

		Dim fpxFileAssociationIsAlreadyAssigned As Boolean
		fpxFileAssociationIsAlreadyAssigned = Win32Api.FileAssociationIsAlreadyAssigned("fpx", "fpxFile", "FPX File", Application.ExecutablePath)

		Dim mdlFileAssociationIsAlreadyAssigned As Boolean
		mdlFileAssociationIsAlreadyAssigned = Win32Api.FileAssociationIsAlreadyAssigned("mdl", "mdlFile", "MDL File", Application.ExecutablePath)

		Dim qcFileAssociationIsAlreadyAssigned As Boolean
		qcFileAssociationIsAlreadyAssigned = Win32Api.FileAssociationIsAlreadyAssigned("qc", "qcFile", "QC File", Application.ExecutablePath)

		'Me.ApplyPanel.Visible = (Not vpkFileAssociationIsAlreadyAssigned) OrElse (Not mdlFileAssociationIsAlreadyAssigned) OrElse (Not qcFileAssociationIsAlreadyAssigned)
		Dim applyPanelShouldBeVisible As Boolean
		applyPanelShouldBeVisible = False
		If vpkFileAssociationIsAlreadyAssigned <> TheApp.Settings.OptionsAutoOpenVpkFileIsChecked Then
			applyPanelShouldBeVisible = True
		ElseIf gmaFileAssociationIsAlreadyAssigned <> TheApp.Settings.OptionsAutoOpenGmaFileIsChecked Then
			applyPanelShouldBeVisible = True
		ElseIf fpxFileAssociationIsAlreadyAssigned <> TheApp.Settings.OptionsAutoOpenFpxFileIsChecked Then
			applyPanelShouldBeVisible = True
		ElseIf mdlFileAssociationIsAlreadyAssigned <> TheApp.Settings.OptionsAutoOpenMdlFileIsChecked Then
			applyPanelShouldBeVisible = True
		ElseIf qcFileAssociationIsAlreadyAssigned <> TheApp.Settings.OptionsAutoOpenQcFileIsChecked Then
			applyPanelShouldBeVisible = True
		End If
		Me.ApplyPanel.Visible = applyPanelShouldBeVisible
	End Sub

#End Region

#Region "Data"

#End Region

End Class
