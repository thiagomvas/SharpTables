using SharpTables.Graph;


var data = new List<Foo>
{
    new Foo { Label = "A", Value = 50 },
    new Foo { Label = "B", Value = 30 },
    new Foo { Label = "C", Value = 20 },
    new Foo { Label = "D", Value = 40 },
    new Foo { Label = "E", Value = 10 },
};

var settings = new GraphSettings<Foo>
{
    ValueGetter = x => x.Value,
    XTickFormatter = x => x.Label,
    YTickFormatter = y => y.ToString(),
    YAxisPadding = 1,
    XAxisPadding = 1,
    NumOfYTicks = 5,
    Header = "My Graph",
    MaxValue = 60,
    MinValue = 0,
    Type = GraphType.Pie
};

var formatting = new PieGraphFormatting
{
    Radius = 10,
    GroupThreshold = 0.05,
    EmptyPoint = '.',
    GraphLine = '*',
    GraphIcon = '@'
};

var g = new Graph<Foo>(data)
    .UseSettings(settings)
    .UseFormatting(formatting);

g.Write();
Console.WriteLine();

settings.Type = GraphType.Bar;
g.Write();
Console.WriteLine();

settings.Type = GraphType.Line;
g.Write();
Console.WriteLine();

Console.ResetColor();
class Foo
{
    public string Label { get; set; }
    public double Value { get; set; }
}