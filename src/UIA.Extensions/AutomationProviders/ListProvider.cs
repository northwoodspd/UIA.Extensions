using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using UIA.Extensions.AutomationProviders.Interfaces;

namespace UIA.Extensions.AutomationProviders
{
    public class ListProvider : ControlProvider, ISelectionProvider
    {
        private readonly ListInformation _listInformation;

        public ListProvider(ListInformation listInformation) : base(listInformation.Control, SelectionPattern.Pattern)
        {
            _listInformation = listInformation;
            ControlType = ControlType.List;
        }

        public IRawElementProviderSimple[] GetSelection()
        {
            return SelectedItems.Select(ListItemFor).ToArray();
        }

        public bool CanSelectMultiple
        {
            get { return _listInformation.CanSelectMultiple; }
        }

        public bool IsSelectionRequired
        {
            get { return _listInformation.IsRequired; }
        }

        public override List<AutomationProvider> Children
        {
            get { return _listInformation.ListItems.Select(ListItemFor).ToList(); }
        }

        private IEnumerable<ListItemInformation> SelectedItems
        {
            get { return _listInformation.ListItems.Where(x => x.IsSelected); }
        }

        private AutomationProvider ListItemFor(ListItemInformation listItemInformation, int index)
        {
            return new ListItemProvider(this, listItemInformation) { RuntimeId = index };
        }
    }
}