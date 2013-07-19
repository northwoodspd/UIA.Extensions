using System.Windows.Automation.Provider;
using System.Windows.Forms;
using UIA.Fluent.AutomationProviders;

namespace UIA.Fluent
{
    public static class AutomationExtensions
    {
        public static AutomationConfigurer ExposeAutomation(this Control control)
        {
            return new AutomationConfigurer(control);
        }
    }

    public class AutomationConfigurer
    {
        private BaseProvider _provider;
        private AutomationHandler _automationHandler;

        public AutomationConfigurer(Control control)
        {
            _provider = new BaseProvider(control);
            _automationHandler = new AutomationHandler(control, _provider);
        }

        public AutomationConfigurer WithName(string name)
        {
            _provider.Name = name;
            return this;
        }
    }

    public class AutomationHandler : NativeWindow
    {
        private readonly BaseProvider _provider;
        const int WmGetobject = 0x3d;

        public AutomationHandler(IWin32Window window, BaseProvider provider)
        {
            _provider = provider;
            AssignHandle(window.Handle);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WmGetobject)
            {
                m.Result = AutomationInteropProvider.ReturnRawElementProvider(m.HWnd, m.WParam, m.LParam, _provider);
                return;
            }

            base.WndProc(ref m);
        }
    }
}
