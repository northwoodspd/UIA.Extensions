using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using Moq;
using NUnit.Framework;
using Should.Fluent;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;
using UIA.Extensions.AutomationProviders.Tables.Stubs;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders.Tables
{
    [TestFixture]
    public class TableRowProviderTest
    {
        private RowInformationStub _rowInformationStub;
        private Mock<AutomationProvider> _parent;
        private readonly List<CellInformation> _values = new List<CellInformation>();

        [SetUp]
        public void SetUp()
        {
            _provider = null;
            _rowInformationStub = new RowInformationStub { Cells = _values };
            _parent = new Mock<AutomationProvider>();
        }

        [Test]
        public void ItIsOfTheDataItemIlk()
        {
            TableRowProvider.GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .Should().Equal(ControlType.DataItem.Id);
        }

        [Test]
        public void ItDoublesAsASelectionItem()
        {
            TableRowProvider.Should().Be.AssignableFrom<ISelectionItemProvider>();
            TableRowProvider.GetPatternProvider(SelectionItemPatternIdentifiers.Pattern.Id).Should()
                .Be.SameAs(TableRowProvider);
        }

        [Test]
        public void ItIsSelectable()
        {
            TableRowProvider.Select();

            _rowInformationStub.ShouldHaveBeenSelected();
        }

        [Test]
        public void ItKnowsIfItIsSelected()
        {
            TableRowProvider.Select();

            TableRowProvider.IsSelected.Should().Be.True();
        }

        [Test]
        public void TheRowValuesAreTheChildren()
        {
            AddCells("item 1", "item 2", "item 3");
            TableRowProvider.Children.Count.Should().Equal(3);
            TableRowProvider.Children.Select(x => x.Name).Should().Equal(new[] { "item 1", "item 2", "item 3" });
        }

        [Test]
        public void RowsWithTheSameValueAndChildrenAreEqual()
        {
            var oneRow = new TableRowProvider(_parent.Object, _rowInformationStub);
            var sameRow = new TableRowProvider(_parent.Object, _rowInformationStub);

            oneRow.Should().Equal(sameRow);
        }

        private TableRowProvider _provider;
        private TableRowProvider TableRowProvider
        {
            get
            {
               return _provider ?? (_provider = new TableRowProvider(_parent.Object, _rowInformationStub));
            }
        }

        private void AddCells(params string[] values)
        {
            values.ForEach(x => _values.Add(new CellInformationStub(x)));
        }
    }
}
