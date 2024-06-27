using SharpTables;

Formatting f = Formatting.Minimalist;

object[,] dataset = new object[,]
{
	{ "Name", "Age", "City" },
	{ "John Doe", 42, "New York" },
	{ "Jane Doe", 36, "Chicago" },
	{ "Joe Bloggs", 25, "Los Angeles" },
	{ "Jenny Smith", 28, "Miami" }
};

Table table = Table.FromDataSet(dataset, f);
table.NumberAlignment = Alignment.Center;
table.SetColumnColor(1, ConsoleColor.Yellow);
table.SetColumnPadding(1, 1, true);
table.Print();
