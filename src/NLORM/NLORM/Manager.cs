using NETORM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NETORM
{
    public static class Manager
    {
        public static INETORMDb GetDb(string connectionString, SupportedDb dbType)
        {
             return NETORMFactory.Instance.GetDb(connectionString, dbType);
        }
    }
}
