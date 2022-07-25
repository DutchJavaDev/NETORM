using NETORM.Attributes;
using NETORM.Data;
using NETORM.ORM.Builder;
using System.Reflection;

namespace NETORM
{
    public static class DatabaseConfig
    {
        static string _connectionString { get; set; } = "User ID=root;Password=myPassword;Host=localhost;Port=5432;Database=myDataBase;";

        static IList<Type> _types;

        static IEnumerable<TableRecord> _records;

        static DatabaseConfig() 
        {
            _types = GetTableTypes();
            _records = Enumerable.Empty<TableRecord>();
        }

        public static async Task<object> CreateConnection(string connection) 
        {
            _connectionString = connection;

            if(_types.Count > 0)
                _records = await RecordBuilder.BuildAsync(_types);

            return new { };
        }

        static IList<Type> GetTableTypes()
        {
            var assembly = GetMainAssembly();

            var assemblyTypes = assembly.GetExportedTypes();

            return assemblyTypes.Where(i => i.CustomAttributes.Any(i => i.AttributeType.Name == nameof(TableAttribute))).ToList();
        }

        static Assembly GetMainAssembly() 
        {
            var assembly = Assembly.GetEntryAssembly();

            return assembly ?? Assembly.GetExecutingAssembly();
        }
    }
}
