using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter.Syntaxis.ProgramParts
{
    class CilType
    {
        public CilType ParentType { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int NativeBytes { get; set; }
        public bool IsReferenced { get; set; }
        public List<CilType> ReferenceParts { get; set; }
        public List<CilType> InternalParts { get; set; }
        public int FullSize
        {
            get
            {
                int result = NativeBytes;
                if (ParentType != null)
                    result += ParentType.FullSize;               
                result += ReferenceParts.Count * 4;
                foreach (var part in InternalParts)
                    result += part.FullSize;
                return result;
            }
        }
        public List<Method> Methods = new List<Method>();
    }
}
