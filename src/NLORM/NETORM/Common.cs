using System.Reflection;

namespace NETORM
{
    internal class Common
    {
        public static IList<PropertyInfo> GetValidProperties(IEnumerable<PropertyInfo> properties)
        {
            return properties.Where(i => i != null).ToList();
        }
    }
}
