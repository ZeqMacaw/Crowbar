Imports System.IO

Public Class SourceSmdFile14

#Region "Creation and Destruction"

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData14)
		Me.theOutputFileStreamWriter = outputFileStream
		Me.theMdlFileData = mdlFileData
	End Sub

#End Region

#Region "Methods"

	'NOTE: Half-Life SDK model compiler does not allow comments in an SMD file.
	'Public Sub WriteHeaderComment()
	'	Common.WriteHeaderComment(Me.theOutputFileStreamWriter)
	'End Sub

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

	Public Sub WriteSkeletonSection()
		Dim line As String = ""

		'skeleton
		line = "skeleton"
		Me.theOutputFileStreamWriter.WriteLine(line)

		If TheApp.Settings.DecompileStricterFormatIsChecked Then
			line = "time 0"
		Else
			line = "  time 0"
		End If
		Me.theOutputFileStreamWriter.WriteLine(line)
		For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
			line = "    "
			line += boneIndex.ToString(TheApp.InternalNumberFormat)
			line += " "
			line += Me.theMdlFileData.theBones(boneIndex).position.x.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += Me.theMdlFileData.theBones(boneIndex).position.y.ToString("0.000000", TheApp.InternalNumberFormat)
			'line += Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).positionY.ToString("0.000000", TheApp.InternalNumberFormat)
			'line += " "
			'line += (-Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).positionX).ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += Me.theMdlFileData.theBones(boneIndex).position.z.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += Me.theMdlFileData.theBones(boneIndex).rotation.x.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += Me.theMdlFileData.theBones(boneIndex).rotation.y.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += Me.theMdlFileData.theBones(boneIndex).rotation.z.ToString("0.000000", TheApp.InternalNumberFormat)
			Me.theOutputFileStreamWriter.WriteLine(line)
		Next

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteSkeletonSectionForAnimation(ByVal aSequenceDesc As SourceMdlSequenceDesc10, ByVal blendIndex As Integer)
		Dim line As String = ""
		Dim boneIndex As Integer
		Dim aFrameLine As AnimationFrameLine
		Dim position As New SourceVector()
		Dim rotation As New SourceVector()
		Dim scale As Double
		Dim tempValue As Double

		'skeleton
		line = "skeleton"
		Me.theOutputFileStreamWriter.WriteLine(line)

		Me.theAnimationFrameLines = New SortedList(Of Integer, AnimationFrameLine)()
		For frameIndex As Integer = 0 To aSequenceDesc.frameCount - 1
			Me.theAnimationFrameLines.Clear()
			Me.CalcAnimation(aSequenceDesc, blendIndex, frameIndex)

			If TheApp.Settings.DecompileStricterFormatIsChecked Then
				line = "time "
			Else
				line = "  time "
			End If
			line += CStr(frameIndex)
			Me.theOutputFileStreamWriter.WriteLine(line)

			For i As Integer = 0 To Me.theAnimationFrameLines.Count - 1
				boneIndex = Me.theAnimationFrameLines.Keys(i)
				aFrameLine = Me.theAnimationFrameLines.Values(i)

				position.x = aFrameLine.position.x
				position.y = aFrameLine.position.y
				position.z = aFrameLine.position.z
				If Me.theMdlFileData.theBones(boneIndex).parentBoneIndex = -1 Then
					'void ExtractMotion( )
					'[...]
					'			for (j = 0; j < sequence[i].numframes; j++)
					'[...]
					'						VectorScale( motion, j * 1.0 / (sequence[i].numframes - 1), adj );
					'[...]
					'							VectorSubtract( sequence[i].panim[q]->pos[k][j], adj, sequence[i].panim[q]->pos[k][j] );
					'[...]
					'			VectorCopy( motion, sequence[i].linearmovement );
					scale = frameIndex / (aSequenceDesc.frameCount - 1)
					If (aSequenceDesc.motiontype And SourceModule10.STUDIO_LX) = SourceModule10.STUDIO_LX Then
						position.x += scale * aSequenceDesc.linearmovement.x
					End If
					If (aSequenceDesc.motiontype And SourceModule10.STUDIO_LY) = SourceModule10.STUDIO_LY Then
						position.y += scale * aSequenceDesc.linearmovement.y
					End If
					If (aSequenceDesc.motiontype And SourceModule10.STUDIO_LZ) = SourceModule10.STUDIO_LZ Then
						position.z += scale * aSequenceDesc.linearmovement.z
					End If

					'	defaultzrotation = Q_PI / 2;
					'int Cmd_Sequence( )
					'[...]
					'	zrotation = defaultzrotation;
					'void Option_Rotate(void )
					'{
					'	GetToken (false);
					'	zrotation = (atof( token ) + 90) * (Q_PI / 180.0);
					'}
					'void Grab_Animation( s_animation_t *panim)
					'[...]
					'	cz = cos( zrotation );
					'	sz = sin( zrotation );
					'[...]
					'				if (panim->node[index].parent == -1) {
					'[...]
					'					panim->pos[index][t][0] = cz * pos[0] - sz * pos[1];
					'					panim->pos[index][t][1] = sz * pos[0] + cz * pos[1];
					'					panim->pos[index][t][2] = pos[2];
					'[...]
					'				}
					'NOTE: cos(90) = 0; sin(90) = 1
					tempValue = position.x
					position.x = position.y
					position.y = -tempValue
				End If

				rotation.x = aFrameLine.rotation.x
				rotation.y = aFrameLine.rotation.y
				If Me.theMdlFileData.theBones(boneIndex).parentBoneIndex = -1 Then
					'	defaultzrotation = Q_PI / 2;
					'int Cmd_Sequence( )
					'[...]
					'	zrotation = defaultzrotation;
					'void Option_Rotate(void )
					'{
					'	GetToken (false);
					'	zrotation = (atof( token ) + 90) * (Q_PI / 180.0);
					'}
					'void Grab_Animation( s_animation_t *panim)
					'[...]
					'				if (panim->node[index].parent == -1) {
					'[...]
					'					// rotate model
					'					rot[2]			+= zrotation;
					'				}
					rotation.z = aFrameLine.rotation.z + MathModule.DegreesToRadians(-90)
				Else
					rotation.z = aFrameLine.rotation.z
				End If

				line = "    "
				line += boneIndex.ToString(TheApp.InternalNumberFormat)

				line += " "
				line += position.x.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += position.y.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += position.z.ToString("0.000000", TheApp.InternalNumberFormat)

				line += " "
				line += rotation.x.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += rotation.y.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += rotation.z.ToString("0.000000", TheApp.InternalNumberFormat)

				'If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
				'	line += "   # "
				'	line += "pos: "
				'	line += aFrameLine.position.debug_text
				'	line += "   "
				'	line += "rot: "
				'	line += aFrameLine.rotation.debug_text
				'End If

				Me.theOutputFileStreamWriter.WriteLine(line)
			Next
		Next

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteTrianglesSection(ByVal aBodyModel As SourceMdlModel14)
		Dim line As String = ""
		Dim materialLine As String = ""
		Dim vertex1Line As String = ""
		Dim vertex2Line As String = ""
		Dim vertex3Line As String = ""
		'Dim materialIndex As Integer
		'Dim materialName As String
		'Dim aMesh As SourceMdlMesh14
		'Dim aTexture As SourceMdlTexture14
		Dim boneIndex As Integer
		Dim aVertexIndex As Integer

		'triangles
		line = "triangles"
		Me.theOutputFileStreamWriter.WriteLine(line)

		Try
			'If aBodyModel.theMeshes IsNot Nothing Then
			'	For meshIndex As Integer = 0 To aBodyModel.theMeshes.Count - 1
			'		aMesh = aBodyModel.theMeshes(meshIndex)
			'		materialIndex = aMesh.skinref
			'		aTexture = Me.theMdlFileData.theTextures(materialIndex)
			'		materialName = aTexture.theFileName

			'		'For groupIndex As Integer = 1 To aStripOrFan.theVertexInfos.Count - 2
			'		'	materialLine = materialName

			'		'	vertex1Line = Me.GetVertexLine(aBodyModel, aStripOrFan.theVertexInfos(0), aTexture)
			'		'	vertex2Line = Me.GetVertexLine(aBodyModel, aStripOrFan.theVertexInfos(groupIndex + 1), aTexture)
			'		'	vertex3Line = Me.GetVertexLine(aBodyModel, aStripOrFan.theVertexInfos(groupIndex), aTexture)

			'		'	If vertex1Line.StartsWith("// ") OrElse vertex2Line.StartsWith("// ") OrElse vertex3Line.StartsWith("// ") Then
			'		'		materialLine = "// " + materialLine
			'		'		If Not vertex1Line.StartsWith("// ") Then
			'		'			vertex1Line = "// " + vertex1Line
			'		'		End If
			'		'		If Not vertex2Line.StartsWith("// ") Then
			'		'			vertex2Line = "// " + vertex2Line
			'		'		End If
			'		'		If Not vertex3Line.StartsWith("// ") Then
			'		'			vertex3Line = "// " + vertex3Line
			'		'		End If
			'		'	End If
			'		'	Me.theOutputFileStreamWriter.WriteLine(materialLine)
			'		'	Me.theOutputFileStreamWriter.WriteLine(vertex1Line)
			'		'	Me.theOutputFileStreamWriter.WriteLine(vertex2Line)
			'		'	Me.theOutputFileStreamWriter.WriteLine(vertex3Line)
			'		'Next
			'	Next
			'End If
			For aVertexIndexIndex As Integer = 0 To Me.theMdlFileData.theIndexes.Count - 3 Step 3
				materialLine = Me.theMdlFileData.theTextures(0).theTextureName

				boneIndex = 0

				aVertexIndex = Me.theMdlFileData.theIndexes(aVertexIndexIndex)
				vertex1Line = Me.GetVertexLine(boneIndex, Me.theMdlFileData.theVertexes(aVertexIndex), Me.theMdlFileData.theNormals(aVertexIndex), Me.theMdlFileData.theUVs(aVertexIndex), aBodyModel.theWeightingHeaders(0), Me.theMdlFileData.theWeightings(aVertexIndex))
				aVertexIndex = Me.theMdlFileData.theIndexes(aVertexIndexIndex + 2)
				vertex2Line = Me.GetVertexLine(boneIndex, Me.theMdlFileData.theVertexes(aVertexIndex), Me.theMdlFileData.theNormals(aVertexIndex), Me.theMdlFileData.theUVs(aVertexIndex), aBodyModel.theWeightingHeaders(0), Me.theMdlFileData.theWeightings(aVertexIndex))
				aVertexIndex = Me.theMdlFileData.theIndexes(aVertexIndexIndex + 1)
				vertex3Line = Me.GetVertexLine(boneIndex, Me.theMdlFileData.theVertexes(aVertexIndex), Me.theMdlFileData.theNormals(aVertexIndex), Me.theMdlFileData.theUVs(aVertexIndex), aBodyModel.theWeightingHeaders(0), Me.theMdlFileData.theWeightings(aVertexIndex))

				If vertex1Line.StartsWith("// ") OrElse vertex2Line.StartsWith("// ") OrElse vertex3Line.StartsWith("// ") Then
					materialLine = "// " + materialLine
					If Not vertex1Line.StartsWith("// ") Then
						vertex1Line = "// " + vertex1Line
					End If
					If Not vertex2Line.StartsWith("// ") Then
						vertex2Line = "// " + vertex2Line
					End If
					If Not vertex3Line.StartsWith("// ") Then
						vertex3Line = "// " + vertex3Line
					End If
				End If
				Me.theOutputFileStreamWriter.WriteLine(materialLine)
				Me.theOutputFileStreamWriter.WriteLine(vertex1Line)
				Me.theOutputFileStreamWriter.WriteLine(vertex2Line)
				Me.theOutputFileStreamWriter.WriteLine(vertex3Line)
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

	Private Function GetVertexLine(ByVal boneIndex As Integer, ByVal position As SourceVector, ByVal normal As SourceVector, ByVal uv As SourceVector, ByVal weightingHeader As SourceMdlWeightingHeader14, ByVal weighting As SourceMdlWeighting14) As String
		Dim line As String

		line = ""
		Try
			line = "  "
			line += boneIndex.ToString(TheApp.InternalNumberFormat)

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

			line += " "
			line += uv.x.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += (-uv.y).ToString("0.000000", TheApp.InternalNumberFormat)

			If weighting.boneCount > 0 Then
				line += " "
				line += weighting.boneCount.ToString(TheApp.InternalNumberFormat)
				For x As Integer = 0 To weighting.boneCount - 1
					line += " "
					line += weightingHeader.theWeightingBoneDatas(0).theWeightingBoneIndexes(weighting.bones(x)).ToString(TheApp.InternalNumberFormat)
					line += " "
					line += weighting.weights(x).ToString("0.000000", TheApp.InternalNumberFormat)
				Next
			End If
		Catch ex As Exception
			line = "// " + line
		End Try

		Return line
	End Function

	'static void CalcAnimation( const CStudioHdr *pStudioHdr,	Vector *pos, Quaternion *q, 
	'	mstudioseqdesc_t &seqdesc,
	'	int sequence, int animation,
	'	float cycle, int boneMask )
	'{
	'	int					i;
	'
	'	mstudioanimdesc_t &animdesc = pStudioHdr->pAnimdesc( animation );
	'	mstudiobone_t *pbone = pStudioHdr->pBone( 0 );
	'	mstudioanim_t *panim = animdesc.pAnim( );
	'
	'	int					iFrame;
	'	float				s;
	'
	'	float fFrame = cycle * (animdesc.numframes - 1);
	'
	'	iFrame = (int)fFrame;
	'	s = (fFrame - iFrame);
	'
	'	float *pweight = seqdesc.pBoneweight( 0 );
	'
	'	for (i = 0; i < pStudioHdr->numbones(); i++, pbone++, pweight++)
	'	{
	'		if (panim && panim->bone == i)
	'		{
	'			if (*pweight > 0 && (pbone->flags & boneMask))
	'			{
	'				CalcBoneQuaternion( iFrame, s, pbone, panim, q[i] );
	'				CalcBonePosition  ( iFrame, s, pbone, panim, pos[i] );
	'			}
	'			panim = panim->pNext();
	'		}
	'		else if (*pweight > 0 && (pbone->flags & boneMask))
	'		{
	'			if (animdesc.flags & STUDIO_DELTA)
	'			{
	'				q[i].Init( 0.0f, 0.0f, 0.0f, 1.0f );
	'				pos[i].Init( 0.0f, 0.0f, 0.0f );
	'			}
	'			else
	'			{
	'				q[i] = pbone->quat;
	'				pos[i] = pbone->pos;
	'			}
	'		}
	'	}
	'}
	'======
	'FROM: SourceEngine2007_source\src_main\public\bone_setup.cpp
	'//-----------------------------------------------------------------------------
	'// Purpose: Find and decode a sub-frame of animation
	'//-----------------------------------------------------------------------------
	'
	'static void CalcAnimation( const CStudioHdr *pStudioHdr,	Vector *pos, Quaternion *q, 
	'	mstudioseqdesc_t &seqdesc,
	'	int sequence, int animation,
	'	float cycle, int boneMask )
	'{
	'#ifdef STUDIO_ENABLE_PERF_COUNTERS
	'	pStudioHdr->m_nPerfAnimationLayers++;
	'#endif
	'
	'	virtualmodel_t *pVModel = pStudioHdr->GetVirtualModel();
	'
	'	if (pVModel)
	'	{
	'		CalcVirtualAnimation( pVModel, pStudioHdr, pos, q, seqdesc, sequence, animation, cycle, boneMask );
	'		return;
	'	}
	'
	'	mstudioanimdesc_t &animdesc = pStudioHdr->pAnimdesc( animation );
	'	mstudiobone_t *pbone = pStudioHdr->pBone( 0 );
	'	const mstudiolinearbone_t *pLinearBones = pStudioHdr->pLinearBones();
	'
	'	int					i;
	'	int					iFrame;
	'	float				s;
	'
	'	float fFrame = cycle * (animdesc.numframes - 1);
	'
	'	iFrame = (int)fFrame;
	'	s = (fFrame - iFrame);
	'
	'	int iLocalFrame = iFrame;
	'	float flStall;
	'	mstudioanim_t *panim = animdesc.pAnim( &iLocalFrame, flStall );
	'
	'	float *pweight = seqdesc.pBoneweight( 0 );
	'
	'	// if the animation isn't available, look for the zero frame cache
	'	if (!panim)
	'	{
	'		// Msg("zeroframe %s\n", animdesc.pszName() );
	'		// pre initialize
	'		for (i = 0; i < pStudioHdr->numbones(); i++, pbone++, pweight++)
	'		{
	'			if (*pweight > 0 && (pStudioHdr->boneFlags(i) & boneMask))
	'			{
	'				if (animdesc.flags & STUDIO_DELTA)
	'				{
	'					q[i].Init( 0.0f, 0.0f, 0.0f, 1.0f );
	'					pos[i].Init( 0.0f, 0.0f, 0.0f );
	'				}
	'				else
	'				{
	'					q[i] = pbone->quat;
	'					pos[i] = pbone->pos;
	'				}
	'			}
	'		}
	'
	'		CalcZeroframeData( pStudioHdr, pStudioHdr->GetRenderHdr(), NULL, pStudioHdr->pBone( 0 ), animdesc, fFrame, pos, q, boneMask, 1.0 );
	'
	'		return;
	'	}
	'
	'	// BUGBUG: the sequence, the anim, and the model can have all different bone mappings.
	'	for (i = 0; i < pStudioHdr->numbones(); i++, pbone++, pweight++)
	'	{
	'		if (panim && panim->bone == i)
	'		{
	'			if (*pweight > 0 && (pStudioHdr->boneFlags(i) & boneMask))
	'			{
	'				CalcBoneQuaternion( iLocalFrame, s, pbone, pLinearBones, panim, q[i] );
	'				CalcBonePosition  ( iLocalFrame, s, pbone, pLinearBones, panim, pos[i] );
	'#ifdef STUDIO_ENABLE_PERF_COUNTERS
	'				pStudioHdr->m_nPerfAnimatedBones++;
	'				pStudioHdr->m_nPerfUsedBones++;
	'#endif
	'			}
	'			panim = panim->pNext();
	'		}
	'		else if (*pweight > 0 && (pStudioHdr->boneFlags(i) & boneMask))
	'		{
	'			if (animdesc.flags & STUDIO_DELTA)
	'			{
	'				q[i].Init( 0.0f, 0.0f, 0.0f, 1.0f );
	'				pos[i].Init( 0.0f, 0.0f, 0.0f );
	'			}
	'			else
	'			{
	'				q[i] = pbone->quat;
	'				pos[i] = pbone->pos;
	'			}
	'#ifdef STUDIO_ENABLE_PERF_COUNTERS
	'			pStudioHdr->m_nPerfUsedBones++;
	'#endif
	'		}
	'	}
	'
	'	// cross fade in previous zeroframe data
	'	if (flStall > 0.0f)
	'	{
	'		CalcZeroframeData( pStudioHdr, pStudioHdr->GetRenderHdr(), NULL, pStudioHdr->pBone( 0 ), animdesc, fFrame, pos, q, boneMask, flStall );
	'	}
	'
	'	if (animdesc.numlocalhierarchy)
	'	{
	'		matrix3x4_t *boneToWorld = g_MatrixPool.Alloc();
	'		CBoneBitList boneComputed;
	'
	'		int i;
	'		for (i = 0; i < animdesc.numlocalhierarchy; i++)
	'		{
	'			mstudiolocalhierarchy_t *pHierarchy = animdesc.pHierarchy( i );
	'
	'			if ( !pHierarchy )
	'				break;
	'
	'			if (pStudioHdr->boneFlags(pHierarchy->iBone) & boneMask)
	'			{
	'				if (pStudioHdr->boneFlags(pHierarchy->iNewParent) & boneMask)
	'				{
	'					CalcLocalHierarchyAnimation( pStudioHdr, boneToWorld, boneComputed, pos, q, pbone, pHierarchy, pHierarchy->iBone, pHierarchy->iNewParent, cycle, iFrame, s, boneMask );
	'				}
	'			}
	'		}
	'
	'		g_MatrixPool.Free( boneToWorld );
	'	}
	'
	'}
	Private Sub CalcAnimation(ByVal aSequenceDesc As SourceMdlSequenceDesc10, ByVal blendIndex As Integer, ByVal frameIndex As Integer)
		Dim s As Double
		Dim aBone As SourceMdlBone10
		Dim anAnimation As SourceMdlAnimation10
		Dim rot As SourceVector
		Dim pos As SourceVector
		Dim aFrameLine As AnimationFrameLine

		s = 0

		For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
			aBone = Me.theMdlFileData.theBones(boneIndex)
			anAnimation = aSequenceDesc.theAnimations(blendIndex * Me.theMdlFileData.theBones.Count + boneIndex)

			If anAnimation IsNot Nothing Then
				If Me.theAnimationFrameLines.ContainsKey(boneIndex) Then
					aFrameLine = Me.theAnimationFrameLines(boneIndex)
				Else
					aFrameLine = New AnimationFrameLine()
					Me.theAnimationFrameLines.Add(boneIndex, aFrameLine)
				End If

				aFrameLine.rotationQuat = New SourceQuaternion()
				rot = CalcBoneRotation(frameIndex, s, aBone, anAnimation, aFrameLine.rotationQuat)
				aFrameLine.rotation = New SourceVector()

				aFrameLine.rotation.x = rot.x
				aFrameLine.rotation.y = rot.y
				aFrameLine.rotation.z = rot.z

				aFrameLine.rotation.debug_text = rot.debug_text

				pos = Me.CalcBonePosition(frameIndex, s, aBone, anAnimation)
				aFrameLine.position = New SourceVector()
				aFrameLine.position.x = pos.x
				aFrameLine.position.y = pos.y
				aFrameLine.position.z = pos.z
				aFrameLine.position.debug_text = pos.debug_text
			End If
		Next
	End Sub

	'FROM: SourceEngine2007_source\public\bone_setup.cpp
	'//-----------------------------------------------------------------------------
	'// Purpose: return a sub frame rotation for a single bone
	'//-----------------------------------------------------------------------------
	'void CalcBoneQuaternion( int frame, float s, 
	'						const Quaternion &baseQuat, const RadianEuler &baseRot, const Vector &baseRotScale, 
	'						int iBaseFlags, const Quaternion &baseAlignment, 
	'						const mstudioanim_t *panim, Quaternion &q )
	'{
	'	if ( panim->flags & STUDIO_ANIM_RAWROT )
	'	{
	'		q = *(panim->pQuat48());
	'		Assert( q.IsValid() );
	'		return;
	'	} 

	'	if ( panim->flags & STUDIO_ANIM_RAWROT2 )
	'	{
	'		q = *(panim->pQuat64());
	'		Assert( q.IsValid() );
	'		return;
	'	}

	'	if ( !(panim->flags & STUDIO_ANIM_ANIMROT) )
	'	{
	'		if (panim->flags & STUDIO_ANIM_DELTA)
	'		{
	'			q.Init( 0.0f, 0.0f, 0.0f, 1.0f );
	'		}
	'		else
	'		{
	'			q = baseQuat;
	'		}
	'		return;
	'	}

	'	mstudioanim_valueptr_t *pValuesPtr = panim->pRotV();

	'	if (s > 0.001f)
	'	{
	'		QuaternionAligned	q1, q2;
	'		RadianEuler			angle1, angle2;

	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 0 ), baseRotScale.x, angle1.x, angle2.x );
	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 1 ), baseRotScale.y, angle1.y, angle2.y );
	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 2 ), baseRotScale.z, angle1.z, angle2.z );

	'		if (!(panim->flags & STUDIO_ANIM_DELTA))
	'		{
	'			angle1.x = angle1.x + baseRot.x;
	'			angle1.y = angle1.y + baseRot.y;
	'			angle1.z = angle1.z + baseRot.z;
	'			angle2.x = angle2.x + baseRot.x;
	'			angle2.y = angle2.y + baseRot.y;
	'			angle2.z = angle2.z + baseRot.z;
	'		}

	'		Assert( angle1.IsValid() && angle2.IsValid() );
	'		if (angle1.x != angle2.x || angle1.y != angle2.y || angle1.z != angle2.z)
	'		{
	'			AngleQuaternion( angle1, q1 );
	'			AngleQuaternion( angle2, q2 );

	'	#ifdef _X360
	'			fltx4 q1simd, q2simd, qsimd;
	'			q1simd = LoadAlignedSIMD( q1 );
	'			q2simd = LoadAlignedSIMD( q2 );
	'			qsimd = QuaternionBlendSIMD( q1simd, q2simd, s );
	'			StoreUnalignedSIMD( q.Base(), qsimd );
	'	#else
	'			QuaternionBlend( q1, q2, s, q );
	'#End If
	'		}
	'		else
	'		{
	'			AngleQuaternion( angle1, q );
	'		}
	'	}
	'	else
	'	{
	'		RadianEuler			angle;

	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 0 ), baseRotScale.x, angle.x );
	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 1 ), baseRotScale.y, angle.y );
	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 2 ), baseRotScale.z, angle.z );

	'		if (!(panim->flags & STUDIO_ANIM_DELTA))
	'		{
	'			angle.x = angle.x + baseRot.x;
	'			angle.y = angle.y + baseRot.y;
	'			angle.z = angle.z + baseRot.z;
	'		}

	'		Assert( angle.IsValid() );
	'		AngleQuaternion( angle, q );
	'	}

	'	Assert( q.IsValid() );

	'	// align to unified bone
	'	if (!(panim->flags & STUDIO_ANIM_DELTA) && (iBaseFlags & BONE_FIXED_ALIGNMENT))
	'	{
	'		QuaternionAlign( baseAlignment, q, q );
	'	}
	'}
	'
	'inline void CalcBoneQuaternion( int frame, float s, 
	'						const mstudiobone_t *pBone,
	'						const mstudiolinearbone_t *pLinearBones,
	'						const mstudioanim_t *panim, Quaternion &q )
	'{
	'	if (pLinearBones)
	'	{
	'		CalcBoneQuaternion( frame, s, pLinearBones->quat(panim->bone), pLinearBones->rot(panim->bone), pLinearBones->rotscale(panim->bone), pLinearBones->flags(panim->bone), pLinearBones->qalignment(panim->bone), panim, q );
	'	}
	'	else
	'	{
	'		CalcBoneQuaternion( frame, s, pBone->quat, pBone->rot, pBone->rotscale, pBone->flags, pBone->qAlignment, panim, q );
	'	}
	'}
	'Private Function CalcBoneQuaternion(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone, ByVal anAnimation As SourceMdlAnimation) As SourceQuaternion
	'	Dim rot As New SourceQuaternion()
	'	Dim angleVector As New SourceVector()

	'	If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT) > 0 Then
	'		rot.x = anAnimation.theRot48.x
	'		rot.y = anAnimation.theRot48.y
	'		rot.z = anAnimation.theRot48.z
	'		rot.w = anAnimation.theRot48.w
	'		Return rot
	'	ElseIf (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT2) > 0 Then
	'		rot.x = anAnimation.theRot64.x
	'		rot.y = anAnimation.theRot64.y
	'		rot.z = anAnimation.theRot64.z
	'		rot.w = anAnimation.theRot64.w
	'		Return rot
	'	End If

	'	If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMROT) = 0 Then
	'		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0 Then
	'			rot.x = 0
	'			rot.y = 0
	'			rot.z = 0
	'			rot.w = 1
	'		Else
	'			rot.x = aBone.quat.x
	'			rot.y = aBone.quat.y
	'			rot.z = aBone.quat.z
	'			rot.w = aBone.quat.w
	'		End If
	'		Return rot
	'	End If

	'	Dim rotV As SourceMdlAnimationValuePointer

	'	rotV = anAnimation.theRotV

	'	If rotV.animValueOffset(0) <= 0 Then
	'		angleVector.x = 0
	'	Else
	'		angleVector.x = Me.ExtractAnimValue(frameIndex, rotV.theAnimValues(0), aBone.rotationScaleX)
	'	End If
	'	If rotV.animValueOffset(1) <= 0 Then
	'		angleVector.y = 0
	'	Else
	'		angleVector.y = Me.ExtractAnimValue(frameIndex, rotV.theAnimValues(1), aBone.rotationScaleY)
	'	End If
	'	If rotV.animValueOffset(2) <= 0 Then
	'		angleVector.z = 0
	'	Else
	'		angleVector.z = Me.ExtractAnimValue(frameIndex, rotV.theAnimValues(2), aBone.rotationScaleZ)
	'	End If

	'	If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) = 0 Then
	'		angleVector.x += aBone.quat.x
	'		angleVector.y += aBone.quat.y
	'		angleVector.z += aBone.quat.z
	'	End If

	'	rot = MathModule.AngleQuaternion(angleVector)

	'	'	if (!(panim->flags & STUDIO_ANIM_DELTA) && (iBaseFlags & BONE_FIXED_ALIGNMENT))
	'	'	{
	'	'		QuaternionAlign( baseAlignment, q, q );
	'	'	}

	'	Return rot
	'End Function
	Private Function CalcBoneRotation(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone10, ByVal anAnimation As SourceMdlAnimation10, ByRef rotationQuat As SourceQuaternion) As SourceVector
		Dim rot As New SourceQuaternion()
		Dim angleVector As New SourceVector()

		If anAnimation.animationValueOffsets(3) <= 0 Then
			angleVector.x = aBone.rotation.x
		Else
			angleVector.x = Me.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues(3), aBone.rotationScale.x, aBone.rotation.x)
		End If
		If anAnimation.animationValueOffsets(4) <= 0 Then
			angleVector.y = aBone.rotation.y
		Else
			angleVector.y = Me.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues(4), aBone.rotationScale.y, aBone.rotation.y)
		End If
		If anAnimation.animationValueOffsets(5) <= 0 Then
			angleVector.z = aBone.rotation.z
		Else
			angleVector.z = Me.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues(5), aBone.rotationScale.z, aBone.rotation.z)
		End If

		angleVector.debug_text = "anim"

		rotationQuat = MathModule.EulerAnglesToQuaternion(angleVector)
		Return angleVector
	End Function

	'FROM: SourceEngine2007_source\public\bone_setup.cpp
	'//-----------------------------------------------------------------------------
	'// Purpose: return a sub frame position for a single bone
	'//-----------------------------------------------------------------------------
	'void CalcBonePosition(	int frame, float s,
	'						const Vector &basePos, const Vector &baseBoneScale, 
	'						const mstudioanim_t *panim, Vector &pos	)
	'{
	'	if (panim->flags & STUDIO_ANIM_RAWPOS)
	'	{
	'		pos = *(panim->pPos());
	'		Assert( pos.IsValid() );

	'		return;
	'	}
	'	else if (!(panim->flags & STUDIO_ANIM_ANIMPOS))
	'	{
	'		if (panim->flags & STUDIO_ANIM_DELTA)
	'		{
	'			pos.Init( 0.0f, 0.0f, 0.0f );
	'		}
	'		else
	'		{
	'			pos = basePos;
	'		}
	'		return;
	'	}

	'	mstudioanim_valueptr_t *pPosV = panim->pPosV();
	'	int					j;

	'	if (s > 0.001f)
	'	{
	'		float v1, v2;
	'		for (j = 0; j < 3; j++)
	'		{
	'			ExtractAnimValue( frame, pPosV->pAnimvalue( j ), baseBoneScale[j], v1, v2 );
	'			//ZM: This is really setting pos.x when j = 0, pos.y when j = 1, and pos.z when j = 2.
	'			pos[j] = v1 * (1.0 - s) + v2 * s;
	'		}
	'	}
	'	else
	'	{
	'		for (j = 0; j < 3; j++)
	'		{
	'			//ZM: This is really setting pos.x when j = 0, pos.y when j = 1, and pos.z when j = 2.
	'			ExtractAnimValue( frame, pPosV->pAnimvalue( j ), baseBoneScale[j], pos[j] );
	'		}
	'	}

	'	if (!(panim->flags & STUDIO_ANIM_DELTA))
	'	{
	'		pos.x = pos.x + basePos.x;
	'		pos.y = pos.y + basePos.y;
	'		pos.z = pos.z + basePos.z;
	'	}

	'	Assert( pos.IsValid() );
	'}
	Private Function CalcBonePosition(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone10, ByVal anAnimation As SourceMdlAnimation10) As SourceVector
		Dim pos As New SourceVector()

		If anAnimation.animationValueOffsets(0) <= 0 Then
			'pos.x = 0
			pos.x = aBone.position.x
		Else
			pos.x = Me.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues(0), aBone.positionScale.x, aBone.position.x)
		End If

		If anAnimation.animationValueOffsets(1) <= 0 Then
			'pos.y = 0
			pos.y = aBone.position.y
		Else
			pos.y = Me.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues(1), aBone.positionScale.y, aBone.position.y)
		End If

		If anAnimation.animationValueOffsets(2) <= 0 Then
			'pos.z = 0
			pos.z = aBone.position.z
		Else
			pos.z = Me.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues(2), aBone.positionScale.z, aBone.position.z)
		End If

		pos.debug_text = "anim"

		Return pos
	End Function

	'FROM: SourceEngine2007_source\public\bone_setup.cpp
	'void ExtractAnimValue( int frame, mstudioanimvalue_t *panimvalue, float scale, float &v1 )
	'{
	'	if ( !panimvalue )
	'	{
	'		v1 = 0;
	'		return;
	'	}

	'	int k = frame;

	'	while (panimvalue->num.total <= k)
	'	{
	'		k -= panimvalue->num.total;
	'		panimvalue += panimvalue->num.valid + 1;
	'		if ( panimvalue->num.total == 0 )
	'		{
	'			Assert( 0 ); // running off the end of the animation stream is bad
	'			v1 = 0;
	'			return;
	'		}
	'	}
	'	if (panimvalue->num.valid > k)
	'	{
	'		v1 = panimvalue[k+1].value * scale;
	'	}
	'	else
	'	{
	'		// get last valid data block
	'		v1 = panimvalue[panimvalue->num.valid].value * scale;
	'	}
	'}
	Public Function ExtractAnimValue(ByVal frameIndex As Integer, ByVal animValues As List(Of SourceMdlAnimationValue10), ByVal scale As Double, ByVal adjustment As Double) As Double
		Dim v1 As Double
		' k is frameCountRemainingToBeChecked
		Dim k As Integer
		Dim animValueIndex As Integer

		Try
			k = frameIndex
			animValueIndex = 0
			While animValues(animValueIndex).total <= k
				k -= animValues(animValueIndex).total
				animValueIndex += animValues(animValueIndex).valid + 1
				If animValueIndex >= animValues.Count OrElse animValues(animValueIndex).total = 0 Then
					'NOTE: Bad if it reaches here. This means maybe a newer format of the anim data was used for the model.
					v1 = 0
					Return v1
				End If
			End While

			If animValues(animValueIndex).valid > k Then
				'NOTE: The animValues index needs to be offset from current animValues index to match the C++ code above in comment.
				' value[n] = ( sequence[i].panim[q]->pos[j][n][k] - bonetable[j].pos[k] ) / bonetable[j].posscale[k]; 
				'	v = ( sequence[i].panim[q]->rot[j][n][k-3] - bonetable[j].rot[k-3] ); 
				'	if (v >= Q_PI)
				'		v -= Q_PI * 2;
				'	if (v < -Q_PI)
				'		v += Q_PI * 2;
				'	value[n] = v / bonetable[j].rotscale[k-3]; 
				v1 = animValues(animValueIndex + k + 1).value * scale + adjustment
			Else
				'NOTE: The animValues index needs to be offset from current animValues index to match the C++ code above in comment.
				v1 = animValues(animValueIndex + animValues(animValueIndex).valid).value * scale + adjustment
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		Return v1
	End Function

#End Region

#Region "Data"

	Private theOutputFileStreamWriter As StreamWriter
	Private theMdlFileData As SourceMdlFileData14

	Private theAnimationFrameLines As SortedList(Of Integer, AnimationFrameLine)

#End Region

End Class
