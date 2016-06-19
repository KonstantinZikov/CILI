using BLL.Interface.Exceptions;
using BLL.Interface.Services;
using CilPlayground.Infrastructure.Mappers;
using CilPlayground.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CilPlayground.Controllers
{
    public class AdminController : Controller
    {
        public AdminController(IUserService service)
        {
            _service = service;
        }

        readonly private IUserService _service;

        public ActionResult Index()
        {
            var models = new List<UserViewModel>
                (_service.GetAllEntities().Select(e => e.ToModel())).OrderBy(e => e.Name).ToList();
            return View("Admin", models);
        }

        [HttpPost]
        public ActionResult Create(UserViewModel model)
        {
            try
            {
                _service.Create(model.ToBll());
            }
            catch (ServiceException ex)
            {
                return Json(new { answer = ex.Message, success = false });
            }
            return Json(new { answer = "User added successfully.", success = true });
        }

        [HttpPost]
        public ActionResult Delete(UserViewModel model)
        {
            try
            {
                _service.Delete(model.ToBll());
            }
            catch (ServiceException ex)
            {
                return Json(new { answer = ex.Message, success = false });
            }
            return Json(new { answer = "User deleted successfully.", success = true });
        }

        [HttpPost]
        public ActionResult Update(UserViewModel model)
        {
            try
            {
                _service.Update(model.ToBll());
            }
            catch (ServiceException ex)
            {
                return Json(new { answer = ex.Message, success = false });
            }
            return Json(new { answer = "User updated successfully.", success = true });
        }
    }
}