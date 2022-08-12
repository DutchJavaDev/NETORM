using Microsoft.VisualStudio.TestTools.UnitTesting;
using NETORM.Attributes;
using NETORM.Data;

namespace NETORM.ORM.Builder.Tests
{
    [TestClass()]
    public class RecordBuilderTests
    {
        [TestMethod()]
        public async Task CreateRecordTypeFromObject()
        {
            // Arrange
            var type = typeof(Profile);

            // Act
            var record = await RecordBuilder.BuildAsync(new[] { type });

            // Assert
            Assert.IsNotNull(type);
        }

        [TestMethod()]
        public async Task VerifyTableRecord()
        {
            // Arrange
            var type = typeof(Profile);
            var columnCount = type.GetProperties().Count(i => i.PropertyType.IsPrimitive);

            // Act
            var record = await RecordBuilder.BuildAsync(new[] { type });

            // Assert
            var tableAttribute = (TableAttribute)Attribute.GetCustomAttribute(type, typeof(TableAttribute));
            Assert.AreEqual(tableAttribute.TableName, record[0].TableName);
            Assert.AreEqual(columnCount, record[0].ColumnRecords.ToList().Count);
        }

        [TestMethod]
        public async Task VerifyColumRecords() 
        {
            // Arrange
            var type = typeof(Profile);
            var types = type.GetProperties();
            List<ColumnRecord> columns;

            // Act
            var record = await RecordBuilder.BuildAsync(new[] { type });

            // Assert
            columns = record[0].ColumnRecords.ToList();

            for (var i = 0; i < columns.Count; i++)
            {
                var expected = types[i];
                var actuel = columns[i];

                Assert.AreEqual(expected.Name, actuel.Name);
                Assert.AreEqual(expected.PropertyType.Name, actuel.Type);
            }
            
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