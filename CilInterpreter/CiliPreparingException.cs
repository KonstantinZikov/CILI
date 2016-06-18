using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter.Syntaxis
{
    class CiliPreparingException : Exception
    {
        public CiliPreparingException(string message) : base(message) { }
    }
}
