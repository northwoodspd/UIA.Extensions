using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;

namespace UIA.Fluent.AutomationProviders
{
    public class TableProvider : AutomationProvider, ITableProvider
    {
        private readonly DataGridView _dataGrid;

        public TableProvider(DataGridView dataGridView) : base(dataGridView)
        {
            _dataGrid = dataGridView;
            SetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id, ControlType.Table.Id);
        }

        protected override List<int> SupportedPatterns
        {
            get { return new List<int> { TablePatternIdentifiers.Pattern.Id, GridPatternIdentifiers.Pattern.Id }; }
        }

        public IRawElementProviderSimple GetItem(int row, int column)
        {
            throw new System.NotImplementedException();
        }

        public int RowCount { get { return _dataGrid.RowCount; }}
        public int ColumnCount { get; private set; }
        public IRawElementProviderSimple[] GetRowHeaders()
        {
            throw new System.NotImplementedException();
        }

        public IRawElementProviderSimple[] GetColumnHeaders()
        {
            throw new System.NotImplementedException();
        }

        public RowOrColumnMajor RowOrColumnMajor { get; private set; }
    }
}