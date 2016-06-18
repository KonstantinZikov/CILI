using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter.Lexical
{
    class LexicalAnalizeException : Exception
    {
        public LexicalAnalizeException(string message) : base(message){}
    }
}
