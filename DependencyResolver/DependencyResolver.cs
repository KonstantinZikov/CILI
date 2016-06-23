using Ninject;
using ORM;
using BLL.Interface.Services;
using BLL.Services;
using DAL.Concrete;
using System.Data.Entity;
using BLL.Services.ExecuteServiceUtils;
using Dal.Interfaces;
using Ninject.Web.Common;
using DAL.Interfaces.DTO;

namespace DependencyResolver
{
    public static class DependencyResolver
    {
        public static void Configure(this IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<DbContext>().To<EntityModel>().InRequestScope();
           
            //Repositories
            kernel.Bind<IRepository<DalUser>>().To<UserRepository>();
            kernel.Bind<IRepository<DalInstruction>>().To<InstructionRepository>();
            kernel.Bind<IRepository<DalRole>>().To<RoleRepository>();
            kernel.Bind<IRepository<DalDocument>>().To<DocumentRepository>();
            kernel.Bind<IDocumentRepository>().To<DocumentRepository>();

            //Services
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IInstructionService>().To<InstructionService>();
            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IExecuteService>().To<ExecuteService>().InSingletonScope();
            kernel.Bind<IDocumentService>().To<DocumentService>();
            kernel.Bind<IEncryptService>().To<EncryptService>().InSingletonScope();
        }
    }
}
