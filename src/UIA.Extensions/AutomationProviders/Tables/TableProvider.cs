using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;

namespace UIA.Extensions.AutomationProviders.Tables
{
    public class TableProvider : ControlProvider, ITableProvider
    {
        private readonly TableInformation _tableInformation;

        public TableProvider(TableInformation tableInformation) : base(tableInformation.Control)
        {
            _tableInformation = tableInformation;
            SetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id, ControlType.Table.Id);
            SetPropertyValue(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id, ControlType.Table.LocalizedControlType);
        }

        public int RowCount { get { return _tableInformation.RowCount; } }
        public int ColumnCount { get { return _tableInformation.ColumnCount; } }

        private ChildProvider _headerProvider;
        private ChildProvider HeaderProvider
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

        private List<ChildProvider> _rows = new List<ChildProvider>();
        private IEnumerable<ChildProvider> RowProviders
        {
            get
            {
                UpdateRowsIfNecessary();
                return _rows;
            }
        }

        private void UpdateRowsIfNecessary()
        {
            var currentRows = _tableInformation.Rows.Select(x => new TableRowProvider(this, x)).Cast<ChildProvider>().ToList();
            if (!_rows.SequenceEqual(currentRows))
            {
                _rows = currentRows;
            }
        }

        public override List<ChildProvider> Children
        {
            get { return new[] { HeaderProvider }.Concat(RowProviders).Where(x => null != x).ToList(); }
        }

        public bool HasHeaders
        {
            get { return _tableInformation.Headers.Count != 0; }
        }

        protected override List<int> SupportedPatterns
        {
            get { return new List<int> { TablePatternIdentifiers.Pattern.Id, GridPatternIdentifiers.Pattern.Id }; }
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