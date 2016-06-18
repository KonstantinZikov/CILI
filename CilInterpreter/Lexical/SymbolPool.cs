using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter.Lexical
{
    internal class SymbolPool
    {
        public static ReadOnlyCollection<string> SymbolStrings { get; private set; }
        static SymbolPool()
        {
            SymbolStrings = new ReadOnlyCollection<string>(new List<string> {
                "[","]",
                "{","}",
                "(",")",
                "<",">",
                "(",")",
                ",",
                "::",
                ":"
            });
        }
    }
}
