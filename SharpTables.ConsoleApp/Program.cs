using SharpTables;
using SharpTables.Annotations;
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
	new Foo { A = 1, B = "Hello", C = null },
	new Foo { A = 2, B = "World", C = false },
	new Foo { A = 3, B = "Something", C = true },
};

Foo[] otherFoos = new Foo[]
{
    new Foo { A = 4, B = "Bye", C = true },
    new Foo { A = null, B = "World", C = false },
    new Foo { A = 6, B = null}
};

Table.FromDataSet(foos)
	.UseFormatting(tableFormatting)
    .UseNullOrEmptyReplacement("NULL")
    .Print();


class Foo
{
    [TableOrder(1)]
    [TableColor(ConsoleColor.Red)]
    [TableAlignment(Alignment.Right)]
    [TableDisplayName("Some Int")]
	public int? A { get; set; }

    [TableDisplayName("Some String")]
    [TableColor(ConsoleColor.Cyan)]
    [TableAlignment(Alignment.Right)]
    public string? B { get; set; }

    [TableOrder(0)]
    public bool? C { get; set; }
    private double D { get; set; } = 1.234;
    public string E = "Field!";
    public static string F = "Static!";
    public static string G { get; set; } = "Static PROP!";
}

