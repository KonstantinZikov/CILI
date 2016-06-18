using CilInterpreter.Syntaxis.ProgramParts;
using System.Collections.Generic;

namespace CilInterpreter.Assemblies.cili.Types
{
    class ArrayType : CilType
    {
        public CilType InternalType { get; private set; }
        bool IsReferenced;
        
        ArrayType(CilType internalType = null)
        {          
            Name = "Array";
            if (internalType == null)
                IsReferenced = true;
            else
                InternalType = internalType;
            InternalParts = new List<CilType>();
            ReferenceParts = new List<CilType>();
            Methods = new List<Method>
            {
                new Method()
                {
                    Name = "GetValue",
                    ParamTypes = new List<CilType> { TypePool.Int32 },
                    ResultType = InternalType,
                    Return = true,
                    ThisParam = this,
                    PredefinedAction = (context, parametres) =>
                    {
                        var heap = context.Memory;
                        int sIndex = parametres[0].Ref() + 8;
                        int index = parametres[1].I4();
                        byte[] result;
                        if (IsReferenced)
                        {
                            result = new byte[4];
                            for (int i = 0; i < 4; i++)
                                result[i] = heap[sIndex + 8 + index * 4 + i];
                        }
                        else
                        {
                            int size = InternalType.FullSize;
                            result = new byte[size];
                            for (int i = 0; i < size; i++)
                                result[i] = heap[sIndex + 8 + index * size + i];
                        }
                        context.Stack.Push(result);
                    }
                },
                new Method()
                {
                    Name = "SetValue",
                    ParamTypes = new List<CilType> {InternalType, TypePool.Int32 },
                    ThisParam = this,
                    PredefinedAction = (context, parametres) =>
                    {
                        var heap = context.Memory;
                        int sIndex = parametres[0].Ref();
                        byte[] targetBytes = parametres[1];
                        int index = parametres[2].I4();
                        if (IsReferenced)
                        {
                            for (int i = 0; i < 4; i++)
                                heap[sIndex + 8 + index * 4 + i] = targetBytes[i];
                        }
                        else
                        {
                            int size = InternalType.FullSize;
                            for (int i = 0; i < size; i++)
                                heap[sIndex + 8 + index * size + i] = targetBytes[i];
                        }
                    }
                }
            };
    }

        public static ArrayType Instance(CilType type)
        {
            if (type == null)
                return ReferenceType;
            if (ArrayMap.ContainsKey(type))
                return ArrayMap[type];
            var result = new ArrayType(type);
            ArrayMap.Add(type, result);
            return result;
        }

        private static ArrayType ReferenceType = new ArrayType(null);
        private static Dictionary<CilType, ArrayType> ArrayMap = new Dictionary<CilType, ArrayType>(); 
    }
}
