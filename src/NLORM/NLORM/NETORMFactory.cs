using NETORM.Core;
using NETORM.Core.Exceptions;
using NETORM.MSSQL;
using NETORM.MySql;
using NETORM.SQLite;

namespace NETORM
{
    internal class NETORMFactory
    {
        private static NETORMFactory instance;

        private NETORMFactory() { }

        public static NETORMFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NETORMFactory();
                }
                return instance;
            }
        }

        public INETORMDb GetDb(string ConnectString, SupportedDb dbType)
        {
            switch (dbType)
            {
                case SupportedDb.MSSQL:
                    return new NETORMMSSQLDb(ConnectString);
                case SupportedDb.SQLITE:
                    return new NETORMSQLiteDb(ConnectString);
                case SupportedDb.MYSQL:
                    return new NETORMMySqlDb(ConnectString);
                default:
                    throw new NETORMException("N","Not SupportDB");
            }
        }
    }
}
