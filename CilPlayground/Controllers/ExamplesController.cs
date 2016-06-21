using System.Web.Mvc;

namespace CilPlayground.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class ExamplesController : Controller
    {
        // GET: Examples
        public ActionResult Index()
        {
            return View("Examples");
        }
    }
}