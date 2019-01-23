using System.Linq;
using FluentAssertions;
using ReactiveUI;
using Splat;
using Xunit;

namespace SplatDependencyResolversTests
{
    public class LocatorTests
    {
        [Fact]
        public void Should_Register_ReactiveUI_Stuff()
        {
            var scheduler = RxApp.MainThreadScheduler; // invoke RxApp static constructor

            var converters = Locator.Current.GetServices<IBindingTypeConverter>().ToList();

            converters.Should().NotBeEmpty();
            converters.Should().HaveCount(3);
            converters.Should().Contain(x => x.GetType() == typeof(ComponentModelTypeConverter));
            converters.Should().Contain(x => x.GetType() == typeof(StringConverter));
            converters.Should().Contain(x => x.GetType() == typeof(EqualityTypeConverter));
        }
    }
}