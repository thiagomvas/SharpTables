using SharpTables.Graph;

var graph = new Graph<double>()
{
    Values = [10.1, 6.3, 20.12345, 1, 17.1, 11.9, 12, 19.361, 9.123]
};

var settings = new GraphSettings<double>()
{
    ValueGetter = x => x,
    XTickFormatter = x => x.ToString("0.0"),
    YTickFormatter = y => y.ToString("0.0"),
    YAxisPadding = 2,
    XAxisPadding = 1,
    NumOfYTicks = 5
};
graph.Settings = settings;

graph.Write();
Console.WriteLine();