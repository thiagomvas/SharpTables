namespace SharpTables.Graph
{
    /// <summary>
    /// Represents the settings for a graph.
    /// </summary>
    /// <typeparam name="T">The type of data in the graph.</typeparam>
    public record GraphSettings<T>
    {
        /// <summary>
        /// Gets or sets the function to retrieve the value from the data.
        /// </summary>
        public Func<T, double> ValueGetter { get; set; } = x => (double)(object)x;

        /// <summary>
        /// Gets or sets the function to format the X-axis tick labels.
        /// </summary>
        public Func<T, string> XTickFormatter { get; set; } = x => x.ToString();

        /// <summary>
        /// Gets or sets the function to format the Y-axis tick labels.
        /// </summary>
        public Func<double, string> YTickFormatter { get; set; } = y => y.ToString();

        /// <summary>
        /// Gets or sets the padding for the Y-axis.
        /// </summary>
        public int YAxisPadding { get; set; } = 1;

        /// <summary>
        /// Gets or sets the padding for the X-axis.
        /// </summary>
        public int XAxisPadding { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of Y-axis ticks.
        /// </summary>
        public int NumOfYTicks { get; set; } = 5;
    }
}
