using System.Windows.Forms;
using FluentAssertions;
using NUnit.Framework;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;

namespace UIA.Extensions.AutomationProviders.Defaults.Tables
{
    [TestFixture]
    class DataGridRowInformationTest
    {
        private DataGridViewRow _dataGridRow;
        private RowInformation _rowInformation;
        private DataGridView _dataGridView;

        [SetUp]
        public void SetUp()
        {
            SetupTheTable();

            _rowInformation = DataGridRowInformation.FromRow(_dataGridRow);
        }

        [Test]
        public void ItCanSelectTheRow()
        {
            _rowInformation.Select();
            _dataGridRow.Selected.Should().BeTrue();
        }

        [Test]
        public void SelectingTheRowClearsTheOtherSelectedRows()
        {
            var otherSelected = AddSelectedRow("Other Row");

            _rowInformation.Select();

            otherSelected.Selected.Should().BeFalse();
        }

        [Test]
        public void ItCanBeAddedToTheSelection()
        {
            var otherSelected = AddSelectedRow("Other Selected");

            _rowInformation.AddToSelection();

            _rowInformation.IsSelected.Should().BeTrue();
            otherSelected.Selected.Should().BeTrue();
        }

        private void SetupTheTable()
        {
            _dataGridView = new DataGridView();
            _dataGridView.Columns.Add("Name", "Name");
            _dataGridRow = AddRow("Expected Name");
        }

        private DataGridViewRow AddRow(string name)
        {
            return _dataGridView.Rows[_dataGridView.Rows.Add(new object[] {name})];
        }

        private DataGridViewRow AddSelectedRow(string name)
        {
            var row = AddRow(name);
            row.Selected = true;
            return row;
        }
    }
}
