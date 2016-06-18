using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class EntryPointAtom : Atom
    {
        public override void PreAction()
        {
            Method method = Parent as Method;
            if (method == null)
                throw new Exception(".entrypoint must be decleared in method's body.");
            Atom parent = method;
            while (parent.Parent != null)
                parent = parent.Parent;
            var assembly = parent as Assembly;
            if (assembly == null)
                throw new Exception(".method must be decleared in class's body.");
            if (assembly.EntryPoint != null)
                throw new Exception("Double entrypoint definition");
            assembly.EntryPoint = method;
        }

        public override Atom Create() { return new EntryPointAtom(); }
    }
}
