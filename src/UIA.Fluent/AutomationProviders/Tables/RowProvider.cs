using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Provider;

namespace UIA.Fluent.AutomationProviders.Tables
{
    public class RowProvider : ChildProvider, ISelectionItemProvider
    {
        private readonly RowInformation _rowInformation;

        public RowProvider(AutomationProvider parent, RowInformation rowInformation) : base(parent)
        {
            _rowInformation = rowInformation;
            Name = rowInformation.Value;
        }

        protected override List<int> SupportedPatterns
        {
            get { return new List<int> { SelectionItemPatternIdentifiers.Pattern.Id }; }
        }

        protected override int ControlTypeId
        {
            get { return ControlType.DataItem.Id; }
        }

        public void Select()
        {
            throw new System.NotImplementedException();
        }

        public void AddToSelection()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveFromSelection()
        {
            throw new System.NotImplementedException();
        }

        public bool IsSelected { get; private set; }
        public IRawElementProviderSimple SelectionContainer { get; private set; }
    }
}