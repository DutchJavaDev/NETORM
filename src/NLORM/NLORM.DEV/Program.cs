using NETORM;
using NETORM.Core.Attributes;
using System.Linq;
using System.Data;


using (var db = Manager.GetDb(@"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", SupportedDb.MSSQL)) 
{
    db.Open();

   // db.CreateTable<Item>();

    var item = new Item { ID = Guid.NewGuid().ToString(), Name = "", Email = "" };

    db.Insert<Item>(item);

    Console.WriteLine(db.Query<Item>().FirstOrDefault().ID);
}

Console.ReadKey();

Console.ReadKey();

[TableName("itemTable")]
public class Item
{
    [ColumnName("UID")]
    public string ID { get; set; }

    [ColumnName("UNAME")]
    [ColumnType(DbType.String, "30", false, "User Email")]
    public string Name { get; set; }

    public string Email { get; set; }

    //public DateTime Birth { get; set; }
}