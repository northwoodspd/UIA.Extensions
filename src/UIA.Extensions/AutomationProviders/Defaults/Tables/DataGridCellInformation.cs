using System.Windows;
using System.Windows.Forms;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders.Defaults.Tables
{
    public class DataGridCellInformation : CellInformation
    {
        protected readonly DataGridViewCell Cell;

        public DataGridCellInformation(DataGridViewCell cell)
        {
            Cell = cell;
        }

        public static CellInformation FromCell(DataGridViewCell cell)
        {
            return new DataGridCellInformation(cell);
        }

        public override string Value
        {
            get { return (Cell.Value ?? "").ToString(); }
        }

        public override int Row
        {
            get { return Cell.RowIndex; }
        }


        public override int Column
        {
            get { return Cell.ColumnIndex; }
        }

        public override Rect Location
        {
            get
            {
                var cellDisplayRectangle = Cell.DataGridView.GetCellDisplayRectangle(Column, Row, false);
                return Cell.DataGridView.RectangleToScreen(cellDisplayRectangle).AsWindowsRect();
            }
        }
    }
}