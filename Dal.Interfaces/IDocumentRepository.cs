using System;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfaces.DTO;

namespace Dal.Interfaces
{
    public interface IDocumentRepository : IRepository<DalDocument>
    {       
        IQueryable<DalDocument> GetWithoutCodeAndDescription
            (Expression<Func<DalDocument, bool>> function);
    }
}