using SharpTables;


Formatting f = Formatting.ASCII with 
{ 
	DividerColor = ConsoleColor.DarkGray,
	BottomLeftDivider = '@',
	BottomRightDivider = '@',
	TopLeftDivider = '@',
	TopRightDivider = '@',
	MiddleDivider = '%',
	Header = Formatting.ASCII.Header with { Separated = true, }
};

object[,] dataset = new object[,]
{
	{ "Name", "Age", "City" },
	{ "John Doe", 42, "New York" },
	{ "Jane Doe", 36, "Chicago" },
	{ "Joe Bloggs", 25, "Los Angeles" },
	{ "Jenny Smith", 28, "Miami" }
};

Table table = Table.FromDataSet(dataset, f);

// You may also add rows manually!
table.AddRow(new Row(["Jimmy Jones", null, "Las Vegas"]));

table.SetColumnColor(1, ConsoleColor.Yellow);
table.EmptyReplacement = "N/A";
table.SetColumnPadding(1, 5, true);
table.Print();
Console.WriteLine();


