using CilInterpreter.Assemblies.cili;

namespace CilInterpreter.Assemblies
{
    class MscorlibAssembly : AssemblyDefinition
    {
        protected override Assembly Create()
        {
            Assembly assembly = new Assembly();
            assembly.Name = "mscorlib";
            var Cili = AssemblyPool.GetInstance(typeof(CiliAssembly));
            foreach (var c in Cili.Classes)
                assembly.Classes.Add(c);
            foreach (var m in Cili.Methods)
                assembly.Methods.Add(m);
            return assembly;
        }        
    }
}
