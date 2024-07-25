namespace SharpTables.Graph
{
    /// <summary>
    /// Represents the formatting for a pie graph.
    /// </summary>
    public record PieGraphFormatting : GraphFormatting
    {
        /// <summary>
        /// Gets or sets the colors for the pie graph.
        /// </summary>
        public ConsoleColor[] Colors = [
            ConsoleColor.Red,ConsoleColor.Green,ConsoleColor.Blue,ConsoleColor.Yellow,ConsoleColor.Cyan,ConsoleColor.Magenta,ConsoleColor.DarkRed,ConsoleColor.DarkGreen,ConsoleColor.DarkBlue,ConsoleColor.DarkYellow
            ];

        /// <summary>
        /// Gets or sets the radius of the pie graph.
        /// </summary>
        /// <remarks>
        /// Consider this the "height" of the graph. To make it rounder, the width will be scaled up.
        /// </remarks>
        public int Radius { get; set; } = 10;

        /// <summary>
        /// Gets or sets the threshold for grouping slices together if theyre below a certain percentage. Must be between 0 and 1
        /// </summary>
        public double GroupThreshold { get; set; } = 0.05;

        /// <summary>
        /// Gets or sets whether to show the legend.
        /// </summary>
        public bool ShowLegend { get; set; } = true;

        /// <summary>
        /// Gets or sets the character to use for the center of the pie graph.
        /// </summary>
        public char CenterChar { get; set; } = ' ';
    }
}
