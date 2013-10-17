using System.Windows.Forms;
using UIA.Extensions.AutomationProviders;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions
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
        public ControlProvider ControlProvider { get { return _controlProvider; } }

        public AutomationConfigurer WithName(string name)
        {
            _controlProvider.Name = name;
            return this;
        }

        public AutomationConfigurer WithChildren(params AutomationProvider[] children)
        {
            children.ForEach(_controlProvider.AddChild);
            return this;
        }
    }
}