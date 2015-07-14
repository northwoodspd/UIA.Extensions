using System.Windows.Forms;

namespace UIA.Extensions.AutomationProviders.Interfaces
{
    public abstract class ListInformation
    {
        protected ListInformation(Control control)
        {
            Control = control;
        }

        public Control Control { get; protected set; }
        public bool IsRequired { get; protected set; }
        public bool CanSelectMultiple { get; protected set; }
    }
}