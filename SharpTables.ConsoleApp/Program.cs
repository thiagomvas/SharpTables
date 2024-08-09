using SharpTables;
using SharpTables.Annotations;
using SharpTables.Extensions;

var foos = new List<Foo>
{
    new Foo { Id = 1, FirstName = "John", Wins = 2 },
    new Foo { Id = 2, FirstName = "Jane", Wins = 9 },
    new Foo { Id = 3, FirstName = "Joe", Wins = 4 },
    new Foo { Id = 4, FirstName = "Jill", Wins = 3 }
};

Console.Out.Graph(foos, SharpTables.Graph.GraphType.Pie);

public class Foo
{
    [TableOrder(0)]
    [TableColor(ConsoleColor.Red)]
    public int Id { get; set; }

    [TableDisplayName("Name")]
    [TableOrder(1)]
    [GraphKey]
    public string FirstName { get; set; }

    [GraphValue]
    public int Wins { get; set; }
}