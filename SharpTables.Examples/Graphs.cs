using SharpTables.Graph;

namespace SharpTables.Examples
{
    public static class Graphs
    {
        public static void DrawPriceHistoryLineGraph(IEnumerable<Order> orders)
        {
            var formatting = new GraphFormatting()
            {
                GraphIcon = '@',
                GraphLine = '*',
                YAxisTickLine = '-',
            };

            var settings = new GraphSettings<Order>
            {
                ValueGetter = x => x.Price,
                XTickFormatter = x => x.Item,
                YTickFormatter = y => y.ToString("0.00") + '$',
                YAxisPadding = 1,
                XAxisPadding = 1,
                NumOfYTicks = 5,
                Header = "Price History Line Graph",
                MaxValue = 5,
                MinValue = 0,
                Type = GraphType.Line
            };

            var g = new Graph<Order>(orders)
                .UseSettings(settings)
                .UseFormatting(formatting);

            g.Write();
            Console.WriteLine();
        }
        public static void DrawOrderQuantityPieGraph(IEnumerable<Order> orders)
        {
            var formatting = new PieGraphFormatting()
            {
                Radius = 10,
                GroupThreshold = 0.01, // Below 1% will be grouped together
                EmptyPoint = '.',
                GraphIcon = '@'
            };

            var settings = new GraphSettings<Order>
            {
                ValueGetter = x => x.Quantity,
                XTickFormatter = x => x.Item,
                YTickFormatter = y => y.ToString(),
                YAxisPadding = 1,
                XAxisPadding = 1,
                NumOfYTicks = 5,
                Header = "Order Quantity Pie Graph",
                MaxValue = orders.Max(x => x.Quantity),
                MinValue = 0,
                Type = GraphType.Pie
            };

            var g = new Graph<Order>(orders)
                .UseSettings(settings)
                .UseFormatting(formatting);

            g.Write();
        }
    }
}
