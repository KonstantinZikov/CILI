using System.Collections.Generic;

namespace CilInterpreter.Syntaxis.ProgramParts
{
    class Class : Atom
    {
        public override Atom Create(){return new Class();}
        public List<CilType> Fields { get; set; } = new List<CilType>();
        public List<Method> Methods { get; set; } = new List<Method>();
        public string Name { get; set; }
        public CilType LinkedType { get; set; }
        public bool Static { get; set; }
    }
}
