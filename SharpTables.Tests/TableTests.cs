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
        public void TableFromT_ShouldOnlyIncludePublicInstanceProps()
        {
            // Arrange
            TestType[] data = [
                new()
            ];

            // Act
            Table table = Table.FromDataSet(data);
            var headers = table.Header.Cells.Select(c => c.Text);

            // Assert
            Assert.That(headers, Has.Member(nameof(TestType.PublicProp)));
            Assert.That(headers, Has.No.Member(nameof(TestType.PublicField)));
            Assert.That(headers, Has.No.Member(nameof(TestType.PublicStaticField)));
            Assert.That(headers, Has.No.Member(nameof(TestType.PublicStaticProp)));
            Assert.That(headers, Has.No.Member(nameof(TestType.InternalField)));
            Assert.That(headers, Has.No.Member(nameof(TestType.InternalProp)));
            Assert.That(headers, Has.No.Member(nameof(TestType.InternalStaticField)));
            Assert.That(headers, Has.No.Member(nameof(TestType.InternalStaticProp)));
            Assert.That(headers, Has.No.Member("PrivateField"));
            Assert.That(headers, Has.No.Member("PrivateProp"));
            Assert.That(headers, Has.No.Member("PrivateStaticField"));
            Assert.That(headers, Has.No.Member("PrivateStaticProp"));
        }

        [Test]
        public void IgnoreAttribute_ShouldIgnoreProperty()
        {
            // Arrange
            TestType[] data = new TestType[]
            {
                new TestType { IgnoredProp = 1 }
            };

            Table table = Table.FromDataSet(data);

            // Act
            var headers = table.Header.Cells.Select(c => c.Text);

            // Assert
            Assert.That(headers, Has.No.Member(nameof(TestType.IgnoredProp)));
        }

        [Test]
        public void OrderAttribute_ShouldOrderProperties()
        {
            // Arrange
            TestType[] data = new TestType[]
            {
                new TestType { OrderedProp = 1, OrderedProp2 = 2 }
            };

            Table table = Table.FromDataSet(data);

            // Act
            var headers = table.Header.Cells.Select(c => c.Text).ToArray();

            // Assert
            Assert.That(headers[0], Is.EqualTo(nameof(TestType.OrderedProp)));
            Assert.That(headers[1], Is.EqualTo(nameof(TestType.OrderedProp2)));
        }

        [Test]
        public void DisplayNameAttribute_ShouldRenameProperty()
        {
            // Arrange
            TestType[] data = new TestType[]
            {
                new TestType { NamedProp = 1 }
            };

            Table table = Table.FromDataSet(data);

            // Act
            var headers = table.Header.Cells.Select(c => c.Text).ToArray();

            bool containsNamedProp = headers.Contains(nameof(TestType.NamedProp));
            bool containsRenamedProp = headers.Contains("RenamedProp");

            // Assert
            Assert.That(containsNamedProp, Is.False, "Named prop is included");
            Assert.That(containsRenamedProp, Is.True, "Renamed prop is not included");
        }

        [Test]
        public void ColorAttribute_ShouldColorProperty()
        {
            // Arrange
            TestType[] data = new TestType[]
            {
                new TestType { ColoredProp = 1 }
            };

            Table table = Table.FromDataSet(data);
            int coloredIndex = table.Header.Cells.ToList().FindIndex(c => c.Text == nameof(TestType.ColoredProp));

            // Act
            var coloredCell = table.Rows[0].Cells[coloredIndex];

            // Assert
            Assert.That(coloredCell.Color, Is.EqualTo(ConsoleColor.Blue));
        }

        [Test]
        public void AlignmentAttribute_ShouldAlignProperty()
        {
            // Arrange
            TestType[] data = new TestType[]
            {
                new TestType { AlignedProp = 1 }
            };

            Table table = Table.FromDataSet(data);
            int alignedIndex = table.Header.Cells.ToList().FindIndex(c => c.Text == nameof(TestType.AlignedProp));

            // Act
            var alignedCell = table.Rows[0].Cells[alignedIndex];

            // Assert
            Assert.That(alignedCell.Alignment, Is.EqualTo(Alignment.Center));
        }
    }
}