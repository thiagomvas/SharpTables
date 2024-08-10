using SharpTables.Annotations;
using SharpTables.Graph;

namespace SharpTables.Extensions
{
    public static class ConsoleExtensions
    {
        /// <summary>
        /// Writes the specified table to the console.
        /// </summary>
        /// <param name="writer">The text writer.</param>
        /// <param name="table">The table to write.</param>
        public static void Table(this TextWriter writer, Table table)
        {
            table.Write(writer);
        }

        /// <summary>
        /// Writes the specified data as a table to the console.
        /// </summary>
        /// <typeparam name="T">The type of the data.</typeparam>
        /// <param name="writer">The text writer.</param>
        /// <param name="data">The data to write.</param>
        public static void Table<T>(this TextWriter writer, IEnumerable<T> data)
        {
            Table t = SharpTables.Table.FromDataSet(data);
            t.Write(writer);
        }

        /// <summary>
        /// Writes the specified data as a table to the console with the specified formatting.
        /// </summary>
        /// <typeparam name="T">The type of the data.</typeparam>
        /// <param name="writer">The text writer.</param>
        /// <param name="data">The data to write.</param>
        /// <param name="formatting">The table formatting.</param>
        public static void Table<T>(this TextWriter writer, IEnumerable<T> data, TableFormatting formatting)
        {
            Table t = SharpTables.Table.FromDataSet(data)
                .UseFormatting(formatting);
            t.Write(writer);
        }

        /// <summary>
        /// Writes the specified data as a table to the console with the specified settings.
        /// </summary>
        /// <typeparam name="T">The type of the data.</typeparam>
        /// <param name="writer">The text writer.</param>
        /// <param name="data">The data to write.</param>
        /// <param name="settings">The table settings.</param>
        public static void Table<T>(this TextWriter writer, IEnumerable<T> data, TableSettings settings)
        {
            Table t = SharpTables.Table.FromDataSet(data)
                .UseSettings(settings);
            t.Write(writer);
        }

        /// <summary>
        /// Writes the specified data as a table to the console with the specified formatting and settings.
        /// </summary>
        /// <typeparam name="T">The type of the data.</typeparam>
        /// <param name="writer">The text writer.</param>
        /// <param name="data">The data to write.</param>
        /// <param name="formatting">The table formatting.</param>
        /// <param name="settings">The table settings.</param>
        public static void Table<T>(this TextWriter writer, IEnumerable<T> data, TableFormatting formatting, TableSettings settings)
        {
            Table t = SharpTables.Table.FromDataSet(data)
                .UseSettings(settings)
                .UseFormatting(formatting);
            t.Write(writer);
        }

        /// <summary>
        /// Writes the specified data as a table to the console with the specified settings and formatting.
        /// </summary>
        /// <typeparam name="T">The type of the data.</typeparam>
        /// <param name="writer">The text writer.</param>
        /// <param name="data">The data to write.</param>
        /// <param name="settings">The table settings.</param>
        /// <param name="formatting">The table formatting.</param>
        public static void Table<T>(this TextWriter writer, IEnumerable<T> data, TableSettings settings, TableFormatting formatting)
        {
            Table t = SharpTables.Table.FromDataSet(data)
                .UseSettings(settings)
                .UseFormatting(formatting);
            t.Write(writer);
        }

        /// <summary>
        /// Writes the specified data as a graph to the console.
        /// </summary>
        /// <typeparam name="T">The type of the data.</typeparam>
        /// <param name="writer">The text writer.</param>
        /// <param name="data">The data to write.</param>
        public static void Graph<T>(this TextWriter writer, IEnumerable<T> data)
        {
            var g = new Graph<T>(data);
            g.Write();
        }

        /// <summary>
        /// Writes the specified data as a graph to the console with the specified graph type.
        /// </summary>
        /// <typeparam name="T">The type of the data.</typeparam>
        /// <param name="writer">The text writer.</param>
        /// <param name="data">The data to write.</param>
        /// <param name="type">The graph type.</param>
        /// <remarks>This will only work if the class is annotated with <see cref="GraphKeyAttribute"/> and <see cref="GraphValueAttribute"/></remarks>
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

        /// <summary>
        /// Writes the specified data as a graph to the console with the specified graph type and settings.
        /// </summary>
        /// <typeparam name="T">The type of the data.</typeparam>
        /// <param name="writer">The text writer.</param>
        /// <param name="data">The data to write.</param>
        /// <param name="type">The graph type.</param>
        /// <param name="settings">The graph settings.</param>
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

        /// <summary>
        /// Writes the specified data as a graph to the console with the specified graph type and formatting.
        /// </summary>
        /// <typeparam name="T">The type of the data.</typeparam>
        /// <param name="writer">The text writer.</param>
        /// <param name="data">The data to write.</param>
        /// <param name="type">The graph type.</param>
        /// <param name="formatting">The graph formatting.</param>
        /// <remarks>This will only work if the class is annotated with <see cref="GraphKeyAttribute"/> and <see cref="GraphValueAttribute"/></remarks>
        public static void Graph<T>(this TextWriter writer, IEnumerable<T> data, GraphType type, GraphFormatting formatting)
        {
            var g = new Graph<T>(data)
                .UseGraphType(type)
                .UseFormatting(formatting);

            g.Write();
        }

        /// <summary>
        /// Writes the specified data as a graph to the console with the specified graph type, formatting, and settings.
        /// </summary>
        /// <typeparam name="T">The type of the data.</typeparam>
        /// <param name="writer">The text writer.</param>
        /// <param name="data">The data to write.</param>
        /// <param name="type">The graph type.</param>
        /// <param name="formatting">The graph formatting.</param>
        /// <param name="settings">The graph settings.</param>
        public static void Graph<T>(this TextWriter writer, IEnumerable<T> data, GraphType type, GraphFormatting formatting, GraphSettings<T> settings)
        {
            var g = new Graph<T>(data)
                .UseGraphType(type)
                .UseFormatting(formatting)
                .UseSettings(settings);

            g.Write();
        }

        /// <summary>
        /// Writes the specified data as a graph to the console with the specified graph type, settings, and formatting.
        /// </summary>
        /// <typeparam name="T">The type of the data.</typeparam>
        /// <param name="writer">The text writer.</param>
        /// <param name="data">The data to write.</param>
        /// <param name="type">The graph type.</param>
        /// <param name="settings">The graph settings.</param>
        /// <param name="formatting">The graph formatting.</param>
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
