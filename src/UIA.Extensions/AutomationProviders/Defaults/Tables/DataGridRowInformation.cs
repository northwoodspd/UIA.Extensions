using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders.Defaults.Tables
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

        public override string Value
        {
            get { return _dataGridViewRow.Cells[0].Value as string; }
        }

        public override List<CellInformation> Cells
        {
            get { return _dataGridViewRow.Cells.Select(DataGridCellInformation.FromCell).ToList(); }
        }

        public override void Select()
        {
            _dataGridViewRow.DataGridView.ClearSelection();
            _dataGridViewRow.Selected = true;
        }

        public override void AddToSelection()
        {
            _dataGridViewRow.Selected = true;
        }

        public override void ClearSelection()
        {
            _dataGridViewRow.Selected = false;
        }

        public override bool IsSelected
        {
            get { return _dataGridViewRow.Selected; }
        }
    }
}