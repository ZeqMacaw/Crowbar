Imports System.IO
Imports System.Text

Public Class SourceQcFile31
	Inherits SourceQcFile

#Region "Creation and Destruction"

	'Public Sub New(ByVal outputFileStream As StreamWriter, ByVal outputPathFileName As String, ByVal mdlFileData As SourceMdlFileData31, ByVal phyFileData As SourcePhyFileData37, ByVal vtxFileData As SourceVtxFileData06, ByVal modelName As String)
	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal outputPathFileName As String, ByVal mdlFileData As SourceMdlFileData31, ByVal vtxFileData As SourceVtxFileData06, ByVal phyFileData As SourcePhyFileData, ByVal modelName As String)
		Me.theOutputFileStreamWriter = outputFileStream
		Me.theMdlFileData = mdlFileData
		Me.thePhyFileData = phyFileData
		Me.theVtxFileData = vtxFileData
		Me.theModelName = modelName

		Me.theOutputPath = FileManager.GetPath(outputPathFileName)
		Me.theOutputFileNameWithoutExtension = Path.GetFileNameWithoutExtension(outputPathFileName)
	End Sub

#End Region

#Region "Methods"

	Public Sub WriteGroup(ByVal qciGroupName As String, ByVal writeGroupAction As WriteGroupDelegate, ByVal includeLineIsCommented As Boolean, ByVal includeLineIsIndented As Boolean)
		If TheApp.Settings.DecompileGroupIntoQciFilesIsChecked Then
			Dim qciFileName As String
			Dim qciPathFileName As String
			Dim mainOutputFileStream As StreamWriter

			mainOutputFileStream = Me.theOutputFileStreamWriter

			Try
				'qciPathFileName = Path.Combine(Me.theOutputPathName, Me.theOutputFileNameWithoutExtension + "_flexes.qci")
				qciFileName = Me.theOutputFileNameWithoutExtension + "_" + qciGroupName + ".qci"
				qciPathFileName = Path.Combine(Me.theOutputPath, qciFileName)

				Me.theOutputFileStreamWriter = File.CreateText(qciPathFileName)

				'Me.WriteFlexLines()
				'Me.WriteFlexControllerLines()
				'Me.WriteFlexRuleLines()
				writeGroupAction.Invoke()
			Catch ex As Exception
				Throw
			Finally
				If Me.theOutputFileStreamWriter IsNot Nothing Then
					Me.theOutputFileStreamWriter.Flush()
					Me.theOutputFileStreamWriter.Close()

					Me.theOutputFileStreamWriter = mainOutputFileStream
				End If
			End Try

			Try
				If File.Exists(qciPathFileName) Then
					Dim qciFileInfo As New FileInfo(qciPathFileName)
					If qciFileInfo.Length > 0 Then
						Dim line As String = ""

						Me.theOutputFileStreamWriter.WriteLine()

						If includeLineIsCommented Then
							line += "// "
						End If
						If includeLineIsIndented Then
							line += vbTab
						End If
						If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
							line += "$Include "
						Else
							line += "$include "
						End If
						line += """"
						line += qciFileName
						line += """"
						Me.theOutputFileStreamWriter.WriteLine(line)
					End If
				End If
			Catch ex As Exception
				Throw
			End Try
		Else
			'Me.WriteFlexLines()
			'Me.WriteFlexControllerLines()
			'Me.WriteFlexRuleLines()
			writeGroupAction.Invoke()
		End If
	End Sub

	Public Sub WriteModelNameCommand()
		Dim line As String = ""
		'Dim modelPath As String
		Dim modelPathFileName As String

		'modelPath = FileManager.GetPath(CStr(theSourceEngineModel.theMdlFileHeader.name).Trim(Chr(0)))
		'modelPathFileName = Path.Combine(modelPath, theSourceEngineModel.ModelName + ".mdl")
		'modelPathFileName = CStr(theSourceEngineModel.MdlFileHeader.name).Trim(Chr(0))
		modelPathFileName = Me.theMdlFileData.theModelName

		Me.theOutputFileStreamWriter.WriteLine()

		'$modelname "survivors/survivor_producer.mdl"
		'$modelname "custom/survivor_producer.mdl"
		If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
			line = "$ModelName "
		Else
			line = "$modelname "
		End If
		line += """"
		line += modelPathFileName
		line += """"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteIncludeMainQcLine()
		Dim line As String = ""

		Me.theOutputFileStreamWriter.WriteLine()

		'$include "Rochelle_world.qci"
		If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
			line += "$Include "
		Else
			line += "$include "
		End If
		line += """"
		line += "decompiled.qci"
		line += """"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteHeaderComment()
		Common.WriteHeaderComment(Me.theOutputFileStreamWriter)
	End Sub

	Public Sub WriteStaticPropCommand()
		Dim line As String = ""

		'$staticprop
		If (Me.theMdlFileData.flags And SourceMdlFileData.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
			Me.theOutputFileStreamWriter.WriteLine()

			If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
				line = "$StaticProp"
			Else
				line = "$staticprop"
			End If
			Me.theOutputFileStreamWriter.WriteLine(line)
		End If
	End Sub

	Public Sub WriteBodyGroupCommand()
		Dim line As String = ""
		Dim aBodyPart As SourceMdlBodyPart31
		Dim aVtxBodyPart As SourceVtxBodyPart06
		Dim aBodyModel As SourceMdlModel31
		Dim aVtxModel As SourceVtxModel06

		'$bodygroup "belt"
		'{
		'//	studio "zoey_belt.smd"
		'	"blank"
		'}
		'$bodygroup "shoes"
		'{
		'//  studio "zoey_shoes.smd"
		'    studio "zoey_feet.smd"
		'}
		'FROM: VDC wiki: 
		'$bodygroup sights
		'{
		'	studio "ironsights.smd"
		'	studio "laser_dot.smd"
		'	blank
		'}
		If Me.theMdlFileData.theBodyParts IsNot Nothing AndAlso Me.theMdlFileData.theBodyParts.Count > 0 Then
			line = ""
			Me.theOutputFileStreamWriter.WriteLine(line)

			For bodyPartIndex As Integer = 0 To Me.theMdlFileData.theBodyParts.Count - 1
				'If Me.theMdlFileData.theModelCommandIsUsed AndAlso bodyPartIndex = Me.theMdlFileData.theBodyPartIndexThatShouldUseModelCommand Then
				'	Me.WriteModelCommand()
				'	Continue For
				'End If
				aBodyPart = Me.theMdlFileData.theBodyParts(bodyPartIndex)

				If Me.theVtxFileData IsNot Nothing AndAlso Me.theVtxFileData.theVtxBodyParts IsNot Nothing AndAlso Me.theVtxFileData.theVtxBodyParts.Count > 0 Then
					aVtxBodyPart = Me.theVtxFileData.theVtxBodyParts(bodyPartIndex)
				Else
					aVtxBodyPart = Nothing
				End If

				If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
					line = "$BodyGroup "
				Else
					line = "$bodygroup "
				End If
				line += """"
				line += aBodyPart.theName
				line += """"
				Me.theOutputFileStreamWriter.WriteLine(line)

				line = "{"
				Me.theOutputFileStreamWriter.WriteLine(line)

				If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 Then
					For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
						aBodyModel = aBodyPart.theModels(modelIndex)

						If aVtxBodyPart IsNot Nothing AndAlso aVtxBodyPart.theVtxModels IsNot Nothing AndAlso aVtxBodyPart.theVtxModels.Count > 0 Then
							aVtxModel = aVtxBodyPart.theVtxModels(modelIndex)
						Else
							aVtxModel = Nothing
						End If

						line = vbTab
						If aBodyModel.name(0) = ChrW(0) AndAlso (aVtxModel IsNot Nothing AndAlso aVtxModel.theVtxModelLods(0).theVtxMeshes Is Nothing) Then
							line += "blank"
						Else
							aBodyModel.theSmdFileNames(0) = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames(0), bodyPartIndex, modelIndex, 0, Me.theModelName, aBodyModel.name)
							line += "studio "
							line += """"
							line += aBodyModel.theSmdFileNames(0)
							line += """"
						End If
						Me.theOutputFileStreamWriter.WriteLine(line)
					Next
				End If

				line = "}"
				Me.theOutputFileStreamWriter.WriteLine(line)
			Next
		End If
	End Sub

	Public Sub WriteGroupLod()
		Me.WriteLodCommand()
	End Sub

	Public Sub WriteSurfacePropCommand()
		Dim line As String = ""

		If Me.theMdlFileData.theSurfacePropName <> "" Then
			line = ""
			Me.theOutputFileStreamWriter.WriteLine(line)

			'$surfaceprop "flesh"
			If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
				line = "$SurfaceProp "
			Else
				line = "$surfaceprop "
			End If
			line += """"
			line += Me.theMdlFileData.theSurfacePropName
			line += """"
			Me.theOutputFileStreamWriter.WriteLine(line)
		End If
	End Sub

	Public Sub WriteIllumPositionCommand()
		Dim line As String
		Dim offsetX As Double
		Dim offsetY As Double
		Dim offsetZ As Double

		offsetX = Math.Round(Me.theMdlFileData.illuminationPosition.y, 3)
		offsetY = -Math.Round(Me.theMdlFileData.illuminationPosition.x, 3)
		offsetZ = Math.Round(Me.theMdlFileData.illuminationPosition.z, 3)

		line = ""
		Me.theOutputFileStreamWriter.WriteLine(line)

		line = ""
		If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
			line += "$IllumPosition "
		Else
			line += "$illumposition "
		End If
		line += offsetX.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += offsetY.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += offsetZ.ToString("0.######", TheApp.InternalNumberFormat)
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteEyePositionCommand()
		Dim line As String = ""
		Dim offsetX As Double
		Dim offsetY As Double
		Dim offsetZ As Double

		offsetX = Math.Round(Me.theMdlFileData.eyePosition.y, 3)
		offsetY = -Math.Round(Me.theMdlFileData.eyePosition.x, 3)
		offsetZ = Math.Round(Me.theMdlFileData.eyePosition.z, 3)

		If offsetX = 0 AndAlso offsetY = 0 AndAlso offsetZ = 0 Then
			Exit Sub
		End If

		line = ""
		Me.theOutputFileStreamWriter.WriteLine(line)

		'$eyeposition -0.000 0.000 70.000
		'NOTE: Probably stored in different order in MDL file, based on MDL v48 source code.
		'FROM: utils\studiomdl\studiomdl.cpp Cmd_Eyeposition()
		'eyeposition[1] = verify_atof (token);
		'eyeposition[0] = -verify_atof (token);
		'eyeposition[2] = verify_atof (token);
		If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
			line = "$EyePosition "
		Else
			line = "$eyeposition "
		End If
		line += offsetX.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += offsetY.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += offsetZ.ToString("0.######", TheApp.InternalNumberFormat)
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteCdMaterialsCommand()
		Dim line As String = ""

		'$cdmaterials "models\survivors\producer\"
		'$cdmaterials "models\survivors\"
		'$cdmaterials ""
		If Me.theMdlFileData.theTexturePaths IsNot Nothing Then
			line = ""
			Me.theOutputFileStreamWriter.WriteLine(line)

			For i As Integer = 0 To Me.theMdlFileData.theTexturePaths.Count - 1
				Dim aTexturePath As String
				aTexturePath = Me.theMdlFileData.theTexturePaths(i)
				'NOTE: Write out null or empty strings, because Crowbar should show what was stored.
				'If Not String.IsNullOrEmpty(aTexturePath) Then
				If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
					line = "$CDMaterials "
				Else
					line = "$cdmaterials "
				End If
				line += """"
				line += aTexturePath
				line += """"
				Me.theOutputFileStreamWriter.WriteLine(line)
				'End If
			Next
		End If
	End Sub

	Public Sub WriteTextureGroupCommand()
		Dim line As String = ""

		If Me.theMdlFileData.theSkinFamilies IsNot Nothing AndAlso Me.theMdlFileData.theSkinFamilies.Count > 0 AndAlso Me.theMdlFileData.theTextures IsNot Nothing AndAlso Me.theMdlFileData.theTextures.Count > 0 AndAlso Me.theMdlFileData.skinReferenceCount > 0 Then
			Dim processedSkinFamilies As List(Of List(Of Short))
			If TheApp.Settings.DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked Then
				processedSkinFamilies = Me.GetSkinFamiliesOfChangedMaterials(Me.theMdlFileData.theSkinFamilies)
			Else
				processedSkinFamilies = Me.theMdlFileData.theSkinFamilies
			End If

			Dim skinFamiliesOfTextureFileNames As List(Of List(Of String))
			skinFamiliesOfTextureFileNames = New List(Of List(Of String))(processedSkinFamilies.Count)
			Dim skinReferenceCount As Integer
			skinReferenceCount = processedSkinFamilies(0).Count
			For i As Integer = 0 To processedSkinFamilies.Count - 1
				Dim aSkinFamily As List(Of Short)
				aSkinFamily = processedSkinFamilies(i)

				Dim textureFileNames As New List(Of String)(skinReferenceCount)
				For j As Integer = 0 To skinReferenceCount - 1
					Dim aTexture As SourceMdlTexture31
					aTexture = Me.theMdlFileData.theTextures(aSkinFamily(j))

					textureFileNames.Add(aTexture.thePathFileName)
				Next

				skinFamiliesOfTextureFileNames.Add(textureFileNames)
			Next

			If (Not TheApp.Settings.DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked) OrElse (skinFamiliesOfTextureFileNames.Count > 1) Then
				Me.theOutputFileStreamWriter.WriteLine()

				If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
					line = "$TextureGroup ""skinfamilies"""
				Else
					line = "$texturegroup ""skinfamilies"""
				End If
				Me.theOutputFileStreamWriter.WriteLine(line)
				line = "{"
				Me.theOutputFileStreamWriter.WriteLine(line)

				Dim skinFamilyLines As List(Of String)
				skinFamilyLines = Me.GetTextureGroupSkinFamilyLines(skinFamiliesOfTextureFileNames)
				For skinFamilyLineIndex As Integer = 0 To skinFamilyLines.Count - 1
					Me.theOutputFileStreamWriter.WriteLine(skinFamilyLines(skinFamilyLineIndex))
				Next

				line = "}"
				Me.theOutputFileStreamWriter.WriteLine(line)
			End If
		End If
	End Sub

	Public Sub WriteTextureFileNameComments()
		'// Model uses material "producer_head.vmt"
		'// Model uses material "producer_body.vmt"
		'// Model uses material "producer_head_it.vmt"
		'// Model uses material "producer_body_it.vmt"
		'// Model uses material "models/survivors/producer/producer_hair.vmt"
		'// Model uses material "models/survivors/producer/producer_eyeball_l.vmt"
		'// Model uses material "models/survivors/producer/producer_eyeball_r.vmt"
		If Me.theMdlFileData.theTextures IsNot Nothing Then
			Dim line As String

			line = ""
			Me.theOutputFileStreamWriter.WriteLine(line)

			line = "// This list shows the VMT file names used in the SMD files."
			Me.theOutputFileStreamWriter.WriteLine(line)

			For j As Integer = 0 To Me.theMdlFileData.theTextures.Count - 1
				Dim aTexture As SourceMdlTexture31
				aTexture = Me.theMdlFileData.theTextures(j)
				line = "// """
				line += aTexture.thePathFileName
				line += ".vmt"""
				Me.theOutputFileStreamWriter.WriteLine(line)
			Next
		End If
	End Sub

	Public Sub WriteGroupBox()
		Me.WriteCBoxCommand()
		Me.WriteBBoxCommand()
		If Me.theMdlFileData.version >= 27 AndAlso Me.theMdlFileData.version <= 30 Then
			Me.WriteHBoxRelatedCommands_MDL27to30()
		Else
			Me.WriteHBoxRelatedCommands()
		End If
	End Sub

	Public Sub WriteGroupAnimation()
		'NOTE: Must write $animation lines before $sequence lines that use them.
		Try
			Me.WriteAnimationOrDeclareAnimationCommand()
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
		Try
			Me.WriteSequenceOrDeclareSequenceCommand()
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Public Sub WriteGroupCollision()
		Me.WriteCollisionModelOrCollisionJointsCommand()
		'Me.WriteCollisionTextCommand()
	End Sub

