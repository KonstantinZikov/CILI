using System.Data.Entity;
using DAL.Interfaces.DTO;
using ORM;
using System;
using System.Linq;
using System.Linq.Expressions;
using Dal.Interfaces;
using static DAL.ExpressionParameterReplacer.ParameterReplacer;

namespace DAL.Concrete
{
    public class DocumentRepository : BaseRepositpry<DalDocument,Document>, IDocumentRepository
    {
        public DocumentRepository(DbContext context) : base(context)
        {
            ToDal = (document) =>
                new DalDocument()
                {
                    Id = document.Id,
                    Name = document.Name,
                    UserId = document.UserId,
                    Code = document.Code,
                    Description = document.Description,
                    IsExample = document.IsExample,
                    LastChangeTime = document.LastChangeTime
                };
        }

        protected override Document ToOrm(DalDocument document)
        {
            if (document == null)
                throw new ArgumentNullException("document is null.");
            return new Document
            {
                Id = document.Id,
                Name = document.Name,
                UserId = document.UserId,
                Code = document.Code,
                Description = document.Description,
                IsExample = document.IsExample,
                LastChangeTime = document.LastChangeTime
            };
        }

        protected override void Update(DalDocument dal, Document document)
        {
            if (dal == null)
                throw new ArgumentNullException("dal is null.");
            if (document == null)
                throw new ArgumentNullException("document is null.");
            document.Name = dal.Name;
            document.UserId = dal.UserId;
            document.Code = dal.Code;
            document.LastChangeTime = dal.LastChangeTime;
            document.IsExample = dal.IsExample;
            document.Description = dal.Description;
        }

        public IQueryable<DalDocument> GetWithoutCodeAndDescription(Expression<Func<DalDocument,bool>> function)
        {
            var newExpr = Replace<DalDocument, Document>(function, typeof(Document));
            return _context.Set<Document>().Where(newExpr).Select(d => new DalDocument()
            {
                Id = d.Id,
                Name = d.Name,
                LastChangeTime = d.LastChangeTime,
                IsExample = d.IsExample
            });
        }
    }
}