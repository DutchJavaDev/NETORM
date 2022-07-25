using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using NETORM.Core.BasicDefinitions;
using NETORM.Core.Attributes;

namespace NETORM.Core
{
    public class ObjectDefinitionConverter
    {
        public ObjectDefinition ConverClassToModelDefinition<T>() 
        {
            return ConverClassToModelDefinition(typeof(T));
        }

        public ObjectDefinition ConverClassToModelDefinition(Type T)
        {
            var attributes = T.GetCustomAttributes(true);
            var properties = T.GetProperties();
            string tableName = GetTableNameByType(T,attributes);
            var fiedlDic = GetColumnDefinition(attributes, properties);
            var objectDef = new ObjectDefinition(tableName, fiedlDic);
        
            return objectDef;
        }

        private string GetTableNameByType(Type type,IEnumerable<object> attributes)
        {
            var tableAttr = attributes.Where(i => i.GetType() == typeof(TableAttribute)).FirstOrDefault();

            if (tableAttr is null)
                return type.Name;

            return (tableAttr as TableAttribute).TableName;
        }

        private Dictionary<string, ColumnDefinition> GetColumnDefinition(IEnumerable<object> attributes, IEnumerable<PropertyInfo> properties)
        {
			var ret = new Dictionary<string, ColumnDefinition>();
            foreach (var pro in properties)
            {
                var columnFieldDef = GetColumnDefByProprty(attributes);

				if ( GenCloumn( pro) )
					ret.Add(pro.Name, columnFieldDef);
            }
            return ret;
        }

        private ColumnDefinition GetColumnDefByProprty(IEnumerable<object> attributes)
        {
            var columDef = new ColumnDefinition();
            
            var columnAttr = attributes.Where(i => i.GetType() == typeof(ColumnAttribute)).FirstOrDefault();


            if (columnAttr != null)
            {
                AsignColumnAttrToDef(columDef, columnAttr as ColumnAttribute);
            }
            else
            {
                
            }
            
            return columDef;
        }

        private void AsignColumnAttrToDef(ColumnDefinition columnDefinition,ColumnAttribute columnAttribute)
        {
            
        }

        //private void AsignColNameAttrToDef(ColumnDefinition colunmF,
        //    ColumnAttribute colNameAttr,PropertyInfo prop)
        //{
        //    colunmF.ColumnName = colNameAttr == null ? prop.Name : colNameAttr.ColumnName;
        //}

        //private void AsignColTypeAttrToDef(ColumnDefinition colunmF, 
        //    ColumnTypeAttribute colTypeAttr,PropertyInfo prop)
        //{
        //    if (colTypeAttr != null)
        //    {
        //        colunmF.PropName = prop.Name;
        //        colunmF.FieldType = colTypeAttr.DBType;
        //        colunmF.Length = colTypeAttr.Length;
        //        colunmF.Nullable = colTypeAttr.Nullable;
        //        colunmF.Comment = colTypeAttr.Comment;
        //    }
        //    else
        //    {
        //        colunmF.PropName = prop.Name;
        //        colunmF.FieldType = Dapper.SqlMapper.LookupDbType(prop.PropertyType,prop.Name);
        //    }
        //}

		private bool GenCloumn(PropertyInfo prop)
		{
			var tag = true;

			var attrs = prop.GetCustomAttributes( true);

		    foreach ( object attr in attrs)
			{
			    var ngattr = attr as NotGenColumnAttribute;
			    if (ngattr != null)
			    {
			        tag = false;
			    }
			}

		    return tag;
		}
    }
}
