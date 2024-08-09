using SharpTables.Annotations;
using SharpTables.Pagination;
using System.Numerics;
using System.Reflection;
using System.Text;

namespace SharpTables
{
    public class Table : IConsoleWriteable
    {
        /// <summary>
        /// Gets the header of the table. To modify it, use <see cref="SetHeader(Row)"/>
        /// </summary>
        /// <remarks>By default, the first row added will become the header unless <see cref="SetHeader(Row)"/> is called</remarks>
        public Row Header { get; private set; }
        /// <summary>
        /// Gets or sets the rows of the table
        /// </summary>
        public List<Row> Rows { get; set; }

        /// <summary>
        /// Gets or sets the settings of the table.
        /// </summary>
        public TableSettings Settings { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        public Table()
        {
            Rows = new List<Row>();
            Settings = new();
        }

        /// <summary>
        /// Adds a row to the table.
        /// </summary>
        /// <param name="row">The row to add.</param>
        /// <returns>The table with changes applied</returns>
        public Table AddRow(Row row)
        {
            if (Header == null)
            {
                Header = row;
            }
            row.LineIndex = Rows.Count;
            for (int i = 0; i < row.Cells.Count; i++)
            {
                row.Cells[i].Position = new Vector2(i, row.LineIndex);
            }
            Rows.Add(row);
            return this;
        }

        /// <summary>
        /// Adds a row to the table.
        /// </summary>
        /// <param name="cells">The text in each cell for this row</param>
        /// <returns>The table with changes applied</returns>
        public Table AddRow(params string[] cells)
        {
            return AddRow(new Row(cells));
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
        /// Sets the header of the table. 
        /// </summary>
        /// <param name="cells">The title for each row</param>
        /// <returns>The table with changes applied</returns>
        public Table SetHeader(params string[] cells)
        {
            var row = new Row(cells);
            return SetHeader(row);
        }

        /// <summary>
        /// Applies a preset action to every cell in the table. 
        /// </summary>
        /// <param name="presetter"></param>
        /// <returns>The table with changes applied</returns>
        public Table UsePreset(Action<Cell> presetter)
        {
            this.Settings.CellPreset = presetter;
            return this;
        }

        /// <summary>
        /// Replaces the current table settings with the given one.
        /// </summary>
        /// <param name="settings">The settings to apply</param>
        /// <returns>The table with changes applied</returns>
        public Table UseSettings(TableSettings settings)
        {
            this.Settings = settings;
            return this;
        }

        /// <summary>
        /// Displays the row indexes in the table.
        /// </summary>
        /// <returns>The table with changes applied</returns>
        public Table DisplayRowIndexes()
        {
            Settings.DisplayRowIndexes = true;
            return this;
        }

        /// <summary>
        /// Displays the row count at the end of the table.
        /// </summary>
        /// <returns>The table with changes applied</returns>
        public Table DisplayRowCount()
        {
            Settings.DisplayRowCount = true;
            return this;
        }

        /// <summary>
        /// Applies formatting settings to the table.
        /// </summary>
        /// <param name="formatting">The formatting settings</param>
        /// <returns>The table with changes applied</returns>
        public Table UseFormatting(TableFormatting formatting)
        {
            Settings.TableFormatting = formatting;
            return this;
        }

        /// <summary>
        /// Applies a color to the row indexes.
        /// </summary>
        /// <param name="color">The row index color</param>
        /// <returns>The table with changes applied</returns>
        public Table UseRowIndexColor(ConsoleColor color)
        {
            Settings.RowIndexColor = color;
            return this;
        }

        /// <summary>
        /// Defines a replacement string for null or empty string values in the cells.
        /// </summary>
        /// <param name="replacement">The replacement string</param>
        /// <returns>The table with changes applied</returns>
        public Table UseNullOrEmptyReplacement(string replacement)
        {
            Settings.NullOrEmptyReplacement = replacement;
            return this;
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

        /// <summary>
        /// Creates a table from a collection using the properties of the type.
        /// </summary>
        /// <typeparam name="T">The data type</typeparam>
        /// <param name="data">The data set</param>
        /// <returns>A table containing all the property values of the dataset</returns>
        /// <remarks>
        /// Only public instance properties without a <see cref="TableIgnoreAttribute"/> will be added to the table.
        /// </remarks>
        public static Table FromDataSet<T>(IEnumerable<T> data)
        {
            PropertyInfo[] properties = TableHelper.GetProperties(typeof(T));
            Table table = new Table();

            string[] headerTitles = new string[properties.Length];
            // Check for DisplayName annotation
            for (int i = 0; i < properties.Length; i++)
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

        /// <summary>
        /// Adds a collection of data to the table using the properties of the type.
        /// </summary>
        /// <typeparam name="T">The data type</typeparam>
        /// <param name="data">The data set</param>
        /// <returns>A table containing all the property values of the dataset</returns>
        /// <remarks>
        /// Only public instance properties without a <see cref="TableIgnoreAttribute"/> will be added to the table.
        /// </remarks>
        public Table AddDataSet<T>(IEnumerable<T> data)
        {
            TableHelper.AddTDataset(this, data);
            return this;
        }

        /// <summary>
        /// Converts the table to a paginated table with the given number of rows per page.
        /// </summary>
        /// <param name="rowsPerPage">The number of rows per page</param>
        /// <returns>
        /// A paginated table with the given number of rows per page.
        /// </returns>
        public PaginatedTable ToPaginatedTable(int rowsPerPage)
        {
            List<Table> pages = new();
            for (int i = 0; i < Rows.Count; i += rowsPerPage)
            {
                Table page = new();
                page.Settings = this.Settings;
                page.Header = Header;
                page.Rows = Rows.Skip(i).Take(rowsPerPage).ToList();
                pages.Add(page);
            }
            return new PaginatedTable(pages);
        }

        /// <summary>
        /// Prints the table to the console.
        /// </summary>
        public void Write()
        {
            var rows = Rows.Select(r => r.Clone()).ToList();
            foreach (var row in rows)
            {
                if (Settings.DisplayRowIndexes)
                {
                    row.Cells.Insert(0, new Cell(row.LineIndex + 1) { Padding = 0, Alignment = Alignment.Right, Color = Settings.RowIndexColor });
                }
                foreach (var cell in row.Cells)
                {
                    if (cell.IsNull)
                        cell.Text = Settings.NullOrEmptyReplacement;

                    Settings.CellPreset?.Invoke(cell);
                }
            }
            StringBuilder sb = new StringBuilder();
            var temp = rows.ToList();
            var tempHeader = Header.Clone();
            if (Settings.DisplayRowIndexes)
            {
                tempHeader.Cells.Insert(0, new Cell(" ") { Padding = 0, Alignment = Alignment.Right });
            }
            temp.Add(tempHeader);
            // Setup
            int[] widestCellPerColumn = new int[tempHeader.Cells.Count];
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
            int columnCount = tempHeader.Cells.Count;

            // Print the header dividers
            if (Settings.TableFormatting.Header.HasTopDivider)
                PrintHorizontalDivider(widestCellPerColumn, Settings.TableFormatting.Header.TopLeftDivider, Settings.TableFormatting.Header.TopMiddleDivider, Settings.TableFormatting.Header.TopRightDivider, Settings.TableFormatting.Header.HorizontalDivider, Settings.TableFormatting.Header.DividerColor);

            // Print the header row
            Row headerRow = tempHeader;
            foreach (var cell in headerRow.Cells)
            {
                Console.ForegroundColor = Settings.TableFormatting.Header.DividerColor;
                Console.Write(Settings.TableFormatting.Header.VerticalDivider);
                Console.ForegroundColor = cell.Color;
                Console.Write(GetCellString(cell, widestCellPerColumn[headerRow.Cells.IndexOf(cell)]));
                Console.ResetColor();
            }
            Console.ForegroundColor = Settings.TableFormatting.Header.DividerColor;
            Console.WriteLine(Settings.TableFormatting.Header.VerticalDivider);
            Console.ResetColor();

            if (Settings.TableFormatting.Header.Separated)
            {
                PrintHorizontalDivider(widestCellPerColumn, Settings.TableFormatting.Header.BottomLeftDivider, Settings.TableFormatting.Header.BottomMiddleDivider, Settings.TableFormatting.Header.BottomRightDivider, Settings.TableFormatting.Header.HorizontalDivider, Settings.TableFormatting.Header.DividerColor);
                PrintHorizontalDivider(widestCellPerColumn, Settings.TableFormatting.TopLeftDivider, Settings.TableFormatting.TopMiddleDivider, Settings.TableFormatting.TopRightDivider, Settings.TableFormatting.HorizontalDivider, Settings.TableFormatting.DividerColor);
            }
            else
            {
                PrintHorizontalDivider(widestCellPerColumn, Settings.TableFormatting.Header.LeftMiddleDivider, Settings.TableFormatting.Header.MiddleDivider, Settings.TableFormatting.Header.RightMiddleDivider, Settings.TableFormatting.Header.HorizontalDivider, Settings.TableFormatting.Header.DividerColor);
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
                    if (string.IsNullOrWhiteSpace(cell.Text) || cell.IsNull)
                    {
                        cell.Text = Settings.NullOrEmptyReplacement;
                    }
                    string cellText = GetCellString(cell, widestCellPerColumn[i]);
                    Console.ForegroundColor = Settings.TableFormatting.DividerColor;
                    Console.Write(Settings.TableFormatting.VerticalDivider);
                    Console.ForegroundColor = cell.Color;
                    Console.Write(cellText);
                    Console.ResetColor();
                }
                Console.ForegroundColor = Settings.TableFormatting.DividerColor;
                Console.WriteLine(Settings.TableFormatting.VerticalDivider);
                Console.ResetColor();

                // Print the middle divider
                if (row != rows.Last())
                {
                    PrintHorizontalDivider(widestCellPerColumn, Settings.TableFormatting.LeftMiddleDivider, Settings.TableFormatting.MiddleDivider, Settings.TableFormatting.RightMiddleDivider, Settings.TableFormatting.HorizontalDivider, Settings.TableFormatting.DividerColor);
                }
            }

            // Print the bottom divider
            PrintHorizontalDivider(widestCellPerColumn, Settings.TableFormatting.BottomLeftDivider, Settings.TableFormatting.BottomMiddleDivider, Settings.TableFormatting.BottomRightDivider, Settings.TableFormatting.HorizontalDivider, Settings.TableFormatting.DividerColor);
            if (Settings.DisplayRowCount)
                Console.WriteLine($"Row count: {rows.Count}");
        }

        public void Write(TextWriter writer)
        {
            var rows = Rows.Select(r => r.Clone()).ToList();
            foreach (var row in rows)
            {
                if (Settings.DisplayRowIndexes)
                {
                    row.Cells.Insert(0, new Cell(row.LineIndex + 1) { Padding = 0, Alignment = Alignment.Right, Color = Settings.RowIndexColor });
                }
                foreach (var cell in row.Cells)
                {
                    if (cell.IsNull)
                        cell.Text = Settings.NullOrEmptyReplacement;

                    Settings.CellPreset?.Invoke(cell);
                }
            }
            StringBuilder sb = new StringBuilder();
            var temp = rows.ToList();
            var tempHeader = Header.Clone();
            if (Settings.DisplayRowIndexes)
            {
                tempHeader.Cells.Insert(0, new Cell(" ") { Padding = 0, Alignment = Alignment.Right });
            }
            temp.Add(tempHeader);
            // Setup
            int[] widestCellPerColumn = new int[tempHeader.Cells.Count];
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
            int columnCount = tempHeader.Cells.Count;

            // Print the header dividers
            if (Settings.TableFormatting.Header.HasTopDivider)
                PrintHorizontalDivider(widestCellPerColumn, Settings.TableFormatting.Header.TopLeftDivider, Settings.TableFormatting.Header.TopMiddleDivider, Settings.TableFormatting.Header.TopRightDivider, Settings.TableFormatting.Header.HorizontalDivider, Settings.TableFormatting.Header.DividerColor);

            // Print the header row
            Row headerRow = tempHeader;
            foreach (var cell in headerRow.Cells)
            {
                Console.ForegroundColor = Settings.TableFormatting.Header.DividerColor;
                writer.Write(Settings.TableFormatting.Header.VerticalDivider);
                Console.ForegroundColor = cell.Color;
                writer.Write(GetCellString(cell, widestCellPerColumn[headerRow.Cells.IndexOf(cell)]));
                Console.ResetColor();
            }
            Console.ForegroundColor = Settings.TableFormatting.Header.DividerColor;
            writer.WriteLine(Settings.TableFormatting.Header.VerticalDivider);
            Console.ResetColor();

            if (Settings.TableFormatting.Header.Separated)
            {
                PrintHorizontalDivider(widestCellPerColumn, Settings.TableFormatting.Header.BottomLeftDivider, Settings.TableFormatting.Header.BottomMiddleDivider, Settings.TableFormatting.Header.BottomRightDivider, Settings.TableFormatting.Header.HorizontalDivider, Settings.TableFormatting.Header.DividerColor);
                PrintHorizontalDivider(widestCellPerColumn, Settings.TableFormatting.TopLeftDivider, Settings.TableFormatting.TopMiddleDivider, Settings.TableFormatting.TopRightDivider, Settings.TableFormatting.HorizontalDivider, Settings.TableFormatting.DividerColor);
            }
            else
            {
                PrintHorizontalDivider(widestCellPerColumn, Settings.TableFormatting.Header.LeftMiddleDivider, Settings.TableFormatting.Header.MiddleDivider, Settings.TableFormatting.Header.RightMiddleDivider, Settings.TableFormatting.Header.HorizontalDivider, Settings.TableFormatting.Header.DividerColor);
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
                    if (string.IsNullOrWhiteSpace(cell.Text) || cell.IsNull)
                    {
                        cell.Text = Settings.NullOrEmptyReplacement;
                    }
                    string cellText = GetCellString(cell, widestCellPerColumn[i]);
                    Console.ForegroundColor = Settings.TableFormatting.DividerColor;
                    writer.Write(Settings.TableFormatting.VerticalDivider);
                    Console.ForegroundColor = cell.Color;
                    writer.Write(cellText);
                    Console.ResetColor();
                }
                Console.ForegroundColor = Settings.TableFormatting.DividerColor;
                writer.WriteLine(Settings.TableFormatting.VerticalDivider);
                Console.ResetColor();

                // Print the middle divider
                if (row != rows.Last())
                {
                    PrintHorizontalDivider(widestCellPerColumn, Settings.TableFormatting.LeftMiddleDivider, Settings.TableFormatting.MiddleDivider, Settings.TableFormatting.RightMiddleDivider, Settings.TableFormatting.HorizontalDivider, Settings.TableFormatting.DividerColor);
                }
            }

            // Print the bottom divider
            PrintHorizontalDivider(widestCellPerColumn, Settings.TableFormatting.BottomLeftDivider, Settings.TableFormatting.BottomMiddleDivider, Settings.TableFormatting.BottomRightDivider, Settings.TableFormatting.HorizontalDivider, Settings.TableFormatting.DividerColor);
            if (Settings.DisplayRowCount)
                writer.WriteLine($"Row count: {rows.Count}");
        }

        /// <summary>
        /// Converts the table to its string representation.
        /// </summary>
        /// <returns>The string representation of the table.</returns>
        public override string ToString()
        {
            var rows = Rows.Select(r => r.Clone()).ToList();
            foreach (var row in rows)
            {
                if (Settings.DisplayRowIndexes)
                {
                    row.Cells.Insert(0, new Cell(row.LineIndex + 1) { Padding = 0, Alignment = Alignment.Right, Color = Settings.RowIndexColor });
                }
            }
            StringBuilder sb = new StringBuilder();
            var temp = rows.ToList();
            var tempHeader = Header.Clone();
            if (Settings.DisplayRowIndexes)
            {
                tempHeader.Cells.Insert(0, new Cell(" ") { Padding = 0, Alignment = Alignment.Right });
            }
            temp.Add(tempHeader);
            // Setup
            int[] widestCellPerColumn = new int[tempHeader.Cells.Count];
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
            int columnCount = temp[0].Cells.Count;

            // Print the header dividers
            if (Settings.TableFormatting.Header.HasTopDivider)
                sb.Append(HorizontalDivider(widestCellPerColumn, Settings.TableFormatting.Header.TopLeftDivider, Settings.TableFormatting.Header.TopMiddleDivider, Settings.TableFormatting.Header.TopRightDivider, Settings.TableFormatting.Header.HorizontalDivider, Settings.TableFormatting.Header.DividerColor));

            // Print the header row
            Row headerRow = tempHeader;
            for (int i = 0; i < columnCount; i++)
            {
                Cell? cell = headerRow.Cells[i];
                sb.Append($"{Settings.TableFormatting.Header.VerticalDivider}");
                sb.Append($"{Utils.ResizeStringToWidth(cell.Text, widestCellPerColumn[i])}");
            }
            sb.AppendLine($"{Settings.TableFormatting.Header.VerticalDivider}");

            if (Settings.TableFormatting.Header.Separated)
            {
                sb.Append(HorizontalDivider(widestCellPerColumn, Settings.TableFormatting.Header.BottomLeftDivider, Settings.TableFormatting.Header.BottomMiddleDivider, Settings.TableFormatting.Header.BottomRightDivider, Settings.TableFormatting.Header.HorizontalDivider, Settings.TableFormatting.Header.DividerColor));
                sb.Append(HorizontalDivider(widestCellPerColumn, Settings.TableFormatting.TopLeftDivider, Settings.TableFormatting.TopMiddleDivider, Settings.TableFormatting.TopRightDivider, Settings.TableFormatting.HorizontalDivider, Settings.TableFormatting.DividerColor));
            }
            else
            {
                sb.Append(HorizontalDivider(widestCellPerColumn, Settings.TableFormatting.Header.LeftMiddleDivider, Settings.TableFormatting.Header.MiddleDivider, Settings.TableFormatting.Header.RightMiddleDivider, Settings.TableFormatting.Header.HorizontalDivider, Settings.TableFormatting.Header.DividerColor));
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
                        cell.Text = Settings.NullOrEmptyReplacement;
                    }
                    string cellText = GetCellString(cell, widestCellPerColumn[i]);
                    sb.Append($"{Settings.TableFormatting.VerticalDivider}");
                    sb.Append($"{cellText}");
                }
                sb.AppendLine($"{Settings.TableFormatting.VerticalDivider}");

                // Print the middle divider
                if (row != rows.Last())
                {
                    sb.Append(HorizontalDivider(widestCellPerColumn, Settings.TableFormatting.LeftMiddleDivider, Settings.TableFormatting.MiddleDivider, Settings.TableFormatting.RightMiddleDivider, Settings.TableFormatting.HorizontalDivider, Settings.TableFormatting.DividerColor));
                }
            }

            // Print the bottom divider
            sb.Append(HorizontalDivider(widestCellPerColumn, Settings.TableFormatting.BottomLeftDivider, Settings.TableFormatting.BottomMiddleDivider, Settings.TableFormatting.BottomRightDivider, Settings.TableFormatting.HorizontalDivider, Settings.TableFormatting.DividerColor));

            return sb.ToString();
        }
        /// <summary>
        /// Converts the table to a markdown string.
        /// </summary>
        /// <returns>The markdown representation of this table</returns>
        /// <remarks>Styling and custom table formatting not included. If the custom formatting is needed, use <see cref="ToString()"/></remarks>
        public string ToMarkdown()
        {
            var temp = Rows.Select(r => r.Clone()).ToList();
            var tempHeader = new Row();
            tempHeader.Cells = Header.Cells.ToList();
            if (Settings.DisplayRowIndexes)
            {
                tempHeader.Cells.Insert(0, new Cell(" ") { Padding = 0, Alignment = Alignment.Right });
            }

            foreach (var row in temp)
            {
                foreach (var cell in row.Cells)
                {
                    if (cell.IsNull)
                        cell.Text = Settings.NullOrEmptyReplacement;
                }

                if (Settings.DisplayRowIndexes)
                {
                    row.Cells.Insert(0, new Cell(row.LineIndex + 1) { Padding = 0, Alignment = Alignment.Right, Color = Settings.RowIndexColor });
                }
            }


            StringBuilder sb = new();
            // Header
            sb.Append("|");
            foreach (var cell in tempHeader.Cells)
                sb.Append($"{cell.Text}|");
            sb.AppendLine();
            sb.Append("| ");
            foreach (var cell in tempHeader.Cells)
                sb.Append($"- | ");
            sb.AppendLine();

            // Rows
            foreach (var row in temp)
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
            var temp = Rows.Select(r => r.Clone()).ToList();
            var tempHeader = new Row();
            tempHeader.Cells.AddRange(Header.Cells);
            if (Settings.DisplayRowIndexes)
            {
                tempHeader.Cells.Insert(0, new Cell(" ") { Padding = 0, Alignment = Alignment.Right });
            }

            foreach (var row in temp)
            {
                foreach (var cell in row.Cells)
                {
                    if (cell.IsNull)
                        cell.Text = Settings.NullOrEmptyReplacement;
                }

                if (Settings.DisplayRowIndexes)
                {
                    row.Cells.Insert(0, new Cell(row.LineIndex + 1) { Padding = 0, Alignment = Alignment.Right, Color = Settings.RowIndexColor });
                }
            }
            StringBuilder sb = new();
            sb.AppendLine("<table>");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            foreach (var cell in tempHeader.Cells)
                sb.AppendLine($"<th>{cell.Text}</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");
            foreach (var row in temp)
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

