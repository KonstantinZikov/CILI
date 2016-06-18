using CilInterpreter.Syntaxis.ProgramParts;
using CilInterpreter.Assemblies.cili.Types;

namespace CilInterpreter.Assemblies.cili
{
    partial class CiliAssembly
    {
        private static Class CreateString()
        {
            Class String = new Class();
            String.Name = "System.String";
            String.LinkedType = TypePool.String;

            Method Constructor1 = new Method();
            Constructor1.Name = ".ctor";
            Constructor1.ThisParam = TypePool.String;
            Constructor1.ResultType = TypePool.String;
            Constructor1.ParamTypes.Add(ArrayType.Instance(TypePool.Char));
            Constructor1.PredefinedAction = (context, parametres) =>
            {
                int index = parametres[0].Ref() + 4;
                for (int i = 0; i < 4; i++)
                    context.Memory[index + i] = parametres[1][i];
                context.Stack.Push(parametres[0]);
            };

            String.Methods.Add(Constructor1);

            return String;
        }
    }
}
