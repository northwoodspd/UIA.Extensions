using System;
using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;

namespace UIA.Extensions.AutomationProviders
{
    public class InvokeProvider : ControlProvider, IInvokeProvider
    {
        private readonly Action _invoke;

        public InvokeProvider(Control control, Action invoke) : base(control)
        {
            _invoke = invoke;
        }

        protected override List<int> SupportedPatterns
        {
            get { return new List<int> { InvokePattern.Pattern.Id }; }
        }

        public void Invoke()
        {
            _invoke();
        }
    }
}