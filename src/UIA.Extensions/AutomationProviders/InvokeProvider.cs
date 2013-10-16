using System;
using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;
using UIA.Extensions.AutomationProviders.Interfaces;

namespace UIA.Extensions.AutomationProviders
{
    public class InvokeProvider : ControlProvider, IInvokeProvider
    {
        private readonly Action _invokable;

        public InvokeProvider(InvokeControl invokable) : base(invokable.Control)
        {
            _invokable = invokable.Invoke;
        }

        public InvokeProvider(Control control, Action invokable) : base(control)
        {
            _invokable = invokable;
        }

        protected override List<int> SupportedPatterns
        {
            get { return new List<int> { InvokePattern.Pattern.Id }; }
        }

        public void Invoke()
        {
            _invokable();
        }
    }
}