using CilInterpreter.Executing;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class LoadStringAtom : Atom
    {
        public override Atom Create() { return new LoadStringAtom(); }
        private string data;

        public override void PreAction()
        {
            string value = Tokens["string"].Value;
            data = value.Substring(1, value.Length - 2);
        }

        public override void Action(Context context)
            => Helper.StringToStack(context, data);            
        
    }
}
