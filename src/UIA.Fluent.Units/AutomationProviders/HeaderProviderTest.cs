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
            FirstChild.Name.Should().Equal("First Column");
        }

        [Test]
        public void LastHeaderIsTheLastChild()
        {
            _headers.AddRange(new[] { "First Column", "Last Column" });
            LastChild.Name.Should().Equal("Last Column");
        }

        [Test]
        public void HeaderItemsKnowAboutTheirSiblings()
        {
            _headers.AddRange(new[] { "First", "Second", "Third" });
            After(FirstChild).Name.Should().Equal("Second");
        }

        private HeaderItemProvider FirstChild
        {
            get { return HeaderProvider.Navigate(NavigateDirection.FirstChild) as HeaderItemProvider; }
        }

        private HeaderItemProvider LastChild
        {
            get { return HeaderProvider.Navigate(NavigateDirection.LastChild) as HeaderItemProvider; }
        }

        private static HeaderItemProvider After(HeaderItemProvider headerItem)
        {
            return headerItem.Navigate(NavigateDirection.NextSibling) as HeaderItemProvider;
        }

        private HeaderProvider _header;
        private HeaderProvider HeaderProvider
        {
            get { return _header ?? (_header = new HeaderProvider(null, _headers)); }
        }
    }
}
