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
                .Select(x => new ListItemInformation(x)).ToList();

            control.Text = _listOptions.First().Text;
        }

        public override List<ListItemInformation> ListItems
        {
            get { return _listOptions; }
        }
    }
}