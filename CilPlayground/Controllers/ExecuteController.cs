using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.Interface.Services;

namespace CilPlayground.Controllers
{
    public class ExecuteController : Controller
    {
        private readonly IExecuteService _executeService;

        public ExecuteController(IExecuteService executeService)
        {
            _executeService = executeService;
        }

        [HttpPost]
        public async Task<ActionResult> Execute(string code)
        {
            var bytes = new byte[16];
            var result = await _executeService.Execute(code,new Guid(bytes));
            return Json(new { condition = result.Item1.ToString(), output = result.Item2 });
        }

        [HttpPost]
        public async Task<ActionResult> Continue(string input)
        {
            var bytes = new byte[16];
            var result = await _executeService.Continue(input, new Guid(bytes));
            return Json(new { condition = result.Item1.ToString(), output = result.Item2 });
        }

        [HttpPost]
        public ActionResult Stop()
        {
            var bytes = new byte[16];
            var result = _executeService.Stop(new Guid(bytes));
            return Json(new { condition = result.Item1.ToString(), output = result.Item2 });
        }
    }
}