using NETORM.Data;
using System.Collections.Concurrent;
using System.Reflection;

namespace NETORM.ORM.Builder
{
    internal class RecordBuilder
    {
        static readonly ConcurrentBag<TableRecord> tableRecords;

        static RecordBuilder()
        {
            tableRecords = new ConcurrentBag<TableRecord>();
        }

        public static async Task<IList<TableRecord>> BuildAsync(IEnumerable<Type> types) 
        {
            tableRecords.Clear();
            // This is where shit is going to go crazy

            foreach (var type in types)
                await Task.Run(() => CreateRecord(type));

            return tableRecords.ToList();
        }

        static void CreateRecord(Type type)
        {
            var properties = type.GetProperties();
            var attributes = type.GetCustomAttributes(true);
            tableRecords.Add(new TableRecord(type.Name, CreateColumnRecords(properties), new TableConstrain()));
        }

        private static IEnumerable<ColumnRecord> CreateColumnRecords(PropertyInfo[] properties)
        {
            return properties.Where(i => i.PropertyType.IsPrimitive)
                .Select(i => new ColumnRecord(i.Name, i.PropertyType.Name, 0, new ColumnConstraint()))
                .ToList();
        }
    }
}
