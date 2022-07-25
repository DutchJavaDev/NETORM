using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace NETORM.Core.Exceptions
{
    [Serializable]
    public class NETORMException : Exception,ISerializable
    {
        public string ErrorCode 
        {
            get 
            {
                return errorCode;
            }
        }
        private string errorCode;

        public NETORMException(string errorCode,string message) :base(message)
        {
            this.errorCode = errorCode;
        }
    }
}
