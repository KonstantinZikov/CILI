using System;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Exceptions;
using BLL.Interface.Services;
using CilPlayground.Infrastructure.Mappers;
using CilPlayground.Models;

namespace CilPlayground.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class ExamplesController : Controller
    {
        private readonly IDocumentService _documentService;

        public ExamplesController(IDocumentService documentService)
        {
            _documentService = documentService;
        }


        public ActionResult Index()
        {
            DocumentViewModel document;
            var documents = _documentService.GetExamplesWithoutCode()
                .Select(d=>d.ToModel()).ToList();
            if (documents.Any())
            {
                document = _documentService.Get(documents[0].Id).ToModel();
            }
            else
            {
                document = new DocumentViewModel()
                {
                    Name = "Empty Example",
                    Code = "",
                    Description = "Sorry, but there are no examples:("
                };
            }
            var model = new UserCodeViewModel()
            {
                Documents = documents,
                CurrentDocument = document
            };
            return View("Examples", model);
        }

        [HttpPost]
        public ActionResult GetFile(string fileName)
        {
            try
            {
                var document = _documentService.GetExample(fileName);
                if (document == null)
                    return Json(new { answer = $"Example with name {fileName} doesn't exist.",
                        success = false });
                return Json(new { answer = document, success = true });
            }
            catch (ServiceException ex)
            {
                return Json(new { answer = "INTERNAL ERROR: " + ex.Message, success = false });
            }
        }
    }
}