using SharpTables.Graph;
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

        public static void Graph<T>(this TextWriter writer, IEnumerable<T> data)
        {
            var g = new Graph<T>(data);
            g.Write();
        }

        public static void Graph<T>(this TextWriter writer, IEnumerable<T> data, GraphType type)
        {
            var g = new Graph<T>(data)
                .UseGraphType(type);
            if (type == GraphType.Pie)
            {
                g.UseFormatting(new PieGraphFormatting());
            }
            
            g.Write();
            
        }

        public static void Graph<T>(this TextWriter writer, IEnumerable<T> data, GraphType type, GraphSettings<T> settings)
        {
            var g = new Graph<T>(data)
                .UseGraphType(type)
                .UseSettings(settings);
            if (type == GraphType.Pie)
            {
                g.UseFormatting(new PieGraphFormatting());
            }
            
            g.Write();
        }

        public static void Graph<T>(this TextWriter writer, IEnumerable<T> data, GraphType type, GraphFormatting formatting)
        {
            var g = new Graph<T>(data)
                .UseGraphType(type)
                .UseFormatting(formatting);

            g.Write();
        }

        public static void Graph<T>(this TextWriter writer, IEnumerable<T> data, GraphType type, GraphFormatting formatting, GraphSettings<T> settings)
        {
            var g = new Graph<T>(data)
                .UseGraphType(type)
                .UseFormatting(formatting)
                .UseSettings(settings);

            g.Write();
        }

        public static void Graph<T>(this TextWriter writer, IEnumerable<T> data, GraphType type, GraphSettings<T> settings, GraphFormatting formatting)
        {
            var g = new Graph<T>(data)
                .UseGraphType(type)
                .UseFormatting(formatting)
                .UseSettings(settings);

            g.Write();
        }

    }
}
