Imports System.IO
Imports System.Text

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

#Region "Delegates"

	'Public Delegate Sub ReadAniAnimationDelegate(ByVal aniFileInputFileStreamPosition As Long, ByVal aniFileStreamEndPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc, ByVal sectionFrameCount As Integer, ByVal sectionIndex As Integer)

#End Region

#Region "Methods"

	'FROM: SourceEngine2006_source\public\write.cpp
	'      WriteAnimations()
	'==============================================
	'for (i = 0; i < animcount; i++) 
	'{
	'	{
	'		//ZM: This is for animations found in separate MDL files, i.e. $includemodel.

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
			Dim animBlockInputFileStreamPosition As Long
			Dim animBlockInputFileStreamEndPosition As Long
			Dim anAnimationDesc As SourceMdlAnimationDesc44

			For anAnimDescIndex As Integer = 0 To Me.theRealMdlFileData.theAnimationDescs.Count - 1
				anAnimationDesc = Me.theRealMdlFileData.theAnimationDescs(anAnimDescIndex)

				animBlockInputFileStreamPosition = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.animBlock).dataStart
				animBlockInputFileStreamEndPosition = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.animBlock).dataEnd

				Try
					Dim sectionIndex As Integer
					If anAnimationDesc.theSections IsNot Nothing AndAlso anAnimationDesc.theSections.Count > 0 Then
						Dim sectionFrameCount As Integer
						Dim sectionCount As Integer

						sectionCount = anAnimationDesc.theSections.Count
						For sectionIndex = 0 To sectionCount - 1
							If anAnimationDesc.theSections(sectionIndex).animBlock > 0 Then
								If sectionIndex < sectionCount - 2 Then
									sectionFrameCount = anAnimationDesc.sectionFrameCount
								Else
									sectionFrameCount = anAnimationDesc.frameCount - ((sectionCount - 2) * anAnimationDesc.sectionFrameCount)
								End If

								animBlockInputFileStreamPosition = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.theSections(sectionIndex).animBlock).dataStart
								animBlockInputFileStreamEndPosition = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.theSections(sectionIndex).animBlock).dataEnd
								Me.ReadAniAnimation(animBlockInputFileStreamPosition + anAnimationDesc.theSections(sectionIndex).animOffset, animBlockInputFileStreamEndPosition + anAnimationDesc.theSections(sectionIndex).animOffset, anAnimationDesc, sectionFrameCount, sectionIndex, (sectionIndex >= sectionCount - 2) Or (anAnimationDesc.frameCount = (sectionIndex + 1) * anAnimationDesc.sectionFrameCount))
							End If
						Next
					ElseIf anAnimationDesc.animBlock > 0 Then
						sectionIndex = 0
						Me.ReadAniAnimation(animBlockInputFileStreamPosition + anAnimationDesc.animOffset, animBlockInputFileStreamEndPosition + anAnimationDesc.animOffset, anAnimationDesc, anAnimationDesc.frameCount, sectionIndex, True)
					End If

					'NOTE: These seem to always be stored in the MDL file.
					'If anAnimationDesc.animBlock > 0 Then
					'	Me.ReadMdlIkRules(animBlockInputFileStreamPosition + anAnimationDesc.animblockIkRuleOffset, anAnimationDesc)
					'	Me.ReadLocalHierarchies(animBlockInputFileStreamPosition, anAnimationDesc)
					'End If
				Catch ex As Exception
					Dim debug As Integer = 4242
				End Try
			Next
		End If
	End Sub

	Public Overridable Sub ReadAniAnimation(ByVal aniFileInputFileStreamPosition As Long, ByVal aniFileStreamEndPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc44, ByVal sectionFrameCount As Integer, ByVal sectionIndex As Integer, ByVal lastSectionIsBeingRead As Boolean)
		Dim aSectionOfAnimation As List(Of SourceMdlAnimation)
		aSectionOfAnimation = anAnimationDesc.theSectionsOfAnimations(sectionIndex)
		Me.ReadMdlAnimation(aniFileInputFileStreamPosition, anAnimationDesc, sectionFrameCount, aSectionOfAnimation, lastSectionIsBeingRead)
	End Sub

#End Region

#Region "Private Methods"

#End Region

#Region "Data"

	'Protected theInputFileReader As BinaryReader

	'Protected theMdlFileData As SourceMdlFileData44
	Protected theRealMdlFileData As SourceMdlFileData44

#End Region

End Class
