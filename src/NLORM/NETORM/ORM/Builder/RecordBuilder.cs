using NETORM.Data;
using System.Collections.Concurrent;

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
            Console.WriteLine(type.Name);
            tableRecords.Add(new TableRecord(type.Name, Enumerable.Empty<ColumnRecord>(), new TableConstrain()));
        }
    }
}
