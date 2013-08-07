using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UIA.Fluent.AutomationProviders.Tables
{
    public class DataGridTableInformation : TableInformation
    {
        private readonly DataGridView _dataGrid;

        public DataGridTableInformation(DataGridView dataGrid)
        {
            _dataGrid = dataGrid;
        }

        public Control Control
        {
            get { return _dataGrid; }
        }

        public List<string> Headers
        {
            get { return (from DataGridViewColumn column in _dataGrid.Columns select column.HeaderText).ToList(); }
        }

        public List<RowInformation> Rows
        {
            get
            {
                return (from DataGridViewRow row in _dataGrid.Rows
                        select new DataGridRowInformation(row)).Cast<RowInformation>().ToList();
            }
        }

        public int RowCount
        {
            get { return _dataGrid.RowCount; }
        }

        public int ColumnCount
        {
            get { return _dataGrid.ColumnCount; }
        }
    }

    public class DataGridRowInformation : RowInformation
    {
        private readonly DataGridViewRow _dataGridViewRow;

        public DataGridRowInformation(DataGridViewRow dataGridViewRow)
        {
            _dataGridViewRow = dataGridViewRow;
        }

        public string Value
        {
            get { return _dataGridViewRow.Cells[0].Value as string; }
        }

        public List<string> Values
        {
            get
            {
                return (from DataGridViewCell cell in _dataGridViewRow.Cells
                        select cell.Value.ToString()).ToList();
            }
        }
    }
}