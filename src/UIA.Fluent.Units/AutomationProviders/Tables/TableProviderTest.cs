using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;
using NUnit.Framework;
using Should.Fluent;

namespace UIA.Fluent.AutomationProviders.Tables
{
    [TestFixture]
    public class TableProviderTest
    {
        private TableProvider _tableProvider;
        private FakeTableInformation _tableInformation;

        [SetUp]
        public void SetUp()
        {
            _tableInformation = new FakeTableInformation();
            _tableProvider = new TableProvider(_tableInformation);
        }

        [Test]
        public void ItHasTheTableControlType()
        {
            _tableProvider.GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .Should().Equal(ControlType.Table.Id);
        }

        [Test]
        public void ItIsBothOfTypeGridAndOfTypeTable()
        {
            _tableProvider.GetPatternProvider(TablePatternIdentifiers.Pattern.Id)
                .Should().Be.SameAs(_tableProvider);

            _tableProvider.GetPatternProvider(TablePatternIdentifiers.Pattern.Id)
                .Should().Be.SameAs(_tableProvider);
        }

        [Test]
        public void ItHasTheRowCount()
        {
            _tableInformation.RowCount = 7;
            _tableProvider.RowCount.Should().Equal(7);
        }

        [Test]
        public void ItHasTheColumnCount()
        {
            _tableInformation.ColumnCount = 42;
            _tableProvider.ColumnCount.Should().Equal(42);
        }

        [TestFixture]
        public class Headers
        {
            private TableProvider _tableProvider;
            private FakeTableInformation _tableInformation;

            [SetUp]
            public void SetUp()
            {
                _tableInformation = new FakeTableInformation();
                _tableProvider = new TableProvider(_tableInformation);
            }

            [Test]
            public void ArePresentIfThereAreHeaders()
            {
                _tableInformation.AddHeaders("First Header", "Second Header");

                _tableProvider.Navigate(NavigateDirection.FirstChild)
                    .Should().Be.OfType<HeaderProvider>();
            }

            [Test]
            public void IsMissingIfThereAreNonetoSpeakOf()
            {
                _tableProvider.Navigate(NavigateDirection.FirstChild)
                    .Should().Be.Null();
            }

            [Test]
            public void HeadersCanBeLazilyLoaded()
            {
                _tableProvider.Navigate(NavigateDirection.FirstChild).Should().Be.Null();
                _tableInformation.AddHeaders("Some Header");
                _tableProvider.Navigate(NavigateDirection.FirstChild).Should().Be.OfType<HeaderProvider>();
            }
        }
    }

    public class FakeTableInformation : TableInformation
    {
        private readonly List<string> _headers;

        public FakeTableInformation()
        {
            _headers = new List<string>();
        }

        public int RowCount { get; set; }
        public int ColumnCount { get; set; }

        public Control Control
        {
            get { return new Control(); }
        }

        public List<string> Headers
        {
            get { return _headers; }
        }

        public void AddHeaders(params string[] headers)
        {
            _headers.AddRange(headers);
        }
    }
}
