using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SharpTables.Graph
{
    public class Graph<T>
    {
        public List<T> Values { get; set; }
        public GraphSettings<T> Settings { get; set; } = new();
        public GraphFormatting Formatting { get; set; } = new();

        public void Write()
        {
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
    }
}
