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

            if (HasHeaders)
            {
                Children.Add(new HeaderProvider(this));
            }
        }

        public bool HasHeaders
        {
            get { return _tableInformation.Headers.Count != 0; }
        }

        protected override IRawElementProviderFragment FirstChild
        {
            get { return Children.FirstOrDefault(); }
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