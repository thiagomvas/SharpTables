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
            Cells = values.Select(v => new Cell(v)).ToList();
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="Row"/> class with the specified cells.
        /// </summary>
        /// <param name="cells">The cells to add to the row.</param>
        public Row(IEnumerable<object> cells)
        {
            Cells = cells.Select(c => new Cell(c)).ToList();
        }

        internal Row Clone()
        {
            Row row = new Row();
            row.LineIndex = LineIndex;
            row.Cells = Cells.Select(c => c.Clone()).ToList();
            return row;
        }
    }
}
