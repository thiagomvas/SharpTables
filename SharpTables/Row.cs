namespace SharpTables
{
	/// <summary>
	/// Represents a row in a table.
	/// </summary>
	public class Row
	{
		/// <summary>
		/// Gets or sets the list of cells in the row.
		/// </summary>
		public List<Cell> Cells { get; set; }

		public int LineIndex { get; internal set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Row"/> class.
		/// </summary>
		public Row()
		{
			Cells = new List<Cell>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Row"/> class with the specified values.
		/// </summary>
		/// <param name="values">The values of each cell in this row, in order</param>
		public Row(params object[] values)
		{
			Cells = values.Select(v => new Cell(v?.ToString() ?? string.Empty)).ToList();
		}



		/// <summary>
		/// Initializes a new instance of the <see cref="Row"/> class with the specified cells.
		/// </summary>
		/// <param name="cells">The cells to add to the row.</param>
		public Row(IEnumerable<object> cells)
		{
			Cells = cells.Select(c => new Cell(c?.ToString() ?? string.Empty)).ToList();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Row"/> class with the specified cells.
		/// </summary>
		/// <param name="cells">The cells to add to the row.</param>
		public Row(IEnumerable<Cell> cells)
		{
			Cells = cells.ToList();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Row"/> class with the specified cells and preset cell properties.
		/// </summary>
		/// <param name="cells">The cells to add to the row.</param>
		/// <param name="preset">The preset cell properties to apply to each cell.</param>
		public Row(IEnumerable<object> cells, Cell preset)
		{
			Cells = cells.Select(c => new Cell(c?.ToString() ?? string.Empty) { Color = preset.Color, Padding = preset.Padding }).ToList();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Row"/> class with the specified cells and preset cell properties.
		/// </summary>
		/// <param name="cells">The cells to add to the row.</param>
		/// <param name="preset">The preset cell properties to apply to each cell.</param>
		public Row(IEnumerable<Cell> cells, Cell preset)
		{
			Cells = cells.Select(c => new Cell(c.Text) { Color = preset.Color, Padding = preset.Padding }).ToList();
		}

	}
}
