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
    if (c.IsBool)
    {
        bool b = c.GetValue<bool>();
        c.Text = b ? "V" : "X";
        c.Alignment = Alignment.Center;
        c.Color = b ? ConsoleColor.Green : ConsoleColor.Red;
    }
    if (c.IsNumeric)
    {
        c.Color = ConsoleColor.Yellow;
        c.Padding = 0;
        c.Alignment = Alignment.Right;
    }
    if (c.IsNull)
    {
        c.Alignment = Alignment.Center;
        c.Color = ConsoleColor.Blue;
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
    .DisplayRowIndexes()
    .ToPaginatedTable(10);

while (true)
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
    public Guid Id { get; set; }
    public string Item { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}
