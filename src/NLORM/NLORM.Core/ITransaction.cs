﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NETORM.Core
{
    public interface ITransaction
    {
        void Commit();
        void Rollback();
        void Dispose();
    }
}
