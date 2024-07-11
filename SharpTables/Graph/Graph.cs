using System.Text;
using SharpTables.Pagination;

namespace SharpTables.Graph
{
    /// <summary>
    /// Utility class to draw a graph in the console. Not recommended for very large data sets. 
    /// </summary>
    /// <typeparam name="T">The type of data stored in the graph</typeparam>
    public class Graph<T> : IConsoleWriteable
    {
        /// <summary>
        /// The values to be graphed
        /// </summary>
        public List<T> Values { get; set; }

        /// <summary>
        /// The settings for the graph
        /// </summary>
        public GraphSettings<T> Settings { get; set; } = new();

        /// <summary>
        /// The formatting for the graph
        /// </summary>
        public GraphFormatting Formatting { get; set; } = new();

        public Graph(IEnumerable<T> values)
        {
            Values = values.ToList();
        }

        /// <summary>
        /// Sets the settings for the graph
        /// </summary>
        /// <param name="settings">The settings object</param>
        public Graph<T> UseSettings(GraphSettings<T> settings)
        {
            Settings = settings;
            return this;
        }

        /// <summary>
        /// Sets the formatting for the graph
        /// </summary>
        /// <param name="formatting">The formatting object</param>
        public Graph<T> UseFormatting(GraphFormatting formatting)
        {
            Formatting = formatting;
            return this;
        }

        public static Graph<T> FromDataSet<T>(IEnumerable<T> data)
        {
            return new Graph<T>(data);
        }

        /// <summary>
        /// Sets the value getter for the graph
        /// </summary>
        /// <param name="valueGetter">The value getter function</param>
        /// <returns>The graph with changes applied</returns>
        public Graph<T> UseValueGetter(Func<T, double> valueGetter)
        {
            Settings.ValueGetter = valueGetter;
            return this;
        }

        /// <summary>
        /// Sets the X tick formatting function for the graph
        /// </summary>
        /// <param name="xTickFormatter">The formatting function</param>
        /// <returns>The graph with changes applied</returns>
        public Graph<T> UseXTickFormatter(Func<T, string> xTickFormatter)
        {
            Settings.XTickFormatter = xTickFormatter;
            return this;
        }

        /// <summary>
        /// Sets the Y tick formatting function for the graph
        /// </summary>
        /// <param name="yTickFormatter">The formatting function</param>
        /// <returns>The graph with changes applied</returns>
        public Graph<T> UseYTickFormatter(Func<double, string> yTickFormatter)
        {
            Settings.YTickFormatter = yTickFormatter;
            return this;
        }

        /// <summary>
        /// Sets the Y axis padding (number of lines between ticks) for the graph. 
        /// </summary>
        /// <param name="yAxisPadding">The number of lines of padding</param>
        /// <returns>The graph with changes applied</returns>
        public Graph<T> UseYAxisPadding(int yAxisPadding)
        {
            Settings.YAxisPadding = yAxisPadding;
            return this;
        }

        /// <summary>
        /// Sets the X axis padding (number of spaces between ticks) for the graph.
        /// </summary>
        /// <param name="xAxisPadding">The padding value</param>
        /// <returns>The graph with changes applied</returns>
        public Graph<T> UseXAxisPadding(int xAxisPadding)
        {
            Settings.XAxisPadding = xAxisPadding;
            return this;
        }

        /// <summary>
        /// Sets the number of Y ticks for the graph
        /// </summary>
        /// <param name="numOfYTicks">The number of ticks in the Y axis</param>
        /// <returns>The graph with changes applied</returns>
        public Graph<T> UseNumOfYTicks(int numOfYTicks)
        {
            Settings.NumOfYTicks = numOfYTicks;
            return this;
        }

        /// <summary>
        /// Sets the header for the graph
        /// </summary>
        /// <param name="header">The header text</param>
        /// <returns>The graph with changes applied</returns>
        public Graph<T> UseHeader(string header)
        {
            Settings.Header = header;
            return this;
        }

        /// <summary>
        /// Sets the maximum value for the Y axis. Leave null for automatic sizing.
        /// </summary>
        /// <param name="maxValue">The value of the highest tick in the graph</param>
        /// <returns>The graph with changes applied</returns>
        public Graph<T> UseMaxValue(double maxValue)
        {
            Settings.MaxValue = maxValue;
            return this;
        }

        /// <summary>
        /// Sets the minimum value for the Y axis. Leave null for automatic sizing.
        /// </summary>
        /// <param name="minValue">The value of the lowest tick in the graph</param>
        /// <returns>The graph with changes applied</returns>
        public Graph<T> UseMinValue(double minValue)
        {
            Settings.MinValue = minValue;
            return this;
        }



        public PaginatedGraph<T> ToPaginatedGraph(int columnsPerPage)
        {
            var paginatedGraph = new PaginatedGraph<T>(Values);
            paginatedGraph.Settings = Settings;
            paginatedGraph.Formatting = Formatting;
            paginatedGraph.ColumnsPerPage = columnsPerPage;
            return paginatedGraph;
        }

