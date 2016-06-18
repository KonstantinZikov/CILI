using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class ExternAssemblyAtom : Atom
    {
        public override Atom Create() { return new ExternAssemblyAtom(); }

        public override void PreAction()
        {
            Assembly assembly = Parent as Assembly;
            if (assembly == null)
                throw new CiliPreparingException("Extern assemblies must be defined in program root.");
            Assembly externAssembly = assembly.PotentialReferencedAssemblies.Find(a => a.Name == Tokens["name"].Value);
            if (externAssembly == null)
                throw new CiliPreparingException($"Unknown extern assembly {Tokens["name"].Value}.");
            if (assembly.ReferencedAssemblies.Contains(externAssembly))
                throw new CiliPreparingException($"Double defenition of extern assembly {externAssembly.Name}.");
            assembly.ReferencedAssemblies.Add(externAssembly);
            base.PreAction();
        }
    }
}
