using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace NETORM.Core.Test
{
    /// <summary>
    /// Summary description for TestException
    /// </summary>
    [TestClass]
    public class TestException
    {
        public TestException()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void TestDBTypeGuidGen()
        {
            var cfd = createFakeCfdByDbType(DbType.Guid);
            var cfdDic = new Dictionary<string, BasicDefinitions.ColumnDefinition>();
            cfdDic.Add("test1", cfd);
            var md = new NETORM.Core.BasicDefinitions.ObjectDefinition("Test", cfdDic);
            var sqlGen = new BaseSqlGenerator();
            try
            {
                sqlGen.GenCreateTableSql(md);
            }
            catch (NETORM.Core.Exceptions.NETORMException nle)
            {
                Assert.AreEqual(nle.ErrorCode, "SG");
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestDBTypeUInt16Gen()
        {
            var cfd = createFakeCfdByDbType(DbType.UInt16);
            var cfdDic = new Dictionary<string, BasicDefinitions.ColumnDefinition>();
            cfdDic.Add("test1", cfd);
            var md = new NETORM.Core.BasicDefinitions.ObjectDefinition("Test", cfdDic);
            var sqlGen = new BaseSqlGenerator();
            try
            {
                sqlGen.GenCreateTableSql(md);
            }
            catch (NETORM.Core.Exceptions.NETORMException nle)
            {
                Assert.AreEqual(nle.ErrorCode, "SG");
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        private BasicDefinitions.ColumnDefinition createFakeCfdByDbType(DbType dbType)
        {
            var cfd = new NETORM.Core.BasicDefinitions.ColumnDefinition();
            cfd.FieldType = dbType;
            return cfd;
        }
    }
}
