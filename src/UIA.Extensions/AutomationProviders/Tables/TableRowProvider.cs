using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;

namespace UIA.Extensions.AutomationProviders.Tables
{
    public class TableRowProvider : ChildProvider, ISelectionItemProvider
    {
        private readonly RowInformation _rowInformation;

        public TableRowProvider(AutomationProvider parent, RowInformation rowInformation) : base(parent)
        {
            _rowInformation = rowInformation;
            Name = rowInformation.Value;

            rowInformation.Cells.ForEach(x => Children.Add(new TableCellProvider(this, x)));
        }

        protected override List<int> SupportedPatterns
        {
            get { return new List<int> { SelectionItemPatternIdentifiers.Pattern.Id }; }
        }

        protected override int ControlTypeId
        {
            get { return ControlType.DataItem.Id; }
        }

        public bool IsSelected
        {
            get { return _rowInformation.IsSelected; }
        }

        public void Select()
        {
            _rowInformation.Select();
        }

        public void AddToSelection()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveFromSelection()
        {
            throw new System.NotImplementedException();
        }

        public IRawElementProviderSimple SelectionContainer { get; private set; }
    }
}