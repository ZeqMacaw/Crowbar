Imports System.IO

Public Class SourceVtaFile2531

#Region "Creation and Destruction"

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData2531)
		Me.theOutputFileStreamWriter = outputFileStream
		Me.theMdlFileData = mdlFileData
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

		'skeleton
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
		Dim aFlexFrame As FlexFrame2531

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
			Dim aBodyModel As SourceMdlModel2531
			Dim vertexCount As Integer
			aBodyModel = Me.theMdlFileData.theBodyParts(0).theModels(0)
			If aBodyModel.vertexListType = 0 Then
				vertexCount = aBodyModel.theVertexesType0.Count
			ElseIf aBodyModel.vertexListType = 1 Then
				vertexCount = aBodyModel.theVertexesType1.Count
			ElseIf aBodyModel.vertexListType = 2 Then
				vertexCount = aBodyModel.theVertexesType2.Count
			Else
				vertexCount = 0
			End If

			Dim position As New SourceVector()
			Dim normal As New SourceVector()
			For vertexIndex As Integer = 0 To vertexCount - 1
				If aBodyModel.vertexListType = 0 Then
					position.x = aBodyModel.theVertexesType0(vertexIndex).position.x
					position.y = aBodyModel.theVertexesType0(vertexIndex).position.y
					position.z = aBodyModel.theVertexesType0(vertexIndex).position.z
					normal.x = aBodyModel.theVertexesType0(vertexIndex).normal.x
					normal.y = aBodyModel.theVertexesType0(vertexIndex).normal.y
					normal.z = aBodyModel.theVertexesType0(vertexIndex).normal.z
				ElseIf aBodyModel.vertexListType = 1 Then
					position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * Me.theMdlFileData.hullMinPosition.y
					position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * Me.theMdlFileData.hullMinPosition.z
					position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * Me.theMdlFileData.hullMinPosition.x
					normal.x = (aBodyModel.theVertexesType1(vertexIndex).normalX / 65535) * Me.theMdlFileData.hullMaxPosition.x
					normal.y = (aBodyModel.theVertexesType1(vertexIndex).normalY / 65535) * Me.theMdlFileData.hullMaxPosition.y
					normal.z = (aBodyModel.theVertexesType1(vertexIndex).normalZ / 65535) * Me.theMdlFileData.hullMaxPosition.z
				ElseIf aBodyModel.vertexListType = 2 Then
					position.x = (aBodyModel.theVertexesType2(vertexIndex).positionX / 255) * Me.theMdlFileData.hullMinPosition.y
					position.y = (aBodyModel.theVertexesType2(vertexIndex).positionY / 255) * Me.theMdlFileData.hullMinPosition.z
					position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * Me.theMdlFileData.hullMinPosition.x
					normal.x = (aBodyModel.theVertexesType2(vertexIndex).normalX / 255) * Me.theMdlFileData.hullMaxPosition.x
					normal.y = (aBodyModel.theVertexesType2(vertexIndex).normalY / 255) * Me.theMdlFileData.hullMaxPosition.y
					normal.z = (aBodyModel.theVertexesType2(vertexIndex).normalZ / 255) * Me.theMdlFileData.hullMaxPosition.z
				Else
					Dim debug As Integer = 4242
				End If

				line = "    "
				line += vertexIndex.ToString(TheApp.InternalNumberFormat)
				line += " "
				line += position.x.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += position.y.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += position.z.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += normal.x.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += normal.y.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += normal.z.ToString("0.000000", TheApp.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		Dim timeIndex As Integer
		Dim flexTimeIndex As Integer
		Dim aFlexFrame As FlexFrame2531

		Try
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
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

#End Region

#Region "Private Delegates"

#End Region

#Region "Private Methods"

	Private Sub WriteVertexAnimLines(ByVal aFlex As SourceMdlFlex2531, ByVal bodyAndMeshVertexIndexStart As Integer)
		Dim line As String
		Dim vertexIndex As Integer
		Dim position As New SourceVector()
		Dim normal As New SourceVector()
		Dim aBodyModel As SourceMdlModel2531

		Try
			aBodyModel = Me.theMdlFileData.theBodyParts(0).theModels(0)

			For i As Integer = 0 To aFlex.theVertAnims.Count - 1
				Dim aVertAnim As SourceMdlVertAnim2531
				aVertAnim = aFlex.theVertAnims(i)

				vertexIndex = aVertAnim.index + bodyAndMeshVertexIndexStart
				If aBodyModel.vertexListType = 0 Then
					position.x = aBodyModel.theVertexesType0(vertexIndex).position.x
					position.y = aBodyModel.theVertexesType0(vertexIndex).position.y
					position.z = aBodyModel.theVertexesType0(vertexIndex).position.z
					normal.x = aBodyModel.theVertexesType0(vertexIndex).normal.x
					normal.y = aBodyModel.theVertexesType0(vertexIndex).normal.y
					normal.z = aBodyModel.theVertexesType0(vertexIndex).normal.z
				ElseIf aBodyModel.vertexListType = 1 Then
					position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * Me.theMdlFileData.hullMinPosition.y
					position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * Me.theMdlFileData.hullMinPosition.z
					position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * Me.theMdlFileData.hullMinPosition.x
					normal.x = (aBodyModel.theVertexesType1(vertexIndex).normalX / 65535) * Me.theMdlFileData.hullMaxPosition.x
					normal.y = (aBodyModel.theVertexesType1(vertexIndex).normalY / 65535) * Me.theMdlFileData.hullMaxPosition.y
					normal.z = (aBodyModel.theVertexesType1(vertexIndex).normalZ / 65535) * Me.theMdlFileData.hullMaxPosition.z
				ElseIf aBodyModel.vertexListType = 2 Then
					position.x = (aBodyModel.theVertexesType2(vertexIndex).positionX / 255) * Me.theMdlFileData.hullMinPosition.y
					position.y = (aBodyModel.theVertexesType2(vertexIndex).positionY / 255) * Me.theMdlFileData.hullMinPosition.z
					position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * Me.theMdlFileData.hullMinPosition.x
					normal.x = (aBodyModel.theVertexesType2(vertexIndex).normalX / 255) * Me.theMdlFileData.hullMaxPosition.x
					normal.y = (aBodyModel.theVertexesType2(vertexIndex).normalY / 255) * Me.theMdlFileData.hullMaxPosition.y
					normal.z = (aBodyModel.theVertexesType2(vertexIndex).normalZ / 255) * Me.theMdlFileData.hullMaxPosition.z
				Else
					Dim debug As Integer = 4242
				End If

				'TEST: Values are too big.
				'position.x += aVertAnim.deltaX
				'position.y += aVertAnim.deltaY
				'position.z += aVertAnim.deltaZ
				'normal.x += aVertAnim.nDeltaX
				'normal.y += aVertAnim.nDeltaY
				'normal.z += aVertAnim.nDeltaZ
				'------
				'TEST: Values are too big, but seem to be somewhat close to what they should be.
				'position.x += (aVertAnim.deltaX / 255)
				'position.y += (aVertAnim.deltaY / 255)
				'position.z += (aVertAnim.deltaZ / 255)
				'normal.x += (aVertAnim.nDeltaX / 255)
				'normal.y += (aVertAnim.nDeltaY / 255)
				'normal.z += (aVertAnim.nDeltaZ / 255)
				'position.x += (aVertAnim.deltaY / 255)
				'position.y += (aVertAnim.deltaX / 255)
				'position.z += (aVertAnim.deltaZ / 255)
				'normal.x += (aVertAnim.nDeltaY / 255)
				'normal.y += (aVertAnim.nDeltaX / 255)
				'normal.z += (aVertAnim.nDeltaZ / 255)
				'TEST: Seems close, but pushes all flexes slightly to the right (when viewing from front).
				'position.x += (aVertAnim.deltaX / 2550)
				'position.y += (aVertAnim.deltaY / 2550)
				'position.z += (aVertAnim.deltaZ / 2550)
				'normal.x += (aVertAnim.nDeltaX / 2550)
				'normal.y += (aVertAnim.nDeltaY / 2550)
				'normal.z += (aVertAnim.nDeltaZ / 2550)
				'position.x += (aVertAnim.deltaX / 2550)
				'position.y += (aVertAnim.deltaZ / 2550)
				'position.z += (aVertAnim.deltaY / 2550)
				'normal.x += (aVertAnim.nDeltaX / 2550)
				'normal.y += (aVertAnim.nDeltaZ / 2550)
				'normal.z += (aVertAnim.nDeltaY / 2550)
				'position.x += (aVertAnim.deltaY / 2550)
				'position.y += (aVertAnim.deltaX / 2550)
				'position.z += (aVertAnim.deltaZ / 2550)
				'normal.x += (aVertAnim.nDeltaY / 2550)
				'normal.y += (aVertAnim.nDeltaX / 2550)
				'normal.z += (aVertAnim.nDeltaZ / 2550)
				'position.x += (aVertAnim.deltaY / 2550)
				'position.y += (aVertAnim.deltaZ / 2550)
				'position.z += (aVertAnim.deltaX / 2550)
				'normal.x += (aVertAnim.nDeltaY / 2550)
				'normal.y += (aVertAnim.nDeltaZ / 2550)
				'normal.z += (aVertAnim.nDeltaX / 2550)
				'position.x += (aVertAnim.deltaZ / 2550)
				'position.y += (aVertAnim.deltaX / 2550)
				'position.z += (aVertAnim.deltaY / 2550)
				'normal.x += (aVertAnim.nDeltaZ / 2550)
				'normal.y += (aVertAnim.nDeltaX / 2550)
				'normal.z += (aVertAnim.nDeltaY / 2550)
				'position.x += (aVertAnim.deltaZ / 2550)
				'position.y += (aVertAnim.deltaY / 2550)
				'position.z += (aVertAnim.deltaX / 2550)
				'normal.x += (aVertAnim.nDeltaZ / 2550)
				'normal.y += (aVertAnim.nDeltaY / 2550)
				'normal.z += (aVertAnim.nDeltaX / 2550)
				'TEST: [- 2550]
				'position.x -= (aVertAnim.deltaX / 2550)
				'position.y -= (aVertAnim.deltaY / 2550)
				'position.z -= (aVertAnim.deltaZ / 2550)
				'normal.x -= (aVertAnim.nDeltaX / 2550)
				'normal.y -= (aVertAnim.nDeltaY / 2550)
				'normal.z -= (aVertAnim.nDeltaZ / 2550)
				'TEST: [-zyx 2550]
				'position.x -= (aVertAnim.deltaZ / 2550)
				'position.y -= (aVertAnim.deltaY / 2550)
				'position.z -= (aVertAnim.deltaX / 2550)
				'normal.x -= (aVertAnim.nDeltaZ / 2550)
				'normal.y -= (aVertAnim.nDeltaY / 2550)
				'normal.z -= (aVertAnim.nDeltaX / 2550)
				'TEST: 
				'position.x -= (aVertAnim.deltaX / 2550)
				'position.y -= (aVertAnim.deltaY / 2550)
				'position.z -= (aVertAnim.deltaZ / 2550)
				'normal.x -= (aVertAnim.nDeltaX / 2550)
				'normal.y -= (aVertAnim.nDeltaY / 2550)
				'normal.z -= (aVertAnim.nDeltaZ / 2550)
				''TEST:
				'position.x -= (aVertAnim.deltaZ / 2550)
				'position.y -= (aVertAnim.deltaX / 2550)
				'position.z -= (aVertAnim.deltaY / 2550)
				'normal.x -= (aVertAnim.nDeltaZ / 2550)
				'normal.y -= (aVertAnim.nDeltaX / 2550)
				'normal.z -= (aVertAnim.nDeltaY / 2550)
				'TEST:
				'position.x -= (aVertAnim.deltaY / 2550)
				'position.y -= (aVertAnim.deltaX / 2550)
				'position.z -= (aVertAnim.deltaZ / 2550)
				'normal.x -= (aVertAnim.nDeltaY / 2550)
				'normal.y -= (aVertAnim.nDeltaX / 2550)
				'normal.z -= (aVertAnim.nDeltaZ / 2550)
				''TEST:
				'position.x -= (aVertAnim.deltaY / 327670)
				'position.y -= (aVertAnim.deltaX / 327670)
				'position.z -= (aVertAnim.deltaZ / 327670)
				'TEST: seems closest to being correct
				position.x -= (aVertAnim.deltaX / 327670)
				position.y -= (aVertAnim.deltaY / 327670)
				position.z -= (aVertAnim.deltaZ / 327670)
				'------
				'position.x += (aVertAnim.deltaX / 128)
				'position.y += (aVertAnim.deltaY / 128)
				'position.z += (aVertAnim.deltaZ / 128)
				'normal.x += (aVertAnim.nDeltaX / 128)
				'normal.y += (aVertAnim.nDeltaY / 128)
				'normal.z += (aVertAnim.nDeltaZ / 128)
				'position.x += (aVertAnim.deltaX / 128)
				'position.y += (aVertAnim.deltaY / 128)
				'position.z += (aVertAnim.deltaZ / 128)
				'normal.x += (aVertAnim.nDeltaX / 128)
				'normal.y += (aVertAnim.nDeltaY / 128)
				'normal.z += (aVertAnim.nDeltaZ / 128)
				'position.x += (aVertAnim.deltaX / 1280)
				'position.y += (aVertAnim.deltaY / 1280)
				'position.z += (aVertAnim.deltaZ / 1280)
				'normal.x += (aVertAnim.nDeltaX / 1280)
				'normal.y += (aVertAnim.nDeltaY / 1280)
				'normal.z += (aVertAnim.nDeltaZ / 1280)
				'position.x += (aVertAnim.deltaY / 1280)
				'position.y += (aVertAnim.deltaX / 1280)
				'position.z += (aVertAnim.deltaZ / 1280)
				'normal.x += (aVertAnim.nDeltaY / 1280)
				'normal.y += (aVertAnim.nDeltaX / 1280)
				'normal.z += (aVertAnim.nDeltaZ / 1280)
				'------
				'TEST: Values are too big, but maybe the SourceFloat8Bits calculations are wrong.
				'position.x += aVertAnim.flDelta(0).TheFloatValue
				'position.y += aVertAnim.flDelta(1).TheFloatValue
				'position.z += aVertAnim.flDelta(2).TheFloatValue
				'normal.x += aVertAnim.flNDelta(0).TheFloatValue
				'normal.y += aVertAnim.flNDelta(1).TheFloatValue
				'normal.z += aVertAnim.flNDelta(2).TheFloatValue
				'------
				'position.x += (aVertAnim.deltaX / 32767)
				'position.y += (aVertAnim.deltaY / 32767)
				'position.z += (aVertAnim.deltaZ / 32767)
				'normal.x = 1
				'normal.y = 1
				'normal.z = 1
				'------
				'position.x += aVertAnim.flDelta(0).TheFloatValue
				'position.y += aVertAnim.flDelta(1).TheFloatValue
				'position.z += aVertAnim.flDelta(2).TheFloatValue
				'normal.x = 1
				'normal.y = 1
				'normal.z = 1

				line = "    "
				line += vertexIndex.ToString(TheApp.InternalNumberFormat)
				line += " "
				line += position.x.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += position.y.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += position.z.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += normal.x.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += normal.y.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += normal.z.ToString("0.000000", TheApp.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

#End Region

#Region "Data"

	Private theOutputFileStreamWriter As StreamWriter
	Private theMdlFileData As SourceMdlFileData2531

#End Region

End Class
