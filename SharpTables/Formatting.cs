namespace SharpTables
{
	public class Formatting
	{
		public char TopLeftDivider { get; set; } = '┌';
		public char TopRightDivider { get; set; } = '┐';
		public char BottomLeftDivider { get; set; } = '└';
		public char BottomRightDivider { get; set; } = '┘';
		public char HorizontalDivider { get; set; } = '─';
		public char VerticalDivider { get; set; } = '│';
		public char TopMiddleDivider { get; set; } = '┬';
		public char BottomMiddleDivider { get; set; } = '┴';
		public char MiddleDivider { get; set; } = '┼';
		public char LeftMiddleDivider { get; set; } = '├';
		public char RightMiddleDivider { get; set; } = '┤';
		public ConsoleColor DividerColor { get; set; } = ConsoleColor.DarkGray;

		public HeaderFormatting Header { get; set; } = new HeaderFormatting();

		public class HeaderFormatting
		{
			public char TopLeftDivider { get; set; } = '╔';
			public char TopRightDivider { get; set; } = '╗';
			public char BottomLeftDivider { get; set; } = '╚';
			public char BottomRightDivider { get; set; } = '╝';
			public char HorizontalDivider { get; set; } = '═';
			public char VerticalDivider { get; set; } = '║';
			public char TopMiddleDivider { get; set; } = '╦';
			public char BottomMiddleDivider { get; set; } = '╩';
			public char MiddleDivider { get; set; } = '╬';
			public char LeftMiddleDivider { get; set; } = '╠';
			public char RightMiddleDivider { get; set; } = '╣';
			public ConsoleColor DividerColor { get; set; } = ConsoleColor.White;
		}
	}

}
