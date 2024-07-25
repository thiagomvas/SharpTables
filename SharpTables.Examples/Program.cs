using Bogus;
using SharpTables;
using SharpTables.Examples;
using SharpTables.Pagination;

var date = DateTime.Today;

var priceHistory = new Order[]
{
    new() { Id = Guid.NewGuid(), Item = date.AddDays(Random.Shared.Next(1, 10)).ToShortDateString(), Producer = "Apple Inc.", Quantity = 1, Price = Random.Shared.NextSingle() * 5 },
    new() { Id = Guid.NewGuid(), Item = date.AddDays(Random.Shared.Next(1, 10)).ToShortDateString(), Producer = "Apple Inc.", Quantity = 1, Price = Random.Shared.NextSingle() * 5 },
    new() { Id = Guid.NewGuid(), Item = date.AddDays(Random.Shared.Next(1, 10)).ToShortDateString(), Producer = "Apple Inc.", Quantity = 1, Price = Random.Shared.NextSingle() * 5 },
    new() { Id = Guid.NewGuid(), Item = date.AddDays(Random.Shared.Next(1, 10)).ToShortDateString(), Producer = "Apple Inc.", Quantity = 1, Price = Random.Shared.NextSingle() * 5 },
    new() { Id = Guid.NewGuid(), Item = date.AddDays(Random.Shared.Next(1, 10)).ToShortDateString(), Producer = "Apple Inc.", Quantity = 1, Price = Random.Shared.NextSingle() * 5 },
};

// Using bogus to generate fake data
var orders = new Faker<Order>()
    .RuleFor(o => o.Id, f => f.Random.Guid())
    .RuleFor(o => o.Item, f => f.Commerce.Product())
    .RuleFor(o => o.Quantity, f => f.Random.Number(1, 1000))
    .RuleFor(o => o.Price, f => f.Random.Double(1, 100))
    .RuleFor(o => o.Producer, f => f.Company.CompanyName().OrNull(f, 0.1f)) // Generate a few nullables
    .Generate(100);

orders.ForEach(o => o.Price = Math.Round(o.Price, 2)); // Rounding the prices to 2 decimal places

// Creating a formatting object from a template
var tableFormatting = TableFormatting.ASCII with
{
    DividerColor = ConsoleColor.DarkGray,
    BottomLeftDivider = '@',
    BottomRightDivider = '@',
    TopLeftDivider = '@',
    TopRightDivider = '@',
    MiddleDivider = '%',
    Header = TableFormatting.ASCII.Header with { Separated = true, }
};

// Creating a cell preset action
Action<Cell> cellPreset = c =>
{
    // Checking for cell type
    if (c.Type == typeof(Guid))
    {
        c.Text = c.Text.Substring(0, 8) + "...";
    }
    else if (c.IsNull)
    {
        c.Color = ConsoleColor.Blue;
    }
    else if (c.IsNumeric)
    {
        c.Alignment = Alignment.Center;
    }
};

// Overriding the default header
Row customHeader = new Row("This", "is", "a custom", "header", "cool right?");
customHeader.Cells.ForEach(c => c.Color = ConsoleColor.DarkGreen); // Setting the color of the header cells

// Creating a table from the data
Table table = Table.FromDataSet(orders)                // This overload will generate a table based on the properties. Header is automatically generated
    .UseFormatting(tableFormatting)                    // Applying the formatting
                                                       //.SetHeader(customHeader)                           // Setting a custom header. 
    .UsePreset(cellPreset)                             // Applying the cell preset
    .DisplayRowIndexes()                               // Displaying row indexes
    .UseRowIndexColor(ConsoleColor.DarkBlue)           // Setting the row index color
    .UseNullOrEmptyReplacement("N/A");                 // Replacing null values or empty strings with N/A


PaginatedTable paginatedTable = table.ToPaginatedTable(10); // Creating a paginated table with 10 rows per page

// Displaying the table with controls
while (true)
{
    Console.Clear();
    paginatedTable.PrintCurrent();
    Console.WriteLine($"Page {paginatedTable.CurrentPageIndex + 1} of {paginatedTable.TotalPages}");
    Console.WriteLine("[<-] Previous Page | [->] Next Page | [ESC] Exit | [Q] Order Quanity Graph (10 first products) | [H] Price History Graph ");


    var key = Console.ReadKey().Key;
    switch (key)
    {
        case ConsoleKey.LeftArrow:
            paginatedTable.PrintPrevious(); // Prints the previous page and moves the page index back
            break;
        case ConsoleKey.RightArrow:
            paginatedTable.PrintNext(); // Prints the next page and moves the page index forward
            break;
        case ConsoleKey.Escape:
            return;
        case ConsoleKey.Q:
            Console.Clear();
            Graphs.DrawOrderQuantityPieGraph(orders.Take(10));
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            break;
        case ConsoleKey.H:
            Console.Clear();
            Graphs.DrawPriceHistoryLineGraph(priceHistory);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            break;
        default:
            break;
    }
}