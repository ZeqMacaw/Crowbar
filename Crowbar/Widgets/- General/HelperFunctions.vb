Module HelperFunctions

	Public Function MouseIsOverControl(ByVal c As Control) As Boolean
		'Console.WriteLine(" MouseIsOverControl")
		''Console.WriteLine("  c.ClientRectangle: " + c.ClientRectangle.ToString())
		'Console.WriteLine("  c.DisplayRectangle: " + c.DisplayRectangle.ToString())
		'Console.WriteLine("  Cursor.Position: " + Cursor.Position.ToString())
		'Console.WriteLine("  c.PointToClient(Cursor.Position): " + c.PointToClient(Cursor.Position).ToString())
		Return c.DisplayRectangle.Contains(c.PointToClient(Cursor.Position))
	End Function

End Module
