using System;
using System.Windows.Forms;
using UIA.Fluent.AutomationProviders;
using UIA.Fluent.AutomationProviders.Tables;

namespace UIA.Fluent.Extensions
{
    public static class AutomationExtensions
    {
        public static AutomationConfigurer ExposeAutomation(this Control control)
        {
            return new AutomationConfigurer(control);
        }

        public static AutomationConfigurer AsValueControl(this Control control, Func<string> getter, Action<string> setter)
        {
            return new AutomationConfigurer(control, new ValueControlProvider(control, getter, setter));
        }

        public static AutomationConfigurer AsTable(this DataGridView dataGridView)
        {
            return new AutomationConfigurer(dataGridView, new TableControlProvider(new DataGridTableInformation(dataGridView)));
        }
    }
}
