using System;

namespace NETORM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public string ColumnName { get; private set; }
        public string Comment { get; private set; }
        public ColumnAttribute(string name = "", string comment = "")
        {
            ColumnName = name;
            Comment = comment;
        }
        
    }

 }
