using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CilInterpreter;
using static CilPlayground.ExecuteEngine.InterpreterCondition;
using System.Threading;
namespace CilPlayground.ExecuteEngine
{
    public class ExecuteContext
    {
        Interpreter interpreter;
        Task ExecutingCode;
        TaskCompletionSource<InterpreterCondition> tcs;
        string _output = string.Empty;

        public string Output
        {
            get
            {
                var result = _output;
                _output = string.Empty;
                return result;
            }
        }

        public ExecuteContext()
        {
            interpreter = new Interpreter();
            interpreter.WaitInput += InputAction;
            interpreter.Output += OutputAction;
        }

        public InterpreterCondition Condition { get; private set; } = Stopped;

        public async Task<Tuple<string, string>> Execute(string code)
        {
            //TODO concurent+
            if (Condition != Stopped)
            {
                interpreter.Stop();
                ExecutingCode.Wait();
            }
            Condition = Running;
            tcs = new TaskCompletionSource<InterpreterCondition>();
            interpreter.Code = code;
            ExecutingCode = new Task(
                () => {
                    interpreter.Run();
                    tcs.SetResult(Finished);
                    Condition = Stopped;
                }
            );
            ExecutingCode.Start();
            return new Tuple<string, string>((await tcs.Task).ToString(), Output);
        }

        public async Task<Tuple<string, string>> Continue(string input)
        {
            //TODO concurent+
            if (Condition == Stopped)
                return new Tuple<string, string>(Stopped.ToString(), "Programm was stopped a moment ago.");
            if (Condition == Running)
                return new Tuple<string, string>(Stopped.ToString(), "Programm doesn't need input:)");
            tcs = new TaskCompletionSource<InterpreterCondition>();
            interpreter.Input(input);
            return new Tuple<string, string>((await tcs.Task).ToString(), Output);
        }

        public Tuple<string, string> Stop()
        {
            interpreter.Stop();
            ExecutingCode.Wait();
            return new Tuple<string, string>(Finished.ToString(), Output);
        }
            
        void OutputAction(string output) => _output += output;

        void InputAction()
        {
            tcs.SetResult(WaitingForInput);
            Condition = WaitingForInput;
            tcs = new TaskCompletionSource<InterpreterCondition>();
        }
    }
}