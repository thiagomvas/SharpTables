# SharpTables
A versatile and customizable console table formatter. Generate tables ready to be written to console with the ability to customize even the characters used by the generator to generate the table.

## Example usage
### Basic table
```cs
Formatting f = Formatting.Minimalist;

object[,] dataset = new object[,]
{
	{ "Name", "Age", "City" },
	{ "John Doe", 42, "New York" },
	{ "Jane Doe", 36, "Chicago" },
	{ "Joe Bloggs", 25, "Los Angeles" },
	{ "Jenny Smith", 28, "Miami" }
};

Table table = Table.FromDataSet(dataset, f); // Also supports IEnumerable<IEnumerable<T>>

// You may also add rows manually!
table.AddRow(new Row(["Jimmy Jones", null, "Las Vegas"])); // Supports nullables

table.SetColumnColor(1, ConsoleColor.Yellow);
table.EmptyReplacement = "N/A"; // Replacement for null or empty strings
table.SetColumnPadding(1, 5, true);
table.Print();
Console.WriteLine();

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

## Contributing
If you found a bug, have any questions, want to implement your own additions or contribute in any other way, feel free to open a pull request!

## License
This project is licensed under the MIT License
