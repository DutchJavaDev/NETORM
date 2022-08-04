using System.Reflection;

namespace NETORM
{
    internal class Common
    {

        public static IEnumerable<PropertyInfo> GetValidProperties(IEnumerable<PropertyInfo> properties)
        {
            return properties.Where(i => i.PropertyType.IsPrimitive);
        }

        public static string GetDatabaseType(string netType) 
        {
            switch (netType)
            {
                case "Int64": return "BIGINT";
                case "Int32": return "mysql";
                default: throw new ArgumentNullException(nameof(netType));
            }
        }
    }
}
