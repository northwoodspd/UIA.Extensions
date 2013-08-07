using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;

namespace UIA.Fluent.AutomationProviders
{
    public class ControlProvider : AutomationProvider
    {
        protected readonly Control Control;

        public ControlProvider(Control control)
        {
            Control = control;
            Id = Control.Name;
            SetPropertyValue(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id, Control.GetType().FullName);
        }

        public  override IRawElementProviderSimple HostRawElementProvider
        {
            get { return AutomationInteropProvider.HostProviderFromHandle(Control.Handle); }
        }
    }
}
