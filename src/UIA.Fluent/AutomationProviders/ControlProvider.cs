using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;

namespace UIA.Fluent.AutomationProviders
{
    public class ControlProvider : AutomationProvider
    {
        protected readonly Control _control;

        public ControlProvider(Control control)
        {
            _control = control;
            Id = _control.Name;
            SetPropertyValue(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id, _control.GetType().FullName);
        }

        public  override IRawElementProviderSimple HostRawElementProvider
        {
            get { return AutomationInteropProvider.HostProviderFromHandle(_control.Handle); }
        }
    }
}
