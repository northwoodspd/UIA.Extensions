﻿using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using FluentAssertions;
using NUnit.Framework;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;
using UIA.Extensions.AutomationProviders.Tables.Stubs;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders.Tables
{
    [TestFixture]
    public class TableProviderTest
    {
        private TableProvider _tableProvider;
        private static TableInformationStub _tableInformationStub;

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
                .Should().BeEquivalentTo(ControlType.Table.Id);
        }

        [Test]
        public void ItLocalizesTheControlTypeAsWell()
        {
            _tableProvider.GetPropertyValue(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id)
                .Should().BeEquivalentTo(ControlType.Table.LocalizedControlType);
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
            _tableProvider.RowCount.Should().Be(7);
        }

        [Test]
        public void ItHasTheColumnCount()
        {
            ExpectHeaders(Enumerable.Range(0, 42).Select(x => string.Empty).ToArray());
            _tableProvider.ColumnCount.Should().Be(42);
        }

        [Test]
        public void ItKnowsIfMultipleSelectionsAreSupported()
        {
            _tableInformationStub.OverriddenCanSelectMultiple = true;
            _tableProvider.CanSelectMultiple.Should().BeTrue();
        }

        [Test]
        public void ImmediateChildrenHaveProperRuntimeIds()
        {
            ExpectHeaders("Header");
            _tableInformationStub.AddRows(5);
            _tableProvider.Children.Select(x => x.RuntimeId).Should().Equal(Enumerable.Range(0, 6));
        }

        [TestFixture]
        public class DataItems
        {
            private TableProvider _tableProvider;

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
                _tableProvider.Children.Count.Should().Be(5);
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
                _tableProvider.Children.Select(x => x.Name).Should().BeEquivalentTo("Row0", "Row1");
            }

            [Test]
            public void CanBeAdded()
            {
                _tableInformationStub.AddRows(3);
                _tableProvider.Children.Count.Should().Be(3);

                _tableInformationStub.AddRows(2);
                _tableProvider.Children.Count.Should().Be(5);
            }
        }

        [TestFixture]
        public class Headers
        {
            private TableProvider _tableProvider;

            [SetUp]
            public void SetUp()
            {
                _tableInformationStub = new TableInformationStub();
                _tableProvider = new TableProvider(_tableInformationStub);
            }

            [Test]
            public void ArePresentIfThereAreHeaders()
            {
                ExpectHeaders("First Header", "Second Header");

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
                ExpectHeaders("Some Header");
                _tableProvider.Navigate(NavigateDirection.FirstChild).Should().BeOfType<HeaderProvider>();
            }

            [Test]
            public void AreExposedDirectlyAsWell()
            {
                ExpectHeaders("First Header", "Second Header");
                _tableProvider.GetColumnHeaders().Select(HeaderValue)
                    .Should().BeEquivalentTo("First Header", "Second Header");
            }

            public string HeaderValue(IRawElementProviderSimple element)
            {
                return element.GetPropertyValue(AutomationElementIdentifiers.NameProperty.Id).ToString();
            }
        }

        private static void ExpectHeaders(params string[] headers)
        {
            _tableInformationStub.AddHeaders(headers.Select(x => new HeaderInformation { Text = x }).ToArray());
        }
    }
}
