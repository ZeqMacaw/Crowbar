Imports System.IO

Public Class SourceVtaFile44

#Region "Creation and Destruction"

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData44, ByVal vvdFileData As SourceVvdFileData04)
		Me.theOutputFileStreamWriter = outputFileStream
		Me.theMdlFileData = mdlFileData
		Me.theVvdFileData = vvdFileData
	End Sub

#End Region

#Region "Methods"

	Public Sub WriteHeaderComment()
		Common.WriteHeaderComment(Me.theOutputFileStreamWriter)
	End Sub

	Public Sub WriteHeaderSection()
		Dim line As String = ""

		'version 1
		line = "version 1"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteNodesSection()
		Dim line As String = ""
		Dim name As String

		'nodes
		line = "nodes"
		Me.theOutputFileStreamWriter.WriteLine(line)

		For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
			name = Me.theMdlFileData.theBones(boneIndex).theName

			line = "  "
			line += boneIndex.ToString(TheApp.InternalNumberFormat)
			line += " """
			line += name
			line += """ "
			line += Me.theMdlFileData.theBones(boneIndex).parentBoneIndex.ToString(TheApp.InternalNumberFormat)
			Me.theOutputFileStreamWriter.WriteLine(line)
		Next

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteSkeletonSectionForVertexAnimation()
		Dim line As String = ""

		line = "skeleton"
		Me.theOutputFileStreamWriter.WriteLine(line)

		If TheApp.Settings.DecompileStricterFormatIsChecked Then
			line = "time 0 # basis shape key"
		Else
			line = "  time 0 # basis shape key"
		End If
		Me.theOutputFileStreamWriter.WriteLine(line)

		Dim timeIndex As Integer
		Dim flexTimeIndex As Integer
		Dim aFlexFrame As FlexFrame

		timeIndex = 1
		'NOTE: The first frame was written in code above.
		For flexTimeIndex = 1 To Me.theMdlFileData.theFlexFrames.Count - 1
			aFlexFrame = Me.theMdlFileData.theFlexFrames(flexTimeIndex)

			If TheApp.Settings.DecompileStricterFormatIsChecked Then
				line = "time "
			Else
				line = "  time "
			End If
			line += timeIndex.ToString(TheApp.InternalNumberFormat)
			line += " # "
			line += aFlexFrame.flexDescription
			Me.theOutputFileStreamWriter.WriteLine(line)

			timeIndex += 1
		Next

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteVertexAnimationSection()
		Dim line As String = ""

		line = "vertexanimation"
		Me.theOutputFileStreamWriter.WriteLine(line)

		If TheApp.Settings.DecompileStricterFormatIsChecked Then
			line = "time 0 # basis shape key"
		Else
			line = "  time 0 # basis shape key"
		End If
		Me.theOutputFileStreamWriter.WriteLine(line)

		Try
			Dim aVertex As SourceVertex
			For vertexIndex As Integer = 0 To Me.theVvdFileData.theVertexes.Count - 1
				If Me.theVvdFileData.fixupCount = 0 Then
					aVertex = Me.theVvdFileData.theVertexes(vertexIndex)
				Else
					'NOTE: I don't know why lodIndex is not needed here, but using only lodIndex=0 matches what MDL Decompiler produces.
					'      Maybe the listing by lodIndex is only needed internally by graphics engine.
					aVertex = Me.theVvdFileData.theFixedVertexesByLod(0)(vertexIndex)
				End If

				line = "    "
				line += vertexIndex.ToString(TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.positionX.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.positionY.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.positionZ.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.normalX.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.normalY.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.normalZ.ToString("0.000000", TheApp.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)
			Next
		Catch
		End Try

		Dim timeIndex As Integer
		Dim flexTimeIndex As Integer
		Dim aFlexFrame As FlexFrame

		timeIndex = 1
		'NOTE: The first frame was written in code above.
		For flexTimeIndex = 1 To Me.theMdlFileData.theFlexFrames.Count - 1
			aFlexFrame = Me.theMdlFileData.theFlexFrames(flexTimeIndex)

			If TheApp.Settings.DecompileStricterFormatIsChecked Then
				line = "time "
			Else
				line = "  time "
			End If
			line += timeIndex.ToString(TheApp.InternalNumberFormat)
			line += " # "
			line += aFlexFrame.flexDescription
			Me.theOutputFileStreamWriter.WriteLine(line)

			For x As Integer = 0 To aFlexFrame.flexes.Count - 1
				Me.WriteVertexAnimLines(aFlexFrame.flexes(x), aFlexFrame.bodyAndMeshVertexIndexStarts(x))
			Next

			timeIndex += 1
		Next

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

#End Region

#Region "Private Delegates"

#End Region

#Region "Private Methods"

	Private Sub WriteVertexAnimLines(ByVal aFlex As SourceMdlFlex, ByVal bodyAndMeshVertexIndexStart As Integer)
		Dim line As String
		Dim aVertex As SourceVertex
		Dim vertexIndex As Integer
		Dim positionX As Double
		Dim positionY As Double
		Dim positionZ As Double
		Dim normalX As Double
		Dim normalY As Double
		Dim normalZ As Double

		For i As Integer = 0 To aFlex.theVertAnims.Count - 1
			Dim aVertAnim As SourceMdlVertAnim
			aVertAnim = aFlex.theVertAnims(i)

			'TODO: Figure out why decompiling teen angst zoey (which has 39 shape keys) gives 55 shapekeys.
			'      - Probably extra ones are related to flexpairs (right and left).
			'      - Eyelids are combined, e.g. second shapekey from source vta is upper_lid_lowerer
			'        that contains both upper_right_lowerer and upper_left_lowerer.

			vertexIndex = aVertAnim.index + bodyAndMeshVertexIndexStart
			If Me.theVvdFileData.fixupCount = 0 Then
				aVertex = Me.theVvdFileData.theVertexes(vertexIndex)
			Else
				'NOTE: I don't know why lodIndex is not needed here, but using only lodIndex=0 matches what MDL Decompiler produces.
				'      Maybe the listing by lodIndex is only needed internally by graphics engine.
				aVertex = Me.theVvdFileData.theFixedVertexesByLod(0)(vertexIndex)
			End If

			positionX = aVertex.positionX + aVertAnim.flDelta(0).TheFloatValue
			positionY = aVertex.positionY + aVertAnim.flDelta(1).TheFloatValue
			positionZ = aVertex.positionZ + aVertAnim.flDelta(2).TheFloatValue
			normalX = aVertex.normalX + aVertAnim.flNDelta(0).TheFloatValue
			normalY = aVertex.normalY + aVertAnim.flNDelta(1).TheFloatValue
			normalZ = aVertex.normalZ + aVertAnim.flNDelta(2).TheFloatValue
			line = "    "
			line += vertexIndex.ToString(TheApp.InternalNumberFormat)
			line += " "
			line += positionX.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += positionY.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += positionZ.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += normalX.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += normalY.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += normalZ.ToString("0.000000", TheApp.InternalNumberFormat)

			'TEST:
			'If aFlex.vertAnimType = aFlex.STUDIO_VERT_ANIM_WRINKLE Then
			'	CType(aVertAnim, SourceMdlVertAnimWrinkle).wrinkleDelta = Me.theInputFileReader.ReadInt16()
			'End If
			'If blah Then
			'	line += " // wrinkle value: "
			'	line += aVertAnim.flDelta(0).the16BitValue.ToString()
			'End If

			Me.theOutputFileStreamWriter.WriteLine(line)
		Next
	End Sub

#End Region

#Region "Data"

	Private theOutputFileStreamWriter As StreamWriter
	Private theMdlFileData As SourceMdlFileData44
	Private theVvdFileData As SourceVvdFileData04

#End Region

End Class
