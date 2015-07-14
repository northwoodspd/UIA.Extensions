using System.Windows.Forms;
using UIA.Extensions.AutomationProviders.Interfaces;

namespace UIA.Extensions.TestApplication
{
    public class ComboBoxActingLabel : ComboBoxInformation
    {
        public ComboBoxActingLabel(Control control) : base(control)
        {
            IsRequired = true;
        }
    }
}