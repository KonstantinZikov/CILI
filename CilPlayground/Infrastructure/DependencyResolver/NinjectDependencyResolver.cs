using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DependencyResolver;
using Ninject;

namespace CilPlayground.Infrastructure.DependencyResolver
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            kernel.Configure();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}