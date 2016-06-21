using System.Web.Mvc;

namespace CilPlayground.Controllers
{
    [Authorize (Roles = "Admin, User")]
    public class UserCodesController : Controller
    {
        // GET: UserCodes
        public ActionResult Index()
        {
            return View("UserCodes");
        }
    }
}