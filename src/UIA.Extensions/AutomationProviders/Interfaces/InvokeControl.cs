using System.Windows.Forms;

namespace UIA.Extensions.AutomationProviders.Interfaces
{
    public abstract class InvokeControl
    {
        public InvokeControl(Control control)
        {
            Control = control;
        }

        public Control Control { get; private set; }
        public abstract void Invoke();
    }
}