        /// <summary>
        /// Writes the graph to the console
        /// </summary>
        public void Write()
        {
            // Setup
            var values = Values.Select(Settings.ValueGetter).ToList();
            var max = Settings.MaxValue ?? values.Max() * 1.1f;
            var min = Settings.MinValue ?? values.Min() * 0.9f;
            int yTickPadding = 2;
            int xTickPadding = 2;

            int maxStrlen = Settings.YTickFormatter(max).Length;
            int minStrlen = Settings.YTickFormatter(min).Length;

            int x0 = maxStrlen + yTickPadding + 1;
            int y0 = Settings.NumOfYTicks * Settings.YAxisPadding;

            int[] numCenterCoords = new int[Values.Count];
            int[] numsOffset = new int[Values.Count];

            // Get coordinates for numbers and offsets to position bars
            for (int i = 0; i < Values.Count; i++)
            {
                numsOffset[i] = Settings.XTickFormatter(Values[i]).Length / 2;
                if (i == 0)
                {
                    numCenterCoords[i] = x0 + 1;
                }
                else
                {
                    numCenterCoords[i] = numCenterCoords[i - 1] + Settings.XTickFormatter(Values[i - 1]).Length + xTickPadding + 1;
                }
            }

            int lineCount = Settings.NumOfYTicks * (Settings.YAxisPadding + 1);
            int lineWidth = numCenterCoords[Values.Count - 1] + Settings.XTickFormatter(Values[Values.Count - 1]).Length + xTickPadding + 1;

            // Print the header centered
            int headerStart = (lineWidth - Settings.Header.Length) / 2;
            Console.WriteLine(new string(' ', headerStart) + Settings.Header);

            // Build the graph
            for (int y = 0; y <= lineCount; y++)
            {

                double yVal = max - y * (max - min) / lineCount;
                if (y == lineCount)
                    yVal = min;
                // Y ticks
                if (y % (Settings.YAxisPadding + 1) == 0)
                {
                    Console.ForegroundColor = Formatting.YAxisLabelColor;
                    Console.Write(Settings.YTickFormatter(yVal));
                    Console.ForegroundColor = Formatting.YAxisColor;
                    Console.Write(new string(Formatting.EmptyPoint, maxStrlen - Settings.YTickFormatter(yVal).Length + yTickPadding));
                    Console.Write(Formatting.YAxisTick);
                }
                else
                {
                    Console.ForegroundColor = Formatting.YAxisColor;
                    Console.Write(new string(Formatting.EmptyPoint, maxStrlen + yTickPadding));
                    Console.Write(Formatting.VerticalLine);
                }
                Console.ResetColor();
                // Bars
                for (int x = x0; x <= lineWidth; x++)
                {
                    bool hitBar = false;
                    for (int i = 0; i < Values.Count; i++)
                    {
                        if (numCenterCoords[i] + numsOffset[i] == x)
                        {
                            if (values[i] >= yVal)
                            {
                                Console.ForegroundColor = Formatting.GraphIconColor;
                                Console.Write(Formatting.GraphIcon);
                                Console.ResetColor();
                                hitBar = true;
                            }
                        }
                    }

                    if (!hitBar)
                    {
                        if (y % (Settings.YAxisPadding + 1) == 0)
                        {
                            Console.ForegroundColor = Formatting.YAxisTickLineColor;
                            Console.Write(Formatting.YAxisTickLine);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = Formatting.EmptyPointColor;
                            Console.Write(Formatting.EmptyPoint);
                            Console.ResetColor();
                        }
                    }
                    else
                        hitBar = false;

                }
                Console.WriteLine();
            }

            // Draw X axis
            lineCount++;
            string xAxis = new string(Formatting.HorizontalLine, lineWidth);

            // Put a "+" where the bars are
            for (int i = 0; i < Values.Count; i++)
            {
                xAxis = xAxis.Remove(numCenterCoords[i] + numsOffset[i], 1);
                xAxis = xAxis.Insert(numCenterCoords[i] + numsOffset[i], Formatting.XAxisTick.ToString());
            }
            xAxis = xAxis.Remove(x0 - 1, 1);
            xAxis = xAxis.Insert(x0 - 1, Formatting.Origin.ToString());

            Console.ForegroundColor = Formatting.XAxisColor;
            foreach(char c in xAxis)
            {
                if(c == Formatting.XAxisTick)
                {
                    Console.ForegroundColor = Formatting.XAxisTickColor;
                    Console.Write(c);
                    Console.ForegroundColor = Formatting.XAxisColor;
                }
                else
                {
                    Console.Write(c);
                }
            }
            Console.WriteLine();
            Console.ForegroundColor = Formatting.XAxisLabelColor;
            // Draw X axis ticks
            for (int x = 0; x < lineWidth;)
            {
                if (x == x0 - 1)
                {
                    Console.Write(Formatting.VerticalLine);
                    x++;
                    continue;
                }
                bool hasHit = false;
                for (int i = 0; i < Values.Count; i++)
                {
                    if (numCenterCoords[i] == x)
                    {
                        Console.Write(Settings.XTickFormatter(Values[i]));
                        x += Settings.XTickFormatter(Values[i]).Length;
                        hasHit = true;
                        break;
                    }
                }
                if (!hasHit)
                {
                    Console.Write(' ');
                    x++;
                }
            }
            Console.ResetColor();
        }

