namespace SharpTables
{
	public class Formatting
	{
		#region Properties
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
		#endregion


		#region Presets

		public static readonly Formatting ASCII = new Formatting
		{
			TopLeftDivider = '+',
			TopRightDivider = '+',
			BottomLeftDivider = '+',
			BottomRightDivider = '+',
			HorizontalDivider = '-',
			VerticalDivider = '|',
			TopMiddleDivider = '+',
			BottomMiddleDivider = '+',
			MiddleDivider = '+',
			LeftMiddleDivider = '+',
			RightMiddleDivider = '+',
			DividerColor = ConsoleColor.Gray,
			Header = new HeaderFormatting
			{
				TopLeftDivider = '+',
				TopRightDivider = '+',
				BottomLeftDivider = '+',
				BottomRightDivider = '+',
				HorizontalDivider = '-',
				VerticalDivider = '|',
				TopMiddleDivider = '+',
				BottomMiddleDivider = '+',
				MiddleDivider = '+',
				LeftMiddleDivider = '+',
				RightMiddleDivider = '+',
				DividerColor = ConsoleColor.White
			}
		};

		public static readonly Formatting DoubleLined = new Formatting
		{
			TopLeftDivider = '╔',
			TopRightDivider = '╗',
			BottomLeftDivider = '╚',
			BottomRightDivider = '╝',
			HorizontalDivider = '═',
			VerticalDivider = '║',
			TopMiddleDivider = '╦',
			BottomMiddleDivider = '╩',
			MiddleDivider = '╬',
			LeftMiddleDivider = '╠',
			RightMiddleDivider = '╣',
			DividerColor = ConsoleColor.DarkGray,
			Header = new HeaderFormatting
			{
				TopLeftDivider = '╔',
				TopRightDivider = '╗',
				BottomLeftDivider = '╚',
				BottomRightDivider = '╝',
				HorizontalDivider = '═',
				VerticalDivider = '║',
				TopMiddleDivider = '╦',
				BottomMiddleDivider = '╩',
				MiddleDivider = '╬',
				LeftMiddleDivider = '╠',
				RightMiddleDivider = '╣',
				DividerColor = ConsoleColor.White
			}
		};

		public static readonly Formatting SingleLined = new Formatting
		{
			TopLeftDivider = '┌',
			TopRightDivider = '┐',
			BottomLeftDivider = '└',
			BottomRightDivider = '┘',
			HorizontalDivider = '─',
			VerticalDivider = '│',
			TopMiddleDivider = '┬',
			BottomMiddleDivider = '┴',
			MiddleDivider = '┼',
			LeftMiddleDivider = '├',
			RightMiddleDivider = '┤',
			DividerColor = ConsoleColor.DarkGray,
			Header = new HeaderFormatting
			{
				TopLeftDivider = '┌',
				TopRightDivider = '┐',
				BottomLeftDivider = '└',
				BottomRightDivider = '┘',
				HorizontalDivider = '─',
				VerticalDivider = '│',
				TopMiddleDivider = '┬',
				BottomMiddleDivider = '┴',
				MiddleDivider = '┼',
				LeftMiddleDivider = '├',
				RightMiddleDivider = '┤',
				DividerColor = ConsoleColor.White
			}
		};

		public static readonly Formatting Minimalist = new Formatting
		{
			TopLeftDivider = ' ',
			TopRightDivider = ' ',
			BottomLeftDivider = ' ',
			BottomRightDivider = ' ',
			HorizontalDivider = ' ',
			VerticalDivider = ' ',
			TopMiddleDivider = ' ',
			BottomMiddleDivider = ' ',
			MiddleDivider = ' ',
			LeftMiddleDivider = ' ',
			RightMiddleDivider = ' ',
			DividerColor = ConsoleColor.DarkGray,
			Header = new HeaderFormatting
			{
				TopLeftDivider = '-',
				TopRightDivider = '-',
				BottomLeftDivider = '-',
				BottomRightDivider = '-',
				HorizontalDivider = '─', 
				VerticalDivider = ' ',
				TopMiddleDivider = '-',
				BottomMiddleDivider = '-',
				MiddleDivider = '-',
				LeftMiddleDivider = '-',
				RightMiddleDivider = '-',
				DividerColor = ConsoleColor.White,
				HasTopDivider = false
			}
		};
		#endregion

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
			public bool HasTopDivider { get; set; } = true;
		}
	}

}
