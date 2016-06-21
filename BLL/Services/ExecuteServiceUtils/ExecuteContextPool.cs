using System;
using System.Collections.Concurrent;

namespace BLL.Services.ExecuteServiceUtils
{
    internal static class ExecuteContextPool
    {

        private static readonly ConcurrentDictionary<string, ExecuteContext> UserContextMap 
            = new ConcurrentDictionary<string, ExecuteContext>();
        private static readonly ConcurrentDictionary<Guid, ExecuteContext> GuestContextMap 
            = new ConcurrentDictionary<Guid, ExecuteContext>();

        public static ExecuteContext GetUserContext(string userName)
        {
            ExecuteContext value;
            if (!UserContextMap.TryGetValue(userName, out value))
            {
                value = new ExecuteContext();
                if (!UserContextMap.TryAdd(userName, value))
                {
                    UserContextMap.TryGetValue(userName, out value);
                }                           
            }
            return value;
        }

        public static ExecuteContext GetGuestContext(Guid guid)
        {
            ExecuteContext value;
            if (!GuestContextMap.TryGetValue(guid, out value))
            {
                value = new ExecuteContext();
                if (!GuestContextMap.TryAdd(guid, value))
                {
                    GuestContextMap.TryGetValue(guid, out value);
                }
            }
            return value;
        }
    }
}