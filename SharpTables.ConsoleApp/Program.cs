using SharpTables;

Formatting f = new();

var table = new Table(f);
table.AddRow(new Row([ "Name", "Age", "Occupation" ]));
AddRow(table, [ "John Doe", 30, "Software Developer" ]);
AddRow(table, [ "Jane Doe", 25, null ]);
AddRow(table, [ "James Smith",null , "Manager" ]);
AddRow(table, [ "Judy Smith", 40, "CEO" ]);

table.Print();

void AddRow(Table table, object[] row)
{
	var cells = row.Select(c => new Cell(c?.ToString() ?? "null")).ToList();
	foreach(var cell in cells)
	{
		if(cell.Text == "null")
			cell.Color = ConsoleColor.Red;
	}

	table.AddRow(new Row(cells));
}