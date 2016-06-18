using CilInterpreter.Syntaxis;
using CilInterpreter.Syntaxis.ProgramParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter
{
    interface IAtomType
    {        
        bool Valid(TokenStream codeStream);
        TokenStream Is(TokenStream codeStream);
        Atom Get(TokenStream codeStream);
        Atom Create();
    }
}
