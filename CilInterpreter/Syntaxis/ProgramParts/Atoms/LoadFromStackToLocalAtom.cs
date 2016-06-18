using CilInterpreter.Executing;
using System;
using System.Collections.Generic;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class LoadFromStackToLocalAtom : Atom
    {
        public override Atom Create() { return new LoadFromStackToLocalAtom(); }

        public int Offset { get; set; }
        public int Size { get; set; }
        public Method parentMethod { get; set; }

        public override void PreAction()
        {
            Assembly assembly = Helper.GetParentAssembly(Parent);
            if (!assembly.Done)
                assembly.Continuation += PreAction;
            else
            {
                parentMethod = Parent as Method;
                string tokenValue = Tokens["identifier"].Value;
                int index;
                if (!int.TryParse(tokenValue, out index))
                {               
                    index = parentMethod.LocalVars.FindIndex(p => tokenValue == p.Item2);
                    if (index == -1)
                        throw new CiliRuntimeException($"Unknown identifier {tokenValue}");                    
                }
                for (int i = 0; i < index; i++)
                {
                    var type = parentMethod.LocalVars[i].Item1;
                    if (type.IsReferenced || type.FullSize <= 4)
                        Offset += 4;
                    else
                        Offset += type.FullSize;
                }
                var targetType = parentMethod.LocalVars[index].Item1;
                if (targetType.IsReferenced || targetType.FullSize <= 4)
                    Size = 4;
                else
                    Size = targetType.FullSize;
            }
        }

        public override void Action(Context context)
        {
            byte[] data = context.Stack.Pop();
            int i = parentMethod.LocalAddress;
            context.Memory[i + Offset, i + Offset + Size] = data;
        }
    }
}
