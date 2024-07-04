namespace SharpTables.Annotations
{

    /// <summary>
    /// Indicates that the property should be ignored when generating a table.
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class TableIgnoreAttribute : Attribute
    {
        public TableIgnoreAttribute()
        {
        }
    }
}
