using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using CilPlayground.Infrastructure.Mappers;
using CilPlayground.Models;
using BLL.Interface.Entities;
using BLL.Interface.Exceptions;

namespace CilPlayground.Controllers
{
    public class InstructionsController : Controller
    {
        public InstructionsController(IInstructionService service)
        {
            _service = service;
        }

        readonly private IInstructionService _service;
      
        public ActionResult Index()
        {
            var models = new List<InstructionViewModel>
                (_service.GetAllEntities().Select(e=>e.ToModel())).OrderBy(e=>e.Name).ToList();
            return View("Instructions",models);
        }

        [HttpPost]
        public ActionResult Create(InstructionViewModel model)
        {
            try
            {
                _service.Create(model.ToBll());
            }
            catch(ServiceException ex)
            {
                return Json(new { answer = ex.Message, seccessfully = false });
            }
            return Json(new { answer = "Instruction added successfully.",seccessfully = true });
        }

        [HttpPost]
        public ActionResult Delete()
        {
            return Json(new { result = "Instruction deleted successfully." });
        }

        [HttpPost]
        public ActionResult Update(InstructionViewModel model)
        {
            try
            {
                _service.Update(model.ToBll());
            }
            catch (ServiceException ex)
            {
                return Json(new { answer = ex.Message, seccessfully = false });
            }
            return Json(new { answer = "Instruction updated successfully.", seccessfully = true });
        }
    }
}