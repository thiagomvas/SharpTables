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

            for (int i = 0; i < Values.Count; i++)
            {
                numsOffset[i] = XTickGetter(Values[i]).Length / 2;
                if (i == 0)
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


            for (int y = 0; y <= lineCount; y++)
            {
                double yVal = max - y * (max - min) / lineCount;
                if (y == lineCount)
                    yVal = min;
                // Y tick
                if (y % (yTickSpacing + 1) == 0)
                {
                    Console.Write(YTickGetter(yVal));
                    Console.Write(new string(' ', maxStrlen - YTickGetter(yVal).Length + yTickPadding));
                    Console.Write("|");
                }
                else
                {
                    Console.Write(new string(' ', maxStrlen + yTickPadding));
                    Console.Write("|");
                }
                for (int x = x0; x <= lineWidth; x++)
                {
                    bool hitBar = false;
                    for (int i = 0; i < Values.Count; i++)
                    {
                        if (numCenterCoords[i] + numsOffset[i] == x)
                        {
                            if (Values[i] >= yVal)
                            {
                                Console.Write("#");
                                hitBar = true;
                            }
                        }
                    }


                    if (!hitBar)
                    {
                        Console.Write('.');
                    }
                    else
                        hitBar = false;

                }
                Console.WriteLine();
            }
            lineCount++;
            string xAxis = new string('-', lineWidth);

            // Put a "+" where the bars are
            for (int i = 0; i < Values.Count; i++)
            {
                xAxis = xAxis.Remove(numCenterCoords[i] + numsOffset[i], 1);
                xAxis = xAxis.Insert(numCenterCoords[i] + numsOffset[i], "+");
            }
            xAxis = xAxis.Remove(x0-1, 1);
            xAxis = xAxis.Insert(x0-1, "+");

            Console.WriteLine(xAxis);
            for(int x = 0; x < lineWidth;)
            {
                if(x == x0 - 1)
                {
                    Console.Write('|');
                    x++;
                    continue;
                }
                bool hasHit = false;
                for(int i = 0; i < Values.Count ; i++)
                {
                    if (numCenterCoords[i] == x)
                    {
                        Console.Write(XTickGetter(Values[i]));
                        x += XTickGetter(Values[i]).Length;
                        hasHit = true;
                        break;
                    }
                }
                if (!hasHit)
                {
                    Console.Write(' ');
                    x++;
                }
                else
                {
                    hasHit = false;
                }
            }


        }
    }
}
