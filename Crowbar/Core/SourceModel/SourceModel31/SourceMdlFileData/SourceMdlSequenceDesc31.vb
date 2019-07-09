Public Class SourceMdlSequenceDesc31
	Inherits SourceMdlSequenceDescBase

	Public Sub New()
		'	short				anim[MAXSTUDIOBLENDS][MAXSTUDIOBLENDS];	// f64: 16x16x2 = 512 bytes each anim a short
		Me.anim = New List(Of List(Of Short))(MAXSTUDIOBLENDS)
		For rowIndex As Integer = 0 To MAXSTUDIOBLENDS - 1
			Dim animRow As New List(Of Short)(MAXSTUDIOBLENDS)
			For columnIndex As Integer = 0 To MAXSTUDIOBLENDS - 1
				animRow.Add(0)
			Next
			Me.anim.Add(animRow)
		Next
	End Sub

	Public nameOffset As Integer
	Public activityNameOffset As Integer
	Public flags As Integer
	Public activity As Integer
	Public activityWeight As Integer
	Public eventCount As Integer
	Public eventOffset As Integer

	Public bbMin As New SourceVector()
	Public bbMax As New SourceVector()

	Public frameCount As Integer

	Public blendCount As Integer
	Public blendOffset As Integer

	Public anim As List(Of List(Of Short))

	Public movementIndex As Integer

	Public sequenceGroup As Integer

	Public groupSize(1) As Integer
	Public paramIndex(1) As Integer
	Public paramStart(1) As Single
	Public paramEnd(1) As Single
	Public paramParent As Integer

	Public fadeInTime As Single
	Public fadeOutTime As Single

	Public entryNodeIndex As Integer
	Public exitNodeIndex As Integer
	Public nodeFlags As Integer

	Public entryPhase As Single
	Public exitPhase As Single
	Public lastFrame As Single

	Public nextSeq As Integer
	Public pose As Integer

	Public ikRuleCount As Integer
	Public autoLayerCount As Integer
	Public autoLayerOffset As Integer
	Public weightOffset As Integer
	Public poseKeyOffset As Integer

	Public ikLockCount As Integer
	Public ikLockOffset As Integer
	Public keyValueOffset As Integer
	Public keyValueSize As Integer

	Public unused(2) As Integer

	Public theActivityName As String
	'Public theAnimDescIndexes As List(Of Short)
	'Public theAutoLayers As List(Of SourceMdlAutoLayer37)
	'Public theBoneWeights As List(Of Double)
	'Public theEvents As List(Of SourceMdlEvent37)
	'Public theIkLocks As List(Of SourceMdlIkLock37)
	'Public theKeyValues As String
	Public theName As String
	'Public thePoseKeys As List(Of Double)
	'Public theWeightListIndex As Integer

	'Public theBoneWeightsAreDefault As Boolean

End Class
