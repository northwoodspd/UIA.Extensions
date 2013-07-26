using System;
using System.Collections.Generic;
using System.Windows.Automation;
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

        public static AutomationConfigurer AsValueControl(this Control control, Func<string> getter, Action<string> setter)
        {
            return new AutomationConfigurer(control, new ValueProvider(control, getter, setter));
        }
    }

    public class AutomationConfigurer
    {
        private readonly BaseProvider _provider;
        private AutomationHandler _automationHandler;

        public AutomationConfigurer(Control control)
        {
            _provider = new BaseProvider(control);
            _automationHandler = new AutomationHandler(control, _provider);
        }

        public AutomationConfigurer(Control control, BaseProvider provider)
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

    public class ValueProvider : BaseProvider, IValueProvider
    {
        private readonly Func<string> _getter;
        private readonly Action<string> _setter;

        public ValueProvider(Control control, Func<string> getter, Action<string> setter)
            : base(control)
        {
            _getter = getter;
            _setter = setter;
        }

        protected override List<int> SupportedPatterns
        {
            get { return new List<int> { ValuePatternIdentifiers.Pattern.Id }; }
        }

        public void SetValue(string value)
        {
            _setter(value);
        }

        public string Value
        {
            get { return _getter(); }
        }

        public bool IsReadOnly { get; private set; }
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
