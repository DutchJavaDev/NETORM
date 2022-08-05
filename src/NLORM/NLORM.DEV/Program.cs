using NETORM;
using NETORM.Attributes;

await NETORMConfig.CreateConnection("");

Console.WriteLine("Done");

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
