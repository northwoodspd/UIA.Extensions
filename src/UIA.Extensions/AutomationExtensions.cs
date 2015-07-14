using System;
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

        public static AutomationConfigurer AsList<T>(this Control control) where T : ListInformation
        {
            var provider = (T)Activator.CreateInstance(typeof(T), control);
            return new AutomationConfigurer(control, new ListProvider(provider));
        }
    }
}
