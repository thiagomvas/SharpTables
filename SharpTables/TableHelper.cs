using SharpTables.Annotations;
using System.Reflection;

namespace SharpTables
{
    internal static class TableHelper
    {
        public static PropertyInfo[] GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetCustomAttribute<TableIgnoreAttribute>() is null)
                .OrderBy(p => GetOrder(p))
                .ToArray();
        }
        // Order by whether or not the TableOrderAttribute is present
        public static int GetOrder(PropertyInfo property)
        {
            var orderAttribute = property.GetCustomAttribute<Annotations.TableOrderAttribute>();
            return orderAttribute?.Order ?? int.MaxValue;
        }

        public static void AddTDataset<T>(Table target, IEnumerable<T> data)
        {
            PropertyInfo[] properties = GetProperties(typeof(T));

            // Add the data to the table
            foreach (T item in data)
            {
                var row = new Row();
                for (int i = 0; i < properties.Length; i++)
                {
                    var value = properties[i].GetValue(item);
                    var cell = new Cell(value)
                    {
                        Alignment = properties[i].GetCustomAttribute<TableAlignmentAttribute>()?.Alignment ?? Alignment.Left,
                        Color = properties[i].GetCustomAttribute<TableColorAttribute>()?.Color ?? ConsoleColor.White
                    };
                    row.Cells.Add(cell);
                }
                target.AddRow(row);
            }
        }

        public static void AddDataSet(Table target, object[,] data)
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                object[] row = new object[data.GetLength(1)];
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    row[j] = data[i, j];
                }
                target.AddRow(new Row(row));
            }
        }
        public static void AddDataSet(Table target, IEnumerable<IEnumerable<object>> data)
        {
            foreach (IEnumerable<object> row in data)
            {
                target.AddRow(new Row(row));
            }
        }

        public static void AddDataSet<T>(Table target, IEnumerable<T> data, Func<T, Row> generatorFunc)
        {
            foreach (T item in data)
            {
                Row row = generatorFunc(item);
                target.AddRow(row);
            }
        }
    }
}
