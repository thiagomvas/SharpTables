using Bogus;
using SharpTables;
using SharpTables.Annotations;


Formatting tableFormatting = Formatting.ASCII with
{
    DividerColor = ConsoleColor.DarkGray,
    BottomLeftDivider = '@',
    BottomRightDivider = '@',
    TopLeftDivider = '@',
    TopRightDivider = '@',
    MiddleDivider = '%',
    Header = Formatting.ASCII.Header with { Separated = true, }
};

Action<Cell> cellPreset = c =>
{
    if(c.Text.Length > 10)
    {
        c.Text = c.Text.Substring(0, 10) + "...";
    }
};

var faker = new Faker<Order>()
    .RuleFor(o => o.Id, f => f.Random.Guid())
    .RuleFor(o => o.Item, f => f.Commerce.ProductName())
    .RuleFor(o => o.Quantity, f => f.Random.Number(1, 10))
    .RuleFor(o => o.Price, f => f.Random.Double(1, 100));

var dataset = faker.Generate(100);
foreach (var item in dataset)
{
    item.Price = Math.Round(item.Price, 2);
}

var pages = Table.FromDataSet(dataset)
    .UseFormatting(tableFormatting)
    .UseNullOrEmptyReplacement("NULL")
    .UsePreset(cellPreset)
    .DisplayRowCount()
    .DisplayRowIndexes()
    .ToPaginatedTable(10);

while(true)
{
    Console.Clear();
    pages.PrintCurrentPage();
    Console.WriteLine("[<-] Previous Page | [->] Next Page");

    var key = Console.ReadKey().Key;
    if (key == ConsoleKey.LeftArrow)
    {
        pages.PreviousPage();
    }
    else if (key == ConsoleKey.RightArrow)
    {
        pages.NextPage();
    }
    else
    {
        break;
    }
}

class Order
{
    [TableDisplayName("Order ID")]
    public Guid Id { get; set; }
    public string Item { get; set; }

    [TableAlignment(Alignment.Center)]
    [TableColor(ConsoleColor.Yellow)]
    public int Quantity { get; set; }

    [TableColor(ConsoleColor.Green)]
    public double Price { get; set; }
}
