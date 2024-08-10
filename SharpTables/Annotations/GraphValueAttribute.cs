namespace SharpTables.Annotations
{

    /// <summary>
    /// Defines a property to be used as the value for graph generation.
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,  Inherited = false, AllowMultiple = false)]
    public sealed class GraphValueAttribute : Attribute
    {

    }
}
