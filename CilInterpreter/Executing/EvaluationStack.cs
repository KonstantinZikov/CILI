using CilInterpreter.Syntaxis;
using CilInterpreter.Syntaxis.ProgramParts;
using System;
using System.Collections.Generic;

namespace CilInterpreter.Executing
{
    class EvaluationStack
    {
        public EvaluationStack(int stackSize)
        {
            elements = new Stack<byte[]>(stackSize);
            isPointers = new Stack<bool>();
            position = 0;
            max = stackSize;
        }

        Stack<byte[]> elements;
        int max;
        int position;
        public Stack<bool> isPointers;

        public byte[] Pop()
        {
            if (position == 0)
                throw new CiliRuntimeException("Evaluation stack is empty.");
            position--;
            isPointers.Pop();
            return elements.Pop();
        }

        public void Push(byte[] bytes)
        {
            if (position == max)
                throw new CiliRuntimeException("Evaluation stack is overloaded.");
            elements.Push(bytes);
            isPointers.Push(false);
            position++;
        }

        public void Push(int pointer)
        {
            isPointers.Push(true);
            Push(BitConverter.GetBytes(pointer));
        }
        public bool Empty { get { return position == 0; } }
    }
}
