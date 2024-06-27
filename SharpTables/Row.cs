namespace SharpTables
{
	public class Row
	{
		public List<Cell> Cells { get; set; }
		public Row()
		{
			Cells = new List<Cell>();
		}
		public Row(IEnumerable<object> cells)
		{
			Cells = cells.Select(c => new Cell(c?.ToString() ?? string.Empty)).ToList();
		}

		public Row(IEnumerable<Cell> cells)
		{
			Cells = cells.ToList();
		}

		public Row(IEnumerable<object> cells, Cell preset)
		{
			Cells = cells.Select(c => new Cell(c?.ToString() ?? string.Empty) { Color = preset.Color, PaddingRight = preset.PaddingRight}).ToList();
		}
		public Row(IEnumerable<Cell> cells, Cell preset)
		{
			Cells = cells.Select(c => new Cell(c.Text) { Color = preset.Color, PaddingRight = preset.PaddingRight}).ToList();
		}
	}
}
