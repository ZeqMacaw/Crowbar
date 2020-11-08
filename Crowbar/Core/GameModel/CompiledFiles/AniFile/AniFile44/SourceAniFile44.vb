Imports System.IO

Public Class SourceAniFile44
	Inherits SourceMdlFile44

#Region "Creation and Destruction"

	Public Sub New(ByVal aniFileReader As BinaryReader, ByVal aniFileData As SourceFileData, ByVal mdlFileData As SourceFileData)
		Me.theInputFileReader = aniFileReader
		Me.theMdlFileData = CType(aniFileData, SourceMdlFileData44)
		Me.theRealMdlFileData = CType(mdlFileData, SourceMdlFileData44)

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
			For Each anAnimationDesc As SourceMdlAnimationDesc44 In Me.theRealMdlFileData.theAnimationDescs
				Try
					Dim animBlockInputFileStreamPosition As Long = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.animBlock).dataStart
					'Dim animBlockInputFileStreamEndPosition As Long = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.animBlock).dataEnd
					Dim sectionIndex As Integer

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

	Protected theRealMdlFileData As SourceMdlFileData44

#End Region

End Class
