using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Exceptions;
using BLL.Interface.Services;
using CilPlayground.Infrastructure.Mappers;
using CilPlayground.Models;

namespace CilPlayground.Controllers
{
    public class TryController : Controller
    {

        private readonly IDocumentService _documentService;

        public TryController(IDocumentService documentService)
        {
            _documentService = documentService;
        }  
        
        public ActionResult Index()
        {
            if (Roles.IsUserInRole("User") || Roles.IsUserInRole("Admin"))
                return RedirectToAction("Index", "Interpreter");
            DocumentViewModel document;
            var documents = _documentService.GetExamplesWithoutCode().ToList();
            if (documents.Any())
            {
               document = _documentService.Get(documents[0].Id).ToModel();
            }
            else
            {
                document = new DocumentViewModel()
                {
                    Name = "Empty example",
                    Code = string.Empty,
                    Description = "Sorry, but there are no examples:("
                };
            }
            return View("Try",document);
        }

        [HttpPost]
        public ActionResult NextExample(int index)
        {
            try
            {
                DocumentViewModel document;
                var documents = _documentService.GetExamplesWithoutCode().ToList();
                if (documents.Any())
                {
                    int count = documents.Count;
                    document = _documentService.Get(documents[index%count].Id).ToModel();
                }
                else
                {
                    document = new DocumentViewModel()
                    {
                        Name = "Empty example",
                        Code = string.Empty,
                        Description = "Sorry, but there are no examples:("
                    };
                }
                return Json(new {document = document, success = true});
            }
            catch (ServiceException ex)
            {
                return Json(new {answer = "INTERNAL ERROR: " + ex.Message, success = false});
            }
        }
    }
}