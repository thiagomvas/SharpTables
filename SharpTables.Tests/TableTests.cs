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