using System;

namespace CilInterpreter.Assemblies
{
    class ProgramException :Exception
    {
        public ProgramException(string message) : base(message) { }
    }
}
