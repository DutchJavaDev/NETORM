﻿using System;
using System.Data;

namespace NETORM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnTypeAttribute : Attribute
    {
        public ColumnTypeAttribute(DbType dbtype,string length,bool nullable,string comment)
        {
            DBType = dbtype;
            Length = length;
            Nullable = nullable;
            Comment = comment;
        }
        public DbType DBType { get; set; }
        public string Length { get; set; }
        public bool Nullable { get; set; }
        public string Comment { get; set; }
    }
}
