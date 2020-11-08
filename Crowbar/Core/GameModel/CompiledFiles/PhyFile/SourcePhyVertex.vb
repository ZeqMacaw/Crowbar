Imports System.Collections

Public Class SourcePhyVertex

	Public Sub New()
		Me.theNormal = New SourceVector()
		Me.theNormalIsNormalized = False
	End Sub

	Public vertex As New SourceVector()

	Public ReadOnly Property Normal As SourceVector
		Get
			If Not Me.theNormalIsNormalized Then
				MathModule.VectorNormalize(Me.theNormal)
				Me.theNormalIsNormalized = True
			End If
			Return Me.theNormal
		End Get
	End Property

	Public ReadOnly Property UnnormalizedNormal As SourceVector
		Get
			Return Me.theNormal
		End Get
	End Property

	Private theNormal As SourceVector
	Private theNormalIsNormalized As Boolean

End Class
