using System;
using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;
using UIA.Extensions.AutomationProviders;
using UIA.Extensions.AutomationProviders.Defaults;
using UIA.Extensions.AutomationProviders.Defaults.Tables;
using UIA.Extensions.AutomationProviders.Interfaces;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;
using UIA.Extensions.AutomationProviders.Tables;

namespace UIA.Extensions
{
    public static class AutomationExtensions
    {
        public static AutomationConfigurer ExposeAutomation(this Control control)
        {
            return new AutomationConfigurer(control);
        }

        public static AutomationConfigurer AsValueControl<T>(this Control control) where T : ValueControl
        {
            var valueControl = (T)Activator.CreateInstance(typeof(T), control);
            return new AutomationConfigurer(control, new ValueProvider(valueControl));
        }

        public static AutomationConfigurer AsInvoke<T>(this Control control) where T : InvokeControl
        {
            var invokeControl = (T) Activator.CreateInstance(typeof (T), control);
            return new AutomationConfigurer(control, new InvokeProvider(invokeControl));
        }

        public static AutomationConfigurer AsInvoke(this Control control, Action action)
        {
            return new AutomationConfigurer(control, new InvokeProvider(control, action));
        }

        public static AutomationConfigurer AsRangeValue(this NumericUpDown numericControl)
        {
            return numericControl.AsRangeValue<NumericUpDownRangeValue>();
        }

        public static AutomationConfigurer AsRangeValue<T>(this Control control) where T : RangeValueControl
        {
            var rangeControl = (T)Activator.CreateInstance(typeof(T), control);
            return new AutomationConfigurer(control, new RangeValueProvider(rangeControl));
        }

        public static AutomationConfigurer AsTable(this DataGridView control)
        {
            return control.AsTable<DataGridTableInformation>();
        }

        public static AutomationConfigurer AsTable<T>(this Control control) where T : TableInformation
        {
            var provider = (T)Activator.CreateInstance(typeof(T), control);
            return new AutomationConfigurer(control, new TableProvider(provider));
        }
    }

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

    public abstract class InvokeControl
    {
        public InvokeControl(Control control)
        {
            Control = control;
        }

        public Control Control { get; private set; }
        public abstract void Invoke();
    }
}
