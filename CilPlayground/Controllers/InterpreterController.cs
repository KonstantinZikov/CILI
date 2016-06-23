using CilPlayground.Models;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Services;
using CilPlayground.Infrastructure.Mappers;

namespace CilPlayground.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class InterpreterController : Controller
    {

        private readonly IDocumentService _documentService;

        public InterpreterController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public ActionResult Index()
        {
            DocumentViewModel document;
            var name = HttpContext.User.Identity.Name;
            var documents = _documentService.GetAllByUserName(name).Select(d => d.ToModel()).ToList();
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
            return View("Interpreter", model);
        }

        public ActionResult Document(string fileName)
        {
            var name = HttpContext.User.Identity.Name;
            var documents = _documentService.GetAllByUserName(name).Select(d => d.ToModel()).ToList();
            var model = new UserCodeViewModel()
            {
                Documents = documents,
                CurrentDocument = documents.Find(d => d.Name == fileName)
            };
            return View("Interpreter", model);
        }
    }
}