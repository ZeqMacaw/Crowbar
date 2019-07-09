Imports System.IO

Module ParseSMD

	Public Function ParseMeshSmdFiles(ByVal pathOrPathFileName As String, ByRef materialCount As Integer) As List(Of ParseMeshSmdInfo)
		infos = New List(Of ParseMeshSmdInfo)()

		materialCount = 0

		If File.Exists(pathOrPathFileName) Then
			ParseMeshSmdFile(pathOrPathFileName, materialCount)
		ElseIf Directory.Exists(pathOrPathFileName) Then
			For Each aPathFileName As String In Directory.GetFiles(pathOrPathFileName, "*.smd")
				If File.Exists(aPathFileName) Then
					ParseMeshSmdFile(aPathFileName, materialCount)
				End If
			Next
		End If

		Return infos
	End Function

	'TODO: ParseMeshSmdFile(). 
	Public Sub ParseMeshSmdFile(ByVal pathFileName As String, ByRef materialCount As Integer)
		info = New ParseMeshSmdInfo()
		infos.Add(info)

		Dim tokenCount As Integer

		commandsRemaining = New List(Of String)()
		commandsRemaining.Add("version")
		commandsRemaining.Add("nodes")
		commandsRemaining.Add("skeleton")
		commandsRemaining.Add("triangles")

		'NOTE: This code is converted and modified a little from Valve's MDL v48 studiomdl.exe source code: src_main\utils\studiomdl\v1support.cpp > Load_SMD().

		outputFileStream = New StreamReader(pathFileName)
		Try
			Dim command As String = ""
			Dim commandOption As Integer

			info.lineCount = 0
			While GetLineInput()
				tokenCount = GetCommandAndOption(command, commandOption)
				If tokenCount = 0 Then
					Continue While
				End If

				If command.ToLower() = "version" Then
					If commandOption <> 1 Then
						info.messages.Add("ERROR: Incorrect version on line " + info.lineCount.ToString() + ". Must be: version 1")
						Exit While
					End If
					commandsRemaining.Remove("version")

				ElseIf command.ToLower() = "nodes" Then
					info.boneCount = GrabNodes()
					commandsRemaining.Remove("nodes")

				ElseIf command.ToLower() = "skeleton" Then
					'			Grab_Animation( psource, "BindPose" );
					commandsRemaining.Remove("skeleton")

				ElseIf command.ToLower() = "triangles" Then
					'			Grab_Triangles( psource );
					commandsRemaining.Remove("triangles")

				ElseIf command = "//" OrElse command = ";" OrElse command = "#" Then
					Continue While

				Else
					Dim commandsRemainingText As String
					commandsRemainingText = String.Join(" ", commandsRemaining)
					info.messages.Add("ERROR: Unknown command on line " + info.lineCount.ToString() + ": " + command)
					info.messages.Add("       Expected one of these commands: " + commandsRemainingText)
					Exit While
				End If
			End While
		Catch ex As Exception
			info.messages.Add("WARNING: Exception raised on line " + info.lineCount.ToString() + ": " + ex.Message)
		Finally
			outputFileStream.Close()
		End Try
	End Sub

	Private Function GetLineInput() As Boolean
		While outputFileStream.Peek() >= 0
			line = outputFileStream.ReadLine()
			info.lineCount += 1
			' Skip comments.
			If Not line.StartsWith("//") Then
				Return True
			End If
		End While
		Return False
	End Function

	Private Function GetCommandAndOption(ByRef command As String, ByRef commandOption As Integer) As Integer
		Dim tokenCount As Integer = 0

		Dim tokens() As String = line.Split(whitespaceSeparators, StringSplitOptions.RemoveEmptyEntries)
		If tokens.Length > 0 Then
			command = tokens(0)
			tokenCount += 1

			If tokens.Length > 1 Then
				Try
					commandOption = Integer.Parse(tokens(1))
					tokenCount += 1
				Catch ex As Exception
					commandOption = 0
					Return tokenCount
				End Try
			End If
		End If

		Return tokenCount
	End Function

	Private Function GrabNodes() As Integer
		Dim boneCount As Integer = 0
		Dim tokenCount As Integer = 0

		Dim boneIndex As Integer
		Dim boneName As String = ""
		Dim parentBoneIndex As Integer

		While GetLineInput()
			tokenCount = GetBoneIndexAndNameAndParent(boneIndex, boneName, parentBoneIndex)
			If tokenCount = 3 Then
				'TODO: Show warning about clipped boneName longer than 1023 characters. Valve source code shows boneName using max of 1024 characters, including null end byte.
				'	char name[1024];

				If info.boneNames.Contains(boneName) Then
					info.messages.Add("WARNING: Duplicate bone name of """ + boneName + """ at line " + info.lineCount.ToString())
				Else
					info.boneNames.Add(boneName)
					If boneIndex > boneCount Then
						boneCount = boneIndex
					End If
				End If
			Else
				Return boneCount + 1
			End If
		End While

		info.messages.Add("ERROR: Unexpected end of file at line " + info.lineCount.ToString())
		Return 0
	End Function

	Private Function GetBoneIndexAndNameAndParent(ByRef boneIndex As Integer, ByRef boneName As String, ByRef parentBoneIndex As Integer) As Integer
		Dim tokenCount As Integer = 0

		Dim tokens() As String = line.Split(whitespaceSeparators, StringSplitOptions.RemoveEmptyEntries)
		If tokens.Length > 0 Then
			Try
				boneIndex = Integer.Parse(tokens(0))
				tokenCount += 1
			Catch ex As Exception
				boneIndex = 0
			End Try

			If tokens.Length > 1 Then
				boneName = tokens(1)
				tokenCount += 1

				If tokens.Length > 2 Then
					Try
						parentBoneIndex = Integer.Parse(tokens(2))
						tokenCount += 1
					Catch ex As Exception
						parentBoneIndex = 0
					End Try
				End If
			End If
		End If

		Return tokenCount
	End Function

	'void Grab_Animation( s_source_t *pSource, const char *pAnimName )
	'{
	'	Vector pos;
	'	RadianEuler rot;
	'	char cmd[1024];
	'	int index;
	'	int	t = -99999999;
	'	int size;
	'
	'	s_sourceanim_t *pAnim = FindOrAddSourceAnim( pSource, pAnimName );
	'	pAnim->startframe = -1;
	'
	'	size = pSource->numbones * sizeof( s_bone_t );
	'
	'	while ( GetLineInput() ) 
	'	{
	'		if ( sscanf( g_szLine, "%d %f %f %f %f %f %f", &index, &pos[0], &pos[1], &pos[2], &rot[0], &rot[1], &rot[2] ) == 7 )
	'		{
	'			if ( pAnim->startframe < 0 )
	'			{
	'				MdlError( "Missing frame start(%d) : %s", g_iLinecount, g_szLine );
	'			}
	'
	'			scale_vertex( pos );
	'			VectorCopy( pos, pAnim->rawanim[t][index].pos );
	'			VectorCopy( rot, pAnim->rawanim[t][index].rot );
	'
	'			clip_rotations( rot ); // !!!
	'			continue;
	'		}
	'		
	'		if ( sscanf( g_szLine, "%1023s %d", cmd, &index ) == 0 )
	'		{
	'			MdlError( "MdlError(%d) : %s", g_iLinecount, g_szLine );
	'			continue;
	'		}
	'
	'		if ( !Q_stricmp( cmd, "time" ) ) 
	'		{
	'			t = index;
	'			if ( pAnim->startframe == -1 )
	'			{
	'				pAnim->startframe = t;
	'			}
	'			if ( t < pAnim->startframe )
	'			{
	'				MdlError( "Frame MdlError(%d) : %s", g_iLinecount, g_szLine );
	'			}
	'			if ( t > pAnim->endframe )
	'			{
	'				pAnim->endframe = t;
	'			}
	'			t -= pAnim->startframe;
	'
	'			if ( t >= pAnim->rawanim.Count())
	'			{
	'				s_bone_t *ptr = NULL;
	'				pAnim->rawanim.AddMultipleToTail( t - pAnim->rawanim.Count() + 1, &ptr );
	'			}
	'
	'			if ( pAnim->rawanim[t] != NULL )
	'			{
	'				continue;
	'			}
	'
	'			pAnim->rawanim[t] = (s_bone_t *)kalloc( 1, size );
	'
	'			// duplicate previous frames keys
	'			if ( t > 0 && pAnim->rawanim[t-1] )
	'			{
	'				for ( int j = 0; j < pSource->numbones; j++ )
	'				{
	'					VectorCopy( pAnim->rawanim[t-1][j].pos, pAnim->rawanim[t][j].pos );
	'					VectorCopy( pAnim->rawanim[t-1][j].rot, pAnim->rawanim[t][j].rot );
	'				}
	'			}
	'			continue;
	'		}
	'		
	'		if ( !Q_stricmp( cmd, "end" ) ) 
	'		{
	'			pAnim->numframes = pAnim->endframe - pAnim->startframe + 1;
	'
	'			for ( t = 0; t < pAnim->numframes; t++ )
	'			{
	'				if ( pAnim->rawanim[t] == NULL)
	'				{
	'					MdlError( "%s is missing frame %d\n", pSource->filename, t + pAnim->startframe );
	'				}
	'			}
	'
	'			Build_Reference( pSource, pAnimName );
	'			return;
	'		}
	'
	'		MdlError( "MdlError(%d) : %s", g_iLinecount, g_szLine );
	'	}
	'
	'	MdlError( "unexpected EOF: %s\n", pSource->filename );
	'}

	Private Sub GrabAnimation()
		Dim tokenCount As Integer

		Dim boneIndex As Integer
		Dim position As New SourceVector()
		Dim rotation As New SourceVector()
		Dim command As String = ""
		Dim frameIndex As Integer

		'	int	t = -99999999;
		'	int size;
		'
		'	s_sourceanim_t *pAnim = FindOrAddSourceAnim( pSource, pAnimName );
		'	pAnim->startframe = -1;
		'
		'	size = pSource->numbones * sizeof( s_bone_t );
		'
		While GetLineInput()
			tokenCount = GetAnimationBoneIndexAndPositionAndRotation(boneIndex, position, rotation)
			If tokenCount = 7 Then
				'			if ( pAnim->startframe < 0 )
				'			{
				'				MdlError( "Missing frame start(%d) : %s", g_iLinecount, g_szLine );
				'			}
				'
				'			scale_vertex( pos );
				'			VectorCopy( pos, pAnim->rawanim[t][index].pos );
				'			VectorCopy( rot, pAnim->rawanim[t][index].rot );
				'
				'			clip_rotations( rot ); // !!!
				Continue While
			End If

			'		if ( sscanf( g_szLine, "%1023s %d", cmd, &index ) == 0 )
			tokenCount = GetAnimationTimeAndIndex(command, frameIndex)
			If tokenCount = 0 Then
				'			MdlError( "MdlError(%d) : %s", g_iLinecount, g_szLine );
				Continue While
			End If

			If command.ToLower() = "time" Then
				'			t = index;
				'			if ( pAnim->startframe == -1 )
				'			{
				'				pAnim->startframe = t;
				'			}
				'			if ( t < pAnim->startframe )
				'			{
				'				MdlError( "Frame MdlError(%d) : %s", g_iLinecount, g_szLine );
				'			}
				'			if ( t > pAnim->endframe )
				'			{
				'				pAnim->endframe = t;
				'			}
				'			t -= pAnim->startframe;
				'
				'			if ( t >= pAnim->rawanim.Count())
				'			{
				'				s_bone_t *ptr = NULL;
				'				pAnim->rawanim.AddMultipleToTail( t - pAnim->rawanim.Count() + 1, &ptr );
				'			}
				'
				'			if ( pAnim->rawanim[t] != NULL )
				'			{
				'				continue;
				'			}
				'
				'			pAnim->rawanim[t] = (s_bone_t *)kalloc( 1, size );
				'
				'			// duplicate previous frames keys
				'			if ( t > 0 && pAnim->rawanim[t-1] )
				'			{
				'				for ( int j = 0; j < pSource->numbones; j++ )
				'				{
				'					VectorCopy( pAnim->rawanim[t-1][j].pos, pAnim->rawanim[t][j].pos );
				'					VectorCopy( pAnim->rawanim[t-1][j].rot, pAnim->rawanim[t][j].rot );
				'				}
				'			}
				Continue While
			End If

			If command.ToLower() = "end" Then
				'			pAnim->numframes = pAnim->endframe - pAnim->startframe + 1;
				'
				'			for ( t = 0; t < pAnim->numframes; t++ )
				'			{
				'				if ( pAnim->rawanim[t] == NULL)
				'				{
				'					MdlError( "%s is missing frame %d\n", pSource->filename, t + pAnim->startframe );
				'				}
				'			}
				'
				'			Build_Reference( pSource, pAnimName );
				'			return;
			End If

			'		MdlError( "MdlError(%d) : %s", g_iLinecount, g_szLine );
		End While

		'	MdlError( "unexpected EOF: %s\n", pSource->filename );
	End Sub

	'		if ( sscanf( g_szLine, "%d %f %f %f %f %f %f", &index, &pos[0], &pos[1], &pos[2], &rot[0], &rot[1], &rot[2] ) == 7 )
	Private Function GetAnimationBoneIndexAndPositionAndRotation(ByRef boneIndex As Integer, ByRef position As SourceVector, ByRef rotation As SourceVector) As Integer
		Dim tokenCount As Integer = 0

		Dim tokens() As String = line.Split(whitespaceSeparators, StringSplitOptions.RemoveEmptyEntries)
		If tokens.Length > 0 Then
			Try
				boneIndex = Integer.Parse(tokens(0))
				tokenCount += 1
			Catch ex As Exception
				Return tokenCount
			End Try

			If tokens.Length > 1 Then
				Try
					position.x = Double.Parse(tokens(1))
					tokenCount += 1
				Catch ex As Exception
					Return tokenCount
				End Try

				If tokens.Length > 2 Then
					Try
						position.y = Double.Parse(tokens(2))
						tokenCount += 2
					Catch ex As Exception
						Return tokenCount
					End Try

					If tokens.Length > 3 Then
						Try
							position.z = Double.Parse(tokens(3))
							tokenCount += 1
						Catch ex As Exception
							Return tokenCount
						End Try

						If tokens.Length > 4 Then
							Try
								rotation.x = Double.Parse(tokens(4))
								tokenCount += 1
							Catch ex As Exception
								Return tokenCount
							End Try

							If tokens.Length > 5 Then
								Try
									rotation.y = Double.Parse(tokens(5))
									tokenCount += 1
								Catch ex As Exception
									Return tokenCount
								End Try

								If tokens.Length > 6 Then
									Try
										rotation.z = Double.Parse(tokens(6))
										tokenCount += 1
									Catch ex As Exception
										Return tokenCount
									End Try
								End If
							End If
						End If
					End If
				End If
			End If
		End If

		Return tokenCount
	End Function

	Private Function GetAnimationTimeAndIndex(ByRef command As String, ByRef frameIndex As Integer) As Integer
		Dim tokenCount As Integer = 0

		Dim tokens() As String = line.Split(whitespaceSeparators, StringSplitOptions.RemoveEmptyEntries)
		If tokens.Length > 0 Then
			command = tokens(0)
			tokenCount += 1

			If tokens.Length > 1 Then
				Try
					frameIndex = Integer.Parse(tokens(1))
					tokenCount += 1
				Catch ex As Exception
					frameIndex = 0
					Return tokenCount
				End Try
			End If
		End If

		Return tokenCount
	End Function

	'void Grab_Triangles( s_source_t *psource )
	'{
	'	int		i;
	'	Vector	vmin, vmax;
	'
	'	vmin[0] = vmin[1] = vmin[2] = 99999;
	'	vmax[0] = vmax[1] = vmax[2] = -99999;
	'
	'	g_numfaces = 0;
	'	numvlist = 0;
	' 
	'	//
	'	// load the base triangles
	'	//
	'	int texture;
	'	int material;
	'	char texturename[MAX_PATH];
	'
	'	while (1) 
	'	{
	'		if (!GetLineInput()) 
	'			break;
	'
	'		// check for end
	'		if (IsEnd( g_szLine )) 
	'			break;
	'
	'		// Look for extra junk that we may want to avoid...
	'		int nLineLength = strlen( g_szLine );
	'		if (nLineLength >= sizeof( texturename ))
	'		{
	'			MdlWarning("Unexpected data at line %d, (need a texture name) ignoring...\n", g_iLinecount );
	'			continue;
	'		}
	'
	'		// strip off trailing smag
	'		strncpy( texturename, g_szLine, sizeof( texturename ) - 1 );
	'		for (i = strlen( texturename ) - 1; i >= 0 && ! isgraph( texturename[i] ); i--)
	'		{
	'		}
	'		texturename[i + 1] = '\0';
	'
	'		// funky texture overrides
	'		for (i = 0; i < numrep; i++)  
	'		{
	'			if (sourcetexture[i][0] == '\0') 
	'			{
	'				strcpy( texturename, defaulttexture[i] );
	'				break;
	'			}
	'			if (stricmp( texturename, sourcetexture[i]) == 0) 
	'			{
	'				strcpy( texturename, defaulttexture[i] );
	'				break;
	'			}
	'		}
	'
	'		if (texturename[0] == '\0')
	'		{
	'			// weird source problem, skip them
	'			GetLineInput();
	'			GetLineInput();
	'			GetLineInput();
	'			continue;
	'		}
	'
	'		if (stricmp( texturename, "null.bmp") == 0 || stricmp( texturename, "null.tga") == 0 || stricmp( texturename, "debug/debugempty" ) == 0)
	'		{
	'			// skip all faces with the null texture on them.
	'			GetLineInput();
	'			GetLineInput();
	'			GetLineInput();
	'			continue;
	'		}
	'
	'		texture = LookupTexture( texturename, ( g_smdVersion > 1 ) );
	'		psource->texmap[texture] = texture;	// hack, make it 1:1
	'		material = UseTextureAsMaterial( texture );
	'
	'		s_face_t f;
	'		ParseFaceData( psource, material, &f );
	'
	'		// remove degenerate triangles
	'		if (f.a == f.b || f.b == f.c || f.a == f.c)
	'		{
	'			// printf("Degenerate triangle %d %d %d\n", f.a, f.b, f.c );
	'			continue;
	'		}
	'	
	'		g_src_uface[g_numfaces] = f;
	'		g_face[g_numfaces].material = material;
	'		g_numfaces++;
	'	}
	'
	'	BuildIndividualMeshes( psource );
	'}

	Private infos As List(Of ParseMeshSmdInfo)
	Private info As ParseMeshSmdInfo

	Private outputFileStream As StreamReader
	Private line As String
	Private commandsRemaining As List(Of String)
	'NOTE: Empty array means all whitespace in Split().
	Private whitespaceSeparators() As Char = {}

End Module
