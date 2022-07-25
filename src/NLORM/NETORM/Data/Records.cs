namespace NETORM.Data
{
    public record struct TableRecord(string TableName, IEnumerable<ColumnRecord> ColumnRecords, TableConstrain TableConstrain);

    public record struct ColumnRecord(string Type, int Lenght, ColumnConstraint ColumnConstraint);

    public record struct ColumnConstraint();

    public record struct TableConstrain();
}
