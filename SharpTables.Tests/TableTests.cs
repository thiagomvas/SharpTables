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
		public void Test()
		{
			Assert.Pass();
		}
	}
}