
using Bogus;
using SharpTables.Graph;

var faker = new Faker<Foo>()
    .RuleFor(f => f.Fizz, f => f.Random.Int(-100, 100))
    .RuleFor(f => f.Buzz, f => f.Random.String2(3));

var data = faker.Generate(10);

var formatting = new GraphFormatting() with
{
    GraphLine = '*',
};

var graph = new Graph<Foo>(data)
    .UseValueGetter(f => f.Fizz)
    .UseXTickFormatter(f => f.Buzz)
    .UseYTickFormatter(f => f.ToString("0"))
    .UseMinValue(-100)
    .UseMaxValue(100)
    .UseFormatting(formatting)
    .UseGraphType(GraphType.Bar)
    .UseHeader("Bar Graph");

graph.Write();
Console.WriteLine("\n\n");

graph.UseGraphType(GraphType.Line).UseHeader("Line Graph");
graph.Write();
Console.WriteLine("\n\n");

graph.UseGraphType(GraphType.Scatter).UseHeader("Scatter Graph");
graph.Write();
Console.WriteLine("\n\n");


class Foo
{
    public int Fizz { get; set; }
    public string Buzz { get; set; }
}