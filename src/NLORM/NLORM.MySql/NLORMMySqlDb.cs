using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NETORM.Core;
using MySql.Data.MySqlClient;

namespace NETORM.MySql
{
    public class NETORMMySqlDb : NETORMBaseDb
    {
        public NETORMMySqlDb(string dbConnectionString)
        {
            DbConnection = new MySqlConnection(dbConnectionString);
            SqlBuilder = new MySqlSqlBuilder();
        }
    }
}
