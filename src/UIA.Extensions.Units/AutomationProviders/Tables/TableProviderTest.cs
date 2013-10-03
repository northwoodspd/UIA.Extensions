using System;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using FluentAssertions;
using NUnit.Framework;
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
                .ShouldBeEquivalentTo(ControlType.Table.Id);
        }

        [Test]
        public void ItLocalizesTheControlTypeAsWell()
        {
            _tableProvider.GetPropertyValue(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id)
                .ShouldBeEquivalentTo(ControlType.Table.LocalizedControlType);
        }

        [Test]
        public void ItIsBothOfTypeGridAndOfTypeTable()
        {
            _tableProvider.GetPatternProvider(TablePatternIdentifiers.Pattern.Id)
                .Should().BeSameAs(_tableProvider);

            _tableProvider.GetPatternProvider(TablePatternIdentifiers.Pattern.Id)
                .Should().BeSameAs(_tableProvider);
        }

        [Test]
        public void ItHousesSelections()
        {
            _tableProvider.GetPatternProvider(SelectionPatternIdentifiers.Pattern.Id)
                .Should().BeSameAs(_tableProvider);
        }

        [Test]
        public void ItHasTheRowCount()
        {
            _tableInformationStub.AddRows(7);
            _tableProvider.RowCount.ShouldBeEquivalentTo(7);
        }

        [Test]
        public void ItHasTheColumnCount()
        {
            _tableInformationStub.AddHeaders(Enumerable.Range(0, 42).Select(x => String.Empty).ToArray());
            _tableProvider.ColumnCount.ShouldBeEquivalentTo(42);
        }

        [Test]
        public void ItKnowsIfMultipleSelectionsAreSupported()
        {
            _tableInformationStub.CanSelectMultiple = true;
            _tableProvider.CanSelectMultiple.Should().BeTrue();
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
                    .Should().BeOfType<HeaderProvider>();
            }

            [Test]
            public void IsMissingIfThereAreNonetoSpeakOf()
            {
                _tableProvider.Navigate(NavigateDirection.FirstChild)
                    .Should().BeNull();
            }

            [Test]
            public void HeadersCanBeLazilyLoaded()
            {
                _tableProvider.Navigate(NavigateDirection.FirstChild).Should().BeNull();
                _tableInformationStub.AddHeaders("Some Header");
                _tableProvider.Navigate(NavigateDirection.FirstChild).Should().BeOfType<HeaderProvider>();
            }

            [Test]
            public void AreExposedDirectlyAsWell()
            {
                _tableInformationStub.AddHeaders("First Header", "Second Header");
                _tableProvider.GetColumnHeaders().Select(HeaderValue)
                              .ShouldBeEquivalentTo(new[] {"First Header", "Second Header"});
            }

            public string HeaderValue(IRawElementProviderSimple element)
            {
                return element.GetPropertyValue(AutomationElementIdentifiers.NameProperty.Id).ToString();
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
                _tableProvider.Children.Count.ShouldBeEquivalentTo(5);
            }

            [Test]
            public void AreWiredUpToOneAnother()
            {
                _tableInformationStub.AddRows(3);
                var theFirst = _tableProvider.FirstChild();
                var theSecond = theFirst.NextSibling();
                var theLast = _tableProvider.LastChild();

                theSecond.NextSibling().Should().BeSameAs(theLast);
            }

            [Test]
            public void HaveValuesOnThem()
            {
                _tableInformationStub.AddRows(2);
                _tableProvider.Children.Select(x => x.Name).ShouldBeEquivalentTo(new[] { "Row0", "Row1" });
            }

            [Test]
            public void CanBeAdded()
            {
                _tableInformationStub.AddRows(3);
                _tableProvider.Children.Count.ShouldBeEquivalentTo(3);

                _tableInformationStub.AddRows(2);
                _tableProvider.Children.Count.ShouldBeEquivalentTo(5);
            }
        }
    }
}
