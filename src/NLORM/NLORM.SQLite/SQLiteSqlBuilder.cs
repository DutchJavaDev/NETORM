using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using NETORM.Core;
using NETORM.Core.BasicDefinitions;

namespace NETORM.SQLite
{
    public class SQLiteSqlBuilder : BaseSqlBuilder 
    {

        public override ISqlBuilder CreateOne()
        {
            return new SQLiteSqlBuilder();
        }
    }
}
