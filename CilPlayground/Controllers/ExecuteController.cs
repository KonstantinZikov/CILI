using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;

namespace CilPlayground.Controllers
{
    public class ExecuteController : Controller
    {
        private const string cookieName = "ExecutionInfo";
        private const int cookieMinutes = 5;

        private readonly IExecuteService _executeService;
        private readonly IEncryptService _encryptService;

        public ExecuteController(IExecuteService executeService, IEncryptService encryptService)
        {
            _encryptService = encryptService; 
            _executeService = executeService;
        }

        [HttpPost]
        public async Task<ActionResult> Execute(string code)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var result = await _executeService.Execute(code, HttpContext.User.Identity.Name);
                return Json(new { condition = result.Item1.ToString(), output = result.Item2 });
            }
            
            var cookie = Request.Cookies[cookieName];     
            if (cookie != null)
            {
                byte[] data = Convert.FromBase64String(cookie["id"]);
                int id = _encryptService.Decrypt(data);
                var result = await _executeService.Execute(code, id);
                return Json(new { condition = result.Item1.ToString(), output = result.Item2 });
            }
            else
            {
                int id = _executeService.GetNewId();
                byte[] data = _encryptService.Encrypt(id);
                var responceCookie = new HttpCookie(cookieName)
                {
                    Expires = DateTime.Now.AddMinutes(cookieMinutes)
                };
                responceCookie.Values.Add("id", Convert.ToBase64String(data));
                Response.Cookies.Add(responceCookie);

                var result = await _executeService.Execute(code, id);
                return Json(new { condition = result.Item1.ToString(), output = result.Item2 });
            }                              
                       
        }

        [HttpPost]
        public async Task<ActionResult> Continue(string input)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userResult = await _executeService.Continue(input, HttpContext.User.Identity.Name);
                return Json(new { condition = userResult.Item1.ToString(), output = userResult.Item2 });
            }

            var cookie = Request.Cookies[cookieName];
            if (cookie == null)
                return Json(new {condition = InterpreterCondition.Stopped, output = "Ivalid cookie."});          
            byte[] data = Convert.FromBase64String(cookie["id"]);
            int id = _encryptService.Decrypt(data);
            var result = await _executeService.Continue(input, id);
            return Json(new { condition = result.Item1.ToString(), output = result.Item2 });
        }

        [HttpPost]
        public ActionResult Stop()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userResult = _executeService.Stop(HttpContext.User.Identity.Name);
                return Json(new { condition = userResult.Item1.ToString(), output = userResult.Item2 });
            }

            var cookie = Request.Cookies[cookieName];
            if (cookie == null)
                return Json(new {condition = InterpreterCondition.Stopped, output = "Ivalid cookie."});
           
            byte[] data = Convert.FromBase64String(cookie["id"]);
            int id = _encryptService.Decrypt(data);
            var result = _executeService.Stop(id);
            return Json(new { condition = result.Item1.ToString(), output = result.Item2 });           
        }
    }
}