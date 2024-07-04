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


		/// <summary>
		/// Gets the position of the cell in the table.
		/// </summary>
		public Vector2 Position { get; internal set; }

		/// <summary>
		/// Gets or sets the cell's text alignment
		/// </summary>
		public Alignment Alignment { get; set; } = Alignment.Left;
		private readonly Type? type;

		/// <summary>
		/// The type of the cell's value.
		/// </summary>
		public Type? Type
		{
			get { return type; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Cell"/> class with the specified text.
		/// The color of the cell is set to white.
		/// </summary>
		/// <param name="text">The text content of the cell.</param>
		public Cell(object text)
		{
			Text = text?.ToString() ?? "";
			Color = ConsoleColor.White;
			type = text?.GetType() ?? null;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Cell"/> class with the specified text and color.
		/// </summary>
		/// <param name="text">The text content of the cell.</param>
		/// <param name="color">The color of the cell.</param>
		public Cell(object text, ConsoleColor color)
        {
            Text = text?.ToString() ?? "null";
            Color = color;
            type = text?.GetType() ?? null;
        }

		public T GetValue<T>()
		{
			return (T)Convert.ChangeType(Text, typeof(T));
		}

		/// <summary>
		/// Gets a value indicating whether the text content of the cell is numeric.
		/// </summary>
		public bool IsNumeric => double.TryParse(Text, out _);

		/// <summary>
		/// Gets a value indicating whether the text content of the cell is a boolean.
		/// </summary>
		public bool IsBool => bool.TryParse(Text, out _);

		/// <summary>
		/// Gets a value indicating whether the cell has a null value.
		/// </summary>
		public bool IsNull => type is null;
	}
}
