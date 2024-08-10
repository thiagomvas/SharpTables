namespace SharpTables.Annotations
{
    /// <summary>
    /// Defines a property to be used as a label for graph generation.
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class GraphKeyAttribute : Attribute
    {
    }
}
