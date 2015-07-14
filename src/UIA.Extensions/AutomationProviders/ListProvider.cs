using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using UIA.Extensions.AutomationProviders.Interfaces;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders
{
    public class ListProvider : ControlProvider, ISelectionProvider
    {
        private readonly ListInformation _provider;

        public ListProvider(ListInformation provider) : base(provider.Control, SelectionPattern.Pattern)
        {
            _provider = provider;
            SetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id, ControlType.List.Id);
            SetPropertyValue(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id, ControlType.List.LocalizedControlType);
        }

        public IRawElementProviderSimple[] GetSelection()
        {
            throw new System.NotImplementedException();
        }

        public bool CanSelectMultiple
        {
            get { return _provider.CanSelectMultiple; }
        }

        public bool IsSelectionRequired
        {
            get { return _provider.IsRequired; }
        }

        public override List<AutomationProvider> Children
        {
            get { return _provider.ListItems.Select(ListItemFor).ToList(); }
        }

        private AutomationProvider ListItemFor(ListItemInformation listItemInformation, int index)
        {
            return new ListItemProvider(this, listItemInformation) { RuntimeId = index };
        }
    }

    public class ListItemProvider : AutomationProvider, ISelectionItemProvider
    {
        public ListItemProvider(AutomationProvider listProvider, ListItemInformation listItem) : base(listProvider, SelectionItemPattern.Pattern)
        {
            Name = listItem.Text;
        }

        public override bool Equals(object obj)
        {
            return this.CeremoniallyEquals(obj, other => Equals(Name, other.Name) && Equals(Name, other.Name));
        }

        public override int GetHashCode()
        {
            return this.CombinedHashCodes(Name);
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