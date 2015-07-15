using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UIA.Extensions.AutomationProviders.Interfaces;

namespace UIA.Extensions.TestApplication
{
    public class ListActingLabel : ListInformation
    {
        private readonly List<ListItemInformation> _listOptions;

        public ListActingLabel(Control control) : base(control)
        {
            IsRequired = true;
            CanSelectMultiple = true;

            _listOptions = new[] {"First Option", "Second Option", "Third Option"}
                .Select(x => ListActingLabelItem.Create(x, this)).ToList();

            _listOptions.First().Select();
        }

        public void UpdateSelection(ListActingLabelItem toSelect)
        {
            foreach (var selected in _listOptions.Where(x => x.IsSelected && x != toSelect))
            {
                selected.RemoveFromSelection();
            }

            Control.Text = toSelect.Text;
        }

        public override List<ListItemInformation> ListItems
        {
            get { return _listOptions; }
        }

        public void Clear()
        {
            Control.Text = string.Empty;
        }
    }

    public class ListActingLabelItem : ListItemInformation
    {
        private readonly ListActingLabel _parent;

        private ListActingLabelItem(string text, ListActingLabel parent) : base(text)
        {
            _parent = parent;
        }

        public override void Select()
        {
            IsSelected = true;
            _parent.UpdateSelection(this);
        }

        public override void AddToSelection()
        {
            Select();
        }

        public override void RemoveFromSelection()
        {
            IsSelected = false;
            _parent.Clear();
        }

        public static ListItemInformation Create(string text, ListActingLabel parent)
        {
            return new ListActingLabelItem(text, parent);
        }
    }
}