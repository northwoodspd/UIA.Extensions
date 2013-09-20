using System.Windows;
using System.Windows.Forms;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;
using UIA.Extensions.InternalExtensions;

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

        public override string Value
        {
            get { return (_cell.Value ?? "").ToString(); }
        }

        public override int Row
        {
            get { return _cell.RowIndex; }
        }


        public override int Column
        {
            get { return _cell.ColumnIndex; }
        }

        public override Rect Location
        {
            get { return _cell.DataGridView.GetCellDisplayRectangle(Column, Row, false).AsWindowsRect(); }
        }
    }
}