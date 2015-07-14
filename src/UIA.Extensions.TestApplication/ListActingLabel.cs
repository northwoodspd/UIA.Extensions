using System.Windows.Forms;
using UIA.Extensions.AutomationProviders.Interfaces;

namespace UIA.Extensions.TestApplication
{
    public class ListActingLabel : ListInformation
    {
        public ListActingLabel(Control control) : base(control)
        {
            IsRequired = true;
            CanSelectMultiple = true;
        }
    }
}