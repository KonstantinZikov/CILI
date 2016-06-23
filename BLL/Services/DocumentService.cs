using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using DAL.Interfaces.DTO;
using BLL.Interface.Exceptions;
using Dal.Interfaces;

namespace BLL.Services
{
    public class DocumentService : BaseService<DocumentEntity, DalDocument>, IDocumentService
    {
        private readonly IRepository<DalUser> _userRepository;

        public DocumentService(IUnitOfWork unitOfWork,
            IDocumentRepository repository, IRepository<DalUser> userRepository)
            : base(unitOfWork, repository)
        {
            _userRepository = userRepository;
        }

        protected override DocumentEntity ToBll(DalDocument dal)
        {
            if (dal == null) return null;
            return new DocumentEntity()
            {
                Id = dal.Id,
                Name = dal.Name,
                UserId = dal.UserId,
                Code = dal.Code,
                Description = dal.Description,
                IsExample = dal.IsExample,
                LastChangeTime = dal.LastChangeTime
            };
        }

        protected override DalDocument ToDal(DocumentEntity bll)
        {
            if (bll == null) return null;
            return new DalDocument()
            {
                Id = bll.Id,
                Name = bll.Name,
                UserId = bll.UserId,
                Code = bll.Code,
                Description = bll.Description,
                IsExample = bll.IsExample,
                LastChangeTime = bll.LastChangeTime
            };
        }

        protected override void Validate(DocumentEntity entity)
        {
            if (entity.Id < 0)
                throw new ServiceException(
                    $"Document id must be greater then zero, but it is {entity.Id}");
            if (entity.UserId < 0)
                throw new ServiceException(
                    $"Document UserId must be greater then zero, but it is {entity.Id}");
            if (entity.Name == null)
                throw new ServiceException("Document name is null.");
            if (entity.Code?.Length > 10000)          
                throw new ValidationException("Code's max length is 10000 symbols.");
            if (entity.Description?.Length > 1000)
                throw new ValidationException("Description's max length is 1000 symbols.");
        }

        public DocumentEntity GetByName(string name, string userName)
        {
            var user = _userRepository.GetByPredicate(u => u.Name == userName).FirstOrDefault();
            if (user == null)
                throw new ServiceException($"Unknown user {userName}");

            return ToBll(Repository.GetByPredicate(d=>d.Name == name)
                .FirstOrDefault(d => d.UserId == user.Id));
        }

        public IEnumerable<string> GetAllNames(string userName)
        {
            var user = _userRepository.GetByPredicate(u => u.Name == userName).FirstOrDefault();
            if (user == null)
                throw new ServiceException($"Unknown user {userName}");
            return Repository.GetByPredicate(d => d.UserId == user.Id)
                .OrderBy(d=>d.LastChangeTime)
                .Select(d => d.Name);
        }

        public void CreateOrUpdate(DocumentEntity entity)
        {
            var existing = Repository.GetByPredicate(d => d.UserId == entity.UserId)
                .FirstOrDefault(d=>d.Name == entity.Name);
            if (existing == null)
            {
                Create(entity);
            }               
            else
            {
                entity.Id = existing.Id;
                Update(entity);
            }               
        }

        public IEnumerable<DocumentEntity> GetAllByUserName(string name)
        {
            var user = _userRepository.GetByPredicate(u => u.Name == name).FirstOrDefault();
            if (user == null)
                throw new ServiceException($"Unknown user {name}");
            return Repository.GetByPredicate(d => d.UserId == user.Id).Select(ToBll);
        }

        public IEnumerable<DocumentEntity> GetExamplesWithoutCode()
        {
            return (Repository as IDocumentRepository)
                .GetWithoutCodeAndDescription(d => d.IsExample == true).Select(ToBll);
        }

        public DocumentEntity GetExample(string name)
        {
            var doc = Repository.GetByPredicate(d => d.Name == name)
                .FirstOrDefault(d => d.IsExample);
            return ToBll(doc);
        }
    }
}
