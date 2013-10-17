using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;

namespace UIA.Extensions.AutomationProviders.Tables
{
    public class TableCellProvider : AutomationProvider, ITableItemProvider
    {
        private readonly CellInformation _cell;

        public TableCellProvider(AutomationProvider parent, CellInformation cell) : base(parent, TableItemPattern.Pattern, GridItemPattern.Pattern)
        {
            _cell = cell;
            ControlType = ControlType.Text;
        }

        public override string Name
        {
            get { return _cell.Value; }
        }

        public int Row
        {
            get { return _cell.Row; }
        }

        public int Column
        {
            get { return _cell.Column; }
        }

        public override Rect BoundingRectangle
        {
            get { return _cell.Location; }
        }

        public IRawElementProviderSimple ContainingGrid { get { return FragmentRoot; }}

        public int RowSpan { get; private set; }
        public int ColumnSpan { get; private set; }

        public IRawElementProviderSimple[] GetRowHeaderItems()
        {
            throw new System.NotImplementedException();
        }

        public IRawElementProviderSimple[] GetColumnHeaderItems()
        {
            throw new System.NotImplementedException();
        }
    }
}