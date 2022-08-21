using NETORM.Data;
using NETORM.Interface;
using Npgsql;
using Dapper;

namespace NETORM.ORM
{
    public class PostgressDb : IORM
    {
        NpgsqlConnection Connection;

        string ConnectionString { get; set; } = string.Empty;

        string Database { get; set; } = string.Empty;

        public PostgressDb(string connectionString, string database)
        {
            ConnectionString = connectionString;
            Database = database;
        }

        private async void CreateDatabase() 
        {
            using (Connection = new NpgsqlConnection(ConnectionString))
            {
                await Connection.OpenAsync();
                var sql = $"CREATE DATABASE {Database}";
                var i = await Connection.ExecuteAsync(sql);
            }
        }

        public async Task CreateTable(TableRecord tableRecord)
        {
            using (Connection = new NpgsqlConnection(ConnectionString))
            {
                var sql = $"CREATE TABLE {tableRecord.TableName}";
                await Connection.QueryAsync<TableRecord>(sql);
            }
        }

        public Task CreatDatabase()
        {
            throw new NotImplementedException();
        }
    }
}
