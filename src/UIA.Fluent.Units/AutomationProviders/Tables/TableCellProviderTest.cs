using System.Windows.Automation;
using System.Windows.Automation.Provider;
using Moq;
using NUnit.Framework;
using Should.Fluent;

namespace UIA.Fluent.AutomationProviders.Tables
{
    [TestFixture]
    public class TableCellProviderTest
    {
        private TableCellProvider _cellProvider;
        private Mock<AutomationProvider> _parent;
        private FakeTableInformation.FakeCellInformation _cellInformation;

        [SetUp]
        public void SetUp()
        {
            _parent = new Mock<AutomationProvider>();
            _cellInformation = new FakeTableInformation.FakeCellInformation("Expected Name");
            _cellProvider = new TableCellProvider(_parent.Object, _cellInformation);
        }

        [Test]
        public void ItIsOfTheTextControlType()
        {
            _cellProvider.GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .Should().Equal(ControlType.Text.Id);
        }

        [Test]
        public void TheNameIsTheValue()
        {
            _cellProvider.Name.Should().Equal("Expected Name");
        }

        [Test]
        public void ItDoubleAsATableItem()
        {
            _cellProvider.Should().Be.AssignableFrom<ITableItemProvider>();
            _cellProvider.GetPatternProvider(TableItemPatternIdentifiers.Pattern.Id)
                .Should().Be.SameAs(_cellProvider);
        }
    }
}
