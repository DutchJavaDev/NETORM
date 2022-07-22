using System.Data;

namespace NETORM.Core.BasicDefinitions
{
    public class ColumnFieldDefinition
    {
        public string PropName { get; set; }
        public string ColumnName { get; set; }
        public DbType FieldType { get; set; }
        public string Length { get; set; }
        public bool Nullable { get; set; }
        public string Comment { get; set; }
    }
}
