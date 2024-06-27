namespace SharpTables
{
	internal static class Utils
	{
		public static int MeasureStringWidth(string str)
		{
			if (str == null)
				throw new ArgumentNullException(nameof(str));

			int width = 0;
			foreach (char c in str)
			{
				if (char.IsHighSurrogate(c)) // Handle surrogate pairs (for Unicode characters outside the BMP)
					continue;

				// Assuming a fixed width font, each character occupies one column
				width++;
			}
			return width;
		}
		public static string ResizeStringToWidth(string str, int targetWidth)
		{
			if (str == null)
				throw new ArgumentNullException(nameof(str));
			if (targetWidth < 0)
				throw new ArgumentOutOfRangeException(nameof(targetWidth), "Target width must be non-negative");

			int currentWidth = MeasureStringWidth(str);

			if (currentWidth > targetWidth)
			{
				// Truncate the string
				return str.Substring(0, targetWidth);
			}
			else
			{
				// Add whitespace until the string is the target width
				return str.PadRight(targetWidth);
			}
		}

		public static string ResizeStringToWidth(string str, int targetWidth, Alignment alignment)
		{
			if (str == null)
				throw new ArgumentNullException(nameof(str));
			if (targetWidth < 0)
				throw new ArgumentOutOfRangeException(nameof(targetWidth), "Target width must be non-negative");

			int currentWidth = MeasureStringWidth(str);

			if (currentWidth > targetWidth)
			{
				// Truncate the string
				return str.Substring(0, targetWidth);
			}
			else
			{
				// Add whitespace until the string is the target width
				switch (alignment)
				{
					case Alignment.Left:
						return str.PadRight(targetWidth);
					case Alignment.Center:
						int padding = targetWidth - currentWidth;
						int leftPadding = padding / 2;
						int rightPadding = padding - leftPadding;
						return str.PadLeft(currentWidth + leftPadding).PadRight(targetWidth);
					case Alignment.Right:
						return str.PadLeft(targetWidth);
					default:
						throw new ArgumentOutOfRangeException(nameof(alignment), alignment, "Invalid alignment");
				}
			}
		}
	}
}
