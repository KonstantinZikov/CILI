using System.Web.Mvc;

namespace CilPlayground.Controllers
{
    public class ErrorController : Controller
    {
                
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}