using System.Collections.Generic;

namespace NETORM.Core.BasicDefinitions
{
    public struct ObjectDefinition
    {
        public readonly string TableName { get; private set; }
        public readonly Dictionary<string, ColumnDefinition> PropertyColumnDic { get; private set; }
        public ObjectDefinition (string tablename, Dictionary<string, ColumnDefinition> propertycolumnDic)
        {
            TableName = tablename;
            PropertyColumnDic = propertycolumnDic;
        }
        
    }
}
