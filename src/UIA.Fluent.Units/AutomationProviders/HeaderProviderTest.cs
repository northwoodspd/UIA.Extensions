using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using NUnit.Framework;
using Should.Fluent;

namespace UIA.Fluent.AutomationProviders
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
        public void FirstHeaderIsTheFirstChild()
        {
            _headers.Add("First Column");
            var firstChild = HeaderProvider.Navigate(NavigateDirection.FirstChild) as HeaderItemProvider;
            firstChild.Name.Should().Equal("First Column");
        }

        [Test]
        public void LastHeaderIsTheLastChild()
        {
            _headers.Add("First Column");
            _headers.Add("Last Column");
            var lastChild = HeaderProvider.Navigate(NavigateDirection.LastChild) as HeaderItemProvider;
            lastChild.Name.Should().Equal("Last Column");
        }

        private HeaderProvider _header;
        private HeaderProvider HeaderProvider
        {
            get { return _header ?? (_header = new HeaderProvider(null, _headers)); }
        }
    }
}
