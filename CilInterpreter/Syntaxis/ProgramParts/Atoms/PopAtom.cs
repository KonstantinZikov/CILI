using CilInterpreter.Executing;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class PopAtom : Atom
    {
        public override void Action(Context context)
        {
            context.Stack.Pop();
        }

        public override Atom Create() { return new PopAtom(); }
    }
}
