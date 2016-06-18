using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CilPlayground.Controllers
{
    public class InterpreterController : Controller
    {
        // GET: Interpreter
        public ActionResult Index()
        {
            return View("Interpreter");
        }
    }
}