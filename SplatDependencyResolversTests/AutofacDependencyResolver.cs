using System;
using Autofac;
using Splat.Autofac;

namespace SplatDependencyResolversTests
{
    public class AutofacDependencyResolverWithUpdate : AutofacDependencyResolver
    {
        private readonly IContainer _container;

        public AutofacDependencyResolverWithUpdate(IContainer container): base(container)
        {
            _container = container;
        }

        public override void Register(Func<object> factory, Type serviceType, string contract = null)
        {
            var builder = new ContainerBuilder();
            if (string.IsNullOrEmpty(contract))
            {
                builder.Register(x => factory()).As(serviceType).AsImplementedInterfaces();
            }
            else
            {
                builder.Register(x => factory()).Named(contract, serviceType).AsImplementedInterfaces();
            }
            builder.Update(_container);
        }
    }
}
