using System.Windows.Forms;
using FluentAssertions;
using NUnit.Framework;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;
using UIA.Extensions.InternalExtensions;

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
        public void RowsHaveValue()
        {
            _rowInformation.Value.Should().Be("Expected Name");
        }

        [Test]
        public void RowValuesDoNotHaveToBeStrings()
        {
            _dataGridRow.SetValues(123);
            _rowInformation.Value.Should().Be("123");
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

        [Test]
        public void TheSelectionCanBeCleared()
        {
            _rowInformation.AddToSelection();

            _rowInformation.ClearSelection();

            _rowInformation.IsSelected.Should().BeFalse();
        }

        private void SetupTheTable()
        {
            _dataGridView = new DataGridView();
            _dataGridView.Columns.Add("Name", "Name");
            _dataGridRow = AddRow("Expected Name");
        }

        private DataGridViewRow AddRow(params object[] values)
        {
            return _dataGridView.Rows[_dataGridView.Rows.Add(values)];
        }

        private void AddColumns(params string[] headers)
        {
            headers.ForEach(x => _dataGridView.Columns.Add(x, x));
        }

        private DataGridViewRow AddSelectedRow(string name)
        {
            var row = AddRow(name);
            row.Selected = true;
            return row;
        }
    }
}
