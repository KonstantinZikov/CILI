using System;
using System.Collections.Generic;
using CilInterpreter.Syntaxis;
using CilInterpreter.Assemblies;
using CilInterpreter.Assemblies.cili;
using CilInterpreter.Executing;
using CilInterpreter.Lexical;

namespace CilInterpreter
{
    public class Interpreter
    {
        public void Input(string data)
        {
            input.AddInput(data);
        }

        private Context context;
        private Input input;
        public event Action WaitInput;
        public event Action<string> Output;
        public event Action NextLine;

        public string Code { get; set; }

        public Interpreter(string code = null)
        {
            Code = code;          
        }

        public void Run(int maxOperations = 10000)
        {
            input = new Input();
            context = new Context(OnOutput, input);
            input.WaitInput += OnWaitInput;
            if (Code != null)
            {             
                try
                {
                    var tokens = LexicalAnalizer.Analize(Code);
                    var assembly = SyntaxisAnalizer.CreateProgramTree(tokens);
                    List<Assembly> assemblies = new List<Assembly>();
                    assemblies.Add(AssemblyPool.GetInstance(typeof(CiliAssembly)));
                    assemblies.Add(AssemblyPool.GetInstance(typeof(MscorlibAssembly)));
                    ExecuteEngine.Execute(assembly, assemblies, context, maxOperations);
                }
                catch(LexicalAnalizeException ex)
                {
                    OnOutput("Lexical analize error: " + ex.Message + "\r\n");
                }
                catch (SyntaxisAnalizeException ex)
                {
                    OnOutput("Syntaxis analize error: " + ex.Message + "\r\n");
                }
                catch (CiliPreparingException ex)
                {
                    OnOutput("Code preparing error: " + ex.Message + "\r\n");
                }
                catch (CiliRuntimeException ex)
                {
                    OnOutput("Runtime error: " + ex.Message + "\r\n");
                }
                catch (ProgramException ex)
                {
                    OnOutput("Programm error: " + ex.Message + "\r\n");
                }
                catch(Exception ex)
                {
                    OnOutput("INTERNAL ERROR: " + ex.Message + "\r\n");
                }
            }
        }

        public void Stop()
        {
            input.Stop();
            context.Stopped = true;         
        }

        public void OnOutput(string data)
        {
            Output?.Invoke(data);
        }

        public void OnWaitInput()
        {
            WaitInput?.Invoke();
        }
    }
}
