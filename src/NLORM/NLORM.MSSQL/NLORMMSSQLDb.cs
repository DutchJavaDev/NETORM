using NETORM.Core;
using System.Data.SqlClient;

namespace NETORM.MSSQL
{
	public class NETORMMSSQLDb : NETORMBaseDb
	{
		public NETORMMSSQLDb( string connectionString)
		{
            this.DbConnection = new SqlConnection(connectionString);
			this.SqlBuilder = new MSSQLSqlBuilder();
		}
	}
}
