using System.Xml;

namespace SharpTables.Extensions
{
    public static class ConsoleExtensions
    {
        public static void Table(this TextWriter writer, Table table)
        {
            table.Write(writer);
        }

        public static void Table<T>(this TextWriter writer, IEnumerable<T> data)
        {
            Table t = SharpTables.Table.FromDataSet(data);
            t.Write(writer);
        }

        public static void Table<T>(this TextWriter writer, IEnumerable<T> data, TableFormatting formatting)
        {
            Table t = SharpTables.Table.FromDataSet(data)
                .UseFormatting(formatting);
            t.Write(writer);
        }

        public static void Table<T>(this TextWriter writer, IEnumerable<T> data, TableSettings settings)
        {
            Table t = SharpTables.Table.FromDataSet(data)
                .UseSettings(settings);
            t.Write(writer);
        }


        public static void Table<T>(this TextWriter writer, IEnumerable<T> data, TableFormatting formatting, TableSettings settings)
        {
            Table t = SharpTables.Table.FromDataSet(data)
                .UseSettings(settings)
                .UseFormatting(formatting);
            t.Write(writer);
        }

        public static void Table<T>(this TextWriter writer, IEnumerable<T> data, TableSettings settings, TableFormatting formatting)
        {
            Table t = SharpTables.Table.FromDataSet(data)
                .UseSettings(settings)
                .UseFormatting(formatting);
            t.Write(writer);
        }
    }
}
