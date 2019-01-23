using System.Linq;
using FluentAssertions;
using ReactiveUI;
using SimpleInjector;
using Splat;
using Splat.SimpleInjector;
using Xunit;

namespace SplatDependencyResolversTests
{
    public class SimpleInjectorTests
    {
        [Fact]
        public void Should_Register_ReactiveUI_Stuff()
        {
            var container = new Container();
            var scheduler = RxApp.MainThreadScheduler; // invoke RxApp static constructor
            Locator.Current = new SimpleInjectorDependencyResolver(container);

            var converters = container.GetAllInstances<IBindingTypeConverter>().ToList();

            converters.Should().NotBeEmpty();
            converters.Should().HaveCount(3);
            converters.Should().Contain(x => x.GetType() == typeof(ComponentModelTypeConverter));
            converters.Should().Contain(x => x.GetType() == typeof(StringConverter));
            converters.Should().Contain(x => x.GetType() == typeof(EqualityTypeConverter));
        }
    }
}