        /// <summary>
        /// Returns the graph as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            // Setup
            var values = Values.Select(Settings.ValueGetter).ToList();
            var max = values.Max() * 1.1f;
            var min = values.Min() * 0.9f;
            int yTickPadding = 2;
            int xTickPadding = 2;

            int maxStrlen = Settings.YTickFormatter(max).Length;
            int minStrlen = Settings.YTickFormatter(min).Length;

            int x0 = maxStrlen + yTickPadding + 1;
            int y0 = Settings.NumOfYTicks * Settings.YAxisPadding;

            int[] numCenterCoords = new int[Values.Count];
            int[] numsOffset = new int[Values.Count];

            // Get coordinates for numbers and offsets to position bars
            for (int i = 0; i < Values.Count; i++)
            {
                numsOffset[i] = Settings.XTickFormatter(Values[i]).Length / 2;
                if (i == 0)
                {
                    numCenterCoords[i] = x0 + 1;
                }
                else
                {
                    numCenterCoords[i] = numCenterCoords[i - 1] + Settings.XTickFormatter(Values[i - 1]).Length + xTickPadding + 1;
                }
            }

            int lineCount = Settings.NumOfYTicks * (Settings.YAxisPadding + 1);
            int lineWidth = numCenterCoords[Values.Count - 1] + Settings.XTickFormatter(Values[Values.Count - 1]).Length + xTickPadding + 1;

            // Build the graph
            for (int y = 0; y <= lineCount; y++)
            {
                double yVal = max - y * (max - min) / lineCount;
                if (y == lineCount)
                    yVal = min;
                // Y ticks
                if (y % (Settings.YAxisPadding + 1) == 0)
                {
                    sb.Append(Settings.YTickFormatter(yVal));
                    sb.Append(new string(Formatting.EmptyPoint, maxStrlen - Settings.YTickFormatter(yVal).Length + yTickPadding));
                    sb.Append(Formatting.YAxisTick);
                }
                else
                {
                    sb.Append(new string(Formatting.EmptyPoint, maxStrlen + yTickPadding));
                    sb.Append(Formatting.VerticalLine);
                }
                // Bars
                for (int x = x0; x <= lineWidth; x++)
                {
                    bool hitBar = false;
                    for (int i = 0; i < Values.Count; i++)
                    {
                        if (numCenterCoords[i] + numsOffset[i] == x)
                        {
                            if (values[i] >= yVal)
                            {
                                sb.Append(Formatting.GraphIcon);
                                hitBar = true;
                            }
                        }
                    }

                    if (!hitBar)
                    {
                        if (y % (Settings.YAxisPadding + 1) == 0)
                        {
                            sb.Append(Formatting.YAxisTickLine);
                        }
                        else
                        {
                            sb.Append(Formatting.EmptyPoint);
                        }
                    }
                    else
                        hitBar = false;
                }
                sb.AppendLine();
            }

            // Draw X axis
            lineCount++;
            string xAxis = new string(Formatting.HorizontalLine, lineWidth);

            // Put a "+" where the bars are
            for (int i = 0; i < Values.Count; i++)
            {
                xAxis = xAxis.Remove(numCenterCoords[i] + numsOffset[i], 1);
                xAxis = xAxis.Insert(numCenterCoords[i] + numsOffset[i], Formatting.XAxisTick.ToString());
            }
            xAxis = xAxis.Remove(x0 - 1, 1);
            xAxis = xAxis.Insert(x0 - 1, Formatting.Origin.ToString());

            foreach (char c in xAxis)
            {
                sb.Append(c);
            }
            sb.AppendLine();

            // Draw X axis ticks
            for (int x = 0; x < lineWidth;)
            {
                if (x == x0 - 1)
                {
                    sb.Append(Formatting.VerticalLine);
                    x++;
                    continue;
                }
                bool hasHit = false;
                for (int i = 0; i < Values.Count; i++)
                {
                    if (numCenterCoords[i] == x)
                    {
                        sb.Append(Settings.XTickFormatter(Values[i]));
                        x += Settings.XTickFormatter(Values[i]).Length;
                        hasHit = true;
                        break;
                    }
                }
                if (!hasHit)
                {
                    sb.Append(' ');
                    x++;
                }
            }

            return sb.ToString();
        }
    }
}
