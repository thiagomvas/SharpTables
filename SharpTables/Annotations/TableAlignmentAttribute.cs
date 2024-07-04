namespace SharpTables.Annotations
{
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
