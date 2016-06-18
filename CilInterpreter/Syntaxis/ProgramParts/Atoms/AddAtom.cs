using System;
using CilInterpreter.Executing;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class AddAtom : Atom
    {
        public override Atom Create()
        {
            return new AddAtom();
        }

        public override void Action(Context context)
        {
            int num1 = context.Stack.Pop().I4();
            int num2 = context.Stack.Pop().I4();
            context.Stack.Push(BitConverter.GetBytes(num1 + num2));
        }
    }
}
