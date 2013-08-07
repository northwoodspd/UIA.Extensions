using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;
using NUnit.Framework;
using Should.Fluent;
using UIA.Fluent.AutomationProviders.Tables;

namespace UIA.Fluent.AutomationProviders
{
    [TestFixture]
    public class TableProviderTest
    {
        private static TableProvider _tableProvider;
        private static FakeTableInformation _tableInformation;

        [SetUp]
        public void SetUp()
        {
            _tableInformation = new FakeTableInformation();
        }

        [Test]
        public void ItHasTheTableControlType()
        {
            CreateTable();

            _tableProvider.GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .Should().Equal(ControlType.Table.Id);
        }

        [Test]
        public void ItIsBothOfTypeGridAndOfTypeTable()
        {
            CreateTable();

            _tableProvider.GetPatternProvider(TablePatternIdentifiers.Pattern.Id)
                .Should().Be.SameAs(_tableProvider);

            _tableProvider.GetPatternProvider(TablePatternIdentifiers.Pattern.Id)
                .Should().Be.SameAs(_tableProvider);
        }

        [Test]
        public void ItReturnsTheNumberOfRows()
        {
            CreateTable();

            _tableInformation.RowCount = 7;
            _tableProvider.RowCount.Should().Equal(7);
        }

        [TestFixture]
        public class Headers
        {
            [SetUp]
            public void SetUp()
            {
                _tableInformation = new FakeTableInformation();
            }

            [Test]
            public void ArePresentIfThereAreHeaders()
            {
                _tableInformation.AddHeaders("First Header", "Second Header");
                CreateTable();

                _tableProvider.Navigate(NavigateDirection.FirstChild)
                    .Should().Be.OfType<HeaderProvider>();
            }

            [Test]
            public void IsMissingIfThereAreNonetoSpeakOf()
            {
                CreateTable();
                _tableInformation.Headers.Count.Should().Equal(0);

                _tableProvider.Navigate(NavigateDirection.FirstChild)
                    .Should().Be.Null();
            }
        }

        private static void CreateTable()
        {
            _tableProvider = new TableProvider(_tableInformation);
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

        public Control Control
        {
            get { return new Control(); }
        }

        public IList<string> Headers
        {
            get { return _headers; }
        }

        public void AddHeaders(params string[] headers)
        {
            _headers.AddRange(headers);
        }
    }
}
