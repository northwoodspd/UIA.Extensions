using System.Windows.Forms;
using NUnit.Framework;
using Should.Fluent;
using UIA.Fluent.AutomationProviders.Tables;

namespace UIA.Fluent.AutomationProviders.Defaults.Tables
{
    [TestFixture]
    class DataGridCellInformationTest
    {
        private TestCell _cell;
        private CellInformation _cellInformation;

        [SetUp]
        public void SetUp()
        {
            _cell = new TestCell();
            _cellInformation = DataGridCellInformation.FromCell(_cell);
        }

        [Test]
        public void ItReturnsTheValue()
        {
            _cell.Value = "Expected value";
            _cellInformation.Value.Should().Equal("Expected value");
        }

        [Test]
        public void ItDefaultsToEmptyIfThereIsNoValue()
        {
            _cell.Value = null;
            _cellInformation.Value.Should().Be.Empty();
        }

        class TestCell : DataGridViewCell
        { }
    }
}
