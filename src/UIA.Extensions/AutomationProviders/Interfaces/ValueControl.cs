using System.Windows.Forms;

namespace UIA.Extensions.AutomationProviders.Interfaces
{
    public abstract class ValueControl
    {
        protected ValueControl(Control control)
        {
            Control = control;
        }

        public Control Control { get; private set; }
        public abstract string Value { get; set; }
        public virtual bool ReadOnly { get; set; }
    }
}