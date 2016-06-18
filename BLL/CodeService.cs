using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using CilInterpreter;

namespace BLL
{
    public class CodeService : ICodeService
    {
        CilInterpreter.Interpreter interpreter = new CilInterpreter.Interpreter();
        StringBuilder result = new StringBuilder();

        public string Interpret(string code)
        {
            result.Clear();
            interpreter.Code = code;
            interpreter.Output += getOutput;
            try
            {
                interpreter.Run();
            }
            catch(Exception ex)
            {
                result.Append(ex.Message);
            }
            return result.ToString();
        }

        void getOutput(string output)
        {
            result.Append(output);
        }
    }
}
