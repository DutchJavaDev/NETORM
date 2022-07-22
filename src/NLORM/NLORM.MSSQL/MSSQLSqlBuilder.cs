using NETORM.Core;

namespace NETORM.MSSQL
{
    public class MSSQLSqlBuilder : BaseSqlBuilder
    {
        public override ISqlBuilder CreateOne()
        {
            return new MSSQLSqlBuilder();
        }
    }
}
