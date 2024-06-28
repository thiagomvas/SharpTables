using System.Numerics;

namespace SharpTables
{
	/// <summary>
	/// Represents a cell in a table.
	/// </summary>
	public class Cell
	{
		/// <summary>
		/// Gets or sets the text content of the cell.
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Gets or sets the color of the cell.
		/// </summary>
		public ConsoleColor Color { get; set; }

		/// <summary>
		/// Gets or sets the padding value of the cell.
		/// </summary>
		public int Padding { get; set; } = 5;

		public Vector2 Position { get; internal set; }
		public Alignment Alignment { get; set; } = Alignment.Left;

		/// <summary>
		/// Initializes a new instance of the <see cref="Cell"/> class with the specified text.
		/// The color of the cell is set to white.
		/// </summary>
		/// <param name="text">The text content of the cell.</param>
		public Cell(string text)
		{
			Text = text;
			Color = ConsoleColor.White;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Cell"/> class with the specified text and color.
		/// </summary>
		/// <param name="text">The text content of the cell.</param>
		/// <param name="color">The color of the cell.</param>
		public Cell(string text, ConsoleColor color)
		{
			Text = text;
			Color = color;
		}

		/// <summary>
		/// Gets a value indicating whether the text content of the cell is numeric.
		/// </summary>
		public bool IsNumeric => double.TryParse(Text, out _);
	}
}
