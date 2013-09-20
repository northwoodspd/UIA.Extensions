using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using NUnit.Framework;
using Should.Fluent;

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
                .Should().Equal(ControlType.Header.Id);
        }

        [Test]
        public void ItHasHeaderItemChildren()
        {
            _headers.AddRange(new[] { "first", "second", "other" });
            HeaderProvider.Children.TrueForAll(x => x.GetType() == typeof (HeaderItemProvider)).Should().Be.True();
        }

        [Test]
        public void TheHeaderStringsAreTheChildren()
        {
            _headers.AddRange(new[] { "first", "second", "other" });
            HeaderProvider.Children.Select(x => x.Name).Should().Equal(new[] {"first", "second", "other"});
        }

        [Test]
        public void SameHeadersAreEqualInTheEyesOfUs()
        {
            var firstHeader = new HeaderProvider(null, new[] {"first", "second"});
            var secondHeader = new HeaderProvider(null, new[] {"first", "second"});
            firstHeader.Should().Equal(secondHeader);
        }

        private HeaderProvider _header;
        private HeaderProvider HeaderProvider
        {
            get { return _header ?? (_header = new HeaderProvider(null, _headers)); }
        }
    }
}
