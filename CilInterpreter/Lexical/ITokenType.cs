using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter
{
    interface ITokenType
    {
        bool Is(string word);
    }
}
