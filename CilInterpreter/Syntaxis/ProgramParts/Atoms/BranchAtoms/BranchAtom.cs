using CilInterpreter.Executing;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class BranchAtom: Atom
    {
        public int JumpAddress { get; set; }
        public string TargetName { get; set; }

        public override void PreAction()
        {
            TargetName = Tokens["target"].Value;
        }

        public override void Action(Context context)
        {
            context.CurrentInstruction = JumpAddress;
        }

        public override Atom Create(){return new BranchAtom();}
    }
}
