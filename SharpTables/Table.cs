﻿using SharpTables.Annotations;
using System.Numerics;
using System.Reflection;
using System.Text;

namespace SharpTables
{
	public class Table
	{
		private Action<Cell> presetter;
		/// <summary>
		/// Gets the header of the table. To modify it, use <see cref="SetHeader(Row)"/>
		/// </summary>
		/// <remarks>By default, the first row added will become the header unless <see cref="SetHeader(Row)"/> is called</remarks>
		public Row Header { get; private set; }
		/// <summary>
		/// Gets or sets the rows of the table
		/// </summary>
		private List<Row> rows { get; set; }

		/// <summary>
		/// Gets or sets the string used to replace null or empty string values
		/// </summary>
		public string EmptyReplacement = "";

		/// <summary>
		/// Gets or sets the table formatting used. This does not include text formatting.
		/// </summary>
		public Formatting Formatting { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Table"/> class.
		/// </summary>
		public Table()
		{
			rows = new List<Row>();
			Formatting = new Formatting();
		}

        /// <summary>
        /// Adds a row to the table.
        /// </summary>
        /// <param name="row">The row to add.</param>
        /// <returns>The table with changes applied</returns>
        public Table AddRow(Row row)
		{
			if(Header == null)
			{
				Header = row;
			}
			row.LineIndex = rows.Count;
			for (int i = 0; i < row.Cells.Count; i++)
			{
				row.Cells[i].Position = new Vector2(i, row.LineIndex);
			}
			rows.Add(row);
			return this;
		}

        /// <summary>
        /// Sets the header of the table. 
        /// </summary>
        /// <param name="row">The row to add</param>
        /// <returns>The table with changes applied</returns>
        public Table SetHeader(Row row)
		{
			Header = row;
			foreach (var cell in row.Cells)
				cell.Padding = 0;
			return this;
		}

        /// <summary>
        /// Applies a preset action to every cell in the table. 
        /// </summary>
        /// <param name="presetter"></param>
        /// <returns>The table with changes applied</returns>
        public Table UsePreset(Action<Cell> presetter)
		{
			this.presetter = presetter;
			return this;
		}

		/// <summary>
		/// Applies formatting settings to the table.
		/// </summary>
		/// <param name="formatting">The formatting settings</param>
		/// <returns>The table with changes applied</returns>
		public Table UseFormatting(Formatting formatting)
		{
            Formatting = formatting;
            return this;
        }
		/// <summary>
		/// Prints the table to the console.
		/// </summary>
		public void Print()
		{
			foreach(var row in rows)
			{
				foreach(var cell in row.Cells)
				{
					presetter?.Invoke(cell);
				}
			}
			// Setup
			var temp = rows.ToList();
			temp.Add(Header);
			int[] widestCellPerColumn = new int[temp.Max(r => r.Cells.Count)];
			int columnCount = temp[0].Cells.Count;
			foreach (Row row in temp)
			{
				for (int i = 0; i < row.Cells.Count; i++)
				{
					var cell = row.Cells[i];
					int cellWidth = Utils.MeasureStringWidth(cell.Text) + cell.Padding;
					if (cellWidth > widestCellPerColumn[i])
					{
						widestCellPerColumn[i] = cellWidth;
					}
				}
			}

			// Print the header dividers
			if (Formatting.Header.HasTopDivider)
				PrintHorizontalDivider(widestCellPerColumn, Formatting.Header.TopLeftDivider, Formatting.Header.TopMiddleDivider, Formatting.Header.TopRightDivider, Formatting.Header.HorizontalDivider, Formatting.Header.DividerColor);

			// Print the header row
			Row headerRow = Header;
			foreach (var cell in headerRow.Cells)
			{
				Console.ForegroundColor = Formatting.Header.DividerColor;
				Console.Write(Formatting.Header.VerticalDivider);
				Console.ForegroundColor = cell.Color;
				Console.Write(GetCellString(cell, widestCellPerColumn[headerRow.Cells.IndexOf(cell)]));
				Console.ResetColor();
			}
			Console.ForegroundColor = Formatting.Header.DividerColor;
			Console.WriteLine(Formatting.Header.VerticalDivider);
			Console.ResetColor();

			if (Formatting.Header.Separated)
			{
				PrintHorizontalDivider(widestCellPerColumn, Formatting.Header.BottomLeftDivider, Formatting.Header.BottomMiddleDivider, Formatting.Header.BottomRightDivider, Formatting.Header.HorizontalDivider, Formatting.Header.DividerColor);
				PrintHorizontalDivider(widestCellPerColumn, Formatting.TopLeftDivider, Formatting.TopMiddleDivider, Formatting.TopRightDivider, Formatting.HorizontalDivider, Formatting.DividerColor);
			}
			else
			{
				PrintHorizontalDivider(widestCellPerColumn, Formatting.Header.LeftMiddleDivider, Formatting.Header.MiddleDivider, Formatting.Header.RightMiddleDivider, Formatting.Header.HorizontalDivider, Formatting.Header.DividerColor);
			}

			foreach (Row row in rows)
			{
				// Print the row with cell values
				for (int i = 0; i < columnCount; i++)
				{
					// If the row has fewer cells than the header, add empty cells
					if (i >= row.Cells.Count)
					{
						row.Cells.Add(new Cell(""));
					}

					Cell cell = row.Cells[i];
					if (string.IsNullOrWhiteSpace(cell.Text))
					{
						cell.Text = EmptyReplacement;
					}
					string cellText = GetCellString(cell, widestCellPerColumn[i]);
					Console.ForegroundColor = Formatting.DividerColor;
					Console.Write(Formatting.VerticalDivider);
					Console.ForegroundColor = cell.Color;
					Console.Write(cellText);
					Console.ResetColor();
				}
				Console.ForegroundColor = Formatting.DividerColor;
				Console.WriteLine(Formatting.VerticalDivider);
				Console.ResetColor();

				// Print the middle divider
				if (row != rows.Last())
				{
					PrintHorizontalDivider(widestCellPerColumn, Formatting.LeftMiddleDivider, Formatting.MiddleDivider, Formatting.RightMiddleDivider, Formatting.HorizontalDivider, Formatting.DividerColor);
				}
			}

			// Print the bottom divider
			PrintHorizontalDivider(widestCellPerColumn, Formatting.BottomLeftDivider, Formatting.BottomMiddleDivider, Formatting.BottomRightDivider, Formatting.HorizontalDivider, Formatting.DividerColor);
		}


		/// <summary>
		/// Converts the table to its string representation.
		/// </summary>
		/// <returns>The string representation of the table.</returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			var temp = rows.ToList();
			temp.Add(Header);
			// Setup
			int[] widestCellPerColumn = new int[temp.Max(r => r.Cells.Count)];
			int columnCount = temp[0].Cells.Count;
			foreach (Row row in temp)
			{
				for (int i = 0; i < row.Cells.Count; i++)
				{
					var cell = row.Cells[i];
					int cellWidth = Utils.MeasureStringWidth(cell.Text) + cell.Padding;
					if (cellWidth > widestCellPerColumn[i])
					{
						widestCellPerColumn[i] = cellWidth;
					}
				}
			}

			// Print the header dividers
			if (Formatting.Header.HasTopDivider)
				sb.Append(HorizontalDivider(widestCellPerColumn, Formatting.Header.TopLeftDivider, Formatting.Header.TopMiddleDivider, Formatting.Header.TopRightDivider, Formatting.Header.HorizontalDivider, Formatting.Header.DividerColor));

			// Print the header row
			Row headerRow = Header;
			foreach (var cell in headerRow.Cells)
			{
				sb.Append($"{Formatting.Header.VerticalDivider}");
				sb.Append($"{Utils.ResizeStringToWidth(cell.Text, widestCellPerColumn[headerRow.Cells.IndexOf(cell)])}");
			}
			sb.AppendLine($"{Formatting.Header.VerticalDivider}");

			if (Formatting.Header.Separated)
			{
				sb.Append(HorizontalDivider(widestCellPerColumn, Formatting.Header.BottomLeftDivider, Formatting.Header.BottomMiddleDivider, Formatting.Header.BottomRightDivider, Formatting.Header.HorizontalDivider, Formatting.Header.DividerColor));
				sb.Append(HorizontalDivider(widestCellPerColumn, Formatting.TopLeftDivider, Formatting.TopMiddleDivider, Formatting.TopRightDivider, Formatting.HorizontalDivider, Formatting.DividerColor));
			}
			else
			{
				sb.Append(HorizontalDivider(widestCellPerColumn, Formatting.Header.LeftMiddleDivider, Formatting.Header.MiddleDivider, Formatting.Header.RightMiddleDivider, Formatting.Header.HorizontalDivider, Formatting.Header.DividerColor));
			}

			foreach (Row row in rows)
			{
				// Print the row with cell values
				for (int i = 0; i < columnCount; i++)
				{
					// If the row has fewer cells than the header, add empty cells
					if (i >= row.Cells.Count)
					{
						row.Cells.Add(new Cell(""));
					}

					Cell cell = row.Cells[i];
					if (string.IsNullOrWhiteSpace(cell.Text))
					{
						cell.Text = EmptyReplacement;
					}
					string cellText = GetCellString(cell, widestCellPerColumn[i]);
					sb.Append($"{Formatting.VerticalDivider}");
					sb.Append($"{cellText}");
				}
				sb.AppendLine($"{Formatting.VerticalDivider}");

				// Print the middle divider
				if (row != rows.Last())
				{
					sb.Append(HorizontalDivider(widestCellPerColumn, Formatting.LeftMiddleDivider, Formatting.MiddleDivider, Formatting.RightMiddleDivider, Formatting.HorizontalDivider, Formatting.DividerColor));
				}
			}

			// Print the bottom divider
			sb.Append(HorizontalDivider(widestCellPerColumn, Formatting.BottomLeftDivider, Formatting.BottomMiddleDivider, Formatting.BottomRightDivider, Formatting.HorizontalDivider, Formatting.DividerColor));

			return sb.ToString();
		}

