using System;
using System.Threading.Tasks;
using BLL.Interface.Services;
using BLL.Services.ExecuteServiceUtils;

namespace BLL.Services
{
    public class ExecuteService : IExecuteService
    {
       
        public async Task<Tuple<InterpreterCondition,string>> Continue(string input, Guid guestGuid)
        {
            var context = ExecuteContextPool.GetGuestContext(guestGuid);
            return await context.Continue(input);
        }

        public async Task<Tuple<InterpreterCondition, string>> Continue(string input, string userName)
        {
            var context = ExecuteContextPool.GetUserContext(userName);
            return await context.Continue(input);
        }

        public async Task<Tuple<InterpreterCondition, string>> Execute(string code, Guid guestGuid)
        {
            var context = ExecuteContextPool.GetGuestContext(guestGuid);
            return await context.Execute(code);
        }

        public async Task<Tuple<InterpreterCondition, string>> Execute(string code, string userName)
        {
            var context = ExecuteContextPool.GetUserContext(userName);
            return await context.Execute(code);
        }

        public Tuple<InterpreterCondition, string> Stop(Guid guestGuid)
        {
            var context = ExecuteContextPool.GetGuestContext(guestGuid);
            return context.Stop();
        }

        public Tuple<InterpreterCondition, string> Stop(string userName)
        {
            var context = ExecuteContextPool.GetUserContext(userName);
            return context.Stop();
        }
    }
}
