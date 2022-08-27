namespace NETORM.Data
{
    public record struct TableRecord(string TableName, List<ColumnRecord> ColumnRecords, TableConstrain TableConstrain);

    public record struct ColumnRecord(string Name, string Type, ColumnConstraint ColumnConstraint);

    public record struct ColumnConstraint();

    public record struct TableConstrain();
}
