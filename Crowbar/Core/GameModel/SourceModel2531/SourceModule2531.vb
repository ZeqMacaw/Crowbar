Imports System.IO

Module SourceModule2531

	'Public Function GetBodyGroupSmdFileName(ByVal bodyPartIndex As Integer, ByVal modelIndex As Integer, ByVal lodIndex As Integer, ByVal theModelCommandIsUsed As Boolean, ByVal modelName As String, ByVal bodyModelName As String, ByVal bodyPartCount As Integer, ByVal bodyModelCount As Integer, ByVal sequenceGroupFileName As String) As String
	'	Dim bodyGroupSmdFileName As String

	'	If bodyPartIndex = 0 AndAlso modelIndex = 0 AndAlso lodIndex = 0 AndAlso Not String.IsNullOrEmpty(sequenceGroupFileName) AndAlso Not FileManager.FilePathHasInvalidChars(sequenceGroupFileName) Then
	'		bodyGroupSmdFileName = Path.GetFileName(sequenceGroupFileName.Trim(Chr(0))).ToLower(TheApp.InternalCultureInfo)
	'		If Not bodyGroupSmdFileName.StartsWith(modelName) Then
	'			bodyGroupSmdFileName = modelName + "_" + bodyGroupSmdFileName
	'		End If
	'	Else
	'		bodyGroupSmdFileName = SourceFileNamesModule.GetBodyGroupSmdFileName(bodyPartIndex, modelIndex, lodIndex, theModelCommandIsUsed, modelName, bodyModelName, bodyPartCount, bodyModelCount)
	'	End If

	'	Return bodyGroupSmdFileName
	'End Function

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
		ElseIf type = (STUDIO_XR Or STUDIO_RLOOP) Then
			Return "XR"
		ElseIf type = (STUDIO_YR Or STUDIO_RLOOP) Then
			Return "YR"
		ElseIf type = (STUDIO_ZR Or STUDIO_RLOOP) Then
			Return "ZR"
		End If

		Return ""
	End Function

	'	Public activityMap As String() = {"ACT_RESET", _
	'"ACT_IDLE", _
	'"ACT_GUARD", _
	'"ACT_WALK", _
	'"ACT_RUN", _
	'"ACT_FLY", _
	'"ACT_SWIM", _
	'"ACT_HOP", _
	'"ACT_LEAP", _
	'"ACT_FALL", _
	'"ACT_LAND", _
	'"ACT_STRAFE_LEFT", _
	'"ACT_STRAFE_RIGHT", _
	'"ACT_ROLL_LEFT", _
	'"ACT_ROLL_RIGHT", _
	'"ACT_TURN_LEFT", _
	'"ACT_TURN_RIGHT", _
	'"ACT_CROUCH", _
	'"ACT_CROUCHIDLE", _
	'"ACT_STAND", _
	'"ACT_USE", _
	'"ACT_SIGNAL1", _
	'"ACT_SIGNAL2", _
	'"ACT_SIGNAL3", _
	'"ACT_TWITCH", _
	'"ACT_COWER", _
	'"ACT_SMALL_FLINCH", _
	'"ACT_BIG_FLINCH", _
	'"ACT_RANGE_ATTACK1", _
	'"ACT_RANGE_ATTACK2", _
	'"ACT_MELEE_ATTACK1", _
	'"ACT_MELEE_ATTACK2", _
	'"ACT_RELOAD", _
	'"ACT_ARM", _
	'"ACT_DISARM", _
	'"ACT_EAT", _
	'"ACT_DIESIMPLE", _
	'"ACT_DIEBACKWARD", _
	'"ACT_DIEFORWARD", _
	'"ACT_DIEVIOLENT", _
	'"ACT_BARNACLE_HIT", _
	'"ACT_BARNACLE_PULL", _
	'"ACT_BARNACLE_CHOMP", _
	'"ACT_BARNACLE_CHEW", _
	'"ACT_SLEEP", _
	'"ACT_INSPECT_FLOOR", _
	'"ACT_INSPECT_WALL", _
	'"ACT_IDLE_ANGRY", _
	'"ACT_WALK_HURT", _
	'"ACT_RUN_HURT", _
	'"ACT_HOVER", _
	'"ACT_GLIDE", _
	'"ACT_FLY_LEFT", _
	'"ACT_FLY_RIGHT", _
	'"ACT_DETECT_SCENT", _
	'"ACT_SNIFF", _
	'"ACT_BITE", _
	'"ACT_THREAT_DISPLAY", _
	'"ACT_FEAR_DISPLAY", _
	'"ACT_EXCITED", _
	'"ACT_SPECIAL_ATTACK1", _
	'"ACT_SPECIAL_ATTACK2", _
	'"ACT_COMBAT_IDLE", _
	'"ACT_WALK_SCARED", _
	'"ACT_RUN_SCARED", _
	'"ACT_VICTORY_DANCE", _
	'"ACT_DIE_HEADSHOT", _
	'"ACT_DIE_CHESTSHOT", _
	'"ACT_DIE_GUTSHOT", _
	'"ACT_DIE_BACKSHOT", _
	'"ACT_FLINCH_HEAD", _
	'"ACT_FLINCH_CHEST", _
	'"ACT_FLINCH_STOMACH", _
	'"ACT_FLINCH_LEFTARM", _
	'"ACT_FLINCH_RIGHTARM", _
	'"ACT_FLINCH_LEFTLEG", _
	'"ACT_FLINCH_RIGHTLEG"
	'}

	'#define MAXSTUDIOBLENDS		16		// f64: ~New
	Public MAXSTUDIOBLENDS As Integer = 16


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
