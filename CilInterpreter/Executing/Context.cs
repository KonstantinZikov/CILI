using CilInterpreter.Assemblies.cili.Types;
using CilInterpreter.Syntaxis.ProgramParts;
using System;
using System.Collections.Generic;

namespace CilInterpreter.Executing
{
    class Context
    {
        public Context(Action<string> output, Input input)
        {
            _input = input;
            _output = output;
            Memory = new Memory();
            Memory.TypeMap = new List<CilType>(TypePool.Types);
            foreach(var type in TypePool.Types)
                Memory.TypeMap.Add(ArrayType.Instance(type));
        }

        private Action<string> _output;
        private Input _input;

        public bool Stopped { get; set; }
        public Memory Memory { get; set; }
        public Atom[] Instructions { get; set; } = new Atom[1024 * 1024];
        public int CurrentInstruction { get; set; }
        public Stack<int> ReturnsStack { get; set; } = new Stack<int>();
        private List<EvaluationStack> SuperStack = new List<EvaluationStack>();
        public void MethodCall(int stackSize)
        {
            SuperStack.Add(new EvaluationStack(stackSize));
        }
        public void MethodRet()
        {
            if (!Stack.Empty)
                LastStack.Push(Stack.Pop());
            SuperStack.RemoveAt(SuperStack.Count - 1);
        }
        public EvaluationStack LastStack { get { return SuperStack[SuperStack.Count - 2]; } }
        public EvaluationStack Stack { get { return SuperStack[SuperStack.Count - 1]; } }
        public void Output(string data)
        {
            _output(data);
        }
        public string Input()
        {
            return _input.GetInput();
        }

    }
}
