using System.Windows.Forms;
using UIA.Fluent.AutomationProviders.Tables;

namespace UIA.Fluent.AutomationProviders.Defaults.Tables
{
    public class DataGridCellInformation : CellInformation
    {
        private readonly DataGridViewCell _cell;

        public DataGridCellInformation(DataGridViewCell cell)
        {
            _cell = cell;
        }

        public string Value
        {
            get { return _cell.Value.ToString(); }
        }

        public int Row
        {
            get { return _cell.RowIndex; }
        }


        public int Column
        {
            get { return _cell.ColumnIndex; }
        }
    }
}