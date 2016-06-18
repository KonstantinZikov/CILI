using System.Web.Mvc;

namespace CilPlayground.Controllers
{
    public class TryController : Controller
    {      
        // GET: TryPage
        public ActionResult Index()
        {            
            return View("Try");
        }
        
    }
}