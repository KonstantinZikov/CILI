using CilInterpreter.Executing;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class BranchGreaterOrEqualAtom: BranchAtom
    {
        public override void Action(Context context)
        {
            var value2 = context.Stack.Pop();
            if (value2.Length != 4)
                throw new CiliRuntimeException("The \"bge\" command use 4-byte values, "
                    + "but second value is of a different size.");
            var value1 = context.Stack.Pop();
            if (value1.Length != 4)
                throw new CiliRuntimeException("The \"bge\" command use 4-byte values, "
                    + "but first value is of a different size.");
            if (value1.I4() >= value2.I4())
                context.CurrentInstruction = JumpAddress;
        }

        public override Atom Create(){return new BranchGreaterOrEqualAtom();}
    }
}
