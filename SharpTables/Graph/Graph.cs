using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SharpTables.Graph
{
    public class Graph
    {
        public List<double> Values { get; set; }


        private int yTickSpacing = 1;
        private int xTickSpacing = 3;
        private int numOfYTicks = 5;

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

            string largestYTick = max.ToString("0.0");
            string largestXTick = largestYTick;

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
                    var emptySpaceOffset = new string(' ', largestYTick.Length + 2 - i.ToString("0.0").Length);
                    Console.Write($"{i:0.0}{emptySpaceOffset}|");
                }
                else
                {
                    Console.Write(new string(' ', largestYTick.Length + 2) + "|");
                }
                foreach (int num in Values)
                {
                    // resize padding to make sure the bar is centered on the x tick
                    int len = num.ToString("0.0").Length;
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

            // print the x divider
            Console.WriteLine(new string('-', largestYTick.Length + 2) + "+" + new string('-', Values.Count * 3));

            // print the x ticks centered on bars
            Console.Write(new string(' ', largestYTick.Length + 2));
            for (int i = 0; i < Values.Count; i++)
            {
                var num = Values[i];
                int len = num.ToString("0.0").Length;
                lPadding = new string(' ', len / 2 + xTickSpacing - (i == Values.Count - 1 ? 2 : 1) - (len % 2 != 0 ? 1 : 0));
                rPadding = new string(' ', len / 2 + xTickSpacing - 1 - (len % 2 != 0 ? 1 : 0));
                Console.Write($"{lPadding}{Values[i].ToString("0.0")}{rPadding}");
            }
        }
    }
}
