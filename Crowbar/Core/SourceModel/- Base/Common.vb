Imports System.IO

Module Common

	Public Function ReadPhyCollisionTextSection(ByVal theInputFileReader As BinaryReader, ByVal endOffset As Long) As String
		Dim result As String = ""
		Dim streamLastPosition As Long

		Try
			'streamLastPosition = theInputFileReader.BaseStream.Length() - 1
			streamLastPosition = endOffset

			If streamLastPosition > theInputFileReader.BaseStream.Position Then
				'NOTE: Use -1 to avoid including the null terminator character.
				result = theInputFileReader.ReadChars(CInt(streamLastPosition - theInputFileReader.BaseStream.Position - 1))
				' Read the NULL byte to help with debug logging.
				theInputFileReader.ReadChar()
				' Only grab text to the first NULL byte. (Needed for PHY data stored within Titanfall 2 MDL file.)
				result = result.Substring(0, result.IndexOf(Chr(0)))
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		Return result
	End Function

	Public Function GetFlexRule(ByVal flexDescs As List(Of SourceMdlFlexDesc), ByVal flexControllers As List(Of SourceMdlFlexController), ByVal flexRule As SourceMdlFlexRule) As String
		Dim flexRuleEquation As String
		flexRuleEquation = vbTab
		flexRuleEquation += "%"
		flexRuleEquation += flexDescs(flexRule.flexIndex).theName
		flexRuleEquation += " = "
		If flexRule.theFlexOps IsNot Nothing AndAlso flexRule.theFlexOps.Count > 0 Then
			Dim aFlexOp As SourceMdlFlexOp
			Dim dmxFlexOpWasUsed As Boolean

			' Convert to infix notation.

			Dim stack As Stack(Of IntermediateExpression) = New Stack(Of IntermediateExpression)()
			Dim rightExpr As String
			Dim leftExpr As String

			dmxFlexOpWasUsed = False
			For i As Integer = 0 To flexRule.theFlexOps.Count - 1
				aFlexOp = flexRule.theFlexOps(i)
				If aFlexOp.op = SourceMdlFlexOp.STUDIO_CONST Then
					stack.Push(New IntermediateExpression(Math.Round(aFlexOp.value, 6).ToString("0.######", TheApp.InternalNumberFormat), 10))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_FETCH1 Then
					'int m = pFlexcontroller( (LocalFlexController_t)pops->d.index)->localToGlobal;
					'stack[k] = src[m];
					'k++; 
					stack.Push(New IntermediateExpression(flexControllers(aFlexOp.index).theName, 10))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_FETCH2 Then
					stack.Push(New IntermediateExpression("%" + flexDescs(aFlexOp.index).theName, 10))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_ADD Then
					Dim rightIntermediate As IntermediateExpression = stack.Pop()
					Dim leftIntermediate As IntermediateExpression = stack.Pop()

					Dim newExpr As String = Convert.ToString(leftIntermediate.theExpression) + " + " + Convert.ToString(rightIntermediate.theExpression)
					stack.Push(New IntermediateExpression(newExpr, 1))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_SUB Then
					Dim rightIntermediate As IntermediateExpression = stack.Pop()
					Dim leftIntermediate As IntermediateExpression = stack.Pop()

					Dim newExpr As String = Convert.ToString(leftIntermediate.theExpression) + " - " + Convert.ToString(rightIntermediate.theExpression)
					stack.Push(New IntermediateExpression(newExpr, 1))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_MUL Then
					Dim rightIntermediate As IntermediateExpression = stack.Pop()
					If rightIntermediate.thePrecedence < 2 Then
						rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")"
					Else
						rightExpr = rightIntermediate.theExpression
					End If

					Dim leftIntermediate As IntermediateExpression = stack.Pop()
					If leftIntermediate.thePrecedence < 2 Then
						leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")"
					Else
						leftExpr = leftIntermediate.theExpression
					End If

					Dim newExpr As String = leftExpr + " * " + rightExpr
					stack.Push(New IntermediateExpression(newExpr, 2))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_DIV Then
					Dim rightIntermediate As IntermediateExpression = stack.Pop()
					If rightIntermediate.thePrecedence < 2 Then
						rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")"
					Else
						rightExpr = rightIntermediate.theExpression
					End If

					Dim leftIntermediate As IntermediateExpression = stack.Pop()
					If leftIntermediate.thePrecedence < 2 Then
						leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")"
					Else
						leftExpr = leftIntermediate.theExpression
					End If

					Dim newExpr As String = leftExpr + " / " + rightExpr
					stack.Push(New IntermediateExpression(newExpr, 2))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_NEG Then
					Dim rightIntermediate As IntermediateExpression = stack.Pop()

					Dim newExpr As String = "-" + rightIntermediate.theExpression
					stack.Push(New IntermediateExpression(newExpr, 10))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_EXP Then
					Dim ignoreThisOpBecauseItIsMistakeToBeHere As Integer = 4242
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_OPEN Then
					Dim ignoreThisOpBecauseItIsMistakeToBeHere As Integer = 4242
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_CLOSE Then
					Dim ignoreThisOpBecauseItIsMistakeToBeHere As Integer = 4242
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_COMMA Then
					Dim ignoreThisOpBecauseItIsMistakeToBeHere As Integer = 4242
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_MAX Then
					Dim rightIntermediate As IntermediateExpression = stack.Pop()
					If rightIntermediate.thePrecedence < 5 Then
						rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")"
					Else
						rightExpr = rightIntermediate.theExpression
					End If

					Dim leftIntermediate As IntermediateExpression = stack.Pop()
					If leftIntermediate.thePrecedence < 5 Then
						leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")"
					Else
						leftExpr = leftIntermediate.theExpression
					End If

					Dim newExpr As String = "max(" + leftExpr + ", " + rightExpr + ")"
					stack.Push(New IntermediateExpression(newExpr, 5))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_MIN Then
					Dim rightIntermediate As IntermediateExpression = stack.Pop()
					If rightIntermediate.thePrecedence < 5 Then
						rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")"
					Else
						rightExpr = rightIntermediate.theExpression
					End If

					Dim leftIntermediate As IntermediateExpression = stack.Pop()
					If leftIntermediate.thePrecedence < 5 Then
						leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")"
					Else
						leftExpr = leftIntermediate.theExpression
					End If

					Dim newExpr As String = "min(" + leftExpr + ", " + rightExpr + ")"
					stack.Push(New IntermediateExpression(newExpr, 5))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_2WAY_0 Then
					'TODO: SourceMdlFlexOp.STUDIO_2WAY_0
					'	'#define STUDIO_2WAY_0	15	// Fetch a value from a 2 Way slider for the 1st value RemapVal( 0.0, 0.5, 0.0, 1.0 )
					'	'int m = pFlexcontroller( (LocalFlexController_t)pops->d.index )->localToGlobal;
					'	'stack[ k ] = RemapValClamped( src[m], -1.0f, 0.0f, 1.0f, 0.0f );
					'	'k++; 
					Dim newExpression As String
					'	= C + (D - C) * (min(max((val - A) / (B - A), 0.0f), 1.0f))
					'	"1 - (min(max(" + flexControllers(aFlexOp.index).theName + " + 1, 0), 1))"
					newExpression = "(1 - (min(max(" + flexControllers(aFlexOp.index).theName + " + 1, 0), 1)))"
					stack.Push(New IntermediateExpression(newExpression, 5))
					dmxFlexOpWasUsed = True
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_2WAY_1 Then
					'TODO:			   SourceMdlFlexOp.STUDIO_2WAY_1()
					'#define STUDIO_2WAY_1	16	// Fetch a value from a 2 Way slider for the 2nd value RemapVal( 0.5, 1.0, 0.0, 1.0 )
					'int m = pFlexcontroller( (LocalFlexController_t)pops->d.index )->localToGlobal;
					'stack[ k ] = RemapValClamped( src[m], 0.0f, 1.0f, 0.0f, 1.0f );
					'k++; 
					Dim newExpression As String
					'	= C + (D - C) * (min(max((val - A) / (B - A), 0.0f), 1.0f))
					'	"(min(max(" + flexControllers(aFlexOp.index).theName + ", 0), 1))"
					newExpression = "(min(max(" + flexControllers(aFlexOp.index).theName + ", 0), 1))"
					stack.Push(New IntermediateExpression(newExpression, 5))
					dmxFlexOpWasUsed = True
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_NWAY Then
					'TODO:			   SourceMdlFlexOp.STUDIO_NWAY()
					Dim v As SourceMdlFlexController
					v = flexControllers(aFlexOp.index)

					Dim valueControllerIndex As IntermediateExpression
					Dim flValue As String
					valueControllerIndex = stack.Pop()
					flValue = flexControllers(CInt(valueControllerIndex.theExpression)).theName

					Dim filterRampW As IntermediateExpression
					Dim filterRampZ As IntermediateExpression
					Dim filterRampY As IntermediateExpression
					Dim filterRampX As IntermediateExpression
					filterRampW = stack.Pop()
					filterRampZ = stack.Pop()
					filterRampY = stack.Pop()
					filterRampX = stack.Pop()

					Dim greaterThanX As String
					Dim lessThanY As String
					Dim remapX As String
					Dim greaterThanEqualY As String
					Dim lessThanEqualZ As String
					Dim greaterThanZ As String
					Dim lessThanW As String
					Dim remapZ As String
					greaterThanX = "min(1, (-min(0, (" + filterRampX.theExpression + " - " + flValue + "))))"
					lessThanY = "min(1, (-min(0, (" + flValue + " - " + filterRampY.theExpression + "))))"
					remapX = "min(max((" + flValue + " - " + filterRampX.theExpression + ") / (" + filterRampY.theExpression + " - " + filterRampX.theExpression + "), 0), 1)"
					greaterThanEqualY = "-(min(1, (-min(0, (" + flValue + " - " + filterRampY.theExpression + ")))) - 1)"
					lessThanEqualZ = "-(min(1, (-min(0, (" + filterRampZ.theExpression + " - " + flValue + ")))) - 1)"
					greaterThanZ = "min(1, (-min(0, (" + filterRampZ.theExpression + " - " + flValue + "))))"
					lessThanW = "min(1, (-min(0, (" + flValue + " - " + filterRampW.theExpression + "))))"
					remapZ = "(1 - (min(max((" + flValue + " - " + filterRampZ.theExpression + ") / (" + filterRampW.theExpression + " - " + filterRampZ.theExpression + "), 0), 1)))"

					flValue = "((" + greaterThanX + " * " + lessThanY + ") * " + remapX + ") + (" + greaterThanEqualY + " * " + lessThanEqualZ + ") + ((" + greaterThanZ + " * " + lessThanW + ") * " + remapZ + ")"

					Dim newExpression As String
					newExpression = "((" + flValue + ") * (" + v.theName + "))"
					stack.Push(New IntermediateExpression(newExpression, 5))
					dmxFlexOpWasUsed = True
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_COMBO Then
					'#define STUDIO_COMBO	18	// Perform a combo operation (essentially multiply the last N values on the stack)
					'int m = pops->d.index;
					'int km = k - m;
					'for ( int i = km + 1; i < k; ++i )
					'{
					'	stack[ km ] *= stack[ i ];
					'}
					'k = k - m + 1;
					Dim count As Integer
					Dim newExpression As String
					Dim intermediateExp As IntermediateExpression
					count = aFlexOp.index
					newExpression = ""
					intermediateExp = stack.Pop()
					newExpression += intermediateExp.theExpression
					For j As Integer = 2 To count
						intermediateExp = stack.Pop()
						newExpression += " * " + intermediateExp.theExpression
					Next
					newExpression = "(" + newExpression + ")"
					stack.Push(New IntermediateExpression(newExpression, 5))
					dmxFlexOpWasUsed = True
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_DOMINATE Then
					'int m = pops->d.index;
					'int km = k - m;
					'float dv = stack[ km ];
					'for ( int i = km + 1; i < k; ++i )
					'{
					'	dv *= stack[ i ];
					'}
					'stack[ km - 1 ] *= 1.0f - dv;
					'k -= m;
					Dim count As Integer
					Dim newExpression As String
					Dim intermediateExp As IntermediateExpression
					count = aFlexOp.index
					newExpression = ""
					intermediateExp = stack.Pop()
					newExpression += intermediateExp.theExpression
					For j As Integer = 2 To count
						intermediateExp = stack.Pop()
						newExpression += " * " + intermediateExp.theExpression
					Next
					intermediateExp = stack.Pop()
					newExpression = intermediateExp.theExpression + " * (1 - " + newExpression + ")"
					newExpression = "(" + newExpression + ")"
					stack.Push(New IntermediateExpression(newExpression, 5))
					dmxFlexOpWasUsed = True
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_DME_LOWER_EYELID Then
					Dim pCloseLidV As SourceMdlFlexController
					pCloseLidV = flexControllers(aFlexOp.index)
					Dim flCloseLidV As String
					Dim flCloseLidVMin As String = Math.Round(pCloseLidV.min, 6).ToString("0.######", TheApp.InternalNumberFormat)
					Dim flCloseLidVMax As String = Math.Round(pCloseLidV.max, 6).ToString("0.######", TheApp.InternalNumberFormat)
					flCloseLidV = "(min(max((" + pCloseLidV.theName + " - " + flCloseLidVMin + ") / (" + flCloseLidVMax + " - " + flCloseLidVMin + "), 0), 1))"

					Dim closeLidIndex As IntermediateExpression = stack.Pop()
					Dim pCloseLid As SourceMdlFlexController
					pCloseLid = flexControllers(CInt(closeLidIndex.theExpression))
					Dim flCloseLid As String
					Dim flCloseLidMin As String = Math.Round(pCloseLid.min, 6).ToString("0.######", TheApp.InternalNumberFormat)
					Dim flCloseLidMax As String = Math.Round(pCloseLid.max, 6).ToString("0.######", TheApp.InternalNumberFormat)
					flCloseLid = "(min(max((" + pCloseLid.theName + " - " + flCloseLidMin + ") / (" + flCloseLidMax + " - " + flCloseLidMin + "), 0), 1))"

					' Unused, but need to pop it off the stack.
					Dim blinkIndex As IntermediateExpression = stack.Pop()

					Dim eyeUpDownIndex As IntermediateExpression = stack.Pop()
					Dim pEyeUpDown As SourceMdlFlexController
					pEyeUpDown = flexControllers(CInt(eyeUpDownIndex.theExpression))
					Dim flEyeUpDown As String
					Dim flEyeUpDownMin As String = Math.Round(pEyeUpDown.min, 6).ToString("0.######", TheApp.InternalNumberFormat)
					Dim flEyeUpDownMax As String = Math.Round(pEyeUpDown.max, 6).ToString("0.######", TheApp.InternalNumberFormat)
					flEyeUpDown = "(-1 + 2 * (min(max((" + pEyeUpDown.theName + " - " + flEyeUpDownMin + ") / (" + flEyeUpDownMax + " - " + flEyeUpDownMin + "), 0), 1)))"

					Dim newExpression As String
					newExpression = "(min(1, (1 - " + flEyeUpDown + ")) * (1 - " + flCloseLidV + ") * " + flCloseLid + ")"
					stack.Push(New IntermediateExpression(newExpression, 5))
					dmxFlexOpWasUsed = True
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_DME_UPPER_EYELID Then
					Dim pCloseLidV As SourceMdlFlexController
					pCloseLidV = flexControllers(aFlexOp.index)
					Dim flCloseLidV As String
					Dim flCloseLidVMin As String = Math.Round(pCloseLidV.min, 6).ToString("0.######", TheApp.InternalNumberFormat)
					Dim flCloseLidVMax As String = Math.Round(pCloseLidV.max, 6).ToString("0.######", TheApp.InternalNumberFormat)
					flCloseLidV = "(min(max((" + pCloseLidV.theName + " - " + flCloseLidVMin + ") / (" + flCloseLidVMax + " - " + flCloseLidVMin + "), 0), 1))"

					Dim closeLidIndex As IntermediateExpression = stack.Pop()
					Dim pCloseLid As SourceMdlFlexController
					pCloseLid = flexControllers(CInt(closeLidIndex.theExpression))
					Dim flCloseLid As String
					Dim flCloseLidMin As String = Math.Round(pCloseLid.min, 6).ToString("0.######", TheApp.InternalNumberFormat)
					Dim flCloseLidMax As String = Math.Round(pCloseLid.max, 6).ToString("0.######", TheApp.InternalNumberFormat)
					flCloseLid = "(min(max((" + pCloseLid.theName + " - " + flCloseLidMin + ") / (" + flCloseLidMax + " - " + flCloseLidMin + "), 0), 1))"

					' Unused, but need to pop it off the stack.
					Dim blinkIndex As IntermediateExpression = stack.Pop()

					Dim eyeUpDownIndex As IntermediateExpression = stack.Pop()
					Dim pEyeUpDown As SourceMdlFlexController
					pEyeUpDown = flexControllers(CInt(eyeUpDownIndex.theExpression))
					Dim flEyeUpDown As String
					Dim flEyeUpDownMin As String = Math.Round(pEyeUpDown.min, 6).ToString("0.######", TheApp.InternalNumberFormat)
					Dim flEyeUpDownMax As String = Math.Round(pEyeUpDown.max, 6).ToString("0.######", TheApp.InternalNumberFormat)
					flEyeUpDown = "(-1 + 2 * (min(max((" + pEyeUpDown.theName + " - " + flEyeUpDownMin + ") / (" + flEyeUpDownMax + " - " + flEyeUpDownMin + "), 0), 1)))"

					Dim newExpression As String
					newExpression = "(min(1, (1 + " + flEyeUpDown + ")) * " + flCloseLidV + " * " + flCloseLid + ")"
					stack.Push(New IntermediateExpression(newExpression, 5))
					dmxFlexOpWasUsed = True
				Else
					stack.Clear()
					Exit For
				End If
			Next

			' The loop above leaves the final expression on the top of the stack.
			If dmxFlexOpWasUsed Then
				flexRuleEquation += stack.Peek().theExpression + " // WARNING: Expression is an approximation of what can only be done via DMX file."
			ElseIf stack.Count = 1 Then
				flexRuleEquation += stack.Peek().theExpression
			ElseIf stack.Count = 0 OrElse stack.Count > 1 Then
				flexRuleEquation = "// " + flexRuleEquation + stack.Peek().theExpression + " // ERROR: Unknown flex operation."
			Else
				flexRuleEquation = "// [Empty flex rule found and ignored.]"
			End If
		End If
		Return flexRuleEquation
	End Function

	Public Sub ProcessTexturePaths(ByVal theTexturePaths As List(Of String), ByVal theTextures As List(Of SourceMdlTexture), ByVal theModifiedTexturePaths As List(Of String), ByVal theModifiedTextureFileNames As List(Of String))
		If theTexturePaths IsNot Nothing Then
			For Each aTexturePath As String In theTexturePaths
				theModifiedTexturePaths.Add(aTexturePath)
			Next
		End If
		If theTextures IsNot Nothing Then
			For Each aTexture As SourceMdlTexture In theTextures
				theModifiedTextureFileNames.Add(aTexture.thePathFileName)
			Next
		End If

		If TheApp.Settings.DecompileRemovePathFromSmdMaterialFileNamesIsChecked Then
			'SourceFileNamesModule.CopyPathsFromTextureFileNamesToTexturePaths(theModifiedTexturePaths, theModifiedTextureFileNames)
			SourceFileNamesModule.MovePathsFromTextureFileNamesToTexturePaths(theModifiedTexturePaths, theModifiedTextureFileNames)
		End If
	End Sub

	Public Sub WriteHeaderComment(ByVal outputFileStreamWriter As StreamWriter)
		If Not TheApp.Settings.DecompileStricterFormatIsChecked Then
			Dim line As String = ""

			line = "// "
			line += TheApp.GetHeaderComment()
			outputFileStreamWriter.WriteLine(line)
		End If
	End Sub

End Module
