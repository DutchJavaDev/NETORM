using NETORM.Attributes;
using NETORM.Data;
using NETORM.Interface;
using NETORM.ORM;
using NETORM.ORM.Builder;
using System.Reflection;
using Npgsql;
using Dapper;
using System.Text;

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

        public static async Task<IORM> CreateConnection(string connectionString, string database = "") 
        {
            _records = await RecordBuilder.BuildAsync(_types);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await CreateDatabase(connection);
                await CreateTables(connection);
            }

            return _CreateConnection(string.IsNullOrEmpty(connectionString) ? _connectionString : connectionString, 
                                    string.IsNullOrEmpty(database) ? _database : database);
        }

        private static async Task CreateTables(NpgsqlConnection connection)
        {
            var builder = new StringBuilder();

            foreach(var table in _records)
            {
                builder.Append($"CREATE TABLE IF NOT EXIST {table.TableName} (");
                //

                //
                builder.Append(");");
            }
        }

        private static async Task CreateDatabase(NpgsqlConnection connection)
        {
            try
            {
                await connection.ExecuteAsync($"CREATE DATABASE {_database}");
            }
            catch (PostgresException e)
            {
                if (!e.Message.Contains("already exists"))
                {
                    throw e;
                }
            }
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
