Public Class SourceMdlFileData04
	Inherits SourceMdlFileDataBase

	Public Sub New()
		MyBase.New()

		Me.theChecksumIsValid = False
	End Sub

	'Public id(3) As Char
	'Public version As Integer
	Public unknown01 As Integer

	Public boneCount As Integer
	Public bodyPartCount As Integer
	' modelCount? Why would this value be here if it's already in SourceMdlBodyPart04?
	Public unknownCount As Integer
	Public sequenceDescCount As Integer
	' Total frames from all sequences + an extra for each sequence.
	Public sequenceFrameCount As Integer

	Public unknown02 As Integer

	Public theBodyParts As List(Of SourceMdlBodyPart04)
	Public theBones As List(Of SourceMdlBone04)
	Public theSequenceDescs As List(Of SourceMdlSequenceDesc04)

End Class
