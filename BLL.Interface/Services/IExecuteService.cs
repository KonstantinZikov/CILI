using System;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IExecuteService
    {
        Task<Tuple<InterpreterCondition,string>> Execute(string code, string userName);
        Task<Tuple<InterpreterCondition,string>> Execute(string code, Guid guestGuid);

        Task<Tuple<InterpreterCondition,string>> Continue(string input, string userName);
        Task<Tuple<InterpreterCondition,string>> Continue(string input, Guid guestGuid);

        Tuple<InterpreterCondition, string> Stop(string userName);
        Tuple<InterpreterCondition, string> Stop(Guid guestGuid);
    }
}
