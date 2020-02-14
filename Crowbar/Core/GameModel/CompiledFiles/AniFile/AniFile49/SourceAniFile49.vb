Imports System.IO

Public Class SourceAniFile49
	Inherits SourceMdlFile49

#Region "Creation and Destruction"

	Public Sub New(ByVal aniFileReader As BinaryReader, ByVal aniFileData As SourceFileData, ByVal mdlFileData As SourceFileData)
		Me.theInputFileReader = aniFileReader
		Me.theMdlFileData = CType(aniFileData, SourceMdlFileData49)
		Me.theRealMdlFileData = CType(mdlFileData, SourceMdlFileData49)

		Me.theMdlFileData.theFileSeekLog.FileSize = Me.theInputFileReader.BaseStream.Length

		'NOTE: Need the bone data from the real MDL file because SourceAniFile inherits SourceMdlFile.ReadMdlAnimation() that uses the data.
		Me.theMdlFileData.theBones = Me.theRealMdlFileData.theBones
	End Sub

#End Region

#Region "Methods"

	'TODO: [2015-08-16] Currently the same as SourceAniFile48. Not sure how to share the code while still having the two versions call different ReadAniAnimation().
	'Public Sub ReadAniBlocks(ByVal delegateReadAniAnimation As ReadAniAnimationDelegate)
	Public Sub ReadAnimationAniBlocks()
		If Me.theRealMdlFileData.theAnimationDescs IsNot Nothing Then
			For Each anAnimationDesc As SourceMdlAnimationDesc49 In Me.theRealMdlFileData.theAnimationDescs
				Try
					Dim animBlockInputFileStreamPosition As Long = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.animBlock).dataStart
					'Dim animBlockInputFileStreamEndPosition As Long = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.animBlock).dataEnd
					Dim sectionIndex As Integer

					'NOTE: There can be sections in ANI file even if anAnimationDesc.animBlock = 0.
					If anAnimationDesc.theSections IsNot Nothing AndAlso anAnimationDesc.theSections.Count > 0 Then
						Dim sectionCount As Integer = anAnimationDesc.theSections.Count
						Dim sectionFrameCount As Integer
						Dim section As SourceMdlAnimationSection

						For sectionIndex = 0 To sectionCount - 1
							section = anAnimationDesc.theSections(sectionIndex)
							If section.animBlock > 0 Then
								If sectionIndex < sectionCount - 2 Then
									sectionFrameCount = anAnimationDesc.sectionFrameCount
								Else
									'NOTE: Due to the weird calculation of sectionCount in studiomdl, this line is called twice, which means there are two "last" sections.
									'      This also likely means that the last section is bogus unused data.
									sectionFrameCount = anAnimationDesc.frameCount - ((sectionCount - 2) * anAnimationDesc.sectionFrameCount)
								End If

								animBlockInputFileStreamPosition = Me.theRealMdlFileData.theAnimBlocks(section.animBlock).dataStart
								'animBlockInputFileStreamEndPosition = Me.theRealMdlFileData.theAnimBlocks(section.animBlock).dataEnd
								Me.ReadAnimationFrames(animBlockInputFileStreamPosition + section.animOffset, anAnimationDesc, sectionFrameCount, sectionIndex, (sectionIndex >= sectionCount - 2) Or (anAnimationDesc.frameCount = (sectionIndex + 1) * anAnimationDesc.sectionFrameCount))
							End If
						Next
					ElseIf anAnimationDesc.animBlock > 0 Then
						sectionIndex = 0
						Me.ReadAnimationFrames(animBlockInputFileStreamPosition + anAnimationDesc.animOffset, anAnimationDesc, anAnimationDesc.frameCount, sectionIndex, True)
					End If

					'NOTE: These seem to always be stored in the MDL file for MDL44.
					If Me.theMdlFileData.version <> 44 AndAlso anAnimationDesc.animBlock > 0 Then
						Me.ReadMdlIkRules(animBlockInputFileStreamPosition + anAnimationDesc.animblockIkRuleOffset, anAnimationDesc)
						Me.ReadLocalHierarchies(animBlockInputFileStreamPosition, anAnimationDesc)
					End If
				Catch ex As Exception
					Dim debug As Integer = 4242
				End Try
			Next
		End If
	End Sub

#End Region

#Region "Private Methods"

#End Region

#Region "Data"

	Protected theRealMdlFileData As SourceMdlFileData49

#End Region

End Class
