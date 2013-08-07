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
        private Mock<AutomationProvider> _parent;
        private FakeTableInformation.FakeCellInformation _cellInformation;

        [SetUp]
        public void SetUp()
        {
            _parent = new Mock<AutomationProvider>();
            _cellInformation = new FakeTableInformation.FakeCellInformation();
        }
        
        [TearDown]
        public void TearDown()
        {
            _cellProvider = null;
        }

        [Test]
        public void ItIsOfTheTextControlType()
        {
            CellProvider.GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .Should().Equal(ControlType.Text.Id);
        }

        [Test]
        public void TheNameIsTheValue()
        {
            _cellInformation.Value = "Expected Name";
            CellProvider.Name.Should().Equal("Expected Name");
        }

        [Test]
        public void ItDoubleAsATableItem()
        {
            CellProvider.Should().Be.AssignableFrom<ITableItemProvider>();
            CellProvider.GetPatternProvider(TableItemPatternIdentifiers.Pattern.Id)
                .Should().Be.SameAs(_cellProvider);
        }

        [Test]
        public void ItKnowsTheRow()
        {
            _cellInformation.Row = 7;
            CellProvider.Row.Should().Equal(7);
        }

        [Test]
        public void ItKnowsTheColumn()
        {
            _cellInformation.Column = 42;
            CellProvider.Column.Should().Equal(42);
        }

        private TableCellProvider _cellProvider;
        private TableCellProvider CellProvider
        {
            get { return _cellProvider ?? (_cellProvider = new TableCellProvider(_parent.Object, _cellInformation)); }
        }
    }
}
