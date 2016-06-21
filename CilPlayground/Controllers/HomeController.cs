using System.Web.Mvc;
using System.Web.Security;

namespace CilPlayground.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            if (Roles.IsUserInRole("User") || Roles.IsUserInRole("Admin"))
                return RedirectToAction("Index", "Interpreter");
            return View("Home");
        }       
    }
}