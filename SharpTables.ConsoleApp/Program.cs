using SharpTables;


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

List<Foo> foos = new List<Foo>
{
	new Foo { A = 1, B = "Hello", C = true },
	new Foo { A = 2, B = "World", C = false },
	new Foo { A = 3, B = "!!!!", C = true },
};

Table table = Table.FromDataSet(foos, f => new(f.C, f.B, f.A)); // Novo overload: Passar uma lista de elementos e uma factory ou gerador de linhas usando esses elementos.
table.Formatting = tableFormatting;
table.EmptyReplacement = "N/A";

// Agr posso aplicar preset por celula.
table.UsePreset(c =>
{
	if (bool.TryParse(c.Text, out bool b))
	{
		c.Text = b ? "V" : "X";
		c.Alignment = Alignment.Center;
		c.Color = b ? ConsoleColor.Green : ConsoleColor.Red;
		c.Padding = 0;
	}
	if (int.TryParse(c.Text, out int i))
	{
		c.Color = ConsoleColor.Yellow;
		c.Padding = 0;
	}
});
table.AddHeaderRow(new Row("Is Active", "Name", "ID")); // Colocar uma row no topo da tabela.
table.Print();


class Foo
{
	public int A;
	public string B;
	public bool C;
}

