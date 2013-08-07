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
        private RowProvider _rowProvider;

        [SetUp]
        public void SetUp()
        {
            _rowInformation = new FakeTableInformation.FakeRowInformation();
            _parent = new Mock<AutomationProvider>();
            _rowProvider = new RowProvider(_parent.Object, _rowInformation);
        }

        [Test]
        public void ItIsOfTheDataItemIlk()
        {
            _rowProvider.GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .Should().Equal(ControlType.DataItem.Id);
        }

        [Test]
        public void ItDoublesAsASelectionItem()
        {
            _rowProvider.Should().Be.AssignableFrom<ISelectionItemProvider>();
            _rowProvider.GetPatternProvider(SelectionItemPatternIdentifiers.Pattern.Id).Should()
                .Be.SameAs(_rowProvider);
        }
    }
}
