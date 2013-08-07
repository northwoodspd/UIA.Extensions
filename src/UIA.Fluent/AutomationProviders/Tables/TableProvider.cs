using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Provider;

namespace UIA.Fluent.AutomationProviders.Tables
{
    public class TableProvider : ControlProvider, ITableProvider
    {
        private readonly TableInformation _tableInformation;

        public TableProvider(TableInformation tableInformation) : base(tableInformation.Control)
        {
            _tableInformation = tableInformation;
            SetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id, ControlType.Table.Id);
        }

        public int RowCount { get { return _tableInformation.RowCount; } }
        public int ColumnCount { get { return _tableInformation.ColumnCount; } }

        private ChildProvider _headerProvider;
        private ChildProvider HeaderProvider
        {
            get
            {
                if (null == _headerProvider && HasHeaders)
                {
                    _headerProvider = new HeaderProvider(this, _tableInformation.Headers);
                }

                return _headerProvider;
            }
        }

        private List<ChildProvider> _rows;
        private IEnumerable<ChildProvider> RowProviders
        {
            get
            {
                if (null == _rows && RowCount > 0)
                {
                    _rows = _tableInformation.Rows.Select(x => new RowProvider(this, x)).Cast<ChildProvider>().ToList();
                }

                return _rows ?? new List<ChildProvider>();
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
            throw new System.NotImplementedException();
        }
        public RowOrColumnMajor RowOrColumnMajor { get; private set; }
    }
}