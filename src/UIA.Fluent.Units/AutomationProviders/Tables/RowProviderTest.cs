using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using Moq;
using NUnit.Framework;
using Should.Fluent;

namespace UIA.Fluent.AutomationProviders.Tables
{
    [TestFixture]
    public class RowProviderTest
    {
        private FakeTableInformation.FakeRowInformation _rowInformation;
        private Mock<AutomationProvider> _parent;
        private readonly List<string> _values = new List<string>();

        [SetUp]
        public void SetUp()
        {
            _provider = null;
            _rowInformation = new FakeTableInformation.FakeRowInformation {Values = _values};
            _parent = new Mock<AutomationProvider>();
        }

        [Test]
        public void ItIsOfTheDataItemIlk()
        {
            RowProvider.GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .Should().Equal(ControlType.DataItem.Id);
        }

        [Test]
        public void ItDoublesAsASelectionItem()
        {
            RowProvider.Should().Be.AssignableFrom<ISelectionItemProvider>();
            RowProvider.GetPatternProvider(SelectionItemPatternIdentifiers.Pattern.Id).Should()
                .Be.SameAs(RowProvider);
        }

        [Test]
        public void TheRowValuesAreTheChildren()
        {
            _values.AddRange(new[] { "item 1", "item 2", "item 3" });
            RowProvider.Children.Count.Should().Equal(3);
            RowProvider.Children.Select(x => x.Name).Should().Equal(new[] { "item 1", "item 2", "item 3" });
        }

        private RowProvider _provider;
        private RowProvider RowProvider
        {
            get
            {
               return _provider ?? (_provider = new RowProvider(_parent.Object, _rowInformation));
            }
        }

    }
}