#End Region

#Region "Private Delegates"

	Public Delegate Sub WriteGroupDelegate()

#End Region

#Region "Private Methods"

	Private Sub WriteLodCommand()
		Dim line As String = ""

		'NOTE: Data is from VTX file.
		'$lod 10
		' {
		'  replacemodel "producer_model_merged.dmx" "lod1_producer_model_merged.dmx"
		'}
		'$lod 15
		' {
		'  replacemodel "producer_model_merged.dmx" "lod2_producer_model_merged.dmx"
		'}
		'$lod 40
		' {
		'  replacemodel "producer_model_merged.dmx" "lod3_producer_model_merged.dmx"
		'}
		If Me.theVtxFileData IsNot Nothing AndAlso Me.theMdlFileData.theBodyParts IsNot Nothing Then
			If Me.theVtxFileData.theVtxBodyParts Is Nothing Then
				Return
			End If
			If Me.theVtxFileData.theVtxBodyParts(0).theVtxModels Is Nothing Then
				Return
			End If
			If Me.theVtxFileData.theVtxBodyParts(0).theVtxModels(0).theVtxModelLods Is Nothing Then
				Return
			End If
			If Me.theVtxFileData.lodCount <= 1 Then
				Return
			End If

			Dim aBodyPart As SourceVtxBodyPart06
			Dim aVtxModel As SourceVtxModel06
			Dim aBodyModel As SourceMdlModel31
			Dim lodIndex As Integer
			Dim aLodQcInfo As LodQcInfo
			Dim aLodQcInfoList As List(Of LodQcInfo)
			Dim aLodList As SortedList(Of Double, List(Of LodQcInfo))
			Dim aLodListOfFacialFlags As SortedList(Of Double, Boolean)
			Dim switchPoint As Double

			aLodList = New SortedList(Of Double, List(Of LodQcInfo))()
			aLodListOfFacialFlags = New SortedList(Of Double, Boolean)()
			For bodyPartIndex As Integer = 0 To Me.theVtxFileData.theVtxBodyParts.Count - 1
				aBodyPart = Me.theVtxFileData.theVtxBodyParts(bodyPartIndex)

				If aBodyPart.theVtxModels IsNot Nothing Then
					For modelIndex As Integer = 0 To aBodyPart.theVtxModels.Count - 1
						aVtxModel = aBodyPart.theVtxModels(modelIndex)

						If aVtxModel.theVtxModelLods IsNot Nothing Then
							aBodyModel = Me.theMdlFileData.theBodyParts(bodyPartIndex).theModels(modelIndex)
							'NOTE: This check is for skipping "blank" bodygroup. Example: the third bodygroup of L4D2's "infected/common_female_tshirt_skirt.mdl".
							If aBodyModel.name(0) = ChrW(0) AndAlso aVtxModel.theVtxModelLods(0).theVtxMeshes Is Nothing Then
								Continue For
							End If

							'NOTE: Start loop at 1 to skip first LOD, which isn't needed for the $lod command.
							For lodIndex = 1 To Me.theVtxFileData.lodCount - 1
								'TODO: Why would this count be different than the file header count?
								If lodIndex >= aVtxModel.theVtxModelLods.Count Then
									Exit For
								End If

								switchPoint = aVtxModel.theVtxModelLods(lodIndex).switchPoint
								If Not aLodList.ContainsKey(switchPoint) Then
									aLodQcInfoList = New List(Of LodQcInfo)()
									aLodList.Add(switchPoint, aLodQcInfoList)
									aLodListOfFacialFlags.Add(switchPoint, aVtxModel.theVtxModelLods(lodIndex).theVtxModelLodUsesFacial)
								Else
									aLodQcInfoList = aLodList(switchPoint)
								End If

								aBodyModel.theSmdFileNames(0) = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames(0), bodyPartIndex, modelIndex, 0, Me.theModelName, Me.theMdlFileData.theBodyParts(bodyPartIndex).theModels(modelIndex).name)
								aBodyModel.theSmdFileNames(lodIndex) = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames(lodIndex), bodyPartIndex, modelIndex, lodIndex, Me.theModelName, Me.theMdlFileData.theBodyParts(bodyPartIndex).theModels(modelIndex).name)
								aLodQcInfo = New LodQcInfo()
								aLodQcInfo.referenceFileName = aBodyModel.theSmdFileNames(0)
								aLodQcInfo.lodFileName = aBodyModel.theSmdFileNames(lodIndex)
								aLodQcInfoList.Add(aLodQcInfo)
							Next
						End If
					Next
				End If
			Next

			line = ""
			Me.theOutputFileStreamWriter.WriteLine(line)

			Dim lodQcInfoListOfShadowLod As List(Of LodQcInfo)
			lodQcInfoListOfShadowLod = Nothing

			lodIndex = 0
			For lodListIndex As Integer = 0 To aLodList.Count - 1
				switchPoint = aLodList.Keys(lodListIndex)
				If switchPoint = -1 Then
					' Skip writing $shadowlod. Write it last after this loop.
					lodQcInfoListOfShadowLod = aLodList.Values(lodListIndex)
					Continue For
				End If
				lodIndex += 1

				aLodQcInfoList = aLodList.Values(lodListIndex)

				If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
					line = "$LOD "
				Else
					line = "$lod "
				End If
				line += switchPoint.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)

				line = "{"
				Me.theOutputFileStreamWriter.WriteLine(line)
				Me.WriteLodOptions(lodIndex, aLodListOfFacialFlags.Values(lodListIndex), aLodQcInfoList)
				line = "}"
				Me.theOutputFileStreamWriter.WriteLine(line)
			Next

			'NOTE: As a requirement for the compiler, write $shadowlod last.
			lodIndex = aLodList.Count - 1
			If lodQcInfoListOfShadowLod IsNot Nothing Then
				'// Shadow lod reserves -1 as switch value
				'// which uniquely identifies a shadow lod
				'newLOD.switchValue = -1.0f;
				If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
					line = "$ShadowLOD"
				Else
					line = "$shadowlod"
				End If
				Me.theOutputFileStreamWriter.WriteLine(line)

				line = "{"
				Me.theOutputFileStreamWriter.WriteLine(line)
				Me.WriteLodOptions(lodIndex, False, lodQcInfoListOfShadowLod)
				line = "}"
				Me.theOutputFileStreamWriter.WriteLine(line)
			End If
		End If
	End Sub

	Private Sub WriteLodOptions(ByVal lodIndex As Integer, ByVal lodUsesFacial As Boolean, ByVal aLodQcInfoList As List(Of LodQcInfo))
		Dim line As String = ""
		Dim aLodQcInfo As LodQcInfo

		For i As Integer = 0 To aLodQcInfoList.Count - 1
			aLodQcInfo = aLodQcInfoList(i)

			If aLodQcInfo.lodFileName = "" Then
				line = vbTab
				line += "removemodel "
				line += """"
				line += aLodQcInfo.referenceFileName
				line += """"
			Else
				line = vbTab
				line += "replacemodel "
				line += """"
				line += aLodQcInfo.referenceFileName
				line += """ """
				line += aLodQcInfo.lodFileName
				line += """"
			End If

			Me.theOutputFileStreamWriter.WriteLine(line)
		Next

		For Each aBone As SourceMdlBone31 In Me.theMdlFileData.theBones
			If aBone.parentBoneIndex >= 0 _
				AndAlso ((lodIndex = 1 AndAlso (aBone.flags And SourceMdlBone.BONE_USED_BY_VERTEX_LOD1) = 0) _
				OrElse (lodIndex = 2 AndAlso (aBone.flags And SourceMdlBone.BONE_USED_BY_VERTEX_LOD2) = 0) _
				OrElse (lodIndex = 3 AndAlso (aBone.flags And SourceMdlBone.BONE_USED_BY_VERTEX_LOD3) = 0) _
				OrElse (lodIndex = 4 AndAlso (aBone.flags And SourceMdlBone.BONE_USED_BY_VERTEX_LOD4) = 0) _
				OrElse (lodIndex = 5 AndAlso (aBone.flags And SourceMdlBone.BONE_USED_BY_VERTEX_LOD5) = 0) _
				OrElse (lodIndex = 6 AndAlso (aBone.flags And SourceMdlBone.BONE_USED_BY_VERTEX_LOD6) = 0) _
				OrElse (lodIndex = 7 AndAlso (aBone.flags And SourceMdlBone.BONE_USED_BY_VERTEX_LOD7) = 0)) Then
				'replacebone "ValveBiped.Bip01_Neck1" "ValveBiped.Bip01_Head1"
				line = vbTab
				line += "replacebone "
				line += """"
				line += aBone.theName
				line += """ """
				line += Me.theMdlFileData.theBones(aBone.parentBoneIndex).theName
				line += """"
				Me.theOutputFileStreamWriter.WriteLine(line)
			End If
		Next

		line = vbTab
		If lodUsesFacial Then
			If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
				line += "Facial"
			Else
				line += "facial"
			End If
		Else
			If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
				line += "NoFacial"
			Else
				line += "nofacial"
			End If
		End If
		Me.theOutputFileStreamWriter.WriteLine(line)

		If (Me.theMdlFileData.flags And SourceMdlFileData.STUDIOHDR_FLAGS_USE_SHADOWLOD_MATERIALS) > 0 Then
			Me.theOutputFileStreamWriter.WriteLine()

			If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
				line = "Use_ShadowLOD_Materials"
			Else
				line = "use_shadowlod_materials"
			End If
			Me.theOutputFileStreamWriter.WriteLine(line)
		End If
	End Sub

	Private Sub WriteCBoxCommand()
		Dim line As String = ""
		Dim minX As Double
		Dim minY As Double
		Dim minZ As Double
		Dim maxX As Double
		Dim maxY As Double
		Dim maxZ As Double

		line = ""
		Me.theOutputFileStreamWriter.WriteLine(line)

		If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
			line = "// Clipping box or view bounding box."
			Me.theOutputFileStreamWriter.WriteLine(line)
		End If

		'FROM: VDC wiki: 
		'$cbox <float|minx> <float|miny> <float|minz> <float|maxx> <float|maxy> <float|maxz> 
		minX = Math.Round(Me.theMdlFileData.viewBoundingBoxMinPosition.x, 3)
		minY = Math.Round(Me.theMdlFileData.viewBoundingBoxMinPosition.y, 3)
		minZ = Math.Round(Me.theMdlFileData.viewBoundingBoxMinPosition.z, 3)
		maxX = Math.Round(Me.theMdlFileData.viewBoundingBoxMaxPosition.x, 3)
		maxY = Math.Round(Me.theMdlFileData.viewBoundingBoxMaxPosition.y, 3)
		maxZ = Math.Round(Me.theMdlFileData.viewBoundingBoxMaxPosition.z, 3)
		If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
			line = "$CBox "
		Else
			line = "$cbox "
		End If
		line += minX.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += minY.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += minZ.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += maxX.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += maxY.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += maxZ.ToString("0.######", TheApp.InternalNumberFormat)
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Private Sub WriteBBoxCommand()
		Dim line As String = ""
		Dim minX As Double
		Dim minY As Double
		Dim minZ As Double
		Dim maxX As Double
		Dim maxY As Double
		Dim maxZ As Double

		line = ""
		Me.theOutputFileStreamWriter.WriteLine(line)

		If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
			line = "// Bounding box or hull. Used for collision with a world object."
			Me.theOutputFileStreamWriter.WriteLine(line)
		End If

		'$bbox -16.0 -16.0 -13.0 16.0 16.0 75.0
		'FROM: VDC wiki: 
		'$bbox (min x) (min y) (min z) (max x) (max y) (max z)
		minX = Math.Round(Me.theMdlFileData.hullMinPosition.x, 3)
		minY = Math.Round(Me.theMdlFileData.hullMinPosition.y, 3)
		minZ = Math.Round(Me.theMdlFileData.hullMinPosition.z, 3)
		maxX = Math.Round(Me.theMdlFileData.hullMaxPosition.x, 3)
		maxY = Math.Round(Me.theMdlFileData.hullMaxPosition.y, 3)
		maxZ = Math.Round(Me.theMdlFileData.hullMaxPosition.z, 3)
		line = ""
		If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
			line += "$BBox "
		Else
			line += "$bbox "
		End If
		line += minX.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += minY.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += minZ.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += maxX.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += maxY.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += maxZ.ToString("0.######", TheApp.InternalNumberFormat)
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Private Sub WriteHBoxRelatedCommands_MDL27to30()
		Dim line As String = ""
		Dim commentTag As String = ""
		Dim hitBoxWasAutoGenerated As Boolean = False
		Dim skipBoneInBBoxCommandWasUsed As Boolean = False

		If Me.theMdlFileData.theHitboxes_MDL27to30 Is Nothing OrElse Me.theMdlFileData.theHitboxes_MDL27to30.Count < 1 Then
			Exit Sub
		End If

		hitBoxWasAutoGenerated = (Me.theMdlFileData.flags And SourceMdlFileData.STUDIOHDR_FLAGS_AUTOGENERATED_HITBOX) > 0
		If hitBoxWasAutoGenerated AndAlso Not TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
			Exit Sub
		End If

		Me.theOutputFileStreamWriter.WriteLine()

		If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
			line = "// Hitbox info. Used for damage-based collision."
			Me.theOutputFileStreamWriter.WriteLine(line)
		End If

		If hitBoxWasAutoGenerated Then
			line = "// The hitbox info below was automatically generated when compiled because no hitbox info was provided."
			Me.theOutputFileStreamWriter.WriteLine(line)

			'NOTE: Only comment-out the hbox lines if auto-generated.
			commentTag = "// "
		End If

		Me.WriteHBoxCommands_MDL27to30(Me.theMdlFileData.theHitboxes_MDL27to30, commentTag, skipBoneInBBoxCommandWasUsed)

		If skipBoneInBBoxCommandWasUsed Then
			If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
				line = "$SkipBoneInBBox"
			Else
				line = "$skipboneinbbox"
			End If
			Me.theOutputFileStreamWriter.WriteLine(commentTag + line)
		End If
	End Sub

	Private Sub WriteHBoxCommands_MDL27to30(ByVal theHitboxes As List(Of SourceMdlHitbox31), ByVal commentTag As String, ByRef theSkipBoneInBBoxCommandWasUsed As Boolean)
		Dim line As String = ""
		Dim aHitbox As SourceMdlHitbox31

		For j As Integer = 0 To theHitboxes.Count - 1
			aHitbox = theHitboxes(j)
			If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
				line = "$HBox "
			Else
				line = "$hbox "
			End If
			line += aHitbox.groupIndex.ToString(TheApp.InternalNumberFormat)
			line += " "
			line += """"
			line += Me.theMdlFileData.theBones(aHitbox.boneIndex).theName
			line += """"
			line += " "
			line += aHitbox.boundingBoxMin.x.ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += aHitbox.boundingBoxMin.y.ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += aHitbox.boundingBoxMin.z.ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += aHitbox.boundingBoxMax.x.ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += aHitbox.boundingBoxMax.y.ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += aHitbox.boundingBoxMax.z.ToString("0.######", TheApp.InternalNumberFormat)
			Me.theOutputFileStreamWriter.WriteLine(commentTag + line)

			If Not theSkipBoneInBBoxCommandWasUsed Then
				If aHitbox.boundingBoxMin.x > 0 _
				 OrElse aHitbox.boundingBoxMin.y > 0 _
				 OrElse aHitbox.boundingBoxMin.z > 0 _
				 OrElse aHitbox.boundingBoxMax.x < 0 _
				 OrElse aHitbox.boundingBoxMax.y < 0 _
				 OrElse aHitbox.boundingBoxMax.z < 0 _
				 Then
					theSkipBoneInBBoxCommandWasUsed = True
				End If
			End If
		Next
	End Sub

	Private Sub WriteHBoxRelatedCommands()
		Dim line As String = ""
		Dim commentTag As String = ""
		Dim hitBoxWasAutoGenerated As Boolean = False
		Dim skipBoneInBBoxCommandWasUsed As Boolean = False

		If Me.theMdlFileData.theHitboxSets Is Nothing OrElse Me.theMdlFileData.theHitboxSets.Count < 1 Then
			Exit Sub
		End If

		hitBoxWasAutoGenerated = (Me.theMdlFileData.flags And SourceMdlFileData.STUDIOHDR_FLAGS_AUTOGENERATED_HITBOX) > 0
		If hitBoxWasAutoGenerated AndAlso Not TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
			Exit Sub
		End If

		Me.theOutputFileStreamWriter.WriteLine()

		If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
			line = "// Hitbox info. Used for damage-based collision."
			Me.theOutputFileStreamWriter.WriteLine(line)
		End If

		If hitBoxWasAutoGenerated Then
			line = "// The hitbox info below was automatically generated when compiled because no hitbox info was provided."
			Me.theOutputFileStreamWriter.WriteLine(line)

			'NOTE: Only comment-out the hbox lines if auto-generated.
			commentTag = "// "
		End If

		'FROM: HLMV for survivor_producer: 
		'$hboxset "L4D"
		'$hbox 3 "ValveBiped.Bip01_Pelvis"	    -5.33   -4.00   -4.00     5.33    4.00    4.00
		'$hbox 6 "ValveBiped.Bip01_L_Thigh"	     4.44   -3.02   -2.53    16.87    2.31    1.91
		'$hbox 6 "ValveBiped.Bip01_L_Calf"	     0.44   -1.78   -2.22    17.32    2.66    2.22
		'$hbox 6 "ValveBiped.Bip01_L_Toe0"	    -3.11   -0.44   -1.20     1.33    1.33    2.18
		'$hbox 7 "ValveBiped.Bip01_R_Thigh"	     4.44   -3.02   -2.53    16.87    2.31    1.91
		'$hbox 7 "ValveBiped.Bip01_R_Calf"	     0.44   -1.78   -2.22    17.32    2.66    2.22
		'$hbox 7 "ValveBiped.Bip01_R_Toe0"	    -3.11   -0.44   -1.20     1.33    1.33    2.18
		'$hbox 3 "ValveBiped.Bip01_Spine1"	    -4.44   -3.77   -5.33     4.44    5.55    5.33
		'$hbox 2 "ValveBiped.Bip01_Spine2"	    -2.66   -3.02   -5.77    10.66    5.86    5.77
		'$hbox 1 "ValveBiped.Bip01_Neck1"	     0.00   -2.22   -2.00     3.55    2.22    2.00
		'$hbox 1 "ValveBiped.Bip01_Head1"	    -0.71   -3.55   -2.71     6.39    3.55    2.18
		'$hbox 4 "ValveBiped.Bip01_L_UpperArm"	     0.00   -1.86   -1.78     9.77    1.69    1.78
		'$hbox 4 "ValveBiped.Bip01_L_Forearm"	     0.44   -1.55   -1.55    10.21    1.55    1.55
		'$hbox 4 "ValveBiped.Bip01_L_Hand"	     0.94   -1.28   -2.13     4.94    0.50    1.15
		'$hbox 5 "ValveBiped.Bip01_R_UpperArm"	     0.00   -1.86   -1.78     9.77    1.69    1.78
		'$hbox 5 "ValveBiped.Bip01_R_Forearm"	     0.44   -1.55   -1.55    10.21    1.55    1.55
		'$hbox 5 "ValveBiped.Bip01_R_Hand"	     0.94   -1.28   -2.13     4.94    0.50    1.15

		Dim aHitboxSet As SourceMdlHitboxSet31
		For i As Integer = 0 To Me.theMdlFileData.theHitboxSets.Count - 1
			aHitboxSet = Me.theMdlFileData.theHitboxSets(i)

			If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
				line = "$HBoxSet "
			Else
				line = "$hboxset "
			End If
			line += """"
			line += aHitboxSet.theName
			line += """"
			Me.theOutputFileStreamWriter.WriteLine(commentTag + line)

			If aHitboxSet.theHitboxes Is Nothing Then
				Continue For
			End If

			Me.WriteHBoxCommands(aHitboxSet.theHitboxes, commentTag, aHitboxSet.theName, skipBoneInBBoxCommandWasUsed)
		Next

		If skipBoneInBBoxCommandWasUsed Then
			If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
				line = "$SkipBoneInBBox"
			Else
				line = "$skipboneinbbox"
			End If
			Me.theOutputFileStreamWriter.WriteLine(commentTag + line)
		End If
	End Sub

	Private Sub WriteHBoxCommands(ByVal theHitboxes As List(Of SourceMdlHitbox31), ByVal commentTag As String, ByVal hitboxSetName As String, ByRef theSkipBoneInBBoxCommandWasUsed As Boolean)
		Dim line As String = ""
		Dim aHitbox As SourceMdlHitbox31

		For j As Integer = 0 To theHitboxes.Count - 1
			aHitbox = theHitboxes(j)
			If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
				line = "$HBox "
			Else
				line = "$hbox "
			End If
			line += aHitbox.groupIndex.ToString(TheApp.InternalNumberFormat)
			line += " "
			line += """"
			line += Me.theMdlFileData.theBones(aHitbox.boneIndex).theName
			line += """"
			line += " "
			line += aHitbox.boundingBoxMin.x.ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += aHitbox.boundingBoxMin.y.ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += aHitbox.boundingBoxMin.z.ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += aHitbox.boundingBoxMax.x.ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += aHitbox.boundingBoxMax.y.ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += aHitbox.boundingBoxMax.z.ToString("0.######", TheApp.InternalNumberFormat)
			Me.theOutputFileStreamWriter.WriteLine(commentTag + line)

			If Not theSkipBoneInBBoxCommandWasUsed Then
				If aHitbox.boundingBoxMin.x > 0 _
				 OrElse aHitbox.boundingBoxMin.y > 0 _
				 OrElse aHitbox.boundingBoxMin.z > 0 _
				 OrElse aHitbox.boundingBoxMax.x < 0 _
				 OrElse aHitbox.boundingBoxMax.y < 0 _
				 OrElse aHitbox.boundingBoxMax.z < 0 _
				 Then
					theSkipBoneInBBoxCommandWasUsed = True
				End If
			End If
		Next
	End Sub

	Private Sub WriteAnimationOrDeclareAnimationCommand()
		If Me.theMdlFileData.theAnimationDescs IsNot Nothing Then
			For i As Integer = 0 To Me.theMdlFileData.theAnimationDescs.Count - 1
				Dim anAnimationDesc As SourceMdlAnimationDesc31
				anAnimationDesc = Me.theMdlFileData.theAnimationDescs(i)

				If anAnimationDesc.theName(0) <> "@" Then
					Me.WriteAnimationLine(anAnimationDesc)
				End If
			Next
		End If
	End Sub

	Private Sub WriteAnimationLine(ByVal anAnimationDesc As SourceMdlAnimationDesc31)
		Dim line As String = ""

		Me.theOutputFileStreamWriter.WriteLine()

		If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_OVERRIDE) > 0 Then
			If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
				line = "$DeclareAnimation"
			Else
				line = "$declareanimation"
			End If
			line += " """
			'TODO: Does this need to check and remove initial "@" from name?
			line += anAnimationDesc.theName
			line += """"
			Me.theOutputFileStreamWriter.WriteLine(line)
		Else
			'$animation a_reference "primary_idle.dmx" lx ly
			'NOTE: The $Animation command must have name first and file name second and on same line as the command.
			If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
				line = "$Animation"
			Else
				line = "$animation"
			End If
			anAnimationDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(anAnimationDesc.theSmdRelativePathFileName, Me.theModelName, anAnimationDesc.theName)
			line += " """
			line += anAnimationDesc.theName
			line += """ """
			line += anAnimationDesc.theSmdRelativePathFileName
			line += """"
			'NOTE: Opening brace must be on same line as the command.
			line += " {"
			Me.theOutputFileStreamWriter.WriteLine(line)

			Me.WriteAnimationOptions(Nothing, anAnimationDesc, Nothing)

			line = "}"
			Me.theOutputFileStreamWriter.WriteLine(line)
		End If
	End Sub

	'angles
	'autoik
	'blockname
	'cmdlist
	'fps                // done
	'frame              // baked-in and decompiles as separate anim smd files
	'fudgeloop
	'if
	'loop               // done
	'motionrollback
	'noanimblock
	'noanimblockstall
	'noautoik
	'origin
	'post               // baked-in 
	'rotate
	'scale
	'snap               // ($sequence version handled elsewhere)
	'startloop
	'ParseCmdlistToken( panim->numcmds, panim->cmds )
	'TODO: All these options (LX, LY, etc.) seem to be baked-in, but might need to be calculated for anims that have movement.
	'lookupControl( token )       
	Private Sub WriteAnimationOptions(ByVal aSequenceDesc As SourceMdlSequenceDesc31, ByVal anAnimationDesc As SourceMdlAnimationDesc31, ByVal impliedAnimDesc As SourceMdlAnimationDesc31)
		Dim line As String = ""

		line = vbTab
		line += "fps "
		line += anAnimationDesc.fps.ToString("0.######", TheApp.InternalNumberFormat)
		Me.theOutputFileStreamWriter.WriteLine(line)

		If aSequenceDesc Is Nothing Then
			If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_LOOPING) > 0 Then
				line = vbTab
				line += "loop"
				Me.theOutputFileStreamWriter.WriteLine(line)
			End If
		Else
			If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_LOOPING) > 0 Then
				line = vbTab
				line += "loop"
				Me.theOutputFileStreamWriter.WriteLine(line)
			End If
		End If

		'Me.WriteCmdListOptions(aSequenceDesc, anAnimationDesc, impliedAnimDesc)
	End Sub

	Private Sub WriteSequenceOrDeclareSequenceCommand()
		'$sequence producer "producer" fps 30.00
		'$sequence ragdoll "ragdoll" ACT_DIERAGDOLL 1 fps 30.00
		If Me.theMdlFileData.theSequenceDescs IsNot Nothing Then
			For i As Integer = 0 To Me.theMdlFileData.theSequenceDescs.Count - 1
				Dim aSequenceDesc As SourceMdlSequenceDesc31
				aSequenceDesc = Me.theMdlFileData.theSequenceDescs(i)

				'Me.WriteSequenceLine(aSequenceDesc)
			Next
		End If
	End Sub

	'Private Sub WriteSequenceLine(ByVal aSequenceDesc As SourceMdlSequenceDesc31)
	'	Dim line As String = ""

	'	Me.theOutputFileStreamWriter.WriteLine()

	'	If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_OVERRIDE) > 0 Then
	'		If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
	'			line = "$DeclareSequence"
	'		Else
	'			line = "$declaresequence"
	'		End If
	'		line += " """
	'		line += aSequenceDesc.theName
	'		line += """"
	'		Me.theOutputFileStreamWriter.WriteLine(line)
	'	Else
	'		If aSequenceDesc.theAnimDescIndexes IsNot Nothing OrElse aSequenceDesc.theAnimDescIndexes.Count > 0 Then
	'			If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
	'				line = "$Sequence "
	'			Else
	'				line = "$sequence "
	'			End If
	'			line += """"
	'			line += aSequenceDesc.theName
	'			line += """"
	'			'NOTE: Opening brace must be on same line as the command.
	'			line += " {"
	'			Me.theOutputFileStreamWriter.WriteLine(line)

	'			Try
	'				Me.WriteSequenceOptions(aSequenceDesc)
	'			Catch ex As Exception
	'				Dim debug As Integer = 4242
	'			End Try

	'			line = "}"
	'			Me.theOutputFileStreamWriter.WriteLine(line)
	'		End If
	'	End If
	'End Sub

	'Private Sub WriteSequenceOptions(ByVal aSequenceDesc As SourceMdlSequenceDesc37)
	'	Dim line As String = ""
	'	Dim valueString As String
	'	Dim impliedAnimDesc As SourceMdlAnimationDesc37 = Nothing

	'	Dim anAnimationDesc As SourceMdlAnimationDesc37
	'	Dim name As String
	'	For j As Integer = 0 To aSequenceDesc.theAnimDescIndexes.Count - 1
	'		anAnimationDesc = Me.theMdlFileData.theAnimationDescs(aSequenceDesc.theAnimDescIndexes(j))
	'		name = anAnimationDesc.theName

	'		line = vbTab
	'		line += """"
	'		If name(0) = "@" Then
	'			'NOTE: There should only be one implied anim desc.
	'			impliedAnimDesc = anAnimationDesc
	'			anAnimationDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(anAnimationDesc.theSmdRelativePathFileName, Me.theModelName, anAnimationDesc.theName)
	'			line += anAnimationDesc.theSmdRelativePathFileName
	'		Else
	'			If Not name.StartsWith("a_") Then
	'				line += "a_"
	'			End If
	'			line += name
	'		End If
	'		line += """"
	'		Me.theOutputFileStreamWriter.WriteLine(line)
	'	Next

	'	If aSequenceDesc.theActivityName <> "" Then
	'		line = vbTab
	'		line += "activity "
	'		line += """"
	'		line += aSequenceDesc.theActivityName
	'		line += """ "
	'		line += aSequenceDesc.activityWeight.ToString(TheApp.InternalNumberFormat)
	'		Me.theOutputFileStreamWriter.WriteLine(line)
	'	End If

	'	If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_AUTOPLAY) > 0 Then
	'		line = vbTab
	'		line += "autoplay"
	'		Me.theOutputFileStreamWriter.WriteLine(line)
	'	End If

	'	If aSequenceDesc.theEvents IsNot Nothing Then
	'		Dim frameIndex As Integer
	'		Dim frameCount As Integer
	'		frameCount = Me.theMdlFileData.theAnimationDescs(aSequenceDesc.theAnimDescIndexes(0)).frameCount
	'		For j As Integer = 0 To aSequenceDesc.theEvents.Count - 1
	'			If frameCount <= 1 Then
	'				frameIndex = 0
	'			Else
	'				frameIndex = CInt(aSequenceDesc.theEvents(j).cycle * (frameCount - 1))
	'			End If
	'			line = vbTab
	'			line += "{ "
	'			line += "event "
	'			line += aSequenceDesc.theEvents(j).eventIndex.ToString(TheApp.InternalNumberFormat)
	'			line += " "
	'			line += frameIndex.ToString(TheApp.InternalNumberFormat)
	'			If aSequenceDesc.theEvents(j).options <> "" Then
	'				line += " """
	'				line += CStr(aSequenceDesc.theEvents(j).options).Trim(Chr(0))
	'				line += """"
	'			End If
	'			line += " }"
	'			Me.theOutputFileStreamWriter.WriteLine(line)
	'		Next
	'	End If

	'	valueString = aSequenceDesc.fadeInTime.ToString("0.######", TheApp.InternalNumberFormat)
	'	line = vbTab
	'	line += "fadein "
	'	line += valueString
	'	Me.theOutputFileStreamWriter.WriteLine(line)

	'	valueString = aSequenceDesc.fadeOutTime.ToString("0.######", TheApp.InternalNumberFormat)
	'	line = vbTab
	'	line += "fadeout "
	'	line += valueString
	'	Me.theOutputFileStreamWriter.WriteLine(line)

	'	If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_HIDDEN) > 0 Then
	'		line = vbTab
	'		line += "hidden"
	'		Me.theOutputFileStreamWriter.WriteLine(line)
	'	End If

	'	If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_SNAP) > 0 Then
	'		line = vbTab
	'		line += "snap"
	'		Me.theOutputFileStreamWriter.WriteLine(line)
	'	End If

	'	If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_WORLD) > 0 Then
	'		line = vbTab
	'		line += "worldspace"
	'		Me.theOutputFileStreamWriter.WriteLine(line)
	'	End If

	'	Me.WriteAnimationOptions(aSequenceDesc, firstAnimDesc, impliedAnimDesc)
	'End Sub

	Public Sub WriteCollisionModelOrCollisionJointsCommand()
		Dim line As String = ""

		'NOTE: Data is from PHY file.
		'$collisionmodel "tree_deciduous_01a_physbox.smd"
		'{
		'	$mass 350.0
		'	$concave
		'}	
		'$collisionjoints "phymodel.smd"
		'{
		'	$mass 100.0
		'	$inertia 10.00
		'	$damping 0.05
		'	$rotdamping 5.00
		'	$rootbone "valvebiped.bip01_pelvis"
		'	$jointrotdamping "valvebiped.bip01_pelvis" 3.00
		'
		'	$jointmassbias "valvebiped.bip01_spine1" 8.00
		'	$jointconstrain "valvebiped.bip01_spine1" x limit -10.00 10.00 0.00
		'	$jointconstrain "valvebiped.bip01_spine1" y limit -16.00 16.00 0.00
		'	$jointconstrain "valvebiped.bip01_spine1" z limit -20.00 30.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_spine2" 9.00
		'	$jointconstrain "valvebiped.bip01_spine2" x limit -10.00 10.00 0.00
		'	$jointconstrain "valvebiped.bip01_spine2" y limit -10.00 10.00 0.00
		'	$jointconstrain "valvebiped.bip01_spine2" z limit -20.00 20.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_r_clavicle" 4.00
		'	$jointrotdamping "valvebiped.bip01_r_clavicle" 6.00
		'	$jointconstrain "valvebiped.bip01_r_clavicle" x limit -15.00 15.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_clavicle" y limit -10.00 10.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_clavicle" z limit -0.00 45.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_l_clavicle" 4.00
		'	$jointrotdamping "valvebiped.bip01_l_clavicle" 6.00
		'	$jointconstrain "valvebiped.bip01_l_clavicle" x limit -15.00 15.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_clavicle" y limit -10.00 10.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_clavicle" z limit -0.00 45.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_l_upperarm" 5.00
		'	$jointrotdamping "valvebiped.bip01_l_upperarm" 2.00
		'	$jointconstrain "valvebiped.bip01_l_upperarm" x limit -15.00 20.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_upperarm" y limit -40.00 32.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_upperarm" z limit -80.00 25.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_l_forearm" 4.00
		'	$jointrotdamping "valvebiped.bip01_l_forearm" 4.00
		'	$jointconstrain "valvebiped.bip01_l_forearm" x limit -40.00 15.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_forearm" y limit 0.00 0.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_forearm" z limit -120.00 10.00 0.00
		'
		'	$jointrotdamping "valvebiped.bip01_l_hand" 1.00
		'	$jointconstrain "valvebiped.bip01_l_hand" x limit -25.00 25.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_hand" y limit -35.00 35.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_hand" z limit -50.00 50.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_r_upperarm" 5.00
		'	$jointrotdamping "valvebiped.bip01_r_upperarm" 2.00
		'	$jointconstrain "valvebiped.bip01_r_upperarm" x limit -15.00 20.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_upperarm" y limit -40.00 32.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_upperarm" z limit -80.00 25.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_r_forearm" 4.00
		'	$jointrotdamping "valvebiped.bip01_r_forearm" 4.00
		'	$jointconstrain "valvebiped.bip01_r_forearm" x limit -40.00 15.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_forearm" y limit 0.00 0.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_forearm" z limit -120.00 10.00 0.00
		'
		'	$jointrotdamping "valvebiped.bip01_r_hand" 1.00
		'	$jointconstrain "valvebiped.bip01_r_hand" x limit -25.00 25.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_hand" y limit -35.00 35.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_hand" z limit -50.00 50.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_r_thigh" 7.00
		'	$jointrotdamping "valvebiped.bip01_r_thigh" 7.00
		'	$jointconstrain "valvebiped.bip01_r_thigh" x limit -25.00 25.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_thigh" y limit -10.00 15.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_thigh" z limit -55.00 25.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_r_calf" 4.00
		'	$jointconstrain "valvebiped.bip01_r_calf" x limit -10.00 25.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_calf" y limit -5.00 5.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_calf" z limit -10.00 115.00 0.00
		'
		'	$jointrotdamping "valvebiped.bip01_r_foot" 2.00
		'	$jointconstrain "valvebiped.bip01_r_foot" x limit -20.00 30.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_foot" y limit -30.00 20.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_foot" z limit -30.00 50.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_l_thigh" 7.00
		'	$jointrotdamping "valvebiped.bip01_l_thigh" 7.00
		'	$jointconstrain "valvebiped.bip01_l_thigh" x limit -25.00 25.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_thigh" y limit -10.00 15.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_thigh" z limit -55.00 25.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_l_calf" 4.00
		'	$jointconstrain "valvebiped.bip01_l_calf" x limit -10.00 25.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_calf" y limit -5.00 5.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_calf" z limit -10.00 115.00 0.00
		'
		'	$jointrotdamping "valvebiped.bip01_l_foot" 2.00
		'	$jointconstrain "valvebiped.bip01_l_foot" x limit -20.00 30.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_foot" y limit -30.00 20.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_foot" z limit -30.00 50.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_head1" 4.00
		'	$jointrotdamping "valvebiped.bip01_head1" 3.00
		'	$jointconstrain "valvebiped.bip01_head1" x limit -50.00 50.00 0.00
		'	$jointconstrain "valvebiped.bip01_head1" y limit -20.00 20.00 0.00
		'	$jointconstrain "valvebiped.bip01_head1" z limit -26.00 30.00 0.00
		'}
		If Me.thePhyFileData IsNot Nothing AndAlso Me.thePhyFileData.solidCount > 0 Then
			Me.theOutputFileStreamWriter.WriteLine(line)

			'If Me.theSourceEngineModel.PhyFileHeader.checksum <> Me.theSourceEngineModel.MdlFileHeader.checksum Then
			'	line = "// The PHY file's checksum value is not the same as the MDL file's checksum value."
			'	Me.theOutputFileStreamWriter.WriteLine(line)
			'End If

			'NOTE: The smd file name for $collisionjoints is not stored in the mdl file.
			'TODO: Find a better way to determine which to use.
			'If theSourceEngineModel.thePhyFileHeader.theSourcePhyPhysCollisionModels.Count < 2 Then
			'TODO: Why not use this "if" statement? It seems reasonable that a solid matches a set of convex shapes for one bone.
			'      For example, L4D2 van has several convex shapes, but only one solid and one bone.
			'      Same for w_minigun. Both use $concave.
			'If Me.theSourceEngineModel.thePhyFileHeader.solidCount = 1 Then
			If Me.thePhyFileData.theSourcePhyIsCollisionModel Then
				If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
					line = "$CollisionModel "
				Else
					line = "$collisionmodel "
				End If
			Else
				If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
					line = "$CollisionJoints "
				Else
					line = "$collisionjoints "
				End If
			End If
			'line += """phymodel.smd"""
			line += """"
			Me.thePhyFileData.thePhysicsMeshSmdFileName = SourceFileNamesModule.CreatePhysicsSmdFileName(Me.thePhyFileData.thePhysicsMeshSmdFileName, Me.theModelName)
			line += Me.thePhyFileData.thePhysicsMeshSmdFileName
			line += """"
			Me.theOutputFileStreamWriter.WriteLine(line)
			line = "{"
			Me.theOutputFileStreamWriter.WriteLine(line)

			Me.WriteCollisionModelOrCollisionJointsOptions()

			line = "}"
			Me.theOutputFileStreamWriter.WriteLine(line)
		End If
	End Sub

	'$animatedfriction
	'$assumeworldspace
	'$automass          // baked-in as mass
	'$concave           // done
	'$concaveperjoint
	'$damping           // done
	'$drag
	'$inertia           // done
	'$jointcollide
	'$jointconstrain    // done
	'$jointdamping      // done
	'$jointinertia      // done
	'$jointmassbias     // done
	'$jointmerge
	'$jointrotdamping   // done
	'$jointskip
	'$mass              // done
	'$masscenter
	'$maxconvexpieces
	'$noselfcollisions  //done
	'$remove2d
	'$rollingDrag
	'$rootbone          // done
	'$rotdamping        // done
	'$weldnormal
	'$weldposition
	Private Sub WriteCollisionModelOrCollisionJointsOptions()
		Dim line As String = ""

		line = vbTab
		line += "$mass "
		line += Me.thePhyFileData.theSourcePhyEditParamsSection.totalMass.ToString("0.######", TheApp.InternalNumberFormat)
		Me.theOutputFileStreamWriter.WriteLine(line)
		line = vbTab
		line += "$inertia "
		line += Me.thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theInertia.ToString("0.######", TheApp.InternalNumberFormat)
		Me.theOutputFileStreamWriter.WriteLine(line)
		line = vbTab
		line += "$damping "
		line += Me.thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theDamping.ToString("0.######", TheApp.InternalNumberFormat)
		Me.theOutputFileStreamWriter.WriteLine(line)
		line = vbTab
		line += "$rotdamping "
		line += Me.thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theRotDamping.ToString("0.######", TheApp.InternalNumberFormat)
		Me.theOutputFileStreamWriter.WriteLine(line)
		If Me.thePhyFileData.theSourcePhyEditParamsSection.rootName <> "" Then
			line = vbTab
			line += "$rootbone """
			line += Me.thePhyFileData.theSourcePhyEditParamsSection.rootName
			line += """"
			Me.theOutputFileStreamWriter.WriteLine(line)
		End If
		If Me.thePhyFileData.theSourcePhyEditParamsSection.jointMergeMap IsNot Nothing AndAlso Me.thePhyFileData.theSourcePhyEditParamsSection.jointMergeMap.Count > 0 Then
			For Each jointMergeKey As String In Me.thePhyFileData.theSourcePhyEditParamsSection.jointMergeMap.Keys
				For Each jointMergeValue As String In Me.thePhyFileData.theSourcePhyEditParamsSection.jointMergeMap(jointMergeKey)
					line = vbTab
					line += "$jointmerge """
					line += jointMergeKey
					line += """ """
					line += jointMergeValue
					line += """"
					Me.theOutputFileStreamWriter.WriteLine(line)
				Next
			Next
		End If
		If Me.thePhyFileData.theSourcePhyEditParamsSection.concave = "1" Then
			line = vbTab
			line += "$concave"
			Me.theOutputFileStreamWriter.WriteLine(line)
		End If

		For i As Integer = 0 To Me.thePhyFileData.theSourcePhyPhysCollisionModels.Count - 1
			Dim aSourcePhysCollisionModel As SourcePhyPhysCollisionModel
			aSourcePhysCollisionModel = Me.thePhyFileData.theSourcePhyPhysCollisionModels(i)

			line = ""
			Me.theOutputFileStreamWriter.WriteLine(line)

			'If aSourcePhysCollisionModel.theDragCoefficientIsValid Then
			'End If

			If aSourcePhysCollisionModel.theMassBiasIsValid Then
				line = vbTab
				line += "$jointmassbias """
				line += aSourcePhysCollisionModel.theName
				line += """ "
				line += aSourcePhysCollisionModel.theMassBias.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)
			End If

			If aSourcePhysCollisionModel.theDamping <> Me.thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theDamping Then
				line = vbTab
				line += "$jointdamping """
				line += aSourcePhysCollisionModel.theName
				line += """ "
				line += aSourcePhysCollisionModel.theDamping.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)
			End If

			If aSourcePhysCollisionModel.theInertia <> Me.thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theInertia Then
				line = vbTab
				line += "$jointinertia """
				line += aSourcePhysCollisionModel.theName
				line += """ "
				line += aSourcePhysCollisionModel.theInertia.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)
			End If

			If aSourcePhysCollisionModel.theRotDamping <> Me.thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theRotDamping Then
				line = vbTab
				line += "$jointrotdamping """
				line += aSourcePhysCollisionModel.theName
				line += """ "
				line += aSourcePhysCollisionModel.theRotDamping.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)
			End If

			If Me.thePhyFileData.theSourcePhyRagdollConstraintDescs.ContainsKey(aSourcePhysCollisionModel.theIndex) Then
				Dim aConstraint As SourcePhyRagdollConstraint
				aConstraint = Me.thePhyFileData.theSourcePhyRagdollConstraintDescs(aSourcePhysCollisionModel.theIndex)
				line = vbTab
				line += "$jointconstrain """
				line += aSourcePhysCollisionModel.theName
				line += """ x limit "
				line += aConstraint.theXMin.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aConstraint.theXMax.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aConstraint.theXFriction.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)
				line = vbTab
				line += "$jointconstrain """
				line += aSourcePhysCollisionModel.theName
				line += """ y limit "
				line += aConstraint.theYMin.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aConstraint.theYMax.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aConstraint.theYFriction.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)
				line = vbTab
				line += "$jointconstrain """
				line += aSourcePhysCollisionModel.theName
				line += """ z limit "
				line += aConstraint.theZMin.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aConstraint.theZMax.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aConstraint.theZFriction.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)
			End If
		Next

		If Not Me.thePhyFileData.theSourcePhySelfCollides Then
			line = vbTab
			line += "$noselfcollisions"
			Me.theOutputFileStreamWriter.WriteLine(line)
		Else
			For Each aSourcePhyCollisionPair As SourcePhyCollisionPair In Me.thePhyFileData.theSourcePhyCollisionPairs
				line = vbTab
				line += "$jointcollide"
				line += " "
				line += """"
				line += Me.thePhyFileData.theSourcePhyPhysCollisionModels(aSourcePhyCollisionPair.obj0).theName
				line += """"
				line += " "
				line += """"
				line += Me.thePhyFileData.theSourcePhyPhysCollisionModels(aSourcePhyCollisionPair.obj1).theName
				line += """"
				Me.theOutputFileStreamWriter.WriteLine(line)
			Next
		End If
	End Sub

#End Region

#Region "Data"

	'Private theModel As SourceModel
	Private theOutputFileStreamWriter As StreamWriter
	Private theMdlFileData As SourceMdlFileData31
	Private thePhyFileData As SourcePhyFileData
	Private theVtxFileData As SourceVtxFileData06
	Private theModelName As String

	Private theOutputPath As String
	Private theOutputFileNameWithoutExtension As String

#End Region

End Class
