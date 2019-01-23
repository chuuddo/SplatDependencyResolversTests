using System.Collections.Generic;
using System.Linq;
using Autofac;
using FluentAssertions;
using ReactiveUI;
using Splat;
using Splat.Autofac;
using Xunit;

namespace SplatDependencyResolversTests
{
    public class AutofacTests
    {
        [Fact]
        public void Should_Register_ReactiveUI_Stuff()
        {
            var builder = new ContainerBuilder();
            var scheduler = RxApp.MainThreadScheduler; // invoke RxApp static constructor
            var container = builder.Build();
            Locator.Current = new AutofacDependencyResolver(container);

            var converters = container.Resolve<IEnumerable<IBindingTypeConverter>>().ToList();

            converters.Should().NotBeEmpty();
            converters.Should().HaveCount(3);
            converters.Should().Contain(x => x.GetType() == typeof(ComponentModelTypeConverter));
            converters.Should().Contain(x => x.GetType() == typeof(StringConverter));
            converters.Should().Contain(x => x.GetType() == typeof(EqualityTypeConverter));
        }

        [Fact]
        public void Should_Register_ReactiveUI_Stuff_With_Update_Added()
        {
            var builder = new ContainerBuilder();
            var scheduler = RxApp.MainThreadScheduler; // invoke RxApp static constructor
            var container = builder.Build();
            Locator.Current = new AutofacDependencyResolverWithUpdate(container);

            var converters = container.Resolve<IEnumerable<IBindingTypeConverter>>().ToList();

            converters.Should().NotBeEmpty();
            converters.Should().HaveCount(3);
            converters.Should().Contain(x => x.GetType() == typeof(ComponentModelTypeConverter));
            converters.Should().Contain(x => x.GetType() == typeof(StringConverter));
            converters.Should().Contain(x => x.GetType() == typeof(EqualityTypeConverter));
        }
    }
}