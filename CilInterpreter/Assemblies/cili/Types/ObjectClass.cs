using CilInterpreter.Assemblies.cili.Types;
using CilInterpreter.Syntaxis.ProgramParts;

namespace CilInterpreter.Assemblies.cili
{
    partial class CiliAssembly
    {
        public static Class CreateObject()
        {
            Class Object = new Class();
            Object.Name = "System.Object";
            Object.LinkedType = TypePool.Object;                  
            return Object;
        }
    }
}
