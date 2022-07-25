namespace NETORM.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public string TableName { get; private set; }

        public TableAttribute(string tableName = "") 
        {
            TableName = tableName;
        }
    }
}
