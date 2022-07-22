using System;

namespace NETORM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute : Attribute
    {
        public ColumnNameAttribute(string columnName)
        {
            ColumnName = columnName;
        }
        public string ColumnName { get; set; }
    }
 }
