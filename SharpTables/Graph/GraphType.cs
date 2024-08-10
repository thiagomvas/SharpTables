namespace SharpTables.Graph
{
    /// <summary>
    /// Represents the type of graph.
    /// </summary>
    public enum GraphType
    {
        /// <summary>
        /// Bar graph type.
        /// </summary>
        Bar,

        /// <summary>
        /// Line graph type.
        /// </summary>
        Line,

        /// <summary>
        /// Scatter graph type.
        /// </summary>
        Scatter,

        /// <summary>
        /// Pie graph type.
        /// </summary>
        /// <remarks>
        /// Use this with a <see cref="PieGraphFormatting"/> object for formatting.
        /// </remarks>
        Pie
    }
}
