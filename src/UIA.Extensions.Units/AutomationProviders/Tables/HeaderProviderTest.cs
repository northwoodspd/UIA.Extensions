using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using FluentAssertions;
using NUnit.Framework;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;

namespace UIA.Extensions.AutomationProviders.Tables
{
    [TestFixture]
    public class HeaderProviderTest
    {
        private List<HeaderInformation> _headers;

        [SetUp]
        public void SetUp()
        {
            _header = null;
            _headers = new List<HeaderInformation>();
        }

        [Test]
        public void ItHasTheCorrectControlType()
        {
            HeaderProvider.GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .ShouldBeEquivalentTo(ControlType.Header.Id);
        }

        [Test]
        public void ItHasHeaderItemChildren()
        {
            _headers.AddRange(HeadersFor("first", "second", "other"));
            HeaderProvider.Children.TrueForAll(x => x.GetType() == typeof(HeaderItemProvider)).Should().BeTrue();
        }

        [Test]
        public void ChildrenHaveTheCorrectRuntimeIds()
        {
            _headers.AddRange(HeadersFor("one", "two", "three"));
            HeaderProvider.Children.Select(x => x.RuntimeId).Should().Equal(new[] { 0, 1, 2 });
        }

        [Test]
        public void TheHeaderStringsAreTheChildren()
        {
            _headers.AddRange(HeadersFor("first", "second", "other"));
            HeaderProvider.Children.Select(x => x.Name).ShouldBeEquivalentTo(new[] { "first", "second", "other" });
        }

        [Test]
        public void SameHeadersAreEqualInTheEyesOfUs()
        {
            var firstHeader = new HeaderProvider(null, new[] { new HeaderInformation("first"), new HeaderInformation("second") });
            var secondHeader = new HeaderProvider(null, new[] { new HeaderInformation("first"), new HeaderInformation("second") });
            firstHeader.ShouldBeEquivalentTo(secondHeader);
        }

        private HeaderProvider _header;

        private HeaderProvider HeaderProvider
        {
            get { return _header ?? (_header = new HeaderProvider(null, _headers)); }
        }

        private static IEnumerable<HeaderInformation> HeadersFor(params string[] headers)
        {
            return headers.Select(x => new HeaderInformation(x));
        }
    }
}
