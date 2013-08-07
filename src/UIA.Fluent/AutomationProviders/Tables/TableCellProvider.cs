using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Provider;

namespace UIA.Fluent.AutomationProviders.Tables
{
    public class TableCellProvider : ChildProvider, ITableItemProvider
    {
        public TableCellProvider(AutomationProvider parent, CellInformation cell)
            : base(parent)
        {
            Name = cell.Value;
        }

        protected override int ControlTypeId
        {
            get { return ControlType.Text.Id; }
        }

        protected override List<int> SupportedPatterns
        {
            get { return new List<int> { TableItemPatternIdentifiers.Pattern.Id, GridItemPatternIdentifiers.Pattern.Id }; }
        }

        public int Row { get; private set; }
        public int Column { get; private set; }
        public int RowSpan { get; private set; }
        public int ColumnSpan { get; private set; }
        public IRawElementProviderSimple ContainingGrid { get; private set; }
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