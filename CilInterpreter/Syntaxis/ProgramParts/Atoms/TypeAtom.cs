using CilInterpreter.Executing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class TypeAtom : Atom
    {
        public CilType TargetType { get; set; }

        public override void PreAction()
        {
            if ((bool)Info["predefined"] == true)
            {
                Assembly assembly = Helper.GetParentAssembly(Parent);
                if (assembly.Done == false)
                    assembly.Continuation += PreAction;
                else
                {
                    string name = Tokens["name"].Value;
                    Assembly cili = assembly.ReferencedAssemblies.Find(a => a.Name == "Cili" || a.Name == "mscorlib");
                    if (cili == null)
                        throw new CiliPreparingException($"Unknown type {name}." +
                            "Plug extern assembly \"cili\" or it's alias \"mscorlib\" to enable predifined types.");
                    if (name != "void")
                    {
                        Class targetClass = cili.Classes.Find((c) => c?.LinkedType?.ShortName == name);
                        if (targetClass == null)
                            throw new CiliPreparingException($"Internal error: The type {name} " +
                                "wasn't defined in cili.");
                        TargetType = targetClass.LinkedType;
                    }
                }
            }
        }


        public override Atom Create() { return new TypeAtom(); }
        public override bool Equals(object obj)
        {
            var type = obj as TypeAtom;
            if (type == null) return false;
            return Equals(type);
        }
        public bool Equals(TypeAtom atom)
        {
            if ((bool)Info["predefined"] == true)
            {
                if ((bool)atom.Info["predefined"] == false) return false;
                if (Tokens["name"].Value == atom.Tokens["name"].Value)
                    return true;
            }
            else
            {
                if ((bool)atom.Info["predefined"] == true) return false;
                if (atom.Tokens["assembly"].Value == Tokens["assembly"].Value)
                    if (atom.Tokens["name"].Value == Tokens["name"].Value)
                        return true;
            }
            return false;
        }
    }
}
