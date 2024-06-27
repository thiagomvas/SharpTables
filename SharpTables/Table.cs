namespace SharpTables
{
	public class Table
	{
		public List<Row> Rows { get; set; }
		public Alignment NumberAlignment { get; set; } = Alignment.Left;
		private readonly Formatting _formatting;
		public Table()
		{
			Rows = new List<Row>();
			_formatting = new Formatting();
		}
		public Table(Formatting formatting)
		{
			Rows = new List<Row>();
			_formatting = formatting;
		}


		public void AddRow(Row row)
		{
			Rows.Add(row);
		}
		public void SetColumnColor(int column, ConsoleColor color, bool changeHeaderColor = false)
		{
			foreach (Row row in Rows)
			{
				if(!changeHeaderColor && row == Rows[0])
				{
					continue;
				}
				if (column < row.Cells.Count)
				{
					row.Cells[column].Color = color;
				}
			}
		}
		public void SetColumnPadding(int column, int padding, bool changeHeaderPadding = false)
		{
			foreach (Row row in Rows)
			{
				if(!changeHeaderPadding && row == Rows[0])
				{
					continue;
				}
				if (column < row.Cells.Count)
				{
					row.Cells[column].PaddingRight = padding;
				}
			}
		}
		/// <summary>
		/// Prints the table to the console
		/// </summary>
		/// <param name="separateHeader">Whether the header should be printed separated from the rest of the table</param>
		public void Print(bool separateHeader = false)
		{
			// Setup
			int[] widestCellPerColumn = new int[Rows.Max(r => r.Cells.Count)];
			int columnCount = Rows[0].Cells.Count;
			foreach (Row row in Rows)
			{
				for (int i = 0; i < row.Cells.Count; i++)
				{
					var cell = row.Cells[i];
					int cellWidth = Utils.MeasureStringWidth(cell.Text) + cell.PaddingRight;
					if (cellWidth > widestCellPerColumn[i])
					{
						widestCellPerColumn[i] = cellWidth;
					}
				}
			}

			// Print the header dividers
			PrintHorizontalDivider(widestCellPerColumn, _formatting.Header.TopLeftDivider, _formatting.Header.TopMiddleDivider, _formatting.Header.TopRightDivider, _formatting.Header.HorizontalDivider, _formatting.Header.DividerColor);

			// Print the header row
			Row headerRow = Rows.First();
			foreach (var cell in headerRow.Cells)
			{
				Console.ForegroundColor = _formatting.Header.DividerColor;
				Console.Write(_formatting.Header.VerticalDivider);
				Console.ForegroundColor = cell.Color;
				Console.Write(Utils.ResizeStringToWidth(cell.Text, widestCellPerColumn[headerRow.Cells.IndexOf(cell)]));
				Console.ResetColor();
			}
			Console.ForegroundColor = _formatting.Header.DividerColor;
			Console.WriteLine(_formatting.Header.VerticalDivider);
			Console.ResetColor();

			if(separateHeader)
			{
				PrintHorizontalDivider(widestCellPerColumn, _formatting.Header.BottomLeftDivider, _formatting.Header.BottomMiddleDivider, _formatting.Header.BottomRightDivider, _formatting.Header.HorizontalDivider, _formatting.Header.DividerColor);	
				PrintHorizontalDivider(widestCellPerColumn, _formatting.TopLeftDivider, _formatting.TopMiddleDivider, _formatting.TopRightDivider, _formatting.HorizontalDivider, _formatting.DividerColor);
			}
			else
			{
				PrintHorizontalDivider(widestCellPerColumn, _formatting.Header.LeftMiddleDivider, _formatting.Header.MiddleDivider, _formatting.Header.RightMiddleDivider, _formatting.Header.HorizontalDivider, _formatting.Header.DividerColor);
			}

			foreach (Row row in Rows.Skip(1))
			{
				// Print the row with cell values
				for (int i = 0; i < columnCount; i++)
				{
					// If the row has fewer cells than the header, add empty cells
					if(i >= row.Cells.Count)
					{
						row.Cells.Add(new Cell(""));
					}
					
					Cell cell = row.Cells[i];
					string cellText = "";
					if(double.TryParse(cell.Text, out _))
					{
						cellText = Utils.ResizeStringToWidth(cell.Text, widestCellPerColumn[i], NumberAlignment);
					}
					else
					{
						cellText = Utils.ResizeStringToWidth(cell.Text, widestCellPerColumn[i]);
					}
					Console.ForegroundColor = _formatting.DividerColor;
					Console.Write(_formatting.VerticalDivider);
					Console.ForegroundColor = cell.Color;
					Console.Write(cellText);
					Console.ResetColor();
				}
				Console.ForegroundColor = _formatting.DividerColor;
				Console.WriteLine(_formatting.VerticalDivider);
				Console.ResetColor();

				// Print the middle divider
				if (row != Rows.Last())
				{
					PrintHorizontalDivider(widestCellPerColumn, _formatting.LeftMiddleDivider, _formatting.MiddleDivider, _formatting.RightMiddleDivider, _formatting.HorizontalDivider, _formatting.DividerColor);
				}
			}

			// Print the bottom divider
			PrintHorizontalDivider(widestCellPerColumn, _formatting.BottomLeftDivider, _formatting.BottomMiddleDivider, _formatting.BottomRightDivider, _formatting.HorizontalDivider, _formatting.DividerColor);
		}

		public static Table FromDataSet(object[,] data) => FromDataSet(data, new Formatting());
		public static Table FromDataSet(object[,] data, Formatting formatting)
		{
			Table table = new Table(formatting);
			for (int i = 0; i < data.GetLength(0); i++)
			{
				object[] row = new object[data.GetLength(1)];
				for (int j = 0; j < data.GetLength(1); j++)
				{
					row[j] = data[i, j];
				}
				table.AddRow(new Row(row));
			}
			return table;
		}

		public static Table FromDataSet(IEnumerable<IEnumerable<object>> data) => FromDataSet(data, new Formatting());
		public static Table FromDataSet(IEnumerable<IEnumerable<object>> data, Formatting formatting)
		{
			Table table = new Table(formatting);
			foreach (IEnumerable<object> row in data)
			{
				table.AddRow(new Row(row));
			}
			return table;
		}

		private void PrintHorizontalDivider(int[] columnWidths, char left, char middle, char right, char horizontal, ConsoleColor dividerColor)
		{
			Console.ForegroundColor = dividerColor;
			Console.Write(left);
			for (int i = 0; i < columnWidths.Length; i++)
			{
				Console.Write(new string(horizontal, columnWidths[i]));
				if (i < columnWidths.Length - 1)
				{
					Console.Write(middle);
				}
			}
			Console.WriteLine(right);
			Console.ResetColor();
		}

	}
}
