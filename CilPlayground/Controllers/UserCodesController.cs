using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CilPlayground.Controllers
{
    public class UserCodesController : Controller
    {
        // GET: UserCodes
        public ActionResult Index()
        {
            return View("UserCodes");
        }
    }
}