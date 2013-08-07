using System.Windows.Forms;
using UIA.Fluent.AutomationProviders;

namespace UIA.Fluent
{
    public class AutomationConfigurer
    {
        private readonly AutomationControlProvider _controlProvider;
        private AutomationHandler _automationHandler;

        public AutomationConfigurer(Control control)
        {
            _controlProvider = new AutomationControlProvider(control);
            _automationHandler = new AutomationHandler(control, _controlProvider);
        }

        public AutomationConfigurer(IWin32Window control, AutomationControlProvider controlProvider)
        {
            _controlProvider = controlProvider;
            _automationHandler = new AutomationHandler(control, _controlProvider);
        }

        public AutomationConfigurer WithName(string name)
        {
            _controlProvider.Name = name;
            return this;
        }
    }
}