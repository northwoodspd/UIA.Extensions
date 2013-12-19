using System.Windows.Forms;
using FluentAssertions;
using NUnit.Framework;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;

namespace UIA.Extensions.AutomationProviders.Defaults.Tables
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
            _cellInformation.Value.ShouldBeEquivalentTo("Expected value");
        }

        [Test]
        public void ItDefaultsToEmptyIfThereIsNoValue()
        {
            _cell.Value = null;
            _cellInformation.Value.Should().BeEmpty();
        }

        [Test]
        public void NonStringsAreValuesToo()
        {
            _cell.Value = 123;
            _cellInformation.Value.Should().BeEquivalentTo("123");
        }

        class TestCell : DataGridViewCell
        { }
    }
}
