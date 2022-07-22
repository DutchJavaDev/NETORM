using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NETORM.Core;

namespace NETORM.SQLite.Test
{
    [TestClass]
    public class TestDelete
    {
        string connectionString;
        private static string filePath;
        public TestDelete()
        {
            filePath = "C:\\test.sqlite";
            connectionString = "Data Source=" + filePath;
        }

        class TestClassOne
        {
            public string Id { get; set;}

            public int income { get; set;}
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            try
            {
                var db = new NETORMSQLiteDb(connectionString);
                this.createtable( db);
                this.insertdata( db);
            }
            finally
            {

            }
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            try
            {
                File.Delete(filePath);
            }
            finally
            {

            }
        }

        private void createtable( INETORMDb db)
        {
            db.CreateTable<TestClassOne>();
        }
        private void insertdata( INETORMDb db)
        {
            db.Insert<TestClassOne>( new TestClassOne(){ Id = "sssss", income = 123456} );
            db.Insert<TestClassOne>( new TestClassOne(){ Id = "rrrrr", income = 789012} );
            db.Insert<TestClassOne>( new TestClassOne(){ Id = "fffff", income = 345678} );
            db.Insert<TestClassOne>( new TestClassOne(){ Id = "lllll", income = 901234} );
            db.Insert<TestClassOne>( new TestClassOne(){ Id = "alber", income = 901234} );
        }

        [TestMethod]
        public void TestDeleteOneRecord()
        {
            var db = new NETORMSQLiteDb( connectionString);
            int i =  db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss"}).Delete<TestClassOne>();
            var items = db.FilterBy( FilterType.EQUAL_AND, new { Id = "sssss"}).Query<TestClassOne>();
            Assert.AreEqual( 0, items.Count() );
        }

        [TestMethod]
        public void TestDeleteTwoRecord()
        {
            var db = new NETORMSQLiteDb( connectionString);
            db.FilterBy(FilterType.EQUAL_AND, new { income = 901234}).Delete<TestClassOne>();
            var items = db.Query<TestClassOne>();
            Assert.AreEqual( 3, items.Count() );
        }

        [TestMethod]
        public void TestDeleteAllRecords()
        {
            var db = new NETORMSQLiteDb(connectionString);
            int totalcount = db.Query<TestClassOne>().Count();
            int dc = db.Delete<TestClassOne>();

            var items = db.Query<TestClassOne>();
            Assert.AreEqual(dc, totalcount);
        }
    }
}
