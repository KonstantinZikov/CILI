using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IDocumentService
    {
        DocumentEntity Get(int id);
        IEnumerable<DocumentEntity> GetAllEntities();
        void Create(DocumentEntity instruction);
        void CreateOrUpdate(DocumentEntity instruction);
        void Update(DocumentEntity instruction);
        void Delete(DocumentEntity instruction);
        DocumentEntity GetByName(string name, string userName);
        IEnumerable<DocumentEntity> GetAllByUserName(string userName);
        IEnumerable<string> GetAllNames(string userName);
        IEnumerable<DocumentEntity> GetExamplesWithoutCode();
        DocumentEntity GetExample(string name);
    }
}
