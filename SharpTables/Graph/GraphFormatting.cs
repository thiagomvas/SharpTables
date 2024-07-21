namespace SharpTables.Graph
{
    /// <summary>
    /// Represents the formatting options for a graph.
    /// </summary>
    public record GraphFormatting
    {
        /// <summary>
        /// Gets or sets the character used to represent an empty point on the graph.
        /// </summary>
        public char EmptyPoint { get; set; } = ' ';

        /// <summary>
        /// Gets or sets the character used to represent a horizontal line on the graph.
        /// </summary>
        public char HorizontalLine { get; set; } = '-';

        /// <summary>
        /// Gets or sets the character used to represent a vertical line on the graph.
        /// </summary>
        public char VerticalLine { get; set; } = '|';

        /// <summary>
        /// Gets or sets the character used to represent the origin point on the graph.
        /// </summary>
        public char Origin { get; set; } = 'O';

        /// <summary>
        /// Gets or sets the character used to represent the tick mark on the x-axis.
        /// </summary>
        public char XAxisTick { get; set; } = '+';

        /// <summary>
        /// Gets or sets the character used to represent the tick mark on the y-axis.
        /// </summary>
        public char YAxisTick { get; set; } = '+';

        /// <summary>
        /// Gets or sets the character used to represent the tick line on the y-axis.
        /// </summary>
        public char YAxisTickLine { get; set; } = '.';

        /// <summary>
        /// Gets or sets the character used to represent the graph icon.
        /// </summary>
        public char GraphIcon { get; set; } = '#';

        public char GraphLine { get; set; } = '-';

        /// <summary>
        /// Gets or sets the color of the y-axis.
        /// </summary>
        public ConsoleColor YAxisColor { get; set; } = ConsoleColor.Gray;

        /// <summary>
        /// Gets or sets the color of the x-axis.
        /// </summary>
        public ConsoleColor XAxisColor { get; set; } = ConsoleColor.Gray;

        /// <summary>
        /// Gets or sets the color of the graph icon.
        /// </summary>
        public ConsoleColor GraphIconColor { get; set; } = ConsoleColor.Yellow;

        /// <summary>
        /// Gets or sets the color of an empty point on the graph.
        /// </summary>
        public ConsoleColor EmptyPointColor { get; set; } = ConsoleColor.DarkGray;

        /// <summary>
        /// Gets or sets the color of the tick line on the y-axis.
        /// </summary>
        public ConsoleColor YAxisTickLineColor { get; set; } = ConsoleColor.DarkGray;

        /// <summary>
        /// Gets or sets the color of the y-axis label.
        /// </summary>
        public ConsoleColor YAxisLabelColor { get; set; } = ConsoleColor.White;

        /// <summary>
        /// Gets or sets the color of the x-axis label.
        /// </summary>
        public ConsoleColor XAxisLabelColor { get; set; } = ConsoleColor.White;

        /// <summary>
        /// Gets or sets the color of the tick mark on the x-axis.
        /// </summary>
        public ConsoleColor XAxisTickColor { get; set; } = ConsoleColor.White;
    }
}
