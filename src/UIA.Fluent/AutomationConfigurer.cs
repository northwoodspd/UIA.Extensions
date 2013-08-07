using System.Windows.Forms;
using UIA.Fluent.AutomationProviders;

namespace UIA.Fluent
{
    public class AutomationConfigurer
    {
        private readonly ControlProvider _controlProvider;

        public AutomationConfigurer(Control control)
        {
            _controlProvider = new ControlProvider(control);
            AutomationHandler = new AutomationHandler(control, _controlProvider);
        }

        public AutomationConfigurer(IWin32Window control, ControlProvider controlProvider)
        {
            _controlProvider = controlProvider;
            AutomationHandler = new AutomationHandler(control, _controlProvider);
        }

        public AutomationHandler AutomationHandler { get; set; }

        public AutomationConfigurer WithName(string name)
        {
            _controlProvider.Name = name;
            return this;
        }
    }
}