using NETORM.Attributes;
using NETORM.Data;
using NETORM.ORM.Builder;
using System.Reflection;

namespace NETORM
{
    public static class NETORMConfig
    {
        private static string _connectionString { get; set; } = "User ID=root;Password=myPassword;Host=localhost;Port=5432;Database=myDataBase;";

        private static readonly IEnumerable<Type> _types;

        private static IEnumerable<TableRecord> _records;

        static NETORMConfig() 
        {
            _types = GetTableTypes();
            _records = Enumerable.Empty<TableRecord>();
        }

        public static async Task<object> CreateConnection(string connection) 
        {
            _connectionString = connection;

            _records = await RecordBuilder.BuildAsync(_types);

            return new { };
        }

        private static IEnumerable<Type> GetTableTypes()
        {
            var assembly = GetMainAssembly();

            var assemblyTypes = assembly.GetExportedTypes();

            return assemblyTypes.Where(i => Attribute.IsDefined(i, typeof(TableAttribute)));
        }

        private static Assembly GetMainAssembly() 
        {
            var assembly = Assembly.GetEntryAssembly();

            return assembly ?? Assembly.GetExecutingAssembly();
        }
    }
}
