using System.Windows.Forms;
using UIA.Extensions.AutomationProviders.Tables;

namespace UIA.Extensions.AutomationProviders.Defaults.Tables
{
    public class DataGridCellInformation : CellInformation
    {
        private readonly DataGridViewCell _cell;

        private DataGridCellInformation(DataGridViewCell cell)
        {
            _cell = cell;
        }

        public static CellInformation FromCell(DataGridViewCell cell)
        {
            return new DataGridCellInformation(cell);
        }

        public string Value
        {
            get { return (_cell.Value ?? "").ToString(); }
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