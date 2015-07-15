using System.Windows.Automation;
using System.Windows.Automation.Provider;
using UIA.Extensions.AutomationProviders.Interfaces;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders
{
    public class ListItemProvider : AutomationProvider, ISelectionItemProvider
    {
        private readonly ListItemInformation _listItem;

        public ListItemProvider(AutomationProvider listProvider, ListItemInformation listItem) : base(listProvider, SelectionItemPattern.Pattern)
        {
            _listItem = listItem;
            Name = listItem.Text;
            ControlType = ControlType.ListItem;
        }

        public void Select()
        {
            _listItem.Select();
        }

        public void AddToSelection()
        {
            _listItem.AddToSelection();
        }

        public void RemoveFromSelection()
        {
            _listItem.RemoveFromSelection();
        }

        public bool IsSelected
        {
            get { return _listItem.IsSelected; }
        }

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