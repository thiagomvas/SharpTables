# SharpTables
A versatile and customizable console table formatter. Generate tables ready to be written to console with the ability to customize even the characters used by the generator to generate the table.

## Example usage
For example programs, see the [Examples](
### Basic table
```cs
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

Table table = Table.FromDataSet(dataset) // Also supports IEnumerable<IEnumerable<T>>
    .AddRow(new Row("Jimmy Jones", null, "Las Vegas")) // Supports nullables and manually adding rows
    .UseNullOrEmptyReplacement("N/A")
    .UsePreset(cell => cell.Color = cell.IsNumeric ? ConsoleColor.Yellow : ConsoleColor.White);

table.Print();

/*
 Name             Age      City
────────────────────────────────────────────
 John Doe         42       New York

 Jane Doe         36       Chicago

 Joe Bloggs       25       Los Angeles

 Jenny Smith      28       Miami

 Jimmy Jones      N/A      Las Vegas
*/
```

### Custom Formatting
The ``Formatting`` class already has some presets, you can also modify them using ``with`` due to them being ``record`` types, or make your own instance. By default, using ``new Formatting()`` will use ``Formatting.Default``

```cs

// Using the power of records!
Formatting format = Formatting.ASCII with 
{ 
	DividerColor = ConsoleColor.DarkGray,
	BottomLeftDivider = '@',
	BottomRightDivider = '@',
	TopLeftDivider = '@',
	TopRightDivider = '@',
	MiddleDivider = '%',
	Header = Formatting.ASCII.Header with { Separated = true, }
};
/*
+----------------+--------+----------------+
|Name            |Age     |City            |
+----------------+--------+----------------+
@----------------+--------+----------------@
|John Doe        |42      |New York        |
+----------------%--------%----------------+
|Jane Doe        |36      |Chicago         |
+----------------%--------%----------------+
|Joe Bloggs      |25      |Los Angeles     |
+----------------%--------%----------------+
|Jenny Smith     |28      |Miami           |
+----------------%--------%----------------+
|Jimmy Jones     |N/A     |Las Vegas       |
@----------------+--------+----------------@
*/
```

### Cell Formatting
You can also use cell-specific formatting, which can be used to change how the cell will look like in the table.

```cs

List<Foo> foos = new List<Foo>
{
	new Foo { A = 1, B = "Hello", C = true },
	new Foo { A = 2, B = "World", C = false },
	new Foo { A = 3, B = "Something", C = true },
};

// 'c' represents any cell in the table
table.UsePreset(c =>
{
	if (bool.TryParse(c.Text, out bool b))
	{
		c.Text = b ? "V" : "X";
		c.Alignment = Alignment.Center;
		c.Color = b ? ConsoleColor.Green : ConsoleColor.Red;
	}
	if (int.TryParse(c.Text, out int i))
	{
		c.Color = ConsoleColor.Yellow;
		c.Padding = 0;
		c.Alignment = Alignment.Right;
	}
});
/*
The result is colored on console.
╔═══════════╦══════════════╦════╗
║ Is Active ║     Name     ║ ID ║
╠═══════════╬══════════════╬════╣
│     V     │Hello         │   1│
├───────────┼──────────────┼────┤
│     X     │World         │   2│
├───────────┼──────────────┼────┤
│     V     │Something     │   3│
└───────────┴──────────────┴────┘
*/
```

## Contributing
If you found a bug, have any questions, want to implement your own additions or contribute in any other way, feel free to open a pull request!

## License
This project is licensed under the MIT License
