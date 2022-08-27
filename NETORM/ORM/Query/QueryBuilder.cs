namespace NETORM.ORM.Query
{
    public static class QueryBuilder
    {
        private static readonly Dictionary<Type, string> _supportedTypes = new Dictionary<Type, string> 
        {
            {typeof(int), "integer" },
            {typeof(double), "double precision" },
            {typeof(float), "real" },
            {typeof(short), "smallint" },
            {typeof(long), "bigint" },
            {typeof(char), "char" },
            {typeof(byte), "char" },
            {typeof(bool), "boolean" }
        };

        public static string GetDbType(Type type)
        {
            if (_supportedTypes.TryGetValue(type, out var sqlDbType))
            {
                return sqlDbType;
            }

            return string.Empty;
        }
    }
}
