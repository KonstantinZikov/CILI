namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class MethodBodyAtom : Atom
    {
        public override void PreAction()
        {
            Method method = Parent as Method;
            if (method == null)
                throw new CiliPreparingException("Method's body declared without header.");
            int count = (int)Info["actionsCount"];
            for (int i = 0; i < count; i++)
            {
                string action = $"action{i}";
                method.Actions.Add(Atoms[action]);
                Atoms[action].Parent = method;             
            }
            foreach (var atom in Atoms)
                atom.Value.PreAction();
        }

        public override Atom Create() { return new MethodBodyAtom(); }
    }
}
