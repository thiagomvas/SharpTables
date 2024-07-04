namespace SharpTables.Annotations
{
    /// <summary>
    /// Indicates that the column should be displayed with the specified color in the table.
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class TableColorAttribute : Attribute
    {
        readonly ConsoleColor _color;

        // This is a positional argument
        public TableColorAttribute(ConsoleColor color)
        {
            _color = color;

        }

        public ConsoleColor Color
        {
            get { return _color; }
        }
    }
}
