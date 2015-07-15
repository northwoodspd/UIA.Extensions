using System.Windows.Automation;
using System.Windows.Automation.Provider;
using UIA.Extensions.AutomationProviders.Interfaces;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders
{
    public class ListItemProvider : AutomationProvider, ISelectionItemProvider
    {
        public ListItemProvider(AutomationProvider listProvider, ListItemInformation listItem) : base(listProvider, SelectionItemPattern.Pattern)
        {
            Name = listItem.Text;
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

        public override bool Equals(object obj)
        {
            return this.CeremoniallyEquals(obj, other => Equals(Name, other.Name) && Equals(Name, other.Name));
        }

        public override int GetHashCode()
        {
            return this.CombinedHashCodes(Name);
        }
    }
}