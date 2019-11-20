Public Class SourceMdlAutoLayer37

	'struct mstudioautolayer_t
	'{
	'	int	iSequence;
	'	int	flags;
	'	float	start;	// beginning of influence
	'	float	peak;	// start of full influence
	'	float	tail;	// end of full influence
	'	float	end;	// end of all influence
	'};

	Public sequenceIndex As Integer
	Public flags As Integer
	Public influenceStart As Double
	Public influencePeak As Double
	Public influenceTail As Double
	Public influenceEnd As Double

End Class
