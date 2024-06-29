using SharpTables;
using System.Data;


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
	new Foo { A = 3, B = "Something", C = true },
};

Table table = Table.FromDataSet(foos, f => new(f.C, f.B, f.A)); // Novo overload: Passar uma lista de elementos e uma factory ou gerador de linhas usando esses elementos.
table.Formatting = tableFormatting;

table.UsePreset(c =>
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
});
table.EmptyReplacement = "N/A";

table.SetHeader(new Row("Is Active", "Name", "ID")); // Define uma row como header ao inves de usar a primeira linha do dataset.
table.AddDataSet(foos, f => new(f.C, f.B, f.A)); // Adiciona um dataset ao inves de substituir o existente.
foreach(var cell in table.Header.Cells)
{
	cell.Alignment = Alignment.Center;
	cell.Padding = 2;
	cell.Color = ConsoleColor.Cyan;
}

table.Print();

class Foo
{
	public int A;
	public string B;
	public bool C;
}

