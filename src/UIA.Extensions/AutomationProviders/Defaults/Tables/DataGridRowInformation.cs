using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders.Defaults.Tables
{
    public class DataGridRowInformation : RowInformation
    {
        protected readonly DataGridViewRow DataGridViewRow;

        public DataGridRowInformation(DataGridViewRow dataGridViewRow)
        {
            DataGridViewRow = dataGridViewRow;
        }

        public static RowInformation FromRow(DataGridViewRow row)
        {
            return new DataGridRowInformation(row);
        }

        public override string Value
        {
            get { return Cells[0].Value; }
        }

        public override List<CellInformation> Cells
        {
            get { return DataGridViewRow.Cells.Select(DataGridCellInformation.FromCell).ToList(); }
        }

        public override void Select()
        {
            DataGridViewRow.DataGridView.ClearSelection();
            DataGridViewRow.Selected = true;
        }

        public override void AddToSelection()
        {
            DataGridViewRow.Selected = true;
        }

        public override void ClearSelection()
        {
            DataGridViewRow.Selected = false;
        }

        public override bool IsSelected
        {
            get { return DataGridViewRow.Selected; }
        }

        public override bool IsVisible
        {
            get { return DataGridViewRow.Visible; }
            set { DataGridViewRow.Visible = value; }
        }
    }
}