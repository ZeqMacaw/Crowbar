Imports System.IO

Module SourceModule10

	'Public Function GetAnimationSmdRelativePathFileName(ByVal modelName As String, ByVal iAnimationName As String, ByVal blendIndex As Integer, Optional ByVal includeExtension As Boolean = True) As String
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

	'	If includeExtension Then
	'		If Path.GetExtension(animationSmdRelativePathFileName) <> ".smd" Then
	'			'NOTE: Add the ".smd" extension, keeping the existing extension in file name, which is often ".dmx" for newer models. 
	'			'      Thus, user can see that model might have newer features that Crowbar does not yet handle.
	'			animationSmdRelativePathFileName += ".smd"
	'		End If
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

		'Type can be -1 if original QC did not use any of the control texts above, so use a control text that is not one of the above.
		Return "N"
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

	' For the SourceMdlSequenceDesc10.activityId field.
	'FROM: [1999] HLStandardSDK\SourceCode\dlls\activity.h
	'typedef enum {
	'	ACT_RESET = 0,		// Set m_Activity to this invalid value to force a reset to m_IdealActivity
	'	ACT_IDLE = 1,
	'	ACT_GUARD,
	'	ACT_WALK,
	'	ACT_RUN,
	'	ACT_FLY,				// Fly (and flap if appropriate)
	'	ACT_SWIM,
	'	ACT_HOP,				// vertical jump
	'	ACT_LEAP,				// long forward jump
	'	ACT_FALL,
	'	ACT_LAND,
	'	ACT_STRAFE_LEFT,
	'	ACT_STRAFE_RIGHT,
	'	ACT_ROLL_LEFT,			// tuck and roll, left
	'	ACT_ROLL_RIGHT,			// tuck and roll, right
	'	ACT_TURN_LEFT,			// turn quickly left (stationary)
	'	ACT_TURN_RIGHT,			// turn quickly right (stationary)
	'	ACT_CROUCH,				// the act of crouching down from a standing position
	'	ACT_CROUCHIDLE,			// holding body in crouched position (loops)
	'	ACT_STAND,				// the act of standing from a crouched position
	'	ACT_USE,
	'	ACT_SIGNAL1,
	'	ACT_SIGNAL2,
	'	ACT_SIGNAL3,
	'	ACT_TWITCH,
	'	ACT_COWER,
	'	ACT_SMALL_FLINCH,
	'	ACT_BIG_FLINCH,
	'	ACT_RANGE_ATTACK1,
	'	ACT_RANGE_ATTACK2,
	'	ACT_MELEE_ATTACK1,
	'	ACT_MELEE_ATTACK2,
	'	ACT_RELOAD,
	'	ACT_ARM,				// pull out gun, for instance
	'	ACT_DISARM,				// reholster gun
	'	ACT_EAT,				// monster chowing on a large food item (loop)
	'	ACT_DIESIMPLE,
	'	ACT_DIEBACKWARD,
	'	ACT_DIEFORWARD,
	'	ACT_DIEVIOLENT,
	'	ACT_BARNACLE_HIT,		// barnacle tongue hits a monster
	'	ACT_BARNACLE_PULL,		// barnacle is lifting the monster ( loop )
	'	ACT_BARNACLE_CHOMP,		// barnacle latches on to the monster
	'	ACT_BARNACLE_CHEW,		// barnacle is holding the monster in its mouth ( loop )
	'	ACT_SLEEP,
	'	ACT_INSPECT_FLOOR,		// for active idles, look at something on or near the floor
	'	ACT_INSPECT_WALL,		// for active idles, look at something directly ahead of you ( doesn't HAVE to be a wall or on a wall )
	'	ACT_IDLE_ANGRY,			// alternate idle animation in which the monster is clearly agitated. (loop)
	'	ACT_WALK_HURT,			// limp  (loop)
	'	ACT_RUN_HURT,			// limp  (loop)
	'	ACT_HOVER,				// Idle while in flight
	'	ACT_GLIDE,				// Fly (don't flap)
	'	ACT_FLY_LEFT,			// Turn left in flight
	'	ACT_FLY_RIGHT,			// Turn right in flight
	'	ACT_DETECT_SCENT,		// this means the monster smells a scent carried by the air
	'	ACT_SNIFF,				// this is the act of actually sniffing an item in front of the monster
	'	ACT_BITE,				// some large monsters can eat small things in one bite. This plays one time, EAT loops.
	'	ACT_THREAT_DISPLAY,		// without attacking, monster demonstrates that it is angry. (Yell, stick out chest, etc )
	'	ACT_FEAR_DISPLAY,		// monster just saw something that it is afraid of
	'	ACT_EXCITED,			// for some reason, monster is excited. Sees something he really likes to eat, or whatever.
	'	ACT_SPECIAL_ATTACK1,	// very monster specific special attacks.
	'	ACT_SPECIAL_ATTACK2,	
	'	ACT_COMBAT_IDLE,		// agitated idle.
	'	ACT_WALK_SCARED,
	'	ACT_RUN_SCARED,
	'	ACT_VICTORY_DANCE,		// killed a player, do a victory dance.
	'	ACT_DIE_HEADSHOT,		// die, hit in head. 
	'	ACT_DIE_CHESTSHOT,		// die, hit in chest
	'	ACT_DIE_GUTSHOT,		// die, hit in gut
	'	ACT_DIE_BACKSHOT,		// die, hit in back
	'	ACT_FLINCH_HEAD,
	'	ACT_FLINCH_CHEST,
	'	ACT_FLINCH_STOMACH,
	'	ACT_FLINCH_LEFTARM,
	'	ACT_FLINCH_RIGHTARM,
	'	ACT_FLINCH_LEFTLEG,
	'	ACT_FLINCH_RIGHTLEG,
	'} Activity;

	Public activityMap As String() = {"ACT_RESET", _
"ACT_IDLE", _
"ACT_GUARD", _
"ACT_WALK", _
"ACT_RUN", _
"ACT_FLY", _
"ACT_SWIM", _
"ACT_HOP", _
"ACT_LEAP", _
"ACT_FALL", _
"ACT_LAND", _
"ACT_STRAFE_LEFT", _
"ACT_STRAFE_RIGHT", _
"ACT_ROLL_LEFT", _
"ACT_ROLL_RIGHT", _
"ACT_TURN_LEFT", _
"ACT_TURN_RIGHT", _
"ACT_CROUCH", _
"ACT_CROUCHIDLE", _
"ACT_STAND", _
"ACT_USE", _
"ACT_SIGNAL1", _
"ACT_SIGNAL2", _
"ACT_SIGNAL3", _
"ACT_TWITCH", _
"ACT_COWER", _
"ACT_SMALL_FLINCH", _
"ACT_BIG_FLINCH", _
"ACT_RANGE_ATTACK1", _
"ACT_RANGE_ATTACK2", _
"ACT_MELEE_ATTACK1", _
"ACT_MELEE_ATTACK2", _
"ACT_RELOAD", _
"ACT_ARM", _
"ACT_DISARM", _
"ACT_EAT", _
"ACT_DIESIMPLE", _
"ACT_DIEBACKWARD", _
"ACT_DIEFORWARD", _
"ACT_DIEVIOLENT", _
"ACT_BARNACLE_HIT", _
"ACT_BARNACLE_PULL", _
"ACT_BARNACLE_CHOMP", _
"ACT_BARNACLE_CHEW", _
"ACT_SLEEP", _
"ACT_INSPECT_FLOOR", _
"ACT_INSPECT_WALL", _
"ACT_IDLE_ANGRY", _
"ACT_WALK_HURT", _
"ACT_RUN_HURT", _
"ACT_HOVER", _
"ACT_GLIDE", _
"ACT_FLY_LEFT", _
"ACT_FLY_RIGHT", _
"ACT_DETECT_SCENT", _
"ACT_SNIFF", _
"ACT_BITE", _
"ACT_THREAT_DISPLAY", _
"ACT_FEAR_DISPLAY", _
"ACT_EXCITED", _
"ACT_SPECIAL_ATTACK1", _
"ACT_SPECIAL_ATTACK2", _
"ACT_COMBAT_IDLE", _
"ACT_WALK_SCARED", _
"ACT_RUN_SCARED", _
"ACT_VICTORY_DANCE", _
"ACT_DIE_HEADSHOT", _
"ACT_DIE_CHESTSHOT", _
"ACT_DIE_GUTSHOT", _
"ACT_DIE_BACKSHOT", _
"ACT_FLINCH_HEAD", _
"ACT_FLINCH_CHEST", _
"ACT_FLINCH_STOMACH", _
"ACT_FLINCH_LEFTARM", _
"ACT_FLINCH_RIGHTARM", _
"ACT_FLINCH_LEFTLEG", _
"ACT_FLINCH_RIGHTLEG"
}

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
