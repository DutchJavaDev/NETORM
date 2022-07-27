using NETORM;
using NETORM.Attributes;

await DatabaseConfig.CreateConnection("");

Console.WriteLine("Done");

[Table("tblprofile")]
public class Profile 
{
    public int Int { get; set; }

    public double Double { get; set; }

    public string @String { get; set; }

    public float Float { get; set; }

    public short Short { get; set; }

    public long Long { get; set; }

    public char Char { get; set; }
    
    public byte Byte { get; set; }

    public decimal Deicimal { get; set; }

    string b = @"Boolean
                    Byte
                    SByte
                    Int16
                    UInt16
                    Int32
                    UInt32
                    Int64
                    UInt64
                    Char
                    Double
                    Single";
}
