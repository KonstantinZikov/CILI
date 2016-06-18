using CilInterpreter.Executing;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class BranchTrueAtom: BranchAtom
    {
        public override void Action(Context context)
        {
            var value1 = context.Stack.Pop();
            if (value1.Length != 4)
                throw new CiliRuntimeException("The \"brtrue\" command use 4-byte values, "
                    + "but value is of a different size.");
            if (value1.I4() != 0)
                context.CurrentInstruction = JumpAddress;
        }

        public override Atom Create(){return new BranchTrueAtom();}
    }
}