		/// <summary>
		/// Creates a table from a two-dimensional array of data.
		/// </summary>
		/// <param name="data">The two-dimensional array of data.</param>
		/// <returns>The created table.</returns>
		public static Table FromDataSet(object[,] data)
		{
			Table table = new Table();
			TableHelper.AddDataSet(table, data);
			return table;
		}

		/// <summary>
		/// Creates a table from a collection of data.
		/// </summary>
		/// <param name="data">The collection of data.</param>
		/// <returns>The created table.</returns>
		public static Table FromDataSet(IEnumerable<IEnumerable<object>> data)
		{
			Table table = new Table();
			TableHelper.AddDataSet(table, data);
			return table;
		}

		/// <summary>
		/// Creates a table from a collection of data with a function to produce rows based on each item.
		/// </summary>
		/// <typeparam name="T">The type of item being added.</typeparam>
		/// <param name="data">The dataset</param>
		/// <param name="generatorFunc">The function used to generate rows</param>
		/// <returns>A table generated from every element in the dataset</returns>
		public static Table FromDataSet<T>(IEnumerable<T> data, Func<T, Row> generatorFunc)
		{
			Table result = new();
			TableHelper.AddDataSet(result, data, generatorFunc);
			return result;
		}

		public static Table FromDataSet<T>(IEnumerable<T> data)
		{
			PropertyInfo[] properties = typeof(T).GetProperties().Where(p => p.GetCustomAttribute<TableIgnoreAttribute>() is null).ToArray();
			Table table = new Table();

			properties = properties.OrderBy(p => TableHelper.GetOrder(p)).ToArray();

			string[] headerTitles = new string[properties.Length];
			// Check for DisplayName annotation
			for(int i = 0; i < properties.Length; i++)
			{
                TableDisplayNameAttribute? attribute = properties[i].GetCustomAttribute<TableDisplayNameAttribute>();
				headerTitles[i] = attribute?.Name ?? properties[i].Name;
            }

			table.SetHeader(new Row(headerTitles));

			TableHelper.AddTDataset(table, data);

			return table;
		}

