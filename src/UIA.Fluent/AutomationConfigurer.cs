using System.Windows.Forms;
using UIA.Fluent.AutomationProviders;

namespace UIA.Fluent
{
    public class AutomationConfigurer
    {
        private readonly AutomationProvider _provider;
        private AutomationHandler _automationHandler;

        public AutomationConfigurer(Control control)
        {
            _provider = new AutomationProvider(control);
            _automationHandler = new AutomationHandler(control, _provider);
        }

        public AutomationConfigurer(Control control, AutomationProvider provider)
        {
            _provider = provider;
            _automationHandler = new AutomationHandler(control, _provider);
        }

        public AutomationConfigurer WithName(string name)
        {
            _provider.Name = name;
            return this;
        }
    }
}