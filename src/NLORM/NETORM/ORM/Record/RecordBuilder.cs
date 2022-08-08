using NETORM.Attributes;
using NETORM.Data;
using System.Reflection;

namespace NETORM.ORM.Builder
{
    internal class RecordBuilder
    {
        private static readonly IList<TableRecord> tableRecords;

        static RecordBuilder()
        {
            tableRecords = new List<TableRecord>();
        }

        public static async Task<IList<TableRecord>> BuildAsync(IEnumerable<Type> types) 
        {
            tableRecords.Clear();

            foreach (var type in types)
                await Task.Run(() => CreateRecord(type));

            return tableRecords;
        }

        private static void CreateRecord(Type type)
        {
            var properties = Common.GetValidProperties(type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty));

            tableRecords.Add(new TableRecord(GetTableName(type), CreateColumnRecords(properties), new TableConstrain()));
        }

        private static IEnumerable<ColumnRecord> CreateColumnRecords(IEnumerable<PropertyInfo> properties)
        {
            return properties.Select(i => new ColumnRecord(i.Name, i.PropertyType.Name, 0, new ColumnConstraint()));
        }

        private static string GetTableName(Type type) 
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return (Attribute.GetCustomAttribute(type, typeof(TableAttribute)) as TableAttribute).TableName;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}
