Imports System.IO
Imports System.Text

Public Class SourceQcFile14
	Inherits SourceQcFile

#Region "Creation and Destruction"

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal outputPathFileName As String, ByVal mdlFileData As SourceMdlFileData14, ByVal modelName As String)
		Me.theOutputFileStreamWriter = outputFileStream
		Me.theMdlFileData = mdlFileData
		Me.theModelName = modelName

		Me.theOutputPath = FileManager.GetPath(outputPathFileName)
		Me.theOutputFileNameWithoutExtension = Path.GetFileNameWithoutExtension(outputPathFileName)
	End Sub

#End Region

#Region "Methods"

	Public Sub WriteHeaderComment()
		Common.WriteHeaderComment(Me.theOutputFileStreamWriter)
	End Sub

	Public Sub WriteAttachmentCommand()
		Dim line As String = ""

		If Me.theMdlFileData.theAttachments IsNot Nothing Then
			line = ""
			Me.theOutputFileStreamWriter.WriteLine(line)

			For i As Integer = 0 To Me.theMdlFileData.theAttachments.Count - 1
				Dim anAttachment As SourceMdlAttachment10
				anAttachment = Me.theMdlFileData.theAttachments(i)

				If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
					line = "$Attachment "
				Else
					line = "$attachment "
				End If
				line += i.ToString(TheApp.InternalNumberFormat)
				line += " """
				line += Me.theMdlFileData.theBones(anAttachment.boneIndex).theName
				line += """"
				line += " "

				line += anAttachment.attachmentPoint.x.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += anAttachment.attachmentPoint.y.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += anAttachment.attachmentPoint.z.ToString("0.######", TheApp.InternalNumberFormat)

				Me.theOutputFileStreamWriter.WriteLine(line)
			Next
		End If
	End Sub

	Public Sub WriteBBoxCommand()
		Dim line As String = ""
		Dim minX As Double
		Dim minY As Double
		Dim minZ As Double
		Dim maxX As Double
		Dim maxY As Double
		Dim maxZ As Double

		Me.theOutputFileStreamWriter.WriteLine()

		If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
			line = "// Bounding box or hull. Used for collision with a world object."
			Me.theOutputFileStreamWriter.WriteLine(line)
		End If

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

	Public Sub WriteBodyGroupCommand()
		Dim line As String = ""
		Dim aBodyPart As SourceMdlBodyPart14
		Dim aBodyModel As SourceMdlModel14

		If Me.theMdlFileData.theBodyParts IsNot Nothing AndAlso Me.theMdlFileData.theBodyParts.Count > 0 Then
			Me.theOutputFileStreamWriter.WriteLine()

			For bodyPartIndex As Integer = 0 To Me.theMdlFileData.theBodyParts.Count - 1
				aBodyPart = Me.theMdlFileData.theBodyParts(bodyPartIndex)

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

						line = vbTab
						If aBodyModel.theName = "blank" Then
							line += "blank"
						Else
							aBodyModel.theSmdFileName = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileName, bodyPartIndex, modelIndex, 0, Me.theModelName, Me.theMdlFileData.theBodyParts(bodyPartIndex).theModels(modelIndex).theName)
							line += "studio "
							line += """"
							line += Path.GetFileNameWithoutExtension(aBodyModel.theSmdFileName)
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

	Public Sub WriteCBoxCommand()
		Dim line As String = ""
		Dim minX As Double
		Dim minY As Double
		Dim minZ As Double
		Dim maxX As Double
		Dim maxY As Double
		Dim maxZ As Double

		Me.theOutputFileStreamWriter.WriteLine()

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

	Public Sub WriteCDCommand()
		Dim line As String = ""

		If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
			line = "$CD ""."""
		Else
			line = "$cd ""."""
		End If
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteCDTextureCommand()
		Dim line As String = ""

		If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
			line = "$CDTexture ""."""
		Else
			line = "$cdtexture ""."""
		End If
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteClipToTexturesCommand()
		Dim line As String = ""

		If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
			line = "$ClipToTextures"
		Else
			line = "$cliptotextures"
		End If
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteControllerCommand()
		Dim line As String = ""
		Dim boneController As SourceMdlBoneController10

		'$controller mouth "jaw" X 0 20
		'$controller 0 "tracker" LYR -1 1
		Try
			If Me.theMdlFileData.theBoneControllers IsNot Nothing Then
				If Me.theMdlFileData.theBoneControllers.Count > 0 Then
					Me.theOutputFileStreamWriter.WriteLine()
				End If

				For i As Integer = 0 To Me.theMdlFileData.theBoneControllers.Count - 1
					boneController = Me.theMdlFileData.theBoneControllers(i)

					If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
						line = "$Controller "
					Else
						line = "$controller "
					End If
					If boneController.index = 4 Then
						line += "Mouth"
					Else
						line += boneController.index.ToString(TheApp.InternalNumberFormat)
					End If
					line += " """
					line += Me.theMdlFileData.theBones(boneController.boneIndex).theName
					line += """ "
					line += SourceModule10.GetControlText(boneController.type)
					line += " "
					line += boneController.startAngleDegrees.ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += boneController.endAngleDegrees.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStreamWriter.WriteLine(line)
				Next
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Public Sub WriteExternalTexturesCommand()
		Dim line As String = ""

		If Me.theMdlFileData.textureCount = 0 Then
			Me.theOutputFileStreamWriter.WriteLine()

			If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
				line = "$ExternalTextures"
			Else
				line = "$externaltextures"
			End If
			Me.theOutputFileStreamWriter.WriteLine(line)
		End If
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

	Public Sub WriteFlagsCommand()
		Dim line As String = ""

		Me.theOutputFileStreamWriter.WriteLine()

		If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
			line = "$Flags "
		Else
			line = "$flags "
		End If
		line += Me.theMdlFileData.flags.ToString(TheApp.InternalNumberFormat)
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteHBoxCommands()
		Dim line As String = ""
		Dim aHitbox As SourceMdlHitbox10

		If Me.theMdlFileData.theHitboxes.Count > 0 Then
			Me.theOutputFileStreamWriter.WriteLine()

			For j As Integer = 0 To Me.theMdlFileData.theHitboxes.Count - 1
				aHitbox = Me.theMdlFileData.theHitboxes(j)

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

				Me.theOutputFileStreamWriter.WriteLine(line)
			Next
		End If
	End Sub

	Public Sub WriteModelNameCommand()
		Dim line As String = ""
		Dim modelPathFileName As String

		modelPathFileName = Me.theMdlFileData.theModelName

		Me.theOutputFileStreamWriter.WriteLine()

		If Path.GetExtension(modelPathFileName) <> ".mdl" Then
			line = "// Stored modelname (without quotes): """ + modelPathFileName + """"
			Me.theOutputFileStreamWriter.WriteLine(line)

			modelPathFileName = Me.theMdlFileData.theFileName + ".mdl"
		End If

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

	'NOTE: Although this code is correct, the $sequencegroup command seems completely pointless; it just labels each group, but is not used for anything.
	'Public Sub WriteSequenceGroupCommands()
	'	Dim line As String = ""
	'	Dim aSequenceGroup As SourceMdlSequenceGroup10

	'	If Me.theMdlFileData.theSequenceGroups.Count > 1 Then
	'		Me.theOutputFileStreamWriter.WriteLine()

	'		For sequenceGroupIndex As Integer = 0 To Me.theMdlFileData.theSequenceGroups.Count - 1
	'			aSequenceGroup = Me.theMdlFileData.theSequenceGroups(sequenceGroupIndex)

	'			If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
	'				line = "$SequenceGroup "
	'			Else
	'				line = "$sequencegroup "
	'			End If
	'			line += """"
	'			line += aSequenceGroup.theName
	'			line += """"

	'			Me.theOutputFileStreamWriter.WriteLine(line)
	'		Next
	'	End If
	'End Sub

	Public Sub WriteSequenceGroupSizeCommand()
		Dim line As String = ""
		Dim fileSize As Long = 0
		Dim largestFileSize As Long = 0
		Dim groupSize As Long = 0
		Dim remainder As Double = 0

		If Me.theMdlFileData.theSequenceGroups.Count > 1 Then
			For sequenceGroupIndex As Integer = 0 To Me.theMdlFileData.theSequenceGroups.Count - 1
				fileSize = Me.theMdlFileData.theSequenceGroupFileHeaders(sequenceGroupIndex).theActualFileSize
				If largestFileSize < fileSize Then
					largestFileSize = fileSize
				End If
			Next

			If largestFileSize > 0 Then
				groupSize = largestFileSize \ 1024
				remainder = largestFileSize Mod 1024
				If remainder > 0 Then
					groupSize += 1
				End If

				Me.theOutputFileStreamWriter.WriteLine()

				If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
					line = "$SequenceGroupSize "
				Else
					line = "$sequencegroupsize "
				End If
				line += groupSize.ToString(TheApp.InternalNumberFormat)

				Me.theOutputFileStreamWriter.WriteLine(line)
			End If
		End If
	End Sub

	Public Sub WriteScaleCommand()
		Dim line As String = ""

		If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
			line = "$Scale 1.0"
		Else
			line = "$scale 1.0"
		End If
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteSequenceCommands()
		Dim line As String = ""
		Dim aSequence As SourceMdlSequenceDesc10

		If Me.theMdlFileData.theSequences IsNot Nothing AndAlso Me.theMdlFileData.theSequences.Count > 0 Then
			Me.theOutputFileStreamWriter.WriteLine()

			For sequenceGroupIndex As Integer = 0 To Me.theMdlFileData.theSequences.Count - 1
				aSequence = Me.theMdlFileData.theSequences(sequenceGroupIndex)

				If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
					line = "$Sequence "
				Else
					line = "$sequence "
				End If
				line += """"
				line += aSequence.theName
				line += """"
				'NOTE: Opening brace must be on same line as the command.
				line += " {"
				Me.theOutputFileStreamWriter.WriteLine(line)

				Try
					Me.WriteSequenceOptions(aSequence)
				Catch ex As Exception
					Dim debug As Integer = 4242
				End Try

				line = "}"
				Me.theOutputFileStreamWriter.WriteLine(line)
			Next
		End If
	End Sub

	Public Sub WriteTexRenderMode()
		If Me.theMdlFileData.theTextures IsNot Nothing AndAlso Me.theMdlFileData.theTextures.Count > 0 Then
			Me.theOutputFileStreamWriter.WriteLine()

			For Each texture As SourceMdlTexture14 In Me.theMdlFileData.theTextures
				If ((texture.flags And SourceMdlTexture14.STUDIO_NF_FLATSHADE) > 0) AndAlso (Not texture.theFileName.ToLower().Contains("chrome")) Then
					Me.WriteTexRenderModeLine(texture.theFileName, "flatshade", True)
				End If
				If ((texture.flags And SourceMdlTexture14.STUDIO_NF_CHROME) > 0) AndAlso (Not texture.theFileName.ToLower().Contains("chrome")) Then
					Me.WriteTexRenderModeLine(texture.theFileName, "chrome", False)
				End If
				If (texture.flags And SourceMdlTexture14.STUDIO_NF_FULLBRIGHT) > 0 Then
					Me.WriteTexRenderModeLine(texture.theFileName, "fullbright", False)
				End If
				If (texture.flags And SourceMdlTexture14.STUDIO_NF_NOMIPS) > 0 Then
					Me.WriteTexRenderModeLine(texture.theFileName, "nomips", False)
				End If
				If (texture.flags And SourceMdlTexture14.STUDIO_NF_ALPHA) > 0 Then
					Me.WriteTexRenderModeLine(texture.theFileName, "alpha", False)
				End If
				If (texture.flags And SourceMdlTexture14.STUDIO_NF_ADDITIVE) > 0 Then
					Me.WriteTexRenderModeLine(texture.theFileName, "additive", False)
				End If
				If (texture.flags And SourceMdlTexture14.STUDIO_NF_MASKED) > 0 Then
					Me.WriteTexRenderModeLine(texture.theFileName, "masked", False)
				End If
			Next
		End If
	End Sub

	Public Sub WriteTextureGroupCommand()
		Dim line As String = ""

		If Me.theMdlFileData.theSkinFamilies IsNot Nothing AndAlso Me.theMdlFileData.theSkinFamilies.Count > 0 AndAlso Me.theMdlFileData.theTextures IsNot Nothing AndAlso Me.theMdlFileData.theTextures.Count > 0 AndAlso Me.theMdlFileData.skinReferenceCount > 0 Then
			'Me.theOutputFileStreamWriter.WriteLine()

			'If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
			'	line = "$TextureGroup ""skinfamilies"""
			'Else
			'	line = "$texturegroup ""skinfamilies"""
			'End If
			'Me.theOutputFileStreamWriter.WriteLine(line)
			'line = "{"
			'Me.theOutputFileStreamWriter.WriteLine(line)

			'Dim skinFamilies As New List(Of List(Of String))(Me.theMdlFileData.theSkinFamilies.Count)
			'For i As Integer = 0 To Me.theMdlFileData.theSkinFamilies.Count - 1
			'	Dim aSkinFamily As List(Of Short)
			'	aSkinFamily = Me.theMdlFileData.theSkinFamilies(i)

			'	Dim textureFileNames As New List(Of String)(Me.theMdlFileData.skinReferenceCount)
			'	For j As Integer = 0 To Me.theMdlFileData.skinReferenceCount - 1
			'		Dim aTexture As SourceMdlTexture14
			'		aTexture = Me.theMdlFileData.theTextures(aSkinFamily(j))

			'		textureFileNames.Add(aTexture.theFileName)
			'	Next

			'	skinFamilies.Add(textureFileNames)
			'Next
			'======
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
					Dim aTexture As SourceMdlTexture14
					aTexture = Me.theMdlFileData.theTextures(aSkinFamily(j))

					textureFileNames.Add(aTexture.theFileName)
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

