using System.Windows.Automation;
using System.Windows.Automation.Provider;
using Moq;
using NUnit.Framework;
using Should.Fluent;
using UIA.Fluent.AutomationProviders.Tables.Stubs;

namespace UIA.Fluent.AutomationProviders.Tables
{
    [TestFixture]
    public class TableCellProviderTest
    {
        private Mock<AutomationProvider> _parent;
        private CellInformationStub _cellInformationStub;

        [SetUp]
        public void SetUp()
        {
            _parent = new Mock<AutomationProvider>();
            _cellInformationStub = new CellInformationStub();
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
            _cellInformationStub.Value = "Expected Name";
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
            _cellInformationStub.Row = 7;
            CellProvider.Row.Should().Equal(7);
        }

        [Test]
        public void ItKnowsTheColumn()
        {
            _cellInformationStub.Column = 42;
            CellProvider.Column.Should().Equal(42);
        }

        private TableCellProvider _cellProvider;
        private TableCellProvider CellProvider
        {
            get { return _cellProvider ?? (_cellProvider = new TableCellProvider(_parent.Object, _cellInformationStub)); }
        }
    }
}
