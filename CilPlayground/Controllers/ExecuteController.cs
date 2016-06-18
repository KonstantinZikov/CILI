using CilPlayground.ExecuteEngine;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CilPlayground.Controllers
{
    public class ExecuteController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> Execute(string code)
        {
            var context = ExecuteContextPool.GetGuestContext("1");
            var result = await context.Execute(code);
            return Json(new { condition = result.Item1, output = result.Item2 });
        }

        [HttpPost]
        public async Task<ActionResult> Continue(string input)
        {
            var context = ExecuteContextPool.GetGuestContext("1");
            var result = await context.Continue(input);
            return Json(new { condition = result.Item1, output = result.Item2 });
        }

        [HttpPost]
        public ActionResult Stop()
        {
            var context = ExecuteContextPool.GetGuestContext("1");
            var result = context.Stop();
            return Json(new { condition = result.Item1, output = result.Item2 });
        }
    }
}