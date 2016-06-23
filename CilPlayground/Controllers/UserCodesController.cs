using System;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Exceptions;
using BLL.Interface.Services;
using CilPlayground.Infrastructure.Mappers;
using CilPlayground.Models;

namespace CilPlayground.Controllers
{
    [Authorize (Roles = "Admin, User")]
    public class UserCodesController : Controller
    {
        private readonly IDocumentService _documentService;
        private readonly IUserService _userService;

        public UserCodesController(IDocumentService documentService, IUserService userService)
        {
            _documentService = documentService;
            _userService = userService;
        }

        
        public ActionResult Index()
        {
            DocumentViewModel document;
            var name = HttpContext.User.Identity.Name;
            var documents = _documentService.GetAllByUserName(name).Select(d=>d.ToModel()).ToList();        
            if (documents.Any())
            {
                document = documents[0];
            }
            else
            {
                document = new DocumentViewModel()
                {
                    Name = "New file",
                    Code = "",
                    Description = ""
                };
            }
            var model = new UserCodeViewModel()
            {
                Documents = documents,
                CurrentDocument = document
            };
            return View("UserCodes",model);
        }

        public ActionResult Document(string fileName)
        {
            var name = HttpContext.User.Identity.Name;
            var documents = _documentService.GetAllByUserName(name).Select(d => d.ToModel()).ToList();
            var document = documents.Find(d => d.Name == fileName);
            if (document == null)
            {
                document = new DocumentViewModel()
                {
                    Code = "",
                    Description = ""
                };
            }
            var model = new UserCodeViewModel()
            {
                Documents = documents,
                CurrentDocument = document 
            };
            return View("UserCodes", model);
        }

        [HttpPost]
        public ActionResult GetFile(string fileName)
        {
            try
            {
                var userName = HttpContext.User.Identity.Name;
                var document = _documentService.GetByName(fileName, userName);
                if (document == null)
                    return Json(new { answer = "Document doesn't exist.", success = false });
                return Json(new {answer = document, success = true});
            }
            catch (ServiceException ex)
            {
                return Json(new { answer = "INTERNAL ERROR: " + ex.Message, success = false });
            }            
        }

        [HttpPost]
        public ActionResult Save(DocumentViewModel model)
        {
            try
            {                
                var document  = model.ToBll();
                document.UserId = _userService.Get(HttpContext.User.Identity.Name).Id;
                document.LastChangeTime = DateTime.Now;
                _documentService.CreateOrUpdate(document);
                return Json(new { answer = "ok", success = true });
            }
            catch (ServiceException ex)
            {
                return Json(new { answer = "INTERNAL ERROR: " + ex.Message, success = false });
            }
        }

        [HttpPost]
        public ActionResult Delete(string fileName)
        {
            try
            {
                var userName = HttpContext.User.Identity.Name;
                var document = _documentService.GetByName(fileName, userName);
                if (document == null)
                    return Json(new { answer = "Document doesn't exist.", success = false });
                _documentService.Delete(document);
                return Json(new { answer = document.Code, success = true });
            }
            catch (ServiceException ex)
            {
                return Json(new { answer = "INTERNAL ERROR: " + ex.Message, success = false });
            }
        }

        [HttpPost]
        public ActionResult Rename(string name,string newName)
        {
            try
            {
                var userName = HttpContext.User.Identity.Name;
                var document = _documentService.GetByName(name, userName);
                if (document == null)
                    return Json(new { answer = "Document doesn't exist.", success = false });
                document.Name = newName;
                _documentService.Update(document);
                return Json(new { answer = document.Code, success = true });
            }
            catch (ServiceException ex)
            {
                return Json(new { answer = "INTERNAL ERROR: " + ex.Message, success = false });
            }
        }
    }
}