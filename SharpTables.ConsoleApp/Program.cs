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

List<Foo> foos = new List<Foo>
{
	new Foo { A = 1, B = "Hello", C = true },
	new Foo { A = 2, B = "World", C = false },
	new Foo { A = 3, B = "SharpTables", C = true },
};

Table table = Table.FromDataSet(foos, f => new(f.C, f.B, f.A));

// You may also add rows manually!
table.AddRow(new Row(["Jimmy Jones", null, "Las Vegas"]));

table.EmptyReplacement = "N/A";
table.SetColumnPadding(1, 5, true);
table.Print();
Console.WriteLine();


class Foo
{
	public int A;
	public string B;
	public bool C;
}

