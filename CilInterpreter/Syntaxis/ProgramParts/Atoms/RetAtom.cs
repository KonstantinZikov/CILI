using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CilInterpreter.Executing;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class RetAtom : Atom
    {
        public override void Action(Context context)
        {
            if (!context.Stack.Empty)
                throw new CiliRuntimeException("Evaluation stack must be empty, during the return.");
            foreach(var type in (Parent as Method).LocalVars)
            {
                int size = 4;
                if (!type.Item1.IsReferenced && type.Item1.FullSize > 4)
                    size = type.Item1.FullSize;
                context.Memory.RemoveLocal(size);
            }
            context.CurrentInstruction = context.ReturnsStack.Pop();
        }

        public override Atom Create() { return new RetAtom(); }
    }
}
