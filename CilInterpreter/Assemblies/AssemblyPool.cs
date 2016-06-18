using CilInterpreter.Assemblies.cili;
using CilInterpreter.Syntaxis;
using System;
using System.Collections.Generic;

namespace CilInterpreter.Assemblies
{
    class AssemblyPool
    {
        private static List<AssemblyDefinition> AssemblyList { get; set; } = new List<AssemblyDefinition>()
        {
            new CiliAssembly(),
            new MscorlibAssembly()
        };


        public static Assembly GetInstance(Type type)
        {
            var assembly = AssemblyList.Find(a => a.GetType() == type);
            if (assembly == null)
                throw new CiliPreparingException($"Unknown type of predefined assembly - {type}.");
            return assembly.Instance;
        }
    }
}
