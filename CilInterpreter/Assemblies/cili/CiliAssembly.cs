namespace CilInterpreter.Assemblies.cili
{
    partial class CiliAssembly : AssemblyDefinition
    {
        protected override Assembly Create()
        {
            Assembly assembly = new Assembly();
            assembly.Name = "Cili";
            assembly.Classes.Add(CreateObject());
            assembly.Classes.Add(CreateInt32());
            assembly.Classes.Add(CreateConsole());
            assembly.Classes.Add(CreateString());
            return assembly;
        }      
    }
}
