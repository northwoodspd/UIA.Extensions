using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UIA.Fluent.AutomationProviders.Tables;

namespace UIA.Fluent.AutomationProviders.Defaults.Tables
{
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

        public List<CellInformation> Cells
        {
            get
            {
                return (from DataGridViewCell cell in _dataGridViewRow.Cells
                    select new DataGridCellInformation(cell)).Cast<CellInformation>().ToList();
            }
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