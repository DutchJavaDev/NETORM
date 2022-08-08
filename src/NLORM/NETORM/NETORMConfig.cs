using NETORM.Attributes;
using NETORM.Data;
using NETORM.Interface;
using NETORM.ORM;
using NETORM.ORM.Builder;
using System.Reflection;

namespace NETORM
{
    public static class NETORMConfig
    {
        private static string _connectionString { get; set; } = "User ID=postgres;Password=P@ssw0rd;Host=localhost;Port=5432;";
        private static string _database = "postgres";
        private static readonly IEnumerable<Type> _types;

        private static IEnumerable<TableRecord> _records;

        static NETORMConfig() 
        {
            _types = GetTableTypes();
            _records = Enumerable.Empty<TableRecord>();
        }

        public static async Task<ITransAction> CreateConnection(string connection) 
        {
            ///_connectionString = connection;

            _records = await RecordBuilder.BuildAsync(_types);

            return new PostgressDb(_connectionString, _database);
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
