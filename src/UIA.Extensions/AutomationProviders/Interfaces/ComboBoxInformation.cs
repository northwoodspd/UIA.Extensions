using System.Windows.Forms;

namespace UIA.Extensions.AutomationProviders.Interfaces
{
    public abstract class ComboBoxInformation
    {
        protected ComboBoxInformation(Control control)
        {
            Control = control;
        }

        public Control Control { get; protected set; }
        public bool IsRequired { get; protected set; }
    }
}