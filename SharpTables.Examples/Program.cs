using Bogus;
using SharpTables;
using SharpTables.Examples;

// Using bogus to generate fake data
var orders = new Faker<Order>()
    .RuleFor(o => o.Id, f => f.Random.Guid())
    .RuleFor(o => o.Item, f => f.Commerce.Product())
    .RuleFor(o => o.Quantity, f => f.Random.Number(1, 10))
    .RuleFor(o => o.Price, f => f.Random.Double(1, 100))
    .RuleFor(o => o.Producer, f => f.Company.CompanyName().OrNull(f, 0.1f)) // Generate a few nullables
    .Generate(100);

// Creating a formatting object from a template
var tableFormatting = Formatting.ASCII with
{
    DividerColor = ConsoleColor.DarkGray,
    BottomLeftDivider = '@',
    BottomRightDivider = '@',
    TopLeftDivider = '@',
    TopRightDivider = '@',
    MiddleDivider = '%',
    Header = Formatting.ASCII.Header with { Separated = true, }
};

// Creating a cell preset action
Action<Cell> cellPreset = c =>
{
    // Checking for cell type
    if (c.Type == typeof(Guid) && c.Text.Length > 10)
    {
        c.Text = c.Text.Substring(0, 10) + "...";
    }
    else if (c.IsNull)
    {
        c.Color = ConsoleColor.Blue;
    }
};

// Overriding the default header
Row customHeader = new Row("This", "is", "a custom", "header", "cool right?");
customHeader.Cells.ForEach(c => c.Color = ConsoleColor.DarkGreen); // Setting the color of the header cells

// Creating a table from the data
PaginatedTable paginatedTable = Table.FromDataSet(orders) // This overload will generate a table based on the properties. Header is automatically generated
    .UseFormatting(tableFormatting)   // Applying the formatting
    //.SetHeader(customHeader)          // Setting a custom header. 
    .UsePreset(cellPreset)            // Applying the cell preset
    .DisplayRowIndexes()              // Displaying row indexes
    .UseNullOrEmptyReplacement("N/A") // Replacing null values or empty strings with N/A
    .ToPaginatedTable(10);            // Creating a paginated table with 10 rows per page

// Displaying the table with controls
while (true)
{
    Console.Clear();
    paginatedTable.PrintCurrentPage();
    Console.WriteLine($"Page {paginatedTable.CurrentPageIndex + 1} of {paginatedTable.TotalPages}");
    Console.WriteLine("[<-] Previous Page | [->] Next Page | [S] Write as string | [H] Write as HTML | [M] Write as Markdown | [ESC] Exit");


    var key = Console.ReadKey().Key;
    switch (key)
    {
        case ConsoleKey.LeftArrow:
            paginatedTable.PreviousPage(); // Prints the previous page and moves the page index back
            break;
        case ConsoleKey.RightArrow:
            paginatedTable.NextPage(); // Prints the next page and moves the page index forward
            break;
        case ConsoleKey.Escape:
            return;
        case ConsoleKey.S:
            Console.Clear();
            Console.WriteLine(paginatedTable.Current.ToString()); // Writes the table as a string
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            break;
        case ConsoleKey.H:
            Console.Clear();
            Console.WriteLine(paginatedTable.Current.ToHtml()); // Writes the table as HTML
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            break;
        case ConsoleKey.M:
            Console.Clear();
            Console.WriteLine(paginatedTable.Current.ToMarkdown()); // Writes the table as Markdown
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            break;
    }
}