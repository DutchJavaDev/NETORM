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
        private static string _connectionString { get; set; } = "User ID=postgres;Password=P@ssw0rd!;Host=localhost;Port=5432;";
        private static string _database = "postgres";
        private static readonly IEnumerable<Type> _types;

        private static IEnumerable<TableRecord> _records;

        static NETORMConfig() 
        {
            _types = GetTableTypes();
            _records = Enumerable.Empty<TableRecord>();
        }

        public static async Task<IORM> CreateConnection(string connection, string database = "") 
        {
            _records = await RecordBuilder.BuildAsync(_types);

            return _CreateConnection(string.IsNullOrEmpty(connection) ? _connectionString : connection, 
                                    string.IsNullOrEmpty(database) ? _database : database);
        }

        private static IORM _CreateConnection(string connection, string database)
        {

            /// Later-ish give type of db>/????
            /// // postgress, mysql, sqlserver ... mongodb
            return new PostgressDb(connection, database);
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
