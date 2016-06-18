using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class AssemblyDefenitionAtom : Atom
    {
        public override void PreAction()
        {
            base.PreAction();
            Assembly assembly = Parent as Assembly;
            if (assembly == null)
                throw new Exception("Assembly defenition can't be nested.");
            if (assembly.Name != null)
                throw new Exception("Double assembly definition");
            assembly.Name = Tokens["name"].Value;
        }

        public override Atom Create() { return new AssemblyDefenitionAtom(); }
    }
}
