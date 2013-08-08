using System.Windows.Automation.Provider;
using System.Windows.Forms;
using UIA.Extensions.AutomationProviders;

namespace UIA.Extensions
{
    public class AutomationHandler : NativeWindow
    {
        private readonly ControlProvider _controlProvider;
        const int WmGetobject = 0x3d;

        public AutomationHandler(IWin32Window window, ControlProvider controlProvider)
        {
            _controlProvider = controlProvider;
            AssignHandle(window.Handle);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WmGetobject)
            {
                m.Result = AutomationInteropProvider.ReturnRawElementProvider(m.HWnd, m.WParam, m.LParam, _controlProvider);
                return;
            }

            base.WndProc(ref m);
        }
    }
}