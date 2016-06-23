using BLL.Interface.Exceptions;
using BLL.Interface.Services;
using CilPlayground.Infrastructure.Mappers;
using CilPlayground.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace CilPlayground.Controllers
{
    public class UserController : Controller
    {
        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        [HttpPost]
        public ActionResult SignUp(UserViewModel model)
        {
            var firstOrDefault = _roleService
                .GetAllEntities().FirstOrDefault(r => r.Name == "User");
            if (firstOrDefault != null)
            {
                var roleId = firstOrDefault.Id;
                model.RegistrationTime = DateTime.Now;
                model.RoleId = roleId;
            }
            try
            {
                _userService.Create(model.ToBll());
            }
            catch (ServiceException ex)
            {
                return Json(new { answer = "INTERNAL ERROR: " + ex.Message, success = false });
            }
            catch (ValidationException ex)
            {
                return Json(new { answer = ex.Message, success = false });
            }
            FormsAuthentication.SetAuthCookie(model.Name,true);
            return Json(new { answer = "Registred successfully.", success = true });
        }

        [HttpPost]
        public ActionResult SignIn(UserViewModel model)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Json(new { answer = "Already signed in.", success = false });
            }        
            try
            {
                if (_userService.CheckPassword(model.Name, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return Json(new { answer = "Login successfully.", success = true });
                }                                
                return Json(new { answer = "Wrong name or password. Try again.", success = false });
            }
            catch (ServiceException ex)
            {
                return Json(new { answer = "INTERNAL ERROR: " + ex.Message, success = false });
            }
            catch (ValidationException ex)
            {
                return Json(new { answer = ex.Message, success = false });
            }
            
        }

        [HttpPost]
        public ActionResult SignOut()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }
            return new EmptyResult();
        }
    }
}