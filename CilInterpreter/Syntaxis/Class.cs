using CilInterpreter.Syntaxis.ProgramParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter
{
    class Class : Atom
    {
        public string Name { get; set; }
        public List<Method> Methods { get; } = new List<Method>();
        public override Atom Create() { return new Class(); }
        public CilType LinkedType { get; set; }
    }
}
