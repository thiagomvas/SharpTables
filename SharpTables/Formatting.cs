namespace SharpTables
{
	/// <summary>
	/// Represents the formatting options for tables in SharpTables.
	/// </summary>
	public record Formatting
	{
		#region Properties

		/// <summary>
		/// Gets or sets the character used for the top left corner of the table.
		/// </summary>
		public char TopLeftDivider { get; set; } = '┌';

		/// <summary>
		/// Gets or sets the character used for the top right corner of the table.
		/// </summary>
		public char TopRightDivider { get; set; } = '┐';

		/// <summary>
		/// Gets or sets the character used for the bottom left corner of the table.
		/// </summary>
		public char BottomLeftDivider { get; set; } = '└';

		/// <summary>
		/// Gets or sets the character used for the bottom right corner of the table.
		/// </summary>
		public char BottomRightDivider { get; set; } = '┘';

		/// <summary>
		/// Gets or sets the character used for the horizontal divider of the table.
		/// </summary>
		public char HorizontalDivider { get; set; } = '─';

		/// <summary>
		/// Gets or sets the character used for the vertical divider of the table.
		/// </summary>
		public char VerticalDivider { get; set; } = '│';

		/// <summary>
		/// Gets or sets the character used for the top middle divider of the table.
		/// </summary>
		public char TopMiddleDivider { get; set; } = '┬';

		/// <summary>
		/// Gets or sets the character used for the bottom middle divider of the table.
		/// </summary>
		public char BottomMiddleDivider { get; set; } = '┴';

		/// <summary>
		/// Gets or sets the character used for the middle divider of the table.
		/// </summary>
		public char MiddleDivider { get; set; } = '┼';

		/// <summary>
		/// Gets or sets the character used for the left middle divider of the table.
		/// </summary>
		public char LeftMiddleDivider { get; set; } = '├';

		/// <summary>
		/// Gets or sets the character used for the right middle divider of the table.
		/// </summary>
		public char RightMiddleDivider { get; set; } = '┤';

		/// <summary>
		/// Gets or sets the color of the dividers in the table.
		/// </summary>
		public ConsoleColor DividerColor { get; set; } = ConsoleColor.DarkGray;

		/// <summary>
		/// Gets or sets the formatting options for the table header.
		/// </summary>
		public HeaderFormatting Header { get; set; } = new HeaderFormatting();

		#endregion

		#region Presets

		/// <summary>
		/// Gets the default formatting options for tables.
		/// </summary>
		public static readonly Formatting Default = new();

		/// <summary>
		/// Gets the ASCII formatting options for tables.
		/// </summary>
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

		/// <summary>
		/// Gets the double lined formatting options for tables.
		/// </summary>
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

		/// <summary>
		/// Gets the single lined formatting options for tables.
		/// </summary>
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

		/// <summary>
		/// Gets the minimalist formatting options for tables.
		/// </summary>
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
				TopLeftDivider = '─',
				TopRightDivider = '─',
				BottomLeftDivider = '─',
				BottomRightDivider = '─',
				HorizontalDivider = '─',
				VerticalDivider = ' ',
				TopMiddleDivider = '─',
				BottomMiddleDivider = '─',
				MiddleDivider = '─',
				LeftMiddleDivider = '─',
				RightMiddleDivider = '─',
				DividerColor = ConsoleColor.White,
				HasTopDivider = false
			}
		};

		#endregion

		/// <summary>
		/// Represents the formatting options for the table header.
		/// </summary>
		public record HeaderFormatting
		{
			/// <summary>
			/// Gets or sets the character used for the top left corner of the header.
			/// </summary>
			public char TopLeftDivider { get; set; } = '╔';

			/// <summary>
			/// Gets or sets the character used for the top right corner of the header.
			/// </summary>
			public char TopRightDivider { get; set; } = '╗';

			/// <summary>
			/// Gets or sets the character used for the bottom left corner of the header.
			/// </summary>
			public char BottomLeftDivider { get; set; } = '╚';

			/// <summary>
			/// Gets or sets the character used for the bottom right corner of the header.
			/// </summary>
			public char BottomRightDivider { get; set; } = '╝';

			/// <summary>
			/// Gets or sets the character used for the horizontal divider of the header.
			/// </summary>
			public char HorizontalDivider { get; set; } = '═';

			/// <summary>
			/// Gets or sets the character used for the vertical divider of the header.
			/// </summary>
			public char VerticalDivider { get; set; } = '║';

			/// <summary>
			/// Gets or sets the character used for the top middle divider of the header.
			/// </summary>
			public char TopMiddleDivider { get; set; } = '╦';

			/// <summary>
			/// Gets or sets the character used for the bottom middle divider of the header.
			/// </summary>
			public char BottomMiddleDivider { get; set; } = '╩';

			/// <summary>
			/// Gets or sets the character used for the middle divider of the header.
			/// </summary>
			public char MiddleDivider { get; set; } = '╬';

			/// <summary>
			/// Gets or sets the character used for the left middle divider of the header.
			/// </summary>
			public char LeftMiddleDivider { get; set; } = '╠';

			/// <summary>
			/// Gets or sets the character used for the right middle divider of the header.
			/// </summary>
			public char RightMiddleDivider { get; set; } = '╣';

			/// <summary>
			/// Gets or sets the color of the dividers in the header.
			/// </summary>
			public ConsoleColor DividerColor { get; set; } = ConsoleColor.White;

			/// <summary>
			/// Gets or sets a value indicating whether the header has a top divider.
			/// </summary>
			public bool HasTopDivider { get; set; } = true;
		}
	}

}
