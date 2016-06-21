using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Services;
using CilPlayground.Infrastructure.Mappers;
using CilPlayground.Models;
using BLL.Interface.Exceptions;

namespace CilPlayground.Controllers
{
    public class InstructionsController : Controller
    {
        public InstructionsController(IInstructionService service)
        {
            _service = service;
        }

        private readonly IInstructionService _service;
      
        public ActionResult Index()
        {
            var models = new List<InstructionViewModel>
                (_service.GetAllEntities().Select(e=>e.ToModel())).OrderBy(e=>e.Name).ToList();
            if (HttpContext.User.IsInRole("Admin"))
                return View("InstructionsEdit", models);
            return View("Instructions",models);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(InstructionViewModel model)
        {
            try
            {
                _service.Create(model.ToBll());
            }
            catch(ServiceException ex)
            {
                return Json(new { answer = ex.Message, success = false });
            }
            return Json(new { answer = "Instruction added successfully.",success = true });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(InstructionViewModel model)
        {
            try
            {
                _service.Delete(model.ToBll());
            }
            catch (ServiceException ex)
            {
                return Json(new { answer = ex.Message, success = false });
            }
            return Json(new { answer = "Instruction deleted successfully.", success = true });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Update(InstructionViewModel model)
        {
            try
            {
                _service.Update(model.ToBll());
            }
            catch (ServiceException ex)
            {
                return Json(new { answer = ex.Message, success = false });
            }
            return Json(new { answer = "Instruction updated successfully.", success = true });
        }
    }
}