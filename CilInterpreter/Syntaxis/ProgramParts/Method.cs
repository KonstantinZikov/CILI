using CilInterpreter.Syntaxis.ProgramParts;
using System.Collections.Generic;
using CilInterpreter.Executing;
using CilInterpreter.Syntaxis;
using System;

namespace CilInterpreter
{
    class Method : Atom
    {     
        public CilType ResultType { get; set; }
        public List<CilType> ParamTypes { get; set; } = new List<CilType>();
        public CilType ThisParam { get; set; } = null;
        public List<Tuple<CilType, string>> LocalVars { get; set; } = new List<Tuple<CilType, string>>();
        public int LocalAddress { get; set; }
        public bool Static { get; set; }
        public bool Sealed { get; set; }
        public string Name { get; set; }
        public int StackSize { get; set; } = 8;
        public bool Return { get; set; }
        public List<Atom> Actions { get; } = new List<Atom>();
        public string Label { get; set; }

        public override Atom Create() { return new Method(); }
        private List<byte[]> LoadArguments(Context context)
        {
            List<byte[]> result = new List<byte[]>();
            for (int i = 0; i < ParamTypes.Count; i++)
            {
                result.Add(context.LastStack.Pop());
            }
            if (ThisParam!=null)
                result.Add(context.LastStack.Pop());
            result.Reverse();
            return result;
        }
        public override void Action(Context context)
        {
            context.MethodCall(StackSize);
            LocalAddress = context.Memory.LocalPointer;
            var parametres = LoadArguments(context);
            PredefinedAction?.Invoke(context, parametres);
            context.MethodRet();
            context.CurrentInstruction = context.ReturnsStack.Pop();
        }
        public Action<Context,List<byte[]>> PredefinedAction { get; set; }
    }
}
