using SharpTables.Annotations;

namespace SharpTables.Examples
{
    public class Order
    {
        [TableDisplayName("Order ID")]
        public Guid Id { get; set; }

        [TableAlignment(Alignment.Center)]
        public string Item { get; set; }
        public string? Producer { get; set; }

        public int Quantity { get; set; }

        [TableColor(ConsoleColor.Yellow)]
        public double Price { get; set; }

        [TableIgnore]
        public double IgnoredProperty { get; set; } = 69;
    }
}
