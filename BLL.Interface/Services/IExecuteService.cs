using System;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IExecuteService
    {
        int GetNewId();

        Task<Tuple<InterpreterCondition,string>> Execute(string code, string userName);
        Task<Tuple<InterpreterCondition,string>> Execute(string code, int guestGuid);

        Task<Tuple<InterpreterCondition,string>> Continue(string input, string userName);
        Task<Tuple<InterpreterCondition,string>> Continue(string input, int guestGuid);

        Tuple<InterpreterCondition, string> Stop(string userName);
        Tuple<InterpreterCondition, string> Stop(int guestGuid);

        void ClearOverTheLast(int seconds = 3600);
    }
}
