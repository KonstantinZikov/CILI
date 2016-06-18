using System;

namespace CilInterpreter.Syntaxis
{
    class CiliRuntimeException : Exception
    {
        public CiliRuntimeException(string message) : base(message) { }
    }
}
