Imports System.IO

Module SourceModule06

	'Public Function GetAnimationSmdRelativePathFileName(ByVal modelName As String, ByVal iAnimationName As String, ByVal blendIndex As Integer) As String
	'	Dim animationName As String
	'	Dim animationSmdRelativePathFileName As String

	'	'NOTE: The animation or blend name is not stored, so use sequence name or variation of it.
	'	If blendIndex = -1 Then
	'		animationName = iAnimationName
	'	Else
	'		animationName = iAnimationName + "_blend" + (blendIndex + 1).ToString("00")
	'	End If

	'	If Not TheApp.Settings.DecompileBoneAnimationPlaceInSubfolderIsChecked Then
	'		animationName = modelName + "_anim_" + animationName
	'	End If
	'	animationSmdRelativePathFileName = Path.Combine(GetAnimationSmdRelativePath(modelName), animationName)

	'	If Path.GetExtension(animationSmdRelativePathFileName) <> ".smd" Then
	'		'NOTE: Add the ".smd" extension, keeping the existing extension in file name, which is often ".dmx" for newer models. 
	'		'      Thus, user can see that model might have newer features that Crowbar does not yet handle.
	'		animationSmdRelativePathFileName += ".smd"
	'	End If

	'	Return animationSmdRelativePathFileName
	'End Function

	' For the SourceMdlBoneController10.type and SourceMdlSequenceDesc10.activityId.blendType fields.
	'FROM: [1999] HLStandardSDK\SourceCode\utils\studiomdl\studiomdl.c
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
	Public Function GetControlText(ByVal type As Integer) As String
		If type = STUDIO_X Then
			Return "X"
		ElseIf type = STUDIO_Y Then
			Return "Y"
		ElseIf type = STUDIO_Z Then
			Return "Z"
		ElseIf type = STUDIO_XR Then
			Return "XR"
		ElseIf type = STUDIO_YR Then
			Return "YR"
		ElseIf type = STUDIO_ZR Then
			Return "ZR"
		ElseIf type = STUDIO_LX Then
			Return "LX"
		ElseIf type = STUDIO_LY Then
			Return "LY"
		ElseIf type = STUDIO_LZ Then
			Return "LZ"
		ElseIf type = STUDIO_AX Then
			Return "AX"
		ElseIf type = STUDIO_AY Then
			Return "AY"
		ElseIf type = STUDIO_AZ Then
			Return "AZ"
		ElseIf type = STUDIO_AXR Then
			Return "AXR"
		ElseIf type = STUDIO_AYR Then
			Return "AYR"
		ElseIf type = STUDIO_AZR Then
			Return "AZR"
			'			if (bonecontroller[numbonecontrollers].type & (STUDIO_XR | STUDIO_YR | STUDIO_ZR))
			'			{
			'				if (((int)(bonecontroller[numbonecontrollers].start + 360) % 360) == ((int)(bonecontroller[numbonecontrollers].end + 360) % 360))
			'				{
			'					bonecontroller[numbonecontrollers].type |= STUDIO_RLOOP;
			'				}
			'			}
		ElseIf type = (STUDIO_XR Or STUDIO_RLOOP) Then
			Return "XR"
		ElseIf type = (STUDIO_YR Or STUDIO_RLOOP) Then
			Return "YR"
		ElseIf type = (STUDIO_ZR Or STUDIO_RLOOP) Then
			Return "ZR"
		End If

		Return ""
	End Function

	Public Function GetMultipleControlText(ByVal type As Integer) As String
		Dim result As String = ""

		If (type And STUDIO_X) = STUDIO_X Then
			result += " X"
		End If
		If (type And STUDIO_Y) = STUDIO_Y Then
			result += " Y"
		End If
		If (type And STUDIO_Z) = STUDIO_Z Then
			result += " Z"
		End If
		If (type And STUDIO_XR) = STUDIO_XR Then
			result += " XR"
		End If
		If (type And STUDIO_YR) = STUDIO_YR Then
			result += " YR"
		End If
		If (type And STUDIO_ZR) = STUDIO_ZR Then
			result += " ZR"
		End If
		If (type And STUDIO_LX) = STUDIO_LX Then
			result += " LX"
		End If
		If (type And STUDIO_LY) = STUDIO_LY Then
			result += " LY"
		End If
		If (type And STUDIO_LZ) = STUDIO_LZ Then
			result += " LZ"
		End If
		If (type And STUDIO_AX) = STUDIO_AX Then
			result += " AX"
		End If
		If (type And STUDIO_AY) = STUDIO_AY Then
			result += " AY"
		End If
		If (type And STUDIO_AZ) = STUDIO_AZ Then
			result += " AZ"
		End If
		If (type And STUDIO_AXR) = STUDIO_AXR Then
			result += " AXR"
		End If
		If (type And STUDIO_AYR) = STUDIO_AYR Then
			result += " AYR"
		End If
		If (type And STUDIO_AZR) = STUDIO_AZR Then
			result += " AZR"
		End If

		Return result.Trim()
	End Function

	'FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
	'// motion flags
	'#define STUDIO_X		0x0001
	'#define STUDIO_Y		0x0002	
	'#define STUDIO_Z		0x0004
	'#define STUDIO_XR		0x0008
	'#define STUDIO_YR		0x0010
	'#define STUDIO_ZR		0x0020
	'#define STUDIO_LX		0x0040
	'#define STUDIO_LY		0x0080
	'#define STUDIO_LZ		0x0100
	'#define STUDIO_AX		0x0200
	'#define STUDIO_AY		0x0400
	'#define STUDIO_AZ		0x0800
	'#define STUDIO_AXR		0x1000
	'#define STUDIO_AYR		0x2000
	'#define STUDIO_AZR		0x4000
	'#define STUDIO_TYPES	0x7FFF
	'#define STUDIO_RLOOP	0x8000	// controller that wraps shortest distance

	Public STUDIO_X As Integer = &H1
	Public STUDIO_Y As Integer = &H2
	Public STUDIO_Z As Integer = &H4
	Public STUDIO_XR As Integer = &H8
	Public STUDIO_YR As Integer = &H10
	Public STUDIO_ZR As Integer = &H20

	Public STUDIO_LX As Integer = &H40
	Public STUDIO_LY As Integer = &H80
	Public STUDIO_LZ As Integer = &H100

	Public STUDIO_AX As Integer = &H200
	Public STUDIO_AY As Integer = &H400
	Public STUDIO_AZ As Integer = &H800
	Public STUDIO_AXR As Integer = &H1000
	Public STUDIO_AYR As Integer = &H2000
	Public STUDIO_AZR As Integer = &H4000

	Public STUDIO_RLOOP As Integer = &H8000

End Module
