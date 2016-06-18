using CilInterpreter.Executing;
using CilInterpreter.Syntaxis.ProgramParts;
using CilInterpreter.Assemblies.cili.Types;

namespace CilInterpreter.Assemblies.cili
{
    partial class CiliAssembly
    {
        private static Class CreateConsole()
        {
            Class Console = new Class();
            Console.Name = "System.Console";
            Console.Static = true;

            //WriteLine
            Method WriteLine = new Method();
            WriteLine.Name = "WriteLine";
            WriteLine.Static = true;
            WriteLine.Return = false;
            WriteLine.ParamTypes.Add(TypePool.String);
            WriteLine.PredefinedAction = (context, parametres) =>
            {
                context.Output(Helper.StringFromPointer(context, parametres[0].Ref()) + "\r\n");
            };
            Console.Methods.Add(WriteLine);

            //ReadLine
            Method ReadLine = new Method();
            ReadLine.Name = "ReadLine";
            ReadLine.Static = true;
            ReadLine.Return = true;
            ReadLine.ResultType = TypePool.String;
            ReadLine.PredefinedAction = (context, parametres) =>
            {
                string data = context.Input();
                Helper.StringToStack(context, data);
            };
            Console.Methods.Add(ReadLine);

            return Console;
        }
    }
}
