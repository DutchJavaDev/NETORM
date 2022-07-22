using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NETORM.Core;

namespace NETORM.MySql
{
    public class MySqlSqlBuilder : BaseSqlBuilder
    {
        public MySqlSqlBuilder()
        {
            SqlGen = new MySqlSqlGenerator();
        }

        public override ISqlBuilder CreateOne()
        {
            return new MySqlSqlBuilder();
        }
    }
}
