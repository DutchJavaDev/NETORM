﻿using System;

namespace NETORM.Core.BasicDefinitions
{
    public class FilterObject : IFilterObject
    {
        public Type ClassType { get; set; }
        public FilterType Filter { get; set; }
        public dynamic Cons { get; set; }
    }
}
