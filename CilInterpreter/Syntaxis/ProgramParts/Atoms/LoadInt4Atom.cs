using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CilInterpreter.Executing;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class LoadInt4Atom : Atom
    {
        public override Atom Create(){ return new LoadInt4Atom(); }
        public override void Action(Context context)
        {
            string number = Tokens["number"].Value;
            int num = 0;
            if (!int.TryParse(number, out num))
            {
                throw new CiliRuntimeException($"Number at line {Tokens["number"].Line} is too long.");
            }
            context.Stack.Push(BitConverter.GetBytes(num));          
        }
    }
}
