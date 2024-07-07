namespace SharpTables
{
    /// <summary>
    /// Represents the settings for a table.
    /// </summary>
    public class TableSettings
    {
        /// <summary>
        /// The string to replace empty or null cells.
        /// </summary>
        public string NullOrEmptyReplacement { get; set; } = "Null";
        /// <summary>
        /// The delegate to be called for each cell before it is printed. Used for customizing a cell dynamically.
        /// </summary>
        public Action<Cell> CellPreset { get; set; } = _ => { };
        /// <summary>
        /// The formatting options to be used when printing the table structure.
        /// </summary>
        public TableFormatting TableFormatting { get; set; } = TableFormatting.Default;
        /// <summary>
        /// Whether or not to display how many rows are in the table at the end of it.
        /// </summary>
        public bool DisplayRowCount { get; set; } = false;
        /// <summary>
        /// Whether or not to display the row indexes.
        /// </summary>
        public bool DisplayRowIndexes { get; set; } = false;

        /// <summary>
        /// Represents the color for the row index cells. Will only take effect if <see cref="DisplayRowIndexes"/> is <see langword="true"/>
        /// </summary>
        public ConsoleColor RowIndexColor { get; set; } = ConsoleColor.DarkGray;

    }
}
