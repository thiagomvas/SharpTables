namespace SharpTables.Tests
{
	[TestFixture]
	public class Tests
	{
		private Table table;
		private object[,] data;
		[SetUp]
		public void Setup()
		{
			Formatting f = Formatting.Minimalist;
			data = new object[,]
			{
				{ "Name", "Age", "City" },
				{ "John Doe", 42, "New York" },
				{ "Jane Doe", 36, "Chicago" },
				{ "Joe Bloggs", 25, "Los Angeles" },
				{ "Jenny Smith", 28, "Miami" }
			};
			table = Table.FromDataSet(data);
		}
		[Test]
		public void NumberAlignment_ShouldOnlyChangeNumbers()
		{
			table.NumberAlignment = Alignment.Right;
			var tablestr = table.ToString();

			// Find where 42 is on the string
			int index = tablestr.IndexOf("42");

			// Check if the number only has padding to the left
			Assert.That(tablestr[index - 1], Is.EqualTo(' '));

			// Check if text has no padding
			int textIndex = tablestr.IndexOf("John Doe");
			Assert.That(tablestr[textIndex - 1], Is.Not.EqualTo(' '));


			table.NumberAlignment = Alignment.Left;
			
			tablestr = table.ToString();
			index = tablestr.IndexOf("42");

			// Check if the number only has padding to the right
			Assert.That(tablestr[index + 2], Is.EqualTo(' '));

			table.NumberAlignment = Alignment.Center;
			tablestr = table.ToString();
			index = tablestr.IndexOf("42");

			// Check if the number has padding to the left and right
			Assert.That(tablestr[index - 1], Is.EqualTo(' '));
			Assert.That(tablestr[index + 2], Is.EqualTo(' '));
			// Check if text has no padding
			textIndex = tablestr.IndexOf("John Doe");
			Assert.That(tablestr[textIndex - 1], Is.Not.EqualTo(' '));
		}

		[Test]
		public void TextAlignment_ShouldOnlyChangeText()
		{
			table.TextAlignment = Alignment.Right;
			var tablestr = table.ToString();

			// Find where "John Doe" is on the string
			int index = tablestr.IndexOf("John Doe");

			// Check if the text only has padding to the left
			Assert.That(tablestr[index - 1], Is.EqualTo(' '));

			// Check if number has no padding
			int numberIndex = tablestr.IndexOf("42");
			Assert.That(tablestr[numberIndex - 1], Is.Not.EqualTo(' '));
		}

		[Test]
		public void SetColumnColor_ShouldChangeColor()
		{
			table.SetColumnColor(1, ConsoleColor.Yellow);

			Assert.That(table.Rows[0].Cells[1].Color, Is.Not.EqualTo(ConsoleColor.Yellow));

			// Check if the color of the cells in the column is yellow
			foreach (var row in table.Rows.Skip(1))
			{
				Assert.That(row.Cells[1].Color, Is.EqualTo(ConsoleColor.Yellow));
			}
		}

		[Test]
		public void SetColumnColor_WhenChangeHeaderColor_ShouldChangeHeaderColor()
		{
			table.SetColumnColor(1, ConsoleColor.Yellow, true);

			Assert.That(table.Rows[0].Cells[1].Color, Is.EqualTo(ConsoleColor.Yellow));
		}

		[Test]
		public void SetColumnPadding_ShouldChangePadding()
		{

			table.SetColumnPadding(1, 10);

			Assert.That(table.Rows[0].Cells[1].Padding, Is.Not.EqualTo(10));

			// Check if the color of the cells in the column is yellow
			foreach (var row in table.Rows.Skip(1))
			{
				Assert.That(row.Cells[1].Padding, Is.EqualTo(10));
			}
		}

		public void SetColumnPadding_WhenChangeHeaderPadding_ShouldChangeHeaderPadding()
		{
			table.SetColumnPadding(1, 10, true);

			Assert.That(table.Rows[0].Cells[1].Padding, Is.EqualTo(10));
		}

		[Test]
		public void FromDataSet2DArray_ShouldGenerateProperly()
		{
			for(int y = 0; y < data.GetLength(0); y++)
			{
				for(int x = 0; x < data.GetLength(1); x++)
				{
					Assert.That(table.Rows[y].Cells[x].Text, Is.EqualTo(data[y, x].ToString()));
				}
			}
		}

		[Test]
		public void FromDataSetIEnumerable_ShouldGenerateProperly()
		{
			var list = new List<object[]>
			{
				new object[] { "Name", "Age", "City" },
				new object[] { "John Doe", 42, "New York" },
				new object[] { "Jane Doe", 36, "Chicago" },
				new object[] { "Joe Bloggs", 25, "Los Angeles" },
				new object[] { "Jenny Smith", 28, "Miami" }
			};

			table = Table.FromDataSet(list);
			for(int y = 0; y < list.Count; y++)
			{
				for(int x = 0; x < list[y].Length; x++)
				{
					Assert.That(table.Rows[y].Cells[x].Text, Is.EqualTo(list[y][x].ToString()));
				}
			}
		}

		[Test]
		public void FromDataSet_WhenElementIsNull_ShouldReplaceWithEmptyString()
		{
			object[,] d = new object[,]
			{
				{ "Name", "Age", "City" },
				{ null, 42, "New York" },
			};

			var t = Table.FromDataSet(d);
			Assert.That(t.Rows[1].Cells[0].Text, Is.EqualTo(string.Empty));
		}
	}
}