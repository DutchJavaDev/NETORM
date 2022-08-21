using NETORM;
using NETORM.Attributes;

var connectionString = "User ID=postgres;Password=P@ssw0rd!;Host=localhost;Port=5432;";

await NETORMConfig.CreateConnection(connectionString);

Console.WriteLine("Wating for exit key");
Console.ReadKey();

[Table("tblprofile")]
public class Profile 
{
    public int Int { get; set; }

    public double Double { get; set; }

    public float Float { get; set; }

    public short Short { get; set; }

    public long Long { get; set; }

    public char Char { get; set; }
    
    public byte Byte { get; set; }
}
