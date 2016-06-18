using CilInterpreter.Syntaxis.ProgramParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter
{
    class Assembly : Atom
    {
        public string Name { get; set; }
        public List<Assembly> PotentialReferencedAssemblies { get; set; } = new List<Assembly>();
        public List<Assembly> ReferencedAssemblies { get; set; } = new List<Assembly>();
        public List<Atom> Directives { get; set; } = new List<Atom>();
        public List<Class> Classes { get; set; } = new List<Class>();
        public List<Method> Methods { get; set; } = new List<Method>();
        public Method EntryPoint { get; set; }

        public bool Done { get; private set; }
        public event Action Continuation; 
        public override Atom Create() { return new Assembly(); }

        public override void PreAction()
        {
            base.PreAction();
            Done = true;
            Continuation?.Invoke();
        }
    }
}
