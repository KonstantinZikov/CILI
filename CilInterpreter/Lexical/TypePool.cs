using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter.Lexical
{
    class TypePool
    {
        public static ReadOnlyCollection<string> TypeStrings { get; private set; }
        static TypePool()
        {
            TypeStrings = new ReadOnlyCollection<string>(new List<string> {
                "void",
                "int32",
                "char",
                "string"
            });
        }
    }
}
