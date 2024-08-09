using SharpTables;
using SharpTables.Annotations;
using SharpTables.Extensions;

var foos = new List<Foo>
{
    new Foo { Id = 1, FirstName = "John" },
    new Foo { Id = 2, FirstName = "Jane" },
    new Foo { Id = 3, FirstName = "Joe" },
    new Foo { Id = 4, FirstName = "Jill" }
};

Console.Out.Table(foos, TableFormatting.Minimalist, new TableSettings() { DisplayRowCount = true});

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