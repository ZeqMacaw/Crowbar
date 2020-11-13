Imports System.ComponentModel

Public Class GarrysModTagsUserControl

	Public Sub New()
		MyBase.New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		Me.theOrientation = OrientationType.Vertical
		Me.ChangeOrientation()
	End Sub

	Protected Overrides Sub Init()
		MyBase.Init()

		Dim anEnumList As IList
		anEnumList = EnumHelper.ToList(GetType(GarrysModSteamAppInfo.GarrysModTypeTags))
		Me.ComboBox1.DisplayMember = "Value"
		Me.ComboBox1.ValueMember = "Key"
		Me.ComboBox1.DataSource = anEnumList
		Me.ComboBox1.SelectedValue = GarrysModSteamAppInfo.GarrysModTypeTags.ServerContent

		Me.theCheckBoxes = New List(Of CheckBoxEx)()
		Me.GetAllCheckboxes(Me.GroupBox1.Controls)
		Me.theCheckmarkedCheckBoxes = New List(Of CheckBoxEx)(2)
	End Sub

	Private Sub GetAllCheckboxes(ByVal iWidgets As ControlCollection)
		For Each widget As Control In iWidgets
			If TypeOf widget Is CheckBoxEx AndAlso widget IsNot Me.AddonTagCheckBox Then
				Dim aCheckBox As CheckBoxEx = CType(widget, CheckBoxEx)
				Me.theCheckBoxes.Add(aCheckBox)
			End If
		Next
	End Sub

	Public Property Orientation() As OrientationType
		Get
			Return Me.theOrientation
		End Get
		Set
			Me.theOrientation = Value
			Me.ChangeOrientation()
		End Set
	End Property

	<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	Public Overrides Property ItemTags As BindingListEx(Of String)
		Get
			'Dim tags As BindingListEx(Of String) = MyBase.ItemTags
			'If Not tags.Contains("Addon") Then
			'	tags.Add("Addon")
			'	Me.AddonTagCheckBox.Checked = True
			'	MyBase.RaiseTagsPropertyChanged()
			'End If
			'Return tags
			Return MyBase.ItemTags
		End Get
		Set
			MyBase.ItemTags = Value

			Dim tags As BindingListEx(Of String) = MyBase.ItemTags
			If Not tags.Contains("Addon") Then
				tags.Add("Addon")
				Me.AddonTagCheckBox.Checked = True
				MyBase.RaiseTagsPropertyChanged()
			End If

			Me.theCheckmarkedCheckBoxes.Clear()
			For Each selectedCheckBox As CheckBoxEx In Me.theCheckBoxes
				If selectedCheckBox.Checked Then
					If Me.theCheckmarkedCheckBoxes.Count < 2 Then
						Me.theCheckmarkedCheckBoxes.Add(selectedCheckBox)
					End If
					If Me.theCheckmarkedCheckBoxes.Count = 2 Then
						For Each aCheckBox As CheckBoxEx In Me.theCheckBoxes
							aCheckBox.Enabled = aCheckBox.Checked
						Next
						Exit For
					End If
				End If
			Next
			If Me.theCheckmarkedCheckBoxes.Count < 2 Then
				For Each aCheckBox As CheckBoxEx In Me.theCheckBoxes
					aCheckBox.Enabled = True
				Next
			End If
		End Set
	End Property

	Protected Overrides Sub OnCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		If Not Me.theCheckBoxesAreChangingViaMe Then
			Dim selectedCheckBox As CheckBoxEx = CType(sender, CheckBoxEx)
			If selectedCheckBox.Checked Then
				If Me.theCheckmarkedCheckBoxes.Count < 2 Then
					Me.theCheckmarkedCheckBoxes.Add(selectedCheckBox)
				End If
				If Me.theCheckmarkedCheckBoxes.Count = 2 Then
					For Each aCheckBox As CheckBoxEx In Me.theCheckBoxes
						aCheckBox.Enabled = aCheckBox.Checked
					Next
				End If
			Else
				Me.theCheckmarkedCheckBoxes.Remove(selectedCheckBox)
				For Each aCheckBox As CheckBoxEx In Me.theCheckBoxes
					aCheckBox.Enabled = True
				Next
			End If
		End If

		MyBase.OnCheckBox_CheckedChanged(sender, e)
	End Sub

	Private Sub ChangeOrientation()
		If Me.theOrientation = OrientationType.Horizontal Then
			''Build
			'Me.CheckBox1.Location = New System.Drawing.Point(6, 20)
			''Cartoon
			'Me.CheckBox2.Location = New System.Drawing.Point(6, 43)
			'Comic
			Me.CheckBox3.Location = New System.Drawing.Point(78, 20)
			'Fun
			Me.CheckBox4.Location = New System.Drawing.Point(78, 43)
			'Movie
			Me.CheckBox5.Location = New System.Drawing.Point(150, 20)
			'Realism
			Me.CheckBox6.Location = New System.Drawing.Point(150, 43)
			'Roleplay
			Me.CheckBox7.Location = New System.Drawing.Point(222, 20)
			'Scenic
			Me.CheckBox8.Location = New System.Drawing.Point(222, 43)
			'Water
			Me.CheckBox9.Location = New System.Drawing.Point(294, 20)
			'GroupBox1
			Me.GroupBox1.Size = New System.Drawing.Size(356, 68)
		Else
			''Build
			'Me.CheckBox1.Location = New System.Drawing.Point(6, 20)
			''Cartoon
			'Me.CheckBox2.Location = New System.Drawing.Point(6, 43)
			'Comic
			Me.CheckBox3.Location = New System.Drawing.Point(6, 66)
			'Fun
			Me.CheckBox4.Location = New System.Drawing.Point(6, 89)
			'Movie
			Me.CheckBox5.Location = New System.Drawing.Point(6, 112)
			'Realism
			Me.CheckBox6.Location = New System.Drawing.Point(6, 135)
			'Roleplay
			Me.CheckBox7.Location = New System.Drawing.Point(6, 158)
			'Scenic
			Me.CheckBox8.Location = New System.Drawing.Point(6, 181)
			'Water
			Me.CheckBox9.Location = New System.Drawing.Point(6, 204)
			'GroupBox1
			Me.GroupBox1.Size = New System.Drawing.Size(161, 235)
		End If
	End Sub

	Private theCheckBoxes As List(Of CheckBoxEx)
	Private theCheckmarkedCheckBoxes As List(Of CheckBoxEx)
	Private theOrientation As OrientationType

	' From Garry's Mod web page "Workshop Addon Creation" [ https://wiki.garrysmod.com/page/Workshop_Addon_Creation ]: 
	'type is the type of addon, one of:
	'"ServerContent"
	'"gamemode"
	'"map"
	'"weapon"
	'"vehicle"
	'"npc"
	'"tool"
	'"effects"
	'"model"
	'
	'tags is up to two of these:
	'"fun"
	'"roleplay"
	'"scenic"
	'"movie"
	'"realism"
	'"cartoon"
	'"water"
	'"comic"
	'"build"

End Class
