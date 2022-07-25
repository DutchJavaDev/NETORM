using System;

namespace NETORM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(string name = "")
        {
            TableName = $"{name}";
        }
        public string TableName { get; set; }
    }
}
