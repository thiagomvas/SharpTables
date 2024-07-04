using SharpTables.Annotations;

namespace SharpTables.Tests
{
    public class TestType
    {
        public int PublicProp { get; set; }
        public string PublicField = "Field!";
        public static string PublicStaticField = "Static!";
        public static string PublicStaticProp { get; set; } = "Static Prop!";

        private int PrivateProp { get; set; }
        private string PrivateField;
        private static string PrivateStaticProp { get; set; }
        private static string PrivateStaticField;

        internal int InternalProp { get; set; }
        internal string InternalField;
        internal static string InternalStaticProp { get; set; }
        internal static string InternalStaticField;

        [TableIgnore]
        public int IgnoredProp { get; set; }

        [TableOrder(0)]
        public int OrderedProp { get; set; }

        [TableOrder(1)]
        public int OrderedProp2 { get; set; }

        [TableDisplayName("RenamedProp")]
        public int NamedProp { get; set; }

        [TableOrder(2)]
        [TableColor(ConsoleColor.Blue)]
        public int ColoredProp { get; set; }

        [TableAlignment(Alignment.Center)]
        public int AlignedProp { get; set; }

    }
}
