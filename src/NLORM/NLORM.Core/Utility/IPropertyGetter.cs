﻿using NETORM.Core.BasicDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NETORM.Core.Utility
{
    public interface IPropertyGetter
    {
         IDictionary<string, object> GetPropertyDic(object Fo);
    }
}
