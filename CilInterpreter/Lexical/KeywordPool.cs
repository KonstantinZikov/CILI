using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter
{
    internal class KeywordPool
    {
        public static ReadOnlyCollection<string> KeywordStrings { get; private set; }
        static KeywordPool()
        {
            KeywordStrings = new ReadOnlyCollection<string>(new List<string> {
                "public",
                "private",
                "static",
                "cil",
                "managed",               
            });
        }
    }
}
