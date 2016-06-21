using System;
using System.Threading.Tasks;
using BLL.Interface.Services;
using CilInterpreter;

namespace BLL.Services.ExecuteServiceUtils
{
    internal class ExecuteContext
    {
        private readonly Interpreter _interpreter;
        private Task _executingCode;
        private TaskCompletionSource<InterpreterCondition> _tcs;
        private string _output = string.Empty;

        private readonly object _lockObject = new object();

        public string Output
        {
            get
            {
                lock (_lockObject)
                {
                    var result = _output;
                    _output = string.Empty;
                    return result;
                }
            }
        }

        public ExecuteContext()
        {
            _interpreter = new Interpreter();
            _interpreter.WaitInput += InputAction;
            _interpreter.Output += OutputAction;
        }

        public InterpreterCondition Condition { get; private set; } = InterpreterCondition.Stopped;

        public async Task<Tuple<InterpreterCondition, string>> Execute(string code)
        {
            lock (_lockObject)
            {
                if (Condition != InterpreterCondition.Stopped)
                {
                    _interpreter.Stop();
                    _executingCode.Wait();
                }
                Condition = InterpreterCondition.Running;
                _tcs = new TaskCompletionSource<InterpreterCondition>();
                _interpreter.Code = code;

                _executingCode = new Task(
                    () => {
                        _interpreter.Run();
                        _tcs.SetResult(InterpreterCondition.Finished);
                        Condition = InterpreterCondition.Stopped;
                    }
                );

                _executingCode.Start();               
            }
            return new Tuple<InterpreterCondition, string>(await _tcs.Task, Output);
        }

        public async Task<Tuple<InterpreterCondition, string>> Continue(string input)
        {
            switch (Condition)
            {
                case InterpreterCondition.Stopped:
                    return new Tuple<InterpreterCondition, string>
                        (InterpreterCondition.Stopped, "Programm was stopped a moment ago.");
                case InterpreterCondition.Running:
                    return new Tuple<InterpreterCondition, string>
                        (InterpreterCondition.Stopped, "Programm doesn't need input.");
                default:
                    lock (_lockObject)
                    {
                        _tcs = new TaskCompletionSource<InterpreterCondition>();
                        _interpreter.Input(input);
                    }                  
                    return new Tuple<InterpreterCondition, string>(await _tcs.Task, Output);
            }
            
        }

        public Tuple<InterpreterCondition, string> Stop()
        {
            lock (_lockObject)
            {
                _interpreter.Stop();
                _executingCode.Wait();
            }          
            return new Tuple<InterpreterCondition, string>(InterpreterCondition.Finished, Output);
        }

        private void OutputAction(string output) 
            => _output += output;

        private void InputAction()
        {
            lock (_lockObject)
            {
                _tcs.SetResult(InterpreterCondition.WaitingForInput);
                Condition = InterpreterCondition.WaitingForInput;
                _tcs = new TaskCompletionSource<InterpreterCondition>();
            }           
        }
    }
}