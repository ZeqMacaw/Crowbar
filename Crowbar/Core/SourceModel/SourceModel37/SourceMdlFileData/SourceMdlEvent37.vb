Public Class SourceMdlEvent37

	'struct mstudioevent_t
	'{
	'	float	cycle;
	'	int	event;
	'	int	type;
	'	char	options[64];
	'};

	Public cycle As Double
	Public eventIndex As Integer
	'NOTE: Does not seem to be used, even though it takes up space in the MDL file.
	Public eventType As Integer
	Public options(63) As Char

End Class
