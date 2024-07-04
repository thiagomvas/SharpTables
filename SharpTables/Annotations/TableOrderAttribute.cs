namespace SharpTables.Annotations
{
    /// <summary>
    /// Indicates the order of the property in the table in ascending order.
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class TableOrderAttribute : Attribute
    {
        readonly int _order;
        public TableOrderAttribute(int order)
        {
            _order = order;
        }

        public int Order
        {
            get { return _order; }
        }
    }
}
