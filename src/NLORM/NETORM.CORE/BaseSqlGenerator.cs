using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using NETORM.Core.BasicDefinitions;
using System.Dynamic;
using System.Collections.Generic;

namespace NETORM.Core
{
    public class BaseSqlGenerator : ISqlGenerator
    {
        private const string StringDeafultLength = "255";
        private Dictionary<DbType, Func<ColumnDefinition,string>> createSqlFuncDic = null;

        public string GenCreateTableSql(ObjectDefinition md)
        {
            var ret = new StringBuilder();
            ret.Append("CREATE TABLE " + md.TableName + " (");
            var i = 1;
            foreach (var cfd in md.PropertyColumnDic.Values)
            {
                ret.Append(GenColumnCreateTableSql(cfd));
                if (i != md.PropertyColumnDic.Values.Count)
                {
                    ret.Append(" ,");
                }
                i++;
            }
            ret.Append(")");
            return ret.ToString();
        }

        virtual public string GenTableExistSql(ObjectDefinition md)
        {
            return $@"IF OBJECT_ID('{md.TableName}','U')";
        }

        virtual public string GenCreateString(ColumnDefinition cfd)
        {
            var length = string.IsNullOrEmpty(cfd.Length) ? StringDeafultLength : cfd.Length;
            return GenCreateSqlByType(cfd, "varchar", length);
        }

        virtual public string GenCreateInteger(ColumnDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "INTEGER");
        }

