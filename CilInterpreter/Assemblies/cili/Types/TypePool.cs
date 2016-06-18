using CilInterpreter.Syntaxis.ProgramParts;
using System.Collections.Generic;

namespace CilInterpreter.Assemblies.cili.Types
{
    class TypePool
    {
        static TypePool()
        {
            Types = new List<CilType>()
            {
                Object,
                Int32,
                Char,
                String
            };
        }

        public static List<CilType> Types { get; set; }

        public static CilType Object { get; set; } = new CilType
        {
            Name = "System.Object",
            ShortName = "object",
            ReferenceParts = new List<CilType>(),
            InternalParts = new List<CilType>()
        };

        public static CilType Char { get; set; } = new CilType
        {
            ParentType = Object,
            Name = "System.Char",
            ShortName = "char",
            NativeBytes = 2,
            ReferenceParts = new List<CilType>(),
            InternalParts = new List<CilType>()
        };

        public static CilType Int32 { get; set; } = new CilType
        {
            ParentType = Object,
            Name = "System.Int32",
            ShortName = "int32",
            NativeBytes = 4,
            ReferenceParts = new List<CilType>(),
            InternalParts = new List<CilType>()
        };

        public static CilType String { get; set; } = new CilType
        {
            ParentType = Object,
            Name = "System.String",
            ShortName = "string",
            ReferenceParts = new List<CilType> { ArrayType.Instance(Char) },
            InternalParts = new List<CilType>()
        };

        
    }
}
