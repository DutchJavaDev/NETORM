using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NETORM.Core.BasicDefinitions;

namespace NETORM.Core
{
    public interface ISqlGenerator
    {
        string GenCreateTableSql(ObjectDefinition md);
        string GenDropTableSql(ObjectDefinition md);
        string GenInsertSql(ObjectDefinition md);
        string GenSelectSql(ObjectDefinition md);
        string GenDeleteSql(ObjectDefinition md);
        string GenUpdateSql(ObjectDefinition md,Object obj);
        string GenTableExistSql(ObjectDefinition md);
    }
}
