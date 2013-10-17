using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;

namespace UIA.Extensions.AutomationProviders.Tables
{
    public class TableProvider : ControlProvider, ISelectionProvider, ITableProvider
    {
        private readonly TableInformation _tableInformation;

        public TableProvider(TableInformation tableInformation) : base(tableInformation.Control, SelectionPattern.Pattern, TablePatternIdentifiers.Pattern, GridPatternIdentifiers.Pattern)
        {
            _tableInformation = tableInformation;
            SetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id, ControlType.Table.Id);
            SetPropertyValue(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id, ControlType.Table.LocalizedControlType);
        }

        public int RowCount { get { return _tableInformation.RowCount; } }
        public int ColumnCount { get { return _tableInformation.ColumnCount; } }

        private AutomationProvider _headerProvider;
        private AutomationProvider HeaderProvider
        {
            get
            {
                var currentHeader = new HeaderProvider(this, _tableInformation.Headers);
                if (HasHeaders && !Equals(_headerProvider, currentHeader))
                {
                    _headerProvider = currentHeader;
                }

                return _headerProvider;
            }
        }

        private List<AutomationProvider> _rows = new List<AutomationProvider>();
        private IEnumerable<AutomationProvider> RowProviders
        {
            get
            {
                UpdateRowsIfNecessary();
                return _rows;
            }
        }

        private void UpdateRowsIfNecessary()
        {
            var currentRows = _tableInformation.Rows.Select(x => new TableRowProvider(this, x)).Cast<AutomationProvider>().ToList();
            if (!_rows.SequenceEqual(currentRows))
            {
                _rows = currentRows;
            }
        }

        public override List<AutomationProvider> Children
        {
            get { return new[] { HeaderProvider }.Concat(RowProviders).Where(x => null != x).ToList(); }
        }

        public bool HasHeaders
        {
            get { return _tableInformation.Headers.Count != 0; }
        }

        public bool CanSelectMultiple
        {
            get { return _tableInformation.CanSelectMultiple; }
        }

        public bool IsSelectionRequired { get; private set; }

        public IRawElementProviderSimple[] GetSelection()
        {
            throw new System.NotImplementedException();
        }

        public IRawElementProviderSimple GetItem(int row, int column)
        {
            throw new System.NotImplementedException();
        }

        public IRawElementProviderSimple[] GetRowHeaders()
        {
            throw new System.NotImplementedException();
        }

        public IRawElementProviderSimple[] GetColumnHeaders()
        {
            return HeaderProvider.Children.ToArray();
        }
        public RowOrColumnMajor RowOrColumnMajor { get; private set; }
    }
}