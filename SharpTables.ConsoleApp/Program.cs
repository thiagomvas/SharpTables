using SharpTables.Graph;

Graph graph = new Graph()
{
    Values = [10.1, 6.3, 20.12345, 1, 17.1, 11.9, 12, 19.361, 9.123]
};
graph.XTickGetter = (x) => x.ToString("0.0");
graph.YTickGetter = (y) => y.ToString("0.0");
graph.Write();
Console.WriteLine();
graph.XTickGetter = (x) => x.ToString("0.00");
graph.YTickGetter = (y) => y.ToString("0.00");
graph.Write();
Console.WriteLine();
graph.XTickGetter = (x) => x.ToString("0.000");
graph.YTickGetter = (y) => y.ToString("0.000");
graph.Write();
Console.WriteLine();