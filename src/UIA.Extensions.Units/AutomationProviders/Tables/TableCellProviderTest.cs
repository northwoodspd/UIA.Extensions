using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using UIA.Extensions.AutomationProviders.Tables.Stubs;

namespace UIA.Extensions.AutomationProviders.Tables
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
                .ShouldBeEquivalentTo(ControlType.Text.Id);
        }

        [Test]
        public void TheNameIsTheValue()
        {
            _cellInformationStub.ExpectedValue = "Expected Name";
            CellProvider.Name.ShouldBeEquivalentTo("Expected Name");
        }

        [Test]
        public void ItDoubleAsATableItem()
        {
            CellProvider.Should().BeAssignableTo<ITableItemProvider>();
            CellProvider.GetPatternProvider(TableItemPatternIdentifiers.Pattern.Id)
                .Should().BeSameAs(_cellProvider);
        }

        [Test]
        public void ItKnowsTheRow()
        {
            _cellInformationStub.ExpectedRow= 7;
            CellProvider.Row.ShouldBeEquivalentTo(7);
        }

        [Test]
        public void ItKnowsTheColumn()
        {
            _cellInformationStub.ExpectedColumn = 42;
            CellProvider.Column.ShouldBeEquivalentTo(42);
        }

        [Test]
        public void TheRootIsTheContainingGrid()
        {
            var expectedGrid = new AutomationProvider();
            _parent.Setup(x => x.FragmentRoot).Returns(expectedGrid);

            CellProvider.ContainingGrid.Should().BeSameAs(expectedGrid);
        }

        [Test]
        public void ShouldKnowWhereItIs()
        {
            var expectedLocation = new Rect(0, 0, 100, 100);
            _cellInformationStub.ExpectedLocation = expectedLocation;

            CellProvider.BoundingRectangle.ShouldBeEquivalentTo(expectedLocation);
        }

        private TableCellProvider _cellProvider;
        private TableCellProvider CellProvider
        {
            get { return _cellProvider ?? (_cellProvider = new TableCellProvider(_parent.Object, _cellInformationStub)); }
        }
    }
}
