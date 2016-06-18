using CilInterpreter.Assemblies.cili.Types;
using CilInterpreter.Syntaxis;
using CilInterpreter.Syntaxis.ProgramParts;
using System;


namespace CilInterpreter.Executing
{
    static class Helper
    {
        public static void StringToStack(Context context, string data)
        {
            int index = (context.Memory.TypeMap.FindIndex((m)=>m.GetType() == typeof(ArrayType) && (m as ArrayType).InternalType == TypePool.Char));
            int arrayRef = context.Memory.LoadArray(index, data.Length, false);
            for (int i = 0; i < data.Length; i++)
                context.Memory[arrayRef + 8 + i * 2, arrayRef + 8 + i * 2 + 1] = BitConverter.GetBytes(data[i]);
            index = (context.Memory.TypeMap.IndexOf(TypePool.String));
            int stringRef = context.Memory.LoadObject(index);
            context.Memory[stringRef + 4, stringRef + 8] = BitConverter.GetBytes(arrayRef);
            context.Stack.Push(BitConverter.GetBytes(stringRef));
        }

        public static string StringFromPointer(Context context, int pointer)
        {
            int arrayRef = context.Memory[pointer + 4, pointer + 8].Ref();
            int size = context.Memory[arrayRef + 4, arrayRef + 8].I4();
            byte[] charBytes = context.Memory[arrayRef + 8, arrayRef + 8 + size * 2];
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
                chars[i] = BitConverter.ToChar(charBytes, i * 2);
            return new string(chars);
        }

        public static Assembly GetCili(Assembly assembly)
        {
            var cili = assembly.ReferencedAssemblies.Find(a => a.Name == "Cili" || a.Name == "mscorlib");
            if (cili == null)
                throw new CiliPreparingException("Please connect the Cili assembly or it's alias \"mscorlib\" by using .assembly directive.");
            return cili;
        }

        public static Assembly GetParentAssembly(Atom parent)
        {
            while (parent.Parent != null)
                parent = parent.Parent;
            return parent as Assembly;
        }
    }
}
