using Bogus;
using SharpTables;
using SharpTables.Graph;

var foos = new Faker<Foo>()
    .RuleFor(o => o.Name, f => f.Name.FirstName())
    .RuleFor(o => o.Age, f => f.Random.Number(1, 100));

var graph = new Graph<Foo>(foos.Generate(15));

var settings = new GraphSettings<Foo>()
{
    ValueGetter = x => x.Age,
    XTickFormatter = x => x.Name,
    YTickFormatter = y => y.ToString("0.0"),
    YAxisPadding = 1,
    XAxisPadding = 1,
    NumOfYTicks = 5,
    Header = "Ages",
    MaxValue = 100,
    MinValue = 0,
};

graph.Settings = settings;
Console.WriteLine();
graph.ToPaginatedGraph(5).PrintPage(2);
Console.WriteLine();

Table.FromDataSet(graph.Values.OrderByDescending(f => f.Age)).Write();
public class Foo
{
    public string Name { get; set; }
    public int Age { get; set; }
}