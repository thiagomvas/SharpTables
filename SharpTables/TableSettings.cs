namespace SharpTables
{
    public class TableSettings
    {
        public string NullOrEmptyReplacement { get; set; } = "Null";
        public Action<Cell> CellPreset { get; set; } = _ => { };
        public Formatting TableFormatting { get; set; } = Formatting.Default;
        public bool UseHeader { get; set; } = true;
        public bool DisplayRowCount { get; set; } = false;
        public bool DisplayRowIndexes { get; set; } = false;
        public ConsoleColor RowIndexColor { get; set; } = ConsoleColor.DarkGray;

    }
}
