using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SharpTables.Graph
{
    public class Graph
    {
        public List<double> Values { get; set; }


        private int yTickSpacing = 1;
        private int xTickSpacing = 1;
        private int numOfYTicks = 5;

        public Func<double, string> XTickGetter { get; set; } = (x) => x.ToString("0.000");
        public Func<double, string> YTickGetter { get; set; } = (y) => y.ToString("0.000");

        public int YTickSpacing
        {
            get => yTickSpacing;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Value must be greater than 0");
                }

                yTickSpacing = value;
            }
        }

        public int XTickSpacing
        {
            get => xTickSpacing;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Value must be greater than 0");
                }

                xTickSpacing = value;
            }
        }

        public int NumOfYTicks
        {
            get => numOfYTicks;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Value must be greater than 0");
                }

                numOfYTicks = value;
            }
        }

        public void PrintBarGraph()
        {
            double max = Values.Max() * 1.1;
            double min = Values.Min() * 0.9;

            string largestYTick = YTickGetter(max);
            string largestXTick = XTickGetter(max);

            int leftSpacing, rightSpacing;
            leftSpacing = largestXTick.Length / 2;
            rightSpacing = largestXTick.Length / 2 + 1;

            string lPadding = new string(' ', leftSpacing);
            string rPadding = new string(' ', rightSpacing);

            int lineIndex = 0;
            double tickDelta = (max - min) / numOfYTicks;
            for (double i = max; i >= min - tickDelta; i -= tickDelta / (yTickSpacing + 1))
            {
                if (lineIndex % (yTickSpacing + 1) == 0)
                {
                    var emptySpaceOffset = new string(' ', largestYTick.Length + 2 - YTickGetter(i).Length);
                    Console.Write($"{YTickGetter(i)}{emptySpaceOffset}|");
                }
                else
                {
                    Console.Write(new string(' ', largestYTick.Length + 2) + "|");
                }
                foreach (int num in Values)
                {
                    // resize padding to make sure the bar is centered on the x tick
                    int len = XTickGetter(num).Length;
                    lPadding = new string(' ', len / 2 + xTickSpacing - (len % 2 != 0 ? 1 : 0));
                    rPadding = new string(' ', len / 2 + xTickSpacing);
                    if (num >= i)
                    {
                        Console.Write($"{lPadding}#{rPadding}");
                    }
                    else
                    {
                        Console.Write($"{lPadding} {rPadding}");
                    }
                }
                Console.WriteLine();
                lineIndex++;
            }


            // print the x axis
            Console.Write(new string('-', largestYTick.Length + 2) + "+");
            for (int i = 0; i < Values.Count; i++)
            {
                Console.Write(new string('-', XTickGetter(Values[i]).Length + xTickSpacing * 2));
            }

            Console.WriteLine();

            // print the x ticks centered on bars
            Console.Write(new string(' ', largestYTick.Length + 2) + '|');
            for (int i = 0; i < Values.Count; i++)
            {
                var num = Values[i];
                int len = XTickGetter(num).Length;

                if(i == 0)
                {
                    lPadding = new string(' ', len / 2 + xTickSpacing - (len % 2 != 0 ? 1 : 0) - 2);
                }
                else
                    lPadding = new string(' ', len / 2 + xTickSpacing - (i == Values.Count - 1 ? 2 : 1) - (len % 2 != 0 ? 1 : 0));


                rPadding = new string(' ', len / 2 + xTickSpacing - 1 - (len % 2 != 0 ? 1 : 0));
                Console.Write($"{lPadding}{XTickGetter(Values[i])}{rPadding}");
            }
        }

        public void Write()
        {
            var max = Values.Max() * 1.1f;
            var min = Values.Min() * 0.9f;
            int yTickPadding = 2;
            int xTickPadding = 2;

            int maxStrlen = YTickGetter(max).Length;
            int minStrlen = YTickGetter(min).Length;

            (int l, int t) = Console.GetCursorPosition();

            int x0 = maxStrlen + yTickPadding + 1;
            int y0 = numOfYTicks * yTickSpacing;

            int[] numCenterCoords = new int[Values.Count];
            int[] numsOffset = new int[Values.Count];

            for(int i = 0; i < Values.Count; i++)
            {
                numsOffset[i] = XTickGetter(Values[i]).Length / 2;
                if(i == 0)
                {
                    numCenterCoords[i] = x0 + 1;
                }
                else
                {
                    numCenterCoords[i] = numCenterCoords[i - 1] + XTickGetter(Values[i - 1]).Length + xTickPadding + 1;
                }
            }

            int lineCount = numOfYTicks * (yTickSpacing + 1);
            int lineWidth = numCenterCoords[Values.Count - 1] + XTickGetter(Values[Values.Count - 1]).Length + xTickPadding + 1;
            for(int y = 0; y <= lineCount; y++)
            {
                double yVal = max - y * (max - min) / lineCount;
                if (y == lineCount)
                    yVal = min;
                // Y tick
                if(y % (yTickSpacing + 1) == 0)
                {
                    Console.SetCursorPosition(0, y);
                    Console.Write(YTickGetter(yVal));
                    Console.Write(new string(' ', maxStrlen - YTickGetter(yVal).Length + yTickPadding));
                    Console.Write("|");
                }
                else
                {
                    Console.SetCursorPosition(0, y);
                    Console.Write(new string(' ', maxStrlen + yTickPadding));
                    Console.Write("|");
                }

                for(int i = 0; i < Values.Count; i++)
                {
                    if (Values[i] >= yVal)
                    {
                        Console.SetCursorPosition(numCenterCoords[i] + numsOffset[i], y);
                        Console.Write("#");
                    }
                }
            }
            lineCount++;
            Console.WriteLine();
            string xAxis = new string('-', lineWidth);

            // Put a "+" where the bars are
            for(int i = 0; i < Values.Count; i++)
            {
                xAxis = xAxis.Remove(numCenterCoords[i] + numsOffset[i], 1);
                xAxis = xAxis.Insert(numCenterCoords[i] + numsOffset[i], "+");
            }

            Console.WriteLine(xAxis);
            for(int i = 0; i < Values.Count; i++)
            {
                Console.SetCursorPosition(numCenterCoords[i], lineCount + 1);
                Console.Write(XTickGetter(Values[i]));
            }
        }
    }
}
