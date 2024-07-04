using SharpTables.Annotations;

namespace SharpTables.Examples
{
    public class Order
    {
        [TableDisplayName("Order ID")]
        public Guid Id { get; set; }
        public string Item { get; set; }
        public string? Producer { get; set; }

        [TableColor(ConsoleColor.Blue)]
        public int Quantity { get; set; }

        [TableColor(ConsoleColor.Yellow)]
        public double Price { get; set; }
    }
}
