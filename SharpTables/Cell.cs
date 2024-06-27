namespace SharpTables
{
	public class Cell
	{
		public string Text { get; set; }
		public ConsoleColor Color { get; set; }
		public int PaddingRight { get; set; } = 5;
		public Cell(string text)
		{
			Text = text;
			Color = ConsoleColor.White;
		}
		public Cell(string text, ConsoleColor color)
		{
			Text = text;
			Color = color;
		}
	}
}
