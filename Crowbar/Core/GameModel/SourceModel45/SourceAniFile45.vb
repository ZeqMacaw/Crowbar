Imports System.IO

Public Class SourceAniFile45
	Inherits SourceMdlFile45

#Region "Creation and Destruction"

	Public Sub New(ByVal aniFileReader As BinaryReader, ByVal aniFileData As SourceFileData, ByVal mdlFileData As SourceFileData)
		Me.theInputFileReader = aniFileReader
		Me.theMdlFileData = CType(aniFileData, SourceMdlFileData45)
		Me.theRealMdlFileData = CType(mdlFileData, SourceMdlFileData45)

		Me.theMdlFileData.theFileSeekLog.FileSize = Me.theInputFileReader.BaseStream.Length

		'NOTE: Need the bone data from the real MDL file because SourceAniFile inherits SourceMdlFile.ReadMdlAnimation() that uses the data.
		Me.theMdlFileData.theBones = Me.theRealMdlFileData.theBones
	End Sub

#End Region

#Region "Methods"

	'FROM: SourceEngine2006_source\public\write.cpp
	'      WriteAnimations()
	'==============================================
	'for (i = 0; i < animcount; i++) 
	'{
	'	{
	'		static int iCurAnim = 0;

	'		// align all animation data to cache line boundaries
	'		ALIGN16( pBlockData );

	'		byte *pIkData = WriteAnimationData( srcanim, pBlockData );
	'		byte *pBlockEnd = WriteIkErrors( srcanim, pIkData );

	'		if (g_numanimblocks == 0)
	'		{
	'			g_numanimblocks = 1;
	'			// XBox, align each anim block to 512 for fast io
	'			byte *pBlockData2 = pBlockData;
	'			ALIGN512( pBlockData2 );

	'			int size = pBlockEnd - pBlockData;
	'			int shift = pBlockData2 - pBlockData;

	'			memmove( pBlockData2, pBlockData, size );
	'			memset( pBlockData, 0, shift );

	'			pBlockData = pBlockData2;
	'			pIkData = pIkData + shift;
	'			pBlockEnd = pBlockEnd + shift;

	'			g_animblock[g_numanimblocks].start = pBlockData;
	'			g_numanimblocks++;
	'		}
	'		else if (pBlockEnd - g_animblock[g_numanimblocks-1].start > g_animblocksize)
	'		{
	'			// the data we just wrote went over the boundry
	'			// XBox, align each anim block to 512 for fast io
	'			byte *pBlockData2 = pBlockData;
	'			ALIGN512( pBlockData2 );

	'			int size = pBlockEnd - pBlockData;
	'			int shift = pBlockData2 - pBlockData;

	'			memmove( pBlockData2, pBlockData, size );
	'			memset( pBlockData, 0, shift );

	'			pBlockData = pBlockData2;
	'			pIkData = pIkData + shift;
	'			pBlockEnd = pBlockEnd + shift;

	'			g_animblock[g_numanimblocks-1].end = pBlockData;
	'			g_animblock[g_numanimblocks].start = pBlockData;
	'			g_animblock[g_numanimblocks].iStartAnim = i;

	'			g_numanimblocks++;
	'			If (g_numanimblocks > MAXSTUDIOANIMBLOCKS) Then
	'			{
	'				MdlError( "Too many animation blocks\n");
	'			}
	'		}

	'		if ( i == animcount - 1 )
	'		{
	'			// fixup size for last block
	'			// XBox, align each anim block to 512 for fast io 
	'			ALIGN512( pBlockEnd );
	'		}
	'		g_animblock[g_numanimblocks-1].iEndAnim = i;
	'		g_animblock[g_numanimblocks-1].end = pBlockEnd;

	'		panimdesc[i].animblock	= IsChar( g_numanimblocks-1 );
	'		panimdesc[i].animindex	= IsInt24( pBlockData - g_animblock[panimdesc[i].animblock].start );
	'		panimdesc[i].numikrules = IsChar( srcanim->numikrules );
	'		panimdesc[i].animblockikruleindex = IsInt24( pIkData - g_animblock[panimdesc[i].animblock].start );
	'		pBlockData = pBlockEnd;
	'	}
	'}
	Public Sub ReadAnimationAniBlocks()
		If Me.theRealMdlFileData.theAnimationDescs IsNot Nothing Then
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim animInputFileStreamPosition As Long
			Dim animBlockInputFileStreamPosition As Long
			'Dim animBlockInputFileStreamEndPosition As Long
			Dim anAnimationDesc As SourceMdlAnimationDesc45
			'Dim aSectionOfAnimation As List(Of SourceMdlAnimation)

			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			For anAnimDescIndex As Integer = 0 To Me.theRealMdlFileData.theAnimationDescs.Count - 1
				'animInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, Me.theInputFileReader.BaseStream.Position - 1, 16, "theAnimationDesc alignment (animation block data - cache line boundaries)")
				animInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				anAnimationDesc = Me.theRealMdlFileData.theAnimationDescs(anAnimDescIndex)

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If anAnimationDesc.animBlock > 0 AndAlso ((anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) = 0) Then
					Dim sectionCount As Integer

					animBlockInputFileStreamPosition = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.animBlock).dataStart
					'animBlockInputFileStreamEndPosition = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.animBlock).dataEnd

					Try
						Dim sectionIndex As Integer
						sectionCount = anAnimationDesc.theSectionsOfAnimations.Count
						If anAnimationDesc.sectionOffset <> 0 AndAlso anAnimationDesc.sectionFrameCount > 0 Then
							Dim sectionFrameCount As Integer

							For sectionIndex = 0 To sectionCount - 1
								If sectionIndex < sectionCount - 1 Then
									sectionFrameCount = anAnimationDesc.sectionFrameCount
								Else
									sectionFrameCount = anAnimationDesc.frameCount - ((sectionCount - 1) * anAnimationDesc.sectionFrameCount)
								End If

								animBlockInputFileStreamPosition = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.theSections(sectionIndex).animBlock).dataStart
								'animBlockInputFileStreamEndPosition = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.theSections(sectionIndex).animBlock).dataEnd
								Me.ReadAniAnimation(animBlockInputFileStreamPosition + anAnimationDesc.theSections(sectionIndex).animOffset, anAnimationDesc, sectionFrameCount, sectionIndex)
							Next
						Else
							sectionIndex = 0
							Me.ReadAniAnimation(animBlockInputFileStreamPosition + anAnimationDesc.animOffset, anAnimationDesc, anAnimationDesc.frameCount, sectionIndex)
						End If

						If anAnimationDesc.ikRuleCount > 0 Then
							Me.ReadMdlIkRules(animBlockInputFileStreamPosition + anAnimationDesc.animblockIkRuleOffset, anAnimationDesc)
						End If
					Catch ex As Exception
						Dim debug As Integer = 4242
					End Try

					'If anAnimationDesc.ikRuleCount > 0 Then
					'	Me.ReadMdlIkRules(animInputFileStreamPosition, anAnimationDesc)
					'End If
				End If

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theAniFileData (animation block data) [this includes other logged data offsets]")
			Next
		End If
	End Sub

#End Region

#Region "Private Methods"

	Private Sub ReadAniAnimation(ByVal aniFileInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc45, ByVal sectionFrameCount As Integer, ByVal sectionIndex As Integer)
		Me.theInputFileReader.BaseStream.Seek(aniFileInputFileStreamPosition, SeekOrigin.Begin)

		Dim aSectionOfAnimation As List(Of SourceMdlAnimation)
		aSectionOfAnimation = anAnimationDesc.theSectionsOfAnimations(sectionIndex)
		Me.ReadMdlAnimation(Me.theInputFileReader.BaseStream.Position, anAnimationDesc, sectionFrameCount, aSectionOfAnimation)
	End Sub

#End Region

#Region "Data"

	'Protected theInputFileReader As BinaryReader

	'Protected theMdlFileData As SourceMdlFileData45
	Protected theRealMdlFileData As SourceMdlFileData45

#End Region

End Class
