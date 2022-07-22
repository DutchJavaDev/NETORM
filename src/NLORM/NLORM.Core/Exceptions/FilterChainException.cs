using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NETORM.Core.Exceptions;

namespace NETORM.Core.Exceptions
{
    public class FilterChainException : NETORMException
    {
        public FilterChainException(string message) : base("FCE",message)
        {
        }
    }
}
