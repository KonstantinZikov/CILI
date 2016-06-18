using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class LabelAtom:Atom
    {
        public string Name { get; set; }

        public override void PreAction()
        {
            Name = Tokens["name"].Value;
        }
        public override Atom Create() { return new LabelAtom(); }
    }
}
