namespace SharpTables.Annotations
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class TableIgnoreAttribute : Attribute
    {
        public TableIgnoreAttribute()
        {
        }
    }
}
