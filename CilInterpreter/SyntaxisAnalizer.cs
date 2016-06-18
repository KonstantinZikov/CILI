using CilInterpreter.Syntaxis;
using CilInterpreter.Syntaxis.ProgramParts;
using System.Collections.Generic;

namespace CilInterpreter
{
    internal static class SyntaxisAnalizer
    {
        public static Assembly CreateProgramTree(List<Token> tokens)
        {
            Assembly assembly = new Assembly();
            var codeStream = new TokenStream(tokens);
            int position = 0;
            var atomQueue = new List<IAtomType>();
            bool found = false;
            while (!codeStream.IsFinish)
            {
                foreach(var type in AtomPool.MetaAtoms)
                {
                    if (type.Valid(codeStream))
                    {
                        atomQueue.Add(type);
                        position = codeStream.Position;
                        found = true;
                        break;
                    }
                    codeStream.Reset(position);
                }
                if (!found)
                    throw new SyntaxisAnalizeException(
                        $"Unknown element at line {tokens[codeStream.Position].Line}.");
                found = false;
            }
            codeStream.Reset();
            int i = 0;
            foreach (var type in atomQueue)
            {             
                assembly.Atoms.Add((i++).ToString(),type.Get(codeStream));
                codeStream.Reset(codeStream.Position);
            }
            return assembly;
        }
    }
}
