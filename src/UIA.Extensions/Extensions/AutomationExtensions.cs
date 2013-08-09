using System;
using System.Windows.Forms;
using UIA.Extensions.AutomationProviders;
using UIA.Extensions.AutomationProviders.Defaults.Tables;
using UIA.Extensions.AutomationProviders.Tables;

namespace UIA.Extensions.Extensions
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

        public static AutomationConfigurer AsTable(this DataGridView control)
        {
            return control.AsTable<DataGridTableInformation>();
        }

        public static AutomationConfigurer AsTable<T>(this Control control) where T : TableInformation
        {
            var provider = (T)Activator.CreateInstance(typeof (T), control);
            return new AutomationConfigurer(control, new TableProvider(provider));
        }
    }
}
