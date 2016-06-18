using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CilInterpreter.Executing;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class PrintAtom : Atom
    {
        public override void Action(Context context)
        {
            string output = Tokens["string"].Value.Substring(1, Tokens["string"].Value.Length - 2);
            output+= "" + (char)13 + (char)10;
            context.Output(output);
        }

        public override Atom Create() { return new PrintAtom(); }
    }
}
