using System.Collections.Generic;

namespace CilPlayground.ExecuteEngine
{
    public static class ExecuteContextPool
    {
        private static Dictionary<string, ExecuteContext> _userContextMap = new Dictionary<string, ExecuteContext>();
        private static Dictionary<string, ExecuteContext> _guestContextMap = new Dictionary<string, ExecuteContext>();

        public static ExecuteContext GetUserContext(string userName)
        {
            //TODO concurent+
            ExecuteContext value;
            if (!_userContextMap.TryGetValue(userName, out value))
            {
                value = new ExecuteContext();
                _userContextMap.Add(userName, value);
            }
            return value;
        }

        public static ExecuteContext GetGuestContext(string id)
        {
            //TODO concurent+
            ExecuteContext value;
            if (!_guestContextMap.TryGetValue(id, out value))
            {
                value = new ExecuteContext();
                _guestContextMap.Add(id, value);
            }
            return value;
        }
    }
}