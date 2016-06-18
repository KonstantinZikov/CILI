using CilInterpreter.Executing;
using System;
using System.Collections.Generic;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class InitLocalsAtom : Atom
    {
        public override Atom Create() { return new InitLocalsAtom(); }

        public List<Tuple<CilType, string>> Locals { get; set; } = new List<Tuple<CilType, string>>();

        public override void PreAction()
        {
            Assembly assembly = Helper.GetParentAssembly(Parent);
            if (assembly.Done == false)
            {
                foreach (var atom in Atoms)
                {
                    atom.Value.Parent = this;
                    atom.Value.PreAction();
                }
                assembly.Continuation += PreAction;
            }
            else
            {
                int count = (int)Info["localCount"];
                var method = Parent as Method;
                for (int i = 0; i < count; i++)
                {
                    var type = (Atoms[$"type{i}"] as TypeAtom).TargetType;
                    string name = null;
                    Token nameToken;
                    if (Tokens.TryGetValue($"name{i}", out nameToken))
                        name = nameToken.Value;
                    var pare = new Tuple<CilType, string>(type, name);
                    method.LocalVars.Add(pare);
                    Locals.Add(pare);
                }
            }
        }

        public override void Action(Context context)
        {
            foreach(var pare in Locals)
            {
                var type = pare.Item1;
                var size = 4;
                if (!type.IsReferenced && type.FullSize > 4)
                    size = type.FullSize;
                context.Memory.AddLocal(size);
            }
        }
    }
}
