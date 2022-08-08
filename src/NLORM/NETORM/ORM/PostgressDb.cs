using NETORM.Data;
using NETORM.Interface;
using Npgsql;
using Dapper;
using System.Linq;

namespace NETORM.ORM
{
    public class PostgressDb : ITransAction
    {
        NpgsqlConnection Connection;

        string ConnectionString { get; set; }

        string Database { get; set; }

        public PostgressDb(string connectionString, string database)
        {
            ConnectionString = connectionString;
            Database = database;
            CreateDatabase();
        }

        private async void CreateDatabase() 
        {
            using (Connection = new NpgsqlConnection(ConnectionString))
            {
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
    }
}