#End Region

#Region "Private Delegates"

#End Region

#Region "Private Methods"

	'		else if (stricmp("deform", token ) == 0)
	'		else if (stricmp("event", token ) == 0)
	'		else if (stricmp("pivot", token ) == 0)
	'		else if (stricmp("fps", token ) == 0)
	'		else if (stricmp("origin", token ) == 0)
	'		else if (stricmp("rotate", token ) == 0)
	'		else if (stricmp("scale", token ) == 0)
	'		else if (strnicmp("loop", token, 4 ) == 0)
	'		else if (strnicmp("frame", token, 5 ) == 0)
	'		else if (strnicmp("blend", token, 5 ) == 0)
	'		else if (strnicmp("node", token, 4 ) == 0)
	'		else if (strnicmp("transition", token, 4 ) == 0)
	'		else if (strnicmp("rtransition", token, 4 ) == 0)
	'		else if (lookupControl( token ) != -1)
	'int lookupControl( char *string )
	'{
	'	if (stricmp(string,"X")==0) return STUDIO_X;
	'	if (stricmp(string,"Y")==0) return STUDIO_Y;
	'	if (stricmp(string,"Z")==0) return STUDIO_Z;
	'	if (stricmp(string,"XR")==0) return STUDIO_XR;
	'	if (stricmp(string,"YR")==0) return STUDIO_YR;
	'	if (stricmp(string,"ZR")==0) return STUDIO_ZR;
	'	if (stricmp(string,"LX")==0) return STUDIO_LX;
	'	if (stricmp(string,"LY")==0) return STUDIO_LY;
	'	if (stricmp(string,"LZ")==0) return STUDIO_LZ;
	'	if (stricmp(string,"AX")==0) return STUDIO_AX;
	'	if (stricmp(string,"AY")==0) return STUDIO_AY;
	'	if (stricmp(string,"AZ")==0) return STUDIO_AZ;
	'	if (stricmp(string,"AXR")==0) return STUDIO_AXR;
	'	if (stricmp(string,"AYR")==0) return STUDIO_AYR;
	'	if (stricmp(string,"AZR")==0) return STUDIO_AZR;
	'	return -1;
	'}
	'		else if (stricmp("animation", token ) == 0)
	'		else if ((i = lookupActivity( token )) != 0)
	'int lookupActivity( char *szActivity )
	'{
	'	int i;
	'
	'	for (i = 0; activity_map[i].name; i++)
	'	{
	'		if (stricmp( szActivity, activity_map[i].name ) == 0)
	'			return activity_map[i].type;
	'	}
	'	// match ACT_#
	'	if (strnicmp( szActivity, "ACT_", 4 ) == 0)
	'	{
	'		return atoi( &szActivity[4] );
	'	}
	'	return 0;
	'}
	'		else
	'		{
	'			strcpyn( smdfilename[numblends++], token );
	'		}
	'------
	'  [activity_name or ACT_#]
	'X  animation   (same as using "smdfilename" by itself)
	'/  blend
	'X  deform   (seems to be a deleted command)
	'/  event
	'/  fps
	'X  frame   (not decompilable and not needed; when used the frames will decompile as a separate SMD file)
	'/  loop
	'  node
	'X  origin   (baked in)
	'  pivot
	'X  rotate   (baked in)
	'  rtransition
	'X  scale   (baked in)
	'  transition
	'  [X, Y, Z, XR, YR, ZR, LX, LY, LZ, AX, AY, AZ, AXR, AYR, AZR]
	'/  ["smdFileName"]
	Private Sub WriteSequenceOptions(ByVal aSequenceDesc As SourceMdlSequenceDesc10)
		Dim line As String = ""

		For blendIndex As Integer = 0 To aSequenceDesc.blendCount - 1
			If aSequenceDesc.blendCount = 1 Then
				aSequenceDesc.theSmdRelativePathFileNames(blendIndex) = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(aSequenceDesc.theSmdRelativePathFileNames(blendIndex), Me.theModelName, aSequenceDesc.theName, -1)
			Else
				aSequenceDesc.theSmdRelativePathFileNames(blendIndex) = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(aSequenceDesc.theSmdRelativePathFileNames(blendIndex), Me.theModelName, aSequenceDesc.theName, blendIndex)
			End If

			line = vbTab
			line += """"
			line += FileManager.GetPathFileNameWithoutExtension(aSequenceDesc.theSmdRelativePathFileNames(blendIndex))
			line += """"
			Me.theOutputFileStreamWriter.WriteLine(line)
		Next

		If aSequenceDesc.activityId > 0 Then
			Dim activityName As String
			If aSequenceDesc.activityId < SourceModule10.activityMap.Length Then
				activityName = SourceModule10.activityMap(aSequenceDesc.activityId)
			Else
				activityName = "ACT_" + aSequenceDesc.activityId.ToString(TheApp.InternalNumberFormat)
			End If
			line = vbTab
			line += activityName
			line += " "
			line += aSequenceDesc.activityWeight.ToString(TheApp.InternalNumberFormat)
			Me.theOutputFileStreamWriter.WriteLine(line)
		End If

		For i As Integer = 0 To 1
			If aSequenceDesc.blendType(i) <> 0 Then
				line = vbTab
				line += "blend "
				line += SourceModule10.GetControlText(aSequenceDesc.blendType(i))
				line += " "
				line += aSequenceDesc.blendStart(i).ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aSequenceDesc.blendEnd(i).ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)
			End If
		Next

		If aSequenceDesc.theEvents IsNot Nothing Then
			Dim frameIndex As Integer
			For j As Integer = 0 To aSequenceDesc.theEvents.Count - 1
				If aSequenceDesc.frameCount <= 1 Then
					frameIndex = 0
				Else
					frameIndex = aSequenceDesc.theEvents(j).frameIndex
				End If
				line = vbTab
				line += "{ "
				line += "event "
				line += aSequenceDesc.theEvents(j).eventIndex.ToString(TheApp.InternalNumberFormat)
				line += " "
				line += frameIndex.ToString(TheApp.InternalNumberFormat)
				If aSequenceDesc.theEvents(j).theOptions <> "" Then
					line += " """
					line += aSequenceDesc.theEvents(j).theOptions
					line += """"
				End If
				line += " }"
				Me.theOutputFileStreamWriter.WriteLine(line)
			Next
		End If

		line = vbTab
		line += "fps "
		line += aSequenceDesc.fps.ToString("0.######", TheApp.InternalNumberFormat)
		Me.theOutputFileStreamWriter.WriteLine(line)

		If (aSequenceDesc.flags And SourceMdlSequenceDesc10.STUDIO_LOOPING) > 0 Then
			line = vbTab
			line += "loop"
			Me.theOutputFileStreamWriter.WriteLine(line)
		End If

		If aSequenceDesc.motiontype > 0 Then
			line = vbTab
			line += SourceModule10.GetMultipleControlText(aSequenceDesc.motiontype)
			Me.theOutputFileStreamWriter.WriteLine(line)
		End If

		If aSequenceDesc.thePivots IsNot Nothing AndAlso aSequenceDesc.thePivots.Count > 0 Then
			For pivotIndex As Integer = 0 To aSequenceDesc.thePivots.Count - 1
				line = vbTab
				line += "pivot "
				line += pivotIndex.ToString(TheApp.InternalNumberFormat)
				line += " "
				line += aSequenceDesc.thePivots(0).pivotStart.ToString(TheApp.InternalNumberFormat)
				line += " "
				line += aSequenceDesc.thePivots(0).pivotEnd.ToString(TheApp.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)
			Next
		End If

		Me.WriteSequenceNodeInfo(aSequenceDesc)

		'If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_AUTOPLAY) > 0 Then
		'	line = vbTab
		'	line += "autoplay"
		'	Me.theOutputFileStreamWriter.WriteLine(line)
		'End If

		'If blah Then
		'	line = vbTab
		'	line += ""
		'	Me.theOutputFileStreamWriter.WriteLine(line)
		'End If
	End Sub

	Private Sub WriteSequenceNodeInfo(ByVal aSeqDesc As SourceMdlSequenceDesc10)
		Dim line As String = ""

		If aSeqDesc.entryNodeIndex > 0 Then
			If aSeqDesc.entryNodeIndex = aSeqDesc.exitNodeIndex Then
				'node (name)
				line = vbTab
				line += "node"
				line += " "
				line += (aSeqDesc.entryNodeIndex).ToString(TheApp.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)
			ElseIf (aSeqDesc.nodeFlags And 1) = 0 Then
				'transition (from) (to) 
				line = vbTab
				line += "transition"
				line += " "
				line += (aSeqDesc.entryNodeIndex).ToString(TheApp.InternalNumberFormat)
				line += " "
				line += (aSeqDesc.exitNodeIndex).ToString(TheApp.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)
			Else
				'rtransition (name1) (name2) 
				line = vbTab
				line += "rtransition"
				line += " "
				line += (aSeqDesc.entryNodeIndex).ToString(TheApp.InternalNumberFormat)
				line += " "
				line += (aSeqDesc.exitNodeIndex).ToString(TheApp.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)
			End If
		End If
	End Sub

	Private Sub WriteTexRenderModeLine(ByVal textureFileName As String, ByVal renderMode As String, ByVal lineIsCommented As Boolean)
		Dim line As String = ""

		If lineIsCommented Then
			line = "//"
		End If
		If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
			line += "$TexRenderMode "
		Else
			line += "$texrendermode "
		End If
		line += """"
		line += textureFileName
		line += """"
		line += " "
		line += renderMode
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

#End Region

#Region "Constants"

#End Region

#Region "Data"

	Private theOutputFileStreamWriter As StreamWriter
	Private theMdlFileData As SourceMdlFileData14
	Private theModelName As String

	Private theOutputPath As String
	Private theOutputFileNameWithoutExtension As String

#End Region

End Class
