Imports System.IO

Public Class SourceVrdFile48

#Region "Creation and Destruction"

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData48)
		Me.theOutputFileStreamWriter = outputFileStream
		Me.theMdlFileData = mdlFileData
	End Sub

#End Region

#Region "Methods"

	Public Sub WriteHeaderComment()
		Common.WriteHeaderComment(Me.theOutputFileStreamWriter)
	End Sub

	Public Sub WriteCommands()
		If Me.theMdlFileData.theBones IsNot Nothing Then
			Dim line As String = ""
			Dim aBone As SourceMdlBone
			Dim aParentBone As SourceMdlBone
			Dim aControlBone As SourceMdlBone
			Dim aParentControlBone As SourceMdlBone
			Dim aTrigger As SourceMdlQuatInterpBoneInfo
			Dim aTriggerTrigger As SourceVector
			Dim aTriggerQuat As SourceVector
			Dim aBoneName As String
			Dim aParentBoneName As String
			Dim aParentControlBoneName As String
			Dim aControlBoneName As String

			For i As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
				aBone = Me.theMdlFileData.theBones(i)

				If aBone.proceduralRuleOffset <> 0 Then
					If aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_AXISINTERP Then
					ElseIf aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_QUATINTERP Then
						'<helper> Bip01_L_Elbow Bip01_L_UpperArm Bip01_L_UpperArm Bip01_L_Forearm
						'<display> 1.5 3 3 100
						'<basepos> 0 0 0
						'<trigger> 90 0 0 0 0 0 0 0 0 0
						'<trigger> 90 0 0 -90 0 0 -45 0 0 0

						'int i = sscanf( g_szLine, "%s %s %s %s %s", cmd, pBone->bonename, pBone->parentname, pBone->controlparentname, pBone->controlname );
						aParentBone = Me.theMdlFileData.theBones(aBone.parentBoneIndex)
						aControlBone = Me.theMdlFileData.theBones(aBone.theQuatInterpBone.controlBoneIndex)
						aParentControlBone = Me.theMdlFileData.theBones(aControlBone.parentBoneIndex)

						'NOTE: A bone name in a VRD file must have its characters up to and including the first dot removed.
						'aBoneName = aBone.theName.Replace("ValveBiped.", "")
						'aParentBoneName = aParentBone.theName.Replace("ValveBiped.", "")
						'aParentControlBoneName = aParentControlBone.theName.Replace("ValveBiped.", "")
						'aControlBoneName = aControlBone.theName.Replace("ValveBiped.", "")
						aBoneName = StringClass.RemoveUptoAndIncludingFirstDotCharacterFromString(aBone.theName)
						aParentBoneName = StringClass.RemoveUptoAndIncludingFirstDotCharacterFromString(aParentBone.theName)
						aParentControlBoneName = StringClass.RemoveUptoAndIncludingFirstDotCharacterFromString(aParentControlBone.theName)
						aControlBoneName = StringClass.RemoveUptoAndIncludingFirstDotCharacterFromString(aControlBone.theName)

						Me.theOutputFileStreamWriter.WriteLine()

						line = "<helper>"
						line += " "
						line += aBoneName
						line += " "
						line += aParentBoneName
						line += " "
						line += aParentControlBoneName
						line += " "
						line += aControlBoneName
						Me.theOutputFileStreamWriter.WriteLine(line)

						''NOTE: Use "1" for the 3 size values because it looks like they are not used in compile.
						'line = "<display>"
						'line += " "
						'line += "1"
						'line += " "
						'line += "1"
						'line += " "
						'line += "1"
						'line += " "
						''TODO: Reverse this to decompile.
						''pAxis->percentage = distance / 100.0;
						''tmp = pInterp->pos[k] + pInterp->basepos + g_bonetable[pInterp->control].pos * pInterp->percentage;
						'line += "100"
						'Me.theOutputFileStreamWriter.WriteLine(line)

						line = "<basepos>"
						line += " "
						line += "0"
						line += " "
						line += "0"
						line += " "
						line += "0"
						Me.theOutputFileStreamWriter.WriteLine(line)

						For triggerIndex As Integer = 0 To aBone.theQuatInterpBone.theTriggers.Count - 1
							aTrigger = aBone.theQuatInterpBone.theTriggers(triggerIndex)

							aTriggerTrigger = MathModule.ToEulerAngles(aTrigger.trigger)
							aTriggerQuat = MathModule.ToEulerAngles(aTrigger.quat)

							line = "<trigger>"
							line += " "
							'pAxis->tolerance[j] = DEG2RAD( tolerance );
							line += MathModule.RadiansToDegrees(1 / aTrigger.inverseToleranceAngle).ToString("0.######", TheApp.InternalNumberFormat)

							'trigger.x = DEG2RAD( trigger.x );
							'trigger.y = DEG2RAD( trigger.y );
							'trigger.z = DEG2RAD( trigger.z );
							'AngleQuaternion( trigger, pAxis->trigger[j] );
							line += " "
							line += MathModule.RadiansToDegrees(aTriggerTrigger.x).ToString("0.######", TheApp.InternalNumberFormat)
							line += " "
							line += MathModule.RadiansToDegrees(aTriggerTrigger.y).ToString("0.######", TheApp.InternalNumberFormat)
							line += " "
							line += MathModule.RadiansToDegrees(aTriggerTrigger.z).ToString("0.######", TheApp.InternalNumberFormat)
							'line += " "
							'line += MathModule.RadiansToDegrees(aTriggerTrigger.z).ToString("0.######", TheApp.InternalNumberFormat)
							'line += " "
							'line += MathModule.RadiansToDegrees(aTriggerTrigger.y).ToString("0.######", TheApp.InternalNumberFormat)
							'line += " "
							'line += MathModule.RadiansToDegrees(aTriggerTrigger.x).ToString("0.######", TheApp.InternalNumberFormat)

							'ang.x = DEG2RAD( ang.x );
							'ang.y = DEG2RAD( ang.y );
							'ang.z = DEG2RAD( ang.z );
							'AngleQuaternion( ang, pAxis->quat[j] );
							line += " "
							line += MathModule.RadiansToDegrees(aTriggerQuat.x).ToString("0.######", TheApp.InternalNumberFormat)
							line += " "
							line += MathModule.RadiansToDegrees(aTriggerQuat.y).ToString("0.######", TheApp.InternalNumberFormat)
							line += " "
							line += MathModule.RadiansToDegrees(aTriggerQuat.z).ToString("0.######", TheApp.InternalNumberFormat)
							'line += " "
							'line += MathModule.RadiansToDegrees(aTriggerQuat.z).ToString("0.######", TheApp.InternalNumberFormat)
							'line += " "
							'line += MathModule.RadiansToDegrees(aTriggerQuat.y).ToString("0.######", TheApp.InternalNumberFormat)
							'line += " "
							'line += MathModule.RadiansToDegrees(aTriggerQuat.x).ToString("0.######", TheApp.InternalNumberFormat)

							'VectorAdd( basepos, pos, pAxis->pos[j] );
							line += " "
							line += aTrigger.pos.x.ToString("0.######", TheApp.InternalNumberFormat)
							line += " "
							line += aTrigger.pos.y.ToString("0.######", TheApp.InternalNumberFormat)
							line += " "
							line += aTrigger.pos.z.ToString("0.######", TheApp.InternalNumberFormat)
							Me.theOutputFileStreamWriter.WriteLine(line)
						Next
					End If
				End If
			Next
		End If
	End Sub

#End Region

#Region "Private Delegates"

#End Region

#Region "Private Methods"

#End Region

#Region "Data"

	Private theOutputFileStreamWriter As StreamWriter
	Private theMdlFileData As SourceMdlFileData48

#End Region

End Class
