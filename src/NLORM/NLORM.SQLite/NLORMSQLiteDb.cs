using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using NETORM.Core;

namespace NETORM.SQLite
{
    public class NETORMSQLiteDb : NETORMBaseDb
    {
        public NETORMSQLiteDb(string connectionString)
        {
            DbConnection = new SQLiteConnection(connectionString);
            SqlBuilder = new SQLiteSqlBuilder();
        }
    }
}
