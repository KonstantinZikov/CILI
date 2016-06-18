using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class MaxStackAtom : Atom
    {
        public override void PreAction()
        {
            Method method = Parent as Method;
            if (method == null)
                throw new Exception(".maxstack must be decleared in method's body.");
            if (method.StackSize != 8)
                throw new Exception("Double maxstack definition");
            int size = 0;
            if (!int.TryParse(Tokens["size"].Value,out size))
                throw new Exception(".maxstack param must be the uint32.");
            if (size < 0) size *= -1;
            method.StackSize = size;
        }

        public override Atom Create() { return new MaxStackAtom(); }
    }
}