        virtual public string GenCreateDateTime(ColumnDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "DATETIME");
        }

        virtual public string GenCreateDecimal(ColumnDefinition cfd)
        {
            var length = string.IsNullOrEmpty(cfd.Length) ? StringDeafultLength : cfd.Length;
            return GenCreateSqlByType(cfd, "decimal", length);
        }

        virtual public string GenCreateBit(ColumnDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "bit", null);
        }

        virtual public string GenCreateFloat(ColumnDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "float", null);
        }

        virtual public string GenCreateReal(ColumnDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "real", null);
        }

        virtual public string GenCreateTime(ColumnDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "time", null);
        }

        virtual public string GenCreateTinyint(ColumnDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "tinyint", null);
        }

        virtual public string GenCreateBigint(ColumnDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "bigint", null);
        }

        private string GenCreateSqlByType(ColumnDefinition cfd, string type, string length = "")
        {
            var ret = "";
            ret += " " + cfd.ColumnName + " ";
            var nullable = cfd.Nullable ? "" : "not null";
            if (string.IsNullOrEmpty(length))
            {
                ret += type + " " + nullable;
            }
            else
            {
                ret += type + "(" + length + ") " + nullable;
            }
            return ret;
        }

        virtual public string GenDropTableSql(ObjectDefinition md)
        {
            var ret = new StringBuilder();
            ret.Append(" DROP TABLE ");
            ret.Append(md.TableName);
            return ret.ToString();
        }

        virtual public string GenInsertSql(ObjectDefinition md)
        {
            var ret = new StringBuilder();

            var fields = new StringBuilder();
            var valueFields = new StringBuilder();
            fields.Append(GenInsertColFields(md));
            valueFields.Append(GenInsertValueFields(md));
            ret.Append(" INSERT INTO ");
            ret.Append(md.TableName + "( ");
            ret.Append(fields);
            ret.Append(" ) VALUES ( ");
            ret.Append(valueFields);
            ret.Append(") ");
            return ret.ToString();
        }

        virtual public string GenSelectSql(ObjectDefinition md)
        {
            var ret = new StringBuilder();
            ret.Append(" SELECT ");
            int i = 1;
            foreach (var cdf in md.PropertyColumnDic.Values)
            {
                ret.Append(cdf.ColumnName + " " + cdf.PropName);
                if (i < md.PropertyColumnDic.Count)
                {
                    ret.Append(" , ");
                    i++;
                }
            }
            ret.Append(" FROM " + md.TableName + " ");
            return ret.ToString();
        }

        virtual public string GenDeleteSql(ObjectDefinition md)
        {
            var ret = new StringBuilder();
            ret.Append(" DELETE ");
            ret.Append(" FROM " + md.TableName + " ");
            return ret.ToString();
        }

        public string GenUpdateSql(ObjectDefinition md, Object obj)
        {
            var ret = new StringBuilder();

            var fields = new StringBuilder();
            var valueFields = new StringBuilder();
            fields.Append(GenInsertColFields(md));
            valueFields.Append(GenInsertValueFields(md));
            ret.Append(" UPDATE ");
            ret.Append(md.TableName + " SET ");
            var type = obj.GetType();
            Utility.IPropertyGetter pg;
            if (type.Equals(typeof(ExpandoObject)))
            {
                var expando = obj as ExpandoObject;
                ret.Append(GenExpandoUpdateParaString(expando));
            }
            else
            {
                ret.Append(GenNormalUpdateParaString(obj));
            }
            return ret.ToString();
        }

        private string GenExpandoUpdateParaString(ExpandoObject obj)
        {
            var ret = new StringBuilder();
            var i = 1;
            var pg = new Utility.ExpandoPorpertyGetter();
            var paraDic = pg.GetPropertyDic(obj);
            foreach (var key in paraDic.Keys)
            {
                ret.Append(" " + key + "=@" + key + " ");
                if (i < paraDic.Keys.Count)
                {
                    ret.Append(" , ");
                    i++;
                }
            }
            return ret.ToString();
        }

        private string GenNormalUpdateParaString(object obj)
        {
            var ret = new StringBuilder();
            var objMdf = new ObjectDefinitionConverter().ConverClassToModelDefinition(obj.GetType());
            int i = 1;
            foreach (var df in objMdf.PropertyColumnDic.Values)
            {
                ret.Append(" " + df.ColumnName + "=@" + df.PropName + " ");
                if (i < objMdf.PropertyColumnDic.Values.Count)
                {
                    ret.Append(" , ");
                    i++;
                }
            }
            return ret.ToString();
        }

        private string GenInsertColFields(ObjectDefinition md)
        {
            var ret = "";
            var i = 1;
            foreach (var mdf in md.PropertyColumnDic.Values)
            {
                ret += " " + mdf.ColumnName;
                if (i < md.PropertyColumnDic.Values.Count)
                {
                    ret += " ,";
                }
                i++;
            }
            return ret;
        }

        private string GenInsertValueFields(ObjectDefinition md)
        {
            var ret = "";
            var i = 1;
            foreach (var mdf in md.PropertyColumnDic.Values)
            {
                ret += " @" + mdf.PropName;
                if (i < md.PropertyColumnDic.Values.Count)
                {
                    ret += " ,";
                }
                i++;
            }
            return ret;
        }

        private string GenColumnCreateTableSql(ColumnDefinition cfd)
        {
            GenCreateSqlFuncDic();
            if (!createSqlFuncDic.ContainsKey(cfd.FieldType))
            {
                throw new NETORM.Core.Exceptions.NETORMException("SG","NOT SUPPORT DBTYPE");
            }
            return createSqlFuncDic[cfd.FieldType](cfd);
        }

        private void GenCreateSqlFuncDic()
        {
            if (createSqlFuncDic != null)
            {
                return;
            }
            createSqlFuncDic = new Dictionary<DbType, Func<ColumnDefinition, string>>();
            createSqlFuncDic.Add(DbType.Byte, GenCreateTinyint);
            createSqlFuncDic.Add(DbType.Int16, GenCreateInteger);  //smallInt
            createSqlFuncDic.Add(DbType.Int32, GenCreateInteger);
            createSqlFuncDic.Add(DbType.Int64, GenCreateBigint);
            createSqlFuncDic.Add(DbType.Single, GenCreateReal);
            createSqlFuncDic.Add(DbType.Double, GenCreateFloat);
            createSqlFuncDic.Add(DbType.Decimal, GenCreateDecimal);
            createSqlFuncDic.Add(DbType.Boolean, GenCreateBit);
            createSqlFuncDic.Add(DbType.String, GenCreateString);
            createSqlFuncDic.Add(DbType.DateTime, GenCreateDateTime);
            createSqlFuncDic.Add(DbType.Time, GenCreateTime);
        }

    }
}
