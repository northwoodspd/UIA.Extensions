using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;

namespace UIA.Fluent.AutomationProviders
{
    public class AutomationControlProvider : AutomationProvider
    {
        private readonly Control _control;

        public AutomationControlProvider(Control control)
        {
            _control = control;
            Id = _control.Name;
            SetPropertyValue(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id, _control.GetType().FullName);
        }

        protected override IRawElementProviderSimple HostProvider
        {
            get { return AutomationInteropProvider.HostProviderFromHandle(_control.Handle); }
        }
    }
}
