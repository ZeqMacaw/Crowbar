Public Class SourceMdlVertex31

	Public Sub New()
		Me.boneWeight = New SourceMdlBoneWeight31()
		Me.position = New SourceVector()
		Me.normal = New SourceVector()
	End Sub

	Public boneWeight As SourceMdlBoneWeight31
	Public position As SourceVector
	Public normal As SourceVector
	Public texCoordX As Double
	Public texCoordY As Double

End Class
