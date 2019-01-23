using System.Linq;
using DryIoc;
using FluentAssertions;
using ReactiveUI;
using Splat;
using Xunit;

namespace SplatDependencyResolversTests
{
    public class DryIocTests
    {
        [Fact]
        public void Should_Register_ReactiveUI_Stuff()
        {
            var container = new Container();
            var scheduler = RxApp.MainThreadScheduler; // invoke RxApp static constructor
            Locator.Current = new FuncDependencyResolver(
                (service, name) => container.ResolveMany(service, serviceKey: name),
                (factory, type, name) => container.RegisterDelegate(type, r => factory(), serviceKey: name));

            var converters = container.ResolveMany<IBindingTypeConverter>().ToList();

            converters.Should().NotBeEmpty();
            converters.Should().HaveCount(3);
            converters.Should().Contain(x => x.GetType() == typeof(ComponentModelTypeConverter));
            converters.Should().Contain(x => x.GetType() == typeof(StringConverter));
            converters.Should().Contain(x => x.GetType() == typeof(EqualityTypeConverter));
        }
    }
}