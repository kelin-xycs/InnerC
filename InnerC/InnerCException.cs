using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC
{
    class InnerCException : Exception
    {
        public InnerCException(string message) : base(message)
        {

        }
    }
}
