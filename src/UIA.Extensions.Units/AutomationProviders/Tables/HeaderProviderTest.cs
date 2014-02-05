using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using FluentAssertions;
using NUnit.Framework;

namespace UIA.Extensions.AutomationProviders.Tables
{
    [TestFixture]
    public class HeaderProviderTest
    {
        private List<string> _headers;

        [SetUp]
        public void SetUp()
        {
            _header = null;
            _headers = new List<string>();
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
            _headers.AddRange(new[] { "first", "second", "other" });
            HeaderProvider.Children.TrueForAll(x => x.GetType() == typeof(HeaderItemProvider)).Should().BeTrue();
        }

        [Test]
        public void ChildrenHaveTheCorrectRuntimeIds()
        {
            _headers.AddRange(new[] { "one", "two", "three" });
            HeaderProvider.Children.Select(x => x.RuntimeId).Should().Equal(new[] { 0, 1, 2 });
        }

        [Test]
        public void TheHeaderStringsAreTheChildren()
        {
            _headers.AddRange(new[] { "first", "second", "other" });
            HeaderProvider.Children.Select(x => x.Name).ShouldBeEquivalentTo(new[] { "first", "second", "other" });
        }

        [Test]
        public void SameHeadersAreEqualInTheEyesOfUs()
        {
            var firstHeader = new HeaderProvider(null, new[] { "first", "second" });
            var secondHeader = new HeaderProvider(null, new[] { "first", "second" });
            firstHeader.ShouldBeEquivalentTo(secondHeader);
        }

        private HeaderProvider _header;
        private HeaderProvider HeaderProvider
        {
            get { return _header ?? (_header = new HeaderProvider(null, _headers)); }
        }
    }
}
