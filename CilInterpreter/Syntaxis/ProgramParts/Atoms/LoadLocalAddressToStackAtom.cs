using CilInterpreter.Executing;
using System;
using System.Collections.Generic;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class LoadLocalAddressToStackAtom : Atom
    {
        public override Atom Create() { return new LoadLocalAddressToStackAtom(); }
        public int offset { get; set; } = 0;
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
                        offset += 4;
                    else
                        offset += type.FullSize;
                }
            }
        }

        public override void Action(Context context)
            =>context.Stack.Push(parentMethod.LocalAddress + offset);          
        
    }
}
