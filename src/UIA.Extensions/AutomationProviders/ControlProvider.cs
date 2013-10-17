using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;

namespace UIA.Extensions.AutomationProviders
{
    public class ControlProvider : AutomationProvider
    {
        public readonly Control Control;

        public ControlProvider(Control control)
        {
            Control = control;
        }

        public ControlProvider(Control control, params AutomationPattern[] patterns) : base(patterns)
        {
            Control = control;
        }

        public override string Id
        {
            get { return Control.Name; }
        }

        private string _name;
        public override string Name
        {
            get { return _name ?? Control.Text; }
            set { _name = value; }
        }

        public  override IRawElementProviderSimple HostRawElementProvider
        {
            get { return AutomationInteropProvider.HostProviderFromHandle(Control.Handle); }
        }
    }
}
