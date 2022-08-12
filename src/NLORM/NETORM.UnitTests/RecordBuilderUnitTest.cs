using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NETORM.UnitTests
{
    [TestClass]
    public class RecordBuilderUnitTest
    {
        [TestMethod]
        public void VeryfyExtractedRecordTypeVsActualType()
        {
            // prepare


        }
    }


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
}
