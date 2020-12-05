Imports System.ComponentModel
Imports System.IO
Imports System.IO.Pipes
Imports System.Linq
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks

Public Class PublishUserControl

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()
		' This call is required by the designer.
		InitializeComponent()

		' Set the ToolStrip and its child controls to use same default FontSize as the other controls. 
		'    Inexplicably, the default FontSize for them is 9 instead of 8.25 like all other controls.
		Me.ToolStrip1.Font = Me.Font
		For Each widget As Control In Me.ToolStrip1.Controls
			widget.Font = Me.Font
		Next

		Me.UseInDownloadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.UseInDownloadToolStripMenuItem.Name = "ItemsDataGridViewUseInDownloadToolStripMenuItem"
		Me.UseInDownloadToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		Me.UseInDownloadToolStripMenuItem.Text = "Use in Download"

		Me.ItemContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.ItemContextMenuStrip.Items.Add(Me.UseInDownloadToolStripMenuItem)
		Me.ItemContextMenuStrip.Name = "ItemsDataGridViewContextMenuStrip"
		Me.ItemContextMenuStrip.Size = New System.Drawing.Size(177, 114)
		Me.ContextMenuStrip = Me.ItemContextMenuStrip

		'Me.ItemsDataGridViewUseInDownloadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		'Me.ItemsDataGridViewUseInDownloadToolStripMenuItem.Name = "ItemsDataGridViewUseInDownloadToolStripMenuItem"
		'Me.ItemsDataGridViewUseInDownloadToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		'Me.ItemsDataGridViewUseInDownloadToolStripMenuItem.Text = "Use in Download"

		'Me.ItemsDataGridViewContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
		'Me.ItemsDataGridViewContextMenuStrip.Items.Add(Me.ItemsDataGridViewUseInDownloadToolStripMenuItem)
		'Me.ItemsDataGridViewContextMenuStrip.Name = "ItemsDataGridViewContextMenuStrip"
		'Me.ItemsDataGridViewContextMenuStrip.Size = New System.Drawing.Size(177, 114)
		'Me.ItemsDataGridView.ContextMenuStrip = Me.ItemsDataGridViewContextMenuStrip

		'Me.ItemIdLabelUseInDownloadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		'Me.ItemIdLabelUseInDownloadToolStripMenuItem.Name = "ItemIdLabelUseInDownloadToolStripMenuItem"
		'Me.ItemIdLabelUseInDownloadToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		'Me.ItemIdLabelUseInDownloadToolStripMenuItem.Text = "Use in Download"

		'Me.ItemIdLabelContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
		'Me.ItemIdLabelContextMenuStrip.Items.Add(Me.ItemIdLabelUseInDownloadToolStripMenuItem)
		'Me.ItemIdLabelContextMenuStrip.Name = "ItemIdLabelContextMenuStrip"
		'Me.ItemIdLabelContextMenuStrip.Size = New System.Drawing.Size(177, 114)
		'Me.ItemIDLabel.ContextMenuStrip = Me.ItemIdLabelContextMenuStrip

		'Me.ItemIdTextBoxUseInDownloadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		'Me.ItemIdTextBoxUseInDownloadToolStripMenuItem.Name = "ItemIdTextBoxUseInDownloadToolStripMenuItem"
		'Me.ItemIdTextBoxUseInDownloadToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		'Me.ItemIdTextBoxUseInDownloadToolStripMenuItem.Text = "Use in Download"

		'Me.ItemIdTextBoxContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
		'Me.ItemIdTextBoxContextMenuStrip.Items.Add(Me.ItemIdTextBoxUseInDownloadToolStripMenuItem)
		'Me.ItemIdTextBoxContextMenuStrip.Name = "ItemIdTextBoxContextMenuStrip"
		'Me.ItemIdTextBoxContextMenuStrip.Size = New System.Drawing.Size(177, 114)
		'Me.ItemIDTextBox.ContextMenuStrip = Me.ItemIdTextBoxContextMenuStrip
	End Sub

	'UserControl overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing Then
				Me.Free()
				If components IsNot Nothing Then
					components.Dispose()
				End If
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

#End Region

