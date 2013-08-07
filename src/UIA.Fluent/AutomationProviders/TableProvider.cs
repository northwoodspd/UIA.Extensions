using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using UIA.Fluent.AutomationProviders.Tables;

namespace UIA.Fluent.AutomationProviders
{
    public class TableProvider : ControlProvider, ITableProvider
    {
        private readonly TableInformation _tableInformation;

        public TableProvider(TableInformation tableInformation) : base(tableInformation.Control)
        {
            _tableInformation = tableInformation;
            SetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id, ControlType.Table.Id);
        }

        private ChildProvider _headerProvider = null;
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

        protected override List<ChildProvider> Children
        {
            get { return new[] { HeaderProvider }.Where(x => null != x).ToList(); }
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

        public int RowCount { get { return _tableInformation.RowCount; } }
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