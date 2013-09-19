using System;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using NUnit.Framework;
using Should.Fluent;
using UIA.Extensions.AutomationProviders.Tables.Stubs;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders.Tables
{
    [TestFixture]
    public class TableProviderTest
    {
        private TableProvider _tableProvider;
        private TableInformationStub _tableInformationStub;

        [SetUp]
        public void SetUp()
        {
            _tableInformationStub = new TableInformationStub();
            _tableProvider = new TableProvider(_tableInformationStub);
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
            _tableInformationStub.AddRows(7);
            _tableProvider.RowCount.Should().Equal(7);
        }

        [Test]
        public void ItHasTheColumnCount()
        {
            _tableInformationStub.AddHeaders(Enumerable.Range(0, 42).Select(x => String.Empty).ToArray());
            _tableProvider.ColumnCount.Should().Equal(42);
        }

        [TestFixture]
        public class Headers
        {
            private TableProvider _tableProvider;
            private TableInformationStub _tableInformationStub;

            [SetUp]
            public void SetUp()
            {
                _tableInformationStub = new TableInformationStub();
                _tableProvider = new TableProvider(_tableInformationStub);
            }

            [Test]
            public void ArePresentIfThereAreHeaders()
            {
                _tableInformationStub.AddHeaders("First Header", "Second Header");

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
                _tableInformationStub.AddHeaders("Some Header");
                _tableProvider.Navigate(NavigateDirection.FirstChild).Should().Be.OfType<HeaderProvider>();
            }
        }

        [TestFixture]
        public class DataItems
        {
            private TableProvider _tableProvider;
            private TableInformationStub _tableInformationStub;

            [SetUp]
            public void SetUp()
            {
                _tableInformationStub = new TableInformationStub();
                _tableProvider = new TableProvider(_tableInformationStub);
            }

            [Test]
            public void ArePresentForEachRow()
            {
                _tableInformationStub.AddRows(5);
                _tableProvider.Children.Count.Should().Equal(5);
            }

            [Test]
            public void AreWiredUpToOneAnother()
            {
                _tableInformationStub.AddRows(3);
                var theFirst = _tableProvider.FirstChild();
                var theSecond = theFirst.NextSibling();
                var theLast = _tableProvider.LastChild();

                theSecond.NextSibling().Should().Be.SameAs(theLast);
            }

            [Test]
            public void HaveValuesOnThem()
            {
                _tableInformationStub.AddRows(2);
                _tableProvider.Children.Select(x => x.Name).Should().Equal(new[] { "Row0", "Row1" });
            }

            [Test]
            public void CanBeAdded()
            {
                _tableInformationStub.AddRows(3);
                _tableProvider.Children.Count.Should().Equal(3);

                _tableInformationStub.AddRows(2);
                _tableProvider.Children.Count.Should().Equal(5);
            }
        }
    }
}
