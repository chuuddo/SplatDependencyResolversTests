using System.Linq;
using FluentAssertions;
using Ninject;
using ReactiveUI;
using Splat;
using Xunit;

namespace SplatDependencyResolversTests
{
    public class NinjectTests
    {
        [Fact]
        public void Should_Register_ReactiveUI_Stuff()
        {
            var kernel = new StandardKernel();
            var scheduler = RxApp.MainThreadScheduler; // invoke RxApp static constructor
            Locator.Current = new FuncDependencyResolver(
                (service, name) => name != null ? kernel.GetAll(service, name) : kernel.GetAll(service),
                (factory, service, name) =>
                {
                    var binding = kernel.Bind(service).ToMethod(c => factory());
                    if (name != null) binding.Named(name);
                });

            var converters = kernel.GetAll<IBindingTypeConverter>().ToList();

            converters.Should().NotBeEmpty();
            converters.Should().HaveCount(3);
            converters.Should().Contain(x => x.GetType() == typeof(ComponentModelTypeConverter));
            converters.Should().Contain(x => x.GetType() == typeof(StringConverter));
            converters.Should().Contain(x => x.GetType() == typeof(EqualityTypeConverter));
        }
    }
}