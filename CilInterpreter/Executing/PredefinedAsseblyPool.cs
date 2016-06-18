using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CilInterpreter.Syntaxis.ProgramParts;

namespace CilInterpreter.Executing
{
    class PredefinedAsseblyPool
    {
        static PredefinedAsseblyPool()
        {
            InitializeCili();

        }

        public Dictionary<string, Assembly> Assemblies { get; }
            = new Dictionary<string, Assembly>();

        static void InitializeCili()
        {
            Class Int32 = new Class();
        }

    }
}
