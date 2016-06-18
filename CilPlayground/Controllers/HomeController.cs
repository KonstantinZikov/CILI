using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface;
using BLL;
namespace CilPlayground.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View("Home");
        }       
    }
}