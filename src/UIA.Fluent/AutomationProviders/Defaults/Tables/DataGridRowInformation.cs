using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UIA.Fluent.AutomationProviders.Tables;
using UIA.Fluent.Extensions;

namespace UIA.Fluent.AutomationProviders.Defaults.Tables
{
    public class DataGridRowInformation : RowInformation
    {
        private readonly DataGridViewRow _dataGridViewRow;

        private DataGridRowInformation(DataGridViewRow dataGridViewRow)
        {
            _dataGridViewRow = dataGridViewRow;
        }

        public static RowInformation FromRow(DataGridViewRow row)
        {
            return new DataGridRowInformation(row);
        }

        public string Value
        {
            get { return _dataGridViewRow.Cells[0].Value as string; }
        }

        public List<CellInformation> Cells
        {
            get { return _dataGridViewRow.Cells.Select(DataGridCellInformation.FromCell).ToList(); }
        }

        public void Select()
        {
            _dataGridViewRow.DataGridView.ClearSelection();
            _dataGridViewRow.Selected = true;
        }

        public bool IsSelected
        {
            get { return _dataGridViewRow.Selected; }
        }
    }
}