		/// <summary>
		/// Adds a two-dimensional array of data to the table.
		/// </summary>
		/// <param name="data">The data to be added</param>
		public Table AddDataSet(object[,] data)
		{
			TableHelper.AddDataSet(this, data);
            return this;
        }
		/// <summary>
		/// Adds a collection of data to the table.
		/// </summary>
		/// <param name="data">The data to be added</param>
		public Table AddDataSet(IEnumerable<IEnumerable<object>> data)
		{
			TableHelper.AddDataSet(this, data);
            return this;
        }
		/// <summary>
		/// Adds a collection of data to the table with a function to produce rows based on each item.
		/// </summary>
		/// <typeparam name="T">The type of item being added.</typeparam>
		/// <param name="data">The dataset</param>
		/// <param name="generatorFunc">The function used to generate rows</param>
		public Table AddDataSet<T>(IEnumerable<T> data, Func<T, Row> generatorFunc)
		{
			TableHelper.AddDataSet(this, data, generatorFunc);
            return this;
        }

		public Table AddDataSet<T>(IEnumerable<T> data)
		{
			TableHelper.AddTDataset(this, data);
			return this;
        }

		/// <summary>
		/// Converts the table to a markdown string.
		/// </summary>
		/// <returns>The markdown representation of this table</returns>
		/// <remarks>Styling and custom table formatting not included. If the custom formatting is needed, use <see cref="ToString()"/></remarks>
		public string ToMarkdown()
		{
			StringBuilder sb = new();
			// Header
			sb.Append("|");
			foreach(var cell in Header.Cells)
				sb.Append($"{cell.Text}|");
			sb.AppendLine();
			sb.Append("| ");
			foreach (var cell in Header.Cells)
				sb.Append($"- | ");
			sb.AppendLine();

			// Rows
			foreach (var row in rows)
			{
				sb.Append("| ");
				foreach (var cell in row.Cells)
					sb.Append($"{cell.Text} | ");
				sb.AppendLine();
			}

			return sb.ToString();
		}

