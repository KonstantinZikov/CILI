using System;
using System.Threading.Tasks;
using BLL.Interface.Exceptions;
using BLL.Interface.Services;
using BLL.Services.ExecuteServiceUtils;

namespace BLL.Services
{
    public class ExecuteService : IExecuteService
    {
        private readonly object _lockObj = new object();
        private int _currentId;

        public int GetNewId()
        {
            lock (_lockObj)
            {
                return _currentId++;           
            }
        }

        public async Task<Tuple<InterpreterCondition,string>> Continue(string input, int guestId)
        {
            if (input == null)
                throw new ServiceException("input is null");
            if (input.Length > 100)
                throw new ValidationException("input's max length is 100 symbols.");
            var context = ExecuteContextPool.GetGuestContext(guestId);
            return await context.Continue(input);
        }

        public async Task<Tuple<InterpreterCondition, string>> Continue(string input, string userName)
        {
            if (input == null)
                throw new ServiceException("input is null");
            if (input.Length > 100)
                throw new ValidationException("input's max length is 100 symbols.");
            var context = ExecuteContextPool.GetUserContext(userName);
            return await context.Continue(input);
        }

        public async Task<Tuple<InterpreterCondition, string>> Execute(string code, int guestId)
        {
            if (code == null)
                throw new ServiceException("code is null");
            if (code.Length > 10000)
                throw new ValidationException("Code's max length is 10000 symbols.");
            var context = ExecuteContextPool.GetGuestContext(guestId);
            return await context.Execute(code);
        }

        public async Task<Tuple<InterpreterCondition, string>> Execute(string code, string userName)
        {
            if (code == null)
                throw new ServiceException("code is null");
            if (code.Length > 10000)
                throw new ValidationException("Code's max length is 10000 symbols.");
            var context = ExecuteContextPool.GetUserContext(userName);
            return await context.Execute(code);
        }

        public Tuple<InterpreterCondition, string> Stop(int guestId)
        {
            var context = ExecuteContextPool.GetGuestContext(guestId);
            return context.Stop();
        }

        public Tuple<InterpreterCondition, string> Stop(string userName)
        {
            var context = ExecuteContextPool.GetUserContext(userName);
            return context.Stop();
        }

        public void ClearOverTheLast(int seconds)
        {
            ExecuteContextPool.ClearOverTheLast(seconds);
        }
    }
}
