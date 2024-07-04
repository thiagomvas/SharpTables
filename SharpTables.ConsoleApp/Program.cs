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
};

List<Foo> foos = new List<Foo>
{
	new Foo { A = 1, B = "Hello", C = true },
	new Foo { A = 2, B = "World", C = false },
	new Foo { A = 3, B = "Something", C = true },
};

Foo[] otherFoos = new Foo[]
{
    new Foo { A = 4, B = "Bye", C = true },
    new Foo { A = 5, B = "World", C = false },
    new Foo { A = 6, B = "I Guess"}
};

Table.FromDataSet(foos)
    .AddDataSet(otherFoos)
	.UseFormatting(tableFormatting)
	.UsePreset(cellPreset)
    .Print();

class Foo
{
	public int A { get; set; }
	public string B { get; set; }
    public bool C { get; set; }
}