		/// <summary>
		/// Converts the table to a HTML table.
		/// </summary>
		/// <param name="singleLine">Whether to remove newlines from the output</param>
		/// <returns>The HTML representation of this table</returns>
		/// <remarks>Styling and custom table formatting not included. If the custom formatting is needed, use <see cref="ToString()"/></remarks>
		public string ToHtml(bool singleLine = false)
		{
			StringBuilder sb = new();
			sb.AppendLine("<table>");
			sb.AppendLine("<thead>");
			sb.AppendLine("<tr>");
			foreach (var cell in Header.Cells)
				sb.AppendLine($"<th>{cell.Text}</th>");
			sb.AppendLine("</tr>");
			sb.AppendLine("</thead>");
			sb.AppendLine("<tbody>");
			foreach (var row in rows)
			{
				sb.AppendLine("<tr>");
				foreach (var cell in row.Cells)
					sb.AppendLine($"<td>{cell.Text}</td>");
				sb.AppendLine("</tr>");
			}
			sb.AppendLine("</tbody>");
			sb.AppendLine("</table>");

			if (singleLine)
				sb.Replace("\n", "").Replace("\r", "");

			return sb.ToString();
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

		private string HorizontalDivider(int[] widths, char left, char middle, char right, char horizontal, ConsoleColor color)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(left);
			foreach (int width in widths)
			{
				for (int i = 0; i < width; i++)
				{
					sb.Append(horizontal);
				}
				sb.Append(middle);
			}
			sb.Remove(sb.Length - 1, 1); // Remove the last middle
			sb.Append(right);
			sb.AppendLine();
			return sb.ToString();
		}
		private string GetCellString(Cell cell, int width)
		{
			string cellText = Utils.ResizeStringToWidth(cell.Text, width, cell.Alignment);
			return cellText;
		}
	}
}

