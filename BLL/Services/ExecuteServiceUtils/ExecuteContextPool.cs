using System;
using System.Collections.Concurrent;
using System.Linq;

namespace BLL.Services.ExecuteServiceUtils
{
    internal static class ExecuteContextPool
    {       
        private static readonly ConcurrentDictionary<string, Tuple<ExecuteContext,DateTime>> UserContextMap 
            = new ConcurrentDictionary<string, Tuple<ExecuteContext, DateTime>> ();
        private static readonly ConcurrentDictionary<int, Tuple<ExecuteContext, DateTime>> GuestContextMap 
            = new ConcurrentDictionary<int, Tuple<ExecuteContext, DateTime>>();

        public static ExecuteContext GetUserContext(string userName)
        {
            Tuple<ExecuteContext, DateTime> value;
            if (!UserContextMap.TryGetValue(userName, out value))
            {
                value = new Tuple<ExecuteContext, DateTime>(new ExecuteContext(),DateTime.Now);
                if (!UserContextMap.TryAdd(userName, value))
                {
                    UserContextMap.TryGetValue(userName, out value);
                }                           
            }
            return value.Item1;
        }

        public static ExecuteContext GetGuestContext(int token)
        {
            Tuple<ExecuteContext, DateTime> value;
            if (!GuestContextMap.TryGetValue(token, out value))
            {
                value = new Tuple<ExecuteContext, DateTime>(new ExecuteContext(), DateTime.Now);
                if (!GuestContextMap.TryAdd(token, value))
                {
                    GuestContextMap.TryGetValue(token, out value);
                }
            }
            return value.Item1;
        }

        public static void ClearOverTheLast(int seconds)
        {
            var old = GuestContextMap.Where(
                p => p.Value.Item2 < (DateTime.Now - TimeSpan.FromSeconds(seconds))
            );
            foreach (var element in old)
            {
                Tuple<ExecuteContext, DateTime> value;
                while(!GuestContextMap.TryRemove(element.Key,out value) && value != null);
            }
        }
    }
}