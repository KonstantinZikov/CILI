using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class MethodHeaderAtom:Atom
    {
        public override void PreAction()
        {
            Method method = Parent as Method;
            if (method == null)
                throw new Exception("method's header declared without method's body");
            method.Name = Tokens["name"].Value;
            var resultType =  Atoms["type"] as TypeAtom;
            //method.ResultType 
        }

        public override Atom Create() { return new MethodHeaderAtom(); }
    }
}
