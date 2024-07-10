using Bogus;
using SharpTables;
using SharpTables.Graph;

var foos = new Faker<Foo>()
    .RuleFor(o => o.Name, f => f.Name.FirstName())
    .RuleFor(o => o.Age, f => f.Random.Number(1, 100));

var graph = new Graph<Foo>();
graph.Values = foos.Generate(10);

graph.Settings.ValueGetter = x => x.Age;
graph.Settings.XTickFormatter = x => x.Name;
graph.Settings.YTickFormatter = y => y.ToString("0.0");
graph.Settings.YAxisPadding = 1;
graph.Settings.XAxisPadding = 1;
graph.Settings.NumOfYTicks = 5;
graph.Settings.Header = "Ages of People";

graph.Write();
Console.WriteLine();
Table.FromDataSet(graph.Values.OrderByDescending(f => f.Age)).Print();
public class Foo
{
    public string Name { get; set; }
    public int Age { get; set; }
}