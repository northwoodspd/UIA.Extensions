using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders.Tables
{
    public class TableRowProvider : AutomationProvider, ISelectionItemProvider
    {
        private readonly RowInformation _rowInformation;

        public TableRowProvider(AutomationProvider parent, RowInformation rowInformation) : base(parent, SelectionItemPattern.Pattern)
        {
            _rowInformation = rowInformation;
            Name = rowInformation.Value;
            ControlType = ControlType.DataItem;

            rowInformation.Cells.ForEach(x => Children.Add(new TableCellProvider(this, x)));
        }

        public override string Name
        {
            get { return _rowInformation.Value; }
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
            _rowInformation.AddToSelection();
        }

        public void RemoveFromSelection()
        {
            _rowInformation.ClearSelection();
        }

        public IRawElementProviderSimple SelectionContainer { get; private set; }

        public override bool Equals(object obj)
        {
            return this.CeremoniallyEquals(obj, other => Equals(_rowInformation, other._rowInformation));
        }

        public override int GetHashCode()
        {
            return this.CombinedHashCodes(_rowInformation);
        }
    }
}