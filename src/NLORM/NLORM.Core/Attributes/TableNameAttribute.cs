using System;

namespace NETORM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : Attribute
    {
        public TableNameAttribute(string tableName)
        {
            TableName = $"{tableName}";
        }
        public string TableName { get; set; }
    }
}
