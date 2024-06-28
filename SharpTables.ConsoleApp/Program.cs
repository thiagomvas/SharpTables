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

table.EmptyReplacement = "N/A";
table.SetColumnPadding(1, 5, true);
table.UsePreset(c =>
{
	if(bool.TryParse(c.Text, out bool b))
	{
		c.Text = b ? "V" : "X";
		c.Alignment = Alignment.Center;
		c.Color = b ? ConsoleColor.Green : ConsoleColor.Red;
		c.Padding = 0;
	}
	if(int.TryParse(c.Text, out int i))
	{
		c.Color = ConsoleColor.Yellow;
		c.Padding = 0;
	}
});
table.AddHeaderRow(new Row("Is Active", "Name", "ID"));
table.Print();
Console.WriteLine(table.ToString());


class Foo
{
	public int A;
	public string B;
	public bool C;
}

