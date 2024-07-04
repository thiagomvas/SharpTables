namespace SharpTables.Annotations
{
    /// <summary>
    /// Indicates that the column should be displayed with the specified alignment in the table.
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class TableAlignmentAttribute : Attribute
    {
        readonly Alignment _alignment;

        // This is a positional argument
        public TableAlignmentAttribute(Alignment alignment)
        {
            _alignment = alignment;

        }

        public Alignment Alignment
        {
            get { return _alignment; }
        }
    }
}
