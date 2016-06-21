using BLL.Interface.Exceptions;
using BLL.Interface.Services;
using CilPlayground.Infrastructure.Mappers;
using CilPlayground.Models;
using System.Linq;
using System.Web.Mvc;

namespace CilPlayground.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public AdminController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public ActionResult Index()
        {
            var users = _userService.GetAllEntities().Select(u=>u.ToModel())
                .OrderBy(e=>e.Name).ToList();
            var roles = _roleService.GetAllEntities().Select(r => r.ToModel())
                .OrderBy(e => e.Name).ToList();
            var model = new AdminViewModel
            {
                Users = users,
                Roles = roles
            };
            return View("Admin", model);
        }

        [HttpPost]
        public ActionResult Create(UserViewModel model)
        {
            try
            {
                _userService.Create(model.ToBll());
            }
            catch (ServiceException ex)
            {
                return Json(new { answer = "INTERNAL ERROR: " + ex.Message, success = false });
            }
            catch (UserException ex)
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
                _userService.Delete(model.ToBll());
            }
            catch (ServiceException ex)
            {
                return Json(new { answer = "INTERNAL ERROR: " + ex.Message, success = false });
            }
            catch (UserException ex)
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
                _userService.Update(model.ToBll());
            }
            catch (ServiceException ex)
            {
                return Json(new { answer = "INTERNAL ERROR: " + ex.Message, success = false });
            }
            catch (UserException ex)
            {
                return Json(new { answer = ex.Message, success = false });
            }
            return Json(new { answer = "User updated successfully.", success = true });
        }
    }
}