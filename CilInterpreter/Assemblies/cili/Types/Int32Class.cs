using CilInterpreter.Executing;
using CilInterpreter.Syntaxis.ProgramParts;
using CilInterpreter.Assemblies.cili.Types;
using System;

namespace CilInterpreter.Assemblies.cili
{
    partial class CiliAssembly
    {
        public static Class CreateInt32()
        {
            Class Int32 = new Class();
            Int32.Name = "System.Int32";
            Int32.LinkedType = TypePool.Int32;

            //ToString
            Method ToString = new Method();
            ToString.Name = "ToString";
            ToString.Static = false;
            ToString.ResultType = TypePool.String;
            ToString.ThisParam = TypePool.Int32;
            ToString.Return = true;

            ToString.PredefinedAction = (context, parametres) =>
            {
                var intAddress = parametres[0].Ref();
                int number = context.Memory[intAddress, intAddress + 4].I4();
                var resultString = number.ToString();
                Helper.StringToStack(context, resultString);
            };
            Int32.Methods.Add(ToString);

            //Parse
            Method Parse = new Method();
            Parse.Name = "Parse";
            Parse.Static = true;
            Parse.ResultType = TypePool.Int32;
            Parse.ParamTypes.Add(TypePool.String);
            Parse.Return = true;

            Parse.PredefinedAction = (context, parametres) =>
            {
                string data = Helper.StringFromPointer(context, parametres[0].Ref());
                int result;
                if (!int.TryParse(data, out result))
                    throw new ProgramException($"String {data} can't be parsed to Int32.");
                context.Stack.Push(BitConverter.GetBytes(result));
            };
            Int32.Methods.Add(Parse);

            return Int32;
        }       
    }
}
