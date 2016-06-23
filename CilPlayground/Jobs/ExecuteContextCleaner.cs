using System.Web.Mvc;
using BLL.Interface.Services;
using Quartz;

namespace CilPlayground.Jobs
{
    public class ExecuteContextCleaner : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var executeService = System.Web.Mvc.DependencyResolver.Current.GetService<IExecuteService>();
            executeService.ClearOverTheLast();
        }
    }
}