#Region "Init and Free"

	Private Sub Init()
		TheApp.InitAppInfo()

		If TheApp.Settings.PublishGameSelectedIndex >= TheApp.SteamAppInfos.Count Then
			TheApp.Settings.PublishGameSelectedIndex = 0
		End If
		Me.AppIdComboBox.DisplayMember = "Name"
		Me.AppIdComboBox.ValueMember = "ID"
		Me.AppIdComboBox.DataSource = TheApp.SteamAppInfos
		Me.AppIdComboBox.DataBindings.Add("SelectedIndex", TheApp.Settings, "PublishGameSelectedIndex", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.theBackgroundSteamPipe = New BackgroundSteamPipe()

		Me.GetUserSteamID()

		Me.theSelectedItemIsChangingViaMe = True
		Me.theItemBindingSource = New BindingSource()
		Me.InitItemListWidgets()
		Me.theItemBindingSource.DataSource = Me.theDisplayedItems
		Me.InitItemDetailWidgets()
		Me.theSelectedItemIsChangingViaMe = False

		AddHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged

		Me.theSelectedGameIsStillUpdatingInterface = False
		Me.UpdateSteamAppWidgets()
	End Sub

	'NOTE: This is called after all child widgets (created via designer) are disposed but before this UserControl is disposed.
	Private Sub Free()
		If Me.theBackgroundSteamPipe IsNot Nothing Then
			Me.theBackgroundSteamPipe.Kill()
		End If

		If Me.theTagsWidget IsNot Nothing Then
			RemoveHandler Me.theTagsWidget.TagsPropertyChanged, AddressOf Me.TagsWidget_TagsPropertyChanged
		End If

		If Me.theSelectedItem IsNot Nothing Then
			If Me.theSelectedItem.IsTemplate AndAlso Me.theSelectedItem.IsChanged Then
				Me.SaveChangedTemplateToDraft()
			End If
			RemoveHandler Me.theSelectedItem.PropertyChanged, AddressOf Me.WorkshopItem_PropertyChanged
		End If

		RemoveHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged
	End Sub

	Private Sub GetUserSteamID()
		Dim steamPipe As New SteamPipe()
		Dim result As String = steamPipe.Open("GetUserSteamID", Nothing, "")
		If result <> "success" Then
			Me.theUserSteamID = 0
			Exit Sub
		End If
		Me.theUserSteamID = steamPipe.GetUserSteamID()
		steamPipe.Shut()
	End Sub

	'NOTE: Gets the quota for the logged-in Steam user for the selected SteamApp. 
	Private Sub GetUserSteamAppCloudQuota()
		If Me.theSteamAppInfo.UsesSteamUGC Then
			Me.QuotaProgressBar.Text = ""
			Me.QuotaProgressBar.Value = 0
			Me.ToolTip1.SetToolTip(Me.QuotaProgressBar, "")
		Else
			Dim steamPipe As New SteamPipe()
			Dim result As String = steamPipe.Open("GetQuota", Nothing, "")
			If result <> "success" Then
				Me.theUserSteamID = 0
				Exit Sub
			End If
			Dim availableBytes As ULong
			Dim totalBytes As ULong
			steamPipe.GetQuota(availableBytes, totalBytes)
			steamPipe.Shut()

			If totalBytes = 0 Then
				Me.QuotaProgressBar.Text = "unknown"
				Me.QuotaProgressBar.Value = 0
				Me.ToolTip1.SetToolTip(Me.QuotaProgressBar, "Quota (unknown)")
			Else
				Dim usedBytes As ULong = totalBytes - availableBytes
				Dim progressPercentage As Integer = CInt(usedBytes * Me.QuotaProgressBar.Maximum / totalBytes)
				Dim availableBytesText As String = MathModule.ByteUnitsConversion(availableBytes)
				Dim usedBytesText As String = MathModule.ByteUnitsConversion(usedBytes)
				Dim totalBytesText As String = MathModule.ByteUnitsConversion(totalBytes)
				Me.QuotaProgressBar.Text = availableBytesText + " available "
				Me.QuotaProgressBar.Value = progressPercentage
				Me.ToolTip1.SetToolTip(Me.QuotaProgressBar, "Quota: " + usedBytesText + " used of " + totalBytesText + " total (" + progressPercentage.ToString() + "% used)")
			End If
		End If
	End Sub

	Private Sub InitItemListWidgets()
		Me.theDisplayedItems = New WorkshopItemBindingList()
		Me.theEntireListOfItems = New WorkshopItemBindingList()

		Me.ItemsDataGridView.AutoGenerateColumns = False
		Me.ItemsDataGridView.DataSource = Me.theItemBindingSource

		Dim textColumn As DataGridViewTextBoxColumn

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		textColumn.DataPropertyName = "IsChanged"
		textColumn.DefaultCellStyle.BackColor = SystemColors.Control
		textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
		'textColumn.Frozen = True
		textColumn.HeaderText = "*"
		textColumn.MinimumWidth = 20
		textColumn.Name = "IsChanged"
		textColumn.ReadOnly = True
		textColumn.SortMode = DataGridViewColumnSortMode.Automatic
		textColumn.Width = 20
		Me.ItemsDataGridView.Columns.Add(textColumn)

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		textColumn.DataPropertyName = "ID"
		textColumn.DefaultCellStyle.BackColor = SystemColors.Control
		textColumn.HeaderText = "Item ID"
		textColumn.Name = "ID"
		textColumn.ReadOnly = True
		textColumn.SortMode = DataGridViewColumnSortMode.Automatic
		textColumn.Width = 100
		Me.ItemsDataGridView.Columns.Add(textColumn)

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		textColumn.DataPropertyName = "Title"
		textColumn.DefaultCellStyle.BackColor = SystemColors.Control
		textColumn.HeaderText = "Title"
		textColumn.Name = "Title"
		textColumn.ReadOnly = True
		textColumn.SortMode = DataGridViewColumnSortMode.Automatic
		textColumn.Width = 200
		Me.ItemsDataGridView.Columns.Add(textColumn)

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		textColumn.DataPropertyName = "Posted"
		textColumn.DefaultCellStyle.BackColor = SystemColors.Control
		textColumn.HeaderText = "Posted"
		textColumn.Name = "Posted"
		textColumn.ReadOnly = True
		textColumn.SortMode = DataGridViewColumnSortMode.Automatic
		textColumn.Width = 110
		Me.ItemsDataGridView.Columns.Add(textColumn)

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		textColumn.DataPropertyName = "Updated"
		textColumn.DefaultCellStyle.BackColor = SystemColors.Control
		textColumn.HeaderText = "Updated"
		textColumn.Name = "Updated"
		textColumn.ReadOnly = True
		textColumn.SortMode = DataGridViewColumnSortMode.Automatic
		textColumn.Width = 110
		Me.ItemsDataGridView.Columns.Add(textColumn)

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		textColumn.DataPropertyName = "Visibility"
		textColumn.DefaultCellStyle.BackColor = SystemColors.Control
		textColumn.HeaderText = "Visibility"
		textColumn.Name = "Visibility"
		textColumn.ReadOnly = True
		textColumn.SortMode = DataGridViewColumnSortMode.Automatic
		textColumn.Width = 75
		Me.ItemsDataGridView.Columns.Add(textColumn)

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		textColumn.DataPropertyName = "OwnerName"
		textColumn.DefaultCellStyle.BackColor = SystemColors.Control
		textColumn.HeaderText = "Owner"
		textColumn.Name = "Owner"
		textColumn.ReadOnly = True
		textColumn.SortMode = DataGridViewColumnSortMode.Automatic
		textColumn.Width = 120
		Me.ItemsDataGridView.Columns.Add(textColumn)

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.DataPropertyName = ""
		textColumn.DefaultCellStyle.BackColor = SystemColors.Control
		textColumn.FillWeight = 100
		textColumn.HeaderText = ""
		textColumn.Name = ""
		textColumn.ReadOnly = True
		textColumn.SortMode = DataGridViewColumnSortMode.NotSortable
		textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
		Me.ItemsDataGridView.Columns.Add(textColumn)

		Me.SearchItemsToolStripComboBox.ComboBox.DisplayMember = "Value"
		Me.SearchItemsToolStripComboBox.ComboBox.ValueMember = "Key"
		Me.SearchItemsToolStripComboBox.ComboBox.DataSource = EnumHelper.ToList(GetType(PublishSearchFieldOptions))
		Me.SearchItemsToolStripComboBox.ComboBox.DataBindings.Add("SelectedValue", TheApp.Settings, "PublishSearchField", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.SearchItemsToolStripTextBox.TextBox.DataBindings.Add("Text", TheApp.Settings, "PublishSearchText", False, DataSourceUpdateMode.OnValidation)
	End Sub

	Private Sub InitItemDetailWidgets()
		Me.ItemTitleTextBox.MaxLength = CInt(Steamworks.Constants.k_cchPublishedDocumentTitleMax)
		Me.ItemDescriptionTextBox.MaxLength = CInt(Steamworks.Constants.k_cchPublishedDocumentDescriptionMax)
		Me.ItemChangeNoteTextBox.MaxLength = CInt(Steamworks.Constants.k_cchPublishedDocumentChangeDescriptionMax)

		Me.ItemIDTextBox.DataBindings.Add("Text", Me.theItemBindingSource, "ID", False, DataSourceUpdateMode.OnValidation)
		'TODO: Change ID textbox to combobox dropdownlist that lists most-recently accessed IDs, including those selected via list.
		'Dim anEnumList As IList
		'anEnumList = EnumHelper.ToList(GetType(SteamUGCPublishedFileVisibility))
		'Me.ItemIDComboBox.DisplayMember = "Value"
		'Me.ItemIDComboBox.ValueMember = "Key"
		'Me.ItemIDComboBox.DataSource = anEnumList
		'Me.ItemIDComboBox.DataBindings.Add("SelectedValue", Me.theItemBindingSource, "ID", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.ItemOwnerTextBox.DataBindings.Add("Text", Me.theItemBindingSource, "OwnerName", False, DataSourceUpdateMode.OnValidation)
		Me.ItemPostedTextBox.DataBindings.Add("Text", Me.theItemBindingSource, "Posted", False, DataSourceUpdateMode.OnValidation)
		Me.ItemUpdatedTextBox.DataBindings.Add("Text", Me.theItemBindingSource, "Updated", False, DataSourceUpdateMode.OnValidation)
		Me.ItemTitleTextBox.DataBindings.Add("Text", Me.theItemBindingSource, "Title", False, DataSourceUpdateMode.OnPropertyChanged)
		'NOTE: For RichTextBox, set the Formatting argument to True when DataSourceUpdateMode.OnPropertyChanged is used, to prevent characters being entered in reverse order.
		Me.ItemDescriptionTextBox.DataBindings.Add("Text", Me.theItemBindingSource, "Description", True, DataSourceUpdateMode.OnPropertyChanged)
		Me.ItemChangeNoteTextBox.DataBindings.Add("Text", Me.theItemBindingSource, "ChangeNote", True, DataSourceUpdateMode.OnPropertyChanged)
		Me.ItemContentPathFileNameTextBox.DataBindings.Add("Text", Me.theItemBindingSource, "ContentPathFolderOrFileName", False, DataSourceUpdateMode.OnValidation)
		Me.ItemPreviewImagePathFileNameTextBox.DataBindings.Add("Text", Me.theItemBindingSource, "PreviewImagePathFileName", False, DataSourceUpdateMode.OnValidation)

		Me.ItemVisibilityComboBox.DisplayMember = "Value"
		Me.ItemVisibilityComboBox.ValueMember = "Key"
		Me.ItemVisibilityComboBox.DataSource = EnumHelper.ToList(GetType(WorkshopItem.SteamUGCPublishedItemVisibility))
		Me.ItemVisibilityComboBox.DataBindings.Add("SelectedValue", Me.theItemBindingSource, "Visibility", False, DataSourceUpdateMode.OnPropertyChanged)
	End Sub

#End Region

#Region "Properties"

#End Region

#Region "Methods"

#End Region

#Region "Widget Event Handlers"

	Private Sub PublishUserControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		'NOTE: This code prevents Visual Studio or Windows often inexplicably extending the right side of these widgets.
		Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(Me.AppIdComboBox, Me.RefreshGameItemsButton)

		If Not Me.DesignMode Then
			Me.Init()
		End If
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub RefreshGameItemsButton_Click(sender As Object, e As EventArgs) Handles RefreshGameItemsButton.Click
		Me.UpdateSteamAppWidgets()
	End Sub

	Private Sub OpenSteamSubscriberAgreementButton_Click(sender As Object, e As EventArgs) Handles OpenSteamSubscriberAgreementButton.Click
		Me.OpenSteamSubscriberAgreement()
	End Sub

	Private Sub ItemsDataGridView_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles ItemsDataGridView.CellFormatting
		If e.FormattingApplied Then
			Exit Sub
		End If

		Dim dgv As DataGridView = CType(sender, DataGridView)
		Dim cell As DataGridViewCell = dgv(e.ColumnIndex, e.RowIndex)

		If cell.Value IsNot Nothing Then
			If dgv.Columns(e.ColumnIndex).Name = "IsChanged" Then
				Dim itemIsChanged As Boolean = CType(cell.Value, Boolean)
				If itemIsChanged Then
					e.Value = "*"
				Else
					e.Value = ""
				End If
				e.FormattingApplied = True
			ElseIf (dgv.Columns(e.ColumnIndex).Name = "Posted") OrElse (dgv.Columns(e.ColumnIndex).Name = "Updated") Then
				Dim aDateTime As DateTime = MathModule.UnixTimeStampToDateTime(Long.Parse(CType(cell.Value, String)))
				e.Value = aDateTime.ToShortDateString() + " " + aDateTime.ToShortTimeString()
				e.FormattingApplied = True
			ElseIf dgv.Columns(e.ColumnIndex).Name = "Visibility" Then
				Dim vis As WorkshopItem.SteamUGCPublishedItemVisibility = CType([Enum].Parse(GetType(WorkshopItem.SteamUGCPublishedItemVisibility), CType(cell.Value, String)), WorkshopItem.SteamUGCPublishedItemVisibility)
				e.Value = EnumHelper.GetDescription(vis)
				e.FormattingApplied = True
			End If
		End If
	End Sub

	' Prevent the inexplicable showing of an error window, even though the previous Crowbar version did not ever show the window.
	Private Sub ItemsDataGridView_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles ItemsDataGridView.DataError
		e.Cancel = True
	End Sub

	Private Sub ItemsDataGridView_SelectionChanged(sender As Object, e As EventArgs) Handles ItemsDataGridView.SelectionChanged
		If Not Me.theSelectedItemIsChangingViaMe AndAlso Me.ItemsDataGridView.SelectedRows.Count > 0 Then
			'If Me.ItemsDataGridView.SelectedRows.Count > 0 Then
			'NOTE: Allow the highlight to show in the grid before updating item details.
			Application.DoEvents()
			If Me.theSelectedItem IsNot Nothing AndAlso Me.theSelectedItem.IsTemplate AndAlso Me.theSelectedItem.IsChanged Then
				Me.SaveChangedTemplateToDraft()
			End If
			Me.UpdateItemDetails()
		End If
	End Sub

	'NOTE: Without this handler, when Items grid is sorted, the selection stays at list index instead of with the item.
	Private Sub ItemsDataGridView_Sorted(sender As Object, e As EventArgs) Handles ItemsDataGridView.Sorted
		Me.theSelectedItemIsChangingViaMe = True
		Me.theItemBindingSource.Position = Me.theItemBindingSource.IndexOf(Me.theSelectedItem)
		Me.theSelectedItemIsChangingViaMe = False
	End Sub

	'Private Sub UseInDownloadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemsDataGridViewUseInDownloadToolStripMenuItem.Click, ItemIdLabelUseInDownloadToolStripMenuItem.Click, ItemIdTextBoxUseInDownloadToolStripMenuItem.Click
	'	Me.UseItemIdInDownload()
	'End Sub
	Private Sub UseInDownloadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseInDownloadToolStripMenuItem.Click
		Me.UseItemIdInDownload()
	End Sub

	'Private Sub SearchItemsToolStripComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SearchItemsToolStripComboBox.SelectedIndexChanged

	'End Sub

	Private Sub SearchItemsToolStripTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SearchItemsToolStripTextBox.KeyPress
		If Not SearchItemsToolStripTextBox.Multiline AndAlso e.KeyChar = ChrW(Keys.Return) Then
			Try
				'' Cause validation, which means Validating and Validated events are raised.
				'Me.FindForm().Validate()
				'If TypeOf Me.Parent Is ContainerControl Then
				'	CType(Me.Parent, ContainerControl).Validate()
				'End If
				''NOTE: Prevent annoying beep when textbox is single line.
				'e.Handled = True
				Me.SearchItems()
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub SearchItemsToolStripButton_Click(sender As Object, e As EventArgs) Handles SearchItemsToolStripButton.Click
		Me.SearchItems()
	End Sub

	Private Sub AddItemButton_Click(sender As Object, e As EventArgs) Handles AddItemToolStripButton.Click
		Me.AddDraftItem(Nothing)
		Me.SelectItemInGrid(Me.ItemsDataGridView.Rows.Count - 1)
	End Sub

	Private Sub OwnerLabel_DoubleClick(sender As Object, e As EventArgs) Handles ItemOwnerLabel.DoubleClick
		Me.SwapBetweenOwnerNameAndID()
	End Sub

	Private Sub ToggleWordWrapForDescriptionCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles ToggleWordWrapForDescriptionCheckBox.CheckedChanged
		Me.ToggleWordWrapImageOnCheckbox(CType(sender, CheckBox))
		Me.ItemDescriptionTextBox.WordWrap = Me.ToggleWordWrapForDescriptionCheckBox.Checked
	End Sub

	Private Sub ToggleWordWrapForChangeNoteCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles ToggleWordWrapForChangeNoteCheckBox.CheckedChanged
		Me.ToggleWordWrapImageOnCheckbox(CType(sender, CheckBox))
		Me.ItemChangeNoteTextBox.WordWrap = Me.ToggleWordWrapForChangeNoteCheckBox.Checked
	End Sub

	Private Sub BrowseContentPathFileNameButton_Click(sender As Object, e As EventArgs) Handles BrowseItemContentPathFileNameButton.Click
		Me.BrowseForContentPathFolderOrFileName()
	End Sub

	Private Sub BrowsePreviewImageButton_Click(sender As Object, e As EventArgs) Handles BrowseItemPreviewImagePathFileNameButton.Click
		Me.BrowseForPreviewImage()
	End Sub

	Private Sub SaveAsTemplateOrDraftItemButton_Click(sender As Object, e As EventArgs) Handles SaveAsTemplateOrDraftItemButton.Click
		If SaveAsTemplateOrDraftItemButton.Text = "Save as Template" Then
			Me.SaveItemAsTemplate()
		Else
			Me.AddDraftItem(Me.theSelectedItem)
			If Me.theSelectedItem.IsTemplate AndAlso Me.theSelectedItem.IsChanged Then
				Me.RevertChangedTemplate()
			End If
			Me.SelectItemInGrid(Me.ItemsDataGridView.Rows.Count - 1)
		End If
	End Sub

	Private Sub RefreshOrRevertButton_Click(sender As Object, e As EventArgs) Handles RefreshOrRevertItemButton.Click
		Me.RefreshOrRevertItem()
	End Sub

	Private Sub OpenWorkshopPageButton_Click(sender As Object, e As EventArgs) Handles OpenWorkshopPageButton.Click
		Me.OpenWorkshopPage()
	End Sub

	Private Sub SaveTemplateButton_Click(sender As Object, e As EventArgs) Handles SaveTemplateButton.Click
		Me.SaveTemplate()
	End Sub

	Private Sub DeleteItemButton_Click(sender As Object, e As EventArgs) Handles DeleteItemButton.Click
		Me.DeleteItem()
	End Sub

	'NOTE: There is no automatic data-binding with TagsWidget, so manually bind from widget to object here.
	Private Sub TagsWidget_TagsPropertyChanged(sender As Object, e As EventArgs)
		Me.theSelectedItem.Tags = Me.theTagsWidget.ItemTags
	End Sub

	Private Sub PublishItemButton_Click(sender As Object, e As EventArgs) Handles PublishItemButton.Click
		Me.PublishItem()
	End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub AppSettings_PropertyChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		If e.PropertyName = "PublishGameSelectedIndex" Then
			TheApp.Settings.PublishSearchField = PublishSearchFieldOptions.ID
			TheApp.Settings.PublishSearchText = ""

			Me.UpdateSteamAppWidgets()
		End If
	End Sub

	Private Sub WorkshopItem_PropertyChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		If Me.theSelectedItemDetailsIsChangingViaMe Then
			Exit Sub
		End If

		If e.PropertyName = "ID" Then
			Me.theWorkshopPageLink = AppConstants.WorkshopLinkStart + Me.theSelectedItem.ID
		ElseIf e.PropertyName = "Title" Then
			Me.UpdateItemTitleLabel()
			Me.UpdateItemChangedStatus()
		ElseIf e.PropertyName = "Description" Then
			Me.UpdateItemDescriptionLabel()
			Me.UpdateItemChangedStatus()
		ElseIf e.PropertyName = "ChangeNote" Then
			Me.UpdateItemChangeNoteLabel()
			Me.UpdateItemChangedStatus()
		ElseIf e.PropertyName = "ContentSize" Then
			Me.UpdateItemContentLabel()
		ElseIf e.PropertyName = "ContentPathFolderOrFileName" Then
			Me.UpdateItemContentLabel()
			Me.UpdateItemChangedStatus()
		ElseIf e.PropertyName = "PreviewImageSize" Then
			Me.UpdateItemPreviewImageLabel()
			Me.UpdateItemPreviewImageBox()
		ElseIf e.PropertyName = "PreviewImagePathFileName" Then
			Me.UpdateItemPreviewImageLabel()
			Me.UpdateItemPreviewImageBox()
			Me.UpdateItemChangedStatus()
			'NOTE: Using this property raises an exception, possibly because the DataGridView gets confused by the property being a list, so use "TagsAsTextLine" property.
			'ElseIf e.PropertyName = "Tags" Then
		ElseIf e.PropertyName = "TagsAsTextLine" Then
			Me.UpdateItemTagsLabel()
			Me.UpdateItemChangedStatus()
		ElseIf e.PropertyName = "Visibility" Then
			Me.UpdateItemVisibilityLabel()
			Me.UpdateItemChangedStatus()
		End If

		If Me.theSelectedItem.IsDraft Then
			Me.theSelectedItem.Updated = MathModule.DateTimeToUnixTimeStamp(DateTime.Now())
		End If
	End Sub

	Private Sub GetPublishedItems_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		If e.ProgressPercentage = 0 Then
			Me.LogTextBox.AppendText(CStr(e.UserState))
		ElseIf e.ProgressPercentage = 1 Then
			Me.theExpectedPublishedItemCount = CUInt(e.UserState)
		ElseIf e.ProgressPercentage = 2 Then
			Dim publishedItem As WorkshopItem
			publishedItem = CType(e.UserState, WorkshopItem)

			Dim itemHasBeenFound As Boolean = False
			For Each item As WorkshopItem In Me.theDisplayedItems
				If item.ID = publishedItem.ID Then
					itemHasBeenFound = True
					Exit For
				End If
			Next
			If Not itemHasBeenFound Then
				Me.theDisplayedItems.Add(publishedItem)
			End If

			itemHasBeenFound = False
			For Each item As WorkshopItem In Me.theEntireListOfItems
				If item.ID = publishedItem.ID Then
					itemHasBeenFound = True
					Exit For
				End If
			Next
			If Not itemHasBeenFound Then
				Me.theEntireListOfItems.Add(publishedItem)
			End If
			'ElseIf e.ProgressPercentage = 3 Then
			'	Dim availableBytes As ULong = CULng(e.UserState)
			'	Me.SteamCloudSizeLabel.Text = "(" + availableBytes.ToString("N") + " used /"
			'ElseIf e.ProgressPercentage = 4 Then
			'	Dim totalBytes As ULong = CULng(e.UserState)
			'	Me.SteamCloudSizeLabel.Text += totalBytes.ToString("N") + " total)"
		End If

		Me.UpdateItemListWidgets(True)
	End Sub

	Private Sub GetPublishedItems_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		If e.Cancelled Then
			Dim debug As Integer = 4242
		Else
			Me.UpdateItemListWidgets(False)
		End If
		Me.theSelectedGameIsStillUpdatingInterface = False
	End Sub

	Private Sub GetPublishedItemDetails_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		If e.ProgressPercentage = 0 Then
			Me.LogTextBox.AppendText(CStr(e.UserState))
		ElseIf e.ProgressPercentage = 1 Then
			If Me.ItemPreviewImagePictureBox.Image IsNot Nothing Then
				Me.ItemPreviewImagePictureBox.Image.Dispose()
			End If
			Me.ItemPreviewImagePictureBox.Image = CType(e.UserState, Image)
			'Application.DoEvents()
			Me.ItemPreviewImagePictureBox.Refresh()
		End If
	End Sub

	Private Sub GetPublishedItemDetails_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		If e.Cancelled Then
			Dim debug As Integer = 4242
		Else
			Dim output As BackgroundSteamPipe.GetPublishedFileDetailsOutputInfo = CType(e.Result, BackgroundSteamPipe.GetPublishedFileDetailsOutputInfo)
			Dim publishedItem As WorkshopItem = output.PublishedItem
			If output.Action = "Updated" Then
				If publishedItem.ID <> "0" AndAlso publishedItem.ID = Me.theSelectedItem.ID Then
					Me.theSelectedItem.Updated = publishedItem.Updated
				End If
			Else
				If publishedItem.ID <> "0" Then
					If publishedItem.ID = Me.theSelectedItem.ID Then
						Dim previewImagePathFileName As String = Me.theSelectedItem.PreviewImagePathFileName

						Me.theSelectedItem.CreatorAppID = publishedItem.CreatorAppID
						Me.theSelectedItem.ID = publishedItem.ID
						Me.theSelectedItem.OwnerID = publishedItem.OwnerID
						Me.theSelectedItem.OwnerName = publishedItem.OwnerName
						Me.theSelectedItem.Posted = publishedItem.Posted
						Me.theSelectedItem.Updated = publishedItem.Updated
						Me.theSelectedItem.Title = publishedItem.Title
						Me.theSelectedItem.Description = publishedItem.Description
						Me.theSelectedItem.ContentSize = publishedItem.ContentSize
						If Me.theSteamAppInfo.UsesSteamUGC AndAlso publishedItem.ContentPathFolderOrFileName = "" Then
							Me.theSelectedItem.ContentPathFolderOrFileName = "Folder_" + publishedItem.ID
						Else
							Me.theSelectedItem.ContentPathFolderOrFileName = publishedItem.ContentPathFolderOrFileName
						End If
						Me.theSelectedItem.PreviewImageSize = publishedItem.PreviewImageSize
						Me.theSelectedItem.PreviewImagePathFileName = publishedItem.PreviewImagePathFileName
						Me.theSelectedItem.Visibility = publishedItem.Visibility
						Me.theSelectedItem.TagsAsTextLine = publishedItem.TagsAsTextLine

						Me.theSelectedItem.IsChanged = False
						Me.DeleteTempPreviewImageFile(previewImagePathFileName, Me.theSelectedItem.ID)
						'Me.UpdateItemDetailWidgets()
					Else
						'NOTE: This is an item from SearchItemIDs().
						If output.Action = "FindAll" Then
							Me.theDisplayedItems.Add(publishedItem)
							Me.theEntireListOfItems.Add(publishedItem)
							Me.theSelectedItemIsChangingViaMe = True
							Me.SelectItemInGrid(Me.ItemsDataGridView.Rows.Count - 1)
							Me.theSelectedItemIsChangingViaMe = False
						End If
					End If
				End If
			End If

			'Me.theSelectedItemIsChangingViaMe = False
		End If

		'Me.theSelectedItemIsChangingViaMe = False
		Me.theSelectedItemDetailsIsChangingViaMe = False

		Me.AppIdComboBox.Enabled = True
		Me.ItemsDataGridView.Enabled = True
		Me.ItemTitleTextBox.Enabled = True
		Me.ItemDescriptionTextBox.Enabled = True
		Me.ItemChangeNoteTextBox.Enabled = True
		Me.ItemContentPathFileNameTextBox.Enabled = True
		Me.ItemPreviewImagePathFileNameTextBox.Enabled = True
		Me.ItemTagsGroupBox.Enabled = True
		Me.UpdateItemDetailWidgets()
	End Sub

	Private Sub DeletePublishedItemFromWorkshop_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		Dim debug As Integer = 4242
	End Sub

	Private Sub DeletePublishedItemFromWorkshop_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		If e.Cancelled Then
			Dim debug As Integer = 4242
		Else
			Dim result As String = CType(e.Result, String)
			If result = "success" Then
				Me.LogTextBox.AppendText("Delete of published item succeeded." + vbCrLf)
				If Me.theExpectedPublishedItemCount > 0 Then
					Me.theExpectedPublishedItemCount -= 1UI
				Else
					'TODO: When testing, somehow got to here.
					Dim debug As Integer = 4242
				End If
				Me.UpdateAfterDeleteItem()
			Else
				Me.LogTextBox.AppendText("ERROR: " + result + vbCrLf)
				Me.UpdateItemDetailButtons()
			End If
		End If
		Me.AppIdComboBox.Enabled = True
		Me.ItemsDataGridView.Enabled = True
		Me.ItemTitleTextBox.Enabled = True
		Me.ItemDescriptionTextBox.Enabled = True
		Me.ItemChangeNoteTextBox.Enabled = True
		Me.ItemContentPathFileNameTextBox.Enabled = True
		Me.ItemPreviewImagePathFileNameTextBox.Enabled = True
		Me.ItemTagsGroupBox.Enabled = True
	End Sub

	Private Sub PublishItem_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		If e.ProgressPercentage = 0 Then
			Me.LogTextBox.AppendText(CStr(e.UserState))
		ElseIf e.ProgressPercentage = 1 Then
			Me.LogTextBox.AppendText(vbTab + CStr(e.UserState))
		ElseIf e.ProgressPercentage = 2 Then
			Dim outputInfo As BackgroundSteamPipe.PublishItemProgressInfo = CType(e.UserState, BackgroundSteamPipe.PublishItemProgressInfo)
			'TODO: Change to using a progressbar.
			If outputInfo.TotalUploadedByteCount > 0 Then
				Dim progressPercentage As Integer = CInt(outputInfo.UploadedByteCount * 100 / outputInfo.TotalUploadedByteCount)
				Me.LogTextBox.AppendText(vbTab + vbTab + outputInfo.Status + ": " + outputInfo.UploadedByteCount.ToString("N0") + " / " + outputInfo.TotalUploadedByteCount.ToString("N0") + "   " + progressPercentage.ToString() + " %" + vbCrLf)
			Else
				Me.LogTextBox.AppendText(vbTab + outputInfo.Status + "." + vbCrLf)
			End If
		End If
	End Sub

	Private Sub PublishItem_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		Dim agreementWindowShouldBeShown As Boolean = False

		If e.Cancelled Then
			Dim debug As Integer = 4242
		Else
			Dim outputInfo As BackgroundSteamPipe.PublishItemOutputInfo = CType(e.Result, BackgroundSteamPipe.PublishItemOutputInfo)
			Dim result As String = outputInfo.Result
			Dim publishedItemID As String = outputInfo.PublishedItemID

			If result = "Succeeded" Then
				If Me.theSelectedItem.IsDraft Then
					Me.ChangeDraftItemIntoPublishedItem(Me.theSelectedItem)
					Me.theSelectedItem.ID = publishedItemID
				Else
					Me.ChangeChangedItemIntoPublishedItem(Me.theSelectedItem)
				End If

				Me.theSelectedItem.OwnerID = outputInfo.PublishedItemOwnerID
				Me.theSelectedItem.OwnerName = outputInfo.PublishedItemOwnerName
				Me.theSelectedItem.Posted = outputInfo.PublishedItemPosted
				Me.theSelectedItem.Updated = outputInfo.PublishedItemUpdated
				Me.theSelectedItem.ChangeNote = ""

				If outputInfo.SteamAgreementStatus = "NotAccepted" Then
					agreementWindowShouldBeShown = True
				End If

				Me.DeleteInUseTempPreviewImageFile(Me.theSelectedItem.PreviewImagePathFileName, Me.theSelectedItem.ID)
				Me.theSelectedItem.PreviewImagePathFileName = Path.GetFileName(Me.theSelectedItem.PreviewImagePathFileName)
				Me.theSelectedItem.PreviewImagePathFileNameIsChanged = False
			ElseIf result = "FailedContentAndChangeNote" Then
				'NOTE: Content file and change note were not updated. Keep their changed status.
				If Me.theSelectedItem.IsDraft Then
					Dim contentPathFolderOrFileNameIsChanged As Boolean = Me.theSelectedItem.ContentPathFolderOrFileNameIsChanged
					Dim changeNoteIsChanged As Boolean = Me.theSelectedItem.ChangeNoteIsChanged

					Me.ChangeDraftItemIntoPublishedItem(Me.theSelectedItem)

					Me.theSelectedItem.ContentPathFolderOrFileNameIsChanged = contentPathFolderOrFileNameIsChanged
					Me.theSelectedItem.ChangeNoteIsChanged = changeNoteIsChanged
					Me.theSelectedItem.IsChanged = contentPathFolderOrFileNameIsChanged OrElse changeNoteIsChanged
				End If

				Me.theSelectedItem.TitleIsChanged = False
				Me.theSelectedItem.DescriptionIsChanged = False
				Me.theSelectedItem.PreviewImagePathFileNameIsChanged = False
				Me.theSelectedItem.VisibilityIsChanged = False
				Me.theSelectedItem.TagsIsChanged = False
			End If
		End If

		'Me.AppIdComboBox.Enabled = True
		'Me.ItemsDataGridView.Enabled = True
		'Me.ItemTitleTextBox.Enabled = True
		'Me.ItemDescriptionTextBox.Enabled = True
		'Me.ItemChangeNoteTextBox.Enabled = True
		'Me.ItemContentPathFileNameTextBox.Enabled = True
		'Me.ItemPreviewImagePathFileNameTextBox.Enabled = True
		'Me.ItemTagsGroupBox.Enabled = True
		'Me.UpdateItemDetailWidgets()
		Me.UpdateWidgetsAfterPublish()

		Me.GetUserSteamAppCloudQuota()

		Me.theSelectedItemDetailsIsChangingViaMe = False

		If agreementWindowShouldBeShown Then
			Me.OpenAgreementRequiresAcceptanceWindow()
		End If
	End Sub

#End Region

#Region "Private Methods"

	Private Sub UpdateSteamAppWidgets()
		'NOTE: If this has not been created, then app is in not far enough in Init() and not ready for update.
		If Me.theEntireListOfItems Is Nothing OrElse Me.theSelectedGameIsStillUpdatingInterface Then
			Exit Sub
		End If
		Me.theSelectedGameIsStillUpdatingInterface = True

		If Me.LogTextBox.Text <> "" Then
			Me.LogTextBox.AppendText("------" + vbCrLf)
		End If

		Me.theSteamAppInfo = TheApp.SteamAppInfos(TheApp.Settings.PublishGameSelectedIndex)
		Me.theSteamAppId = Me.theSteamAppInfo.ID
		TheApp.WriteSteamAppIdFile(Me.theSteamAppId.m_AppId)

		Me.theSteamAppUserInfo = Nothing
		Try
			If TheApp.Settings.PublishSteamAppUserInfos.Count > 0 Then
				'NOTE: Using FirstOrDefault() instead of First() to avoid an exception when no item is found.
				Me.theSteamAppUserInfo = TheApp.Settings.PublishSteamAppUserInfos.FirstOrDefault(Function(info) info.AppID = CInt(Me.theSteamAppId.m_AppId))
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
		If Me.theSteamAppUserInfo Is Nothing Then
			'NOTE: Value was not found, so set to new info.
			Me.theSteamAppUserInfo = New SteamAppUserInfo(Me.theSteamAppId.m_AppId)
			TheApp.Settings.PublishSteamAppUserInfos.Add(Me.theSteamAppUserInfo)
		End If

		'NOTE: Swap the Tags widget before selecting an item so when item is selected tags will set correctly.
		Me.SwapSteamAppTagsWidget()

		Dim selectedRowIndex As Integer = 0
		Me.theDisplayedItems.Clear()
		Me.theEntireListOfItems.Clear()
		'Me.theTemplateItemTotalCount = 0
		'Me.theChangedItemTotalCount = 0
		If Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Count = 0 Then
			Me.AddDraftItem(Nothing)
		Else
			Dim draftItem As WorkshopItem
			Dim mostRecentlyUpdatedDraftItemDateTime As Long = 0
			For draftItemIndex As Integer = 0 To Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Count - 1
				draftItem = Me.theSteamAppUserInfo.DraftTemplateAndChangedItems(draftItemIndex)
				If draftItem.IsTemplate Then
					'Me.theTemplateItemTotalCount += 1UI
				ElseIf draftItem.IsPublished Then
					draftItem.IsChanged = True
					'Me.theChangedItemTotalCount += 1UI
				End If
				If mostRecentlyUpdatedDraftItemDateTime < draftItem.Updated Then
					mostRecentlyUpdatedDraftItemDateTime = draftItem.Updated
					selectedRowIndex = draftItemIndex
				End If
				Me.theDisplayedItems.Add(draftItem)
				Me.theEntireListOfItems.Add(draftItem)
			Next
		End If
		Me.SelectItemInGrid(selectedRowIndex)

		Me.GetUserSteamAppCloudQuota()

		Me.theBackgroundSteamPipe.GetPublishedItems(AddressOf Me.GetPublishedItems_ProgressChanged, AddressOf Me.GetPublishedItems_RunWorkerCompleted, Me.theSteamAppId.ToString())
	End Sub

	Private Sub SwapSteamAppTagsWidget()
		If theTagsWidget IsNot Nothing Then
			RemoveHandler Me.theTagsWidget.TagsPropertyChanged, AddressOf Me.TagsWidget_TagsPropertyChanged
		End If

		'Me.theTagsWidget = CType(Me.AppIdComboBox.SelectedItem, SteamAppInfo).TagsWidget
		'Dim info As SteamAppInfo = CType(Me.AppIdComboBox.SelectedItem, SteamAppInfo)
		Dim info As SteamAppInfoBase = TheApp.SteamAppInfos(TheApp.Settings.PublishGameSelectedIndex)
		Dim t As Type = info.TagsControlType
		Me.theTagsWidget = CType(t.GetConstructor(New System.Type() {}).Invoke(New Object() {}), Base_TagsUserControl)
		If Me.ItemTagsGroupBox.Controls.Count > 0 Then
			Me.ItemTagsGroupBox.Controls.RemoveAt(0)
		End If
		Me.ItemTagsGroupBox.Controls.Add(Me.theTagsWidget)
		Me.theTagsWidget.AutoScroll = True
		Me.theTagsWidget.Dock = System.Windows.Forms.DockStyle.Fill
		'Me.theTagsWidget.ItemTags = CType(Resources.GetObject("ContagionTagsUserControl1.ItemTags"), System.Collections.Generic.List(Of String))
		Me.theTagsWidget.Location = New System.Drawing.Point(3, 17)
		Me.theTagsWidget.Name = "TagsUserControl"
		Me.theTagsWidget.Size = New System.Drawing.Size(193, 307)
		Me.theTagsWidget.TabIndex = 0

		AddHandler Me.theTagsWidget.TagsPropertyChanged, AddressOf Me.TagsWidget_TagsPropertyChanged
	End Sub

	Private Sub SelectItemInGrid()
		Dim selectedRowIndex As Integer
		If Me.ItemsDataGridView.SelectedRows.Count > 0 Then
			selectedRowIndex = Me.ItemsDataGridView.SelectedRows(0).Index
		Else
			selectedRowIndex = Me.ItemsDataGridView.Rows.Count - 1
		End If
		Me.SelectItemInGrid(selectedRowIndex)
	End Sub

	Private Sub SelectItemInGrid(ByVal selectedRowIndex As Integer)
		If selectedRowIndex >= Me.ItemsDataGridView.Rows.Count Then
			selectedRowIndex = Me.ItemsDataGridView.Rows.Count - 1
		End If
		'NOTE: This line does not update the widgets connected to the list fields.
		Me.ItemsDataGridView.Rows(selectedRowIndex).Selected = True
		'NOTE: This line is required so that the item detail widgets update when the gird selection is changed programmatically.
		Me.ItemsDataGridView.CurrentCell = Me.ItemsDataGridView.Rows(selectedRowIndex).Cells(0)
		Me.ItemsDataGridView.FirstDisplayedScrollingRowIndex = selectedRowIndex
	End Sub

	Private Sub UseItemIdInDownload()
		TheApp.Settings.DownloadItemIdOrLink = Me.theSelectedItem.ID
	End Sub

	Private Sub SearchItems()
		If Me.SearchItemsToolStripTextBox.Text = "" Then
			Me.ClearSearch()
		Else
			If TheApp.Settings.PublishSearchField = PublishSearchFieldOptions.ID Then
				Me.SearchItemIDs()
			ElseIf TheApp.Settings.PublishSearchField = PublishSearchFieldOptions.Owner Then
				Me.SearchItemOwnerNames()
			ElseIf TheApp.Settings.PublishSearchField = PublishSearchFieldOptions.Title Then
				Me.SearchItemTitles()
			ElseIf TheApp.Settings.PublishSearchField = PublishSearchFieldOptions.Description Then
				Me.SearchItemDescriptions()
			ElseIf TheApp.Settings.PublishSearchField = PublishSearchFieldOptions.AllFields Then
				Me.SearchItemAllFields()
			End If
		End If
		Me.UpdateItemListWidgets(False)
	End Sub

	Private Sub ClearSearch()
		Me.theDisplayedItems.Clear()
		For Each item As WorkshopItem In Me.theEntireListOfItems
			Me.theDisplayedItems.Add(item)
		Next
	End Sub

	Private Sub SearchItemIDs()
		Dim itemTextToFind As String = Me.SearchItemsToolStripTextBox.Text.ToLower()
		Dim itemHasBeenFound As Boolean = False

		Me.theDisplayedItems.Clear()
		For Each item As WorkshopItem In Me.theEntireListOfItems
			If item.ID.ToLower().Contains(itemTextToFind) Then
				itemHasBeenFound = True
				Me.theDisplayedItems.Add(item)
			End If
		Next

		If Not itemHasBeenFound Then
			Try
				If ULong.TryParse(itemTextToFind, Nothing) Then
					GetPublishedItemDetailsViaSteamRemoteStorage(itemTextToFind, "FindAll")
				End If
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub SearchItemOwnerNames()
		Dim itemTextToFind As String = Me.SearchItemsToolStripTextBox.Text.ToLower()
		Dim itemHasBeenFound As Boolean = False

		Me.theDisplayedItems.Clear()
		For Each item As WorkshopItem In Me.theEntireListOfItems
			If item.OwnerName.ToLower().Contains(itemTextToFind) Then
				itemHasBeenFound = True
				Me.theDisplayedItems.Add(item)
			End If
		Next
	End Sub

	Private Sub SearchItemTitles()
		Dim itemTextToFind As String = Me.SearchItemsToolStripTextBox.Text.ToLower()
		Dim itemHasBeenFound As Boolean = False

		Me.theDisplayedItems.Clear()
		For Each item As WorkshopItem In Me.theEntireListOfItems
			If item.Title.ToLower().Contains(itemTextToFind) Then
				itemHasBeenFound = True
				Me.theDisplayedItems.Add(item)
			End If
		Next
	End Sub

	Private Sub SearchItemDescriptions()
		Dim itemTextToFind As String = Me.SearchItemsToolStripTextBox.Text.ToLower()
		Dim itemHasBeenFound As Boolean = False

		Me.theDisplayedItems.Clear()
		For Each item As WorkshopItem In Me.theEntireListOfItems
			If item.Description.ToLower().Contains(itemTextToFind) Then
				itemHasBeenFound = True
				Me.theDisplayedItems.Add(item)
			End If
		Next
	End Sub

	Private Sub SearchItemAllFields()
		Dim itemTextToFind As String = Me.SearchItemsToolStripTextBox.Text.ToLower()
		Dim itemHasBeenFound As Boolean = False

		Me.theDisplayedItems.Clear()
		For Each item As WorkshopItem In Me.theEntireListOfItems
			If item.ID.ToLower().Contains(itemTextToFind) Then
				itemHasBeenFound = True
				Me.theDisplayedItems.Add(item)
			ElseIf item.OwnerName.ToLower().Contains(itemTextToFind) Then
				itemHasBeenFound = True
				Me.theDisplayedItems.Add(item)
			ElseIf item.Title.ToLower().Contains(itemTextToFind) Then
				itemHasBeenFound = True
				Me.theDisplayedItems.Add(item)
			ElseIf item.Description.ToLower().Contains(itemTextToFind) Then
				itemHasBeenFound = True
				Me.theDisplayedItems.Add(item)
			End If
		Next
	End Sub

	Private Sub AddDraftItem(ByVal itemToCopy As WorkshopItem)
		Dim draftItem As WorkshopItem
		If itemToCopy Is Nothing Then
			draftItem = New WorkshopItem()
		Else
			draftItem = CType(itemToCopy.Clone(), WorkshopItem)
			draftItem.SetAllChangedForNonEmptyFields()
		End If
		Me.theDisplayedItems.Add(draftItem)
		Me.theEntireListOfItems.Add(draftItem)
		Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Add(draftItem)
		Me.UpdateItemListWidgets(False)
	End Sub

	Private Sub ChangeDraftItemIntoPublishedItem(ByVal item As WorkshopItem)
		Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Remove(item)
		item.IsChanged = False
		Me.UpdateItemListWidgets(False)
	End Sub

	Private Sub ChangePublishedItemIntoChangedItem(ByVal item As WorkshopItem)
		Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Add(item)
		'Me.theChangedItemTotalCount += 1UI
		Me.UpdateItemListWidgets(False)
		Me.SaveCopyOfPreviewImageFile(item)
	End Sub

	Private Sub ChangeChangedItemIntoPublishedItem(ByVal item As WorkshopItem)
		Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Remove(item)
		'Me.theChangedItemTotalCount -= 1UI
		item.ChangeNote = ""
		item.IsChanged = False
		Me.UpdateItemListWidgets(False)
	End Sub

	Private Sub OpenSteamSubscriberAgreement()
		System.Diagnostics.Process.Start(My.Resources.Link_SteamSubscriberAgreement)
	End Sub

	Private Sub UpdateItemListWidgets(ByVal isProgress As Boolean)
		If Not isProgress Then
			' If the DataGridView is not currently sorted, then sortedColumn is Nothing.
			Dim sortedColumn As DataGridViewColumn = Me.ItemsDataGridView.SortedColumn
			If sortedColumn IsNot Nothing Then
				Dim direction As ListSortDirection
				If Me.ItemsDataGridView.SortOrder = SortOrder.Ascending Then
					direction = ListSortDirection.Ascending
				Else
					direction = ListSortDirection.Descending
				End If
				Me.ItemsDataGridView.Sort(sortedColumn, direction)
			End If
		End If

		'Dim draftItemsDisplayedCount As UInteger = CUInt(Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Count - Me.theTemplateItemDisplayedCount - Me.theChangedItemDisplayedCount)
		'Dim publishedItemsDisplayedCount As UInteger = CUInt(Me.theDisplayedItems.Count - Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Count)
		'Dim draftItemsTotalCount As UInteger = CUInt(Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Count - Me.theTemplateItemTotalCount - Me.theChangedItemTotalCount)
		'Dim publishedItemsTotalCount As UInteger = CUInt(Me.theEntireListOfItems.Count - Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Count)
		Dim draftItemsDisplayedCount As UInteger = Me.theDisplayedItems.DraftItemCount
		Dim publishedItemsDisplayedCount As UInteger = Me.theDisplayedItems.PublishedItemCount
		Dim draftItemsTotalCount As UInteger = Me.theEntireListOfItems.DraftItemCount
		Dim publishedItemsTotalCount As UInteger = Me.theEntireListOfItems.PublishedItemCount

		Me.ItemCountsToolStripLabel.Text = ""
		If Me.theDisplayedItems.Count <> Me.theEntireListOfItems.Count Then
			Me.ItemCountsToolStripLabel.Text += draftItemsDisplayedCount.ToString() + "/"
		End If
		Me.ItemCountsToolStripLabel.Text += draftItemsTotalCount.ToString() + " draft + "
		If Me.theDisplayedItems.Count <> Me.theEntireListOfItems.Count Then
			'Me.ItemCountsToolStripLabel.Text += Me.theTemplateItemDisplayedCount.ToString() + "/"
			Me.ItemCountsToolStripLabel.Text += Me.theDisplayedItems.TemplateItemCount.ToString() + "/"
		End If
		'Me.ItemCountsToolStripLabel.Text += Me.theTemplateItemTotalCount.ToString() + " template + "
		Me.ItemCountsToolStripLabel.Text += Me.theEntireListOfItems.TemplateItemCount.ToString() + " template + "
		If Me.theDisplayedItems.Count <> Me.theEntireListOfItems.Count Then
			'Me.ItemCountsToolStripLabel.Text += Me.theChangedItemDisplayedCount.ToString() + "/"
			Me.ItemCountsToolStripLabel.Text += Me.theDisplayedItems.ChangedItemCount.ToString() + "/"
		End If
		'Me.ItemCountsToolStripLabel.Text += Me.theChangedItemTotalCount.ToString() + " changed + "
		Me.ItemCountsToolStripLabel.Text += Me.theEntireListOfItems.ChangedItemCount.ToString() + " changed + "
		If Me.theDisplayedItems.Count <> Me.theEntireListOfItems.Count Then
			Me.ItemCountsToolStripLabel.Text += publishedItemsDisplayedCount.ToString() + "/"
		End If
		Me.ItemCountsToolStripLabel.Text += publishedItemsTotalCount.ToString() + " published"
		If isProgress Then
			Dim remainingPublishedItemsCount As UInteger = Me.theExpectedPublishedItemCount - publishedItemsTotalCount
			Me.ItemCountsToolStripLabel.Text += " (" + remainingPublishedItemsCount.ToString() + " more to get)"
		Else
			'If (publishedItemsTotalCount + Me.theChangedItemTotalCount) <> Me.theExpectedPublishedItemCount Then
			If (publishedItemsTotalCount + Me.theEntireListOfItems.ChangedItemCount) <> Me.theExpectedPublishedItemCount Then
				Me.ItemCountsToolStripLabel.Text += " (" + Me.theExpectedPublishedItemCount.ToString() + " expected)"
			End If
		End If
		Me.ItemCountsToolStripLabel.Text += " = "
		If Me.theDisplayedItems.Count <> Me.theEntireListOfItems.Count Then
			Me.ItemCountsToolStripLabel.Text += Me.theDisplayedItems.Count.ToString() + "/"
		End If
		Me.ItemCountsToolStripLabel.Text += Me.theEntireListOfItems.Count.ToString() + " total"
	End Sub

	Private Sub UpdateItemDetails()
		If Me.theSelectedItem IsNot Nothing Then
			RemoveHandler Me.theSelectedItem.PropertyChanged, AddressOf Me.WorkshopItem_PropertyChanged
		End If
		Me.theSelectedItem = Me.theDisplayedItems(Me.ItemsDataGridView.SelectedRows(0).Index)
		AddHandler Me.theSelectedItem.PropertyChanged, AddressOf Me.WorkshopItem_PropertyChanged

		If Me.theSelectedItem.IsDraft Then
			Me.UpdateItemDetailWidgets()
		ElseIf Me.theSelectedItem.IsTemplate Then
			Me.theUnchangedSelectedTemplateItem = CType(Me.theSelectedItem.Clone(), WorkshopItem)
			Me.theUnchangedSelectedTemplateItem.IsTemplate = True
			Me.UpdateItemDetailWidgets()
		ElseIf Me.theSelectedItem.IsChanged Then
			Me.UpdateItemDetailWidgets()
		Else
			'NOTE: UpdateItemDetailWidgets() will be called from the 'bw_completed' handler.
			Me.GetPublishedItemDetailsViaSteamRemoteStorage(Me.theSelectedItem.ID, "All")
			Exit Sub
		End If
	End Sub

	Private Sub UpdateItemDetailWidgets()
		If Me.theUserSteamID = 0 Then
			Me.GetUserSteamID()
		End If

		Dim editableTextBoxesAreReadOnly As Boolean = (Me.theSelectedItem.IsPublished) AndAlso (Me.theSelectedItem.OwnerID <> Me.theUserSteamID)
		Dim editableNonTextWidgetsAreEnabled As Boolean = (Me.theSelectedItem.IsDraft) OrElse (Me.theSelectedItem.IsTemplate) OrElse (Me.theSelectedItem.OwnerID = Me.theUserSteamID)

		Me.ItemGroupBox.Enabled = True
		Me.UpdateItemGroupBoxLabel()

		Me.UpdateItemTitleLabel()
		Me.ItemTitleTextBox.ReadOnly = editableTextBoxesAreReadOnly

		Me.UpdateItemDescriptionLabel()
		Me.ItemDescriptionTextBox.ReadOnly = editableTextBoxesAreReadOnly

		Me.UpdateItemChangeNoteLabel()
		Me.ItemChangeNoteTextBox.ReadOnly = editableTextBoxesAreReadOnly

		Me.UpdateItemContentLabel()
		Me.ItemContentPathFileNameTextBox.ReadOnly = editableTextBoxesAreReadOnly

		Me.UpdateItemPreviewImageLabel()
		Me.ItemPreviewImagePathFileNameTextBox.ReadOnly = editableTextBoxesAreReadOnly
		If Not Me.theSelectedItem.IsPublished OrElse Me.theSelectedItem.IsChanged Then
			Me.UpdateItemPreviewImageBox()
		End If

		Me.UpdateItemVisibilityLabel()
		Me.ItemVisibilityComboBox.Enabled = editableNonTextWidgetsAreEnabled

		Me.ItemTagsGroupBox.Enabled = True
		'NOTE: There is no automatic data-binding with TagsWidget, so manually bind from object to widget here.
		Me.theTagsWidget.ItemTags = Me.theSelectedItem.Tags
		Me.UpdateItemTagsLabel()
		Me.theTagsWidget.Enabled = editableNonTextWidgetsAreEnabled

		Me.theWorkshopPageLink = AppConstants.WorkshopLinkStart + Me.theSelectedItem.ID

		Me.UpdateItemDetailButtons()
	End Sub

	Private Sub UpdateItemChangedStatus()
		If Not Me.theSelectedItem.IsChanged Then
			If Me.theSelectedItem.IsTemplate Then
				Me.theSelectedItem.IsChanged = True
				Me.UpdateItemGroupBoxLabel()
			ElseIf Me.theSelectedItem.IsPublished Then
				Me.theSelectedItem.IsChanged = True
				Me.ChangePublishedItemIntoChangedItem(Me.theSelectedItem)
				Me.UpdateItemGroupBoxLabel()
			End If
		End If
		Me.theSelectedItem.Updated = MathModule.DateTimeToUnixTimeStamp(DateTime.Now())
		Me.UpdateItemDetailButtons()
	End Sub

	Private Sub UpdateItemGroupBoxLabel()
		Dim changedMarker As String = ""
		If Me.theSelectedItem.IsChanged AndAlso Not Me.theSelectedItem.IsDraft Then
			changedMarker = AppConstants.ChangedMarker
		End If
		Me.ItemGroupBox.Text = "Item" + changedMarker
	End Sub

	Private Sub UpdateItemTitleLabel()
		Dim titleSize As Integer = Me.theSelectedItem.Title.Length
		Dim titleSizeMax As Integer = CInt(Steamworks.Constants.k_cchPublishedDocumentTitleMax)
		Dim changedMarker As String = ""
		If Me.theSelectedItem.TitleIsChanged AndAlso Not Me.theSelectedItem.IsDraft Then
			changedMarker = AppConstants.ChangedMarker
		End If
		Me.ItemTitleLabel.Text = "Title" + changedMarker + " (" + titleSize.ToString() + " / " + titleSizeMax.ToString() + " characters max):"
	End Sub

	Private Sub UpdateItemDescriptionLabel()
		Dim descriptionSize As Integer = Me.theSelectedItem.Description.Length
		Dim descriptionSizeMax As Integer = CInt(Steamworks.Constants.k_cchPublishedDocumentDescriptionMax)
		Dim changedMarker As String = ""
		If Me.theSelectedItem.DescriptionIsChanged AndAlso Not Me.theSelectedItem.IsDraft Then
			changedMarker = AppConstants.ChangedMarker
		End If
		Me.ItemDescriptionLabel.Text = "Description" + changedMarker + " (" + descriptionSize.ToString() + " / " + descriptionSizeMax.ToString() + " characters max):"
	End Sub

	Private Sub UpdateItemChangeNoteLabel()
		Dim changeNoteSize As Integer = Me.theSelectedItem.ChangeNote.Length
		Dim changeNoteSizeMax As Integer = CInt(Steamworks.Constants.k_cchPublishedDocumentChangeDescriptionMax)
		Dim changedMarker As String = ""
		If Me.theSelectedItem.ChangeNoteIsChanged AndAlso Not Me.theSelectedItem.IsDraft Then
			changedMarker = AppConstants.ChangedMarker
		End If
		Me.ItemChangeNoteLabel.Text = "Change Note" + changedMarker + " (" + changeNoteSize.ToString() + " / " + changeNoteSizeMax.ToString() + " characters max):"
	End Sub

	Private Sub UpdateItemContentLabel()
		Dim contentFileSizeText As String = "0"
		If TheApp.SteamAppInfos(TheApp.Settings.PublishGameSelectedIndex).CanUseContentFolderOrFile Then
			Me.ItemContentFolderOrFileLabel.Text = "Content Folder or File"
			If Directory.Exists(Me.theSelectedItem.ContentPathFolderOrFileName) Then
				Dim folderSize As ULong = FileManager.GetFolderSize(Me.theSelectedItem.ContentPathFolderOrFileName)
				contentFileSizeText = MathModule.ByteUnitsConversion(folderSize)
			ElseIf File.Exists(Me.theSelectedItem.ContentPathFolderOrFileName) Then
				Dim aFile As New FileInfo(Me.theSelectedItem.ContentPathFolderOrFileName)
				contentFileSizeText = MathModule.ByteUnitsConversion(CULng(aFile.Length))
			ElseIf Me.theSelectedItem.ContentSize > 0 AndAlso Me.theSelectedItem.IsPublished Then
				contentFileSizeText = MathModule.ByteUnitsConversion(CULng(Me.theSelectedItem.ContentSize))
			End If
		ElseIf TheApp.SteamAppInfos(TheApp.Settings.PublishGameSelectedIndex).UsesSteamUGC Then
			Me.ItemContentFolderOrFileLabel.Text = "Content Folder"
			If Directory.Exists(Me.theSelectedItem.ContentPathFolderOrFileName) Then
				Dim folderSize As ULong = FileManager.GetFolderSize(Me.theSelectedItem.ContentPathFolderOrFileName)
				contentFileSizeText = MathModule.ByteUnitsConversion(folderSize)
			ElseIf Me.theSelectedItem.ContentSize > 0 AndAlso Me.theSelectedItem.IsPublished Then
				contentFileSizeText = MathModule.ByteUnitsConversion(CULng(Me.theSelectedItem.ContentSize))
			End If
		Else
			Me.ItemContentFolderOrFileLabel.Text = "Content File"
			If File.Exists(Me.theSelectedItem.ContentPathFolderOrFileName) Then
				Dim aFile As New FileInfo(Me.theSelectedItem.ContentPathFolderOrFileName)
				contentFileSizeText = MathModule.ByteUnitsConversion(CULng(aFile.Length))
			ElseIf Me.theSelectedItem.ContentSize > 0 AndAlso Me.theSelectedItem.IsPublished Then
				contentFileSizeText = MathModule.ByteUnitsConversion(CULng(Me.theSelectedItem.ContentSize))
			End If
		End If
		'Dim contentFileSizeMaxText As String = "<unknown>"
		''Dim contentFileSizeMax As Integer = CInt(Steamworks.Constants.k_unMaxCloudFileChunkSize / 1048576)
		''contentFileSizeMaxText = contentFileSizeMax.ToString()
		Dim changedMarker As String = ""
		If Me.theSelectedItem.ContentPathFolderOrFileNameIsChanged AndAlso Not Me.theSelectedItem.IsDraft Then
			changedMarker = AppConstants.ChangedMarker
		End If
		'NOTE: Not sure what max size is, so do not show it.
		'Me.ItemContentFolderOrFileLabel.Text += changedMarker + " (" + contentFileSizeText + " / " + contentFileSizeMaxText + " MB max):"
		Me.ItemContentFolderOrFileLabel.Text += changedMarker
		If contentFileSizeText <> "0" Then
			Me.ItemContentFolderOrFileLabel.Text += " (" + contentFileSizeText + ")"
		End If
		Me.ItemContentFolderOrFileLabel.Text += ":"
	End Sub

	Private Sub UpdateItemPreviewImageLabel()
		Dim previewImageSizeText As String = "0"
		If File.Exists(Me.theSelectedItem.PreviewImagePathFileName) Then
			Dim aFile As New FileInfo(Me.theSelectedItem.PreviewImagePathFileName)
			previewImageSizeText = MathModule.ByteUnitsConversion(CULng(aFile.Length))
		ElseIf Me.theSelectedItem.PreviewImageSize > 0 AndAlso Me.theSelectedItem.IsPublished Then
			previewImageSizeText = MathModule.ByteUnitsConversion(CULng(Me.theSelectedItem.PreviewImageSize))
		End If
		'Dim previewImageSizeMaxText As String = "<unknown>"
		Dim changedMarker As String = ""
		If Me.theSelectedItem.PreviewImagePathFileNameIsChanged AndAlso Not Me.theSelectedItem.IsDraft Then
			changedMarker = AppConstants.ChangedMarker
		End If
		'NOTE: Not sure what max size is, so do not show it.
		'Me.ItemPreviewImageLabel.Text = "Preview Image" + changedMarker + " (" + previewImageSizeText + " / " + previewImageSizeMaxText + " MB max):"
		Me.ItemPreviewImageLabel.Text = "Preview Image" + changedMarker
		If previewImageSizeText <> "0" Then
			Me.ItemPreviewImageLabel.Text += " (" + previewImageSizeText + ")"
		End If
		Me.ItemPreviewImageLabel.Text += ":"
	End Sub

	Private Sub UpdateItemPreviewImageBox()
		If File.Exists(Me.theSelectedItem.PreviewImagePathFileName) Then
			Try
				If Me.ItemPreviewImagePictureBox.Image IsNot Nothing Then
					Me.ItemPreviewImagePictureBox.Image.Dispose()
				End If
				Me.ItemPreviewImagePictureBox.Image = Image.FromFile(Me.theSelectedItem.PreviewImagePathFileName)
			Catch ex As Exception
				' Problem setting Image, so reset the textbox text.
				Me.theSelectedItem.PreviewImagePathFileName = Me.theSavedPreviewImagePathFileName
			End Try
		Else
			If Me.ItemPreviewImagePictureBox.Image IsNot Nothing Then
				Me.ItemPreviewImagePictureBox.Image.Dispose()
			End If
			Me.ItemPreviewImagePictureBox.Image = Nothing
		End If
	End Sub

	'NOTE: When item is changed, save the preview image to a file in Crowbar's appdata folder.
	'      Save using file name like "<ID>_preview_00.bmp" [00 for primary].
	'      Need to delete the file when published or reverted.
	Private Sub SaveCopyOfPreviewImageFile(ByVal item As WorkshopItem)
		If Me.ItemPreviewImagePictureBox.Image Is Nothing Then
			Exit Sub
		End If

		Dim anImageFormat As Imaging.ImageFormat = Me.ItemPreviewImagePictureBox.Image.RawFormat
		Dim previewImagePathFileName As String = ""
		If anImageFormat.Equals(Imaging.ImageFormat.Gif) Then
			previewImagePathFileName = Me.GetPreviewImagePathFileName("temp.gif", item.ID, 0)
		Else
			previewImagePathFileName = Me.GetPreviewImagePathFileName("temp.png", item.ID, 0)
			anImageFormat = Imaging.ImageFormat.Png
		End If
		If previewImagePathFileName <> "" Then
			Try
				Me.ItemPreviewImagePictureBox.Image.Save(previewImagePathFileName, anImageFormat)
			Catch ex As Exception
				If Not File.Exists(previewImagePathFileName) Then
					Me.LogTextBox.AppendText("ERROR: Crowbar tried to save preview image to temp file """ + previewImagePathFileName + """ but Windows gave this message: " + ex.Message)
				End If
			End Try
			If File.Exists(previewImagePathFileName) Then
				Dim selectedItemDetailsIsChangingViaMe As Boolean = Me.theSelectedItemDetailsIsChangingViaMe
				Dim selectedItemPreviewImagePathFileNameIsChanged As Boolean = Me.theSelectedItem.PreviewImagePathFileNameIsChanged
				Me.theSelectedItemDetailsIsChangingViaMe = True

				Me.theSelectedItem.PreviewImagePathFileName = previewImagePathFileName

				Me.theSelectedItemDetailsIsChangingViaMe = selectedItemDetailsIsChangingViaMe
				Me.theSelectedItem.PreviewImagePathFileNameIsChanged = selectedItemPreviewImagePathFileNameIsChanged
			End If
		End If
	End Sub

	' Copy the image into memory, so the image file can be deleted.
	Private Sub DeleteInUseTempPreviewImageFile(ByVal itemPreviewImagePathFileName As String, ByVal itemID As String)
        Dim img As Image = Nothing

        Try
            If File.Exists(itemPreviewImagePathFileName) Then
                img = Image.FromFile(itemPreviewImagePathFileName)
                If Me.ItemPreviewImagePictureBox.Image IsNot Nothing Then
                    Me.ItemPreviewImagePictureBox.Image.Dispose()
                End If
                Me.ItemPreviewImagePictureBox.Image = New Bitmap(img)
                img.Dispose()
            End If
        Catch ex As Exception
            If img IsNot Nothing Then
                img.Dispose()
                img = Nothing
            End If
        End Try

        Me.DeleteTempPreviewImageFile(itemPreviewImagePathFileName, itemID)
    End Sub

	Private Sub DeleteTempPreviewImageFile(ByVal itemPreviewImagePathFileName As String, ByVal itemID As String)
		Dim previewImagePathFileName As String = Me.GetPreviewImagePathFileName(itemPreviewImagePathFileName, itemID, 0)
		If previewImagePathFileName <> "" AndAlso File.Exists(previewImagePathFileName) Then
			Try
				File.Delete(previewImagePathFileName)
			Catch ex As Exception
				Me.LogTextBox.AppendText("ERROR: Crowbar tried to delete an old temp file """ + previewImagePathFileName + """ but Windows gave this message: " + ex.Message)
			End Try
		End If
	End Sub

	Private Function GetPreviewImagePathFileName(ByVal sourcePathFileName As String, ByVal itemID As String, ByVal previewIndex As Integer) As String
		Dim extension As String = Path.GetExtension(sourcePathFileName)
		Dim targetFileName As String = itemID + "_" + previewIndex.ToString("00") + extension
		Dim previewsPath As String = TheApp.GetPreviewsPath()
		Dim targetPathFileName As String
		If previewsPath <> "" Then
			Try
				targetPathFileName = Path.Combine(previewsPath, targetFileName)
			Catch ex As Exception
				targetPathFileName = ""
			End Try
		Else
			targetPathFileName = ""
		End If

		Return targetPathFileName
	End Function

	Private Sub UpdateItemVisibilityLabel()
		Dim changedMarker As String = ""
		If Me.theSelectedItem.VisibilityIsChanged AndAlso Not Me.theSelectedItem.IsDraft Then
			changedMarker = AppConstants.ChangedMarker
		End If
		Me.ItemVisibilityLabel.Text = "Visibility" + changedMarker + ":"
	End Sub

	Private Sub UpdateItemTagsLabel()
		Dim changedMarker As String = ""
		If Me.theSelectedItem.TagsIsChanged AndAlso Not Me.theSelectedItem.IsDraft Then
			changedMarker = AppConstants.ChangedMarker
		End If
		Me.ItemTagsGroupBox.Text = "Tags" + changedMarker
	End Sub

	Private Sub UpdateItemDetailButtons()
		If Me.theUserSteamID = 0 Then
			Me.GetUserSteamID()
		End If

		Dim editableNonTextWidgetsAreEnabled As Boolean = (Me.theSelectedItem.IsDraft) OrElse (Me.theSelectedItem.IsTemplate) OrElse (Me.theSelectedItem.OwnerID = Me.theUserSteamID)

		Me.BrowseItemContentPathFileNameButton.Enabled = editableNonTextWidgetsAreEnabled
		Me.BrowseItemPreviewImagePathFileNameButton.Enabled = editableNonTextWidgetsAreEnabled

		Me.SaveAsTemplateOrDraftItemButton.Enabled = True
		If Me.theSelectedItem.IsTemplate Then
			Me.SaveAsTemplateOrDraftItemButton.Text = "Save as Draft"
		Else
			Me.SaveAsTemplateOrDraftItemButton.Text = "Save as Template"
		End If

		'Me.RefreshOrRevertItemButton.Visible = True
		'Me.RefreshOrRevertItemButton.Enabled = (Me.theSelectedItem.ID <> "" AndAlso Not Me.theSelectedItem.IsDraft)
		Me.RefreshOrRevertItemButton.Enabled = (Me.theSelectedItem.IsPublished) OrElse (Me.theSelectedItem.IsTemplate AndAlso Me.theSelectedItem.IsChanged)
		If (Me.theSelectedItem.IsTemplate) OrElse (Me.theSelectedItem.IsPublished AndAlso Me.theSelectedItem.IsChanged) Then
			Me.RefreshOrRevertItemButton.Text = "Revert"
		Else
			Me.RefreshOrRevertItemButton.Text = "Refresh"
		End If

		Me.OpenWorkshopPageButton.Visible = (Not Me.theSelectedItem.IsTemplate)
		Me.OpenWorkshopPageButton.Enabled = (Not Me.theSelectedItem.IsDraft)

		Me.SaveTemplateButton.Visible = (Me.theSelectedItem.IsTemplate)
		Me.SaveTemplateButton.Enabled = (Me.theSelectedItem.IsChanged)

		Me.DeleteItemButton.Enabled = editableNonTextWidgetsAreEnabled

		'NOTE: SteamRemoteStorage_PublishWorkshopFile requires Item to have a Title, a Description, a Content File, and a Preview Image.
		Me.PublishItemButton.Enabled = (((Me.theSelectedItem.IsDraft) AndAlso (Me.theSelectedItem.Title <> "" AndAlso Me.theSelectedItem.Description <> "" AndAlso Me.theSelectedItem.ContentPathFolderOrFileName <> "" AndAlso Me.theSelectedItem.PreviewImagePathFileName <> "")) OrElse (Me.theSelectedItem.IsChanged AndAlso (Me.theUserSteamID = Me.theSelectedItem.OwnerID)) OrElse (Me.theSelectedItem.IsTemplate))
	End Sub

	Private Sub SwapBetweenOwnerNameAndID()
		If Me.ItemOwnerTextBox.DataBindings("Text").BindingMemberInfo.BindingMember = "OwnerName" Then
			Me.ItemOwnerTextBox.DataBindings.Remove(Me.ItemOwnerTextBox.DataBindings("Text"))
			Me.ItemOwnerTextBox.DataBindings.Add("Text", Me.theItemBindingSource, "OwnerID", False, DataSourceUpdateMode.OnValidation)
		Else
			Me.ItemOwnerTextBox.DataBindings.Remove(Me.ItemOwnerTextBox.DataBindings("Text"))
			Me.ItemOwnerTextBox.DataBindings.Add("Text", Me.theItemBindingSource, "OwnerName", False, DataSourceUpdateMode.OnValidation)
		End If
	End Sub

	Private Sub ToggleWordWrapImageOnCheckbox(ByVal aCheckBox As CheckBox)
		If aCheckBox.Checked Then
			aCheckBox.BackgroundImage = My.Resources.WordWrap
		Else
			aCheckBox.BackgroundImage = My.Resources.WordWrapOff
		End If
	End Sub

	Private Sub BrowseForContentPathFolderOrFileName()
		Dim openFileWdw As New OpenFileDialog()

		If Me.theSteamAppInfo.CanUseContentFolderOrFile OrElse Me.theSteamAppInfo.UsesSteamUGC Then
			If Me.theSteamAppInfo.ContentFileExtensionsAndDescriptions.Count > 0 Then
				openFileWdw.Title = "Open the folder or the package file you want to upload"
			Else
				openFileWdw.Title = "Open the folder you want to upload"
			End If
			openFileWdw.FileName = "[Folder Selection]"
		Else
			openFileWdw.Title = "Open the file you want to upload"
		End If
		If File.Exists(Me.theSelectedItem.ContentPathFolderOrFileName) Then
			openFileWdw.InitialDirectory = FileManager.GetPath(Me.theSelectedItem.ContentPathFolderOrFileName)
		Else
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(Me.theSelectedItem.ContentPathFolderOrFileName)
			If openFileWdw.InitialDirectory = "" Then
				openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			End If
		End If

		'NOTE: Must use temp string because openFileWdw.Filter validates itself on assignment.
		Dim fileFilter As String = ""
		For i As Integer = 0 To Me.theSteamAppInfo.ContentFileExtensionsAndDescriptions.Count - 1
			fileFilter += Me.theSteamAppInfo.ContentFileExtensionsAndDescriptions.Values(i)
			fileFilter += " (*."
			fileFilter += Me.theSteamAppInfo.ContentFileExtensionsAndDescriptions.Keys(i)
			fileFilter += ")|*."
			fileFilter += Me.theSteamAppInfo.ContentFileExtensionsAndDescriptions.Keys(i)
			fileFilter += "|"
		Next
		fileFilter += "All Files (*.*)|*.*"
		openFileWdw.Filter = fileFilter

		openFileWdw.AddExtension = False
		openFileWdw.CheckFileExists = False
		openFileWdw.Multiselect = False
		openFileWdw.ValidateNames = True

		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			If Me.theSteamAppInfo.CanUseContentFolderOrFile OrElse Me.theSteamAppInfo.UsesSteamUGC Then
				If Path.GetFileNameWithoutExtension(openFileWdw.FileName) = "[Folder Selection]" Then
					Me.theSelectedItem.ContentPathFolderOrFileName = FileManager.GetPath(openFileWdw.FileName)
				Else
					Me.theSelectedItem.ContentPathFolderOrFileName = openFileWdw.FileName
				End If
			Else
				Me.theSelectedItem.ContentPathFolderOrFileName = openFileWdw.FileName
			End If
			Me.UpdateItemContentLabel()
		End If
	End Sub

	Private Sub BrowseForPreviewImage()
		Dim openFileWdw As New OpenFileDialog()

		openFileWdw.Title = "Open the image file you want to use for preview image"
		If File.Exists(Me.theSelectedItem.PreviewImagePathFileName) Then
			openFileWdw.InitialDirectory = FileManager.GetPath(Me.theSelectedItem.PreviewImagePathFileName)
		Else
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(Me.theSelectedItem.PreviewImagePathFileName)
			If openFileWdw.InitialDirectory = "" Then
				openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			End If
		End If
		openFileWdw.Filter = "Image Files (*.bmp;*.gif;*.jpeg;*.jpg;*.png;*.wmf)|*.bmp;*.gif;*.jpeg;*.jpg;*.png;*.wmf|All Files (*.*)|*.*"
		openFileWdw.AddExtension = True
		openFileWdw.CheckFileExists = False
		openFileWdw.Multiselect = False
		openFileWdw.ValidateNames = True

		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			' Save the pathFileName in case the PictureBox.Image does not like the file.
			Me.theSavedPreviewImagePathFileName = Me.theSelectedItem.PreviewImagePathFileName
			'NOTE: Changing the file name field also changes the preview picturebox.
			Me.theSelectedItem.PreviewImagePathFileName = openFileWdw.FileName
		End If
	End Sub

	'- If draft, then change draft to template.
	'X If template. This should not occur.
	'- If changed or published, then copy item as template, add it to list, and select it in list.
	Private Sub SaveItemAsTemplate()
		Dim anItem As WorkshopItem
		If Me.theSelectedItem.IsDraft Then
			anItem = Me.theSelectedItem
		Else
			anItem = CType(Me.theSelectedItem.Clone(), WorkshopItem)
		End If

		Me.theSelectedItemDetailsIsChangingViaMe = True
		anItem.IsTemplate = True
		'anItem.ContentPathFolderOrFileName = ""
		'anItem.PreviewImagePathFileName = ""
		anItem.IsChanged = False
		'NOTE: Without this line, the ID field in the widgets does not change until a different item is selected.
		Me.theItemBindingSource.ResetCurrentItem()
		Me.theSelectedItemDetailsIsChangingViaMe = False
		'Me.theTemplateItemTotalCount += 1UI

		If anItem IsNot Me.theSelectedItem Then
			Me.theDisplayedItems.Add(anItem)
			Me.theEntireListOfItems.Add(anItem)
			Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Add(anItem)
			Me.SaveCopyOfPreviewImageFile(anItem)
			Me.SelectItemInGrid(Me.ItemsDataGridView.Rows.Count - 1)
		End If

		Me.UpdateItemListWidgets(False)
		Me.UpdateItemDetails()
	End Sub

	Private Sub RefreshOrRevertItem()
		If Me.theSelectedItem.IsChanged Then
			If Me.theSelectedItem.IsTemplate Then
				'Me.theSelectedItemDetailsIsChangingViaMe = True
				''NOTE: Change the item in the list (and not the Me.theSelectedItem) so that list and selected item stay synced.
				'Dim selectedItemIndex As Integer = Me.theItemBindingSource.IndexOf(Me.theSelectedItem)
				'Me.theDisplayedItems(selectedItemIndex) = Me.theUnchangedSelectedTemplateItem
				'Me.theSelectedItemDetailsIsChangingViaMe = False
				Me.RevertChangedTemplate()
			ElseIf Me.theSelectedItem.IsPublished Then
				Me.ChangeChangedItemIntoPublishedItem(Me.theSelectedItem)
			End If
		End If
		Me.UpdateItemDetails()
	End Sub

	Private Sub OpenWorkshopPage()
		System.Diagnostics.Process.Start(Me.theWorkshopPageLink)
	End Sub

	Private Sub SaveTemplate()
		Me.theSelectedItemDetailsIsChangingViaMe = True
		Me.theSelectedItem.IsChanged = False
		Me.theSelectedItemDetailsIsChangingViaMe = False
		'Me.UpdateItemListWidgets(False)
		Me.UpdateItemDetails()
	End Sub

	Private Sub DeleteItem()
		Dim deleteItemWindow As DeleteItemForm = New DeleteItemForm()
		If Me.theSelectedItem.IsPublished Then
			deleteItemWindow.TextBox1.Text = "Deleting will remove the item from the Workshop permanently." + vbCrLf + "Backup anything you want to save before deleting."
			If deleteItemWindow.ShowDialog() = DialogResult.OK Then
				Me.DeletePublishedItemFromWorkshop()
			End If
		Else
			deleteItemWindow.TextBox1.Text = "Deleting will remove the item from your saved items permanently." + vbCrLf + "Backup anything you want to save before deleting."
			If deleteItemWindow.ShowDialog() = DialogResult.OK Then
				Me.UpdateAfterDeleteItem()
			End If
		End If
	End Sub

	Private Sub UpdateAfterDeleteItem()
		'NOTE: Need to make a temp variable because Me.theSelectedItem will change between function calls.
		Dim deletedItem As WorkshopItem = Me.theSelectedItem
		'If deletedItem.IsTemplate Then
		'	Me.theTemplateItemTotalCount -= 1UI
		'ElseIf Not deletedItem.IsDraft AndAlso deletedItem.IsChanged Then
		'	Me.theChangedItemTotalCount -= 1UI
		'Else
		'End If
		'NOTE: No exception is raised if item is not in any of these lists.
		Me.theDisplayedItems.Remove(deletedItem)
		Me.theEntireListOfItems.Remove(deletedItem)
		Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Remove(deletedItem)
		Me.UpdateItemListWidgets(False)
		Me.SelectItemInGrid()

		Me.GetUserSteamAppCloudQuota()
	End Sub

	Private Sub DeletePublishedItemFromWorkshop()
		Me.AppIdComboBox.Enabled = False
		Me.ItemsDataGridView.Enabled = False
		Me.ItemGroupBox.Enabled = False
		'Me.DeleteItemButton.Enabled = False
		Me.PublishItemButton.Enabled = False
		If Me.LogTextBox.Text <> "" Then
			Me.LogTextBox.AppendText("------" + vbCrLf)
		End If

		Me.theBackgroundSteamPipe.DeletePublishedItemFromWorkshop(AddressOf Me.DeletePublishedItemFromWorkshop_ProgressChanged, AddressOf Me.DeletePublishedItemFromWorkshop_RunWorkerCompleted, Me.theSelectedItem.ID)
	End Sub

	Private Sub GetPublishedItemDetailsViaSteamRemoteStorage(ByVal itemID As String, ByVal action As String)
		Me.AppIdComboBox.Enabled = False
		Me.ItemsDataGridView.Enabled = False
		Me.ItemGroupBox.Enabled = False
		Me.PublishItemButton.Enabled = False
		If Me.LogTextBox.Text <> "" Then
			Me.LogTextBox.AppendText("------" + vbCrLf)
		End If

		Me.theSelectedItemDetailsIsChangingViaMe = True

		Dim input As New BackgroundSteamPipe.GetPublishedFileDetailsInputInfo(itemID, Me.theSteamAppId.ToString(), action)
		Me.theBackgroundSteamPipe.GetPublishedItemDetails(AddressOf Me.GetPublishedItemDetails_ProgressChanged, AddressOf Me.GetPublishedItemDetails_RunWorkerCompleted, input)
	End Sub

	Private Sub PublishItem()
		If Me.theSelectedItem.IsTemplate Then
			Me.SaveChangedTemplateToDraft()
			Me.SelectItemInGrid(Me.ItemsDataGridView.Rows.Count - 1)
		End If

		'NOTE: Need to do this after the template-to-draft change above.
		Me.AppIdComboBox.Enabled = False
		Me.ItemsDataGridView.Enabled = False
		Me.ItemGroupBox.Enabled = False
		'Me.ItemTitleTextBox.Enabled = False
		'Me.ItemDescriptionTextBox.Enabled = False
		'Me.ItemChangeNoteTextBox.Enabled = False
		'Me.ItemContentPathFileNameTextBox.Enabled = False
		'Me.ItemPreviewImagePathFileNameTextBox.Enabled = False
		'Me.SaveAsTemplateOrDraftItemButton.Enabled = False
		'Me.RefreshOrRevertItemButton.Enabled = False
		'Me.OpenWorkshopPageButton.Enabled = True
		'Me.DeleteItemButton.Enabled = False
		'Me.ItemTagsGroupBox.Enabled = False
		Me.PublishItemButton.Enabled = False
		If Me.LogTextBox.Text <> "" Then
			Me.LogTextBox.AppendText("------" + vbCrLf)
		End If

		Dim prePublishChecksAreSuccessful As Boolean = True
		If Me.theSelectedItem.ContentPathFolderOrFileNameIsChanged Then
			If Me.theSteamAppInfo.CanUseContentFolderOrFile Then
				If Not Directory.Exists(Me.theSelectedItem.ContentPathFolderOrFileName) AndAlso Not File.Exists(Me.theSelectedItem.ContentPathFolderOrFileName) Then
					Me.LogTextBox.AppendText("ERROR: Item content folder or file does not exist." + vbCrLf)
					prePublishChecksAreSuccessful = False
				End If
			ElseIf Me.theSteamAppInfo.UsesSteamUGC Then
				If Not Directory.Exists(Me.theSelectedItem.ContentPathFolderOrFileName) Then
					Me.LogTextBox.AppendText("ERROR: Item content folder does not exist." + vbCrLf)
					prePublishChecksAreSuccessful = False
				End If
			Else
				If Not File.Exists(Me.theSelectedItem.ContentPathFolderOrFileName) Then
					Me.LogTextBox.AppendText("ERROR: Item content file does not exist." + vbCrLf)
					prePublishChecksAreSuccessful = False
				End If
			End If
		End If
		If Me.theSelectedItem.PreviewImagePathFileNameIsChanged Then
			If Not File.Exists(Me.theSelectedItem.PreviewImagePathFileName) Then
				Me.LogTextBox.AppendText("ERROR: Item preview image file does not exist." + vbCrLf)
				prePublishChecksAreSuccessful = False
			End If
		End If
		If Not prePublishChecksAreSuccessful Then
			'Me.UpdateItemDetailWidgets()
			Me.UpdateWidgetsAfterPublish()
			Exit Sub
		End If

		Me.theSelectedItemDetailsIsChangingViaMe = True

		Dim inputInfo As New BackgroundSteamPipe.PublishItemInputInfo()
		inputInfo.AppInfo = Me.theSteamAppInfo
		inputInfo.Item = Me.theSelectedItem
		Me.theBackgroundSteamPipe.PublishItem(AddressOf Me.PublishItem_ProgressChanged, AddressOf Me.PublishItem_RunWorkerCompleted, inputInfo)
	End Sub

	Private Sub UpdateWidgetsAfterPublish()
		Me.AppIdComboBox.Enabled = True
		Me.ItemsDataGridView.Enabled = True
		Me.ItemTitleTextBox.Enabled = True
		Me.ItemDescriptionTextBox.Enabled = True
		Me.ItemChangeNoteTextBox.Enabled = True
		Me.ItemContentPathFileNameTextBox.Enabled = True
		Me.ItemPreviewImagePathFileNameTextBox.Enabled = True
		Me.ItemTagsGroupBox.Enabled = True
		Me.UpdateItemDetailWidgets()
	End Sub

	Private Sub OpenAgreementRequiresAcceptanceWindow()
		Dim agreementRequiresAcceptanceWindow As AgreementRequiresAcceptanceForm = New AgreementRequiresAcceptanceForm()
		If agreementRequiresAcceptanceWindow.ShowDialog() = DialogResult.OK Then
			Me.OpenSteamSubscriberAgreement()
		ElseIf agreementRequiresAcceptanceWindow.ShowDialog() = DialogResult.Ignore Then
			Me.OpenWorkshopPage()
		End If
	End Sub

	Private Sub SaveChangedTemplateToDraft()
		Me.AddDraftItem(Me.theSelectedItem)
		If Me.theSelectedItem.IsChanged Then
			Me.RevertChangedTemplate()
		End If
	End Sub

	Private Sub RevertChangedTemplate()
		Me.theSelectedItemDetailsIsChangingViaMe = True
		'NOTE: Change the item in the list (and not the Me.theSlectedItem) so that list and selected item stay synced.
		Dim selectedItemIndex As Integer = Me.theItemBindingSource.IndexOf(Me.theSelectedItem)
		Me.theDisplayedItems(selectedItemIndex) = Me.theUnchangedSelectedTemplateItem
		Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Remove(Me.theSelectedItem)
		Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Add(Me.theUnchangedSelectedTemplateItem)
		RemoveHandler Me.theSelectedItem.PropertyChanged, AddressOf Me.WorkshopItem_PropertyChanged
		Me.theSelectedItem = Me.theUnchangedSelectedTemplateItem
		AddHandler Me.theSelectedItem.PropertyChanged, AddressOf Me.WorkshopItem_PropertyChanged
		Me.theSelectedItemDetailsIsChangingViaMe = False
	End Sub

#End Region

#Region "Data"

	Protected WithEvents ItemContextMenuStrip As System.Windows.Forms.ContextMenuStrip
	Public WithEvents UseInDownloadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

	'Protected WithEvents ItemsDataGridViewContextMenuStrip As System.Windows.Forms.ContextMenuStrip
	'Public WithEvents ItemsDataGridViewUseInDownloadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

	'Protected WithEvents ItemIdLabelContextMenuStrip As System.Windows.Forms.ContextMenuStrip
	'Public WithEvents ItemIdLabelUseInDownloadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

	'Protected WithEvents ItemIdTextBoxContextMenuStrip As System.Windows.Forms.ContextMenuStrip
	'Public WithEvents ItemIdTextBoxUseInDownloadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

	Private theSteamAppUserInfo As SteamAppUserInfo

	Private theUserSteamID As ULong
	Private theSteamAppInfo As SteamAppInfoBase
	Private theSteamAppId As Steamworks.AppId_t

	'Private theTemplateItemDisplayedCount As UInteger
	'Private theTemplateItemTotalCount As UInteger
	'Private theChangedItemDisplayedCount As UInteger
	'Private theChangedItemTotalCount As UInteger
	Private theExpectedPublishedItemCount As UInteger
	Private theDisplayedItems As WorkshopItemBindingList
	Private theEntireListOfItems As WorkshopItemBindingList
	Private theSelectedGameIsStillUpdatingInterface As Boolean

	Private theTagsWidget As Base_TagsUserControl

	Private theItemBindingSource As BindingSource

	Private theSelectedItem As WorkshopItem
	Private theWorkshopPageLink As String
	Private theSelectedItemIsChangingViaMe As Boolean
	Private theSelectedItemDetailsIsChangingViaMe As Boolean
	Private theSavedPreviewImagePathFileName As String
	Private theUnchangedSelectedTemplateItem As WorkshopItem

	Private theBackgroundSteamPipe As BackgroundSteamPipe

	Private Sub ItemPreviewImagePictureBox_Resize(sender As Object, e As EventArgs) Handles ItemPreviewImagePictureBox.Resize
		' Make sure size stays a square even when theme font changes it.
		Dim width As Integer = Me.ItemPreviewImagePictureBox.Width
		Dim height As Integer = Me.ItemPreviewImagePictureBox.Height
		If width <> height Then
			Dim length As Integer = Math.Min(width, height)
			Me.ItemPreviewImagePictureBox.Size = New System.Drawing.Size(length, length)
		End If
	End Sub

#End Region

End Class
