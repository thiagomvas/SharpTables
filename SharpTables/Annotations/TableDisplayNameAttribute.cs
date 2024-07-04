namespace SharpTables.Annotations
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class TableDisplayNameAttribute : Attribute
    {
        readonly string _name;

        // This is a positional argument
        public TableDisplayNameAttribute(string displayName)
        {
            _name = displayName;

        }

        public string Name
        {
            get { return _name; }
        }
    }
}
