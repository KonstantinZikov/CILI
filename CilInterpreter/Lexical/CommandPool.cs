using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CilInterpreter
{
    internal static class CommandPool
    {
        public static ReadOnlyCollection<string> CommandStrings { get; private set; }
        static CommandPool()
        {
            CommandStrings = new ReadOnlyCollection<string>(new List<string> {
                "add",
                "and",

                "br",
                "beq",
                "bgt",
                "ble",
                "blt",
                "brfalse",
                "brnull",
                "brzero",
                "brtrue",
                "brinst",
                "bne",
                
                "call",
                "div",
                "dup",
                "jmp",
                "ldc.i4",
                "ldloc.0",
                "ldloc.1",
                "ldloc.2",
                "ldloca",
                "ldnull",
                "ldstr",
                "mul",
                "neg",
                "pop",
                "ret",
                "starg",
                "stloc",
                "stloc.0",
                "stloc.1",
                "stloc.2",
                "stloc.3",
                "sub",
                "xor",
                "init"
            });
        }

    }
}
